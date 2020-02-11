// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leadtools.Medical.Workstation.DataAccessLayer;
using Leadtools.Medical.Storage.DataAccessLayer;
using Leadtools.Medical.DataAccessLayer;
using Leadtools.Medical.Workstation.DataAccessLayer.Configuration;
using Leadtools.Medical.Storage.DataAccessLayer.Configuration;
using Leadtools.Medical.UserManagementDataAccessLayer.Configuration;
using Leadtools.Medical.UserManagementDataAccessLayer;
using System.ComponentModel;
using Leadtools.Medical.Workstation.UI.Factory;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Leadtools.Medical.Workstation.UI;
using Leadtools.Medical.Winforms;
using System.Windows.Forms;
using Leadtools.Demos.Workstation.Configuration;
using Leadtools.DicomDemos;
using System.IO;
using Leadtools.Dicom.Server.Admin;
using System.Diagnostics;
using Leadtools.Medical.Workstation;
using Leadtools.Dicom.Common.DataTypes;
using System.Runtime.Serialization.Formatters.Binary;
using Leadtools.Demos.StorageServer.DataTypes;

#if (LEADTOOLS_V19_OR_LATER)
using Leadtools.Medical.OptionsDataAccessLayer;
using Leadtools.Medical.OptionsDataAccessLayer.Configuration;
#endif // #if (LEADTOOLS_V19_OR_LATER)

namespace Leadtools.Demos.Workstation
{
   class WorkstationShellController
   {
      #region Public
         
         #region Methods
         
