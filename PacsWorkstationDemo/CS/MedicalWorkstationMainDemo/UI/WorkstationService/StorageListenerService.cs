// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System ;
using System.IO ;
using System.Collections.Generic ;
using System.ComponentModel ;
using System.Drawing  ;
using System.Data ;
using System.Text ;
using System.Net ;
using System.Net.Sockets ;
using System.Windows.Forms ;
using System.Xml ;
using System.ComponentModel.Design ;
using Leadtools.Dicom.Server.Admin ;
using Leadtools.Dicom.AddIn ;
using Leadtools.Dicom.AddIn.Common ;
using Leadtools.Dicom.AddIn.Interfaces ;
using Leadtools.Medical.Workstation.DataAccessLayer ;
using Leadtools.Medical.Workstation ;
using Leadtools.Demos.Workstation.Configuration;
using System.ServiceProcess;
using Leadtools.DicomDemos;
using Leadtools.Medical.Logging.DataAccessLayer.Configuration;
using Leadtools.Medical.Workstation.UI.Factory;
using Leadtools.Demos.StorageServer.DataTypes;
using Leadtools.Dicom;

namespace Leadtools.Demos.Workstation
{
   public partial class StorageListenerService : UserControl
   {
      #region Public
   
         #region Methods
         
            public StorageListenerService ( )
            {
               try
               {
                  InitializeComponent ( ) ;
                  
                  WorkstationAddInsDll = new List<string> ( ) ;
                  
                  WorkstationConfigurationAddInsDll = new List<string>();
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  throw ;
               }
            }
            
         #endregion
   
         #region Properties
         
            public List <string> WorkstationAddInsDll
            {
               get
               {
                  return _workstationAddInsDll ;
               }
               
               private set
               {
                  _workstationAddInsDll = new List <string> ( ) ;
               }
            }
            
            public List <string> WorkstationConfigurationAddInsDll
            {
               get
               {
                  return _workstationConfigurationAddInsDll ;
               }
               
               private set
               {
                  _workstationConfigurationAddInsDll = new List <string> ( ) ;
               }
            }
   
            private EventLogViewerDialog EventLogViewer
            {
               get 
               {
                  try
                  {
                     return WorkstationUIFactory.Instance.GetWorkstsationControl <EventLogViewerDialog> ( UIElementKeys.EventLogViewer ) ;
                  }
                  catch 
                  {
                     return null ;
                  }
               }
               
            }
   
         #endregion
   
         #region Events
         
            public event EventHandler <WorkstationServiceEventArgs> WorkstationServiceCreated ;
            public event EventHandler <WorkstationServiceEventArgs> WorkstationServiceDeleted ;
            public event EventHandler <WorkstationServiceEventArgs> WorkstationServiceChanged ;
   
         #endregion
   
         #region Data Types Definition
   
         #endregion
   
         #region Callbacks
   
         #endregion
   
      #endregion
   
      #region Protected
   
         #region Methods

            protected override void OnLoad ( EventArgs e )
            {
               try
               {
                  base.OnLoad ( e ) ;
                  
                  if ( _DesignMode ) 
                  {
                     return ;
                  }
                  
                  Init ( ) ;
                  
                  RegisterEvents ( ) ;
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  ThreadSafeMessager.ShowError (  exception.Message ) ;
               }
            }

            /// <summary>
            /// Obtains a lifetime service object to control the lifetime policy for this instance. Returning null makes this a singleton object that doesn't
            /// expire.
            /// </summary>
            /// <returns>
            /// An object of type <see cref="T:System.Runtime.Remoting.Lifetime.ILease"/> used to control the lifetime policy for this instance.
            /// This is the current lifetime service object for this instance if one exists; otherwise, a new lifetime service object initialized to 
            /// the value of the <see cref="P:System.Runtime.Remoting.Lifetime.LifetimeServices.LeaseManagerPollTime"/> property.
            /// </returns>
            /// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
            /// <PermissionSet>
            /// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration, Infrastructure"/>
            /// </PermissionSet>
            public override object InitializeLifetimeService ( )
            {
               return null ;
            }
            
            protected override void OnVisibleChanged(EventArgs e)
            {
               try
               {
                  base.OnVisibleChanged(e);
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  throw ;
               }
               
            }
            
            protected virtual void OnWorkstationServiceCreated ( object sender, WorkstationServiceEventArgs e ) 
            {
               if ( null != WorkstationServiceCreated ) 
               {
                  WorkstationServiceCreated ( sender, e ) ;
               }
            }
            
            protected virtual void OnWorkstationServiceDeleted ( object sender, WorkstationServiceEventArgs e ) 
            {
               if ( null != WorkstationServiceDeleted ) 
               {
                  WorkstationServiceDeleted ( sender, e ) ;
               }
            }
            
