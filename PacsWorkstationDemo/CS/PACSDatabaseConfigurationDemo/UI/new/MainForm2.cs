// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using Leadtools.Demos;
using MedicalWorkstationConfigurationDemo.UI;
using MedicalWorkstationConfigurationDemo;
using CSPacsDatabaseConfigurationDemo.UI.New;
using Leadtools.Medical.Logging.DataAccessLayer.Configuration;
using Leadtools.Medical.Storage.DataAccessLayer.Configuration;
using Leadtools.Medical.UserManagementDataAccessLayer.Configuration;
using Leadtools.Medical.OptionsDataAccessLayer.Configuration;
using Leadtools.Medical.AeManagement.DataAccessLayer.Configuration;
using Leadtools.Medical.Winforms.DataAccessLayer.Configuration;
using Leadtools.Medical.PermissionsManagement.DataAccessLayer.Configuration;
using Leadtools.Medical.Forward.DataAccessLayer.Configuration;
using Leadtools.Medical.Worklist.DataAccessLayer.Configuration;
using Leadtools.Medical.Media.DataAccessLayer.Configuration;
using Leadtools.Medical.Workstation.DataAccessLayer.Configuration;
using Leadtools.DicomDemos;
using Leadtools.Medical.DataAccessLayer.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Leadtools.Medical.Worklist.DataAccessLayer;
using Leadtools.Medical.Worklist.DataAccessLayer.BusinessEntity;
using System.Reflection;
using System.IO;
using Leadtools.Medical.DataAccessLayer;
using Leadtools.Medical.UserManagementDataAccessLayer;
using System.Security;
using Leadtools.Medical.PermissionsManagement.DataAccessLayer;
using Leadtools.Medical.Winforms;
using Leadtools.Medical.OptionsDataAccessLayer;
using System.Data.SqlClient;
using Leadtools.Demos.Sql;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Leadtools.Medical.WebViewer.PatientAccessRights.DataAccessAgent;
using Leadtools.Medical.ExportLayout.DataAccessLayer.Configuration;
#if (LEADTOOLS_V19_OR_LATER_MEDICAL_EXTERNAL_STORE) || (LEADTOOLS_V19_OR_LATER)
using Leadtools.Medical.ExternalStore.DataAccessLayer.Configuration;
#endif

namespace CSPacsDatabaseConfigurationDemo.UI
{
   public partial class MainForm2 : Form
   {
      private static DialogResult _defaultConnectionDialogResult = DialogResult.OK;

      public static DialogResult DefaultConnectionDialogResult
      {
         get
         {
            return _defaultConnectionDialogResult;
         }
      }

      public SqlConnectionStringBuilder DefaultSqlConnectionStringBuilder
      {
         get
         {
            return mainOptions1.DefaultSqlConnectionStringBuilder;
         }
         set
         {
            mainOptions1.DefaultSqlConnectionStringBuilder = value;
            mainOptions1.SetDefaultSqlServerLabel();
         }
      }

      public string DefaultConnection
      {
         get;
         set;
      }

      public static SqlConnectionStringBuilder GetDefaultLocalSqlConnection()
      {
         string connectionString;
         string connectionErrorMessage;

         SqlConnectionStringBuilder retDefaultConnectionStringBuilder = new SqlConnectionStringBuilder();
         retDefaultConnectionStringBuilder.IntegratedSecurity = true;
         retDefaultConnectionStringBuilder.InitialCatalog = "master";

         bool bFound = false;

         string[] localServers = SqlUtilities.GetLocalSQLServerInstances();
         if ((localServers != null) && (localServers.Length >= 1))
         {
            int i = 0;
            while (i < localServers.Length && !bFound)
            {
               retDefaultConnectionStringBuilder.DataSource = localServers[i];
               connectionString = retDefaultConnectionStringBuilder.ConnectionString;

               if (SqlUtilities.TestSQLConnectionString(connectionString, out connectionErrorMessage))
               {
                  // retDefaultConnection = localServers[i];
                  bFound = true;
               }
               i++;
            }
            // Postcondition:  i>= localServers.Length || bFound
         }

         if (bFound)
         {
            return retDefaultConnectionStringBuilder;
         }

         return null;
      }

      public static SqlConnectionStringBuilder GetDefaultNetworkSqlConnection()
      {
         SqlConnectionStringBuilder retDefaultConnectionStringBuilder = new SqlConnectionStringBuilder();
         retDefaultConnectionStringBuilder.IntegratedSecurity = true;
         retDefaultConnectionStringBuilder.InitialCatalog = "master";

         string connectionString;
         string connectionErrorMessage;

         bool bFound = false;

         ChooseDefaultSqlServerDialog dlg = new ChooseDefaultSqlServerDialog() { Icon = Program.GetAppIcon() };
         dlg.VerifyConnectionString = true;
         DialogResult dr = dlg.ShowDialog();
         if (dr == DialogResult.OK)
         {
            if (dlg.SqlDataSourceType == SqlDataSourceEnum.SqlServer)
            {
               _defaultConnectionDialogResult = DialogResult.OK;
               connectionString = dlg.GetConnectionString("master");

               if (SqlUtilities.TestSQLConnectionString(connectionString, out connectionErrorMessage))
               {
                  retDefaultConnectionStringBuilder.ConnectionString = connectionString;
                  bFound = true;
               }
            }
            else // SqlDataSourceEnum.SqlServerCompact
            {
               retDefaultConnectionStringBuilder = null;
               bFound = true;
            }
         }
         else
         {
            _defaultConnectionDialogResult = DialogResult.Cancel;
            return null;
         }

         if (bFound)
         {
            return retDefaultConnectionStringBuilder;
         }

         return null;
      }

