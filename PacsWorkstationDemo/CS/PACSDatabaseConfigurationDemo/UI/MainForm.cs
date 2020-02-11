// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using Leadtools.Demos;
using Leadtools.Medical.Logging.DataAccessLayer.Configuration;
using Leadtools.Medical.Storage.DataAccessLayer.Configuration;
using Leadtools.Medical.DataAccessLayer.Configuration;
using Leadtools.Medical.Worklist.DataAccessLayer.Configuration;
using Leadtools.Medical.Workstation.DataAccessLayer.Configuration;
using Leadtools.Medical.UserManagementDataAccessLayer.Configuration;
using Leadtools.Medical.Worklist.DataAccessLayer.BusinessEntity;
using System.Reflection;
using System.IO;
using Leadtools.Medical.Worklist.DataAccessLayer;
using Leadtools.Medical.DataAccessLayer;
using Leadtools.Medical.UserManagementDataAccessLayer;
using System.Security;
using Leadtools.Logging.Configuration;
using Leadtools.Medical.Logging.DataAccessLayer;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Leadtools.Medical.Media.DataAccessLayer.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Leadtools.DicomDemos;
using CSPacsDatabaseConfigurationDemo.Properties;
using Leadtools.Medical.OptionsDataAccessLayer.Configuration;
using Leadtools.Medical.AeManagement.DataAccessLayer.Configuration;
using Leadtools.Medical.Winforms;
using Leadtools.Medical.Winforms.DataAccessLayer.Configuration;
using Leadtools.Medical.PermissionsManagement.DataAccessLayer.Configuration;
using Leadtools.Medical.Forward.DataAccessLayer.Configuration;
using Leadtools.Medical.OptionsDataAccessLayer;
using Leadtools.Medical.PermissionsManagement.DataAccessLayer;
using Leadtools.Dicom.Scp.Command;
using Leadtools.Dicom;
using Leadtools.Medical.Storage.DataAccessLayer;
using Leadtools.Dicom.Scp;
using Leadtools.Logging;
using Leadtools.Logging.Medical;
using System.Text.RegularExpressions;
using Leadtools.Demos.StorageServer.DataTypes;
using Leadtools.Medical.WebViewer.PatientAccessRights.DataAccessAgent;
using Leadtools.Medical.ExportLayout.DataAccessLayer.Configuration;
#if (LEADTOOLS_V19_OR_LATER_MEDICAL_EXTERNAL_STORE) || (LEADTOOLS_V19_OR_LATER)
using Leadtools.Medical.ExternalStore.DataAccessLayer.Configuration;
using Leadtools.Demos.Sql;
using CSPacsDatabaseConfigurationDemo.UI;
#endif

namespace MedicalWorkstationConfigurationDemo.UI
{
   public partial class MainForm : Form
   {
      #region Public
         
         #region Methods
         
            public MainForm ( )
            {
               InitializeComponent ( ) ;
               
               Init ( ) ;
               
               RegisterEvents ( ) ;
               
               bool append = ConfigurationData.AppendProcessType ;
            }

         #endregion
         
         #region Properties
         public Configuration ConfigMachine
         {
            get;
            set;
         }
         
         public Configuration ConfigGlobalPacs
         {
            get;
            set;
         }
         
         public IConfigurationSource ConfigSource
         {
            get;
            set;
         }
         
         public bool GlobalPacsAlreadyExists
         {
            get;
            set;
         }

         private  List<string> _databaseLocations = new List<string>();
            
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
         
            private void SetProcessString ( bool is64, Action <string> setter ) 
            {
               if ( ConfigurationData.AppendProcessType )
               {
                  if ( is64 ) 
                  {
                     setter ( "_64" ) ;
                  }
                  else
                  {
                     setter ( "_32" ) ;
                  }
               }
            }
            
            private void Init ( ) 
            {
               ConfigMachine = GetConfiguration();
               GlobalPacsAlreadyExists = VerifyAlreadyExistGlobalPacsConfig();
               ConfigGlobalPacs = DicomDemoSettingsManager.GetGlobalPacsConfiguration();
               ConfigSource = GetConfigurationSource();
               
               string             userName ;
               DatabaseComponents supportedDbs ;
               
               
               Icon     = ConfigurationData.ApplicationIcon ;
               ShowIcon = true ;
               
               __CurrentControl = null ;

               __StorageServerConnectionString  = new ConnectionStringSettings(); // GetConnectionString( ConfigGlobalPacs, new LoggingDataAccessConfigurationView().DataAccessSettingsSectionName);
               __MedicalWorkstationConnectionString = new ConnectionStringSettings();

               __StorageServerOptionsConnectionString = GetConnectionString(ConfigMachine, new OptionsDataAccessConfigurationView().DataAccessSettingsSectionName); // new ConnectionStringSettings();
               __PatientUpdaterConnectionString = new ConnectionStringSettings(); //GetConnectionString(ConfigMachine, new UserManagementDataAccessConfigurationView().DataAccessSettingsSectionName); // new ConnectionStringSettings();
               
               __LoggingConnectionString        = GetConnectionString( ConfigMachine, new LoggingDataAccessConfigurationView().DataAccessSettingsSectionName);
               __StorageConnectionString        = GetConnectionString( ConfigMachine, new StorageDataAccessConfigurationView().DataAccessSettingsSectionName);
               __WorklistConnectionString       = GetConnectionString( ConfigMachine, new WorklistDataAccessConfigurationView().DataAccessSettingsSectionName);
               __MediaCreationConnectionString  = GetConnectionString( ConfigMachine, new MediaCreationDataAccessConfigurationView().DataAccessSettingsSectionName);
               __WorkstationConnectionString    = GetConnectionString( ConfigMachine, new WorkstationDataAccessConfigurationView().DataAccessSettingsSectionName);
               __UserAccessConnectionString     = GetConnectionString( ConfigMachine, new UserManagementDataAccessConfigurationView().DataAccessSettingsSectionName);
               __ActiveComponentCommand         = null ;
               __DisplayDbConfigCommandList     = new List<DisplayComponentConfigurationCommand> ( ) ;
               
               userName = Environment.UserName ;
               databaseOptions1.WorkstationUserName     = userName ;
               databaseOptions1.WorkstationUserPassword = userName ;
               
               supportedDbs = ConfigurationData.GetSupportedDatabase ( ) ;
               
               SetDefaultDatabasesName ( ) ;
               databaseOptions1.SupportedDatabases = supportedDbs ;

               SetDisplayComponentConfigCommand ( DatabaseComponents.StorageServer, supportedDbs );
               SetDisplayComponentConfigCommand ( DatabaseComponents.StorageServerOptions, supportedDbs);
               SetDisplayComponentConfigCommand ( DatabaseComponents.PatientUpdater, supportedDbs );
               SetDisplayComponentConfigCommand ( DatabaseComponents.Logging, supportedDbs ) ;
               SetDisplayComponentConfigCommand ( DatabaseComponents.Storage, supportedDbs ) ;
               SetDisplayComponentConfigCommand ( DatabaseComponents.Worklist, supportedDbs ) ;
               SetDisplayComponentConfigCommand ( DatabaseComponents.MediaCreation, supportedDbs ) ;
               SetDisplayComponentConfigCommand ( DatabaseComponents.Workstation, supportedDbs ) ;
               SetDisplayComponentConfigCommand ( DatabaseComponents.UserManagement, supportedDbs ) ;

               
               databaseOptions1.Text   = "This wizard allows you to configure the databases that are required for the various PACS Framework components to function." ;
               connectionSummary1.Text = "The following configuration will be applied. To change the configuration click \"Back\", to proceed click \"Next\"." ;
               
               connectionConfiguration1.CanChangeProvider = ConnectionProviders.FromProvider ( ConfigurationData.GetSupportedProvider ( ) ).Length > 1 ;
            }

            private void SetDefaultDatabasesName (  )
            {
               bool is64 ;
               
               
               is64 = Is64Process ( ) ;

               if (string.IsNullOrEmpty(__StorageServerConnectionString.Name))
               {
                  __StorageServerConnectionString.Name = ConfigurationData.DefaultStorageServerDbName;

                  SetProcessString(is64, delegate(string process) { __StorageServerConnectionString.Name += process; });
               }

               if (string.IsNullOrEmpty(__StorageServerOptionsConnectionString.Name))
               {
                  __StorageServerOptionsConnectionString.Name = ConfigurationData.DefaultStorageServerOptionsDbName;

                  SetProcessString(is64, delegate(string process) { __StorageServerOptionsConnectionString.Name += process; });
               }

               if (string.IsNullOrEmpty(__PatientUpdaterConnectionString.Name))
               {
                  __PatientUpdaterConnectionString.Name = ConfigurationData.DefaultPatientUpdaterDbName;

                  SetProcessString(is64, delegate(string process) { __PatientUpdaterConnectionString.Name += process; });
               }
               
               if ( string.IsNullOrEmpty ( __LoggingConnectionString.Name ) )
               {
                  __LoggingConnectionString.Name = ConfigurationData.DefaultLoggingDbName ;
                  
                  SetProcessString ( is64, delegate ( string process ) { __LoggingConnectionString.Name += process ; } ) ;
               }

               if ( string.IsNullOrEmpty ( __StorageConnectionString.Name ) )
               {
                  __StorageConnectionString.Name = ConfigurationData.DefaultStorageDbName  ;
                  
                  SetProcessString ( is64, delegate ( string process ) { __StorageConnectionString.Name += process ; } ) ;
               }
               
               if ( string.IsNullOrEmpty ( __WorklistConnectionString.Name ) )
               {
                  __WorklistConnectionString.Name = ConfigurationData.DefaultWorklistDbName  ;
                  
                  SetProcessString ( is64, delegate ( string process ) { __WorklistConnectionString.Name += process ; } ) ;
               }
               
               if ( string.IsNullOrEmpty ( __MediaCreationConnectionString.Name ) )
               {
                  __MediaCreationConnectionString.Name = ConfigurationData.DefaultMediaCreationDbName  ;
                  
                  SetProcessString ( is64, delegate ( string process ) { __MediaCreationConnectionString.Name += process ; } ) ;
               }
               
               if ( string.IsNullOrEmpty ( __WorkstationConnectionString.Name ) )
               {
                  __WorkstationConnectionString.Name = ConfigurationData.DefaultWorkstationDbName ;
                  
                  SetProcessString ( is64, delegate ( string process ) { __WorkstationConnectionString.Name += process ; } ) ;
               }

               if ( string.IsNullOrEmpty ( __UserAccessConnectionString.Name ) )
               {
                  __UserAccessConnectionString.Name  = ConfigurationData.DefaultUserAccessDbName ;
                  
                  SetProcessString ( is64, delegate ( string process ) { __UserAccessConnectionString.Name += process ; } ) ;
               }
               
               if ( string.IsNullOrEmpty ( __MedicalWorkstationConnectionString.Name ) )
               {
                  __MedicalWorkstationConnectionString.Name  = ConfigurationData.DefaultMedicalWorkstationDbName ;
                  
                  SetProcessString ( is64, delegate ( string process ) { __MedicalWorkstationConnectionString.Name += process ; } ) ;
               }
            }

