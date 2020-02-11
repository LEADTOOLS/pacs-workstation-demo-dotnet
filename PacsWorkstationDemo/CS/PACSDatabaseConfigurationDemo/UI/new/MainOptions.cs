// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using Leadtools.Medical.DataAccessLayer.Configuration;
using Leadtools.Medical.Logging.DataAccessLayer.Configuration;
using Leadtools.Medical.Storage.DataAccessLayer.Configuration;
using Leadtools.Medical.Worklist.DataAccessLayer.Configuration;
using Leadtools.Medical.Media.DataAccessLayer.Configuration;
using Leadtools.Medical.Workstation.DataAccessLayer.Configuration;
using Leadtools.Medical.UserManagementDataAccessLayer.Configuration;
using Leadtools.DicomDemos;
using MedicalWorkstationConfigurationDemo;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using MedicalWorkstationConfigurationDemo.UI;
using Leadtools.Demos;
using Leadtools.Demos.Sql;
using System.Data.SqlClient;
using Leadtools.Medical.OptionsDataAccessLayer.Configuration;
using System.Text.RegularExpressions;

namespace CSPacsDatabaseConfigurationDemo.UI.New
{
   public partial class MainOptions : UserControl
   {
      public MainOptions()
      {
         InitializeComponent();
         RegisterEvents ( ) ;
         
         rbOptionsDefault.Checked = true;
         
         UpdateUI();

         this.lblStorgeServer.AutoSize = true;
         this.lblStorgeServer.AutoSize = true;
         this.lblMedicalWorkstation.AutoSize = true;
         this.lblWorklist.AutoSize = true;
         this.labelCreateOptions.AutoSize = true;
         this.labelConnectOptions.AutoSize = true;

         this.pnlStorageServer.Resize += new System.EventHandler(this.pnlStorageServer_Resize);
      }

      public void InitDefaults()
      {
         StorageServerDbCheckBox.CheckState = CheckState.Checked;
         MedicalWorkstationDbCheckBox.CheckState = CheckState.Checked;
         WorklistDbCheckBox.CheckState = CheckState.Checked;

         bool showStorageServer = _supportedDatabases.IsFlagged(DatabaseComponents.StorageServer);
         panelStorage.Enabled = showStorageServer;
         StorageServerDbCheckBox.Checked = showStorageServer;

         bool showWorklist = _supportedDatabases.IsFlagged(DatabaseComponents.Worklist);
         panelWorklist.Enabled = showWorklist;
         WorklistDbCheckBox.Checked = showWorklist;

         bool showWorkstation = _supportedDatabases.IsFlagged(DatabaseComponents.MedicalWorkstation);
         panelWorkstation.Enabled = showWorkstation;
         MedicalWorkstationDbCheckBox.Checked = showWorkstation;

         if (DatabaseOptions.DisableCreateFromConfiguration())
         {
            CreateNewDbRadioButton.Enabled = false;
            ConnectToDbRadioButton.Checked = true;
         }
      }

      private DatabaseComponents _supportedDatabases = DatabaseComponents.StorageServer | DatabaseComponents.Worklist | DatabaseComponents.MedicalWorkstation;
      public DatabaseComponents SupportedDatabases
      {
         get
         {
            return _supportedDatabases;
         }
         set
         {
            _supportedDatabases = value;
         }
      }
      
      public string LabelCreateNote
      {
         get 
         {
            return lblCreateNote.Text;
         }
         
         set
         {
            lblCreateNote.Text = value;
         }
      }
      
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
      
      public DbConfigurationMode Mode 
      {
         get
         {
            return CreateNewDbRadioButton.Checked ? DbConfigurationMode.Create : DbConfigurationMode.Configure ;
         }
      }
      
      public bool StorageServerOptionsCreateSelected
      {
         get
         {
            return rbOptionsCreate.Checked;
         }
      }
      
      public bool StorageServerOptionsConnectSelected
      {
         get
         {
            return rbOptionsConnect.Checked;
         }
      }
         
