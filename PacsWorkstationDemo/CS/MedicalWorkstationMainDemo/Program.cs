// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Security.Principal;
using System.Windows.Forms;
using Leadtools.Dicom;
using Leadtools.Medical.DataAccessLayer.Configuration;
using Leadtools.Medical.Workstation.UI;
using Leadtools.Demos;

namespace Leadtools.Demos.Workstation
{
   static class Program
   {
      /// <summary>
      /// The main entry point for the application.
      /// </summary>
      [STAThread]
      static void Main(string[] args)
      {
#pragma warning disable 436

#if LEADTOOLS_V19_OR_LATER
            if(!Support.SetLicense())
               return;
#else
            Support.SetLicense();
            if (RasterSupport.KernelExpired)
               return;
#endif

#pragma warning restore 436

         if (Elevation.Restart())
         {
            DemosGlobal.TryRestartElevated(args);
            return;
         }

         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);

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

         try
         {
            Leadtools.Dicom.DicomEngine.Startup ( ) ;
            Leadtools.Dicom.DicomNet.Startup ( ) ;

            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);

            WorkstationShellController.Instance.Run ( ) ;
         }
         catch ( Exception exception ) 
         {
            ViewErrorDetailsDialog detailedError ;
            
            
            detailedError = new ViewErrorDetailsDialog ( exception ) ;
            
            detailedError.ShowDialog ( ) ;
         }
         finally
         {
            Leadtools.Dicom.DicomEngine.Shutdown ( ) ;
            Leadtools.Dicom.DicomNet.Shutdown ( ) ;
         }
      }

      static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
      {
         ViewErrorDetailsDialog detailedError ;
         
         
         detailedError = new ViewErrorDetailsDialog ( e.Exception ) ;
         
         detailedError.ShowDialog ( ) ;
         
      }

      private static bool NeedUAC()
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
   
   public static class WorkstationUtils
   {
      public static bool IsDataAccessSettingsValid ( string sectionName )
      {
         ConfigurationManager.RefreshSection ( sectionName ) ;
         
         try
         {
            DataAccessSettings section = ConfigurationManager.GetSection ( sectionName ) as DataAccessSettings ;
            
            
            if ( null != section ) 
            {
               ConnectionStringSettings connectionSettings ;
               
               
               ConfigurationManager.RefreshSection ( "connectionStrings" ) ;
               connectionSettings = ConfigurationManager.ConnectionStrings  [ section.ConnectionName ] ;
               
               if ( null != connectionSettings ) 
               {
                  return true ;
               }
            }
         }
         catch ( Exception )
         {
            return false ;
         }
         
         return false ;
      }
      
      public static Icon GetApplicationIcon()
      {
         try
         {
            string iconPath ;
            
            
            iconPath = Path.Combine ( Application.StartupPath, "app.ico" ) ;
            
            if ( File.Exists ( iconPath ) )
            {
               return new Icon ( iconPath ) ;
            }
            else
            {
               return Leadtools.Demos.Workstation.Properties.Resources.MedAddon ;
            }
         }
         catch ( Exception )
         {
            return Leadtools.Demos.Workstation.Properties.Resources.MedAddon ;
         }
      }
      
      public static string GetAssociationReasonMessage ( DicomAssociateRejectReasonType reason )
      {
         if ( reason == DicomAssociateRejectReasonType.Calling ) 
         {
            return "Calling AE Title Not Recognized" ;
         }
         
         if ( reason == DicomAssociateRejectReasonType.Called ) 
         {
            return "Called AE Title Not Recognized" ;
         }
         
         return string.Empty ;
      }
   }
}
