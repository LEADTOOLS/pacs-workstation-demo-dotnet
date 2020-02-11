// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leadtools.Medical.Workstation.UI.Factory;
using Leadtools.Medical.Workstation.UI;
using Leadtools.Medical.Winforms;
using Leadtools.Medical.Workstation.Commands;
using System.Windows.Forms;
using Leadtools.Medical.DataAccessLayer;
using Leadtools.Medical.Workstation.DataAccessLayer;
using Leadtools.Medical.Storage.DataAccessLayer;
using Leadtools.Medical.Workstation;
using Leadtools.Demos.Workstation.Configuration;
using System.IO;
using Leadtools.Dicom.Scu;
using Leadtools.Medical.Workstation.Client.Pacs;
using Leadtools.Medical.Workstation.UI.Commands;
using Leadtools.DicomDemos;
using Leadtools.Dicom.Scu.Common;
using Leadtools.Medical.Workstation.Client;
using Leadtools.Dicom;
using Leadtools.Medical.Workstation.Loader;
using Leadtools.MedicalViewer;
using System.Drawing;
using Leadtools.ImageProcessing;
using Leadtools.Drawing;
using System.ComponentModel;
using Leadtools.Annotations;

namespace Leadtools.Demos.Workstation
{
   class WorkstationContainerPresenter
   {
      #region Public
         
         #region Methods
         
            public WorkstationContainerPresenter 
            ( 
               IWorkstationContainer view, 
               ClientQueryDataSet loadingDataSetState
            ) 
            {
               View = view ;

               View.Load += new EventHandler ( View_Load ) ;
               
               __FeaturesCommand     = new Dictionary<string,ICommand> ( ) ;
               __LoadingDataSetState = loadingDataSetState ;
            }
         
         #endregion
         
         #region Properties
         
            public IWorkstationContainer View
            {
               get ;
               private set ;
            }
            
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
         
            private void UpdateViewUI ( )
            {

            }
            
            private void RegisterViewEvents ( )
            {
               ConfigureSearch ( ) ;

               ConfigureViewer ( ) ;

               ConfigureWSConfig ( ) ;

               ConfigureMediaCreation ( ) ;
               
               ConfigureLocalMediaCreation ( ) ;
               
               if ( __LocalMediaCreationController != null || __MediaCreationController != null )
               {
                  View.CanCreateMedia = true ;

                  View.DoCreateMedia += new EventHandler ( View_DoCreateMedia ) ;
               }

               ConfigureListenerService ( ) ;

               ConfigureUserAccounts ( ) ;

               ConfigureQueueManager ( ) ;
               
               ConfigureToggleFullScreenFeature ( ) ;
               
               ConfigureShowHelpFeature ( ) ;

               ExecuteFeature ( UIElementKeys.SearchStudies ) ;
            }

            private void ConfigureShowHelpFeature()
            {
               if ( !string.IsNullOrEmpty ( ConfigurationData.HelpFile ) && File.Exists ( ConfigurationData.HelpFile ) )
               {
                  View.SetHelpNamescpace ( ConfigurationData.HelpFile ) ;
                  
                  View.CanDisplayHelp = true ;

                  View.DoDisplayHelp += new EventHandler ( View_DoDisplayHelp ) ;
               }
               else
               {
                  View.CanDisplayHelp = false ;
               }
            }

            private void ConfigureToggleFullScreenFeature ( )
            {
               ToggleFullScreenCommand  fullScreenCmd ;
               
               
               fullScreenCmd = new ToggleFullScreenCommand ( View.DisplayContainer ) ;
               
               __FeaturesCommand.Add ( FullScreenFeature, fullScreenCmd ) ;

               fullScreenCmd.CommandExecuted += new EventHandler(fullScreenCmd_CommandExecuted);
               
               View.CanToggleFullScreen = true ;

               View.DoToggleFullScreen += new EventHandler ( View_DoToggleFullScreen ) ;
               View.ExitFullScreen     += new EventHandler ( View_ExitFullScreen ) ;
            }

            private void ConfigureSearch ( )
            {
               if ( WorkstationUIFactory.Instance.IsControlRegistered ( UIElementKeys.SearchStudies ) )
               {
                  __SearchStudies = WorkstationUIFactory.Instance.GetWorkstsationControl <SearchStudies> ( UIElementKeys.SearchStudies ) ;
                  
                  View.CanSearch = true ;

                  View.DoSearch += new EventHandler ( View_DoSearch ) ;
                  
                  __FeaturesCommand.Add ( UIElementKeys.SearchStudies, CreateDisplayControlCommand ( __SearchStudies ) ) ;
               }
               else
               {
                  View.CanSearch = false ;
               }
            }
            