            private void SetDisplayComponentConfigCommand ( DatabaseComponents database, DatabaseComponents supportedDbs )
            {
               
               if ( ( supportedDbs & database ) == database )
               {
                  DisplayComponentConfigurationCommand command;
                  
                  
                  command = new DisplayComponentConfigurationCommand ( GetSelectedComponents, 
                                                                       database, 
                                                                       connectionConfiguration1 ) ;

                  command.Executing += new EventHandler ( command_Executing ) ;
                  command.Executed  += new EventHandler ( command_Executed ) ;
                  
                  __DisplayDbConfigCommandList.Add ( command ) ;
               }
            }

            private void RegisterEvents()
            {
               this.Load                     += new EventHandler ( MainForm_Load ) ;
               ButtonNext.Click              += new EventHandler ( ButtonNext_Click ) ;
               ButtonBack.Click              += new EventHandler ( ButtonBack_Click ) ;
               databaseOptions1.ValueChanged += new EventHandler ( databaseOptions1_ValueChanged ) ;
            }

            void MainForm_Load ( object sender, EventArgs e )
            {
               if (DatabaseOptions.DefaultConnectToExistingDatabaseFromConfiguration() && !DatabaseOptions.DisableCreateFromConfiguration())
               {
                  databaseOptions1.Mode = DbConfigurationMode.Configure;
               }
               else
               {
                  databaseOptions1.Mode = DbConfigurationMode.Create;
               }

               SetCurrentControl ( databaseOptions1 ) ;
            }
            
            private DatabaseComponents GetSelectedComponents ( )
            {
               DatabaseComponents components ;
               
               
               components = DatabaseComponents.None ;
               
               if ( databaseOptions1.LoggingDbSelected ) 
               {
                  components |= DatabaseComponents.Logging ;
               }
               
               if ( databaseOptions1.StorageDbSelected ) 
               {
                  components |= DatabaseComponents.Storage ;
               }
               
               if ( databaseOptions1.WorklistDbSelected ) 
               {
                  components |= DatabaseComponents.Worklist ;
               }
               
               if ( databaseOptions1.WorkstationDbSelected )
               {
                  components |= DatabaseComponents.Workstation ;
               }
               
               if ( databaseOptions1.UserManagementDbSelected ) 
               {
                  components |= DatabaseComponents.UserManagement ;
               }
               
               if ( databaseOptions1.MediaCreationDbSelected ) 
               {
                  components |= DatabaseComponents.MediaCreation ;
               }

               if (databaseOptions1.StorageServerDbSelected)
               {
                  components |= DatabaseComponents.StorageServer;
               }

               if (databaseOptions1.StorageServerOptionsDbSelected)
               {
                  components |= DatabaseComponents.StorageServerOptions;
               }

               if (databaseOptions1.PatientUpdaterDbSelected)
               {
                  components |= DatabaseComponents.PatientUpdater;
               }
               
               return components ;
            }
            
            private bool ExecuteCommand ( DisplayComponentConfigurationCommand command )
            {
               if ( command.CanExecute ( ) )
               {
                  command.Execute();
                  
                  return true ;
               }
               else
               {
                  return false ;
               }
            }

            private void SetConnectionStringSettings ( DatabaseComponents database )
            {
               switch ( database )
               {
                  case DatabaseComponents.StorageServer:
                     {
                        __StorageServerConnectionString.ConnectionString = connectionConfiguration1.ConnectionString;
                        __StorageServerConnectionString.ProviderName = connectionConfiguration1.DataProvider;
                     }
                     break;

                  case DatabaseComponents.StorageServerOptions:
                     {
                        __StorageServerOptionsConnectionString.ConnectionString = connectionConfiguration1.ConnectionString;
                        __StorageServerOptionsConnectionString.ProviderName = connectionConfiguration1.DataProvider;
                     }
                     break;

                  case DatabaseComponents.PatientUpdater:
                     {
                        __PatientUpdaterConnectionString.ConnectionString = connectionConfiguration1.ConnectionString;
                        __PatientUpdaterConnectionString.ProviderName = connectionConfiguration1.DataProvider;
                     }
                     break;

                  case DatabaseComponents.Logging:
                     {
                        __LoggingConnectionString.ConnectionString = connectionConfiguration1.ConnectionString;
                        __LoggingConnectionString.ProviderName = connectionConfiguration1.DataProvider;
                     }
                     break;
                  
                  case DatabaseComponents.Storage:
                  {
                     __StorageConnectionString.ConnectionString = connectionConfiguration1.ConnectionString ;
                     __StorageConnectionString.ProviderName     = connectionConfiguration1.DataProvider ;
                  }
                  break ;
                  
                  case DatabaseComponents.Worklist:
                  {
                     __WorklistConnectionString.ConnectionString = connectionConfiguration1.ConnectionString ;
                     __WorklistConnectionString.ProviderName     = connectionConfiguration1.DataProvider ;
                  }
                  break ;
                  
                  case DatabaseComponents.MediaCreation:
                  {
                     __MediaCreationConnectionString.ConnectionString = connectionConfiguration1.ConnectionString ;
                     __MediaCreationConnectionString.ProviderName     = connectionConfiguration1.DataProvider ;
                  }
                  break ;
                  
                  case DatabaseComponents.Workstation:
                  {
                     __WorkstationConnectionString.ConnectionString = connectionConfiguration1.ConnectionString ;
                     __WorkstationConnectionString.ProviderName     = connectionConfiguration1.DataProvider ;
                  }
                  break ;
                  
                  case DatabaseComponents.UserManagement:
                  {
                     __UserAccessConnectionString.ConnectionString = connectionConfiguration1.ConnectionString ;
                     __UserAccessConnectionString.ProviderName     = connectionConfiguration1.DataProvider ;
                  }
                  break;
               }
            }
            
            
            private ConnectionStringSettings GetConnectionStringSettings ( DatabaseComponents database )
            {
               switch ( database )
               {
                  case DatabaseComponents.StorageServer:
                     {
                        return __StorageServerConnectionString;
                     }

                  case DatabaseComponents.StorageServerOptions:
                     {
                        return __StorageServerOptionsConnectionString;
                     }

                  case DatabaseComponents.PatientUpdater:
                     {
                        return __PatientUpdaterConnectionString;
                     }

                  case DatabaseComponents.Logging:
                     {
                        return __LoggingConnectionString;
                     }
                  
                  case DatabaseComponents.Storage:
                  {
                     return __StorageConnectionString ;
                  }
                  
                  case DatabaseComponents.Worklist:
                  {
                     return __WorklistConnectionString ;
                  }
                  
                  case DatabaseComponents.MediaCreation:
                  {
                     return __MediaCreationConnectionString ;
                  }
                  
                  case DatabaseComponents.Workstation:
                  {
                     return __WorkstationConnectionString ;
                  }

                  case DatabaseComponents.UserManagement:
                  {
                     return __UserAccessConnectionString;
                  }
               }
               
               return null ;
            }
            
            private static string MaskPassword(string s)
            {
               string result = s;
               if (string.IsNullOrEmpty(s))
                  return result;
                  
               string passwordRegEx = "password=[^;]+;";

               Match match = Regex.Match(s, passwordRegEx, RegexOptions.IgnoreCase);

               if (match.Success)
               {
                  string passwordString = match.Groups[0].Value;
                  result = s.Replace(passwordString, "password=*****;");
               }

               return result;
            }
            
            private string BuildConfigurationSummary()
            {
               string summary ;
               
               
               summary = string.Format ( "The following databases will be {0}", ( databaseOptions1.Mode == DbConfigurationMode.Create ) ? "created" : "configured" ) ;

               if (databaseOptions1.StorageServerDbSelected)
               {
                  summary += string.Format("\n\nStorage Server Database:\nConnection:{0}\nProvider:{1}", MaskPassword(__StorageServerConnectionString.ConnectionString), __StorageServerConnectionString.ProviderName);
               }

               if (databaseOptions1.StorageServerOptionsDbSelected)
               {
                  summary += string.Format("\n\nStorage Server Options Database:\nConnection:{0}\nProvider:{1}", MaskPassword(__StorageServerOptionsConnectionString.ConnectionString), __StorageServerOptionsConnectionString.ProviderName);
               }
               
               if (databaseOptions1.PatientUpdaterDbSelected)
               {
                  summary += string.Format("\n\nPatient Updater Database:\nConnection:{0}\nProvider:{1}", MaskPassword(__PatientUpdaterConnectionString.ConnectionString), __PatientUpdaterConnectionString.ProviderName);
               }
               
               if (databaseOptions1.LoggingDbSelected)
               {
                  summary += string.Format("\n\nLogging Database:\nConnection:{0}\nProvider:{1}", MaskPassword(__LoggingConnectionString.ConnectionString), __LoggingConnectionString.ProviderName);
               }
               
               if ( databaseOptions1.StorageDbSelected ) 
               {
                  summary += string.Format ( "\n\nStorage Database:\nConnection:{0}\nProvider:{1}", MaskPassword(__StorageConnectionString.ConnectionString), __StorageConnectionString.ProviderName ) ;
               }
               
               if ( databaseOptions1.WorklistDbSelected ) 
               {
                  summary += string.Format ( "\n\nWorklist Database:\nConnection:{0}\nProvider:{1}", MaskPassword(__WorklistConnectionString.ConnectionString), __WorklistConnectionString.ProviderName ) ;
               }
               
               if ( databaseOptions1.MediaCreationDbSelected ) 
               {
                  summary += string.Format ( "\n\nMedia Creation Database:\nConnection:{0}\nProvider:{1}", MaskPassword(__MediaCreationConnectionString.ConnectionString), __MediaCreationConnectionString.ProviderName ) ;
               }
               
               if ( databaseOptions1.WorkstationDbSelected )
               {
                  summary += string.Format ( "\n\nWorkstation Database:\nConnection:{0}\nProvider:{1}", MaskPassword(__WorkstationConnectionString.ConnectionString), __WorkstationConnectionString.ProviderName ) ;
               }
               
               if ( databaseOptions1.UserManagementDbSelected ) 
               {
                  summary += string.Format ( "\n\nUser Management Database:\nConnection:{0}\nProvider:{1}", MaskPassword(__UserAccessConnectionString.ConnectionString), __UserAccessConnectionString.ProviderName ) ;
               }
               
               return summary ;
            }

