// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leadtools.Medical.Workstation.Interfaces.Presenters;
using Leadtools.Medical.Workstation.UI.Factory;
using Leadtools.Medical.Workstation;
using Leadtools.Medical.Workstation.Interfaces;
using Leadtools.Dicom.Common.DataTypes;
using Leadtools.Demos.Workstation.Configuration;
using System.IO;
using Leadtools.DicomDemos;
using Leadtools.Dicom.Scu;
using Leadtools.Dicom.Common.DataTypes.MediaCreation;
using Leadtools.Dicom;

namespace Leadtools.Demos.Workstation
{
   class PacsMediaInformationPresenter : MediaInformationPresenter <IPacsMediaInformationView>
   {
      #region Public
         
         #region Methods
         
            protected override void DoInitializeView ( WorkstationContainer container, IPacsMediaInformationView view)
            {
               __View = view ;
               
               __WorkstationScp = CreateWorkstationScp ( ) ;
               
               __View.Initialize ( Enum.GetValues ( typeof (MediaType) ).OfType <MediaType> ( ).ToArray ( ), 
                                   new RequestPriority[] { RequestPriority.High, RequestPriority.Medium, RequestPriority.Low } ) ;

               __View.ClearInstancesAfterRequest = true ;
               __View.MediaType                  = MediaType.Default ;
               __View.NumberOfCopies             = 1 ;
               __View.Prioirty                   = RequestPriority.Medium ;
               
               __View.SendMediaCreationRequest += new EventHandler ( __View_SendMediaCreationRequest ) ;
               __View.VerifySelectedServer     += new EventHandler ( __View_VerifySelectedServer ) ;
               __View.SelectedServerChanged    += new EventHandler ( __View_SelectedServerChanged ) ;
               
               container.EventBroker.Subscribe <WorkstationScpChangedEventArgs> ( WorkstationScpChanged ) ;
               
               SetViewPacs ( ) ;

               ConfigurationData.ValueChanged += new EventHandler(ConfigurationData_ValueChanged);
               
               UpdateViewUI ( ) ;
               
               view.ActivateView ( container.State.ActiveWorkstation ) ;
            }

         #endregion
         
         #region Properties
         
         #endregion
         
         #region Events
            
         #endregion
         
         #region Data Types Definition
            
         #endregion
         
         #region Callbacks
         
         #endregion
         
      #endregion
      
      #region Protected
         
         #region Methods
         
            protected override void OnDicomInstancesChanged()
            {
               base.OnDicomInstancesChanged ( ) ;
               
               UpdateViewUI ( ) ;
            }
            
            protected override void OnMediaTitleChanged()
            {
               base.OnMediaTitleChanged ( ) ;
               
               UpdateViewUI ( ) ;
            }
            
            
         #endregion
         
         #region Properties
            
         #endregion
         
         #region Events
            
         #endregion
         
         #region Data Members
            
         #endregion
         
         #region Data Types Definition
            
         #endregion
         
      #endregion
      
      #region Private
         
         #region Methods
         
            private ScpInfo CreateWorkstationScp ( )
            {
               DicomAE wsAE = WorkstationShellController.Instance.WorkstationSettings.WorkstationDicomServer ;
               
               
               if ( null != wsAE )
               {
                  return new ScpInfo ( wsAE.AE, wsAE.IPAddress, wsAE.Port, wsAE.Timeout, wsAE.UseTls ) ;
               }
               else
               {
                  return null ;
               }
            }
            
            private ScpInfo[] GetPacs()
            {
               List <ScpInfo> pacs = new List<ScpInfo> ( ) ;
               
               
               if ( null != __WorkstationScp ) 
               {
                  pacs.Add ( __WorkstationScp ) ;
               }
               
               pacs.AddRange ( ConfigurationData.PACS );
               
               return pacs.ToArray ( ) ;
            }
            
            private void SetViewPacs ( )
            {
               __View.SetPacsServers ( GetPacs ( ) ) ;
                                   
               if ( null != __WorkstationScp ) 
               {
                  __View.SelectedServer = __WorkstationScp ;
               }
            }
            
            private void UpdateViewUI ( )
            {
               if ( __Busy ||
                    BurningImagesState.Count == 0 || 
                    __View.MediaTitle.Length == 0 ||
                    !string.IsNullOrEmpty ( ValidateMediaTitle ( __View.MediaTitle ) ) ||
                    __View.SelectedServer == null ) 
               {
                  __View.CanSendCreateRequest = false ;
               }
               else
               {
                  __View.CanSendCreateRequest = true ;
               }
               
               if ( __Busy || __View.SelectedServer == null ) 
               {
                  __View.CanVerify = false ;
               }
               else
               {
                  __View.CanVerify = true ;
               }
            }
            
         #endregion
         
         #region Properties
            
            private IPacsMediaInformationView __View 
            {
               get ;
               set ;
            }
            
            private ScpInfo __WorkstationScp
            {
               get ;
               set ;
            }
            
            private bool __Busy
            {
               get ;
               set ;
            }
            
         #endregion
         
         #region Events
         
            void ConfigurationData_ValueChanged(object sender, EventArgs e)
            {
               SetViewPacs ( ) ;
            }

            void __View_VerifySelectedServer ( object sender, EventArgs e )
            {
               MediaCreationManagementScu scu ;
             
             
               __Busy = true ;
               
               try
               {
                  UpdateViewUI ( ) ;
                  
                  scu = CreateMediaScu ( ) ;
                  
                  try
                  {
                     scu.TestConnection ( ) ;
                     
                     __View.ReportServerVerificationSuccess ( ) ;
                  }
                  catch ( Exception exception ) 
                  {
                     __View.ReportServerVerificationFailure ( GetFormattedErrorMessage ( exception, "Verification Failed." ) ) ;
                  }
               }
               finally
               {
                  __Busy = false ;
                  
                  UpdateViewUI ( ) ;
               }
            }

