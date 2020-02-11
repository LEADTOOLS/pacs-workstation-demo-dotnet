// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.IO;
using Microsoft.Win32;
using System.Drawing;
using System.Windows.Forms;

using Leadtools;
using System.Diagnostics;
using System.Security.Principal;
using System.Reflection;
using System.Threading;
using System.Globalization;

namespace Leadtools.Demos
{
   internal enum GlobalizationLanguage
   {
      English,
      Japanese
   }

   internal static class DemosGlobalization
   {
      private static string _strAdminPrivilege;
      private static string _strDebuggerWarning;
      private static string _strWarning;
      private static string _strSelectOpFolder;

      static DemosGlobalization()
      {
         if (GetCurrentThreadLanguage() == GlobalizationLanguage.Japanese)
         {
            _strAdminPrivilege = "This application needs to be run with administrator privileges";
            _strDebuggerWarning = "This will end your debugging session";
            _strWarning = "Warning";
            _strSelectOpFolder = "Select Output Folder";
         }
         else
         {
            _strAdminPrivilege = "This application needs to be run with administrator privileges";
            _strDebuggerWarning = "This will end your debugging session";
            _strWarning = "Warning";
            _strSelectOpFolder = "Select Output Folder";
         }
      }

      public static string GetResxString(Type type, string stringName)
      {
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(type);
         return resources.GetString(stringName);
      }

      public static GlobalizationLanguage GetCurrentThreadLanguage()
      {
         if (Thread.CurrentThread.CurrentCulture.Name == "ja-JP")
            return GlobalizationLanguage.Japanese;
         else
            return GlobalizationLanguage.English;
      }

      public static string AdminPrivilege
      {
         get { return _strAdminPrivilege; }
      }

      public static string DebuggerWarning
      {
         get { return _strDebuggerWarning; }
      }

      public static string Warning
      {
         get { return _strWarning; }
      }

      public static string SelectOutputFolder
      {
         get { return _strSelectOpFolder; }
      }
   }
}