      public void Init( Configuration configMachine, Configuration configGlobalPacs )
      {
         ConfigMachine    = configMachine ;
         ConfigGlobalPacs = configGlobalPacs ;
         string processor = ( ( Is64Process ( ) ) ? "_64" : "_32" ) ;
         
         btnConfigStorageServer.Tag = _storageServerConnectionString       = new ConfigurationOptions ( ) { ConfigurationLabel = lblStorgeServer,        DbComponent = DatabaseComponents.StorageServer,       ConfigurationSource = ConfigGlobalPacs,   DataAccessName = new StorageDataAccessConfigurationView().DataAccessSettingsSectionName,        ProductName = DicomDemoSettingsManager.ProductNameStorageServer,  DefaultDatabaseName = ConfigurationData.DefaultStorageServerDbName + processor,        FriendlyName = "LEAD Storage Server Database"  } ;
         btnMedicalWorkstation.Tag  = _medicalWorkstationConnectionString  = new ConfigurationOptions ( ) { ConfigurationLabel = lblMedicalWorkstation,  DbComponent = DatabaseComponents.MedicalWorkstation,  ConfigurationSource = ConfigGlobalPacs,   DataAccessName = new StorageDataAccessConfigurationView().DataAccessSettingsSectionName,        ProductName = DicomDemoSettingsManager.ProductNameWorkstation,    DefaultDatabaseName = ConfigurationData.DefaultMedicalWorkstationDbName + processor,   FriendlyName = "Medical Workstation Database"  } ;
         btnWorklist.Tag            = _worklistConnectionString            = new ConfigurationOptions ( ) { ConfigurationLabel = lblWorklist,            DbComponent = DatabaseComponents.Worklist,            ConfigurationSource = ConfigMachine,      DataAccessName = new WorklistDataAccessConfigurationView().DataAccessSettingsSectionName,       ProductName = null,                                               DefaultDatabaseName = ConfigurationData.DefaultWorklistDbName + processor,             FriendlyName = "DICOM Worklist Database" } ;
         //btnLogging.Tag             = _loggingConnectionString             = new ConfigurationOptions ( ) { ConfigurationLabel = lblLogging,             DbComponent = DatabaseComponents.Logging,             ConfigurationSource = ConfigMachine,      DataAccessName = new LoggingDataAccessConfigurationView().DataAccessSettingsSectionName,        ProductName = null,                                               DefaultDatabaseName = ConfigurationData.DefaultLoggingDbName + processor,              FriendlyName = "Logging Database"  } ;
         //btnDicomStorage.Tag        = _dicomStorageConnectionString        = new ConfigurationOptions ( ) { ConfigurationLabel = lblDicomStorage,        DbComponent = DatabaseComponents.Storage,             ConfigurationSource = ConfigMachine,      DataAccessName = new StorageDataAccessConfigurationView().DataAccessSettingsSectionName,        ProductName = null,                                               DefaultDatabaseName = ConfigurationData.DefaultStorageDbName + processor,              FriendlyName = "DICOM Storage Server" } ;
         //btnMediaCreation.Tag       = _mediaCreationConnectionString       = new ConfigurationOptions ( ) { ConfigurationLabel = lblMediaCreation,       DbComponent = DatabaseComponents.MediaCreation,       ConfigurationSource = ConfigMachine,      DataAccessName = new MediaCreationDataAccessConfigurationView().DataAccessSettingsSectionName,  ProductName = null,                                               DefaultDatabaseName = ConfigurationData.DefaultMediaCreationDbName + processor,        FriendlyName = "Media Creation Database"  } ;
         //btnWorkstationDb.Tag       = _workstationConnectionString         = new ConfigurationOptions ( ) { ConfigurationLabel = lblWorkstationDb,       DbComponent = DatabaseComponents.Workstation,         ConfigurationSource = ConfigMachine,      DataAccessName = new WorkstationDataAccessConfigurationView().DataAccessSettingsSectionName,    ProductName = null,                                               DefaultDatabaseName = ConfigurationData.DefaultWorkstationDbName + processor,          FriendlyName = "Workstation Database"  } ;
         //btnWorkstationUser.Tag     = _userAccessConnectionString          = new ConfigurationOptions ( ) { ConfigurationLabel = lblWorkstationUser,     DbComponent = DatabaseComponents.UserManagement,      ConfigurationSource = ConfigMachine,      DataAccessName = new UserManagementDataAccessConfigurationView().DataAccessSettingsSectionName, ProductName = null,                                               DefaultDatabaseName = ConfigurationData.DefaultUserAccessDbName + processor,           FriendlyName = "Workstation User Management Database"  } ;

         buttonConnectOptions.Tag = _storageServerConnectOptionsConnectionString    = new ConfigurationOptions ( ) { ConfigurationLabel = labelConnectOptions,     DbComponent = DatabaseComponents.StorageServerOptions,       ConfigurationSource = ConfigGlobalPacs,   DataAccessName = new OptionsDataAccessConfigurationView().DataAccessSettingsSectionName,        ProductName = DicomDemoSettingsManager.ProductNameStorageServer,  DefaultDatabaseName = ConfigurationData.DefaultStorageServerOptionsDbName + processor,        FriendlyName = "LEAD Storage Server Options Database"  } ;
         buttonCreateOptions.Tag  = _storageServerCreateOptionsConnectionString    = new ConfigurationOptions ( ) { ConfigurationLabel = labelCreateOptions,       DbComponent = DatabaseComponents.StorageServerOptions,       ConfigurationSource = ConfigGlobalPacs,   DataAccessName = new OptionsDataAccessConfigurationView().DataAccessSettingsSectionName,        ProductName = DicomDemoSettingsManager.ProductNameStorageServer,  DefaultDatabaseName = ConfigurationData.DefaultStorageServerOptionsDbName + processor,        FriendlyName = "LEAD Storage Server Options Database"  } ;

         StorageServerDbCheckBox.Tag = pnlStorageServer;
         WorklistDbCheckBox.Tag = pnlWorklist;
         MedicalWorkstationDbCheckBox.Tag = pnlMedicalWorkstation;

         //LoggingDbCheckBox.Tag = pnlLogging ;
         //StorageDbCheckBox.Tag = pnlDicomStorage ;
         //MediaCreationDbCheckBox.Tag = pnlMediaCreation ;
         //WorkstationDbCheckBox.Tag = pnlWorkstationDb ;
         //UserManagementDbCheckBox.Tag = pnlWorkstationUser ;
         
         string userName = Environment.UserName ;
         WorkstationUserNameTextBox.Text     = userName ;
         WorkstationUserPasswordTextBox.Text = userName.ToLower ( ) ;
         
         lblCreateNote.Text = string.Format ( lblCreateNote.Text, GetFolderPath ( ) ) ;
      }