            private void ConfigureViewer ( )
            {
               if ( WorkstationUIFactory.Instance.IsControlRegistered ( UIElementKeys.WorkstationViewer ) )
               {
                  __WorkstationViewer = WorkstationUIFactory.Instance.GetWorkstsationControl <WorkstationViewer> ( UIElementKeys.WorkstationViewer ) ;
                  __WorkstationViewer.Options3D.SupportPanoramic = true;
                  
                  View.CanViewImages = true ;

                  View.DoDisplayViewer += new EventHandler ( View_DoDisplayImages ) ;
                  
                  if ( null != __SearchStudies ) 
                  {
                     __SearchStudies.LoadSeries    += new EventHandler<ProcessSeriesEventArgs> ( __SearchStudies_LoadSeries ) ;
                     __SearchStudies.DisplayViewer += new EventHandler                         ( __SearchStudies_DisplayViewer ) ;
                  }
                  
                  __FeaturesCommand.Add ( UIElementKeys.WorkstationViewer, CreateDisplayControlCommand ( __WorkstationViewer ) ) ;
                  
                  __WorkstationViewer.SeriesDropLoaderRequested += new EventHandler<SeriesDropLoaderRequestedEventArgs> ( __WorkstationViewer_SeriesDropLoaderRequested ) ;
                  __WorkstationViewer.SeriesLoadingCompleted    += new EventHandler<LoadSeriesEventArgs>                ( __WorkstationViewer_SeriesLoadingCompleted ) ;
                  __WorkstationViewer.SeriesLoadingError        += new EventHandler<LoadSeriesErrorEventArgs>           ( __WorkstationViewer_SeriesLoadingError ) ;

                  __WorkstationViewer.ViewerContainer.State.ActiveCellChanged += new EventHandler ( OnAnnotationFeatureExecuted ) ;
                  
                  
                  __WorkstationViewer.ViewerContainer.EventBroker.SubscribeForFeature ( WorkstationFeatures.RectangleFeatureId, OnAnnotationFeatureExecuted ) ;
                  __WorkstationViewer.ViewerContainer.EventBroker.SubscribeForFeature ( WorkstationFeatures.EllipseFeatureId, OnAnnotationFeatureExecuted ) ;
                  __WorkstationViewer.ViewerContainer.EventBroker.SubscribeForFeature ( WorkstationFeatures.ArrowFeatureId, OnAnnotationFeatureExecuted ) ;
                  __WorkstationViewer.ViewerContainer.EventBroker.SubscribeForFeature ( WorkstationFeatures.TextFeatureId, OnAnnotationFeatureExecuted ) ;
                  __WorkstationViewer.ViewerContainer.EventBroker.SubscribeForFeature ( WorkstationFeatures.HilightFeatureId, OnAnnotationFeatureExecuted ) ;
                  
                  __WorkstationViewer.ViewerContainer.EventBroker.SubscribeForFeature ( WorkstationFeatures.RulerFeatureId, OnAnnotationFeatureExecuted ) ;
                  __WorkstationViewer.ViewerContainer.EventBroker.SubscribeForFeature ( WorkstationFeatures.PolyRulerFeatureId, OnAnnotationFeatureExecuted ) ;
                  __WorkstationViewer.ViewerContainer.EventBroker.SubscribeForFeature ( WorkstationFeatures.AngleFeatureId, OnAnnotationFeatureExecuted ) ;
               }
               else
               {
                  View.CanViewImages = false ;
               }
            }

            private void OnAnnotationFeatureExecuted ( object sender, EventArgs args ) 
            {
               if ( null != __WorkstationViewer.ViewerContainer.State.ActiveCell ) 
               {
                  SetCellConfiguration ( __WorkstationViewer.ViewerContainer.State.ActiveCell ) ;
               }
            }

            private static void SetCellConfiguration ( MedicalViewerBaseCell cell )
            {
               if ( cell.DefaultAnnotationColor != ConfigurationData.AnnotationDefaultColor ) 
               {
                  cell.DefaultAnnotationColor = ConfigurationData.AnnotationDefaultColor;
               }
               
               if ( cell.DefaultAnnotationUnit != ConfigurationData.MeasurementUnit )
               {
                  cell.DefaultAnnotationUnit  = ConfigurationData.MeasurementUnit;
               }
            }

            private void ConfigureWSConfig ( )
            {
               if ( WorkstationUIFactory.Instance.IsControlRegistered ( UIElementKeys.WorkstationConfiguration ) )
               {
                  __WorkstationConfiguration = WorkstationUIFactory.Instance.GetWorkstsationControl <WorkstationConfiguration> ( UIElementKeys.WorkstationConfiguration ) ;
                  
                  View.CanConfigure = true ;
                  
                  View.DoConfigure += new EventHandler ( View_DoConfigure ) ;
                  
                  __FeaturesCommand.Add ( UIElementKeys.WorkstationConfiguration, CreateDisplayControlCommand ( __WorkstationConfiguration ) ) ;
               }
               else
               {
                  View.CanConfigure = false ;
               }
            }

