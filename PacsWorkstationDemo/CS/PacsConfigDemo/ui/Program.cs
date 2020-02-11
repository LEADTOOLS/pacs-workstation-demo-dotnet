// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using System.IO;
using System.Net;
using Leadtools.Dicom.Server.Admin;
using Leadtools.Dicom.AddIn.Common;
using System.Net.Sockets;
using System.Reflection;
using Leadtools.Dicom;
using Leadtools;
using Leadtools.Demos;
using System.ServiceProcess;
using System.Windows.Forms;
using System.Security.Principal;
using System.Diagnostics;
using Leadtools.Medical.Storage.DataAccessLayer.Configuration;
using Leadtools.Medical.Workstation.DataAccessLayer.Configuration;
using Leadtools.Medical.UserManagementDataAccessLayer.Configuration;
using Leadtools.Medical.Logging.DataAccessLayer.Configuration;
using Leadtools.Medical.DataAccessLayer.Configuration;
using System.Configuration;
using Leadtools.Medical.Worklist.DataAccessLayer.Configuration;
using Leadtools.DicomDemos;
using System.Collections;
using Microsoft.Win32;
using Leadtools.Medical.Media.DataAccessLayer.Configuration;
using Leadtools.Medical.AeManagement.DataAccessLayer.Configuration;
using Leadtools.Medical.Winforms.DataAccessLayer.Configuration;
using Leadtools.Medical.Forward.DataAccessLayer.Configuration;
using Leadtools.Medical.OptionsDataAccessLayer.Configuration;
using Leadtools.Medical.PermissionsManagement.DataAccessLayer.Configuration;
using Leadtools.Demos.StorageServer.DataTypes;

namespace PACSConfigDemo
{
   static class Program
   {
      public static string _demoName = string.Empty;

      public static string _baseDir = string.Empty;
      public static bool _bUninstall = false;
      public static bool _bInitialize = false;

      private const string _sNewlineTab = "\r\n\t";

      const string sHelpInstructions =
            "Command Line Options:" + _sNewlineTab +
            "/? or /help\t\tDisplays this help" + _sNewlineTab +
            "/uninstall\t\tUninstalls all LEAD DICOM services (no user-interface)" + _sNewlineTab +
            "/server_aetitle={aetitle}\tServer AE title" + _sNewlineTab +
            "/server_port={port}\tServer Port" + _sNewlineTab +
            "/client_aetitle={aetitle}\tClient AE title" + _sNewlineTab +
            "/client_port={port}\t\tClient Port";

      /// <summary>
      /// The main entry point for the application.
      /// </summary>
      [STAThread]
      static int Main(string[] args)
      {
         if (!DicomDemoSettingsManager.Is64Process())
            _demoName = "LEADTOOLS PACS Configuration Demo  (32 bit)";
         else
            _demoName = "LEADTOOLS PACS Configuration Demo  (64 bit)";

         if (DemosGlobal.MustRestartElevated())
         {
            DemosGlobal.TryRestartElevated(args);
            return 0;
         }

         ReadCommandLine(args);
         MySettings mySettings = new MySettings();
         if (_bInitialize)
         {
#if !FOR_DOTNET4
            if (false == DemosGlobal.IsDotNet35Installed())
            {
               return 0;
            }
#endif

            MyUtils.RemoveConfigurationFiles();
            MyUtils.RemoveGlobalPacsConfig();
            mySettings.Save();
            return 0;
         }

         bool showUI = !_bUninstall && !_bInitialize;

#if !FOR_DOTNET4
 
         bool dotNet35Installed = DemosGlobal.IsDotNet35Installed();
         if (showUI && !dotNet35Installed)
         {
            Messager.ShowWarning(null, ".NET Framework 3.5 could not be found on this machine.\n\nPlease install the .NET Framework 3.5 runtime and try again. This program will now exit.");
         }
         if (!dotNet35Installed)
         {
            return 0;
         }
#endif

         mySettings.Load();

#if LEADTOOLS_V19_OR_LATER
         if (showUI)
            if (!Support.SetLicense())
               return -1;
#endif

         // If calling with the /uninstall flag, do not display the nag message
         if (_bUninstall == false)
         {
            if (RasterSupport.KernelExpired)
               return -1;
         }

#if LEADTOOLS_V175_OR_LATER
         if (showUI)
         {
            if (RasterSupport.IsLocked(RasterSupportType.DicomCommunication))
            {
               MessageBox.Show(String.Format("{0} Support is locked!", RasterSupportType.DicomCommunication.ToString()), "Warning");
               return -1;
            }
         }
#else
         
         if (RasterSupport.IsLocked(RasterSupportType.MedicalNet))
         {
            MessageBox.Show(String.Format("{0} Support is locked!", RasterSupportType.MedicalNet.ToString()), "Warning");
            return -1;
         }

         if (RasterSupport.IsLocked(RasterSupportType.MedicalServer))
         {
            MessageBox.Show(String.Format("{0} Support is locked!", RasterSupportType.MedicalServer.ToString()), "Warning");
            return -1;
         }
#endif

         //_admin = new ServiceAdministrator(_baseDir);
         //_admin.Unlock(Support.MedicalServerKey);

         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);

#if (LEADTOOLS_V20_OR_LATER)
         if (DemosGlobal.IsDotNet45OrLaterInstalled() == false)
         {
            MessageBox.Show("To run this application, you must first install Microsoft .NET Framework 4.5 or later.",
               "Microsoft .NET Framework 4.5 or later Required",
               MessageBoxButtons.OK,
               MessageBoxIcon.Exclamation);
            return -1;
         }
#endif

