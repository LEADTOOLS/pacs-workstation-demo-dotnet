// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System ;
using System.Collections.Generic ;
using System.ComponentModel ;
using System.ComponentModel.Design ;
using System.Drawing ;
using System.Data ;
using System.Text ;
using System.Windows.Forms ;
using System.Linq ;
using Leadtools.Medical.Workstation ;
using Leadtools.Medical.Workstation.UI ;
using Leadtools.Demos.Workstation.Configuration ;
using Leadtools.Medical.Workstation.Commands ;
using Leadtools.Medical.Workstation.UI.Commands ;
using Leadtools.Medical.Winforms;
using Leadtools.Medical.Storage.DataAccessLayer;
using Leadtools.Medical.Workstation.Client;
using Leadtools.DicomDemos;
using Leadtools.Medical.DataAccessLayer;
using Leadtools.Medical.Storage.DataAccessLayer.Configuration;
using Leadtools.Dicom.Scu;
using Leadtools.Dicom.Scu.Common;
using Leadtools.Medical.Workstation.Client.Pacs;
using Leadtools.Medical.Workstation.Loader;
using Leadtools.Dicom;
using Leadtools.MedicalViewer;
using Leadtools.ImageProcessing;
using Leadtools.Drawing;
using Leadtools.Codecs;
using Leadtools.Medical.Workstation.DataAccessLayer.Configuration;
using Leadtools.Medical.Workstation.DataAccessLayer;
using System.IO;
using Leadtools.Medical.Workstation.UI.Factory;



namespace Leadtools.Demos.Workstation
{
   public partial class WorkStationContainer : UserControl, IWorkstationContainer
   {
      #region Public
   
         #region Methods

            public WorkStationContainer ( )
            {
               try
               {
                  InitializeComponent();

                  if ( _DesignMode )
                  {
                     return ;
                  }
                  
                  Init ( ) ;
                  
                  RegisterEvents ( ) ;
                  
                  //SearchButton.PerformClick ( ) ;
               }
               catch ( Exception exception ) 
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                  throw ;
               }
            }      
            
            public void SetHelpNamescpace ( string helpNamespace )
            {
               try
               {
                  _helpProvider.HelpNamespace = ConfigurationData.HelpFile ;
                  
                  _helpProvider.SetShowHelp ( this, true ) ;
               }
               catch ( Exception exception ) 
               {
                  ThreadSafeMessager.ShowError ( exception.Message ) ;
               }
            }
            
            public void OnFullScreenChanged ( bool isFullScreen )
            {
               try
               {
                  if ( isFullScreen )
                  {
                     ItemsToolTip.SetToolTip ( FullScreenButton, "Go Normal Window" ) ;
                  }
                  else
                  {
                     ItemsToolTip.SetToolTip ( FullScreenButton, "Go Full Screen" ) ;
                  }
               }
               catch ( Exception exception ) 
               {
                  ThreadSafeMessager.ShowError ( exception.Message ) ;
               }
            }

            public void OnDisplayedControlChanged ( string uiElement ) 
            {
               try
               {
                  this.BackColor = ControlsDisplayPanel.BackColor ;
                  
                  foreach ( Control control in ContainerItemsAutoHidePanel.Controls )
                  {
                     if ( control is Button )
                     {
                        if ( control.BackColor == Color.LightGray )
                        {
                           control.BackColor = Color.DimGray ;
                        }
                     }
                  }
                  
                  if ( _buttonsFeatures.ContainsKey ( uiElement ) )
                  {
                     _buttonsFeatures[ uiElement ].BackColor = Color.LightGray ;
                  }
               }
               catch ( Exception exception ) 
               {
                  ThreadSafeMessager.ShowError ( exception.Message ) ;
               }
            }
         #endregion
   
         #region Properties
         
            public Button WorkstationViewerButton
            {
               get
               {
                  return ViewerButton ;
               }
            }
         