            private void ConfigureMediaCreation ( )
            {
               if ( WorkstationUIFactory.Instance.IsViewRegistered <IMediaBurningManagerView<IPacsMediaInformationView>> ( ) ) 
               {
                  IMediaBurningManagerView<IPacsMediaInformationView> pacsBurningView ;
                  
                  
                  pacsBurningView = WorkstationUIFactory.Instance.GetWorkstationView <IMediaBurningManagerView<IPacsMediaInformationView>> ( ) ;
                  
                  if ( null != __SearchStudies && ConfigurationData.SupportDicomCommunication ) 
                  {
                     __SearchStudies.AddSeriesToMediaBurning += new EventHandler<ProcessSeriesEventArgs> ( __SearchStudies_AddSeriesToMediaBurning ) ;
                  }
                  
                  ICommand displayMediaBurningView ;
                  
                  
                  if ( pacsBurningView is MediaBurningManagerView ) 
                  {
                     MediaBurningManagerView mediaManager = (MediaBurningManagerView) pacsBurningView ;
                     
                     displayMediaBurningView = new ShowMediaManagerCommand ( View.DisplayContainer, mediaManager, MediaViewType.Pacs ) ;
                  }
                  else if ( pacsBurningView is Form )
                  {
                      displayMediaBurningView = new ShowModelessDialogCommand ( View.DisplayContainer, (Form) pacsBurningView ) ;
                  }
                  else
                  {
                     return ;
                  }
                     
                  __FeaturesCommand.Add ( UIElementKeys.MediaBurningManagerView, displayMediaBurningView ) ;
                  
                  MediaInformationPresenter<IPacsMediaInformationView> presenter = new PacsMediaInformationPresenter ( ) ;
                  
                  __MediaCreationController = new MediaBurningManagerController <IPacsMediaInformationView> ( __WorkstationViewer.ViewerContainer,
                                                                                                              pacsBurningView, 
                                                                                                              __LoadingDataSetState,
                                                                                                              presenter,
                                                                                                              displayMediaBurningView ) ;
               }
            }
            
            private void ConfigureLocalMediaCreation ( )
            {
               if ( WorkstationUIFactory.Instance.IsViewRegistered <IMediaBurningManagerView<ILocalMediaInformationView>> ( ) ) 
               {
                  IMediaBurningManagerView<ILocalMediaInformationView> localMediaBurningManager ;
                  
                  
                  
                  localMediaBurningManager = WorkstationUIFactory.Instance.GetWorkstationView <IMediaBurningManagerView<ILocalMediaInformationView>> ( ) ;
                  
                  if ( null != __SearchStudies ) 
                  {
                     __SearchStudies.AddSeriesToLocalMediaBurning += new EventHandler<ProcessSeriesEventArgs> ( __SearchStudies_AddSeriesToLocalMediaBurning ) ;
                  }
                  
                  ICommand displayMediaBurningView ;
                  
                  if ( localMediaBurningManager is MediaBurningManagerView ) 
                  {
                     MediaBurningManagerView mediaManager = (MediaBurningManagerView) localMediaBurningManager ;
                     
                     displayMediaBurningView = new ShowMediaManagerCommand ( View.DisplayContainer, mediaManager, MediaViewType.Local ) ;
                  }
                  else if ( localMediaBurningManager is Form )
                  {
                     displayMediaBurningView = new ShowModelessDialogCommand ( View.DisplayContainer, (Form) localMediaBurningManager ) ;
                  }
                  else
                  {
                     return ;
                  }
                     
                  if ( !__FeaturesCommand.ContainsKey ( UIElementKeys.MediaBurningManagerView ) )
                  {
                     __FeaturesCommand.Add ( UIElementKeys.MediaBurningManagerView, displayMediaBurningView ) ;
                  }
                  
                  LocalMediaInformationPresenter presenter = new LocalMediaInformationPresenter ( ) ;
                  
                  __LocalMediaCreationController = new MediaBurningManagerController <ILocalMediaInformationView> ( __WorkstationViewer.ViewerContainer,
                                                                                                                    localMediaBurningManager,
                                                                                                                    __LoadingDataSetState,
                                                                                                                    presenter,
                                                                                                                    displayMediaBurningView ) ;
               }
            }

            private void ConfigureListenerService ( )
            {
               if ( WorkstationUIFactory.Instance.IsControlRegistered ( UIElementKeys.StorageListenerService ) )
               {
                  __StorageListenerService = WorkstationUIFactory.Instance.GetWorkstsationControl <StorageListenerService> ( UIElementKeys.StorageListenerService ) ;
                  
                  View.CanManageService = true ;

                  View.DoManageService += new EventHandler ( View_DoManageService ) ;
                  
                  __FeaturesCommand.Add ( UIElementKeys.StorageListenerService, CreateDisplayControlCommand ( __StorageListenerService ) ) ;
               }
               else
               {
                  View.CanManageService = false ;
               }
            }

