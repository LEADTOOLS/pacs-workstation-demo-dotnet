// *************************************************************
// Copyright (c) 1991-2020 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Runtime.InteropServices;

using Leadtools;

namespace Leadtools.Demos
{
   internal static class Support
   {
      [Flags] enum MessageBoxOptions : uint{OkOnly= 0x000000, IconHand = 0x000010}
      [DllImport("user32.dll", CharSet=CharSet.Auto)] static extern int MessageBox(IntPtr hWnd, String text, String caption, MessageBoxOptions options);

      public const string MedicalServerKey = "";

      public static bool SetLicense(bool silent)
      {
         try
         {
            // TODO: Change this to use your license file and developer key */
            string licenseFilePath = "Replace this with the path to the LEADTOOLS license file";
            string developerKey = "Replace this with your developer key";
            RasterSupport.SetLicense(licenseFilePath, developerKey);
         }
         catch (Exception ex)
         {
            System.Diagnostics.Debug.Write(ex.Message);
         }

         if (RasterSupport.KernelExpired)
         {
            string[] dirs =
            {
               System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
               System.IO.Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath)
            };

            string licenseFileRelativePath = null;
            string keyFileRelativePath = null;

            foreach (string dir in dirs)
            {
               /* Try the common LIC directory */
               licenseFileRelativePath = System.IO.Path.Combine(dir, "..\\..\\..\\Support\\Common\\License\\LEADTOOLS.LIC");
               keyFileRelativePath = System.IO.Path.Combine(dir, "..\\..\\..\\Support\\Common\\License\\LEADTOOLS.LIC.key");

               if (!System.IO.File.Exists(licenseFileRelativePath))
               {
                  licenseFileRelativePath = System.IO.Path.Combine(dir, "..\\..\\Support\\Common\\License\\LEADTOOLS.LIC");
                  keyFileRelativePath = System.IO.Path.Combine(dir, "..\\..\\Support\\Common\\License\\LEADTOOLS.LIC.key");
               }

               if (!System.IO.File.Exists(licenseFileRelativePath))
               {
                  licenseFileRelativePath = System.IO.Path.Combine(dir, "..\\..\\..\\..\\..\\Support\\Common\\License\\LEADTOOLS.LIC");
                  keyFileRelativePath = System.IO.Path.Combine(dir, "..\\..\\..\\..\\..\\Support\\Common\\License\\LEADTOOLS.LIC.key");
               }

               if (!System.IO.File.Exists(licenseFileRelativePath))
               {
                  licenseFileRelativePath = System.IO.Path.Combine(dir, "..\\..\\..\\..\\..\\..\\Support\\Common\\License\\LEADTOOLS.LIC");
                  keyFileRelativePath = System.IO.Path.Combine(dir, "..\\..\\..\\..\\..\\..\\Support\\Common\\License\\LEADTOOLS.LIC.key");
               }

               if (!System.IO.File.Exists(licenseFileRelativePath))
               {
                  licenseFileRelativePath = System.IO.Path.Combine(dir, "..\\..\\..\\..\\..\\..\\..\\Support\\Common\\License\\LEADTOOLS.LIC");
                  keyFileRelativePath = System.IO.Path.Combine(dir, "..\\..\\..\\..\\..\\..\\..\\Support\\Common\\License\\LEADTOOLS.LIC.key");
               }

               if (!System.IO.File.Exists(licenseFileRelativePath))
               {
                  licenseFileRelativePath = System.IO.Path.Combine(dir, "..\\..\\..\\..\\Support\\Common\\License\\LEADTOOLS.LIC");
                  keyFileRelativePath = System.IO.Path.Combine(dir, "..\\..\\..\\..\\Support\\Common\\License\\LEADTOOLS.LIC.key");
               }

               if (System.IO.File.Exists(licenseFileRelativePath) && System.IO.File.Exists(keyFileRelativePath))
                  break;
            }

            if (System.IO.File.Exists(licenseFileRelativePath) && System.IO.File.Exists(keyFileRelativePath))
            {
               string developerKey = System.IO.File.ReadAllText(keyFileRelativePath);
               try
               {
                  RasterSupport.SetLicense(licenseFileRelativePath, developerKey);
               }
               catch (Exception ex)
               {
                  System.Diagnostics.Debug.Write(ex.Message);
               }
            }
         }

         if (RasterSupport.KernelExpired)
         {
            if (silent == false)
            {
               string msg = "Your license file is missing, invalid or expired. LEADTOOLS will not function. Please contact LEAD Sales for information on obtaining a valid license.";
               string logmsg = string.Format("*** NOTE: {0} ***{1}", msg, Environment.NewLine);
               System.Diagnostics.Debugger.Log(0, null, "*******************************************************************************" + Environment.NewLine);
               System.Diagnostics.Debugger.Log(0, null, logmsg);
               System.Diagnostics.Debugger.Log(0, null, "*******************************************************************************" + Environment.NewLine);

               try { MessageBox(IntPtr.Zero, msg, "No LEADTOOLS License", MessageBoxOptions.OkOnly | MessageBoxOptions.IconHand); } catch { }

               System.Diagnostics.Process.Start("https://www.leadtools.com/downloads/evaluation-form.asp?evallicenseonly=true");
            }

            return false;
         }
         return true;
      }

      public static bool SetLicense()
      {
         return SetLicense(false);
      }

   }
}