            private void SetCurrentControl ( Control newControl ) 
            {
               if ( null != __CurrentControl ) 
               {
                  __CurrentControl.Visible = false ;
               }
               
               __CurrentControl = newControl ;
               
               __CurrentControl.Visible = true ;
               
               __CurrentControl.BringToFront ( ) ;
               
               HeaderDescriptionLabel.Text = __CurrentControl.Text ;
            }

            private void InitConnectionConfigurationControl 
            ( 
               DatabaseComponents database,
               ConnectionConfiguration connectionControl 
            )
            {
               ConnectionStringSettings currentSettings ;
               string                   defaultDbName ;
               bool                     is64 ;
               
               currentSettings  = null ;
               defaultDbName    = string.Empty ;
               is64             = Is64Process ( ) ;
               
               switch ( database ) 
               {

                  case DatabaseComponents.StorageServer:
                     {
                        defaultDbName = ConfigurationData.DefaultStorageServerDbName;

                        SetProcessString(is64, delegate(string process) { defaultDbName += process; });

                        if (!string.IsNullOrEmpty(__StorageServerConnectionString.ConnectionString))
                        {
                           currentSettings = __StorageServerConnectionString;
                        }
                     }
                     break;
                     
                     case DatabaseComponents.MedicalWorkstation:
                     {
                        defaultDbName = ConfigurationData.DefaultMedicalWorkstationDbName;

                        SetProcessString(is64, delegate(string process) { defaultDbName += process; });

                        if (!string.IsNullOrEmpty(__MedicalWorkstationConnectionString.ConnectionString))
                        {
                           currentSettings = __MedicalWorkstationConnectionString;
                        }
                     }
                     break;

                  case DatabaseComponents.StorageServerOptions:
                     {
                        defaultDbName = ConfigurationData.DefaultStorageServerOptionsDbName;

                        SetProcessString(is64, delegate(string process) { defaultDbName += process; });

                        if (!string.IsNullOrEmpty(__StorageServerOptionsConnectionString.ConnectionString))
                        {
                           currentSettings = __StorageServerOptionsConnectionString;
                        }
                     }
                     break;

                  case DatabaseComponents.PatientUpdater:
                     {
                        defaultDbName = ConfigurationData.DefaultPatientUpdaterDbName;

                        SetProcessString(is64, delegate(string process) { defaultDbName += process; });

                        if (!string.IsNullOrEmpty(__PatientUpdaterConnectionString.ConnectionString))
                        {
                           currentSettings = __PatientUpdaterConnectionString;
                        }
                     }
                     break;
                     
                  case DatabaseComponents.Logging:
                  {
                     defaultDbName = ConfigurationData.DefaultLoggingDbName ;
                     
                     SetProcessString ( is64, delegate ( string process ) { defaultDbName += process ; } ) ;
                     
                     if ( !string.IsNullOrEmpty ( __LoggingConnectionString.ConnectionString ) )
                     {
                        currentSettings = __LoggingConnectionString ;
                     }
                  }
                  break ;
                  
                  case DatabaseComponents.Storage:
                  {
                     defaultDbName = ConfigurationData.DefaultStorageDbName ;
                     
                     SetProcessString ( is64, delegate ( string process ) { defaultDbName += process ; } ) ;
                     
                     if ( !string.IsNullOrEmpty ( __StorageConnectionString.ConnectionString ) )
                     {
                        currentSettings = __StorageConnectionString ;
                     }
                  }
                  break ;
                  
                  case DatabaseComponents.Worklist:
                  {
                     defaultDbName = ConfigurationData.DefaultWorklistDbName ;
                     
                     SetProcessString ( is64, delegate ( string process ) { defaultDbName += process ; } ) ;
                     
                     if ( !string.IsNullOrEmpty ( __WorklistConnectionString.ConnectionString ) )
                     {
                        currentSettings = __WorklistConnectionString ;
                     }
                  }
                  break ;
                  
                  case DatabaseComponents.MediaCreation:
                  {
                     defaultDbName = ConfigurationData.DefaultMediaCreationDbName ;
                     
                     SetProcessString ( is64, delegate ( string process ) { defaultDbName += process ; } ) ;
                     
                     if ( !string.IsNullOrEmpty ( __MediaCreationConnectionString.ConnectionString ) )
                     {
                        currentSettings = __MediaCreationConnectionString ;
                     }
                  }
                  break ;
                  
                  case DatabaseComponents.Workstation:
                  {
                     defaultDbName = ConfigurationData.DefaultWorkstationDbName ;
                     
                     SetProcessString ( is64, delegate ( string process ) { defaultDbName += process ; } ) ;
                     
                     if ( !string.IsNullOrEmpty ( __WorkstationConnectionString.ConnectionString ) )
                     {
                        currentSettings = __WorkstationConnectionString ;
                     }
                  }
                  break ;
                  
                  case DatabaseComponents.UserManagement:
                  {
                     defaultDbName = ConfigurationData.DefaultUserAccessDbName ;
                     
                     SetProcessString ( is64, delegate ( string process ) { defaultDbName += process ; } ) ;
                     
                     if ( !string.IsNullOrEmpty ( __UserAccessConnectionString.ConnectionString ) )
                     {
                        currentSettings = __UserAccessConnectionString ;
                     }
                  }
                  break;
               }
               
               if ( null != currentSettings && IsDataProviderSupported ( currentSettings.ProviderName ) )
               {
                  connectionControl.DefaultSqlCeDatabaseName     = Path.ChangeExtension ( Path.Combine ( GetFolderPath ( ), defaultDbName ), "sdf" ) ;
                  connectionControl.DefaultSqlServerDatabaseName = defaultDbName ;
                  
                  connectionControl.SetConnectionString ( currentSettings.ConnectionString, currentSettings.ProviderName ) ;
               }
               else
               {
                  connectionControl.DefaultSqlCeDatabaseName     = Path.ChangeExtension ( Path.Combine ( GetFolderPath ( ), defaultDbName ), "sdf" ) ;
                  connectionControl.DefaultSqlServerDatabaseName = defaultDbName ;
                  
                  connectionControl.ClearConnectionString ( ) ;
               }
            }

            private bool IsDataProviderSupported ( string provider )
            {
               SupportedProviders providers             = ConfigurationData.GetSupportedProvider ( ) ;
               ConnectionProviders[] supportedProviders = ConnectionProviders.FromProvider ( providers ) ;
               ConnectionProviders   currentProvider    = ConnectionProviders.Parse ( provider ) ;
               
               return ( supportedProviders.Contains ( currentProvider ) ) ;
            }

            private bool IsNonStorageServerDbSelected()
            {
               bool machineConfigChanged =
               databaseOptions1.LoggingDbSelected ||
               databaseOptions1.StorageDbSelected ||
               databaseOptions1.WorklistDbSelected ||
               databaseOptions1.MediaCreationDbSelected ||
               databaseOptions1.WorkstationDbSelected ||
               databaseOptions1.UserManagementDbSelected;

               return machineConfigChanged;
            }

            private void StoreConnectionStrings ( )
            {
               ConfigurationManager.RefreshSection("connectionStrings");
               bool machineConfigChanged = false;
               bool globalPacsConfigChanged = false;
               
               if ( databaseOptions1.StorageServerDbSelected )
               {
                  AddConnectionString(ConfigGlobalPacs.ConnectionStrings, __StorageServerConnectionString);
                  globalPacsConfigChanged = true;
               }

               if (databaseOptions1.StorageServerOptionsDbSelected)
               {
                  AddConnectionString(ConfigGlobalPacs.ConnectionStrings, __StorageServerOptionsConnectionString);
                  globalPacsConfigChanged = true;
               }

               if (databaseOptions1.PatientUpdaterDbSelected)
               {
                  AddConnectionString(ConfigGlobalPacs.ConnectionStrings, __PatientUpdaterConnectionString);
                  globalPacsConfigChanged = true;
               }
               
               if ( databaseOptions1.LoggingDbSelected )
               {
                  AddConnectionString(ConfigGlobalPacs.ConnectionStrings, __LoggingConnectionString);
                  globalPacsConfigChanged = true;

#if !LEADTOOLS_V18_OR_LATER
                  AddConnectionString(ConfigMachine.ConnectionStrings, __LoggingConnectionString);
                  machineConfigChanged = true;
#endif // #if !defined(LEADTOOLS_V18_OR_LATER)
               }
               
               if ( databaseOptions1.StorageDbSelected ) 
               {
                  AddConnectionString(ConfigGlobalPacs.ConnectionStrings, __StorageConnectionString);
                  globalPacsConfigChanged = true;
               }

               if (databaseOptions1.UserManagementDbSelected)
               {
                  AddConnectionString(ConfigGlobalPacs.ConnectionStrings, __UserAccessConnectionString);
                  globalPacsConfigChanged = true;
               }
               
#if LEADTOOLS_V18_OR_LATER
               // For version 18, we only write to globalPacsConfig, no longer write to machine.config
               if ( databaseOptions1.WorklistDbSelected )
               {
                  AddConnectionString(ConfigGlobalPacs.ConnectionStrings, __WorklistConnectionString);
                  globalPacsConfigChanged = true;
               }
               
               if ( databaseOptions1.MediaCreationDbSelected )
               {
                  AddConnectionString(ConfigGlobalPacs.ConnectionStrings, __MediaCreationConnectionString);
                  globalPacsConfigChanged = true;
               }
               
               if ( databaseOptions1.WorkstationDbSelected )
               {
                  AddConnectionString(ConfigGlobalPacs.ConnectionStrings, __WorkstationConnectionString);
                  globalPacsConfigChanged = true;
               }
#else
               // For version 17.5 and earlier, we wrote these three connection strings to machine.config
               if ( databaseOptions1.WorklistDbSelected )
               {
                  AddConnectionString(ConfigMachine.ConnectionStrings, __WorklistConnectionString);
                  machineConfigChanged = true;
               }
               
               if ( databaseOptions1.MediaCreationDbSelected )
               {
                  AddConnectionString(ConfigMachine.ConnectionStrings, __MediaCreationConnectionString);
                  machineConfigChanged = true;
               }
               
               if ( databaseOptions1.WorkstationDbSelected )
               {
                  AddConnectionString(ConfigMachine.ConnectionStrings, __WorkstationConnectionString);
                  machineConfigChanged = true;
               }
#endif // #if LEADTOOLS_V18_OR_LATER

               if (machineConfigChanged)
               {
                  ConfigMachine.Save(ConfigurationSaveMode.Modified);
               }
               
               if (globalPacsConfigChanged)
               {
                  Program.ProcessConfiguration(ConfigGlobalPacs);
                  ConfigGlobalPacs.Save(ConfigurationSaveMode.Modified);
               }
            }