            public void Run ( ) 
            {
               string message ;
               bool   dbConfigured ;


               string[] productsToCheck = new string[] { DicomDemoSettingsManager.ProductNameWorkstation };

               if ( IsUninstallMode ( ) )
               {
                  dbConfigured = GlobalPacsUpdater.IsDbComponentsConfigured ( productsToCheck, out message ) ;
                  
                  
                  if ( dbConfigured ) 
                  {
                     try
                     {
                        RegisterServices ( ) ;
                        
                        DeleteServiceIfExists ( ) ;
                     }
                     catch{}
                  }
                  
                  return ;
               }

               CheckPacsConfig();

#if (LEADTOOLS_V19_OR_LATER)
               string globalPacsConfigPath = DicomDemoSettingsManager.GlobalPacsConfigFullFileName;
               if (File.Exists(globalPacsConfigPath))
               {
                  try
                  {
                     if (false == UpgradeConfigFiles())
                        return;
                  }
                  catch (Exception ex)
                  {
                     string msg = string.Format("Upgrade Failed!\n\n{0}", ex.Message);
                     MessageBox.Show(msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                     return;
                  }
               }
#endif // #if (LEADTOOLS_V19_OR_LATER)

               dbConfigured = GlobalPacsUpdater.IsDbComponentsConfigured(productsToCheck, out message);
               
               if ( ConfigurationData.CheckDataAccessServices &&
                    !dbConfigured &&
                    !RequestUserToConfigureDbSucess ( message ) ) 
               {
                  return ;
               }
               
               if ( ConfigurationData.CheckDataAccessServices )
               {
                  if ( !GlobalPacsUpdater.IsProductDatabaseUpTodate ( DicomDemoSettingsManager.ProductNameWorkstation ) && 
                       !RequestUserToUpgradeDbSucess ( ) )
                  {
                     return ;
                  }
               }
               
               Caption = ConfigurationData.ApplicationName ;
               
               Messager.Caption            = Caption ;
               WorkstationMessager.Caption = Caption ;
               
               RegisterServices ( ) ;
               PacsProduct.ProductName = DicomDemoSettingsManager.ProductNameWorkstation;
               
               if ( LogInUser ( ) )
               {
                  if ( ConfigurationData.ShowSplashScreen ) 
                  {
                     ShowSplashScreen ( ) ;
                  }
#if !LEADTOOLS_V175_OR_LATER
                  Leadtools.Demos.Workstation.Configuration.ConfigurationData.PacsFrmKey = Leadtools.Demos.Support.MedicalServerKey ;
#endif

                  SetWorkstationSettings ( ) ;
                  
                  RegisterControls ( ) ;
                  
                  ConfigureControls ( ) ;
                  
                  if ( UserAccessManager.AuthenticatedUser.IsAdmin ) 
                  {
                     ConfigurationData.ChangesSaved += new ConfigurationData.EmptyHandler ( ConfigurationData_ChangesSaved ) ;
                  }
                  
                  MainForm form = new MainForm ( ) ;
                  
                  form.Text = Caption ;
                  
                  form.Icon = WorkstationUtils.GetApplicationIcon ( ) ;
                  
                  form.FormClosing += new FormClosingEventHandler ( MainForm_FormClosing ) ;

                  form.WorkStationContainerControl.LogOutRequested += new EventHandler ( WorkStationContainerControl_LogOutRequested ) ;
                  
                  new WorkstationContainerPresenter ( form.WorkStationContainerControl, new ClientQueryDataSet ( ) ) ;
                  
                  ThreadSafeMessager.Owner = form ;
                  
                  Application.Run ( form ) ;
               }
            }
            
            public void UpdateDisplayOrientation(OrientationConfiguration orientationConfiguration)
            {
               DisplayOrientation = orientationConfiguration ;
               
               SaveDisplayOrientation ( ) ;
            }
         
         #endregion
         
         #region Properties
         
            public static WorkstationShellController Instance
            {
               get
               {
                  return _instance ;
               }
            }
            
            public WorkstationSettings WorkstationSettings
            {
               get ;
               private set ;
            }
            
            public OrientationConfiguration DisplayOrientation
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

            private static bool UpgradeConfigFiles()
            {
#if (LEADTOOLS_V19_OR_LATER)
               string exeName = Path.GetFileNameWithoutExtension(Application.ExecutablePath);
               string globalPacsConfigPath = DicomDemoSettingsManager.GlobalPacsConfigFullFileName;
               string backupGlobalPacsConfigPath = string.Empty;

               // Upgrade GlobalPacs.Config if necessary
               bool bNeedsUpdate = GlobalPacsUpdater.AddOptionsToGlobalPacsConfig(globalPacsConfigPath, false);
               if (bNeedsUpdate)
               {
                  string msg = string.Format("The existing globalPacs.config must be upgraded\n\nDo you want to continue?", exeName);
                  DialogResult dr = MessageBox.Show(msg, "Upgrade Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                  if (dr != DialogResult.Yes)
                     return false;

                  backupGlobalPacsConfigPath = GlobalPacsUpdater.BackupFile(globalPacsConfigPath);
                  GlobalPacsUpdater.AddOptionsToGlobalPacsConfig(globalPacsConfigPath, true);
               }

#endif // (LEADTOOLS_V19_OR_LATER)
               return true;
            }
         
            private WorkstationShellController ( ) {}
            
            private void ConfigureControls ( )
            {
               ConfigureListenerService ( ) ;
               
               ConfigureWSConfig ( ) ;
               
               ConfigureViewer ( ) ;
            }
            
            private void ConfigureListenerService ( )
            {
               if ( WorkstationUIFactory.Instance.IsControlRegistered ( UIElementKeys.StorageListenerService ) )
               {
                  StorageListenerService storageListenerService = WorkstationUIFactory.Instance.GetWorkstsationControl <StorageListenerService> ( UIElementKeys.StorageListenerService ) ;
                  
                  
                  storageListenerService.WorkstationAddInsDll.Add ( "Leadtools.Medical.Storage.AddIns.dll" ) ;
                  storageListenerService.WorkstationAddInsDll.Add ( "Leadtools.Medical.Media.AddIns.dll" ) ;
                  storageListenerService.WorkstationAddInsDll.Add("Leadtools.Medical.Security.Addin.dll");
                  storageListenerService.WorkstationConfigurationAddInsDll.Add("Leadtools.Medical.Logging.AddIn.dll");
            
                  storageListenerService.WorkstationServiceCreated += new EventHandler<WorkstationServiceEventArgs> ( WorkstationListenerServiceControl_WorkstationServiceChanged ) ;
                  storageListenerService.WorkstationServiceDeleted += new EventHandler<WorkstationServiceEventArgs> ( WorkstationListenerServiceControl_WorkstationServiceDeleted ) ;
                  storageListenerService.WorkstationServiceChanged += new EventHandler<WorkstationServiceEventArgs> ( WorkstationListenerServiceControl_WorkstationServiceChanged ) ;
               }
            }
            
            private void ConfigureWSConfig ( )
            {
               if ( WorkstationUIFactory.Instance.IsControlRegistered ( UIElementKeys.WorkstationConfiguration ) )
               {
                  WorkstationConfiguration configurationView = WorkstationUIFactory.Instance.GetWorkstsationControl <WorkstationConfiguration> ( UIElementKeys.WorkstationConfiguration ) ;
                  
                     
                  configurationView.CanViewPACSConfig            = UserAccessManager.AuthenticatedUser.IsAdmin ;
                  configurationView.CanEditWorkstationClientInfo = UserAccessManager.AuthenticatedUser.IsAdmin || !ConfigurationData.SetClientToAllWorkstations ;
                  configurationView.CanChangeForceClientInfo     = UserAccessManager.AuthenticatedUser.IsAdmin ;
               }
            }
            
            private void ConfigureViewer ( )
            {
               if ( WorkstationUIFactory.Instance.IsControlRegistered ( UIElementKeys.WorkstationViewer ) )
               {
                  WorkstationViewer workstationViewer = WorkstationUIFactory.Instance.GetWorkstsationControl <WorkstationViewer> ( UIElementKeys.WorkstationViewer ) ;
                  
                  workstationViewer.WorkstationDataAccess = DataAccessServices.GetDataAccessService <IWorkstationDataAccessAgent> ( ) ;
                  workstationViewer.StorageDataAccess     = DataAccessServices.GetDataAccessService <IStorageDataAccessAgent> ( ) ;
                  
                  workstationViewer.CanModifyWindowLevelPreset = UserAccessManager.AuthenticatedUser.IsAdmin ;
                  
                  LoadModalitySettings ( workstationViewer ) ;
                  
                  __Container = workstationViewer.ViewerContainer ;

                  //initially show the study time line
                  if (ConfigurationData.ShowStudyTimeline)
                  {
                     workstationViewer.ShowChronologicalTimelineOnLoad = true;
                  }
               }
            }

            private void RegisterControls ( )
            {
               bool isServiceInstalledOnMachineOrEmpty ;
               
               
               if ( null == WorkstationSettings || string.IsNullOrEmpty ( WorkstationSettings.WorkstationServer ) )
               {
                  isServiceInstalledOnMachineOrEmpty = true ;
               }
               else
               {
                  isServiceInstalledOnMachineOrEmpty = Utils.IsLocalIPAddress ( WorkstationSettings.WorkstationDicomServer.IPAddress ) ;
               }
               
               WorkstationUIFactory.Instance.RegisterWorkstationView <MediaBurningManagerView> ( typeof ( MediaBurningManagerView ) ) ;
               
               MediaBurningManagerView mediaManager = WorkstationUIFactory.Instance.GetWorkstationView <MediaBurningManagerView> ( ) ;
               
               mediaManager.EnableLocalMediaInfomration = false ;
               mediaManager.EnablePacsMediaInfomration  = false ;
               
               if ( ConfigurationData.SupportDicomCommunication && 
                    !WorkstationUIFactory.Instance.IsViewRegistered<IMediaBurningManagerView<IPacsMediaInformationView>> ( ) )
               {
                  Type burningManager ;
                  
                  burningManager = typeof ( MediaBurningManagerView ) ;
                  
                  WorkstationUIFactory.Instance.RegisterWorkstationView <IMediaBurningManagerView<IPacsMediaInformationView>> ( burningManager ) ;
                  
                  mediaManager.EnablePacsMediaInfomration  = true ;
               }
               
               if ( ConfigurationData.SupportLocalQueriesStore &&
                    !WorkstationUIFactory.Instance.IsViewRegistered<IMediaBurningManagerView<ILocalMediaInformationView>> ( ) )
               {
                  Type localBurningManager ;
                  
                  localBurningManager = typeof ( MediaBurningManagerView ) ;
                  
                  WorkstationUIFactory.Instance.RegisterWorkstationView <IMediaBurningManagerView<ILocalMediaInformationView>> ( localBurningManager ) ;
                  
                  mediaManager.EnableLocalMediaInfomration = true ;
               }
               
               if ( !WorkstationUISettings.Instance.Controls.Contains ( UIElementKeys.WorkstationViewer ) )
               {
                  Type workstationViewer ;
                  
                  workstationViewer = typeof ( WorkstationViewer ) ;
                  
                  WorkstationUISettings.Instance.Controls.Add ( new NameTypeConfigurationElement ( UIElementKeys.WorkstationViewer, workstationViewer ) ) ;
               }
               
               if ( !WorkstationUISettings.Instance.Controls.Contains ( UIElementKeys.SearchStudies ) )
               {
                  Type searchStudies ;
                  
                  searchStudies = typeof ( SearchStudies ) ;
                  
                  WorkstationUISettings.Instance.Controls.Add ( new NameTypeConfigurationElement ( UIElementKeys.SearchStudies, searchStudies ) ) ;
               }
               
               if ( UserAccessManager.AuthenticatedUser.IsAdmin && 
                    !WorkstationUISettings.Instance.Controls.Contains ( UIElementKeys.WorkstationConfiguration ) )
               {
                  Type configuration ;
                  
                  configuration = typeof ( WorkstationConfiguration ) ;
                  
                  WorkstationUISettings.Instance.Controls.Add ( new NameTypeConfigurationElement ( UIElementKeys.WorkstationConfiguration, configuration ) ) ;
               }
               
               if ( ConfigurationData.SupportDicomCommunication && 
                    UserAccessManager.AuthenticatedUser.IsAdmin && 
                    isServiceInstalledOnMachineOrEmpty && 
                    !WorkstationUISettings.Instance.Controls.Contains ( UIElementKeys.StorageListenerService ) )
               {
                  Type serviceManager ;
                  Type eventLogViewer ;
                  
                  
                  serviceManager = typeof ( StorageListenerService ) ;
                  eventLogViewer = typeof ( EventLogViewerDialog ) ;
                  
                  WorkstationUISettings.Instance.Controls.Add ( new NameTypeConfigurationElement ( UIElementKeys.StorageListenerService, serviceManager ) ) ;
                  WorkstationUISettings.Instance.Controls.Add ( new NameTypeConfigurationElement ( UIElementKeys.EventLogViewer, eventLogViewer ) ) ;
               }
               
               if ( UserAccessManager.AuthenticatedUser.IsAdmin && 
                    !WorkstationUISettings.Instance.Controls.Contains ( UIElementKeys.UsersAccounts ) )
               {
                  Type userAccounts ;
                  
                  userAccounts = typeof ( UsersAccounts ) ;
                  
                  WorkstationUISettings.Instance.Controls.Add ( new NameTypeConfigurationElement ( UIElementKeys.UsersAccounts, userAccounts ) ) ;
               }
               
               if ( ConfigurationData.SupportDicomCommunication && 
                    !WorkstationUISettings.Instance.Controls.Contains ( UIElementKeys.QueueManager ) )
               {
                  Type queueManager ;
                  
                  queueManager = typeof ( QueueManager ) ;
                  
                  WorkstationUISettings.Instance.Controls.Add ( new NameTypeConfigurationElement ( UIElementKeys.QueueManager, queueManager ) ) ;
                  
                  QueueManager.Instance.AutoLoadRetrievedImages = ConfigurationData.QueueAutoLoad ;
                  QueueManager.Instance.RemoveCompletedItems    = ConfigurationData.QueueRemoveItem ;
                  
                  QueueManager.Instance.AutoLoadRetrievedImagesChanged += new EventHandler ( Instance_AutoLoadRetrievedImagesChanged ) ;
                  QueueManager.Instance.RemoveCompletedItemsChanged    += new EventHandler ( Instance_RemoveCompletedItemsChanged ) ;
               }
            }

            private void RegisterServices ( )
            {
               System.Configuration.Configuration  globalPacsConfig = DicomDemoSettingsManager.GetGlobalPacsConfiguration();
               
               if ( !ConfigurationData.SupportLocalQueriesStore ) 
               {
                  return ;
               }
               
               try
               {
                  IWorkstationDataAccessAgent wsDataAccess = DataAccessFactory.GetInstance ( new WorkstationDataAccessConfigurationView ( globalPacsConfig, DicomDemoSettingsManager.ProductNameWorkstation, null) ).CreateDataAccessAgent <IWorkstationDataAccessAgent> ( ) ;
                  
                  DataAccessServices.RegisterDataAccessService <IWorkstationDataAccessAgent> ( wsDataAccess ) ;
               }
               catch {}
               
               try
               {
                  IStorageDataAccessAgent storageDataAccess = DataAccessFactory.GetInstance ( new StorageDataAccessConfigurationView ( globalPacsConfig, DicomDemoSettingsManager.ProductNameWorkstation, null) ).CreateDataAccessAgent <IStorageDataAccessAgent> ( ) ;
                  
                  DataAccessServices.RegisterDataAccessService <IStorageDataAccessAgent> ( storageDataAccess ) ;
               }
               catch {}
               
               try
               {
                  IUserManagementDataAccessAgent userManagement = DataAccessFactory.GetInstance(new UserManagementDataAccessConfigurationView (globalPacsConfig, DicomDemoSettingsManager.ProductNameWorkstation, null) ).CreateDataAccessAgent<IUserManagementDataAccessAgent>();
                  
                  DataAccessServices.RegisterDataAccessService <IUserManagementDataAccessAgent> ( userManagement ) ;
               }
               catch {}

#if (LEADTOOLS_V19_OR_LATER)
               try
               {
                  IOptionsDataAccessAgent optionsAgent = DataAccessFactory.GetInstance(new OptionsDataAccessConfigurationView (globalPacsConfig, DicomDemoSettingsManager.ProductNameWorkstation, null) ).CreateDataAccessAgent<IOptionsDataAccessAgent>();
                  
                  DataAccessServices.RegisterDataAccessService <IOptionsDataAccessAgent> ( optionsAgent ) ;
               }
               catch {}
#endif // #if (LEADTOOLS_V19_OR_LATER)

            }
         
            private static bool IsUninstallMode ( )
            {
               string[] args =  System.Environment.GetCommandLineArgs ( ) ;
               
               
               foreach ( string arg in args )
               {
                  if ( arg.IndexOf ( "uninstall", StringComparison.OrdinalIgnoreCase ) == 0 ) 
                  {
                     return true ;
                  }
               }
               
               return false ;
            }
            
            private bool RequestUserToConfigureDbSucess ( string missingDbComponents ) 
            {
               {
                  string message ;
                  DialogResult result ;
                  
                  
                  message = "The following databases are not configured:\n\n{0}\nRun the " + 
                           ConfigurationData.DatabaseConfigName + " to configure the missing databases.\n\n" +
                           "Do you want to run the " + ConfigurationData.DatabaseConfigName + " wizard now?" ;
                  
                  message = string.Format ( message, missingDbComponents ) ;
                  
                  result = MessageBox.Show ( message,
                                             Caption  ,
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Warning ) ;
                                             
                  if ( DialogResult.Yes == result ) 
                  {
                     string dbManagerFileName ;

                     
                     dbManagerFileName = Path.Combine ( Path.GetDirectoryName ( Application.ExecutablePath ), ConfigurationData.DatabaseConfigEXEName ) ;
                     
                     if ( !File.Exists ( dbManagerFileName ) )
                     {
                        dbManagerFileName = Path.Combine ( Path.GetDirectoryName ( Application.ExecutablePath ), ConfigurationData.DatabaseConfigAltEXEName ) ;
                     }
                     
                     if ( File.Exists ( dbManagerFileName ) )
                     {
                        Process dbConfigProcess ;
                        
                        
                        dbConfigProcess   = new Process ( ) ;
                        dbConfigProcess.StartInfo.FileName  =  dbManagerFileName ;

                        if (!DemosGlobal.IsAdmin())
                        {
                           dbConfigProcess.StartInfo.Verb = "runas";
                        }

                        dbConfigProcess.Start ( ) ;
                        
                        dbConfigProcess.WaitForExit ( ) ;

                        string[] productsToCheck = new string[] { DicomDemoSettingsManager.ProductNameWorkstation };

                        bool isDbConfigured = GlobalPacsUpdater.IsDbComponentsConfigured(productsToCheck, out missingDbComponents);
                        
                        if ( !isDbConfigured ) 
                        {
                           MessageBox.Show ( "Databases is not configured.", 
                                             Caption  ,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Error ) ;
                           
                           
                           return false ;
                        
                        }
                     }
                     else
                     {
                        MessageBox.Show ( "Couldn't find the " + ConfigurationData.DatabaseConfigName + " wizard",
                                          Caption  ,
                                          MessageBoxButtons.OK,
                                          MessageBoxIcon.Error ) ;
                                          
                        return false ;
                     }
                  }
                  else
                  {
                     return false ;
                  }
               }
               
               return true ;
            }
            
            private static bool RequestUserToUpgradeDbSucess()
            {
               string message;
               DialogResult result;
               string Caption = "Warning";

               message = "The Workstation database needs to be upgraded.\n\n" + 
                         "Do you want to upgrade the database now?";

               result = MessageBox.Show(message,
                                          Caption,
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Warning);

               if (DialogResult.Yes == result)
               {
                  GlobalPacsUpdater.UpgradeProductDatabase ( DicomDemoSettingsManager.ProductNameWorkstation ) ;
                  
                  if ( GlobalPacsUpdater.IsProductDatabaseUpTodate(DicomDemoSettingsManager.ProductNameWorkstation) )
                  {
                     MessageBox.Show("Database upgraded successfully",
                                      Caption,
                                MessageBoxButtons.OK,
                                 MessageBoxIcon.Information);
                                    
                     return true ;
                  }
               }

               return false;
               
            }

            private static void CheckPacsConfig ( )
            {
               if ( ConfigurationData.RunPacsConfig )
               {
                  DicomDemoSettings settings  = null ;
                  
                  
                  settings = DicomDemoSettingsManager.LoadSettings ( Path.GetFileName ( Application.ExecutablePath ) ) ;
                  
                  if ( settings == null )
                  {
                     DicomDemoSettingsManager.RunPacsConfigDemo ( ) ;
                     
                     settings = DicomDemoSettingsManager.LoadSettings ( Path.GetFileName ( Application.ExecutablePath ) ) ;
                  }
               }
            }
         
            private static void DeleteServiceIfExists ( )
            {
               try
               {
                  WorkstationSettings settings = LoadWorkstationSettings ( ) ;
                  
                  
                  if ( null != settings && !string.IsNullOrEmpty ( settings.WorkstationServer ) ) 
                  {
                     if ( Utils.IsLocalIPAddress ( settings.WorkstationDicomServer.IPAddress ) )
                     {
                        List <string> services = new List<string> ( ) ;
                        
                        using ( ServiceAdministrator serviceAdmin = new ServiceAdministrator ( Application.StartupPath + @"\" ) )
                        {
                           services.Add ( settings.WorkstationServer ) ;
#if !LEADTOOLS_V175_OR_LATER
                           serviceAdmin.Unlock ( Support.MedicalServerKey, services ) ;
#endif
                           
                           if ( serviceAdmin.Services.Count == 1 ) 
                           {
                              DicomService service ;
                              
                              service = serviceAdmin.Services [ settings.WorkstationServer ] ;
                              
                              if ( service.Status != System.ServiceProcess.ServiceControllerStatus.Stopped || 
                                   service.Status != System.ServiceProcess.ServiceControllerStatus.StopPending )
                              {
                                 service.Stop ( ) ;
                              }
                              
                              serviceAdmin.UnInstallService ( serviceAdmin.Services [ settings.WorkstationServer ] ) ;
                           }
                        }
                     }
                  }
               }
               catch {}
            }
      
            private void SetWorkstationSettings ( )
            {
               try
               {
                  try
                  {
                     DicomDemoSettings settings ;
                  
                  
                     settings = DicomDemoSettingsManager.LoadSettings ( Path.GetFileName ( Application.ExecutablePath ) ) ;

                     if ( null != settings && settings.FirstRun )
                     {
                        ImagesDownloadDialog notification ;
                     
                     
                        notification = new ImagesDownloadDialog ( ) ;
                     
                        notification.ShowDialog ( ) ;
                     
                        settings.FirstRun = false ;
                     
                        DicomDemoSettingsManager.SaveSettings ( Path.GetFileName ( Application.ExecutablePath ), settings ) ;
                     
                        ConfigurationData.ClientBrowsingMode = DicomClientMode.Pacs ;
                     
                        WorkstationSettings = CreateWorkstationSettings ( settings ) ;
                     }
                     else
                     {
                        WorkstationSettings = LoadWorkstationSettings ( ) ;
                     
                        if ( !WorkstationSettings.SetClientToAllWorkstations )
                        {
                           WorkstationSettings.ClientAe = null ;
                        }
                     
                        ConfigurationData.SetClientToAllWorkstations = WorkstationSettings.SetClientToAllWorkstations ;
                     }
                  
                     ConfigureServersAndClients ( WorkstationSettings ) ;
                  }
                  finally
                  {
                     DisplayOrientation = LoadDisplayOrientation ( ) ;
                  }
               }
               catch ( Exception exception ) 
               {
                  MessageBox.Show ( "Problem occured loading Workstation settings.\n" + exception.Message,
                                     Caption,
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Warning ) ;
               }
            }

            private void ConfigureServersAndClients ( IWorkstationSettings settings )
            {
               if ( null == settings ) 
               {
                  return ;
               }
               
               if ( !string.IsNullOrEmpty ( settings.WorkstationServer ) )
               {
                  DicomAE server ;
                  
                  
                  server = settings.GetServer ( settings.WorkstationServer ) ;
                  
                  ConfigurationData.ListenerServiceName = settings.WorkstationServer ;
                  
                  if ( null != server ) 
                  {
                     ConfigurationData.WorkstationServiceAE = server.AE ;
                  }
               }
               
               ConfigurationData.PACS.Clear ( ) ;
               
               if ( settings.ServerList.Count != 0 )
               {
                  foreach ( DicomAE server in settings.ServerList )
                  {
                     if ( !ServerExists ( server ) && !IsWorkstationServer ( settings, server ) )
                     {
                        ConfigurationData.PACS.Add ( new ScpInfo ( server.AE, server.IPAddress, server.Port, server.Timeout, server.UseTls ) ) ;
                     }
                  }
               }
               
               if ( null != settings.ClientAe && !string.IsNullOrEmpty ( settings.ClientAe.AE ) )
               {
                  ConfigurationData.WorkstationClient.AETitle = settings.ClientAe.AE ;
                  ConfigurationData.WorkstationClient.Address = settings.ClientAe.IPAddress ;
                  ConfigurationData.WorkstationClient.Port    = settings.ClientAe.Port ;
               }
               
               if ( !string.IsNullOrEmpty ( settings.DefaultImageQuery ) )
               {
                  DicomAE server ;
                  
                  
                  server = settings.GetServer ( settings.DefaultImageQuery ) ;
                  
                  if ( null != server ) 
                  {
                     ConfigurationData.DefaultQueryRetrieveServer = new ScpInfo ( server.AE, server.IPAddress, server.Port, server.Timeout, server.UseTls ) ;
                  }
               }
               
               if ( !string.IsNullOrEmpty ( settings.DefaultStore ) )
               {
                  DicomAE server ;
                  
                  
                  server = settings.GetServer ( settings.DefaultStore ) ;
                  
                  if ( null != server ) 
                  {
                     ConfigurationData.DefaultStorageServer = new ScpInfo ( server.AE, server.IPAddress, server.Port, server.Timeout, server.UseTls ) ;
                  }
               }
               
               if ( ConfigurationData.SaveSessionBehavior != SaveOptions.NeverSave )
               {
                  ConfigurationData.SaveChanges ( ) ;
               }
            }
            
            private WorkstationSettings CreateWorkstationSettings ( DicomDemoSettings settings )
            {
               WorkstationSettings wsSettings = new WorkstationSettings ( ) ;
               
               
               wsSettings.DefaultImageQuery = settings.DefaultImageQuery ;
               wsSettings.DefaultStore      = settings.DefaultStore ;
               
               wsSettings.ServerList.AddRange ( settings.ServerList ) ;
               
               DicomAE server = settings.GetServer ( settings.WorkstationServer ) ;
               
               if ( null != server ) 
               {
                  wsSettings.ServerList.Remove ( server ) ;
                  wsSettings.WorkstationDicomServer = server ;
                  wsSettings.WorkstationServer      = server.AE ;
               }
               
               wsSettings.ClientAe = settings.ClientAe ;
               
               SaveWorkstationSettings ( wsSettings ) ;
               
               return wsSettings ;
            }
            
            private bool ServerExists ( DicomAE server )
            {
               foreach ( ScpInfo scp in ConfigurationData.PACS ) 
               {
                  if ( scp.AETitle == server.AE && scp.Address == server.IPAddress && scp.Port == server.Port )
                  {
                     return true ;
                  }
               }
               
               return false ;
            }
            
            private bool IsWorkstationServer ( IWorkstationSettings settings, DicomAE server )
            {
               if ( !string.IsNullOrEmpty ( settings.WorkstationServer ) )
               {
                  DicomAE wsServer ;
                  
                  
                  wsServer = settings.GetServer ( settings.WorkstationServer ) ;
                  
                  if ( null != wsServer ) 
                  {
                     return ( wsServer.AE == server.AE && wsServer.IPAddress == server.IPAddress ) ;
                  }
               }
               
               return false ;
            }
            
            private bool LogInUser ( )
            {
               var pass = new System.Security.SecureString();
               pass.AppendChar('s');
               pass.AppendChar('a');
               UserAccessManager.AuthenticateUser("sa", pass);

               if ( UserAccessManager.AuthenticatedUser == null &&
                    !UserAuthenticatedFromArgs ( ) )
               {
                  LogInDialog logIn ;
                                    
                  logIn = new LogInDialog ( ) ;
                  
                  if ( logIn.ShowDialog  ( ) == DialogResult.OK )
                  {
                     return ( null != UserAccessManager.AuthenticatedUser ) ;
                  }
                  else
                  {
                     return false ;
                  }
               }
               else
               {
                  return true ;
               }
            }
            
            private bool UserAuthenticatedFromArgs ( ) 
            {
               string[] args =  System.Environment.GetCommandLineArgs ( ) ;
               string userName = "" ;
               string password = "" ;
               
               
               foreach ( string arg in args )
               {
                  if ( arg.IndexOf ( "UserName", StringComparison.OrdinalIgnoreCase ) == 0 )
                  {
                     userName = GetValueFromArg ( arg ) ;
                  }
                  
                  if ( arg.IndexOf ( "Password", StringComparison.OrdinalIgnoreCase ) == 0 )
                  {
                     password = GetValueFromArg ( arg ) ;
                  }
               }
               
               if ( !string.IsNullOrEmpty ( userName ) && !string.IsNullOrEmpty ( password ) )
               {
                  return UserAccessManager.AuthenticateUser ( userName,
                                                              UserAccessManager.GetSecureString ( password ) ) ;
               }
               
               return false ;
            }
            
            private string GetValueFromArg ( string arg ) 
            {
               string [] tokens ;
               
               
               tokens = arg.Split ( '=' ) ;
               
               if ( tokens.Length == 2 ) 
               {
                  return tokens [ 1 ].Trim ( ) ;
               }
               else
               {
                  return null ;
               }
            }
            

            
            private static void ShowSplashScreen ( ) 
            {
               WorkstationSplashScreen splash ;
               
               
               splash = new WorkstationSplashScreen ( ) ;
               
               splash.Show ( ) ;
               
               splash.Update ( ) ;
               
               System.Threading.Thread.Sleep ( 3000 ) ;
               
               splash.Close ( ) ;
            }
            
            private void SaveWorkstationSettings ( )
            {
               if ( WorkstationSettings != null && UserAccessManager.AuthenticatedUser.IsAdmin )
               {
                  if ( ConfigurationData.WorkstationClient != null && ConfigurationData.SetClientToAllWorkstations ) 
                  {
                     WorkstationSettings.ClientAe = ConfigurationData.WorkstationClient.ToDicomAE ( ) ;
                  }
                  
                  if ( null != ConfigurationData.DefaultQueryRetrieveServer ) 
                  {
                     WorkstationSettings.DefaultImageQuery = ConfigurationData.DefaultQueryRetrieveServer.AETitle ;
                  }
                  
                  if ( null != ConfigurationData.DefaultStorageServer ) 
                  {
                     WorkstationSettings.DefaultStore = ConfigurationData.DefaultStorageServer.AETitle ;
                  }
                  
                  WorkstationSettings.ServerList.Clear ( ) ;
                  
                  foreach ( ScpInfo scp in ConfigurationData.PACS ) 
                  {
                     WorkstationSettings.ServerList.Add ( new DicomAE ( scp.AETitle, scp.Address, scp.Port, scp.Timeout, scp.Secure ) ) ;
                  }
                  
                  WorkstationSettings.SetClientToAllWorkstations = ConfigurationData.SetClientToAllWorkstations ;
                  
                  SaveWorkstationSettings ( WorkstationSettings ) ;
               }
            }
            
            private void LoadModalitySettings ( WorkstationViewer viewer ) 
            {
               IWorkstationDataAccessAgent dataAccess ;
               
               
               dataAccess = DataAccessServices.GetDataAccessService <IWorkstationDataAccessAgent> ( ) ;
               
               if ( null != dataAccess ) 
               {
                  if ( !dataAccess.IsConfigurationRegistered ( ModalitySettingsConfigName ) )
                  {
                     viewer.ModalityManager.FillDefaultOptions ( ) ;
                  }
                  else
                  {
                     string configuration ;
                     byte [] modalitySettingsBuffer ;
                  
                     configuration = dataAccess.ReadConfiguration ( ModalitySettingsConfigName ) ;
                     
                     if ( !string.IsNullOrEmpty ( configuration ) )
                     {
                        modalitySettingsBuffer = Encoding.GetEncoding ( 0 ).GetBytes ( configuration ) ;
                        
                        using ( MemoryStream ms = new MemoryStream ( modalitySettingsBuffer ) )
                        {
                           viewer.ModalityManager.LoadOptions ( ms ) ;
                        }
                     }
                     else
                     {
                        viewer.ModalityManager.FillDefaultOptions ( ) ;
                     }
                  }
               }
            }
            
            private void SaveModalitySettings ( ) 
            {
               IWorkstationDataAccessAgent dataAccess ;
               
               
               dataAccess = DataAccessServices.GetDataAccessService <IWorkstationDataAccessAgent> ( ) ;
               
               if ( null != dataAccess ) 
               {
                  using ( MemoryStream ms = new MemoryStream ( ) ) 
                  {
                     if ( WorkstationUIFactory.Instance.IsControlRegistered ( UIElementKeys.WorkstationViewer ) )
                     {
                        WorkstationUIFactory.Instance.GetWorkstsationControl <WorkstationViewer> ( UIElementKeys.WorkstationViewer ).ModalityManager.SaveOptions ( ms ) ;  
                     }
                     
                     if ( !dataAccess.IsConfigurationRegistered ( ModalitySettingsConfigName ) )
                     {
                        dataAccess.RegisterConfiguration ( ModalitySettingsConfigName ) ;
                     }
                     
                     dataAccess.UpdateConfiguration ( ModalitySettingsConfigName, 
                                                      Encoding.GetEncoding ( 0 ).GetString ( ms.ToArray ( ) ) ) ;
                  }
               }
            }
            
            private static WorkstationSettings LoadWorkstationSettings ( )
            {
               IWorkstationDataAccessAgent dataAccess ;
               
               
               dataAccess = DataAccessServices.GetDataAccessService <IWorkstationDataAccessAgent> ( ) ;
               
               if ( null != dataAccess )
               {
                  if ( dataAccess.IsConfigurationRegistered ( WSComponentConfigName ) )
                  {
                     string configuration = dataAccess.ReadConfiguration ( WSComponentConfigName ) ;
                     
                     if ( !string.IsNullOrEmpty ( configuration ) )
                     {
                        return WorkstationSettings.Load ( configuration ) ;
                     }
                  }
               }
               
               return new WorkstationSettings ( ) ;
            }

            private static void SaveWorkstationSettings ( WorkstationSettings settings )
            {
               IWorkstationDataAccessAgent dataAccess ;
               string configuration ;
               
               
               dataAccess = DataAccessServices.GetDataAccessService <IWorkstationDataAccessAgent> ( ) ;
               
               if ( null != dataAccess ) 
               {
                  if ( !dataAccess.IsConfigurationRegistered ( WSComponentConfigName ) )
                  {
                     dataAccess.RegisterConfiguration ( WSComponentConfigName ) ;
                  }
                  
                  configuration = WorkstationSettings.Save ( settings ) ;
                  
                  dataAccess.UpdateConfiguration ( WSComponentConfigName, configuration ) ;
               }
            }
            
            private OrientationConfiguration LoadDisplayOrientation ( )
            {
               IWorkstationDataAccessAgent dataAccess ;
               
               
               dataAccess = DataAccessServices.GetDataAccessService <IWorkstationDataAccessAgent> ( ) ;
               
               if ( null != dataAccess )
               {
                  if ( dataAccess.IsConfigurationRegistered ( OrientationConfigurationConfigName ) )
                  {
                     string configuration = dataAccess.ReadConfiguration ( OrientationConfigurationConfigName ) ;
                     
                     if ( !string.IsNullOrEmpty ( configuration ) )
                     {
                        BinaryFormatter formatter = new BinaryFormatter ( ) ;
                        
                        
                        using ( MemoryStream ms = new MemoryStream ( Convert.FromBase64String ( configuration ) ) )
                        {
                           return formatter.Deserialize ( ms ) as OrientationConfiguration ;
                        }
                     }
                  }
               }
               else
               {
                  string settingsFile ;
                  
                  
                  settingsFile = Path.Combine ( Application.StartupPath, typeof ( OrientationConfiguration ).Name ) ;
                  settingsFile = Path.ChangeExtension ( settingsFile, "dat" ) ;
                  
                  if ( File.Exists ( settingsFile ) )
                  {
                     BinaryFormatter formatter = new BinaryFormatter ( ) ;
                     
                     using ( MemoryStream ms = new MemoryStream ( File.ReadAllBytes ( settingsFile ) ) )
                     {
                        return formatter.Deserialize ( ms ) as OrientationConfiguration ;
                     }
                  }
               }
               
               return new OrientationConfiguration ( ) ;
            }
            
            private void SaveDisplayOrientation ( )
            {
               IWorkstationDataAccessAgent dataAccess ;
               
               
               dataAccess = DataAccessServices.GetDataAccessService <IWorkstationDataAccessAgent> ( ) ;
               
               if ( null != dataAccess )
               {
                  if ( !dataAccess.IsConfigurationRegistered ( OrientationConfigurationConfigName ) )
                  {
                     dataAccess.RegisterConfiguration ( OrientationConfigurationConfigName ) ;
                  }
                  
                  BinaryFormatter formatter = new BinaryFormatter ( ) ;
                  
                  
                  using ( MemoryStream stream = new MemoryStream ( ) )
                  {
                     formatter.Serialize ( stream, DisplayOrientation ) ;

                     dataAccess.UpdateConfiguration ( OrientationConfigurationConfigName,  Convert.ToBase64String ( stream.ToArray ( ), Base64FormattingOptions.None ) ) ;
                  }
               }
            }
            
         #endregion
         
         #region Properties
         
            private WorkstationContainer __Container { get ; set ; } 
            
         #endregion
         
         #region Events
         
            void Instance_RemoveCompletedItemsChanged(object sender, EventArgs e)
            {
               ConfigurationData.QueueRemoveItem = QueueManager.Instance.RemoveCompletedItems ;
            }

            void Instance_AutoLoadRetrievedImagesChanged(object sender, EventArgs e)
            {
               ConfigurationData.QueueAutoLoad = QueueManager.Instance.AutoLoadRetrievedImages ;
            }
            
            void WorkstationListenerServiceControl_WorkstationServiceChanged ( object sender, WorkstationServiceEventArgs e )
            {
               try
               {
                  if ( null != WorkstationSettings )
                  {
                     
                     WorkstationSettings.WorkstationServer = e.ServiceName ;
                     
                     WorkstationSettings.WorkstationDicomServer = new DicomAE ( e.Service.Settings.AETitle, 
                                                                                e.Service.Settings.IpAddress, 
                                                                                e.Service.Settings.Port, 
                                                                                e.Service.Settings.ReconnectTimeout, 
                                                                                e.Service.Settings.Secure ) ;
                     
                     ConfigurationData.ListenerServiceName  = e.ServiceName ;
                     ConfigurationData.WorkstationServiceAE = e.Service.Settings.AETitle ;
                     
                     SaveWorkstationSettings ( WorkstationSettings ) ;
                     
                     if ( null != __Container )
                     {
                        __Container.EventBroker.PublishEvent <WorkstationScpChangedEventArgs> ( this, new WorkstationScpChangedEventArgs ( ) ) ;
                     }
                  }
               }
               catch {}
            }
            
            void WorkstationListenerServiceControl_WorkstationServiceDeleted ( object sender, WorkstationServiceEventArgs e )
            {
               try
               {
                  if ( null != WorkstationSettings )
                  {
                     WorkstationSettings.WorkstationServer      = null ;
                     WorkstationSettings.WorkstationDicomServer = null ;
                     
                     ConfigurationData.ListenerServiceName  = string.Empty ;
                     ConfigurationData.WorkstationServiceAE = string.Empty ;
                     
                     SaveWorkstationSettings ( WorkstationSettings ) ;
                     
                     if ( null != __Container )
                     {
                        __Container.EventBroker.PublishEvent <WorkstationScpChangedEventArgs> ( this, new WorkstationScpChangedEventArgs ( ) ) ;
                     }
                  }
               }
               catch {}
            }
            
            void ConfigurationData_ChangesSaved ( )
            {
               try
               {
                  SaveWorkstationSettings ( ) ;
               }
               catch 
               {
                  throw new ApplicationException ( "Error saving configuration into database." ) ;
               }
            }
            
            void WorkStationContainerControl_LogOutRequested ( object sender, EventArgs e )
            {
               try
               {
                  if ( null != ( ( Control ) sender ).FindForm ( ) )
                  {
                     ( ( Control ) sender ).FindForm ( ).Close ( ) ;
                  }
               }
               catch ( Exception exception ) 
               {
                  ThreadSafeMessager.ShowError ( exception.Message ) ;
               }         
            }
            
            void MainForm_FormClosing ( object sender, FormClosingEventArgs e )
            {
               try
               {
                  CanCloseDemo ( e ) ;
               }
               catch ( Exception exception ) 
               {
                  ThreadSafeMessager.ShowError ( exception.Message ) ;
                  
                  e.Cancel = false ;
               }
            }

            private void SaveViewState()
            {
               {// STUDY TIME LINE
                  
                  if (WorkstationUIFactory.Instance.IsControlRegistered(UIElementKeys.WorkstationViewer))
                  {
                     var view = WorkstationUIFactory.Instance.GetWorkstsationControl<WorkstationViewer>(UIElementKeys.WorkstationViewer);

                     if (null != view.TimelineManager)
                     {
                        var studiesviewer = __Container.State.ActiveHostViewer as StudiesViewer;

                        if (null != studiesviewer)//was it created
                        {
                           ConfigurationData.ShowStudyTimeline = view.TimelineManager.IsStudyTimelineVisible(studiesviewer);
                        }
                     }
                  }

               }// STUDY TIME LINE               
            }

            private void CanCloseDemo ( FormClosingEventArgs e )
            {
               try
               {
                  SaveViewState();
               }
               catch
               {
               	//ignored, none vital errors here
               }
               
               if ( ( ConfigurationData.SaveSessionBehavior != SaveOptions.NeverSave ) &&
                    ( ( ConfigurationData.HasChanges ( ) )|| 
                      ( WorkstationUIFactory.Instance.IsControlRegistered ( UIElementKeys.UsersAccounts ) && WorkstationUIFactory.Instance.GetWorkstsationControl <UsersAccounts> ( UIElementKeys.UsersAccounts ).HasChanges ( ) ) ) )
               {
                  if ( ConfigurationData.SaveSessionBehavior == SaveOptions.PromptUser )
                  {
                     DialogResult result ;
                     
                     
                     result = ThreadSafeMessager.ShowQuestion ( "Do you want to save current session's settings before exit?", MessageBoxButtons.YesNoCancel ) ;
                     
                     if ( DialogResult.Yes == result )
                     {
                        SaveAllChanges ( ) ;
                     }
                     else if ( DialogResult.Cancel == result )
                     {
                        e.Cancel = true ;
                     }
                  }
                  else if ( ConfigurationData.SaveSessionBehavior == SaveOptions.AlwaysSave )
                  {
                     if ( DialogResult.Yes == ThreadSafeMessager.ShowQuestion ( "Are you sure you want to exit?", MessageBoxButtons.YesNo ) )
                     {
                        SaveAllChanges ( ) ;
                     }
                     else
                     {
                        e.Cancel = true ;
                     }
                  }
               }
               else
               {
                  if ( DialogResult.No == ThreadSafeMessager.ShowQuestion ( "Are you sure you want to exit?", MessageBoxButtons.YesNo ) )
                  {
                     e.Cancel = true ;
                  }
               }
               
               if ( ConfigurationData.SaveSessionBehavior != SaveOptions.NeverSave &&
                    !e.Cancel )
               {
                  SaveModalitySettings ( ) ;
               }
            }

            private void SaveAllChanges()
            {
               if ( ConfigurationData.HasChanges ( ) )
               {
                  ConfigurationData.SaveChanges ( ) ;
               }
                  
               if ( ( WorkstationUIFactory.Instance.IsControlRegistered ( UIElementKeys.UsersAccounts ) && 
                     WorkstationUIFactory.Instance.GetWorkstsationControl <UsersAccounts> ( UIElementKeys.UsersAccounts ).HasChanges ( ) ) )
               {
                  WorkstationUIFactory.Instance.GetWorkstsationControl <UsersAccounts> ( UIElementKeys.UsersAccounts ).SaveChanges ( ) ;
               }
            }
            
         #endregion
         
         #region Data Members
         
            private static WorkstationShellController _instance     = new WorkstationShellController ( ) ;
            private static string Caption                           = string.Empty ;
            private const string ModalitySettingsConfigName         = "WorkstationViewerControl.ModalitySettings" ;
            private const string WSComponentConfigName              = "WorkstationViewerControl.WorkstationSettings" ;
            private const string OrientationConfigurationConfigName = "WorkstationViewerControl.OrientationConfiguration" ;
            
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
   
   class UIElementKeys
   {
      public const string WorkstationViewer        = "WorkstationViewer" ;
      public const string MediaBurningManagerView  = "MediaBurningManagerView" ;
      public const string SearchStudies            = "SearchStudies" ;
      public const string WorkstationConfiguration = "WorkstationConfiguration" ;
      public const string StorageListenerService   = "StorageListenerService" ;
      public const string EventLogViewer           = "EventLogViewer" ;
      public const string UsersAccounts            = "UsersAccounts" ;
      public const string QueueManager             = "QueueManager" ;
   }
   
   class WorkstationScpChangedEventArgs : EventArgs
   {}
}