            protected virtual void OnWorkstationServiceChanged ( object sender, WorkstationServiceEventArgs e ) 
            {
               if ( null != WorkstationServiceChanged ) 
               {
                  WorkstationServiceChanged ( sender, e ) ;
               }
            }
   
         #endregion
   
         #region Properties
         
            protected DicomService WorkstationService
            {
               get
               {
                  return _LEADWorkstationService ;
               }
               
               set
               {
                  if ( value != _LEADWorkstationService ) 
                  {
                     if ( null != _LEADWorkstationService ) 
                     {
                        UnloadService ( ) ;
                     }
                     
                     _LEADWorkstationService = value ;
                     
                     if ( null != value ) 
                     {
                        LoadService ( ) ;
                     }
                  }
               }
            }
            
            protected WorkstatoinServiceManager ServiceManager
            {
               get
               {
                  return _serviceManager ;
               }
               
               set
               {
                  _serviceManager = value ;
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
                  string serviceName = null ;

                  if (DemosGlobal.MustRestartElevated())
                  {
                     Enabled = false;
                     return;
                  }
                  
                  EventLogViewerToolStripButton.Enabled = EventLogViewer != null ;
                  
                  serviceName = ConfigurationData.ListenerServiceName ;
                     
                  if ( string.IsNullOrEmpty ( serviceName ) )
                  {
                     serviceName = ConfigurationData.ListenerServiceDefaultName ;
                  }

                  CreateServiceManager ( ) ;
                  
                  WorkstationService = ServiceManager.LoadWorkstationListenerService ( serviceName ) ;
                  
                  if ( null == WorkstationService )
                  {
                     try
                     {
                        WorkstationService = CreateDefaultWorkStationService ( ) ;
                     }
                     catch ( Exception ex ) 
                     {
                        ThreadSafeMessager.ShowError(ex.Message);                        
                        WorkstationService = null ;
                     }
                  }
                  
                  HandleStatusChange ( ) ;
                  
                  if ( null == WorkstationService ) 
                  {
                     ThreadSafeMessager.ShowWarning (  "No Listener service installed." ) ; 
                  }
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;                  
                  
                  HandleStatusChange ( ) ;
                  
                  throw ;
               }
            }

            private void RegisterEvents ( ) 
            {
               try
               {
                  if ( EventLogViewer != null ) 
                  {
                     EventLogViewerToolStripButton.Click += new EventHandler ( eventLogViewer_Click ) ;
                  }
                  
                  EditServerToolStripButton.Click    += new EventHandler ( toolStripButtonEditServer_Click ) ;
                  ToolStripButtonStart.Click         += new EventHandler ( toolStripButtonStart_Click ) ;
                  PauseToolStripButton.Click         += new EventHandler ( toolStripButtonPause_Click ) ;
                  StopToolStripButton.Click          += new EventHandler ( toolStripButtonStop_Click ) ;
                  AddServerToolStripButton.Click     += new EventHandler ( AddServerToolStripButton_Click ) ;
                  DeleteServerToolStripButton.Click  += new EventHandler ( DeleteServerToolStripButton_Click ) ;
                  
                  AddAeTitleToolStripButton.Click    += new EventHandler ( toolStripButtonAddAeTitle_Click ) ;
                  EditAeTitleToolStripButton.Click   += new EventHandler ( toolStripButtonEditAeTitle_Click ) ;
                  DeleteAeTitleToolStripButton.Click += new EventHandler ( toolStripButtonDeleteAeTitle_Click ) ;

                  AeTitlesListView.SelectedIndexChanged += new EventHandler ( listViewAeTitles_SelectedIndexChanged ) ;
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  throw ;
               }
            }
            
            private void CreateServiceManager ( )
            {
               ServiceManager = new WorkstatoinServiceManager ( Application.StartupPath ) ;

               ServiceManager.ServerError += new EventHandler<Leadtools.Dicom.Server.Admin.ErrorEventArgs> ( ServiceManager_ServerError ) ;
            }
         
            private void LoadService ( ) 
            {     
               LoadAddIns ( ) ;
               
               HandleStatusChange ( ) ;
               
               WorkstationService.Message      += new EventHandler<MessageEventArgs> ( __LEADWorkstationService_Message ) ;
               WorkstationService.StatusChange += new EventHandler ( __LEADWorkstationService_StatusChange ) ;
               
               InitializeService ( ) ;
            }
            
            private void UnloadService ( )
            {
               UnloadAddIns ( ) ;
               
               WorkstationService.Message      -= new EventHandler<MessageEventArgs> ( __LEADWorkstationService_Message ) ;
               WorkstationService.StatusChange -= new EventHandler ( __LEADWorkstationService_StatusChange ) ;
            }
            