      private static Image GetImageResource(string imageResourcePath)
      {
         Image image = null;
          Stream imageStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(imageResourcePath);
         if (imageStream != null)
         {
            image = Image.FromStream(imageStream);
         }
         return image;
      }

      // ErrorProvider is used for the icon
      private ErrorProvider _errorProvider = new ErrorProvider();

      private DatabaseComponents _supportedDatabases = DatabaseComponents.None;

      public MainForm2()
      {
         InitializeComponent();

         _supportedDatabases = ConfigurationData.GetSupportedDatabase();
         mainOptions1.SupportedDatabases = _supportedDatabases;

         ConfigMachine = GetConfiguration();
         ConfigGlobalPacs = DicomDemoSettingsManager.GetGlobalPacsConfiguration();
         ConfigSource = GetConfigurationSource();

         if (ConfigGlobalPacs.ConnectionStrings == null)
         {
            ConnectionStringsSection section = new ConnectionStringsSection();
            ConfigGlobalPacs.Sections.Add("connectionStrings", section);
         }

         mainOptions1.Init(ConfigMachine, ConfigGlobalPacs);
         mainOptions1.InitDefaults();
         btnApplyConfiguration.Click += new EventHandler(btnApplyConfiguration_Click);
         buttonClose.Click += ButtonClose_Click;

         ToolStripMenuItem toolStripItem = (ToolStripMenuItem)ContextMenuStrip.Items[1];
         if (toolStripItem != null)
         {
            toolStripItem.Checked = Program.ShouldEnumerateSqlServers;
         }

         labelConfigure.Text = @"<= Click to setup the PACS databases.";

         MyLogger.Initialize(this.listViewStatus);

         ImageList imageListSmall = new ImageList();

         imageListSmall.Images.Add(GetImageResource("CSPacsDatabaseConfigurationDemo.Resources.Start.ico"));
         imageListSmall.Images.Add(GetImageResource("CSPacsDatabaseConfigurationDemo.Resources.InvalidKey.ico"));
         imageListSmall.Images.Add(_errorProvider.Icon);
         listViewStatus.SmallImageList = imageListSmall;

         this.searchAllSQLServersToolStripMenuItem.Checked = Program.ShouldEnumerateSqlServers;

         this.listViewStatus.MouseClick += ListViewStatus_MouseClick;

         this.addDefaultImagesToolStripMenuItem.Click += AddDefaultImagesToolStripMenuItem_Click;
         this.addDefaultImagesToolStripMenuItem.Checked = true;
      }

      private void AddDefaultImagesToolStripMenuItem_Click(object sender, EventArgs e)
      {
         addDefaultImagesToolStripMenuItem.Checked = !addDefaultImagesToolStripMenuItem.Checked;
      }

      private void ListViewStatus_MouseClick(object sender, MouseEventArgs e)
      {
         ListViewItem lvi = listViewStatus.GetItemAt(e.X, e.Y);
         if (lvi != null)
         {
            if (lvi.Font.Underline)
            {
               Process.Start("https://www.leadtools.com/support/guides/externalwebservices-troubleshooting-guide.pdf");
               return;
            }
         }
      }

      private void ButtonClose_Click(object sender, EventArgs e)
      {
         Environment.Exit(0);
      }

      public void ShowChangeDefaultSqlServerButton(bool show)
      {
         mainOptions1.ShowChangeDefaultSqlServerButton(show);
      }

      private static Configuration GetConfiguration()
      {
         return ConfigurationManager.OpenMachineConfiguration();
      }