            private void ConfigureUserAccounts ( )
            {
               if ( WorkstationUIFactory.Instance.IsControlRegistered ( UIElementKeys.UsersAccounts ) )
               {
                  __WorkstationUsersManagement = WorkstationUIFactory.Instance.GetWorkstsationControl <UsersAccounts> ( UIElementKeys.UsersAccounts ) ;
                  
                  View.CanManageUsers = true ;

                  View.DoManageUsers += new EventHandler ( View_DoManageUsers ) ;
                  
                  __FeaturesCommand.Add ( UIElementKeys.UsersAccounts, CreateDisplayControlCommand ( __WorkstationUsersManagement ) ) ;
               }
               else
               {
                  View.CanManageUsers = false ;
               }
            }

            private void ConfigureQueueManager ( )
            {
               if ( WorkstationUIFactory.Instance.IsControlRegistered ( UIElementKeys.QueueManager ) )
               {
                  __QueueManager = WorkstationUIFactory.Instance.GetWorkstsationControl <QueueManager> ( UIElementKeys.QueueManager ) ;
                  
                  View.CanViewQueueManager = true ;

                  View.DoViewQueueManager += new EventHandler ( View_DoViewQueueManager ) ;
                  
                  if ( null != __SearchStudies && ConfigurationData.SupportDicomCommunication ) 
                  {
                     __SearchStudies.StoreSeries    += new EventHandler<StoreSeriesEventArgs>   ( __SearchStudies_StoreSeries ) ;
                     __SearchStudies.RetrieveSeries += new EventHandler<StoreSeriesEventArgs>   ( __SearchStudies_RetrieveSeries );
                  }
                  
                  __QueueManager.SeriesReady += new EventHandler <SeriesInformationEventArgs> ( Instance_SeriesReady ) ;
                  
                  __FeaturesCommand.Add ( UIElementKeys.QueueManager, new ShowModelessDialogCommand ( View.DisplayContainer, __QueueManager ) ) ;
               }
               else
               {
                  View.CanViewQueueManager = false ;
               }
            }
            
            private void LoadSeriesInViewer 
            ( 
               string patientID, 
               string studyInstanceUID, 
               string seriesInstanceUID, 
               DicomClientMode clientMode 
            ) 
            {
               if ( null == __WorkstationViewer ) 
               {
                  return ;
               }
               
               MedicalViewerLoader loader ;
               
               
               loader = new MedicalViewerLoader ( DicomClientFactory.CreateRetrieveClient ( clientMode ) ) ;
               
               InitMedicalViewerLoader ( loader ) ;
               
               if ( __WorkstationViewer.InvokeRequired )
               {
                  __WorkstationViewer.Invoke ( new MethodInvoker ( delegate 
                     { 
                        __WorkstationViewer.LoadSeries ( patientID,
                                                         studyInstanceUID, 
                                                         seriesInstanceUID, 
                                                         loader ) ;
                     } ) ) ;
               }
               else
               {
                  __WorkstationViewer.LoadSeries ( patientID,
                                                   studyInstanceUID, 
                                                   seriesInstanceUID, 
                                                   loader ) ;
               }
            }
            
            private void InitMedicalViewerLoader ( MedicalViewerLoader loader )
            {
               loader.LazyLoading            = ConfigurationData.ViewerLazyLoading.Enable ;
               loader.ViewerPreLoadedImages  = ConfigurationData.ViewerLazyLoading.HiddenImages ;
               loader.DisplayOrientation     = WorkstationShellController.Instance.DisplayOrientation ;
            }

            private DisplayControlCommand CreateDisplayControlCommand ( Control displayedControl )
            {
               DisplayControlCommand command ;
               
               
               command = new DisplayControlCommand ( View.DisplayContainer, displayedControl ) ;

               command.DisplayControlExecuted += new EventHandler<DisplayControlEventArgs>(command_DisplayControlExecuted);
               
               return command ;
            }

            void command_DisplayControlExecuted ( object sender, DisplayControlEventArgs e )
            {
               KeyValuePair <string,ICommand> keyValuePait = __FeaturesCommand.Where ( n=>n.Value == sender ).FirstOrDefault ( ) ;
               
               View.OnDisplayedControlChanged ( keyValuePait.Key ) ;
            }
            
            private void ExecuteFeature ( string feature ) 
            {
               if ( __FeaturesCommand.ContainsKey ( feature ) )
               {
                  __FeaturesCommand [ feature ].Execute ( ) ;
               }
            }
            
            private void PopulateState ( ClientQueryDataSet.StudiesRow study, ClientQueryDataSet.SeriesRow series )
            {
               if ( __LoadingDataSetState.Studies.FindByStudyInstanceUID ( study.StudyInstanceUID ) == null )
               {
                  __LoadingDataSetState.Studies.ImportRow ( study ) ; 
               }
               
               if ( __LoadingDataSetState.Series.FindBySeriesInstanceUID ( series.SeriesInstanceUID ) == null ) 
               {
                  __LoadingDataSetState.Series.ImportRow ( series ) ;
               }
            }
            