            private void InitServiceInformation ( ) 
            {
               if ( null != WorkstationService )
               {
                  WorkstationDisplayNameLabel.Text = WorkstationService.Settings.DisplayName ;
                  IpAddressLabel.Text              = WorkstationService.Settings.IpAddress ;
                  ServerPortLabel.Text             = WorkstationService.Settings.Port.ToString ( ) ;
                  LEADStorageServiceAELabel.Text   = WorkstationService.Settings.AETitle ;

                  // Enable security icon
                  ServerSecurePictureBox.Visible = WorkstationService.Settings.Secure;
               }
               else
               {
                  WorkstationDisplayNameLabel.Text = string.Empty ;
                  IpAddressLabel.Text              = string.Empty ;
                  ServerPortLabel.Text             = string.Empty ;
                  LEADStorageServiceAELabel.Text   = string.Empty ;
                  ServerSecurePictureBox.Visible         = false;
               }
            }
            
            private void LoadAddIns ( ) 
            {
               foreach ( IAddInOptions option in WorkstationService.AddInOptions )
               {
                  
                  ToolStripButton addInButton ;
                  
                  
                  try
                  {
                     addInButton = new ToolStripButton ( ) ;
                     
                     if ( null != option.Image ) 
                     {
                        addInButton.Image = option.Image.ToImage ( ) ;
                     }
                     
                     addInButton.Text         = option.Text ;
                     addInButton.Tag          = option ;
                     addInButton.ToolTipText  = option.Text ;
                     addInButton.DisplayStyle = ToolStripItemDisplayStyle.Image ;
                     
                     addInButton.Click += new EventHandler ( toolStripButtonConfigureDatabase_Click ) ;
                     
                     MainToolStrip.Items.Add ( addInButton ) ;
                  }
                  catch {}
               }
            }
            
            private void UnloadAddIns ( )
            {
               List <ToolStripItem> unLoadedItems ;
               
               
               unLoadedItems  = new List<ToolStripItem> ( ) ;
               
               foreach ( ToolStripItem stripItem in MainToolStrip.Items )
               {
                  if ( stripItem.Tag is IAddInOptions )
                  {
                     unLoadedItems.Add ( stripItem ) ;
                  }
               }
               
               foreach ( ToolStripItem addInItem in unLoadedItems ) 
               {
                  MainToolStrip.Items.Remove ( addInItem ) ;
               }
            }
            
            private DicomService CreateDefaultWorkStationService ( )
            {
               try
               {
                  DicomService service ;
                  
                  
                  if ( ConfigurationData.AutoCreateService ) 
                  {
                     ServerSettings settings ;
                     
                     
                     if ( ServiceManager.WorkstationService != null ) 
                     {
                        ServiceManager.UnloadWorkstationListenerService ( ) ;
                     }
                     
                     settings = GetSettings ( ) ;
                     service  = ServiceManager.InstallWorkstationService ( settings, WorkstationAddInsDll.ToArray(), WorkstationConfigurationAddInsDll.ToArray() ) ;

                     OnWorkstationServiceCreated ( this, new WorkstationServiceEventArgs ( ServiceManager.ServiceName, service ) ) ;
                  }
                  else
                  {
                     service = null ;
                  }
                  
                  return service ;
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  throw ;
               }
            }

            private ServerSettings GetSettings ( )
            {
               ServerSettings settings ;
               
               
               settings = new ServerSettings ( ) ;
               
               IPAddress [ ] addresses ;
               IPAddress iPv4Address = null ;
               
               
               string hostName = Dns.GetHostName ( );
               
               
               addresses = Dns.GetHostAddresses ( Dns.GetHostName ( ) ) ;
               
               if ( 0 == addresses.Length ) 
               {
                  return null ;
               }
               
               foreach ( IPAddress address in addresses )
               {
                  if ( address.AddressFamily == AddressFamily.InterNetwork )
                  {
                     iPv4Address = address ;
                     
                     break ;
                  }
               }
               
               if ( iPv4Address == null ) 
               {
                  return null ;
               }
               
               settings.AETitle        = ConfigurationData.ListenerServiceDefaultName ;
               settings.AllowAnonymous = true ;
               settings.DisplayName    = ConfigurationData.ListenerServiceDefaultDisplayName ;
               settings.IpAddress      = iPv4Address.ToString ( ) ;
               settings.Port           = GetNearestAvailablePort ( iPv4Address, Constants.DefaultPort ) ;
               
               settings.AllowMultipleConnections  = true ;
               settings.ImplementationClass       = "1.2.840.114257.1123456" ;
               settings.ImplementationVersionName = "LTPACSF V19" ;
               
               
               return settings ;
            }
            