      private static IConfigurationSource GetConfigurationSource()
      {
         return ConfigurationSourceFactory.Create();
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

      public IConfigurationSource ConfigSource
      {
         get;
         set;
      }

      void UpdateUI(bool enable)
      {
         mainOptions1.Enabled = enable;
         buttonClose.Enabled = enable;
         labelConfigure.Enabled = enable;
         btnApplyConfiguration.Enabled = enable;

      }


      void btnApplyConfiguration_Click(object sender, EventArgs e)
      {
         MyLogger.ClearErrors();
         try
         {
            if (mainOptions1.ValidateConnections())
            {
               UpdateUI(false);
               List<ConfigurationOptions> configurationOptions = mainOptions1.GetConfigurationOptions();

               if (configurationOptions.Count == 0)
               {
                  MyLogger.MyMessagerShowWarning(this, "No Databases have been selected");
                  return;
               }

               string summary = BuildConfigurationSummary(configurationOptions);

               using (ConnectionSummaryDlg connectionSummaryDlg = new ConnectionSummaryDlg())
               {
                  connectionSummaryDlg.connectionSummary.Summary = summary;

                  if (connectionSummaryDlg.ShowDialog() == DialogResult.OK)
                  {
                     CreateOrConfigure(configurationOptions, mainOptions1.Mode);

                     string connectionString = ConfigurationData.StorageServerConnectionStringFromConfiguration();
                     if (!string.IsNullOrEmpty(connectionString))
                     {
                        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
                        if (builder.IntegratedSecurity)
                        {
                           MyLogger.ShowWarning("You are using integrated security to connect to the database.  The application pool settings may need to be modified.");
                           MyLogger.ShowHyperlink("Click here to address this issue.");
                        }
                     }

                     MyLogger.ShowMessage("Configuration is successful!", StatusType.Check);
                  }
               }
            }
         }
         catch (Exception exception)
         {
            MyLogger.MyMessagerShowError(this, exception);
         }
         finally
         {
            SqlConnection.ClearAllPools();
            UpdateUI(true);
         }
      }

      private void CreateOrConfigure(List<ConfigurationOptions> configurationOptions, DbConfigurationMode dbConfigurationMode)
      {
         DatabaseComponents allDatabases = GetDatabaseComponents(configurationOptions);

         if (dbConfigurationMode == DbConfigurationMode.Create)
         {
            CreateSelectedDatabases(configurationOptions, null);
         }
         else
         {
            // Connecting to existing Storage Server Database but creating the options database
            // In this case, only create the options database
            if (mainOptions1.StorageServerOptionsCreateSelected)
            {
               List<DatabaseComponents> componentsToInstall = new List<DatabaseComponents>();
               componentsToInstall.Add(DatabaseComponents.StorageServerOptions);
               CreateSelectedDatabases(configurationOptions, componentsToInstall);
            }
         }

         StoreConnectionStrings(configurationOptions);

         MyLogger.ShowMessage("Registering Configuration Sections...");
         RegisterConfigSections(configurationOptions, allDatabases);
         MyLogger.ShowFinished();

         if (dbConfigurationMode == DbConfigurationMode.Create)
         {
            if ((allDatabases & DatabaseComponents.Worklist) == DatabaseComponents.Worklist)
            {
               MyLogger.ShowMessage("Storing Modality Worklist data...");
               FillModalityWorklistDummyData();
               MyLogger.ShowFinished();
            }

            if ((allDatabases & DatabaseComponents.MedicalWorkstation) == DatabaseComponents.MedicalWorkstation)
            {
               MyLogger.ShowMessage("Creating Workstation User...");
               InsertWorkstationUser();
               MyLogger.ShowFinished();
            }

            if ((allDatabases & DatabaseComponents.StorageServer) == DatabaseComponents.StorageServer)
            {
               MyLogger.ShowMessage("Creating Storage Server User...");
               InsertStorageServerUser();
               MyLogger.ShowFinished();

               if (Program.IsToolkitDemo == true)
               {
                  if (addDefaultImagesToolStripMenuItem.Checked)
                  {
                     MyLogger.ShowMessage("Adding Default Images...");
                     Program.AddDefaultImages(ConfigGlobalPacs);
                  }
                  // MyLogger.ShowFinished();
               }

               MyLogger.ShowMessage("Adding Default IOD Classes...");
               AddDefaultIodClasses();
               MyLogger.ShowFinished();
            }
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
            SecureString userPassword = GetSecureString(mainOptions1.WorkstationUserPasswordTextBox.Text);
            accessAgent.AddUser(mainOptions1.WorkstationUserNameTextBox.Text, userPassword, true);

            PermissionManagementDataAccessConfigurationView OptionsConfigView = new PermissionManagementDataAccessConfigurationView(ConfigGlobalPacs, DicomDemoSettingsManager.ProductNameStorageServer, null);
            IPermissionManagementDataAccessAgent optionsAgent = DataAccessFactory.GetInstance(OptionsConfigView).CreateDataAccessAgent<IPermissionManagementDataAccessAgent>();

            foreach (string permission in Program.storageServerDefaultPermissionsList)
            {
               optionsAgent.AddUserPermission(permission, mainOptions1.WorkstationUserNameTextBox.Text);
            }
         }
         catch (Exception exception)
         {
            MyLogger.MyMessagerShowError(this, "InsertStorageServerUser Failed: " + exception.Message);
            throw exception;
         }
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
               MyLogger.MyMessagerShowError(this, "Failed to read IOD Classes from Leadtools.Dicom.Tables.dll");
            }

            OptionsDataAccessConfigurationView optionsConfigView = new OptionsDataAccessConfigurationView(DicomDemoSettingsManager.GetGlobalPacsConfiguration(), DicomDemoSettingsManager.ProductNameStorageServer, null);
            IOptionsDataAccessAgent optionsAgent = DataAccessFactory.GetInstance(optionsConfigView).CreateDataAccessAgent<IOptionsDataAccessAgent>();
            optionsAgent.Set<PresentationContextList>(typeof(PresentationContextList).Name, pcList, new Type[0]);
         }
         catch (Exception exception)
         {
            MyLogger.MyMessagerShowError(this, "AddDefaultIodClasses Failed: " + exception.Message);
            throw exception;
         }

      }

