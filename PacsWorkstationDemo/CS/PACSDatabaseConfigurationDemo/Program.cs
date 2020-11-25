// *************************************************************
// Copyright (c) 1991-2020 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Principal;
using System.Diagnostics;
using MedicalWorkstationConfigurationDemo.UI;
using Leadtools.Demos;
using Leadtools.Dicom;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Configuration;
using System.Data.SqlClient;
using Leadtools.Demos.StorageServer.DataTypes;
using Leadtools.DicomDemos;
using Leadtools.Medical.OptionsDataAccessLayer;
using Leadtools.Medical.DataAccessLayer;
using Leadtools.Medical.OptionsDataAccessLayer.Configuration;

namespace MedicalWorkstationConfigurationDemo
{
   static partial class Program
   {
      [DllImport("kernel32.dll")]
      static extern bool AttachConsole(int dwProcessId);
      private const int ATTACH_PARENT_PROCESS = -1;

      const string sNewlineTab = "\r\n\t";
      const string sTab = "\t";

      const string sQuestion = "/?";
      const string sHelp = "/help";
      const string sDicomServerUsername = "/dicom_server_username=";
      const string sDicomServerPassword = "/dicom_server_password=";
      const string sSqlDataSource = "/sql_data_source=";
      const string sSqlAuthenticationType = "/sql_authentication_type=";
      const string sSqlUsername = "/sql_username=";
      const string sSqlPassword = "/sql_password=";
      const string sDatabaseName = "/database_name=";
      const string sDatabaseType = "/database_type=";

      static string sHelpInstructions =
         $"Command Line Options:{sNewlineTab}" +
         $"{sQuestion} or {sHelp}{sTab}Displays this help {sNewlineTab}" +
         $"{sDicomServerUsername}<username>{sNewlineTab}" +
         $"{sDicomServerPassword}<password>{sNewlineTab}" +
         $"{sSqlDataSource}<sqlserver\\instancename>{sNewlineTab}" +
         $"{sSqlAuthenticationType}[sql | windows]{sNewlineTab}" +
         $"{sSqlUsername}<SQL username>{sNewlineTab}" +
         $"{sSqlPassword}<SQL password>{sNewlineTab}" +
         $"{sDatabaseName}<database name>{sNewlineTab}" +
         $"{sDatabaseType}[new | existing]{sNewlineTab}" +
         "";

      public static CommandLineOptions _commandLineOptions = new CommandLineOptions();