            public Button SearchStudiesButton
            {
               get
               {
                  return SearchButton ;
               }
            }
            
            public Button WorkstationConfigurationButton
            {
               get
               {
                  return ConfigurationButton ;
               }
            }
            
            public Button WorkstationLogOutButton
            {
               get
               {
                  return LogOutButton ;
               }
            }
            
            public Button WorkstationQueueManagerButton
            {
               get
               {
                  return QueueManagerButton ;
               }
            }
            
            public Button WorkstationMediaBurningButton
            {
               get 
               {
                  return MediaBurningButton ;
               }
            }
            
            public Button WorkstationFullScreenButton
            {
               get
               {
                  return FullScreenButton ;
               }
            }
            
            public Button WorkstationListenerServiceButton
            {
               get
               {
                  return StorageServiceButton ;
               }
            }
            
            public Button WorkstationUsersManagementButton
            {
               get
               {
                  return UserAccessButton ;
               }
            }
            
               
            public Control DisplayContainer
            {
               get { return ControlsDisplayPanel ; }
            }

            public bool CanSearch
            {
               set { SearchStudiesButton.Visible = value ; }
            }

            public bool CanViewImages
            {
               set { WorkstationViewerButton.Visible = value  ; }
            }

            public bool CanConfigure
            {
               set { WorkstationConfigurationButton.Visible = value ; }
            }

            public bool CanManageUsers
            {
               set { WorkstationUsersManagementButton.Visible = value ; }
            }

            public bool CanManageService
            {
               set { WorkstationListenerServiceButton.Visible = value ; }
            }

            public bool CanCreateMedia
            {
               set { WorkstationMediaBurningButton.Visible = value ; }
            }

            public bool CanViewQueueManager
            {
               set { WorkstationQueueManagerButton.Visible = value ; }
            }

            public bool CanDisplayHelp
            {
               set { WSHelpButton.Visible = value ; }
            }

            public bool CanToggleFullScreen
            {
               set { WorkstationFullScreenButton.Visible = value ; }
            }
            
   
         #endregion
   
         #region Events
         
         #endregion
   
         #region Data Types Definition
   
         #endregion
   
         #region Callbacks
         
            public event EventHandler LogOutRequested ;
            
            public event EventHandler DoSearch ;

            public event EventHandler DoDisplayViewer ;

            public event EventHandler DoConfigure ;

            public event EventHandler DoManageUsers ;

            public event EventHandler DoManageService ;

            public event EventHandler DoCreateMedia ;

            public event EventHandler DoViewQueueManager ;

            public event EventHandler DoDisplayHelp ;

            public event EventHandler DoToggleFullScreen ;
            
            public event EventHandler ExitFullScreen ;
            
   
         #endregion
   
      #endregion
   
      #region Protected
   
         #region Methods
         
            protected override bool ProcessKeyPreview ( ref Message m )
            {
               try
               {
                  if ( m.Msg == WM_KEYDOWN && ( m.WParam.ToInt32 ( ) == ( int ) Keys.Escape ))
                  {
                     OnExitFullScreen ( ) ;
                  }
                  
                  return false ;
               }
               catch ( Exception exception ) 
               {
                  ThreadSafeMessager.ShowError ( exception.Message ) ;
                  
                  return false ;
               }
            }

         #endregion
   
         #region Properties

            protected virtual void OnLogOutRequested ( object sender, EventArgs e ) 
            {
               if ( null != LogOutRequested ) 
               {
                  LogOutRequested ( sender, e ) ;
               }
            }
   
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
         
