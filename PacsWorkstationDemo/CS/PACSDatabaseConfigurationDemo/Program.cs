// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Principal;
using System.Diagnostics;
using MedicalWorkstationConfigurationDemo.UI ;
using Leadtools.Demos;
using Leadtools.Dicom;
using System.Drawing;
using System.Reflection;

namespace MedicalWorkstationConfigurationDemo
{
   static partial class Program
   {
      /// <summary>
        /// The main entry point for the application.
        /// </summary>
      [STAThread]
      static void Main()
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
            Form mainForm = GetMainForm();
            if (mainForm == null)
               return;
            Application.Run(mainForm);
         }
         catch (Exception exception)
         {
            MessageBox.Show("An error has occured. The program will be terminated.\n" + exception.Message,
                              Messager.Caption,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
         }
         finally
         {
            DicomEngine.Shutdown();
         }
      }

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
   }
}