            private void UnInstallService ( ) 
            {
               ServiceManager.UninstallWorkstationService ( ) ;
               
               UnloadAddIns ( ) ;
            }

            void ColorCell(ListViewItem item, int portColumnNumber, bool securePort, ClientPortUsageType portUsage)
            {    
               bool secure = (portUsage == ClientPortUsageType.Secure) || ( (portUsage == ClientPortUsageType.SameAsServer) && WorkstationService.Settings.Secure);
               if (secure == securePort)
               {
                  //cell.Style.ForeColor = dataGridView1.DefaultCellStyle.ForeColor;
                  //cell.Style.SelectionForeColor = dataGridView1.DefaultCellStyle.SelectionForeColor;
                  item.SubItems[portColumnNumber].ForeColor = ListView.DefaultForeColor;
                  item.UseItemStyleForSubItems = false;
               }
               else
               {
                  //cell.Style.ForeColor = Color.LightGray;
                  //cell.Style.SelectionForeColor = Color.DarkGray;
                  item.SubItems[portColumnNumber].ForeColor = Color.LightGray;
                  item.UseItemStyleForSubItems = false;
               }
            }

            void ColorListViewPorts(ListViewItem item, ClientPortUsageType portUsage)
            {
               ColorCell(item, 2, false, portUsage);

               // Secure Port
               ColorCell(item, 3, true, portUsage);
            }
            
            private void AddAe ( AeInfo info )
            {
               try
               {
                  if ( InvokeRequired )
                  {
                     Invoke ( new MethodInvoker ( delegate ( ) 
                     {
                        AddAe ( info ) ;
                     } ) ) ;
                     
                     return ;
                  }
                  
                  ListViewItem item ;
                  
                  
                  item = AeTitlesListView.Items.Add ( info.AETitle ) ;

                  item.SubItems.Add ( info.Address ) ;
                  item.SubItems.Add ( info.Port.ToString ( ) ) ;
                  item.SubItems.Add ( info.SecurePort.ToString ( )  ) ;
                  item.SubItems.Add ( info.ClientPortUsage.ToString() );
                  item.SubItems.Add ( info.LastAccessDate.ToShortDateString());
                  
                  item.Tag = info ;

                  ColorListViewPorts(item, info.ClientPortUsage);
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  throw ;
               }
            }
            
            private void LoadAes ( List<AeInfo> aes )
            {
               try
               {
                  if ( InvokeRequired ) 
                  {
                     Invoke ( new MethodInvoker ( delegate ( ) 
                     {
                        LoadAes ( aes ) ;
                     } ) ) ;
                     
                     return ;
                  }
                  
                  foreach ( AeInfo ae in aes )
                  {
                     AddAe ( ae ) ;
                  }
                  
                  HandleStatusChange ( ) ;
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  throw ;
               }
            }
            
            private void UpdateAe 
            ( 
               string oldAe, 
               AeInfo info, 
               bool failed
            )
            {
               try
               {
                  if ( InvokeRequired )
                  {
                     Invoke ( new MethodInvoker ( delegate ( ) 
                     {
                        UpdateAe ( oldAe, info, failed ) ;
                     } ) ) ;
                     
                     return ;
                  }
                  
                  foreach ( ListViewItem item in AeTitlesListView.Items )
                  {
                     AeInfo old ;
                     
                     
                     old = item.Tag as AeInfo ;

                     if (old.AETitle == oldAe)
                     {
                        //
                        // If the update didn't fail we will update the display
                        //
                        if ( !failed )
                        {
                           item.Text = info.AETitle;
                           item.SubItems [ 1 ].Text = info.Address ;
                           item.SubItems [ 2 ].Text = info.Port.ToString ( ) ;
                           item.SubItems [ 3 ].Text = info.SecurePort.ToString ( ) ;
                           item.SubItems [ 4 ].Text = info.ClientPortUsage.ToString();
                           item.SubItems [ 5 ].Text = info.LastAccessDate.ToShortDateString();
                           
                           item.Tag = info ;
                        }
                        
                        break ;
                     }
                  }
                  
                  HandleStatusChange ( ) ;
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  throw ;
               }
            }
            
            private void RemoveAe ( string deleteAE )
            {
               try
               {
                  if ( InvokeRequired ) 
                  {
                     Invoke ( new MethodInvoker ( delegate ( )
                     {
                        RemoveAe ( deleteAE ) ;
                     } ) ) ;
                     
                     return ;
                  }
                  
                  foreach ( ListViewItem item in AeTitlesListView.Items )
                  {  
                     AeInfo ae ;
                     
                     
                     ae = item.Tag as AeInfo ;

                      if ( ae.AETitle == deleteAE )
                      {
                          AeTitlesListView.Items.Remove ( item ) ;
                          
                          break ;
                      }
                  }
                  
                  HandleStatusChange ( ) ;
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  throw ;
               }
            }
            