      private void RegisterEvents()
      {
         btnChangeDefaultSqlServer.Click += new EventHandler(btnChangeDefaultSqlServer_Click);
         btnConfigStorageServer.Click += new EventHandler(OnConfigureConnectionStringSettings);
         btnMedicalWorkstation.Click  += new EventHandler(OnConfigureConnectionStringSettings);
         btnWorklist.Click            += new EventHandler(OnConfigureConnectionStringSettings);

         StorageServerDbCheckBox.CheckedChanged  += new EventHandler(OnDatabaseOptionsChcekedChanged);
         MedicalWorkstationDbCheckBox.CheckedChanged += new EventHandler(OnDatabaseOptionsChcekedChanged);
         WorklistDbCheckBox.CheckedChanged       += new EventHandler(OnDatabaseOptionsChcekedChanged);

         CreateNewDbRadioButton.CheckedChanged += new EventHandler ( CreateNewDbRadioButton_CheckedChanged ) ;
         rbOptionsConnect.CheckedChanged += new EventHandler(rbOptions_CheckedChanged);
         rbOptionsCreate.CheckedChanged += new EventHandler(rbOptions_CheckedChanged);
         rbOptionsConnect.CheckedChanged += new EventHandler(rbOptions_CheckedChanged);

         buttonCreateOptions.Click += new EventHandler(OnConfigureConnectionStringSettings); //new EventHandler(buttonCreateOptions_Click);
         buttonConnectOptions.Click += new EventHandler(OnConfigureConnectionStringSettings); // new EventHandler(buttonConnectOptions_Click);
      }

      void buttonConnectOptions_Click(object sender, EventArgs e)
      {
         MessageBox.Show("ConnectOptions");
      }

      void buttonCreateOptions_Click(object sender, EventArgs e)
      {
         MessageBox.Show("CreateOptions");
      }

      void rbOptions_CheckedChanged(object sender, EventArgs e)
      {
         UpdateUI();
      }
      