         //Utils.EngineStartup();
         DicomEngine.Startup();

         if (_bUninstall)
         {
            try
            {
               using (ServiceAdministrator serviceAdmin = CreateServiceAdministrator())
               {
                  MyUtils.UninstallAllDicomServersSilent(serviceAdmin);
                  MyUtils.RemoveConfigurationFiles();
               }
            }
            catch (Exception)
            {
            }
         }
         else
         {
            string missingDbComponents;
            DialogResult result = DialogResult.Yes;
            Messager.Caption = _demoName;
            string platform = "32-bit";
            if (DicomDemoSettingsManager.Is64Process())
               platform = "64-bit";
               
            string []productsToCheck = new string[]{DicomDemoSettingsManager.ProductNameDemoServer, DicomDemoSettingsManager.ProductNameWorkstation, DicomDemoSettingsManager.ProductNameStorageServer};

            bool isDbConfigured = GlobalPacsUpdater.IsDbComponentsConfigured(productsToCheck, out missingDbComponents);
            if (!isDbConfigured) // databases not configured
            {
               string message = "The following databases are not configured:\n\n{0}\nRun the {1} CSPacsDatabaseConfigurationDemo to configure the missing databases then run this demo again.\n\nDo you want to run the {2} CSPacsDatabaseConfigurationDemo wizard now?";
               message = string.Format(message, missingDbComponents, platform, platform);

               result = Messager.ShowQuestion(null, message, MessageBoxButtons.YesNo);
               if (DialogResult.Yes == result)
               {
                  RunDatabaseConfigurationDemo();
               }
            }

            mySettings._settings.FirstRun = false;
            mySettings.Save();

            // Add event handler for handling UI thread exceptions to the event
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);

            // Set the unhandled exception mode to force all Windows Forms errors to go through our handler.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // Add the event handler for handling non-UI thread exceptions to the event.
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            try
            {
               Application.Run(new MainForm());
            }
            catch (FileNotFoundException ex)
            {
               MessageBox.Show("File not found exception.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
         }
         DicomEngine.Shutdown();
         return 0;
      }

      public static RegistryKey OpenSoftwareKey(string keyName)
      {
         RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\" + keyName);
         if (key == null)
         {
            key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\" + keyName);
            if (key == null)
               key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\" + keyName);
         }

         return key;
      }