            private void InitializeService ( )
            {
               try
               {
                  if ( InvokeRequired ) 
                  {
                     Invoke ( new MethodInvoker ( delegate ( ) 
                     {
                        InitializeService ( ) ;
                     } ) ) ;
                     
                     return ;
                  }
                  
                  InitServiceInformation ( ) ;
                  
                  if ( ( null != WorkstationService ) && ( WorkstationService.Status == ServiceControllerStatus.Running ) ) 
                  {
                     AeTitlesListView.Items.Clear ( ) ;
                     
                     //
                     // Request server ae titles
                     //    
                     WorkstationService.SendMessage ( MessageNames.GetAeTitles ) ;
                  }
               }
               catch ( Exception ex )
               {
                  ThreadSafeMessager.ShowError( "Error sending message.\n" + ex.Message );
               }
            }
            
            private int GetNearestAvailablePort 
            ( 
               IPAddress address, 
               int port 
            )
            {
               try
               {
                  Socket tcpTestSocket ;
                  
                  
                  tcpTestSocket = new Socket ( AddressFamily.InterNetwork, 
                                               SocketType.Stream, 
                                               ProtocolType.Tcp ) ;
   
                  while ( true ) 
                  {
                     try
                     {
                        IPEndPoint endPoint ;
                        
                        
                        endPoint = new IPEndPoint ( address, port ) ;
                        
                        tcpTestSocket.Bind ( endPoint ) ;
                        
                        if ( tcpTestSocket.IsBound ) 
                        {
                           tcpTestSocket.Close ( ) ;
                           
                           return port ;
                        }
                        else
                        {
                           port++ ;
                        }
                     }
                     catch ( Exception )
                     {
                        port++ ;
                     }
                  }
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  throw ;
               }
            }
            
            private void HandleStatusChange ( )
            {
               try
               {
                  if ( InvokeRequired )
                  {
                     Invoke ( new MethodInvoker ( HandleStatusChange ) ) ;
                     
                     return ;
                  }
                  
                  if ( null != WorkstationService ) 
                  {
                     bool running = WorkstationService.Status == ServiceControllerStatus.Running ;
                     bool paused  = WorkstationService.Status == ServiceControllerStatus.Paused ;

                     ToolStripButtonStart.Enabled = !running || paused ;
                     StopToolStripButton.Enabled = running || paused ;
                     PauseToolStripButton.Enabled = running ;
                     EditServerToolStripButton.Enabled = true ;
                     
                     AddAeTitleToolStripButton.Enabled    = WorkstationService.IsAdminAvailable ;
                     EditAeTitleToolStripButton.Enabled   = WorkstationService.IsAdminAvailable && AeTitlesListView.SelectedItems.Count > 0;
                     DeleteAeTitleToolStripButton.Enabled = WorkstationService.IsAdminAvailable && AeTitlesListView.SelectedItems.Count > 0;
                     
                     AddServerToolStripButton.Enabled = false ;
                     DeleteServerToolStripButton.Enabled = true ;
                  }
                  else
                  {
                     foreach ( ToolStripItem button in MainToolStrip.Items ) 
                     {
                        if ( button != AddServerToolStripButton  ) 
                        {
                           button.Enabled = false ;
                        }
                     }
                     
                     AddServerToolStripButton.Enabled = true ;
                  }
                  
                  System.Configuration.Configuration globalPacsConfig = DicomDemoSettingsManager.GetGlobalPacsConfiguration();
                  LoggingDataAccessConfigurationView loggingConfig = new LoggingDataAccessConfigurationView(globalPacsConfig, DicomDemoSettingsManager.ProductNameStorageServer, null);
                  EventLogViewerToolStripButton.Enabled = GlobalPacsUpdater.IsDataAccessSettingsValid ( globalPacsConfig, loggingConfig.DataAccessSettingsSectionName, DicomDemoSettingsManager.ProductNameWorkstation) ;
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  throw ;
               }
            }
            
         #endregion
   
         #region Properties
         
            
            private bool _DesignMode
            {
               get
               {
                  return (this.GetService(typeof(IDesignerHost)) != null) || 
                         (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime);
               }
            }
   
         #endregion
   
         #region Events
         
