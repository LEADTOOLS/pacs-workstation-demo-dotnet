// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leadtools.Dicom.Common.DataTypes;
using Leadtools.Demos.Workstation.Configuration;
using Leadtools.DicomDemos;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;
using Leadtools.Dicom.Scu;
using System.Net;
using Leadtools.Dicom.Common.DataTypes.MediaCreation;
using Leadtools.Dicom;
using Leadtools.Dicom.Common.Validation;
using Leadtools.Medical.Workstation.Presenters;
using Leadtools.Medical.Workstation;
using Leadtools.Medical.Workstation.Interfaces.Presenters;
using Leadtools.Medical.Workstation.Interfaces;
using Leadtools.Medical.Workstation.UI.Factory;

namespace Leadtools.Demos.Workstation
{

   abstract class MediaInformationPresenter <V> : IWorkstationPresenter <V> where V : class, IMediaInformationView
   {
      private string _internallySetMediaTitle = string.Empty;

      public void SetCurrentPatient ( string patientName, IMediaInformationView view ) 
      {
         if (string.IsNullOrEmpty(view.MediaTitle))
         {
            foreach ( char invalidChar in Path.GetInvalidFileNameChars ( ) )
            {
               patientName = patientName.Replace ( invalidChar, ' ' ) ;
            }
            
            patientName = patientName.Replace ( '^', ' ' ) ; 
            
            if ( patientName.Length > 16 ) 
            {
               patientName = patientName.Substring ( 0, 16 ) ;
            }

            _internallySetMediaTitle = patientName;
            view.MediaTitle = patientName ;
         }
      }

      public void ClearAutoSetCurrentPatient(IMediaInformationView view)
      {
         if (null == view)
            return;

         if (string.IsNullOrEmpty(view.MediaTitle))
            return;

         if (view.MediaTitle == _internallySetMediaTitle)
         {
            _internallySetMediaTitle = string.Empty;
            view.MediaTitle = string.Empty;
         }
      }

      public IMediaInformationView MediaInformationView
      {
         get ;
         private set ;
      }
   
      public BindingList <ClientQueryDataSet.ImagesRow> BurningImagesState
      {
         get ;
         private set ;
      }
      
      public void Initialize ( WorkstationContainer container, V view )
      {
         View            = view ;
         ViewerContainer = container ;
         
         if ( View is IWorkstationInitializer )
         {
            IWorkstationInitializer viewInit ;
            
            
            viewInit = (IWorkstationInitializer) View ;
            
            if ( !viewInit.IsInitialized ) 
            {
               ( ( IWorkstationInitializer ) View ).Initialize ( container ) ;
            }
         }
         
         BurningImagesState = container.State.DataServices.Get <BindingList <ClientQueryDataSet.ImagesRow>> ( ) ;

         BurningImagesState.ListChanged += new ListChangedEventHandler ( burningImagesState_ListChanged ) ;
         
         view.MediaTitleChanged += new EventHandler ( __View_MediaTitleChanged ) ;
         
         MediaInformationView = view ;
         
         View.DeActivated += new EventHandler ( OnViewDeactivated ) ;
         
         DoInitializeView ( container, view ) ;
      }
      
      public void EnsureViewActive ( )
      {
         if ( View != null ) 
         {
            View.EnsureVisible ( ViewerContainer.State.ActiveWorkstation ) ;
         }
      }
      
      public IMediaInformationView View
      {
         get ;
         private set ;
      }
      
      public WorkstationContainer ViewerContainer
      {
         get ;
         private set ;
      }

      public virtual bool CanInitializeView ( WorkstationContainer container )
      {
         return container.State.DataServices.IsRegistered <BindingList <ClientQueryDataSet.ImagesRow>> ( ) ;
      }
      
      public event ErrorEventHandler Error ;

      public event EventHandler ViewDestroyed ;
      
      public event EventHandler ClearInstances ;
      
      protected virtual void OnViewDeactivated ( object sender, EventArgs e )
      {
         if ( null != ViewDestroyed )
         {
            ViewDestroyed ( this, e ) ;
         }
         
         View = null ;
      }
      
      protected virtual void OnExecutionError ( Exception exception )
      {
         if ( null != Error ) 
         {
            Error ( this, new ErrorEventArgs ( exception ) ) ;
         }
      }      
         
      protected virtual void OnMediaTitleChanged ( )
      {
         string errorText;


         errorText = ValidateMediaTitle ( MediaInformationView.MediaTitle ) ;

         if (null != errorText)
         {
            MediaInformationView.MediaTitleValidationError ( errorText ) ;
         }
      }
      
      protected virtual void OnDicomInstancesChanged ( )
      {
         if (BurningImagesState.Count == 0)
            ClearAutoSetCurrentPatient(MediaInformationView);
      }
      
      protected virtual string ValidateMediaTitle ( string mediaTitle )
      {
         if ( mediaTitle.Length <= 16  )
         {
            return null ;
         }
         else
         {
            return "Value can't be more than 16 characters." ;
         }
      }
      
      protected virtual void OnClearInstances()
      {
         if ( null != ClearInstances ) 
         {
            ClearInstances ( this, EventArgs.Empty ) ;
         }
      }
      
      protected virtual List<MediaCreationReferencedSop> GetMediaReferencedSops ( MediaType mediaType )
      {
         List<MediaCreationReferencedSop> referencedSops ;
         string profile ;
         
         referencedSops = new List<MediaCreationReferencedSop> ( ) ;
         profile        = GetProfile ( mediaType ) ;
         
         foreach ( ClientQueryDataSet.ImagesRow image in BurningImagesState ) 
         {
            MediaCreationReferencedSop sop = new MediaCreationReferencedSop ( ) ;
            
            
            sop.SopInstance.ReferencedSopInstanceUid = image.SOPInstanceUID ;
            sop.SopInstance.ReferencedSopClassUid    = image.SOPClassUID ;
            sop.RequestedMediaApplicationProfile     = profile ;
            
            referencedSops.Add ( sop ) ;
         }
         return referencedSops ;
      }
      
      protected virtual string GetProfile ( MediaType mediaType )
      {
         switch ( mediaType ) 
         {
            case MediaType.CD:
            {
               return "STD-GEN-CD" ;
            }
            
            case MediaType.DVD:
            {
               return "STD-GEN-DVD-RAM" ;
            }
            
            default:
            {
               return null ;
            }
         }
      }
      
      protected abstract void DoInitializeView (WorkstationContainer container, V view) ;
      
      void __View_MediaTitleChanged ( object sender, EventArgs e )
      {
         OnMediaTitleChanged ( ) ;
      }

      void burningImagesState_ListChanged ( object sender, ListChangedEventArgs e )
      {
         OnDicomInstancesChanged ( ) ;
      }
   }
}