      public void SetDefaultSqlServerLabel()
      {
         string configurationLabelText = "{0}";
         string labelText = string.Empty;
         string dataSource = "SQL Server Compact";
         if (DefaultSqlConnectionStringBuilder == null)
         {
            textBoxDefaultSqlServer.Text = "SQL Server Compact";
            dataSource = "SQL Server Compact";

            labelText = string.Format(configurationLabelText, dataSource);
         }
         else
         {
            textBoxDefaultSqlServer.Text = DefaultSqlConnectionStringBuilder.DataSource;
            dataSource = DefaultSqlConnectionStringBuilder.DataSource;

            labelText = string.Format(configurationLabelText, DefaultSqlConnectionStringBuilder.ConnectionString);
         }

         UpdateDatabaseLabels();
      }


      void btnChangeDefaultSqlServer_Click(object sender, EventArgs e)
      {
         ChooseDefaultSqlServerDialog dlg = new ChooseDefaultSqlServerDialog(){Icon = Program.GetAppIcon()};
         dlg.VerifyConnectionString = true;
         DialogResult dr = dlg.ShowDialog();
         if (dr == DialogResult.OK)
         {
            if (dlg.SqlDataSourceType == SqlDataSourceEnum.SqlServer)
            {
               string connectionString = dlg.GetConnectionString("master");
               string connectionErrorMessage;

               if (SqlUtilities.TestSQLConnectionString(connectionString, out connectionErrorMessage))
               {
                  DefaultSqlConnectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
                  SetDefaultSqlServerLabel();
               }
               else
               {
                  Messager.ShowWarning(this, connectionErrorMessage);
               }
            }
            else // SqlDataSourceEnum.SqlServerCompact
            {
               DefaultSqlConnectionStringBuilder = null;
               SetDefaultSqlServerLabel();
            }
         }
      }

      public void ShowChangeDefaultSqlServerButton(bool show)
      {
         if (show)
            btnChangeDefaultSqlServer.Show();
         else
            btnChangeDefaultSqlServer.Hide();
      }

      private ConnectionStringSettings GetConnectionConfiguration ( ConfigurationOptions configuration ) 
      {
         if ( configuration.ConnectionSettings != null && !string.IsNullOrEmpty ( configuration.ConnectionSettings.ConnectionString ) )
         {
            return configuration.ConnectionSettings ;
         }
         
         bool is64 ;
         
         
         is64 = Is64Process ( ) ;
         
         ConnectionStringSettings connectionSettings ;
         
         if ( CreateNewDbRadioButton.Checked )
         {
            connectionSettings = new ConnectionStringSettings ( ) ;
         }
         else
         {
            connectionSettings = GetConnectionString ( configuration.ConfigurationSource, configuration.DataAccessName, configuration.ProductName ) ;
         }
         
         connectionSettings.Name =  configuration.DefaultDatabaseName ; 
         
         return connectionSettings ;
      }

      private static bool Is64Process ( ) 
      {
         return IntPtr.Size == 8 ;
      }
      