            private static Configuration GetConfiguration ( ) 
            {
               return ConfigurationManager.OpenMachineConfiguration ( ) ;
            }
            
            private static IConfigurationSource GetConfigurationSource ( ) 
            {
               return ConfigurationSourceFactory.Create ( ) ;
            }

            private void AddConnectionString
            (
               ConnectionStringsSection connectionStringsSection, 
               ConnectionStringSettings connectionString
            )
            {
               try
               {
               ConnectionStringSettings tempConnection ;
               
               
               tempConnection = connectionStringsSection.ConnectionStrings [ connectionString.Name ] ;
               
               if ( null == tempConnection ) 
               {
                  connectionStringsSection.ConnectionStrings.Add ( connectionString ) ;
               }
               else
               {
                  tempConnection.ConnectionString = connectionString.ConnectionString ;
                  tempConnection.ProviderName     = connectionString.ProviderName ;
               }
               }
               catch (Exception ex)
               {
                  Messager.ShowError(this, "AddConnectionString Failed");
                  throw ex;
               }
            }
            
            private ConnectionStringSettings GetConnectionString
            (
               Configuration config,
               string dataAccessSectionName
            )
            {
               DataAccessSettings settings ;
               
               
               try
               {
                  settings = config.GetSection ( dataAccessSectionName ) as DataAccessSettings ;
               
                  if ( null == settings ) 
                  {
                     return new ConnectionStringSettings ( ) ;
                  }
                  else
                  {
                     ConnectionStringSettings connection ;
                  
                  
                     connection = config.ConnectionStrings.ConnectionStrings [ settings.ConnectionName ] ;
                  
                     if ( null == connection ) 
                     {
                        return new ConnectionStringSettings ( ) ;
                     }
                     else
                     {
                        return connection ;
                     }
                  }
               }
               catch ( Exception )
               {
                  return new ConnectionStringSettings ( ) ;
               }
            }

            private void ConfigureLogging(string productName, string connectionStringName, DefaultConnectionNameType connectionType)
            {
               try
               {
                  LoggingDataAccessConfigurationView loggingSectionView;

                  loggingSectionView = new LoggingDataAccessConfigurationView(ConfigGlobalPacs, DicomDemoSettingsManager.ProductNameDemoServer, null);
                  loggingSectionView.Configuration = ConfigGlobalPacs;
                  ConfigureSection(ConfigGlobalPacs, loggingSectionView, connectionStringName, productName, connectionType);
#if LTV17_CONFIG
                  AddLoggingChannel ( config ) ;
#endif
               }
               catch (Exception ex)
               {
                  Messager.ShowError(this, "ConfigureLogging Failed");
                  throw ex;
               }
            }

            private void ConfigureLoggingMachineConfig(string productName, string connectionStringName, DefaultConnectionNameType connectionType)
            {
               try
               {
                  LoggingDataAccessConfigurationView loggingSectionView;

                  loggingSectionView = new LoggingDataAccessConfigurationView();
                  loggingSectionView.Configuration = null;
                  ConfigureSection(ConfigMachine, loggingSectionView, connectionStringName, productName, connectionType);
#if LTV17_CONFIG
                  AddLoggingChannel ( config ) ;
#endif
               }
               catch (Exception ex)
               {
                  Messager.ShowError(this, "ConfigureLogging Failed");
                  throw ex;
               }
            }
            

            private void ConfigureStorage( string productName, string connectionStringName, DefaultConnectionNameType connectionType)
            {
               try
               {
                  StorageDataAccessConfigurationView sectionView = new StorageDataAccessConfigurationView(ConfigGlobalPacs, DicomDemoSettingsManager.ProductNameDemoServer, null);
                  sectionView.ConfigurationSource = ConfigSource;
                  sectionView.Configuration = ConfigGlobalPacs;
                  ConfigureSection(ConfigGlobalPacs, sectionView, connectionStringName, productName, connectionType);                  
               }
               catch (Exception ex)
               {
                  Messager.ShowError(this, "ConfigureStorage Failed");
                  throw ex;
               }
            }

            private void ConfigureUserManagement(string productName, string connectionStringName, DefaultConnectionNameType connectionType)
            {
               try
               {
                  UserManagementDataAccessConfigurationView sectionView = new UserManagementDataAccessConfigurationView(ConfigGlobalPacs, DicomDemoSettingsManager.ProductNameDemoServer, null);
                  sectionView.ConfigurationSource = ConfigSource;
                  sectionView.Configuration = ConfigGlobalPacs;
                  ConfigureSection(ConfigGlobalPacs, sectionView, connectionStringName, productName, connectionType);
               }
               catch (Exception ex)
               {
                  Messager.ShowError(this, "ConfigureUserManagement Failed");
                  throw ex;
               }
            }

            private void ConfigureOptions(string productName, string connectionStringName, DefaultConnectionNameType connectionType)
            {
               try
               {
                  OptionsDataAccessConfigurationView sectionView = new OptionsDataAccessConfigurationView(ConfigGlobalPacs, DicomDemoSettingsManager.ProductNameDemoServer, null);
                  sectionView.ConfigurationSource = ConfigSource;
                  sectionView.Configuration = ConfigGlobalPacs;
                  ConfigureSection(ConfigGlobalPacs, sectionView, connectionStringName, productName, connectionType);
               }
               catch (Exception ex)
               {
                  Messager.ShowError(this, "ConfigureOptions Failed");
                  throw ex;
               }
            }

            private void ConfigureAeManagement(string productName, string connectionStringName, DefaultConnectionNameType connectionType)
            {
               try
               {
                  AeManagementDataAccessConfigurationView sectionView = new AeManagementDataAccessConfigurationView(ConfigGlobalPacs, DicomDemoSettingsManager.ProductNameDemoServer, null);
                  sectionView.ConfigurationSource = ConfigSource;
                  sectionView.Configuration = ConfigGlobalPacs;
                  ConfigureSection(ConfigGlobalPacs, sectionView, connectionStringName, productName, connectionType);
               }
               catch (Exception ex)
               {
                  Messager.ShowError(this, "ConfigureAeManagement Failed");
                  throw ex;
               }
            }

            private void ConfigureAePermissionManagement(string productName, string connectionStringName, DefaultConnectionNameType connectionType)
            {
               try
               {
                  AePermissionManagementDataAccessConfigurationView sectionView = new AePermissionManagementDataAccessConfigurationView(ConfigGlobalPacs, DicomDemoSettingsManager.ProductNameDemoServer, null);
                  sectionView.ConfigurationSource = ConfigSource;
                  sectionView.Configuration = ConfigGlobalPacs;
                  ConfigureSection(ConfigGlobalPacs, sectionView, connectionStringName, productName, connectionType);
               }
               catch (Exception ex)
               {
                  Messager.ShowError(this, "ConfigureAePermissionManagement Failed");
                  throw ex;
               }
            }


            private void ConfigurePermissionManagement(string productName, string connectionStringName, DefaultConnectionNameType connectionType)
            {
               try
               {
                  PermissionManagementDataAccessConfigurationView sectionView = new PermissionManagementDataAccessConfigurationView(ConfigGlobalPacs, DicomDemoSettingsManager.ProductNameDemoServer, null);
                  sectionView.ConfigurationSource = ConfigSource;
                  sectionView.Configuration = ConfigGlobalPacs;
                  ConfigureSection(ConfigGlobalPacs, sectionView, connectionStringName, productName, connectionType);
               }
               catch (Exception ex)
               {
                  Messager.ShowError(this, "ConfigurePermissionManagement Failed");
                  throw ex;
               }
            }


            private void ConfigureForward(string productName, string connectionStringName, DefaultConnectionNameType connectionType)
            {
               try
               {
                  ForwardDataAccessConfigurationView sectionView = new ForwardDataAccessConfigurationView(ConfigGlobalPacs, DicomDemoSettingsManager.ProductNameDemoServer, null);
                  sectionView.ConfigurationSource = ConfigSource;
                  sectionView.Configuration = ConfigGlobalPacs;
                  ConfigureSection(ConfigGlobalPacs, sectionView, connectionStringName, productName, connectionType);
               }
               catch (Exception ex)
               {
                  Messager.ShowError(this, "ConfigureForward Failed");
                  throw ex;
               }
            }

#if (LEADTOOLS_V19_OR_LATER_MEDICAL_EXTERNAL_STORE) || (LEADTOOLS_V19_OR_LATER)
            private void ConfigureExternalStore(string productName, string connectionStringName, DefaultConnectionNameType connectionType)
            {
               try
               {
                  ExternalStoreDataAccessConfigurationView sectionView = new ExternalStoreDataAccessConfigurationView(ConfigGlobalPacs, DicomDemoSettingsManager.ProductNameDemoServer, null);
                  sectionView.ConfigurationSource = ConfigSource;
                  sectionView.Configuration = ConfigGlobalPacs;
                  ConfigureSection(ConfigGlobalPacs, sectionView, connectionStringName, productName, connectionType);
               }
               catch (Exception ex)
               {
                  Messager.ShowError(this, "ConfigureExternalStore Failed");
                  throw ex;
               }
            }
            
#endif // #if (LEADTOOLS_V19_OR_LATER_MEDICAL_EXTERNAL_STORE) || (LEADTOOLS_V19_OR_LATER)         
            private void ConfigureWorklist(string productName, string connectionStringName, DefaultConnectionNameType connectionType)
            {
               try
               {
                  WorklistDataAccessConfigurationView sectionView = new WorklistDataAccessConfigurationView(ConfigGlobalPacs, DicomDemoSettingsManager.ProductNameDemoServer, null);
                  sectionView.ConfigurationSource = ConfigSource;
                  sectionView.Configuration = null;
#if LEADTOOLS_V18_OR_LATER
                  ConfigureSection(ConfigGlobalPacs, sectionView, connectionStringName, productName, connectionType);
#else
                  ConfigureSection(ConfigMachine, sectionView, connectionStringName, productName, connectionType);
#endif // #if LEADTOOLS_V18_OR_LATER
               }
               catch (Exception ex)
               {
                  Messager.ShowError(this, "ConfigureWorklist Failed");
                  throw ex;
               }
            }