      static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
      {
         try
         {
            Exception ex = (Exception)e.ExceptionObject;
            string sMsg = "An CurrentDomain_UnhandledException has occurred in {0}.\n\n";
            string errorMsg = string.Format(sMsg, Assembly.GetExecutingAssembly().Location);

            // Note that the sourceName must be less than or equal to 16 characters
            // If it is longer, it just fails to write!
            string sourceName = "PACSConfigDemo";
            EventLog.WriteEntry(sourceName, errorMsg + ex.Message + "\n\nStack Trace:\n" + ex.StackTrace + "\n\nSource:\n" + ex.Source + "\n\nTargetSite:\n" + ex.TargetSite);

         }
         catch (Exception ex)
         {
            try
            {
               MessageBox.Show("Fatal Non-UI Error",
                   "Fatal Non-UI Error. Could not write the error to the event log. Reason: "
                   + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
               Application.Exit();
            }
         }
      }

      static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
      {

         if (e.Exception is FileNotFoundException)
         {
            MessageBox.Show("File not found exception.\n" + e.Exception, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
         }
         try
         {
            Exception ex = e.Exception;
            string sMsg = "An Application_ThreadException has occurred in {0}.\n\n";
            string errorMsg = string.Format(sMsg, Assembly.GetExecutingAssembly().Location);

            // This exception does not prevent the application from terminating.
            // Log this to the event log.
            // Note that the sourceName must be less than or equal to 16 characters
            // If it is longer, it just fails to write!
            string sourceName = "PACSConfigDemo";
            EventLog.WriteEntry(sourceName, errorMsg + ex.Message + "\n\nStack Trace:\n" + ex.StackTrace + "\n\nSource:\n" + ex.Source + "\n\nTargetSite:\n" + ex.TargetSite);

         }
         catch (Exception ex)
         {
            try
            {
               MessageBox.Show("Fatal Non-UI Error",
                   "Fatal Non-UI Error. Could not write the error to the event log. Reason: "
                   + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
               Application.Exit();
            }
         }
      }

      private static void RunDatabaseConfigurationDemo()
      {
            string dbManagerFileName = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "CSPacsDatabaseConfigurationDemo_Original.exe");

            if (!File.Exists(dbManagerFileName))
            {
               dbManagerFileName = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "CSPacsDatabaseConfigurationDemo.exe");
            }

            if (File.Exists(dbManagerFileName))
            {
               Process dbConfigProcess = new Process();
               dbConfigProcess.StartInfo.FileName = dbManagerFileName;

               dbConfigProcess.Start();
               dbConfigProcess.WaitForExit();
            }
            else
            {
               Messager.ShowError(null,
                                    "Could not find the CSPacsDatabaseConfigurationDemo wizard");
            }
            // return 0 ;
      }

      static public string GetWorkingDirectory()
      {
         string executableName = Application.ExecutablePath;
         FileInfo executableFileInfo = new FileInfo(executableName);
         return executableFileInfo.DirectoryName.ToLower();
      }

      // Here are two sample command lines:
      //       /configure /server_aetitle=STORAGE_SCU /server_ip=10.1.1.167 /server_port=104 /defaults
      //       /configure /server_aetitle=test_server_ae /server_ip=10.1.1.123 /server_port=123 /client_aetitle=test_client_ae /client_ip=test_client_ip /client_port=456 /defaults
      static void ReadCommandLine(string[] args)
      {
         const string sQuestion = "/?";
         const string sHelp = "/help";
         const string sUninstall = "/uninstall";
         const string sBase = "/base=";
         const string sServerAeTitle = "/server_aetitle=";
         const string sServerPort = "/server_port=";
         const string sClientAeTitle = "/client_aetitle=";
         const string sClientPort = "/client_port=";
         const string sInitialize = "/initialize";

         MySettings mySettings = new MySettings();
         _bUninstall = false;
         _baseDir = Path.GetFullPath(GetWorkingDirectory()).ToLower();
         if (!_baseDir.EndsWith("\\"))
            _baseDir += "\\";

         mySettings.Load();

         foreach (string s in args)
         {
            string sVal = string.Empty;

            if ((string.Compare(sHelp, s, true) == 0) || (string.Compare(sQuestion, s, true) == 0))
            {
               MessageBox.Show(sHelpInstructions, "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
               return;
            }
            else if (s.StartsWith(sInitialize, true, null))
            {
               _bInitialize = true;
            }
            else if (s.StartsWith(sUninstall, true, null))
            {
               _bUninstall = true;
            }
            else if (s.StartsWith(sBase, true, null))
            {
               sVal = s.Substring(sBase.Length);
               if (Directory.Exists(sVal))
               {
                  _baseDir = Path.GetFullPath(sVal).ToLower();
                  if (!_baseDir.EndsWith("\\"))
                     _baseDir += "\\";
               }
            }
            else if (s.StartsWith(sServerAeTitle, true, null))
            {
               sVal = s.Substring(sServerAeTitle.Length);
               mySettings._settings.ServerAE = sVal;
            }
            else if (s.StartsWith(sServerPort, true, null))
            {
               sVal = s.Substring(sServerPort.Length);
               mySettings._settings.ServerPort = 104;
               try
               {
                  mySettings._settings.ServerPort = Convert.ToInt32(sVal);
               }
               catch (Exception)
               {
               }
            }
            else if (s.StartsWith(sClientAeTitle, true, null))
            {
               sVal = s.Substring(sClientAeTitle.Length);
               mySettings._settings.ClientAE = sVal;
            }
            else if (s.StartsWith(sClientPort, true, null))
            {
               sVal = s.Substring(sClientPort.Length);
               mySettings._settings.ClientPort = 1000;
               try
               {
                  mySettings._settings.ClientPort = Convert.ToInt32(sVal);
               }
               catch (Exception)
               {
               }
            }
         }
         mySettings.Save();
      }

      public static ServiceAdministrator CreateServiceAdministrator()
      {
         ServiceAdministrator administrator = administrator = new ServiceAdministrator(_baseDir);
#if LEADTOOLS_V175_OR_LATER
         administrator.Initialize();
#else
         administrator.Unlock(Support.MedicalServerKey);
#endif
         return administrator;
      }
   }
}