            private void Init ( ) 
            {
               try
               {
                  _helpProvider         = new HelpProvider ( ) ;
                  __LoadingDataSetState = new ClientQueryDataSet ( ) ;
                  
                  _buttonsFeatures.Add ( UIElementKeys.MediaBurningManagerView, MediaBurningButton ) ;
                  _buttonsFeatures.Add ( UIElementKeys.QueueManager, QueueManagerButton ) ;
                  _buttonsFeatures.Add ( UIElementKeys.SearchStudies, SearchButton ) ;
                  _buttonsFeatures.Add ( UIElementKeys.StorageListenerService, StorageServiceButton ) ;
                  _buttonsFeatures.Add ( UIElementKeys.UsersAccounts, UserAccessButton ) ;
                  _buttonsFeatures.Add ( UIElementKeys.WorkstationConfiguration, WorkstationConfigurationButton ) ;
                  _buttonsFeatures.Add ( UIElementKeys.WorkstationViewer, WorkstationViewerButton ) ;
               }
               catch ( Exception )
               {
                  throw ;
               }
            }
                  
            private void RegisterEvents ( ) 
            {
               try
               {
                  LogOutButton.Click          += new EventHandler ( btnLogOut_Click ) ;
                  
                  ViewerButton.Tag           = new Action ( delegate ( ) { OnDoDisplayViewer ( ) ; } ) ;
                  SearchButton.Tag           = new Action ( delegate ( ) { OnDoSearch ( ) ; } ) ;
                  ConfigurationButton.Tag    = new Action ( delegate ( ) { OnDoConfigure ( ) ; } ) ;
                  UserAccessButton.Tag       = new Action ( delegate ( ) { OnDoManageUsers ( ) ; } ) ;
                  FullScreenButton.Tag       = new Action ( delegate ( ) { OnDoToggleFullScreen ( ) ; } ) ;
                  QueueManagerButton.Tag     = new Action ( delegate ( ) { OnDoViewQueueManager ( ) ; } ) ;
                  MediaBurningButton.Tag     = new Action ( delegate ( ) { OnDoCreateMedia ( ) ; } ) ;
                  StorageServiceButton.Tag   = new Action ( delegate ( ) { OnDoManageService ( ) ; } ) ;
                  WSHelpButton.Tag           = new Action ( delegate ( ) { OnDoDisplayHelp ( ) ; } ) ;


                  ViewerButton.Click          += new EventHandler ( FeatureButton_Click ) ;
                  SearchButton.Click          += new EventHandler ( FeatureButton_Click ) ;
                  ConfigurationButton.Click   += new EventHandler ( FeatureButton_Click ) ;
                  UserAccessButton.Click      += new EventHandler ( FeatureButton_Click ) ;
                  FullScreenButton.Click      += new EventHandler ( FeatureButton_Click ) ;
                  QueueManagerButton.Click    += new EventHandler ( FeatureButton_Click ) ;
                  MediaBurningButton.Click    += new EventHandler ( FeatureButton_Click ) ;
                  StorageServiceButton.Click  += new EventHandler ( FeatureButton_Click ) ;
                  WSHelpButton.Click          += new EventHandler ( FeatureButton_Click ) ;
               }
               catch ( Exception )
               {
                  throw ;
               }
            }
            
            private void OnDoSearch ( )
            {
               if ( null != DoSearch )
               {
                  DoSearch ( this, EventArgs.Empty ) ;
               }
            }

            private void OnDoDisplayViewer ( )
            {
               if ( null != DoDisplayViewer )
               {
                  DoDisplayViewer ( this, EventArgs.Empty ) ;
               }
            }

            private void OnDoConfigure ( )
            {
               if ( null != DoConfigure )
               {
                  DoConfigure ( this, EventArgs.Empty ) ;
               }
            }

            private void OnDoManageUsers ( )
            {
               if ( null != DoManageUsers )
               {
                  DoManageUsers ( this, EventArgs.Empty ) ;
               }
            }

            private void OnDoManageService ( )
            {
               if ( null != DoManageService )
               {
                  DoManageService ( this, EventArgs.Empty ) ;
               }
            }

            private void OnDoCreateMedia ( )
            {
               if ( null != DoCreateMedia )
               {
                  DoCreateMedia ( this, EventArgs.Empty ) ;
               }
            }