            void __LEADWorkstationService_Message 
            ( 
               object sender, 
               MessageEventArgs e 
            )
            {
               try
               {
                  ServiceMessage message ;
                  
                  
                  if ( WorkstationService != ( sender as DicomService ) )
                  {
                     return ;
                  }
                  
                  message = AddInUtils.Clone <ServiceMessage> ( e.Message ) ;

                  switch ( message.Message )
                  {
                     case MessageNames.AddAeTitle:
                     {
                        
                        if ( message.Success )
                        {
                           AddAe ( message.Data [ 0 ] as AeInfo ) ; 
                        }
                        else
                        {
                           ThreadSafeMessager.ShowError ( 
                                                          "Error Adding AE Title.\n" + message.Error ) ;
                        }
                     }
                     break;
                     
                     case MessageNames.GetAeTitles:
                     {
                        if ( message.Success )
                        {
                           LoadAes ( message.Data [ 0 ] as List <AeInfo> ) ;
                        }
                        else
                        {
                           ThreadSafeMessager.ShowError ( 
                                                          "Error Getting AE Titles.\n" +  message.Error ) ;
                        }
                     }
                     break ;
                     
                     case MessageNames.UpdateAeTitle:
                     {
                        if ( !message.Success )
                        {
                            ThreadSafeMessager.ShowError ( 
                                                           "Error Updating AE Title.\n" + message.Error ) ;
                            
                            UpdateAe ( message.Data [ 0 ] as string, 
                                       message.Data [ 1 ] as AeInfo, 
                                       true ) ;
                        }
                        else
                        {
                           UpdateAe ( message.Data [ 0 ] as string, 
                                      message.Data [ 1 ] as AeInfo, 
                                      false ) ;
                        }
                     }
                     break ;
                     
                     case MessageNames.RemoveAeTitle:
                     {
                        if (!message.Success)
                        {
                           ThreadSafeMessager.ShowError ( 
                                                          "Error Removing AE Title\n" + message.Error ) ;
                        }
                        else
                        {
                           RemoveAe ( message.Data [ 0] as string ) ;
                        }
                     }
                     break ;
                  }
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  throw ;
               }
            }
            
            void __LEADWorkstationService_StatusChange 
            ( 
               object sender, 
               EventArgs e 
            )
            {
               try
               {
                  HandleStatusChange ( ) ;
                  
                  if ( WorkstationService == ( sender as DicomService ) && WorkstationService.Status == ServiceControllerStatus.Running )
                  {
                      InitializeService ( ) ;
                  }
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  throw ;
               }
            }
            
            private void toolStripButtonConfigureDatabase_Click(object sender, EventArgs e)
            {
               try
               {
                  if ( sender is ToolStripButton &&
                       ( ( ToolStripButton )sender ).Tag is IAddInOptions )
                  {
                     ( ( IAddInOptions ) ( ( ToolStripButton ) sender ).Tag ).Configure ( this, 
                                                                                          WorkstationService.Settings, 
                                                                                          WorkstationService.ServiceDirectory ) ;
                  }
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                  
                  ThreadSafeMessager.ShowError (  exception.Message ) ;
               }
            }
            
            private void toolStripButtonEditServer_Click ( object sender, EventArgs e )
            {
               try
               {
                  EditServiceDialog dialog ;
                  
                  
                  dialog = new EditServiceDialog ( ) ;

                  dialog.Settings    = AddInUtils.Clone <ServerSettings> ( WorkstationService.Settings ) ;
                  dialog.ServiceName = ServiceManager.ServiceName ;
                  dialog.Mode        = EditServiceDialog.EditMode.EditServer ;
                  
                  if ( dialog.ShowDialog ( this ) == DialogResult.OK )
                  {
                     WorkstationService.Settings = dialog.Settings ;
                     
                     LEADStorageServiceAELabel.Text = WorkstationService.Settings.AETitle ;
                     IpAddressLabel.Text            = WorkstationService.Settings.IpAddress ;
                     ServerPortLabel.Text           = WorkstationService.Settings.Port.ToString ( ) ;
                     ServerSecurePictureBox.Visible = WorkstationService.Settings.Secure;
                     
                     OnWorkstationServiceChanged ( this, new WorkstationServiceEventArgs ( WorkstationService.ServiceName, WorkstationService ) ) ;
                  }
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  ThreadSafeMessager.ShowError (  exception.Message ) ;
               }
            }
            
            private void toolStripButtonStart_Click ( object sender, EventArgs e )
            {
               try
               {
                  WorkstationService.Start ( ) ;
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  ThreadSafeMessager.ShowError (  exception.Message ) ;
               }
            }
            
            void toolStripButtonPause_Click
            (
               object sender, 
               EventArgs e
            )
            {
               try
               {
                  WorkstationService.Pause ( ) ;
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  ThreadSafeMessager.ShowError (  exception.Message ) ;
               }
            }
            