            private StudyInformation GetStudyInformation ( ClientQueryDataSet.StudiesRow study )
            {
               StudyInformation studyInfo = new StudyInformation ( study.IsPatientIDNull ( ) ? string.Empty : study.PatientID,
                                                                   study.StudyInstanceUID )   ; 
            
               studyInfo.AccessionNumber        = study.IsAccessionNumberNull ( )  ? string.Empty : study.AccessionNumber ;
               studyInfo.PatientBirthDate       = study.IsPatientBirthDateNull ( ) ? string.Empty : study.PatientBirthDate ;
               studyInfo.PatientName            = study.IsPatientNameNull ( )      ? string.Empty : study.PatientName ;
               studyInfo.PatientSex             = study.IsPatientSexNull ( )       ? string.Empty : study.PatientSex ;
               studyInfo.ReferringPhysicianName = study.IsReferDrNameNull ( )      ? string.Empty : study.ReferDrName ;
               studyInfo.StudyDate              = study.IsStudyDateNull ( )        ? string.Empty : study.StudyDate ;
               studyInfo.StudyDescription       = study.IsStudyDescriptionNull ( ) ? string.Empty : study.StudyDescription ;
            
               return studyInfo ;
            }

            private SeriesInformation GetSeriesInformation ( StudyInformation studyInfo, ClientQueryDataSet.SeriesRow series )
            {
               string patientID          = studyInfo.PatientID ;
               string studyInstanceUID   = studyInfo.StudyInstanceUID ;
               string seriesInstanceUID  = series.SeriesInstanceUID ;
               string seriesDescription  = series.IsSeriesDescriptionNull ( ) ? string.Empty : series.SeriesDescription ;
               
               SeriesInformation seriesInfo = new SeriesInformation ( patientID, studyInstanceUID, seriesInstanceUID, seriesDescription ) ;
               
               seriesInfo.Modality                       = series.IsModalityNull ( ) ? string.Empty : series.Modality ;
               seriesInfo.NumberOfSeriesRelatedInstances = series.IsFrameCountNull ( ) ? string.Empty : series.FrameCount ;
               seriesInfo.SeriesDate                     = series.IsSeriesDateNull ( ) ? string.Empty : series.SeriesDate ;
               seriesInfo.SeriesNumber                   = series.IsSeriesNumberNull ( ) ? string.Empty : series.SeriesNumber ;
               seriesInfo.SeriesTime                     = series.IsSeriesTimeNull ( ) ? string.Empty : series.SeriesTime ;
               
               return seriesInfo;
            }
            
            private static void FillSeriesThumbnail ( LoadSeriesEventArgs e, SeriesInformation seriesInfo )
            {
               if (e.LoadedSeries.Streamer.SeriesCells.Length > 0)
               {
                  MedicalViewerMultiCell cell = e.LoadedSeries.Streamer.SeriesCells[0];

                  if (cell.VirtualImage != null)
                  {
                     if (cell.VirtualImage[cell.ActiveSubCell].ImageExist)
                     {
                        using ( RasterImage image = cell.VirtualImage[cell.ActiveSubCell].Image.Clone() )
                        {
                           Image thumbImage;

                           if (image.Width != 64 || image.Height != 64)
                           {
                              SizeCommand sizeCommand;

                              
                              sizeCommand = new SizeCommand ( 64, 64, RasterSizeFlags.None);
                              
                              sizeCommand.Run(image);
                           }
                           
                           if ( image.BitsPerPixel != 24 ) 
                           {
                              ColorResolutionCommand colorRes = new ColorResolutionCommand ( ColorResolutionCommandMode.InPlace, 
                                                                                             24, 
                                                                                             RasterByteOrder.Bgr, 
                                                                                             RasterDitheringMethod.None, 
                                                                                             ColorResolutionCommandPaletteFlags.FastMatch, 
                                                                                             null ) ;
                              
                              
                              colorRes.Run ( image ) ;
                           }

                           thumbImage = RasterImageConverter.ConvertToImage ( image, ConvertToImageOptions.InitAlpha ) ;
                           
                           seriesInfo.Thumbnail = thumbImage;
                        }
                     }
                  }
               }
            }

         #endregion
         
         #region Properties
         
            private SearchStudies __SearchStudies
            {
               get ;
               
               set ;
            }
            
            private WorkstationViewer __WorkstationViewer
            {
               get ;
               
               set ;
            }
            
            private WorkstationConfiguration __WorkstationConfiguration
            {
               get ;
               
               set ;
            }
            
            private StorageListenerService __StorageListenerService
            {
               get ;
               
               set ;
            }
            