            private void OnDoViewQueueManager ( )
            {
               if ( null != DoViewQueueManager )
               {
                  DoViewQueueManager ( this, EventArgs.Empty ) ;
               }
            }

            private void OnDoDisplayHelp ( )
            {
               if ( null != DoDisplayHelp )
               {
                  DoDisplayHelp ( this, EventArgs.Empty ) ;
               }
            }

            private void OnDoToggleFullScreen ( )
            {
               if ( null != DoToggleFullScreen )
               {
                  DoToggleFullScreen ( this, EventArgs.Empty ) ;
               }
            }
            
            private void OnExitFullScreen ( ) 
            {
               if ( null != ExitFullScreen ) 
               {
                  ExitFullScreen ( this, EventArgs.Empty ) ;
               }
            }

         #endregion
   
         #region Properties
         
            private WorkstationViewer __WorkstationViewer
            {
               get
               {
                  return _workstationViewer ;
               }
               
               set
               {
                  _workstationViewer = value ;
               }
            }
            
            private SearchStudies __SearchStudies
            {
               get
               {
                  return _searchStudies ;
               }
               
               set
               {
                  _searchStudies = value ;
               }
            }
            
            private WorkstationConfiguration __WorkstationConfiguration
            {
               get
               {
                  return _workstationConfiguration ;
               }
               
               set
               {
                  _workstationConfiguration = value ;
               }
            }
            
            private StorageListenerService __StorageListenerService
            {
               get
               {
                  return _storageListenerService ;
               }
               
               set
               {
                  _storageListenerService = value ;
               }
            }
            
            private UsersAccounts __WorkstationUsersManagement
            {
               get
               {
                  return _usersManagement ;
               }
               
               set
               {
                  _usersManagement = value ;
               }
            }
            
            //private MediaBurningManagerView __MediaBurningManager
            //{
            //   get ;
            //   set ;
            //}
            
            private bool _DesignMode
            {
               get
               {
                  return (this.GetService(typeof(IDesignerHost)) != null) || 
                         (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime);
               }
            }
            
            private ClientQueryDataSet __LoadingDataSetState
            {
               get ;
               set ;
            }
            
            //private MediaBurningManagerController <IMediaInformationView> __MediaCreationController
            //{
            //   get ;
            //   set ;
            //}
         
         #endregion
   
         #region Events
         
            void btnLogOut_Click ( object sender, EventArgs e )
            {
               try
               {
                  OnLogOutRequested ( this, e ) ;
               }
               catch ( Exception exception ) 
               {
                  ThreadSafeMessager.ShowError (  exception.Message ) ;
               }
            }
            
            void FeatureButton_Click ( object sender, EventArgs e )
            {
               try
               {
                  ( ( (Button) sender ).Tag as Action ).Invoke ( ) ;
               }
               catch ( Exception exception ) 
               {
                  ThreadSafeMessager.ShowError (  exception.Message ) ;
               }
            }

         #endregion
   
         #region Data Members
         
            private WorkstationViewer        _workstationViewer ;
            private SearchStudies            _searchStudies ;
            private WorkstationConfiguration _workstationConfiguration ;
            private StorageListenerService   _storageListenerService ;
            private UsersAccounts            _usersManagement ;
            private HelpProvider             _helpProvider ;
            
            private Dictionary <string,Button> _buttonsFeatures = new Dictionary<string,Button> ( ) ;
            
            private const int WM_KEYDOWN = 0x0100 ;
            
         #endregion
   
         #region Data Types Definition
         
            private delegate void StartSeriesLoadingDelegate
            ( 
               string patientId,
               string studyInstanceUid, 
               string seriesInstanceUid,
               MedicalViewerLoader loader
            ) ;
            
            private class Constants
            {
               public const string SeriesInfo = "{0} ({1}) Series \"{2}\" Modality \"{3}\"" ;
            }
   
         #endregion

            private void ViewerButton_Click(object sender, EventArgs e)
            {

            }
   
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