      private ConnectionStringSettings GetConnectionString
      (
         Configuration config,
         string dataAccessSectionName,
         string productName
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
               string connectionName = settings.ConnectionName ;
               
               if ( !string.IsNullOrEmpty ( productName ) && settings.Connections.Count > 0 )
               {
                  for (int i = 0; i < settings.Connections.Count; i++)
                  {
                     if (settings.Connections[i].ProductName == productName)
                     {
                        connectionName = settings.Connections[i].ConnectionName ;
                        
                        break;
                     }
                  }
               }
               
               ConnectionStringSettings connection ;
            
            
               connection = config.ConnectionStrings.ConnectionStrings [ connectionName ] ;
            
               if ( null == connection ) 
               {
                  return new ConnectionStringSettings ( ) ;
               }
               else
               {
                  return new ConnectionStringSettings ( connection.Name, connection.ConnectionString, connection.ProviderName ) ;
               }
            }
         }
         catch ( Exception )
         {
            return new ConnectionStringSettings ( ) ;
         }
      }

      private static Configuration GetConfiguration ( ) 
      {

         return ConfigurationManager.OpenMachineConfiguration ( ) ;
      }

      public static void ChooseDefaultSqlServer()
      {
         using (ConnectionConfigurationDlg config = new ConnectionConfigurationDlg())
         {
         }
      }

      public static string GetLocalSqlDefaultConnectionString(SqlConnectionStringBuilder dataConnection, string database)
      {
         string connectionString;

         if (dataConnection == null)
            return string.Empty;

         dataConnection.InitialCatalog = database;
         connectionString = dataConnection.ConnectionString;

         return connectionString;
      }

      void OnConfigureConnectionStringSettings(object sender, EventArgs notUsed)
      {
         DbConfigurationMode configMode = (CreateNewDbRadioButton.Checked) ? DbConfigurationMode.Create : DbConfigurationMode.Configure ;
         
         Button senderButton = (Button) sender ;
         if (senderButton == buttonCreateOptions)
         {
            configMode = DbConfigurationMode.Create;
         }
         
         
         using ( ConnectionConfigurationDlg config = new ConnectionConfigurationDlg ( ) )
         {
            ConfigurationOptions configuration = (ConfigurationOptions) senderButton.Tag ;
            ConnectionStringSettings connectionSettings ;
            
            connectionSettings = GetConnectionConfiguration ( configuration ) ;
            
            config.connectionConfigurationControl.DefaultSqlCeDatabaseName = Path.ChangeExtension ( Path.Combine ( GetFolderPath ( ), configuration.DefaultDatabaseName ), "sdf" ) ;
            config.connectionConfigurationControl.DefaultSqlServerDatabaseName = configuration.DefaultDatabaseName ;
            config.Title = GetDatabaseConfigHeaderMessage ( configuration, configMode ) ;

            if ( !string.IsNullOrEmpty ( connectionSettings.ConnectionString ) )
            {
               config.connectionConfigurationControl.SetConnectionString ( connectionSettings.ConnectionString, connectionSettings.ProviderName ) ;
            }
            else
            {
               // config.connectionConfigurationControl.ClearConnectionString ( ) ;
               string connectionString;
               if (DefaultSqlConnectionStringBuilder == null)
               {
                  connectionString = GetSqlCEDatabaseConnection(configuration.DefaultDatabaseName);
                  config.connectionConfigurationControl.SetConnectionString(connectionString, ConnectionProviders.SqlCeProvider.Name);
               }
               else
               {
                  connectionString = GetLocalSqlDefaultConnectionString(DefaultSqlConnectionStringBuilder, configuration.DefaultDatabaseName);
                  config.connectionConfigurationControl.SetConnectionString(connectionString, ConnectionProviders.SqlServerProvider.Name);
               }
            }
            
            config.connectionConfigurationControl.Mode = configMode ;
            
            if ( DialogResult.OK == config.ShowDialog ( this ) )
            {
               connectionSettings.ConnectionString = config.connectionConfigurationControl.ConnectionString ;
               connectionSettings.ProviderName = config.connectionConfigurationControl.DataProvider ;
               configuration.ConnectionSettings = connectionSettings ;
               string configText = config.connectionConfigurationControl.ConnectionString.MaskPassword();
               SetLabelText(configuration.ConfigurationLabel, configText);
            }
         }
      }
      
      void OnDatabaseOptionsChcekedChanged (object sender, EventArgs e)
      {
         CheckBox chkSender = (CheckBox) sender ;
         Panel panel = (Panel)chkSender.Tag ;
         
         panel.Enabled = chkSender.Checked ;
         
         if (chkSender == StorageServerDbCheckBox && ConnectToDbRadioButton.Checked)
         {
            UpdateUI();
         }

      }
      
      void UpdateUI()
      {
         panelCreateConnectDatabase.Visible = true;
         panelCreateConnectDatabase.Enabled = true;
         
         panelCreateWarning.Visible = CreateNewDbRadioButton.Checked;
         panelCreateWarning.Enabled = true;
         
         panelDatabases.Visible = true;
         panelDatabases.Enabled = true;
                  
         pnlStorageServer.Enabled = StorageServerDbCheckBox.Checked;
         pnlMedicalWorkstation.Enabled = MedicalWorkstationDbCheckBox.Checked;
         pnlWorklist.Enabled = WorklistDbCheckBox.Checked;
         
         grpUserCred.Visible = CreateNewDbRadioButton.Checked ;
         
         
         panelStorageOptions.Visible = !CreateNewDbRadioButton.Checked && ConfigurationData.ShowStorageServerDatabaseOptionsFromConfiguration();

         // Options Database
         rbOptionsDefault.Enabled = StorageServerDbCheckBox.Checked;
         rbOptionsCreate.Enabled = StorageServerDbCheckBox.Checked;
         rbOptionsConnect.Enabled = StorageServerDbCheckBox.Checked;
         
         panelCreateOptions.Enabled = rbOptionsCreate.Enabled && rbOptionsCreate.Checked;
         panelConnectOptions.Enabled = rbOptionsConnect.Enabled && rbOptionsConnect.Checked;
      }

      void UpdateDatabaseLabels()
      {
         string storageServerConnectionString = MyGetConnectionString(_storageServerConnectionString, true);
         string medicalWorkstationConnectionString = MyGetConnectionString(_medicalWorkstationConnectionString, true);
         string worklistConnectionString = MyGetConnectionString(_worklistConnectionString, true);

         SetLabelText(_storageServerConnectionString.ConfigurationLabel, storageServerConnectionString);
         SetLabelText(_medicalWorkstationConnectionString.ConfigurationLabel, medicalWorkstationConnectionString);
         SetLabelText(_worklistConnectionString.ConfigurationLabel, worklistConnectionString);
      }

      void ChangeConnectionsAndUpdateDatabaseLabels(bool createNew)
      {

         if (createNew)
         {
            NewConnections();
         }
         else
         {
            ExistingConnections();
         }

         UpdateDatabaseLabels();

         UpdateUI();
      }
      
      void CreateNewDbRadioButton_CheckedChanged(object sender, EventArgs e)
      {
         ChangeConnectionsAndUpdateDatabaseLabels(CreateNewDbRadioButton.Checked);
      }
      
      public void SetLabelText ( Label warningLabel, string text )
      {
         if (string.IsNullOrEmpty(text))
         {
            warningLabel.Text = "Not Configured";
            warningLabel.ForeColor = Color.Red;
         }
         else
         {
            warningLabel.Text = text;
            warningLabel.ForeColor = Color.Green;
         }
      }

      ConnectionStringSettings _storageNew = null;
      ConnectionStringSettings _medicalWorkstationNew = null;
      ConnectionStringSettings _worklistNew = null;

      ConnectionStringSettings _storageExisting = null;
      ConnectionStringSettings _medicalWorkstationExisting = null;
      ConnectionStringSettings _worklistExisting = null;

      private void NewConnections ( )
      {
         _storageExisting = _storageServerConnectionString.ConnectionSettings;
         _storageServerConnectionString.ConnectionSettings = _storageNew;

         _medicalWorkstationExisting = _medicalWorkstationConnectionString.ConnectionSettings;
         _medicalWorkstationConnectionString.ConnectionSettings = _medicalWorkstationNew;

         _worklistExisting = _worklistConnectionString.ConnectionSettings;
         _worklistConnectionString.ConnectionSettings = _worklistNew;
      }

      private void ExistingConnections()
      {
         _storageNew = _storageServerConnectionString.ConnectionSettings;
         if (_storageExisting == null)
         {
            // If connecting to an exsting StorageServer database, see if app.config stores a "StorageServerConnectionString" connection string 
            string storageServerConnectionString = ConfigurationData.StorageServerConnectionStringFromConfiguration();
            if (!string.IsNullOrEmpty(storageServerConnectionString))
            {
               string providerName = "System.Data.SqlClient";
               _storageExisting = new ConnectionStringSettings("LeadStorageServer20_32", storageServerConnectionString , providerName);
            }
         }
         _storageServerConnectionString.ConnectionSettings = _storageExisting;


         _medicalWorkstationNew = _medicalWorkstationConnectionString.ConnectionSettings;
         _medicalWorkstationConnectionString.ConnectionSettings = _medicalWorkstationExisting;

         _worklistNew = _worklistConnectionString.ConnectionSettings;
         _worklistConnectionString.ConnectionSettings = _worklistExisting;
      }

      private string GetDatabaseConfigHeaderMessage 
      ( 
         ConfigurationOptions configOption,
         DbConfigurationMode mode 
      )
      {
         return string.Format("{0} {1}", mode.ToString(), configOption.FriendlyName);
      }

      private ConfigurationOptions _storageServerConnectionString ;
      private ConfigurationOptions _worklistConnectionString ;
      private ConfigurationOptions _medicalWorkstationConnectionString;
      
      private ConfigurationOptions _storageServerCreateOptionsConnectionString;
      private ConfigurationOptions _storageServerConnectOptionsConnectionString;
      //private ConfigurationOptions _loggingConnectionString ;
      //private ConfigurationOptions _dicomStorageConnectionString ;
      //private ConfigurationOptions _mediaCreationConnectionString ;
      //private ConfigurationOptions _workstationConnectionString ;
      //private ConfigurationOptions _userAccessConnectionString;
      
      [DllImport("shfolder.dll", CharSet = CharSet.Auto)]
      private static extern int SHGetFolderPath(IntPtr hwndOwner, int nFolder, IntPtr hToken, int dwFlags, StringBuilder lpszPath);

      private const int CommonDocumentsFolder = 0x2e;

      public static string GetFolderPath ( )
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

      internal bool ValidateConnections()
      {
         bool valid = true ;
         if ( !CreateNewDbRadioButton.Checked )
         {
            string missingConfiguration = "" ;
            
            // LEAD Storage Server
            if ( StorageServerDbCheckBox.Checked && !IsConnectionValid ( _storageServerConnectionString ) )
            {
               missingConfiguration += _storageServerConnectionString.FriendlyName +  "\n\r" ;
               valid = false ;
            }
            
            // LEAD Storage Server Options
            if ( rbOptionsCreate.Checked && !IsConnectionValid ( _storageServerCreateOptionsConnectionString ) )
            {
               missingConfiguration += _storageServerCreateOptionsConnectionString.FriendlyName +  "\n\r" ;
               valid = false ;
            }
            else if ( rbOptionsConnect.Checked && !IsConnectionValid ( _storageServerConnectOptionsConnectionString ) )
            {
               missingConfiguration += _storageServerConnectOptionsConnectionString.FriendlyName +  "\n\r" ;
               valid = false ;
            }
            
            // Medical Workstation
            if ( MedicalWorkstationDbCheckBox.Checked && !IsConnectionValid ( _medicalWorkstationConnectionString ) )
            {
               missingConfiguration += _medicalWorkstationConnectionString.FriendlyName + "\n\r" ;
               valid = false ;
            }            
            
            
            // DICOM Worklist Database
            if ( WorklistDbCheckBox.Checked && !IsConnectionValid ( _worklistConnectionString ) )
            {
               missingConfiguration += _worklistConnectionString.FriendlyName + "\n\r" ;
               valid = false ;
            }
            //if ( LoggingDbCheckBox.Checked && !IsConnectionValid ( _loggingConnectionString ) )
            //{
            //   missingConfiguration += _loggingConnectionString.FriendlyName + "\n\r" ;
            //   valid = false ;
            //}
            //if ( StorageDbCheckBox.Checked && !IsConnectionValid ( _dicomStorageConnectionString ) )
            //{
            //   missingConfiguration += _dicomStorageConnectionString.FriendlyName + "\n\r" ;
            //   valid = false ;
            //}

            //if ( MediaCreationDbCheckBox.Checked && !IsConnectionValid ( _mediaCreationConnectionString ) )
            //{
            //   missingConfiguration += _mediaCreationConnectionString.FriendlyName + "\n\r" ;
            //   valid = false ;
            //}
            //if ( WorkstationDbCheckBox.Checked && !IsConnectionValid ( _workstationConnectionString ) )
            //{
            //   missingConfiguration += _workstationConnectionString.FriendlyName + "\n\r" ;
            //   valid = false ;
            //}
            
            //if ( UserManagementDbCheckBox.Checked && !IsConnectionValid ( _userAccessConnectionString ) )
            //{
            //   missingConfiguration += _userAccessConnectionString.FriendlyName + "\n\r" ;
            //   valid = false ;
            //}
            

            
            if ( !valid ) 
            {
               Messager.ShowWarning ( this, "The following database(s) are not configured. Either click on the 'Change' button to set the database information or uncheck the related option.\n\r\n\r" + missingConfiguration ) ;
            }
         }
         else
         {
            if ( string.IsNullOrEmpty ( WorkstationUserNameTextBox.Text ) )
            {
               Messager.ShowWarning ( this, "User name can't be empty" ) ;
               valid = false ;
            }
            
            if ( string.IsNullOrEmpty ( WorkstationUserPasswordTextBox.Text ) )
            {
               Messager.ShowWarning ( this, "Password can't be empty" ) ;
               valid = false ;
            }
         }
         
         return valid ;
      }
      
      bool IsConnectionValid ( ConfigurationOptions configuration ) 
      {
         return null != configuration && null != configuration.ConnectionSettings && !string.IsNullOrEmpty ( configuration.ConnectionSettings.ConnectionString ) ;
      }
      
      private SqlConnectionStringBuilder _defaultSqlConnectionStringBuilder = new SqlConnectionStringBuilder();
      public SqlConnectionStringBuilder DefaultSqlConnectionStringBuilder
      {
         get
         {
            return _defaultSqlConnectionStringBuilder;
         }
         set
         {
            _defaultSqlConnectionStringBuilder = value;
         }
      }

      internal string MyGetConnectionString(ConfigurationOptions configurationOptions, bool mask)
      {
         EnsureDefaultOnCreate(configurationOptions);
         if (configurationOptions.ConnectionSettings == null)
            return null;
         string connectionString = configurationOptions.ConnectionSettings.ConnectionString;
         return mask ? connectionString.MaskPassword() : connectionString;
      }

      internal List <ConfigurationOptions> GetConfigurationOptions()
      {
         List <ConfigurationOptions> configOptions = new List<ConfigurationOptions> ( ) ;

         // LEAD Storage Server
         if ( StorageServerDbCheckBox.Checked )
         {
            EnsureDefaultOnCreate ( _storageServerConnectionString ) ;
            
            configOptions.Add ( _storageServerConnectionString ) ;
         }
         
         // LEAD Storage Server Options
         if ( rbOptionsCreate.Checked )
         {
            EnsureDefaultOnCreate ( _storageServerCreateOptionsConnectionString ) ;
            configOptions.Add ( _storageServerCreateOptionsConnectionString ) ;
         }
         else if ( rbOptionsConnect.Checked )
         {
            EnsureDefaultOnCreate ( _storageServerConnectOptionsConnectionString ) ;
            configOptions.Add ( _storageServerConnectOptionsConnectionString ) ;
         }
         
         // Medical Workstation
         if ( MedicalWorkstationDbCheckBox.Checked )
         {
            EnsureDefaultOnCreate ( _medicalWorkstationConnectionString ) ;
            
            configOptions.Add ( _medicalWorkstationConnectionString ) ;
         }
         
         // DICOM Worklist Database
         if ( WorklistDbCheckBox.Checked )
         {
            EnsureDefaultOnCreate ( _worklistConnectionString ) ;
            
            configOptions.Add ( _worklistConnectionString ) ;
         }
       
         
         return configOptions ;
      }

      private static string GetSqlCEDatabaseConnection(string databaseName)
      {
         return string.Format("Data Source={0}", Path.ChangeExtension(Path.Combine(MainOptions.GetFolderPath(), databaseName), "sdf"));
      }

      private void EnsureDefaultOnCreate(ConfigurationOptions configOption)
      {
         if ( CreateNewDbRadioButton.Checked ) 
         {
            if ( !IsConnectionValid (configOption) )
            {
               configOption.ConnectionSettings                  = new ConnectionStringSettings ( ) ;
               configOption.ConnectionSettings.Name             = configOption.DefaultDatabaseName ;
               
              if (DefaultSqlConnectionStringBuilder == null)
              {
                  configOption.ConnectionSettings.ConnectionString = GetSqlCEDatabaseConnection (configOption.DefaultDatabaseName) ;
                  configOption.ConnectionSettings.ProviderName = ConnectionProviders.SqlCeProvider.Name;
              }
              else
              {
                 configOption.ConnectionSettings.ConnectionString = GetLocalSqlDefaultConnectionString(DefaultSqlConnectionStringBuilder, configOption.DefaultDatabaseName);
                 configOption.ConnectionSettings.ProviderName = ConnectionProviders.SqlServerProvider.Name;
              }

               //}
               
            }
         }
      }

      private void pnlStorageServer_Resize(object sender, EventArgs e)
      {
         int width = Bounds.Width - StorageServerDbCheckBox.Width - btnConfigStorageServer.Width - 25;
         Size newSize = new Size(width, 0);
         this.lblStorgeServer.MaximumSize = newSize ;
         this.lblMedicalWorkstation.MaximumSize = newSize;
         this.lblWorklist.MaximumSize = newSize;
         this.labelCreateOptions.MaximumSize = newSize;
         this.labelConnectOptions.MaximumSize = newSize;
      }
   }
}