      // Here are two sample command lines:
      //       /configure /server_aetitle=STORAGE_SCU /server_ip=10.1.1.167 /server_port=104 /defaults
      //       /configure /server_aetitle=test_server_ae /server_ip=10.1.1.123 /server_port=123 /client_aetitle=test_client_ae /client_ip=test_client_ip /client_port=456 /defaults
      static bool ReadCommandLine(string[] args)
      {
         foreach (string s in args)
         {
            string sVal = string.Empty;

            if ((string.Compare(sHelp, s, true) == 0) || (string.Compare(sQuestion, s, true) == 0))
            {
               MessageBox.Show(sHelpInstructions, "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
               return false;
            }
            else if (s.StartsWith(sSqlDataSource, true, null))
            {
               _commandLineOptions.SqlDataSource = s.MySubstring(sSqlDataSource.Length);
            }
            else if (s.StartsWith(sSqlAuthenticationType, true, null))
            {
               sVal = s.MySubstring(sSqlAuthenticationType.Length);
               _commandLineOptions.SetAuthenticationType(sVal);
            }
            else if (s.StartsWith(sSqlUsername, true, null))
            {
               _commandLineOptions.SqlUsername = s.MySubstring(sSqlUsername.Length);
            }
            else if (s.StartsWith(sSqlPassword, true, null))
            {
               _commandLineOptions.SqlPassword = s.MySubstring(sSqlPassword.Length);
            }
            else if (s.StartsWith(sDatabaseName, true, null))
            {
               _commandLineOptions.DatabaseName = s.MySubstring(sDatabaseName.Length);
            }
            else if (s.StartsWith(sDatabaseType, true, null))
            {
               sVal = s.MySubstring(sDatabaseType.Length);
               _commandLineOptions.SetDatabaseType(sVal);
            }
            else if (s.StartsWith(sDicomServerUsername, true, null))
            {
               _commandLineOptions.DicomServerUsername = s.MySubstring(sDicomServerUsername.Length);
            }
            else if (s.StartsWith(sDicomServerPassword, true, null))
            {
               _commandLineOptions.DicomServerPassword = s.MySubstring(sDicomServerPassword.Length);
            }
         }
         return true;
      }

      public static bool AddDicomServiceNameToGlobalPacsConfig()
      {
         bool success = true;
         try
         {
            OptionsDataAccessConfigurationView optionsConfigView = new OptionsDataAccessConfigurationView(DicomDemoSettingsManager.GetGlobalPacsConfiguration(), DicomDemoSettingsManager.ProductNameStorageServer, null);
            IOptionsDataAccessAgent optionsAgent = DataAccessFactory.GetInstance(optionsConfigView).CreateDataAccessAgent<IOptionsDataAccessAgent>();
            if (optionsAgent != null)
            {
               StorageServerInformation serverInfo = null;
               string name = typeof(StorageServerInformation).Name;

               if (optionsAgent.OptionExits(name))
               {
                  serverInfo = optionsAgent.Get<StorageServerInformation>(name, null, new Type[0]);
                  if (serverInfo != null && !string.IsNullOrEmpty(serverInfo.ServiceName))
                  {
                     GlobalPacsUpdater.ModifyGlobalPacsConfiguration(DicomDemoSettingsManager.ProductNameStorageServer, serverInfo.ServiceName, GlobalPacsUpdater.ModifyConfigurationType.Add);
                  }
               }
            }
         }
         catch (Exception)
         {
            success = false;
         }
         return success;
      }

      static bool SilentConfigure()
      {
         bool success = true;
         try
         {
            if (_commandLineOptions.DatabaseType == DatabaseTypeEnum.New)
            {
               DicomStorageServerSqlInstaller.InstallDatabase(_commandLineOptions.GetConnectionString());
            }

            MainForm.ConfigMachine = MainForm.GetConfiguration();
            MainForm.GlobalPacsAlreadyExists = MainForm.VerifyAlreadyExistGlobalPacsConfig();
            MainForm.ConfigGlobalPacs = DicomDemoSettingsManager.GetGlobalPacsConfiguration();
            MainForm.ConfigSource = MainForm.GetConfigurationSource();

            if (MainForm.ConfigGlobalPacs != null)
            {
               GlobalPacsUpdater.BackupFile(MainForm.ConfigGlobalPacs.FilePath);
            }

            List<ConnectionStringSettings> connectionStringSettingList = new List<ConnectionStringSettings>();

            ConnectionStringSettings storageServerConnectionStringSetting =
               new ConnectionStringSettings(_commandLineOptions.DatabaseName, _commandLineOptions.GetConnectionString(), ConnectionProviders.SqlServerProvider.Name);
            connectionStringSettingList.Add(storageServerConnectionStringSetting);
            MainForm.StoreConnectionStringsCommandLine(connectionStringSettingList);

            MainForm.RegisterConfigSectionsCommandLine();

            if (_commandLineOptions.DatabaseType == DatabaseTypeEnum.Existing)
            {
               // Get DICOM listening service name from options database table
               // Update globalpacs.config to include service name for each DataAccesLayer entry

               AddDicomServiceNameToGlobalPacsConfig();
            }

            if (_commandLineOptions.DatabaseType == DatabaseTypeEnum.New)
            {
               //if (databaseOptions1.WorklistDbSelected)
               //{
               //   // FillModalityWorklistDummyData ( ) ;
               //}

               //if (databaseOptions1.UserManagementDbSelected)
               //{
               //   InsertWorkstationUser();
               //}
            }

            if (_commandLineOptions.DatabaseType == DatabaseTypeEnum.New)
            {
               bool storageServerDbSelected = true;
               if (storageServerDbSelected)
               {
                  MainForm.InsertStorageServerUser(_commandLineOptions.DicomServerUsername, _commandLineOptions.DicomServerPassword);

                  if (Program.IsToolkitDemo == true)
                  {
                     // Program.AddDefaultImages(MainForm.ConfigGlobalPacs);
                  }
                  MainForm.AddDefaultIodClasses();
               }
            }

         }
         catch (Exception ex)
         {
            success = false;
            MyShowError(ex.Message);
         }
         return success;
      }

      /// <summary>
      /// The main entry point for the application.
      /// </summary>
      [STAThread]
      static void Main(string[] args)
      {
         try
         {
            if (IsWindowsVista() && !IsAdmin())
            {
               RestartElevated();
               return;
            }

#if (LEADTOOLS_V20_OR_LATER)
            if (DemosGlobal.IsDotNet45OrLaterInstalled() == false)
            {
               MessageBox.Show("To run this application, you must first install Microsoft .NET Framework 4.5 or later.",
                  "Microsoft .NET Framework 4.5 or later Required",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Exclamation);
               return;
            }
#endif

            Messager.Caption = ConfigurationData.ApplicationName;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DicomEngine.Startup();
            if (!InitializeLicense())
            {
               return;
            }

            bool success = false;
            if (args.Length > 0)
            {
               // Redirect console output to parent process -- must be before any calls to Console.WriteLine()
               AttachConsole(ATTACH_PARENT_PROCESS);
               Console.WriteLine();

               if (ReadCommandLine(args))
               {
                  if (_commandLineOptions.IsValid() == false)
                  {
                     MessageBox.Show("Invalid CommandLine Options");
                     return;
                  }
                  success = SilentConfigure();
               }
            }

            else
            {
               _mainForm = GetMainForm();
               if (_mainForm == null)
                  return;
               Application.Run(_mainForm);
            }


         }
         catch (Exception exception)
         {
            Program.MyShowError("An error has occured. The program will be terminated.\n" + exception.Message);
         }
         finally
         {
            DicomEngine.Shutdown();
            Program.MyShowInformation("Finished.");
         }
      }

      public static Form _mainForm = null;

      public static Icon GetAppIcon()
      {
         Icon icon;

         try
         {
            icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
         }
         catch
         {
            icon = null;
         }
         return icon;
      }

      private static bool IsWindowsVista()
      {
         OperatingSystem system = Environment.OSVersion;

         if (system.Platform == PlatformID.Win32NT && system.Version.Major >= 6)
            return true;

         return false;
      }

      private static bool IsAdmin()
      {
         WindowsIdentity id = WindowsIdentity.GetCurrent();
         WindowsPrincipal p = new WindowsPrincipal(id);

         return p.IsInRole(WindowsBuiltInRole.Administrator);
      }

      private static void RestartElevated()
      {
         ProcessStartInfo startInfo = new ProcessStartInfo();

         startInfo.UseShellExecute = true;
         startInfo.WorkingDirectory = Environment.CurrentDirectory;
         startInfo.FileName = Application.ExecutablePath;
         startInfo.Verb = "runas";
         try
         {
            Process p = Process.Start(startInfo);
         }
         catch (System.ComponentModel.Win32Exception)
         {
            return;
         }
      }

      public static void MyShowError(string message)
      {
         if (Program._mainForm == null)
         {
            Console.WriteLine(message);
         }
         else
         {
            Messager.ShowError(Program._mainForm, message);
         }
      }

      public static void MyShowInformation(string message)
      {
         if (Program._mainForm == null)
         {
            Console.WriteLine(message);
         }
         else
         {
            Messager.ShowInformation(Program._mainForm, message);
         }
      }
   }