            private UsersAccounts __WorkstationUsersManagement
            {
               get ;
               
               set ;
            }
            
            
            private QueueManager __QueueManager
            {
               get ;
               set ;
            }
            
            private Dictionary <string, ICommand> __FeaturesCommand
            {
               get ;
               set ;
            }
            
            private ClientQueryDataSet __LoadingDataSetState
            {
               get ;
               set ;
            }
            
            private MediaBurningManagerController <IPacsMediaInformationView> __MediaCreationController
            {
               get ;
               set ;
            }
            
            private MediaBurningManagerController <ILocalMediaInformationView> __LocalMediaCreationController
            {
               get ;
               set ;
            }
            
         #endregion
         
         #region Events
         
            void View_Load ( object sender, EventArgs e )
            {
               try
               {
                  UpdateViewUI ( ) ;
                  
                  RegisterViewEvents ( ) ;
                  
                  if ( ConfigurationData.RunFullScreen )
                  {
                     ExecuteFeature ( FullScreenFeature ) ;
                  }
               }
               catch ( Exception exception ) 
               {
                  ThreadSafeMessager.ShowError ( exception.Message ) ;
               }
            }
            
            void View_DoSearch ( object sender, EventArgs e )
            {
               ExecuteFeature ( UIElementKeys.SearchStudies ) ;
            }

            void View_DoConfigure ( object sender, EventArgs e )
            {
               ExecuteFeature ( UIElementKeys.WorkstationConfiguration ) ;
            }
            
            void View_DoDisplayImages ( object sender, EventArgs e )
            {
               ExecuteFeature ( UIElementKeys.WorkstationViewer ) ;
            }

            void View_DoCreateMedia ( object sender, EventArgs e )
            {
               ExecuteFeature ( UIElementKeys.MediaBurningManagerView ) ;
            }

            void View_DoManageService ( object sender, EventArgs e )
            {
               ExecuteFeature ( UIElementKeys.StorageListenerService ) ;
            }

            void View_DoManageUsers(object sender, EventArgs e)
            {
               ExecuteFeature ( UIElementKeys.UsersAccounts ) ;
            }

            void View_DoViewQueueManager(object sender, EventArgs e)
            {
               ExecuteFeature ( UIElementKeys.QueueManager ) ;
            }
            
            void View_DoToggleFullScreen ( object sender, EventArgs e )
            {
               ExecuteFeature ( FullScreenFeature ) ;
            }
            
            void View_DoDisplayHelp ( object sender, EventArgs e )
            {
               Help.ShowHelp ( View.DisplayContainer, ConfigurationData.HelpFile ) ;
            }
            
            void View_ExitFullScreen ( object sender, EventArgs e )
            {
               if ( __FeaturesCommand.ContainsKey ( FullScreenFeature ) &&
                    __FeaturesCommand [ FullScreenFeature ] is ToggleFullScreenCommand )
               {
                  ToggleFullScreenCommand cmd = (ToggleFullScreenCommand) __FeaturesCommand [ FullScreenFeature ] ;
                  
                  
                  if ( cmd.FullScreen )
                  {
                     ExecuteFeature ( FullScreenFeature ) ;
                  }
               }
            }
            
            void fullScreenCmd_CommandExecuted ( object sender, EventArgs e )
            {
               try
               {
                  View.OnFullScreenChanged ( ( ( ToggleFullScreenCommand ) sender ).FullScreen ) ;
               }
               catch ( Exception exception ) 
               {
                  ThreadSafeMessager.ShowError ( exception.Message ) ;
               }
            }
            
            void __SearchStudies_LoadSeries ( object sender, ProcessSeriesEventArgs e )
            {
               try
               {
                  PopulateState ( e.Study, e.Series ) ;
                  
                  LoadSeriesInViewer ( e.Study.PatientID,
                                       e.Study.StudyInstanceUID, 
                                       e.Series.SeriesInstanceUID, 
                                       ConfigurationData.ClientBrowsingMode ) ;
               }
               catch ( Exception exception ) 
               {
                  ThreadSafeMessager.ShowError ( exception.Message ) ;
               }
            
            }
            