            private void toolStripButtonStop_Click ( object sender, EventArgs e )
            {
               try
               {
                  WorkstationService.Stop ( ) ;
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  ThreadSafeMessager.ShowError (  exception.Message ) ;
               }
            }
            
            void AddServerToolStripButton_Click ( object sender, EventArgs e )
            {
               try
               {
                  EditServiceDialog dialog ;


                  dialog = new EditServiceDialog ( ) ;
                  
                  dialog.Settings    = GetSettings ( ) ;
                  dialog.ServiceName = ServiceManager.ServiceName ;
                  dialog.Mode        = EditServiceDialog.EditMode.AddServer ;
                  
                  if  ( dialog.ShowDialog(this) == DialogResult.OK )
                  {
                     try
                     {
                        WorkstationService = ServiceManager.InstallWorkstationService ( dialog.Settings, 
                                                                                        WorkstationAddInsDll.ToArray ( ),
                                                                                        WorkstationConfigurationAddInsDll.ToArray() ) ;

                        if ( null != WorkstationService ) 
                        {
                           OnWorkstationServiceCreated ( this, new WorkstationServiceEventArgs ( ServiceManager.ServiceName, WorkstationService ) ) ;
                        }
                     }
                     catch (Exception ex)
                     {
                        ThreadSafeMessager.ShowError (  ex.Message );
                     }
                  }
               }
               catch ( Exception exception ) 
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  ThreadSafeMessager.ShowError (  exception.Message ) ;
               }
            }
            
            
            private void DeleteServerToolStripButton_Click ( object sender, EventArgs e )
            {
               DialogResult result = DialogResult.Yes;


               if ( null == WorkstationService ) 
               {
                  return ;
               }
               
               if ( WorkstationService.Status == ServiceControllerStatus.Running )
               {
                   result = ThreadSafeMessager.ShowQuestion ( "Service is currently running\r\nDo you want to stop and delete?",
                                                              MessageBoxButtons.YesNo ) ;
                                              
                   if (result == DialogResult.Yes)
                   {
                       WorkstationService.Stop();
                       
                       while ( WorkstationService.Status != ServiceControllerStatus.Stopped )
                       {
                           Application.DoEvents ( ) ;
                       }
                   }
               }

               if ( result == DialogResult.Yes )
               {
                  try
                  {
                     string serviceName ;
                     
                     
                     serviceName = ServiceManager.ServiceName ;
                     
                     UnInstallService ( ) ;
                       
                     LEADStorageServiceAELabel.Text = string.Empty ;
                     IpAddressLabel.Text            = string.Empty;
                     ServerPortLabel.Text           = string.Empty;

                     AeTitlesListView.Items.Clear();
                       
                     ThreadSafeMessager.ShowInformation (  "Service successfully uninstalled" ) ;
                       
                     WorkstationService = null ;
                     
                     OnWorkstationServiceDeleted ( this, new WorkstationServiceEventArgs ( serviceName, null ) ) ;
                  }
                  catch (Exception ex)
                  {
                     ThreadSafeMessager.ShowError (  ex.Message );
                  }
               }
               
               HandleStatusChange ( ) ;
           }
            
            private void toolStripButtonAddAeTitle_Click ( object sender, EventArgs e )
            {
               try
               {
                  EditAeTitleDialog dialog ;
                  
                  
                  dialog = new EditAeTitleDialog ( ) ;
                  dialog.ServerSecure = WorkstationService.Settings.Secure;

                  if ( dialog.ShowDialog ( this ) == DialogResult.OK )
                  {
                     AeInfo newAeInfo = AddInUtils.Clone<AeInfo> ( dialog.AeInfo ) ;
                     
                     
                     newAeInfo.Address = newAeInfo.Address ;
                     
                     try
                     {
                        WorkstationService.SendMessage ( MessageNames.AddAeTitle, newAeInfo ) ;
                     }
                     catch ( Exception ex )
                     {
                        ThreadSafeMessager.ShowError (  "Error sending message.\n" + ex.Message ) ;
                     }
                  }
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  ThreadSafeMessager.ShowError (  exception.Message ) ;
               }
            }
            