      private void FillModalityWorklistDummyData()
      {
         WorklistDataAccessConfigurationView worklistConfigView;
         IWorklistDataAccessAgent AccessAgent;
         MWLDataset worklistDataSet;
         Assembly executingAssembly = null;
         Stream datasetDataStream = null;


         worklistConfigView = new WorklistDataAccessConfigurationView(DicomDemoSettingsManager.GetGlobalPacsConfiguration(), DicomDemoSettingsManager.ProductNameDemoServer, null);

         ConfigurationManager.RefreshSection("connectionStrings");
         ConfigurationManager.RefreshSection(worklistConfigView.DataAccessSettingsSectionName);

         AccessAgent = DataAccessFactory.GetInstance(worklistConfigView).CreateDataAccessAgent<IWorklistDataAccessAgent>();
         executingAssembly = Assembly.GetExecutingAssembly();
         datasetDataStream = executingAssembly.GetManifestResourceStream("CSPacsDatabaseConfigurationDemo.Common.MWLDatasetDummyTestData.xml");
         worklistDataSet = new MWLDataset();

         worklistDataSet.ReadXml(datasetDataStream);

         AccessAgent.UpdateMWL(worklistDataSet);

         datasetDataStream.Close();

         worklistDataSet.Dispose();
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
            userPassword = GetSecureString(mainOptions1.WorkstationUserPasswordTextBox.Text);

            accessAgent.AddUser(mainOptions1.WorkstationUserNameTextBox.Text, userPassword, true);
         }
         catch (Exception exception)
         {
            MyLogger.MyMessagerShowError(this, "InsertWorkstationUser Failed: " + exception.Message);
            throw exception;
         }

      }

      public static SecureString GetSecureString(string password)
      {
         char[] passwordChars;
         SecureString secure;


         passwordChars = password.ToCharArray();
         secure = new SecureString();

         foreach (char c in passwordChars)
         {
            secure.AppendChar(c);
         }

         secure.MakeReadOnly();

         return secure;
      }

      private DatabaseComponents GetDatabaseComponents(List<ConfigurationOptions> configurationOptions)
      {
         DatabaseComponents createdComponents = DatabaseComponents.None;

         foreach (ConfigurationOptions configOption in configurationOptions)
         {
            createdComponents |= configOption.DbComponent;
         }

         return createdComponents;
      }

      // If 'componentsToInstall' is null, install all the components
      // Otherwise, only install the components in 'componentsToInstall'
      private DatabaseComponents CreateSelectedDatabases(List<ConfigurationOptions> configurationOptions, List<DatabaseComponents> componentsToInstall)
      {
         DatabaseComponents createdComponents = DatabaseComponents.None;

         foreach (ConfigurationOptions configOption in configurationOptions)
         {
            if (componentsToInstall != null && !componentsToInstall.Contains(configOption.DbComponent))
            {
               continue;
            }

            createdComponents |= configOption.DbComponent;

            string createMessage = string.Format("Creating Database [{0}]...", configOption.FriendlyName);
            MyLogger.ShowMessage(createMessage);

            switch (configOption.DbComponent)
            {
               case DatabaseComponents.StorageServer:
                  {
                     if (configOption.ConnectionSettings.ProviderName == ConnectionProviders.SqlServerProvider.Name)
                     {
                        DicomStorageServerSqlInstaller.InstallDatabase(configOption.ConnectionSettings.ConnectionString);
                     }
                     else
                     {
                        DicomStorageServerSqlCeInstaller.InstallDatabase(configOption.ConnectionSettings.ConnectionString);
                     }
                  }
                  break;

               case DatabaseComponents.StorageServerOptions:
                  {
                     if (configOption.ConnectionSettings.ProviderName == ConnectionProviders.SqlServerProvider.Name)
                     {
                        DicomStorageServerOptionsSqlInstaller.InstallDatabase(configOption.ConnectionSettings.ConnectionString);
                     }
                     else
                     {
                        DicomStorageServerOptionsSqlCeInstaller.InstallDatabase(configOption.ConnectionSettings.ConnectionString);
                     }
                  }
                  break;

               case DatabaseComponents.MedicalWorkstation:
                  {
                     if (configOption.ConnectionSettings.ProviderName == ConnectionProviders.SqlServerProvider.Name)
                     {
                        MedicalWorkstationSqlInstaller.InstallDatabase(configOption.ConnectionSettings.ConnectionString);
                     }
                     else
                     {
                        MedicalWorkstationSqlCeInstaller.InstallDatabase(configOption.ConnectionSettings.ConnectionString);
                     }
                  }
                  break;

               case DatabaseComponents.Logging:
                  {
                     if (configOption.ConnectionSettings.ProviderName == ConnectionProviders.SqlServerProvider.Name)
                     {
                        DicomLoggingSqlInstaller.InstallDatabase(configOption.ConnectionSettings.ConnectionString);
                     }
                     else
                     {
                        DicomLoggingSqlCeInstaller.InstallDatabase(configOption.ConnectionSettings.ConnectionString);
                     }
                  }
                  break;

               case DatabaseComponents.Storage:
                  {
                     if (configOption.ConnectionSettings.ProviderName == ConnectionProviders.SqlServerProvider.Name)
                     {
                        DicomStorageSqlInstaller.InstallDatabase(configOption.ConnectionSettings.ConnectionString);
                     }
                     else
                     {
                        DicomStorageSqlCeInstaller.InstallDatabase(configOption.ConnectionSettings.ConnectionString);
                     }
                  }
                  break;

               case DatabaseComponents.Worklist:
                  {
                     if (configOption.ConnectionSettings.ProviderName == ConnectionProviders.SqlServerProvider.Name)
                     {
                        WorklistSqlInstaller.InstallDatabase(configOption.ConnectionSettings.ConnectionString);
                     }
                     else
                     {
                        WorklistSqlCeInstaller.InstallDatabase(configOption.ConnectionSettings.ConnectionString);
                     }
                  }
                  break;

               case DatabaseComponents.MediaCreation:
                  {
                     if (configOption.ConnectionSettings.ProviderName == ConnectionProviders.SqlServerProvider.Name)
                     {
                        MediaCreationSqlInstaller.InstallDatabase(configOption.ConnectionSettings.ConnectionString);
                     }
                     else
                     {
                        MediaCreationSqlCeInstaller.InstallDatabase(configOption.ConnectionSettings.ConnectionString);
                     }
                  }
                  break;

               case DatabaseComponents.Workstation:
                  {
                     if (configOption.ConnectionSettings.ProviderName == ConnectionProviders.SqlServerProvider.Name)
                     {
                        WorkstationSqlInstaller.InstallDatabase(configOption.ConnectionSettings.ConnectionString);
                     }
                     else
                     {
                        WorkstationSqlCeInstaller.InstallDatabase(configOption.ConnectionSettings.ConnectionString);
                     }
                  }
                  break;

               case DatabaseComponents.UserManagement:
                  {
                     if (configOption.ConnectionSettings.ProviderName == ConnectionProviders.SqlServerProvider.Name)
                     {
                        WorkstationUsersManagementSqlInstaller.InstallDatabase(configOption.ConnectionSettings.ConnectionString);
                     }
                     else
                     {
                        WorkstationUsersManagementSqlCeInstaller.InstallDatabase(configOption.ConnectionSettings.ConnectionString);
                     }
                  }
                  break;
            }
         }

         return createdComponents;
      }

      private void StoreConnectionStrings(List<ConfigurationOptions> configurationOptions)
      {
         ConfigurationManager.RefreshSection("connectionStrings");

         foreach (ConfigurationOptions configOption in configurationOptions)
         {
            ConfigurationManager.RefreshSection("connectionStrings");
            bool machineConfigChanged = false;
            bool globalPacsConfigChanged = false;

            switch (configOption.DbComponent)
            {
               case DatabaseComponents.StorageServer:
               case DatabaseComponents.StorageServerOptions:
               case DatabaseComponents.MedicalWorkstation:
               case DatabaseComponents.Worklist:
                  {
                     AddConnectionString(ConfigGlobalPacs.ConnectionStrings, configOption.ConnectionSettings);
                     globalPacsConfigChanged = true;
                  }
                  break;

                  //case DatabaseComponents.Logging:
                  //{
                  //   AddConnectionString(ConfigGlobalPacs.ConnectionStrings, configOption.ConnectionSettings);
                  //   globalPacsConfigChanged = true;

                  //   AddConnectionString(ConfigMachine.ConnectionStrings, configOption.ConnectionSettings);
                  //   machineConfigChanged = true;
                  //}
                  //break ;

                  //case DatabaseComponents.Storage:
                  //case DatabaseComponents.UserManagement:
                  //{
                  //   AddConnectionString(ConfigGlobalPacs.ConnectionStrings, configOption.ConnectionSettings);
                  //   globalPacsConfigChanged = true;
                  //}
                  //break ;

                  //case DatabaseComponents.Worklist:
                  //case DatabaseComponents.MediaCreation:
                  //case DatabaseComponents.Workstation:
                  //{
                  //   AddConnectionString(ConfigMachine.ConnectionStrings, configOption.ConnectionSettings);
                  //   machineConfigChanged = true;
                  //}
                  // break ;

            }

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
      }

      private void RegisterConfigSections(List<ConfigurationOptions> configurationOptions, DatabaseComponents createdDb)
      {
         foreach (ConfigurationOptions configOption in configurationOptions)
         {
            switch (configOption.DbComponent)
            {
               case DatabaseComponents.StorageServer:
                  {
                     string connectionStringName = configOption.ConnectionSettings.Name;

                     ConfigureLogging(DicomDemoSettingsManager.ProductNameStorageServer, connectionStringName, ((createdDb & DatabaseComponents.Logging) == DatabaseComponents.Logging) ? DefaultConnectionNameType.None : DefaultConnectionNameType.UseMachineConfig);
                     ConfigureStorage(DicomDemoSettingsManager.ProductNameStorageServer, connectionStringName, ((createdDb & DatabaseComponents.Logging) == DatabaseComponents.Storage) ? DefaultConnectionNameType.None : DefaultConnectionNameType.UseMachineConfig);
                     ConfigureUserManagement(DicomDemoSettingsManager.ProductNameStorageServer, connectionStringName, ((createdDb & DatabaseComponents.Logging) == DatabaseComponents.Workstation) ? DefaultConnectionNameType.None : DefaultConnectionNameType.UseMachineConfig);
                     ConfigureAeManagement(DicomDemoSettingsManager.ProductNameStorageServer, connectionStringName, DefaultConnectionNameType.None);
                     ConfigureAePermissionManagement(DicomDemoSettingsManager.ProductNameStorageServer, connectionStringName, DefaultConnectionNameType.None);
                     ConfigurePermissionManagement(DicomDemoSettingsManager.ProductNameStorageServer, connectionStringName, DefaultConnectionNameType.None);
                     ConfigureForward(DicomDemoSettingsManager.ProductNameStorageServer, connectionStringName, DefaultConnectionNameType.None);
#if (LEADTOOLS_V19_OR_LATER_MEDICAL_EXTERNAL_STORE) || (LEADTOOLS_V19_OR_LATER)
                     ConfigureExternalStore(DicomDemoSettingsManager.ProductNameStorageServer, connectionStringName, DefaultConnectionNameType.None);
#endif
                     // ConfigureOptions                 ( DicomDemoSettingsManager.ProductNameStorageServer, connectionStringName, DefaultConnectionNameType.None);

                     if (!mainOptions1.StorageServerOptionsCreateSelected && !mainOptions1.StorageServerOptionsConnectSelected)
                     {
                        // No Load Balancing
                        // The server options database is part of the StorageServerDatabase
                        ConfigureOptions(DicomDemoSettingsManager.ProductNameStorageServer, connectionStringName, DefaultConnectionNameType.None);
                     }
#if (LEADTOOLS_V20_OR_LATER)
                     ConfigurePatientRightsDataAccess(DicomDemoSettingsManager.ProductNameStorageServer, connectionStringName, DefaultConnectionNameType.None);
                     ConfigureExportLayoutDataAccess(DicomDemoSettingsManager.ProductNameStorageServer, connectionStringName, DefaultConnectionNameType.None);
#endif
                  }
                  break;

               case DatabaseComponents.StorageServerOptions:
                  {
                     ConfigureOptions(DicomDemoSettingsManager.ProductNameStorageServer, configOption.ConnectionSettings.Name, DefaultConnectionNameType.None);
                  }
                  break;

               case DatabaseComponents.MedicalWorkstation:
                  {
                     string connectionStringName = configOption.ConnectionSettings.Name;

                     ConfigureLogging(DicomDemoSettingsManager.ProductNameWorkstation, connectionStringName, DefaultConnectionNameType.None);
                     ConfigureStorage(DicomDemoSettingsManager.ProductNameWorkstation, connectionStringName, DefaultConnectionNameType.None);
                     ConfigureUserManagement(DicomDemoSettingsManager.ProductNameWorkstation, connectionStringName, DefaultConnectionNameType.None);
                     //ConfigureAeManagement            ( DicomDemoSettingsManager.ProductNameStorageServer, connectionStringName, DefaultConnectionNameType.None);
                     //ConfigureAePermissionManagement  ( DicomDemoSettingsManager.ProductNameStorageServer, connectionStringName, DefaultConnectionNameType.None);
                     //ConfigurePermissionManagement    ( DicomDemoSettingsManager.ProductNameStorageServer, connectionStringName, DefaultConnectionNameType.None);
                     //ConfigureForward                 ( DicomDemoSettingsManager.ProductNameStorageServer, connectionStringName, DefaultConnectionNameType.None);
#if (LEADTOOLS_V19_OR_LATER)
                     ConfigureOptions(DicomDemoSettingsManager.ProductNameWorkstation, connectionStringName, DefaultConnectionNameType.None);
#endif // #if (LEADTOOLS_V19_OR_LATER)
                     ConfigureMediaCreation(DicomDemoSettingsManager.ProductNameWorkstation, connectionStringName, DefaultConnectionNameType.None);
                     ConfigureWorkstation(DicomDemoSettingsManager.ProductNameWorkstation, connectionStringName, DefaultConnectionNameType.None);


                  }
                  break;

               case DatabaseComponents.Worklist:
                  {
                     ConfigureWorklist(DicomDemoSettingsManager.ProductNameDemoServer, configOption.ConnectionSettings.Name, DefaultConnectionNameType.None);
                  }
                  break;



                  //   case DatabaseComponents.Logging:
                  //   {
                  //      ConfigureLogging(null, configOption.ConnectionSettings.Name, DefaultConnectionNameType.UseCurrent);
                  //      ConfigureLoggingMachineConfig(null, configOption.ConnectionSettings.Name, DefaultConnectionNameType.UseCurrent);
                  //   }
                  //   break;

                  //   case DatabaseComponents.Storage:
                  //   {
                  //      ConfigureStorage( null, configOption.ConnectionSettings.Name, DefaultConnectionNameType.UseCurrent);
                  //   }
                  //   break ;

                  //   case DatabaseComponents.Worklist:
                  //   {
                  //      ConfigureWorklist(null, configOption.ConnectionSettings.Name, DefaultConnectionNameType.UseCurrent);
                  //   }
                  //   break ;

                  //   case DatabaseComponents.MediaCreation:
                  //   {
                  //      ConfigureMediaCreation(null, configOption.ConnectionSettings.Name, DefaultConnectionNameType.UseCurrent);
                  //   }
                  //   break ;

                  //   case DatabaseComponents.Workstation:
                  //   {
                  //      ConfigureWorkstation(null, configOption.ConnectionSettings.Name, DefaultConnectionNameType.UseCurrent);
                  //   }
                  //   break ;

                  //   case DatabaseComponents.UserManagement:
                  //   {
                  //      ConfigureUserManagement(null, configOption.ConnectionSettings.Name, DefaultConnectionNameType.UseCurrent);
                  //   }
                  //   break ;
            }

            try
            {
               ConfigMachine.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
               MyLogger.MyMessagerShowError(this, "ConfigMachine.Save Failed");
               throw ex;
            }

            try
            {
               ConfigGlobalPacs.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
               MyLogger.MyMessagerShowError(this, "ConfigGlobalPacs.Save Failed");
               throw ex;
            }
         }
      }

      private void AddConnectionString
      (
         ConnectionStringsSection connectionStringsSection,
         ConnectionStringSettings connectionString
      )
      {
         try
         {
            ConnectionStringSettings tempConnection;


            tempConnection = connectionStringsSection.ConnectionStrings[connectionString.Name];

            if (null == tempConnection)
            {
               connectionStringsSection.ConnectionStrings.Add(connectionString);
            }
            else
            {
               tempConnection.ConnectionString = connectionString.ConnectionString;
               tempConnection.ProviderName = connectionString.ProviderName;
            }
         }
         catch (Exception ex)
         {
            MyLogger.MyMessagerShowError(this, "AddConnectionString Failed");
            throw ex;
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
            MyLogger.MyMessagerShowError(this, "ConfigureLogging Failed");
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
            MyLogger.MyMessagerShowError(this, "ConfigureLogging Failed");
            throw ex;
         }
      }


      private void ConfigureStorage(string productName, string connectionStringName, DefaultConnectionNameType connectionType)
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
            MyLogger.MyMessagerShowError(this, "ConfigureStorage Failed");
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
            MyLogger.MyMessagerShowError(this, "ConfigureUserManagement Failed");
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
            MyLogger.MyMessagerShowError(this, "ConfigureOptions Failed");
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
            MyLogger.MyMessagerShowError(this, "ConfigureAeManagement Failed");
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
            MyLogger.MyMessagerShowError(this, "ConfigureAePermissionManagement Failed");
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
            MyLogger.MyMessagerShowError(this, "ConfigurePermissionManagement Failed");
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
            MyLogger.MyMessagerShowError(this, "ConfigureForward Failed");
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
            MyLogger.MyMessagerShowError(this, "ConfigureExternalStore Failed");
            throw ex;
         }
      }