            void __SearchStudies_StoreSeries ( object sender, StoreSeriesEventArgs e )
            {
               try
               {
                  PopulateState ( e.Study, e.Series ) ;
                  
                  if ( !__QueueManager.Visible )
                  {
                     __QueueManager.Show ( View.DisplayContainer ) ;
                  }
                  
                  DicomScp                scp ;
                  string                  patientID ;
                  string                  description ;
                  IStorageDataAccessAgent dataAccess ;
                  Compression             compression ;
                  StoreQueueItemCommand   storeCommand ;
                  
                  if ( !ConfigurationData.Compression.Enable )
                  {
                     compression = Leadtools.Dicom.Scu.Common.Compression.Native ;
                  }
                  else
                  {
                     compression = ( ConfigurationData.Compression.Lossy ) ? Leadtools.Dicom.Scu.Common.Compression.Lossy : Leadtools.Dicom.Scu.Common.Compression.Lossless ;
                  }
                  
                  scp = new Leadtools.Dicom.Scu.DicomScp ( ) ;
                  
                  scp.AETitle     = e.Server.AETitle ;
                  scp.PeerAddress = Utils.ResolveIPAddress ( e.Server.Address ) ;
                  scp.Port        = e.Server.Port ;
                  scp.Timeout     = e.Server.Timeout ;
                  
                  patientID = e.Study.IsPatientIDNull ( ) ? string.Empty : e.Study.PatientID ;
                  
                  description = string.Format ( SeriesInfo, 
                                                e.Study.IsPatientNameNull ( ) ? string.Empty : e.Study.PatientName,
                                                patientID,
                                                e.Series.IsSeriesNumberNull ( ) ? string.Empty : e.Series.SeriesNumber,
                                                e.Series.IsModalityNull ( ) ? string.Empty : e.Series.Modality ) ;
                                                
                  dataAccess = DataAccessServices.GetDataAccessService <IStorageDataAccessAgent> ( ) ;
                  
                  if ( null == dataAccess ) 
                  {
                     throw new InvalidOperationException ( "Storage Service is not registered." ) ;
                  }
                  
                  StoreClient client = new StoreClient ( ConfigurationData.WorkstationClient.ToAeInfo ( ),
                                                         scp,
                                                         compression,
                                                         dataAccess ) ;
                  
                  
                  storeCommand = new StoreQueueItemCommand ( new SeriesInformation ( patientID, e.Study.StudyInstanceUID, e.Series.SeriesInstanceUID, description ), client ) ;
                  
                  QueueManager.Instance.AddCommand ( storeCommand ) ;
               }
               catch ( Exception exception ) 
               {
                  ThreadSafeMessager.ShowError ( exception.Message ) ;
               }
            }
            
            private void __SearchStudies_RetrieveSeries ( object sender, StoreSeriesEventArgs e )
            {
               try
               {
                  PopulateState ( e.Study, e.Series ) ;
                  
                  if ( !__QueueManager.Visible )
                  {
                     __QueueManager.Show ( View.DisplayContainer ) ;
                  }
                  
                  DicomScp                 scp ;
                  string                   patientID ;
                  string                   description ;
                  PacsRetrieveClient       client ;
                  RetrieveQueueItemCommand retrieveCommand ;
                  
                  
                  scp = new DicomScp ( ) ;
                  
                  scp.AETitle     = e.Server.AETitle ;
                  scp.PeerAddress = Utils.ResolveIPAddress ( e.Server.Address ) ;
                  scp.Port        = e.Server.Port ;
                  scp.Timeout     = e.Server.Timeout ;
                  scp.Secure      = e.Server.Secure;
                  
                  patientID = e.Study.IsPatientIDNull ( ) ? string.Empty : e.Study.PatientID ;
                  
                  description = string.Format ( SeriesInfo, 
                                                e.Study.IsPatientNameNull ( ) ? string.Empty : e.Study.PatientName,
                                                patientID,
                                                e.Series.IsSeriesNumberNull ( ) ? string.Empty : e.Series.SeriesNumber,
                                                e.Series.IsModalityNull ( ) ? string.Empty : e.Series.Modality ) ;
                  
                  
                  client = DicomClientFactory.CreatePacsRetrieveClient ( scp ) ;
                  
                  retrieveCommand = new RetrieveQueueItemCommand ( new SeriesInformation ( patientID, 
                                                                                           e.Study.StudyInstanceUID, 
                                                                                           e.Series.SeriesInstanceUID, 
                                                                                           description ), 
                                                                   client ) ;
                                                                   
                  
                  __QueueManager.AddCommand ( retrieveCommand ) ;
               }
               catch ( Exception exception ) 
               {
                  ThreadSafeMessager.ShowError ( exception.Message ) ;
               }
            }
            
            private void __SearchStudies_DisplayViewer(object sender, EventArgs e)
            {
               try
               {
                  ExecuteFeature ( UIElementKeys.WorkstationViewer ) ;
               }
               catch ( Exception exception ) 
               {
                  ThreadSafeMessager.ShowError ( exception.Message ) ;
               }
            }
            
            private void __SearchStudies_AddSeriesToMediaBurning ( object sender, ProcessSeriesEventArgs e )
            {
               try
               {
                  __MediaCreationController.AddSeriesToBurningManager ( e.Study, e.Series ) ;
               }
               catch ( Exception exception ) 
               {
                  ThreadSafeMessager.ShowError ( exception.Message ) ;
               }
            }
            