   public class CommandLineOptions
   {
      public CommandLineOptions()
      {
         DatabaseType = DatabaseTypeEnum.None;
         SqlAuthenticationType = SqlAuthenticationTypeEnum.None;
      }

      public string SqlDataSource { get; set; }
      public SqlAuthenticationTypeEnum SqlAuthenticationType { get; set; }
      public string SqlUsername { get; set; }
      public string SqlPassword { get; set; }
      public string DatabaseName { get; set; }
      public DatabaseTypeEnum DatabaseType { get; set; }

      public string DicomServerUsername { get; set; }
      public string DicomServerPassword { get; set; }

      public bool IsValid()
      {
         if (string.IsNullOrWhiteSpace(SqlDataSource))
            return false;

         if (string.IsNullOrWhiteSpace(DatabaseName))
            return false;

         if (SqlAuthenticationType == SqlAuthenticationTypeEnum.None)
            return false;

         if (DatabaseType == DatabaseTypeEnum.None)
            return false;

         if (SqlAuthenticationType == SqlAuthenticationTypeEnum.Sql)
         {
            if (string.IsNullOrWhiteSpace(SqlUsername))
               return false;
         }

         if (string.IsNullOrWhiteSpace(DicomServerUsername))
            return false;

         if (string.IsNullOrWhiteSpace(DicomServerPassword))
            return false;

         return true;
      }

      public void SetAuthenticationType(string s)
      {
         string lower = s.ToLower();
         SqlAuthenticationType = SqlAuthenticationTypeEnum.None;
         if (lower.Contains("sql"))
         {
            SqlAuthenticationType = SqlAuthenticationTypeEnum.Sql;
         }
         else if (lower.Contains("windows"))
         {
            SqlAuthenticationType = SqlAuthenticationTypeEnum.Windows;
         }
      }

      public void SetDatabaseType(string s)
      {
         string lower = s.ToLower();
         DatabaseType = DatabaseTypeEnum.None;
         if (lower.Contains("new"))
         {
            DatabaseType = DatabaseTypeEnum.New;
         }
         else if (lower.Contains("existing"))
         {
            DatabaseType = DatabaseTypeEnum.Existing;
         }
      }

      public string GetConnectionString()
      {
         SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

         if (SqlAuthenticationType == SqlAuthenticationTypeEnum.Sql)
         {
            // builder = new SqlConnectionStringBuilder(_connectionStringSqlAuthentication);
            builder.IntegratedSecurity = false;
            builder.UserID = SqlUsername;
            builder.Password = SqlPassword;
            // builder.Pooling = true;
         }
         else
         {
            builder.IntegratedSecurity = true;
            // builder.Pooling = false;
         }

         builder.DataSource = SqlDataSource;
         builder.InitialCatalog = DatabaseName;
         builder.Pooling = true;

         return builder.ConnectionString;
      }
   }

   public enum SqlAuthenticationTypeEnum
   {
      None = 0,
      Sql = 1,
      Windows = 2
   }

   public enum DatabaseTypeEnum
   {
      None = 0,
      New = 1,
      Existing = 2,
   }
}