            // Workstation only DLLs
            private void ConfigureMediaCreation(string productName, string connectionStringName, DefaultConnectionNameType connectionType)
            {
               try
               {
                  MediaCreationDataAccessConfigurationView sectionView = new MediaCreationDataAccessConfigurationView(/*ConfigGlobalPacs, DicomDemoSettingsManager.ProductNameDemoServer, null*/);
                  sectionView.ConfigurationSource = ConfigSource;
                  sectionView.Configuration = null;
#if LEADTOOLS_V18_OR_LATER
                  ConfigureSection(ConfigGlobalPacs, sectionView, connectionStringName, productName, connectionType);
#else
                  ConfigureSection(ConfigMachine, sectionView, connectionStringName, productName, connectionType);
#endif // #if LEADTOOLS_V18_OR_LATER
               }
               catch (Exception ex)
               {
                  Messager.ShowError(this, "ConfigureMediaCreation Failed");
                  throw ex;
               }
            }

            private void ConfigureWorkstation(string productName, string connectionStringName, DefaultConnectionNameType connectionType)
            {
               try
               {
                  WorkstationDataAccessConfigurationView sectionView = new WorkstationDataAccessConfigurationView(/*ConfigGlobalPacs, DicomDemoSettingsManager.ProductNameDemoServer, null*/);
                  sectionView.ConfigurationSource = ConfigSource;
                  sectionView.Configuration = null;
#if LEADTOOLS_V18_OR_LATER
                  ConfigureSection(ConfigGlobalPacs, sectionView, connectionStringName, productName, connectionType);
#else
                  ConfigureSection(ConfigMachine, sectionView, connectionStringName, productName, connectionType);
#endif // #if LEADTOOLS_V18_OR_LATER
               }
               catch (Exception ex)
               {
                  Messager.ShowError(this, "ConfigureWorkstation Failed");
                  throw ex;
               }
            }

#if (LEADTOOLS_V20_OR_LATER)
      private void ConfigurePatientRightsDataAccess(string productName, string connectionStringName, DefaultConnectionNameType connectionType)
      {
         PatientRightsDataAccessConfigurationView accessRightsConfigurationView = new PatientRightsDataAccessConfigurationView(ConfigGlobalPacs, productName, null);

         accessRightsConfigurationView.Configuration = ConfigGlobalPacs;
         ConfigureSection(ConfigGlobalPacs, accessRightsConfigurationView, connectionStringName, productName, connectionType);
      }

      private void ConfigureExportLayoutDataAccess(string productName, string connectionStringName, DefaultConnectionNameType connectionType)
      {
         ExportLayoutDataAccessConfigurationView accessConfigurationView = new ExportLayoutDataAccessConfigurationView(ConfigGlobalPacs, productName, null);

         accessConfigurationView.Configuration = ConfigGlobalPacs;
         ConfigureSection(ConfigGlobalPacs, accessConfigurationView, connectionStringName, productName, connectionType);
      }
#endif


      private void RegisterConfigSections ( )
            {               
               if (databaseOptions1.StorageServerDbSelected)
               {
                  string connectionStringName = __StorageServerConnectionString.Name;
                  
                  ConfigureLogging                 ( DicomDemoSettingsManager.ProductNameStorageServer, connectionStringName, databaseOptions1.LoggingDbSelected ? DefaultConnectionNameType.None : DefaultConnectionNameType.UseMachineConfig);
                  ConfigureStorage                 ( DicomDemoSettingsManager.ProductNameStorageServer, connectionStringName, databaseOptions1.StorageDbSelected ? DefaultConnectionNameType.None : DefaultConnectionNameType.UseMachineConfig);
                  ConfigureUserManagement          ( DicomDemoSettingsManager.ProductNameStorageServer, connectionStringName, databaseOptions1.WorkstationDbSelected ? DefaultConnectionNameType.None: DefaultConnectionNameType.UseMachineConfig);
                  ConfigureAeManagement            ( DicomDemoSettingsManager.ProductNameStorageServer, connectionStringName, DefaultConnectionNameType.None);
                  ConfigureAePermissionManagement  ( DicomDemoSettingsManager.ProductNameStorageServer, connectionStringName, DefaultConnectionNameType.None);
                  ConfigurePermissionManagement    ( DicomDemoSettingsManager.ProductNameStorageServer, connectionStringName, DefaultConnectionNameType.None);
                  ConfigureForward                 ( DicomDemoSettingsManager.ProductNameStorageServer, connectionStringName, DefaultConnectionNameType.None);
#if (LEADTOOLS_V19_OR_LATER_MEDICAL_EXTERNAL_STORE) || (LEADTOOLS_V19_OR_LATER)
                  ConfigureExternalStore           ( DicomDemoSettingsManager.ProductNameStorageServer, connectionStringName, DefaultConnectionNameType.None);
#endif

#if (LEADTOOLS_V20_OR_LATER)
            ConfigurePatientRightsDataAccess(DicomDemoSettingsManager.ProductNameStorageServer, connectionStringName, DefaultConnectionNameType.None);
            ConfigureExportLayoutDataAccess(DicomDemoSettingsManager.ProductNameStorageServer, connectionStringName, DefaultConnectionNameType.None);
#endif
            if (databaseOptions1.StorageServerOptionsDbSelected)
                  {
                     // For Load Balancing
                     // In this case, the server options database is separate from the StorageServerDatabase -- it is specified by the StorageServerOptionsConnectionString
                     ConfigureOptions              ( DicomDemoSettingsManager.ProductNameStorageServer, __StorageServerOptionsConnectionString.Name, DefaultConnectionNameType.None);
                  }
                  else
                  {
                     // No Load Balancing
                     // The server options database is part of the StorageServerDatabase
                     ConfigureOptions              ( DicomDemoSettingsManager.ProductNameStorageServer, connectionStringName, DefaultConnectionNameType.None);
                  }
                  
               }

               if (databaseOptions1.PatientUpdaterDbSelected)
               {
                  ConfigureUserManagement          ( DicomDemoSettingsManager.ProductNamePatientUpdater, __PatientUpdaterConnectionString.Name, DefaultConnectionNameType.None);
                  ConfigureOptions                 ( DicomDemoSettingsManager.ProductNamePatientUpdater, __PatientUpdaterConnectionString.Name, DefaultConnectionNameType.None);
                  ConfigurePermissionManagement    ( DicomDemoSettingsManager.ProductNamePatientUpdater, __PatientUpdaterConnectionString.Name, DefaultConnectionNameType.None);
               }

               string productName = null;
#if LEADTOOLS_V18_OR_LATER
               productName = DicomDemoSettingsManager.ProductNameWorkstation;
#endif // #if LEADTOOLS_V18_OR_LATER

               if (databaseOptions1.LoggingDbSelected)
               {
                  ConfigureLogging(productName, __LoggingConnectionString.Name, DefaultConnectionNameType.UseCurrent);
#if !LEADTOOLS_V18_OR_LATER
                  ConfigureLoggingMachineConfig(null, __LoggingConnectionString.Name, DefaultConnectionNameType.UseCurrent);
#endif
               }

               if (databaseOptions1.StorageDbSelected)
               {
                  ConfigureStorage( productName, __StorageConnectionString.Name, DefaultConnectionNameType.UseCurrent);
               }

               if (databaseOptions1.WorklistDbSelected)
               {
                  ConfigureWorklist(DicomDemoSettingsManager.ProductNameStorageServer, __WorklistConnectionString.Name, DefaultConnectionNameType.UseCurrent);
               }

               if (databaseOptions1.MediaCreationDbSelected)
               {
                  ConfigureMediaCreation(productName, __MediaCreationConnectionString.Name, DefaultConnectionNameType.UseCurrent);
               }

               if (databaseOptions1.WorkstationDbSelected)
               {
                  ConfigureWorkstation(productName, __WorkstationConnectionString.Name, DefaultConnectionNameType.UseCurrent);
               }

               if (databaseOptions1.UserManagementDbSelected)
               {
                  ConfigureUserManagement(productName, __UserAccessConnectionString.Name, DefaultConnectionNameType.UseCurrent);
               }

#if !LEADTOOLS_V18_OR_LATER
               try
               {
                  ConfigMachine.Save(ConfigurationSaveMode.Modified);
               }
               catch (Exception ex)
               {
                  Messager.ShowError(this, "ConfigMachine.Save Failed");
                  throw ex;
               }
#endif // #if !LEADTOOLS_V18_OR_LATER

               try
               {
                  ConfigGlobalPacs.Save(ConfigurationSaveMode.Modified);
               }
               catch (Exception ex)
               {
                  Messager.ShowError(this, "ConfigGlobalPacs.Save Failed");
                  throw ex;
               }
            }

            private void AddLoggingChannel ( Configuration config ) 
            {
               ConfigSectionHandler section ;
                
               string LoggingSectionName = "logging";
 
               try
               {
                  section = config.GetSection(LoggingSectionName) as ConfigSectionHandler;
               }
               catch ( Exception ) 
               {
                  config.Sections.Remove(LoggingSectionName);

                  section = null ;
               }


               if ( section == null )
               {
                  ChannelElement element ;
                  Type databaseLoggingChannelType ;
                  
                  section = new ConfigSectionHandler ( ) ;
                  element = new ChannelElement ( ) ;
                  
                  databaseLoggingChannelType = typeof ( DataAccessLoggingChannel ) ;
                  
                  element.Name = "DataAccessLoggingChannel" ;
                  element.Type = databaseLoggingChannelType ;
                  
                  section.Channels.Add ( element ) ;

                  config.Sections.Add(LoggingSectionName, section);
               }
               else
               {
                  Type databaseLoggingChannelType ;
                  bool   channelFound ;
                  
                  databaseLoggingChannelType = typeof ( DataAccessLoggingChannel ) ;
                  
                  channelFound = false ;
                  
                  foreach ( ChannelElement channel in section.Channels ) 
                  {
                     if ( channel.Type == databaseLoggingChannelType )
                     {
                        channelFound = true ;
                        
                        break ;
                     }
                  }
                  
                  if ( !channelFound ) 
                  {
                     ChannelElement element ;
                     
                     element = new ChannelElement ( ) ;
                     
                     element.Name = "DataAccessLoggingChannel" ;
                     element.Type = databaseLoggingChannelType ;
                     
                     section.Channels.Add ( element ) ;
                  }
               }
            }