            private void __SearchStudies_AddSeriesToLocalMediaBurning ( object sender, ProcessSeriesEventArgs e )
            {
               try
               {
                  __LocalMediaCreationController.AddSeriesToBurningManager ( e.Study, e.Series ) ;
               }
               catch ( Exception exception ) 
               {
                  ThreadSafeMessager.ShowError ( exception.Message ) ;
               }
            }
            
            private void Instance_SeriesReady ( object sender, SeriesInformationEventArgs e )
            {
               try
               {
                  LoadSeriesInViewer ( e.PatientId, e.StudyInstanceUID, e.SeriesInstanceUID, DicomClientMode.LocalDb ) ;

                  //we also switch the UI to the workstation viewer next
                  if (null != __WorkstationViewer)
                  {
                     ThreadSafeMessager.Owner.Invoke(new Action( ()=> { ExecuteFeature(UIElementKeys.WorkstationViewer); }));
                  }
               }
               catch ( Exception exception ) 
               {
                  ThreadSafeMessager.ShowError ( exception.Message ) ;
               }
            }
            
            void __WorkstationViewer_SeriesDropLoaderRequested ( object sender, SeriesDropLoaderRequestedEventArgs e )
            {
               try
               {
                  MedicalViewerLoader loader ;
                  
                  
                  if ( !ConfigurationData.SupportLocalQueriesStore )
                  {
                     loader = new MedicalViewerLoader ( DicomClientFactory.CreateRetrieveClient ( ) ) ;
                  }
                  else
                  {
                     loader = new MedicalViewerLoader ( DicomClientFactory.CreateLocalRetrieveClient ( ) ) ;
                  }
                  
                  InitMedicalViewerLoader ( loader ) ;
                  
                  e.SeriesLoader = loader ;
               }
               catch ( Exception exception ) 
               {
                  ThreadSafeMessager.ShowError ( exception.Message ) ;
               }
            }

            void __WorkstationViewer_SeriesLoadingError ( object sender, LoadSeriesErrorEventArgs e )
            {
               if  ( e.Error is Leadtools.Dicom.Scu.Common.ClientCommunicationException ) 
               {
                  WorkstationMessager.ShowError ( View.DisplayContainer,
                                                  "Series Loading Error\n." + e.Error.Message + 
                                                  "\nDICOM Status: " + ( e.Error as Leadtools.Dicom.Scu.Common.ClientCommunicationException ).Status ) ;
               
                  if ( WorkstationMessager.DetailedError )
                  {
                     WorkstationMessager.ShowError ( View.DisplayContainer, e.Error ) ;
                  }
               }
               else if ( e.Error is DicomException )
               {
                  WorkstationMessager.ShowError ( View.DisplayContainer, 
                                                  "Series Loading Error\n." + e.Error.Message + 
                                                  "\nDICOM Error: " + ( e.Error as DicomException ).Code ) ;
               
                  if ( WorkstationMessager.DetailedError )
                  {
                     WorkstationMessager.ShowError ( View.DisplayContainer, e.Error ) ;
                  }
               }
               else
               {
                  System.Diagnostics.Debug.Assert ( false, e.Error.Message ) ;
                  
                  WorkstationMessager.ShowError (  View.DisplayContainer, e.Error ) ;
               }

            }

            void __WorkstationViewer_SeriesLoadingCompleted ( object sender, LoadSeriesEventArgs e )
            {
               try
               {
                  if ( SeriesBrowser.Instance.FindSeries ( e.LoadedSeries.SeriesInstanceUID ) != null )
                  {
                     return ;
                  }
                  
                  ClientQueryDataSet.StudiesRow study ;
                  ClientQueryDataSet.SeriesRow  series ;
                  
                  
                  if ( ( study = __LoadingDataSetState.Studies.FindByStudyInstanceUID ( e.LoadedSeries.StudyInstanceUID ) ) != null )
                  {
                     if ( ( series = __LoadingDataSetState.Series.FindBySeriesInstanceUID ( e.LoadedSeries.SeriesInstanceUID ) ) != null )
                     {
                        StudyInformation  studyInfo ;
                        SeriesInformation seriesInfo ;


                        studyInfo  = GetStudyInformation ( study ) ;
                        seriesInfo = GetSeriesInformation ( studyInfo, series ) ;
                        
                        try
                        {
                           FillSeriesThumbnail ( e, seriesInfo ) ;
                        }
                        catch ( Exception ) 
                        {}
                        
                        SeriesBrowser.Instance.AddSeries ( studyInfo, seriesInfo ) ;
                     }
                  }
               }
               catch ( Exception exception ) 
               {
                  ThreadSafeMessager.ShowError ( exception.Message ) ;
               }
            }
            
         #endregion
         
         #region Data Members
         
            private const string FullScreenFeature = "FullScreen" ;
            private const string ShowHelpFeature   = "ShowHelp" ;
            private const string SeriesInfo = "{0} ({1}) Series \"{2}\" Modality \"{3}\"" ;
            
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
}