#endif // #if (LEADTOOLS_V19_OR_LATER_MEDICAL_EXTERNAL_STORE) || (LEADTOOLS_V19_OR_LATER)     

      // Workstation only DLLs
      private void ConfigureWorklist(string productName, string connectionStringName, DefaultConnectionNameType connectionType)
      {
         try
         {
            WorklistDataAccessConfigurationView sectionView = new WorklistDataAccessConfigurationView(ConfigGlobalPacs, productName, null);
            sectionView.ConfigurationSource = ConfigSource;
            sectionView.Configuration = ConfigGlobalPacs;
            ConfigureSection(ConfigGlobalPacs, sectionView, connectionStringName, productName, connectionType);
         }
         catch (Exception ex)
         {
            MyLogger.MyMessagerShowError(this, "ConfigureWorklist Failed");
            throw ex;
         }
      }


      private void ConfigureMediaCreation(string productName, string connectionStringName, DefaultConnectionNameType connectionType)
      {
         try
         {
            MediaCreationDataAccessConfigurationView mediaSectionView = new MediaCreationDataAccessConfigurationView(ConfigGlobalPacs, productName, null);
            mediaSectionView.ConfigurationSource = ConfigSource;
            mediaSectionView.Configuration = ConfigGlobalPacs;
            ConfigureSection(ConfigGlobalPacs, mediaSectionView, connectionStringName, productName, connectionType);
         }
         catch (Exception ex)
         {
            MyLogger.MyMessagerShowError(this, "ConfigureMediaCreation Failed");
            throw ex;
         }
      }

      private void ConfigureWorkstation(string productName, string connectionStringName, DefaultConnectionNameType connectionType)
      {
         try
         {
            WorkstationDataAccessConfigurationView sectionView = new WorkstationDataAccessConfigurationView(ConfigGlobalPacs, DicomDemoSettingsManager.ProductNameWorkstation, null);
            // sectionView.ConfigurationSource = ConfigSource;
            sectionView.Configuration = ConfigGlobalPacs;
            ConfigureSection(ConfigGlobalPacs, sectionView, connectionStringName, productName, connectionType);
         }
         catch (Exception ex)
         {
            MyLogger.MyMessagerShowError(this, "ConfigureWorkstation Failed");
            throw ex;
         }
      }