            private void AddProduct (DataAccessSettings dataAccessSettings, string productName, string connectionName)
            {
               ConnectionElement connectionElement = null;
               if (dataAccessSettings != null)
               {
                  int count = dataAccessSettings.Connections.Count;
                  for (int i = 0; i < count; i++)
                  {
                     if (dataAccessSettings.Connections[i].ProductName == productName)
                     {
                        connectionElement = dataAccessSettings.Connections[i];
                        connectionElement.ConnectionName = connectionName;
                        break;
                     }
                  }
                  if (connectionElement == null)
                  {
                     connectionElement = new ConnectionElement();
                     connectionElement.ProductName = productName;
                     connectionElement.ConnectionName = connectionName;
                     dataAccessSettings.Connections.Add(connectionElement);
                  }
               }
            }

            //private void ConfigureSection
            //(
            //   Configuration config,
            //   DataAccessConfigurationView sectionView,
            //   string connectionName
            //)
            //{
            //   ConfigureSection(config, sectionView, connectionName, string.Empty);
            //}
            
            enum DefaultConnectionNameType
            {
               None = 0, 
               UseMachineConfig = 1,
               UseCurrent = 2,
            }

            private void ConfigureSection
            (
               Configuration config, 
               DataAccessConfigurationView sectionView,
               string connectionName,
               string productName, 
               DefaultConnectionNameType defaultType
            )
            {
               DataAccessSettings dataAccessSettings;


               ConfigurationManager.RefreshSection(sectionView.DataAccessSettingsSectionName);

               try
               {
                  dataAccessSettings = config.Sections.OfType<DataAccessSettings>().FirstOrDefault(n => n.SectionInformation.Name == sectionView.DataAccessSettingsSectionName);
               }
               catch (Exception)
               {
                  config.Sections.Remove(sectionView.DataAccessSettingsSectionName);
                  dataAccessSettings = null;
               }

               bool newSection = false;
               if (null == dataAccessSettings)
               {
                  dataAccessSettings = new DataAccessSettings();
                  newSection = true;
               }

               string defaultConnectionName = string.Empty;
               switch(defaultType)
               {
                  case DefaultConnectionNameType.None:
                     // do nothing
                     break;
                  
                  case DefaultConnectionNameType.UseCurrent:
                     dataAccessSettings.ConnectionName = connectionName;
                     break;

                  case DefaultConnectionNameType.UseMachineConfig:
                     {
                        ConnectionStringSettings css = GetConnectionString(ConfigMachine, sectionView.DataAccessSettingsSectionName);
                        if ((css == null) || string.IsNullOrEmpty(css.Name))
                        {
                           dataAccessSettings.ConnectionName = connectionName;
                        }
                        else
                        {
                           dataAccessSettings.ConnectionName = css.Name;
                           AddConnectionString(ConfigGlobalPacs.ConnectionStrings, css);
                        }
                     }
                     break;
               }

               if (!string.IsNullOrEmpty(productName))
               {
                  AddProduct(dataAccessSettings, productName, connectionName);
               }

               if (newSection)
                  config.Sections.Add(sectionView.DataAccessSettingsSectionName, dataAccessSettings);
            }
            
            private void CreateStorageOptionsDatabase()
            {
               if (databaseOptions1.StorageServerDbSelected && databaseOptions1.StorageServerOptionsDbSelected && databaseOptions1.OptionsDatabaseMode == DbConfigurationMode.Create)
               {
                  if (__StorageServerOptionsConnectionString.ProviderName == ConnectionProviders.SqlServerProvider.Name)
                  {
                     DicomStorageServerOptionsSqlInstaller.InstallDatabase(__StorageServerOptionsConnectionString.ConnectionString);
                  }
                  else
                  {
                     DicomStorageServerOptionsSqlCeInstaller.InstallDatabase(__StorageServerOptionsConnectionString.ConnectionString);
                  }
               }
            }

            private void CreateSelectedDatabases ( )
            {
               if ( databaseOptions1.StorageServerDbSelected ) 
               {
                  if ( __StorageServerConnectionString.ProviderName == ConnectionProviders.SqlServerProvider.Name )
                  {
                     DicomStorageServerSqlInstaller.InstallDatabase ( __StorageServerConnectionString.ConnectionString ) ;
                  }
                  else
                  {
                     DicomStorageServerSqlCeInstaller.InstallDatabase ( __StorageServerConnectionString.ConnectionString ) ;
                  }
               }

               // Patient updater only connects to existing databases -- it does not create new databases
               //if (databaseOptions1.PatientUpdaterDbSelected)
               //{
               //   if (__PatientUpdaterConnectionString.ProviderName == ConnectionProviders.SqlServerProvider.Name)
               //   {
               //      DicomStorageServerSqlInstaller.InstallDatabase(__PatientUpdaterConnectionString.ConnectionString);
               //   }
               //   else
               //   {
               //      DicomStorageServerSqlCeInstaller.InstallDatabase(__PatientUpdaterConnectionString.ConnectionString);
               //   }
               //}
               
               if ( databaseOptions1.LoggingDbSelected ) 
               {
                  if ( __LoggingConnectionString.ProviderName == ConnectionProviders.SqlServerProvider.Name )
                  {
                     DicomLoggingSqlInstaller.InstallDatabase ( __LoggingConnectionString.ConnectionString ) ;
                  }
                  else
                  {
                     DicomLoggingSqlCeInstaller.InstallDatabase ( __LoggingConnectionString.ConnectionString ) ;
                  }
               }
               
               if ( databaseOptions1.StorageDbSelected ) 
               {
                  if ( __StorageConnectionString.ProviderName == ConnectionProviders.SqlServerProvider.Name )
                  {
                     DicomStorageSqlInstaller.InstallDatabase ( __StorageConnectionString.ConnectionString ) ;
                  }
                  else
                  {
                     DicomStorageSqlCeInstaller.InstallDatabase ( __StorageConnectionString.ConnectionString ) ;
                  }
               }
               
               if ( databaseOptions1.WorklistDbSelected )
               {
                  if ( __WorklistConnectionString.ProviderName == ConnectionProviders.SqlServerProvider.Name ) 
                  {
                     WorklistSqlInstaller.InstallDatabase ( __WorklistConnectionString.ConnectionString ) ;
                  }
                  else
                  {
                     WorklistSqlCeInstaller.InstallDatabase ( __WorklistConnectionString.ConnectionString ) ;
                  }
               }
               
               if ( databaseOptions1.MediaCreationDbSelected )
               {
                  if ( __MediaCreationConnectionString.ProviderName == ConnectionProviders.SqlServerProvider.Name ) 
                  {
                     MediaCreationSqlInstaller.InstallDatabase ( __MediaCreationConnectionString.ConnectionString ) ;
                  }
                  else
                  {
                     MediaCreationSqlCeInstaller.InstallDatabase ( __MediaCreationConnectionString.ConnectionString ) ;
                  }
               }
               
               if ( databaseOptions1.WorkstationDbSelected )
               {
                  if ( __WorkstationConnectionString.ProviderName == ConnectionProviders.SqlServerProvider.Name ) 
                  {
                     WorkstationSqlInstaller.InstallDatabase ( __WorkstationConnectionString.ConnectionString ) ;
                  }
                  else
                  {
                     WorkstationSqlCeInstaller.InstallDatabase ( __WorkstationConnectionString.ConnectionString ) ;
                  }
               }
               
               if ( databaseOptions1.UserManagementDbSelected )
               {
                  if ( __UserAccessConnectionString.ProviderName == ConnectionProviders.SqlServerProvider.Name ) 
                  {
                     WorkstationUsersManagementSqlInstaller.InstallDatabase ( __UserAccessConnectionString.ConnectionString ) ;
                  }
                  else
                  {
                     WorkstationUsersManagementSqlCeInstaller.InstallDatabase ( __UserAccessConnectionString.ConnectionString ) ;
                  }
               }
            }

            private void InsertWorkstationUser()
            {
               try
               {
                  UserManagementDataAccessConfigurationView UsersConfigView;
                  IUserManagementDataAccessAgent accessAgent;
                  SecureString userPassword;

                  UsersConfigView = new UserManagementDataAccessConfigurationView(ConfigGlobalPacs, DicomDemoSettingsManager.ProductNameWorkstation, null);

                  ConfigurationManager.RefreshSection("connectionStrings");
                  ConfigurationManager.RefreshSection(UsersConfigView.DataAccessSettingsSectionName);

                  UsersConfigView.ConfigurationSource = GetConfigurationSource();

                  accessAgent = DataAccessFactory.GetInstance(UsersConfigView).CreateDataAccessAgent<IUserManagementDataAccessAgent>();
                  userPassword = GetSecureString(databaseOptions1.WorkstationUserPassword);

                  accessAgent.AddUser(databaseOptions1.WorkstationUserName, userPassword, true);
               }
               catch (Exception exception)
               {
                  Messager.ShowError(this, "InsertWorkstationUser Failed: " + exception.Message);
                  throw exception;
               }

            }

            private void InsertStorageServerUser()
            {
               try
               {
                  UserManagementDataAccessConfigurationView UsersConfigView = new UserManagementDataAccessConfigurationView(ConfigGlobalPacs, DicomDemoSettingsManager.ProductNameStorageServer, null);

                  ConfigurationManager.RefreshSection("connectionStrings");
                  ConfigurationManager.RefreshSection(UsersConfigView.DataAccessSettingsSectionName);

                  UsersConfigView.ConfigurationSource = GetConfigurationSource();
                  UsersConfigView.Configuration = ConfigGlobalPacs;

                  IUserManagementDataAccessAgent accessAgent = DataAccessFactory.GetInstance(UsersConfigView).CreateDataAccessAgent<IUserManagementDataAccessAgent>();
                  SecureString userPassword = GetSecureString(databaseOptions1.WorkstationUserPassword);
                  accessAgent.AddUser(databaseOptions1.WorkstationUserName, userPassword, true);

                  PermissionManagementDataAccessConfigurationView OptionsConfigView = new PermissionManagementDataAccessConfigurationView(ConfigGlobalPacs, DicomDemoSettingsManager.ProductNameStorageServer, null);
                  IPermissionManagementDataAccessAgent optionsAgent = DataAccessFactory.GetInstance(OptionsConfigView).CreateDataAccessAgent<IPermissionManagementDataAccessAgent>();

                  foreach (string permission in Program.storageServerDefaultPermissionsList)
                  {
                     optionsAgent.AddUserPermission(permission, databaseOptions1.WorkstationUserName);
                  }
               }
               catch (Exception exception)
               {
                  Messager.ShowError(this, "InsertStorageServerUser Failed: " + exception.Message);
                  throw exception;
               }
            }

            public class StoreClientSessionProxy : ICStoreClientSessionProxy
            {
               private string affectedSopInstance;
               private string abstractClass;
               private string _serverName;