            private void toolStripButtonEditAeTitle_Click ( object sender, EventArgs e )
            {
               try
               {
                  EditAeTitleDialog dialog ;
                  string oldAe ;
                  
                  
                  if ( AeTitlesListView.SelectedItems.Count == 0 )
                  {
                     return ;
                  }
                  
                  dialog = new EditAeTitleDialog ( ) ;
                  dialog.ServerSecure = WorkstationService.Settings.Secure;
                  
                  dialog.AeInfo = AddInUtils.Clone<AeInfo> ( AeTitlesListView.SelectedItems [ 0 ].Tag as AeInfo ) ;
                  
                  oldAe  = dialog.AeInfo.AETitle ;

                  if ( dialog.ShowDialog ( this ) == DialogResult.OK )
                  {
                     try
                     {
                        AeInfo newAeInfo = AddInUtils.Clone<AeInfo> ( dialog.AeInfo ) ;
                        
                        
                        newAeInfo.Address = newAeInfo.Address ;

                        ColorListViewPorts(AeTitlesListView.SelectedItems[0], newAeInfo.ClientPortUsage);


                        WorkstationService.SendMessage ( MessageNames.UpdateAeTitle,
                                                         oldAe, 
                                                         newAeInfo ) ;
                     }
                     catch (Exception ex)
                     {
                        ThreadSafeMessager.ShowError (  "Error sending message.\n" + ex.Message ) ;
                     }
                  }
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  ThreadSafeMessager.ShowError (  exception.Message ) ;
               }
            }
            
            private void toolStripButtonDeleteAeTitle_Click ( object sender, EventArgs e )
            {
               try
               {
                  AeInfo info  ;
                  
                  
                  if ( AeTitlesListView.SelectedItems.Count == 0 )
                  {
                     return ;
                  }
                  
                  info = AeTitlesListView.SelectedItems [ 0 ].Tag as AeInfo;

                  WorkstationService.SendMessage(MessageNames.RemoveAeTitle, info.AETitle);
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  ThreadSafeMessager.ShowError (  "Error sending message.\n" + exception.Message ) ;
               }
            }
            
            void ServiceManager_ServerError(object sender, Leadtools.Dicom.Server.Admin.ErrorEventArgs e)
            {
               if ( ( null != e.Error ) && ( e.Error is BadImageFormatException ) )
               {
                  string demoVersoin   = "32";
                  string addInsVersion = "64";
                  
                  string msg = 
                    "Error loading [{0}].\n\n" + 
                    "This {1}-bit {3} process cannot load {2}-bit AddIn dlls, so the AddIn options can not be displayed.  " +
                    "Please use the {2}-bit version of the {3} to view the AddIn options.\n\n" + 
                    "Note:\n" +
                    "If you prefer to use the {1}-bit version of the AddIn dlls you can delete the current service and reinstall it from this {3}:\n" + 
                    "* To delete the service, click on 'Delete Server' button from the Workstation Listener Service Manager.\n" + 
                    "* To install a new service, click on 'Add Server' and enter the service information -or leave the defaults- in the dialog then click ok.\n" ;

                  string message = string.Empty;

                   if ( DemosGlobal.Is64Process ( ) )
                   {
                      demoVersoin   = "64";
                      addInsVersion = "32";
                   }
                   else
                   {
                      demoVersoin   = "32";
                      addInsVersion = "64";
                   }
                   
                   message = string.Format(msg, (e.Error as BadImageFormatException).FileName, demoVersoin, addInsVersion, Messager.Caption );
                   
                   ThreadSafeMessager.ShowWarning (  message ) ;
               }
               else
               {
                  ThreadSafeMessager.ShowError (  e.Error.Message ) ;
               }
            }
          
            private void listViewAeTitles_SelectedIndexChanged 
            ( 
               object sender, 
               EventArgs e 
            )
            {
               try
               {
                  HandleStatusChange ( ) ;
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
               }
            }
            
            void eventLogViewer_Click ( object sender, EventArgs e )
            {
               if ( EventLogViewer.Visible ) 
               {
                  EventLogViewer.Focus ( ) ;
               }
               else
               {
                  EventLogViewer.Show ( this ) ;
               }
            }
   
         #endregion
   
         #region Data Members
         
            private DicomService                _LEADWorkstationService ;
            private WorkstatoinServiceManager  _serviceManager ;
            private List <string>              _workstationAddInsDll ;
            private List<string>               _workstationConfigurationAddInsDll;
   
         #endregion
   
         #region Data Types Definition
         
            private class Constants
            {
               public const string   DicomServerConfigFile         = "Leadtools.Dicom.Server.exe.config" ;
               public const string   ConfigurationComponent        = "StorageListenerService" ;
               public const int      DefaultPort                   = 105 ;
            }
   
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
   
   public class WorkstationServiceEventArgs : EventArgs
   {
      public WorkstationServiceEventArgs ( string serviceName, DicomService service ) 
      {
         ServiceName = serviceName ;
         Service     = service ;
      }
      
      public string ServiceName
      {
         get
         {
            return _serviceName ;
         }
         
         set
         {
            _serviceName = value ;
         }
      }
      
      public DicomService Service
      {
         get ;
         private set ;
      }
      
      private string _serviceName ;
   }
}