            void __View_SendMediaCreationRequest ( object sender, EventArgs e )
            {
               MediaCreationManagementScu scu ;
               List <MediaCreationReferencedSop> referencedSops ;
               
               __Busy = true ;
               
               try
               {
                  UpdateViewUI ( ) ;
                  
                  scu = CreateMediaScu ( ) ;
                  
                  scu.MediaSopInstanceUID       = Utils.GenerateDicomUniqueIdentifier ( ) ;
                  scu.NumberOfCopies            = __View.NumberOfCopies ;
                  scu.Priority                  = __View.Prioirty ;
                  scu.MediaFileSetID            = __View.MediaTitle ;
                  scu.LabelText                 = __View.LabelText ;
                  scu.IncludeDisplayApplication = ( __View.IncludeDisplayApplication ) ? YesNo.Yes : YesNo.No ;
                  referencedSops                = GetMediaReferencedSops ( __View.MediaType ) ;
                  
                  try
                  {
                     MediaCreationManagement mediaObject = scu.CreateMedia ( referencedSops ) ;
                     
                     
                     if ( mediaObject.ExecutionStatus == null || mediaObject.ExecutionStatus.ExecutionStatus == null )
                     {
                        scu.UpdateMediaObjectStatus ( mediaObject ) ;
                     }
                     
                     if ( mediaObject.ExecutionStatus.ExecutionStatus.Value != ExecutionStatus.Failure )
                     {
                        __View.ReportMediaCreationSuccess ( ) ;
                     }
                     else
                     {
                        __View.ReportMediaCreationFailure ( "Request sent but failed to execute on the server. Server status is:\n" + 
                                                            mediaObject.ExecutionStatus.ExecutionStatusInfo.ToString ( ) ) ;
                     }
                     
                     __View.MediaTitle = string.Empty ;
                     
                     if ( __View.ClearInstancesAfterRequest ) 
                     {
                        try
                        {
                           OnClearInstances ( ) ;
                        }
                        catch {}
                     }
                  }
                  catch ( Exception exception ) 
                  {
                     __View.ReportMediaCreationFailure ( GetFormattedErrorMessage ( exception, "Media Creation Failed" )  ) ;
                  }
               }
               finally
               {
                  __Busy = false ;
                  
                  UpdateViewUI ( ) ;
               }
            }

            private MediaCreationManagementScu CreateMediaScu ( )
            {
               DicomScp scp ;
               MediaCreationManagementScu scu ;
             
             
               scp = new DicomScp ( Utils.ResolveIPAddress ( __View.SelectedServer.Address ), 
                                    __View.SelectedServer.AETitle, 
                                    __View.SelectedServer.Port ) ;
                                    
               scu = new MediaCreationManagementScu ( ConfigurationData.WorkstationClient.AETitle, scp ) ;
               
               return scu;
            }
            
            
            private string GetFormattedErrorMessage ( Exception exception, string baseMessage ) 
            {
               if ( exception is Leadtools.Dicom.Scu.Common.ClientConnectionException ) 
               {
                  baseMessage = string.Format ( "{0}\n{1}\nDICOM Code: {2}\n{3}", 
                                                baseMessage,
                                                exception.Message,
                                                ( ( Leadtools.Dicom.Scu.Common.ClientConnectionException )exception ).Code,
                                                DicomException.GetCodeMessage ( ( ( Leadtools.Dicom.Scu.Common.ClientConnectionException )exception ).Code ) ) ;
               }
               else if ( exception is Leadtools.Dicom.Scu.Common.ClientAssociationException )
               {
                  baseMessage = string.Format ( "{0}\n{1}\nDICOM Reason: {2}\n{3}", 
                                                baseMessage,
                                                exception.Message,
                                                ( ( Leadtools.Dicom.Scu.Common.ClientAssociationException ) exception ).Reason, 
                                                WorkstationUtils.GetAssociationReasonMessage ( ( ( Leadtools.Dicom.Scu.Common.ClientAssociationException ) exception ).Reason ) ) ;
               }
               else if ( exception is Leadtools.Dicom.Scu.Common.ClientCommunicationException ) 
               {
                  baseMessage = baseMessage + "\n" + exception.Message + "\nDICOM Status: " + ( ( Leadtools.Dicom.Scu.Common.ClientCommunicationException ) exception ).Status ;
               }
               else
               {
                  baseMessage = baseMessage + "\n" + exception.Message  ;
               }
               
               return baseMessage ;
            }

            private void WorkstationScpChanged ( object sender, WorkstationScpChangedEventArgs args ) 
            {
               __WorkstationScp = CreateWorkstationScp ( ) ;
               
               SetViewPacs ( ) ;
            }

            void __View_SelectedServerChanged ( object sender, EventArgs e )
            {
               UpdateViewUI ( ) ;
            }

         #endregion
         
         #region Data Members
         
            
         
         #endregion
         
         #region Data Types Definition
            
         #endregion
         
      #endregion
      
      #region internal
         
         #region Methods
            
         #endregion
         
         #region Properties
            
         #endregion
         
         #region Events
            
         #endregion
         
         #region Data Types Definition
            
         #endregion
         
         #region Callbacks
            
         #endregion
         
      #endregion
   }
   
   public enum MediaType
   {
      Default,
      CD,
      DVD
   }
}