               public DicomCommandStatusType LastStatus;
               public string LastStatusDescriptionMessage;

               #region ICStoreClientSessionProxy Members

               public string AffectedSOPInstance
               {
                  get
                  {
                     return affectedSopInstance;
                  }
                  set
                  {
                     affectedSopInstance = value;
                  }
               }

               public void SendCStoreResponse(DicomCommandStatusType status, string descriptionMessage)
               {
                  LastStatus = status;
                  LastStatusDescriptionMessage = descriptionMessage;
               }

               #endregion

               #region IDICOMCommandClientSessionProxy Members

               public string AbstractClass
               {
                  get { return abstractClass; }
                  set
                  {
                     abstractClass = value;
                  }
               }

               public int MessageID
               {
                  get { return 0; }
               }

               public byte PresentationID
               {
                  get { return 0; }
               }

               #endregion

               #region IClientSessionProxy Members

               public string ClientName
               {
                  get { return System.Threading.Thread.CurrentPrincipal.Identity.Name; }
               }

               public bool IsAssociated
               {
                  get { return true; }
               }

               public bool IsConnected
               {
                  get { return true; }
               }

               public void LogEvent
               (
                  LogType enumType,
                  MessageDirection enumMessageDirection,
                  string strDescription,
                  DicomCommandType command,
                  DicomDataSet DatasetRootElement,
                  SerializableDictionary<string, object> customInfo
               )
               {

               }

               public string ServerName
               {
                  get { return _serverName; }
                  set { _serverName = value; }
               }

               #endregion
            }

            private void AddDefaultIodClasses()
            {
               try
               {
                  PresentationContextList pcList = new PresentationContextList();
                  pcList.Default();
                  
                  // There should be 104 items in the table (as of 2011)
                  if (pcList.Items.Length < 100)
                  {
                     Messager.ShowError(this, "Failed to read IOD Classes from Leadtools.Dicom.Tables.dll");
                  }

                  OptionsDataAccessConfigurationView optionsConfigView = new OptionsDataAccessConfigurationView(DicomDemoSettingsManager.GetGlobalPacsConfiguration(), DicomDemoSettingsManager.ProductNameStorageServer, null);
                  IOptionsDataAccessAgent optionsAgent = DataAccessFactory.GetInstance(optionsConfigView).CreateDataAccessAgent<IOptionsDataAccessAgent>();
                  optionsAgent.Set<PresentationContextList>(typeof(PresentationContextList).Name, pcList, new Type[0]);
               }
               catch (Exception exception)
               {
                  Messager.ShowError(this, "AddDefaultIodClasses Failed: " + exception.Message);
                  throw exception;
               }

            }


            public static SecureString GetSecureString(string password)
            {
               char [ ] passwordChars ;
               SecureString secure ;
               
               
               passwordChars = password.ToCharArray ( ) ;
               secure        = new SecureString ( ) ;
               
               foreach ( char c in passwordChars )
               {
                  secure.AppendChar ( c ) ;
               }
               
               secure.MakeReadOnly ( ) ;
               
               return secure ;
            }

            private void FillModalityWorklistDummyData ( )
            {
               WorklistDataAccessConfigurationView worklistConfigView ;
               IWorklistDataAccessAgent AccessAgent ;
               MWLDataset worklistDataSet ;
               Assembly   executingAssembly   = null ;
               Stream     datasetDataStream   = null ; 
               
               
               worklistConfigView = new WorklistDataAccessConfigurationView ( ) ;
               
               ConfigurationManager.RefreshSection ( "connectionStrings" ) ;
               ConfigurationManager.RefreshSection ( worklistConfigView.DataAccessSettingsSectionName ) ;
               
               AccessAgent       = DataAccessFactory.GetInstance ( worklistConfigView ).CreateDataAccessAgent <IWorklistDataAccessAgent> ( ) ;
               executingAssembly = Assembly.GetExecutingAssembly ( ) ;
               datasetDataStream = executingAssembly.GetManifestResourceStream ( Constants.MWLDatasetData ) ;
               worklistDataSet   = new MWLDataset ( ) ;
               
               worklistDataSet.ReadXml ( datasetDataStream ) ;
               
               AccessAgent.UpdateMWL ( worklistDataSet ) ;
               
               datasetDataStream.Close ( ) ;
               
               worklistDataSet.Dispose ( ) ;
            }
            
            private string GetDatabaseConfigHeaderMessage 
            ( 
               DatabaseComponents databaseComponents, 
               DbConfigurationMode mode 
            )
            {
               switch ( databaseComponents )
               {
                 case DatabaseComponents.StorageServer:
                    {
                       return string.Format("{0} Storage Server Database", mode.ToString());
                    }

                 case DatabaseComponents.StorageServerOptions:
                    {
                       return string.Format("{0} Storage Server Options Database", mode.ToString());
                    }

                 case DatabaseComponents.PatientUpdater:
                    {
                       return string.Format("{0} Patient Updater Database", mode.ToString());
                    }
                    
                  case DatabaseComponents.Logging:
                  {
                     return string.Format ( "{0} Logging Database", mode.ToString ( ) ) ;
                  }
                  
                  case DatabaseComponents.Storage:
                  {
                     return string.Format ( "{0} Storage Database", mode.ToString ( ) ) ;
                  }
                  
                  case DatabaseComponents.Worklist:
                  {
                     return string.Format ( "{0} WorkList Database", mode.ToString ( ) ) ;
                  }
                  
                  case DatabaseComponents.MediaCreation:
                  {
                     return string.Format ( "{0} Media Creation Database", mode.ToString ( ) ) ;
                  }
                  
                  case DatabaseComponents.Workstation:
                  {
                     return string.Format ( "{0} Workstation Database", mode.ToString ( ) ) ;
                  }
                  
                  case DatabaseComponents.UserManagement:
                  {
                     return string.Format ( "{0} User Management Database", mode.ToString ( ) ) ;
                  }
               }
               
               return "" ;
            }
            
            [DllImport("shfolder.dll", CharSet = CharSet.Auto)]
            private static extern int SHGetFolderPath(IntPtr hwndOwner, int nFolder, IntPtr hToken, int dwFlags, StringBuilder lpszPath);

            private const int CommonDocumentsFolder = 0x2e;

            private static string GetFolderPath ( )
            {
               StringBuilder lpszPath ;
               string        path ;
               
               lpszPath = new StringBuilder ( 260 ) ;
               
               // CommonDocuments is the folder than any Vista user (including 'guest') has read/write access
               SHGetFolderPath ( IntPtr.Zero, (int)CommonDocumentsFolder, IntPtr.Zero, 0, lpszPath ) ;
               
               path = lpszPath.ToString ( ) ;
               
               new FileIOPermission(FileIOPermissionAccess.PathDiscovery, path).Demand ( ) ;
               
               return path ;
            }
            
            private static bool Is64Process ( ) 
            {
               return IntPtr.Size == 8 ;
            }
            
            private static bool VerifyAlreadyExistGlobalPacsConfig()
            {
               string pacsConfigFileName = DicomDemoSettingsManager.GlobalPacsConfigFullFileName;
               bool alreadyExist = File.Exists(pacsConfigFileName);
               if (!alreadyExist)
               {
                  StreamWriter sw = File.CreateText(pacsConfigFileName);
                  sw.Write(Resources.GlobalPacs);
                  sw.Close();
                  ;
               }
               return alreadyExist;
            }

         #endregion
         
         #region Properties
         
            private Control __PreviousControl 
            {
               get
               {
                  return _previousControl ;
               }
               
               set
               {
                  _previousControl = value ;
               }
            }
            
            private Control __CurrentControl 
            {
               get
               {
                  return _currentControl ;
               }
               
               set
               {
                  _currentControl = value ;
               }
            }
            
            private Control __NextControl 
            {
               get
               {
                  return _nextControl ;
               }
               
               set
               {
                  _nextControl = value ;
               }
            }
            
            private ConnectionStringSettings __StorageServerConnectionString
            {
               get
               {
                  return _storageServerConnectionString;
               }

               set
               {
                  _storageServerConnectionString = value;
               }
            }
            
            private ConnectionStringSettings __MedicalWorkstationConnectionString
            {
               get
               {
                  return _medicalWorkstationConnectionString;
               }

               set
               {
                  _medicalWorkstationConnectionString = value;
               }
            }

            private ConnectionStringSettings __StorageServerOptionsConnectionString
            {
               get
               {
                  return _storageServerOptionsConnectionString;
               }

               set
               {
                  _storageServerOptionsConnectionString = value;
               }
            }

            private ConnectionStringSettings __PatientUpdaterConnectionString
            {
               get
               {
                  return _patientUpdaterConnectionString;
               }

               set
               {
                  _patientUpdaterConnectionString = value;
               }
            }
            
            private ConnectionStringSettings __LoggingConnectionString
            {
               get
               {
                  return _loggingConnectionString ;
               }
               
               set
               {
                  _loggingConnectionString = value ;
               }
            }
            
            private ConnectionStringSettings __StorageConnectionString
            {
               get
               {
                  return _storageConnectionString ;
               }
               
               set
               {
                  _storageConnectionString = value ;
               }
            }
            
            private ConnectionStringSettings __WorklistConnectionString
            {
               get
               {
                  return _worklistConnectionString ;
               }
               
               set
               {
                  _worklistConnectionString = value ;
               }
            }
            
            
            private ConnectionStringSettings __MediaCreationConnectionString
            {
               get
               {
                  return _mediaCreationConnectionString ;
               }
               
               set
               {
                  _mediaCreationConnectionString = value ;
               }
            }
            
            private ConnectionStringSettings __WorkstationConnectionString
            {
               get
               {
                  return _workstationConnectionString ;
               }
               
               set
               {
                  _workstationConnectionString = value ;
               }
            }
            
            private ConnectionStringSettings __UserAccessConnectionString
            {
               get
               {
                  return _userAccessConnectionString ;
               }
               
               set
               {
                  _userAccessConnectionString = value ;
               }
            }

            private ConnectionStringSettings __PatientManagementConnectionString
            {
               get
               {
                  return _patientManagementConnectionString;
               }

               set
               {
                  _patientManagementConnectionString = value;
               }
            }
            
            private DisplayComponentConfigurationCommand __ActiveComponentCommand
            {
               get
               {
                  return _activeComponentCommand ;
               }
               
               set
               {
                  _activeComponentCommand = value ;
               }
            }
            
            private List <DisplayComponentConfigurationCommand> __DisplayDbConfigCommandList 
            {
               get
               {
                  return _displayDbConfigCommandList ;
               }
               
               set
               {
                  _displayDbConfigCommandList = value ;
               }
            }
            
