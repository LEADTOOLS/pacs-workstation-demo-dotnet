// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leadtools.Medical.Storage.DataAccessLayer;
using Leadtools.Medical.DataAccessLayer;
using System.ComponentModel;
using Leadtools.Medical.Workstation.Commands;
using Leadtools.Medical.Workstation;
using System.Windows.Forms;

namespace Leadtools.Demos.Workstation
{
   class MediaBurningManagerController <T> where T : class, IMediaInformationView
   {
      public MediaBurningManagerController 
      ( 
         WorkstationContainer container,
         IMediaBurningManagerView<T> mediaBurningManagerView,
         ClientQueryDataSet dicomInformationState,
         MediaInformationPresenter<T> mediaCreationPresenter,
         ICommand displayViewCommand
      ) 
      {
         MediaBurningManagerView = mediaBurningManagerView ;
         DicomInformationState   = dicomInformationState ;
         
         
         if ( !container.State.DataServices.IsRegistered <BindingList<ClientQueryDataSet.ImagesRow>> ( ) )
         {
            __BurningImages = new BindingList<ClientQueryDataSet.ImagesRow> ( ) ;
            
            container.State.DataServices.Register <BindingList<ClientQueryDataSet.ImagesRow>> ( __BurningImages ) ;
         }
         else
         {
            __BurningImages = container.State.DataServices.Get <BindingList<ClientQueryDataSet.ImagesRow>> ( ) ;
         }
         
         mediaBurningManagerView.DicomInstancesSelectionView.SetState ( __BurningImages ) ;
                  
         __MediaCreationPresenter = mediaCreationPresenter ;
         
         if ( mediaCreationPresenter.CanInitializeView ( container ) )
         {
            mediaCreationPresenter.Initialize ( container, mediaBurningManagerView.MediaInformationView) ;
         }
         
         DisplayViewCommand = displayViewCommand ;

         __MediaCreationPresenter.ClearInstances    += new EventHandler ( __MediaCreationPresenter_ClearInstnaces ) ;
         MediaBurningManagerView.CloseViewRequested += new EventHandler ( __MediaBurningManager_CloseViewRequested ) ;
      }
      
      public void AddSeriesToBurningManager 
      ( 
         ClientQueryDataSet.StudiesRow study, 
         ClientQueryDataSet.SeriesRow series 
      )
      {
         string seriesInstanceUID = series.SeriesInstanceUID ;
         
         ClientQueryDataSet.ImagesRow[] images =  DicomInformationState.Images.Where ( p=>p.SeriesInstanceUID == seriesInstanceUID  ).ToArray ( ) ;
         
         
         if ( null == images || images.Length == 0 )
         {
            images = FillImages ( series, DicomInformationState ) ;
         }
         
         if ( !string.IsNullOrEmpty ( study.PatientName ) )
         {
            __MediaCreationPresenter.SetCurrentPatient ( study.PatientName, MediaBurningManagerView.MediaInformationView ) ;
         }
         
         MediaBurningManagerView.DicomInstancesSelectionView.AddSeries ( study, series, images ) ;
         
         DisplayViewCommand.Execute ( ) ;
      }
      
      public IMediaBurningManagerView <T> MediaBurningManagerView
      {
         get ;
         private set ;
      }
      
      public ClientQueryDataSet DicomInformationState
      {
         get ;
         private set ;
      }
      
      public ICommand DisplayViewCommand
      {
         get;
         private set ;
      }
      
      private ClientQueryDataSet.ImagesRow[] FillImages 
      ( 
         ClientQueryDataSet.SeriesRow seriesRow, 
         ClientQueryDataSet informationDS 
      )
      {
         IStorageDataAccessAgent storageDataAccess ;
         MatchingParameterCollection matchingcollection ;
         MatchingParameterList       matchingList ;
         Leadtools.Medical.Storage.DataAccessLayer.MatchingParameters.Study matchingStudy ;
         Leadtools.Medical.Storage.DataAccessLayer.MatchingParameters.Series matchingSeries ;
         List <ClientQueryDataSet.ImagesRow> imageRows ;
         
         
         storageDataAccess = DataAccessServices.GetDataAccessService<IStorageDataAccessAgent> ( ) ;
         
         if ( null == storageDataAccess ) 
         {
            return new ClientQueryDataSet.ImagesRow [] {} ;
         }
         
         matchingcollection = new MatchingParameterCollection ( ) ;
         matchingList       = new MatchingParameterList ( ) ;
         matchingStudy      = new Leadtools.Medical.Storage.DataAccessLayer.MatchingParameters.Study ( seriesRow.StudyInstanceUID ) ;
         matchingSeries     = new Leadtools.Medical.Storage.DataAccessLayer.MatchingParameters.Series (seriesRow.SeriesInstanceUID ) ;
         
         matchingList.Add ( matchingStudy ) ;
         matchingList.Add ( matchingSeries ) ;
         matchingcollection.Add ( matchingList ) ;
         
         CompositeInstanceDataSet instances = storageDataAccess.QueryCompositeInstances ( matchingcollection ).ToCompositeInstanceDataSet() ;
         
         imageRows = new List<ClientQueryDataSet.ImagesRow> ( ) ;
         
         foreach ( CompositeInstanceDataSet.InstanceRow instance in instances.Instance ) 
         {
            ClientQueryDataSet.ImagesRow imageRow ;
            
            
            imageRow = informationDS.Images.NewImagesRow ( ) ;
            
            imageRow.StudyInstanceUID  = seriesRow.StudyInstanceUID ;
            imageRow.SeriesInstanceUID = seriesRow.SeriesInstanceUID ;
            imageRow.SOPInstanceUID    = instance.SOPInstanceUID ;
            imageRow.InstanceNumber    = instance.IsInstanceNumberNull ( ) ? string.Empty : instance.InstanceNumber.ToString ( ) ;
            imageRow.SOPClassUID       = instance.IsSOPClassUIDNull ( ) ? string.Empty : instance.SOPClassUID ;
            
            informationDS.Images.AddImagesRow ( imageRow ) ;
            imageRows.Add ( imageRow ) ;
            
            __BurningImages.Add ( imageRow ) ;
         }
         
         return imageRows.ToArray ( ) ;
      }
      
      void __MediaCreationPresenter_ClearInstnaces ( object sender, EventArgs e )
      {
         MediaBurningManagerView.DicomInstancesSelectionView.ClearItems ( ) ;
         
         DicomInformationState.Clear ( ) ;
      }
      
      void __MediaBurningManager_CloseViewRequested ( object sender, EventArgs e )
      {
         if ( MediaBurningManagerView is Form )
         {
            Form view = (Form) MediaBurningManagerView ;
            
            if ( view.Owner != null ) 
            {
               view.Owner.Focus ( ) ;
            }
            
            view.Hide ( ) ;
         }
      }
      
      private MediaInformationPresenter<T> __MediaCreationPresenter
      {
         get ;
         set ;
      }

      private BindingList <ClientQueryDataSet.ImagesRow> __BurningImages 
      {
         get ;
         set ;
      }
   }
}