#if (LEADTOOLS_V20_OR_LATER)
      private void ConfigurePatientRightsDataAccess (string productName, string connectionStringName, DefaultConnectionNameType connectionType)
      {
         PatientRightsDataAccessConfigurationView accessRightsConfigurationView = new PatientRightsDataAccessConfigurationView (ConfigGlobalPacs, productName, null ) ;
         
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

         switch (defaultType)
         {
            case DefaultConnectionNameType.None:
               break;

            case DefaultConnectionNameType.UseCurrent:
               {
                  dataAccessSettings.ConnectionName = connectionName;
               }
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
         {
            config.Sections.Add(sectionView.DataAccessSettingsSectionName, dataAccessSettings);
         }
      }

      private void AddProduct(DataAccessSettings dataAccessSettings, string productName, string connectionName)
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

      private ConnectionStringSettings GetConnectionString
      (
         Configuration config,
         string dataAccessSectionName
      )
      {
         DataAccessSettings settings;


         try
         {
            settings = config.GetSection(dataAccessSectionName) as DataAccessSettings;

            if (null == settings)
            {
               return new ConnectionStringSettings();
            }
            else
            {
               ConnectionStringSettings connection;


               connection = config.ConnectionStrings.ConnectionStrings[settings.ConnectionName];

               if (null == connection)
               {
                  return new ConnectionStringSettings();
               }
               else
               {
                  return connection;
               }
            }
         }
         catch (Exception)
         {
            return new ConnectionStringSettings();
         }
      }


      private string BuildConfigurationSummary(List<ConfigurationOptions> configurationOptions)
      {
         string summary;

         summary = string.Format("The following databases will be {0}", (mainOptions1.Mode == DbConfigurationMode.Create) ? "created" : "configured");

         foreach (ConfigurationOptions configOption in configurationOptions)
         {
            string cs = configOption.ConnectionSettings.ConnectionString.MaskPassword();
            summary += string.Format("\n\n{2}:\nConnection:{0}\nProvider:{1}", cs, configOption.ConnectionSettings.ProviderName, configOption.FriendlyName);
         }

         return summary;
      }

      private void clearStatusToolStripMenuItem_Click(object sender, EventArgs e)
      {
         MyLogger.ClearErrors();
      }

      private void searchAllSQLServersToolStripMenuItem_Click(object sender, EventArgs e)
      {
         ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
         if (menuItem != null)
         {
            Program.ShouldEnumerateSqlServers = !Program.ShouldEnumerateSqlServers;
            menuItem.Checked = Program.ShouldEnumerateSqlServers;
         }
      }
   }

   class ConfigurationOptions
   {
      public ConnectionStringSettings ConnectionSettings;
      public Label ConfigurationLabel;
      public DatabaseComponents DbComponent;
      public Configuration ConfigurationSource;
      public string DataAccessName;
      public string ProductName;
      public string DefaultDatabaseName;
      public string FriendlyName;
   }

   enum DefaultConnectionNameType
   {
      None = 0,
      UseMachineConfig = 1,
      UseCurrent = 2,
   }
   public enum StatusType
   {
      Check = 0,
      Warning = 1,
      Error = 2,
      Nothing = 3
   }

   public static class MyLogger
   {
      private static ListView _listViewStatus;

      public static void Initialize(ListView listView)
      {
         _listViewStatus = listView;
      }

      public static void ClearErrors()
      {
         try
         {
            if (_listViewStatus != null)
            {
               _listViewStatus.Items.Clear();
            }
         }
         catch (Exception)
         {
         }
      }

      private static Font _hyperlinkFont = null;

      public static Font HyperlinkFont
      {
         get
         {
            if (_listViewStatus == null)
               return null;

            if (_hyperlinkFont == null)
            {
               _hyperlinkFont = new Font(_listViewStatus.Font, FontStyle.Underline);
            }
            return _hyperlinkFont;
         }
      }

      private static void AddItemWithIcon(string sMsg, StatusType status)
      {
         AddItemWithIcon(sMsg, status, false);
      }

      private static void AddItemWithIcon(string sMsg, StatusType status, bool hyperlink)
      {
         List<string> strings = new List<string>(Regex.Split(sMsg, @"(?<=\G.{256})", RegexOptions.Singleline));
         foreach (string s in strings)
         {
            AddItemWithIcon256(s, status, hyperlink);
         }
      }

      private static void AddItemWithIcon256(string sMsg, StatusType status, bool hyperlink)
      {
         if (sMsg.Trim().Length == 0)
            return;

         if (_listViewStatus == null)
            return;

         Color foreColor = _listViewStatus.ForeColor;
         Color backColor = Color.White;
         switch (status)
         {
            case StatusType.Nothing:
               foreColor = Color.DarkGreen;
               break;

            case StatusType.Check:
            case StatusType.Warning:
               foreColor = Color.Blue;
               break;
            case StatusType.Error:
               foreColor = Color.Red;
               break;
         }

         if (hyperlink)
         {
            foreColor = Color.Blue;
         }

         ListViewItem li = new ListViewItem(sMsg.Trim(), (int)status);
         _listViewStatus.Items.Add(li);
         li.ForeColor = foreColor;
         li.BackColor = backColor;

         if (hyperlink)
         {
            li.Font = HyperlinkFont;
         }
         _listViewStatus.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
         _listViewStatus.Items[_listViewStatus.Items.Count - 1].EnsureVisible();

         _lastItemLength = li.Text.Length;
      }

      private static int _lastItemLength = 0;

      public static void ShowWaiting(string s)
      {
         if (_listViewStatus == null)
            return;

         var r = Enumerable.Empty<ListViewItem>();

         if (_listViewStatus.Items.Count > 0)
            r = _listViewStatus.Items.OfType<ListViewItem>();

         ListViewItem lastItem = r.LastOrDefault();

         if (lastItem != null)
         {
            string text = lastItem.Text.Substring(0, _lastItemLength);
            lastItem.Text = text + s;

            _listViewStatus.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            _listViewStatus.Items[_listViewStatus.Items.Count - 1].EnsureVisible();
         }
         Application.DoEvents();
      }

      public static void ShowFinished()
      {
         ShowWaiting("Finished");
      }

      public static void ShowError(string sError)
      {
         try
         {
            AddItemWithIcon(sError, StatusType.Error);
            Application.DoEvents();
         }
         catch (Exception)
         {
         }
      }

      public static void ShowWarning(string sMsg)
      {
         try
         {
            AddItemWithIcon(sMsg, StatusType.Warning);
            Application.DoEvents();
         }
         catch (Exception)
         {
         }
      }

      public static void ShowHyperlink(string sMsg)
      {
         try
         {
            AddItemWithIcon(sMsg, StatusType.Nothing, true);
            Application.DoEvents();
         }
         catch (Exception)
         {
         }
      }

      public static void ShowMessage(string sMsg)
      {
         try
         {
            AddItemWithIcon(sMsg, StatusType.Nothing);
            Application.DoEvents();
         }
         catch (Exception)
         {
         }
      }

      public static void ShowMessage(string sMsg, StatusType status)
      {
         try
         {
            AddItemWithIcon(sMsg, status);
            Application.DoEvents();
         }
         catch (Exception)
         {
         }
      }

      public static void MyMessagerShowInformation(IWin32Window owner, string message)
      {
         ShowMessage(message);
         Messager.ShowInformation(owner, message);
      }

      public static void MyMessagerShowWarning(IWin32Window owner, string message)
      {
         ShowWarning( message);
         Messager.ShowWarning(owner, message);
      }

      public static void MyMessagerShowError(IWin32Window owner, string message)
      {
         ShowError( message);
         Messager.ShowError(owner, message);
      }

      public static void MyMessagerShowError(IWin32Window owner, Exception exception)
      {
         ShowError( exception.Message);
         Messager.ShowError(owner, exception);
      }

   }
}