         #endregion
         
         #region Events
         
            void databaseOptions1_ValueChanged ( object sender, EventArgs e )
            {
               ButtonNext.Enabled = ( databaseOptions1.StorageServerDbSelected ||
                                      //databaseOptions1.StorageServerOptionsDbSelected ||
                                      databaseOptions1.PatientUpdaterDbSelected ||
                                      databaseOptions1.LoggingDbSelected || 
                                      databaseOptions1.StorageDbSelected || 
                                      databaseOptions1.WorklistDbSelected || 
                                      databaseOptions1.MediaCreationDbSelected || 
                                      databaseOptions1.WorkstationDbSelected ||
                                      databaseOptions1.UserManagementDbSelected ) ;
            }
            
            void ButtonNext_Click ( object sender, EventArgs e )
            {
               try
               {
                  ButtonBack.Enabled = true ;
                  
                  if ( __CurrentControl == databaseOptions1 ) 
                  {
                     if ( databaseOptions1.UserManagementDbSelected || databaseOptions1.StorageServerDbSelected) 
                     {
                        if ( string.IsNullOrEmpty ( databaseOptions1.WorkstationUserName ) ||
                             string.IsNullOrEmpty ( databaseOptions1.WorkstationUserPassword ) )
                        {
                           ButtonBack.Enabled = false ;
                           
                           Messager.ShowWarning ( this, "Enter a valid user name and password." ) ;
                           
                           return ;
                        }
                        
                        if (string.Compare(databaseOptions1.WorkstationUserPassword, databaseOptions1.WorkstationUserPasswordConfirm) != 0)
                        {
                           ButtonBack.Enabled = false ;
                           
                           Messager.ShowWarning ( this, "'Password' and 'Confirm Password' do not match." ) ;
                           
                           return ;
                        }
                     }
                     
                     foreach ( DisplayComponentConfigurationCommand command in __DisplayDbConfigCommandList )
                     {
                        if ( ExecuteCommand ( command ) )
                        {
                           break ;
                        }
                     }
                  }
                  else if ( __CurrentControl == connectionConfiguration1 ) 
                  {
                     string connectionErrorMessage ;
                     int    currentCommandIndex ;
                     bool   commandExecuted ;
                     
                     if ( null == __ActiveComponentCommand ) 
                     {
                        return ;
                     }

                     if (!connectionConfiguration1.IsConnectionStringValid(out connectionErrorMessage, _databaseLocations))
                     {
                        if ( !string.IsNullOrEmpty ( connectionErrorMessage ) )
                        {
                           Messager.ShowError ( this, connectionErrorMessage ) ;
                        }
                        
                        return ;
                     }


                     _databaseLocations.Add(connectionConfiguration1.ConnectionString);


                     
                     commandExecuted     = false ;
                     currentCommandIndex = __DisplayDbConfigCommandList.IndexOf ( __ActiveComponentCommand ) ;
                     
                     SetConnectionStringSettings ( __ActiveComponentCommand.Database ) ;
                     
                     for ( int commandIndex = currentCommandIndex + 1; commandIndex < __DisplayDbConfigCommandList.Count; commandIndex++ )
                     {
                        DisplayComponentConfigurationCommand command ;
                        
                        
                        command = __DisplayDbConfigCommandList [ commandIndex ] ;

                        if ( ExecuteCommand ( command ) )
                        {
                           commandExecuted = true ;
                           
                           break ;
                        }
                     }
                     
                     if ( !commandExecuted ) 
                     {
                        string summary ;
                        
                        
                        summary = BuildConfigurationSummary ( ) ;
                        
                        connectionSummary1.Summary = summary ;
                        
                        __ActiveComponentCommand = null ;
                        
                        SetCurrentControl ( connectionSummary1 ) ;
                     }
                  }
                  else if ( __CurrentControl == connectionSummary1 ) 
                  {
                     ButtonNext.Enabled = false ;
                     ButtonBack.Enabled = false ;
                     ButtonCancel.Enabled = false ;
                     
                     if ( databaseOptions1.Mode == DbConfigurationMode.Create ) 
                     {
                        CreateSelectedDatabases ( ) ;
                     }
                     else
                     {
                        // The configuration mode is DbConfigurationMode.Configure
                        // Connect to existing database
                        // If connection to a StorageServer database using Load Balancing, then need to create a server options database
                        
                        CreateStorageOptionsDatabase();
                     }
                     
                     if (ConfigGlobalPacs != null)
                     {
                        GlobalPacsUpdater.BackupFile(ConfigGlobalPacs.FilePath);
                     }
                                          
                     StoreConnectionStrings ( ) ;
                     
                     RegisterConfigSections ( ) ;
                     
                     if ( databaseOptions1.Mode == DbConfigurationMode.Create ) 
                     {
                        if ( databaseOptions1.WorklistDbSelected ) 
                        {
                           // FillModalityWorklistDummyData ( ) ;
                        }
                        
                        if ( databaseOptions1.UserManagementDbSelected  ) 
                        {
                           InsertWorkstationUser ( ) ;
                        }
                        
                        if ( databaseOptions1.StorageServerDbSelected)
                        {
                           InsertStorageServerUser();

                           if (Program.IsToolkitDemo == true)
                           {
                              Program.AddDefaultImages(ConfigGlobalPacs);
                           }
                           AddDefaultIodClasses();
                        }
                     }
                     
                     ButtonCancel.Text    = "Close" ;
                     ButtonCancel.Enabled = true ;
                     
                     Messager.ShowInformation ( this, "Database configuration completed successfully." ) ;
                  }
               }
               catch ( Exception exception ) 
               {
                  Messager.ShowError ( this, exception ) ;
                  
                  ButtonNext.Enabled   = true ;
                  ButtonBack.Enabled   = true ;
                  ButtonCancel.Enabled = true ;
               }
            }

            void ButtonBack_Click(object sender, EventArgs e)
            {
               try
               {
                  if ( __CurrentControl == databaseOptions1 ) 
                  {
                    return ;
                  }
                  else if ( __CurrentControl == connectionConfiguration1 ) 
                  {
                     int currentCommandIndex ;
                     bool commandExecuted ;
                     
                     if ( null == __ActiveComponentCommand ) 
                     {
                        return ;
                     }
                     
                     commandExecuted     = false ;
                     currentCommandIndex = __DisplayDbConfigCommandList.IndexOf ( __ActiveComponentCommand ) ;
                     
                     SetConnectionStringSettings ( __ActiveComponentCommand.Database ) ;
                     
                     for ( int commandIndex = currentCommandIndex - 1; commandIndex >= 0; commandIndex-- )
                     {
                        DisplayComponentConfigurationCommand command ;
                        
                        
                        command = __DisplayDbConfigCommandList [ commandIndex ] ;

                        if ( ExecuteCommand ( command ) )
                        {
                           commandExecuted = true ;
                           
                           break ;
                        }
                     }
                     
                     if ( !commandExecuted ) 
                     {
                        SetCurrentControl ( databaseOptions1 ) ;
                        
                        __ActiveComponentCommand = null ;
                     }
                  }
                  else
                  {
                     if ( __CurrentControl == connectionSummary1 ) 
                     {
                        bool commandExecuted ;
                        
                        
                        commandExecuted = false ;
                        
                        for ( int commandIndex = __DisplayDbConfigCommandList.Count -1; commandIndex >= 0; commandIndex-- )
                        {
                           DisplayComponentConfigurationCommand command ;
                           
                           
                           command = __DisplayDbConfigCommandList [ commandIndex ] ;

                           if ( ExecuteCommand ( command ) )
                           {
                              commandExecuted = true ;
                              
                              break ;
                           }
                        }
                        
                        if ( !commandExecuted )
                        {
                           SetCurrentControl ( databaseOptions1 ) ;
                        }
                     }
                  }
               }
               finally
               {
                  if ( __CurrentControl == databaseOptions1 ) 
                  {
                     ButtonBack.Enabled = false ;
                  }
                  if (_databaseLocations.Count >= 1)
                  {
                     _databaseLocations.RemoveAt(_databaseLocations.Count - 1);
                  }
               }
            }
            
            void command_Executed ( object sender, EventArgs e )
            {
               __ActiveComponentCommand = sender as DisplayComponentConfigurationCommand ;
               
               __CurrentControl = __ActiveComponentCommand.ConfigControl ;
               
               HeaderDescriptionLabel.Text = GetDatabaseConfigHeaderMessage ( __ActiveComponentCommand.Database, 
                                                                              __ActiveComponentCommand.ConfigControl.Mode ) ;
            }

            void command_Executing(object sender, EventArgs e)
            {
               DisplayComponentConfigurationCommand command ;
               
               
               command = sender as DisplayComponentConfigurationCommand ;
               
               __CurrentControl.Visible = false ;
               
               command.ConfigControl.Mode = databaseOptions1.Mode ;
               
               // For storage server options, there is only an option to create a new database.
               if (command.Database == DatabaseComponents.StorageServerOptions)
                  command.ConfigControl.Mode = databaseOptions1.OptionsDatabaseMode;
               
               InitConnectionConfigurationControl ( command.Database, command.ConfigControl ) ;
            }
            
            private void ButtonCancel_Click ( object sender, EventArgs e )
            {
               // Application.Exit ( ) ;
               Environment.Exit(0);
            }

         #endregion
         
         #region Data Members
         
            private Control _previousControl ;
            private Control _currentControl ;
            private Control _nextControl ;
            private ConnectionStringSettings _storageServerConnectionString ;
            private ConnectionStringSettings _medicalWorkstationConnectionString;
            private ConnectionStringSettings _storageServerOptionsConnectionString;
            private ConnectionStringSettings _patientUpdaterConnectionString;
            private ConnectionStringSettings _loggingConnectionString ;
            private ConnectionStringSettings _storageConnectionString ;
            private ConnectionStringSettings _worklistConnectionString ;
            private ConnectionStringSettings _mediaCreationConnectionString ;
            private ConnectionStringSettings _workstationConnectionString ;
            private ConnectionStringSettings _userAccessConnectionString;
            private ConnectionStringSettings _patientManagementConnectionString;
            private DisplayComponentConfigurationCommand _activeComponentCommand;
            private List <DisplayComponentConfigurationCommand> _displayDbConfigCommandList ;

            
         #endregion
         
         #region Data Types Definition
         
            private abstract class Constants
            {
               public const string MWLDatasetData = "CSPacsDatabaseConfigurationDemo.Common.MWLDatasetDummyTestData.xml" ;
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
   
   public enum DbConfigurationMode 
   {
      Create,
      Configure
   }
}
