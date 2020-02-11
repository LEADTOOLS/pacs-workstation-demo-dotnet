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
using System.IO;
using Leadtools;
using System.Configuration;
using CSPacsDatabaseConfigurationDemo.UI;
using System.Data.SqlClient;
using Leadtools.Dicom.Scp.Command;
using Leadtools.Medical.Storage.DataAccessLayer;
using Leadtools.Medical.DataAccessLayer;
using Leadtools.Medical.Storage.DataAccessLayer.Configuration;
using Leadtools.DicomDemos;

namespace MedicalWorkstationConfigurationDemo
{
   static partial class Program
   {
      private static bool InitializeLicense()
      {
#if (LEADTOOLS_V19_OR_LATER)
         bool ret = Support.SetLicense();
         return ret;
#else
         return true;
#endif
      }

      public static void ProcessConfiguration(Configuration config)
      {
      }

      public static bool IsToolkitDemo
      {
         get { return true; }
      }

      public static string[] storageServerDefaultPermissionsList = {
            "Admin",
            "PatientUpdaterEdit",
            "PatientUpdaterDelete",
            "PatientUpdaterAdmin", 
            "CanDeleteFromDatabase",
            "CanChangeServerSettings",
            "CanEmptyDatabase",
            };

      public static Form GetMainForm()
      {
         bool bShowChangeDefaultSqlServerButton = true;
         MainForm2 mainForm2 = null;
         SqlConnectionStringBuilder defaultConnectionStringBuilder;

         defaultConnectionStringBuilder = MainForm2.GetDefaultLocalSqlConnection();

         // defaultConnectionStringBuilder = null; // remove this line

         if (defaultConnectionStringBuilder != null)
         {
            bShowChangeDefaultSqlServerButton = false;
         }
         else
         {
            defaultConnectionStringBuilder = MainForm2.GetDefaultNetworkSqlConnection();
         }


         if (MainForm2.DefaultConnectionDialogResult == DialogResult.OK)
         {
            mainForm2 = new MainForm2() { Icon = GetAppIcon() };
            mainForm2.DefaultSqlConnectionStringBuilder = defaultConnectionStringBuilder;
            mainForm2.ShowChangeDefaultSqlServerButton(bShowChangeDefaultSqlServerButton);
         }
         return mainForm2;
      }

      static bool _shouldEnumerateSqlServers = ConfigurationData.ShouldEnumerateSqlServersFromConfiguration();
      public static bool ShouldEnumerateSqlServers
      {
         set { _shouldEnumerateSqlServers = value; }
         get { return _shouldEnumerateSqlServers; }
      }

      private static string BuildDefaultImageFormatString(int padding)
      {
         if (padding < 2)
         {
            return "CSPacsDatabaseConfigurationDemo.Resources.{0}{1}.dcm";
         }

         string padString = string.Empty;
         for (int p = 0; p < padding; p++)
         {
            padString = padString + "0";
         }

         string formatString = string.Format("CSPacsDatabaseConfigurationDemo.Resources.{{0}}{{1:{0}}}.dcm", padString);
         return formatString;
      }

      public static void ImageCountReset()
      {
         _currentImageCount = 0;
      }

      public static void ImageCountUpdate()
      {
         _currentImageCount++;

         string message = string.Format("Adding {0} of {1}", _currentImageCount, TotalImageCount);
         MyLogger.ShowWaiting(message);
      }

      public static void InsertDefaultImages(string prefix, int max, Configuration configuration)
      {
         InsertDefaultImages(prefix, max, 0, configuration);
      }

      public static void InsertDefaultImages(string prefix, int max, int padding, Configuration configuration)
      {
          MedicalWorkstationConfigurationDemo.UI.MainForm.StoreClientSessionProxy proxy = null;
          InstanceCStoreCommand cmd = null;
          IStorageDataAccessAgent agent = DataAccessFactory.GetInstance(new StorageDataAccessConfigurationView(configuration, DicomDemoSettingsManager.ProductNameStorageServer, null)).CreateDataAccessAgent<IStorageDataAccessAgent>();

          string formatString = BuildDefaultImageFormatString(padding);
          for (int i = 1; i < max+1; i++)
          {
              using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(string.Format(formatString, prefix, i)))
              {
                  using (DicomDataSet ds = new DicomDataSet())
                  {
                      ds.Load(stream, DicomDataSetLoadFlags.None);

                      proxy = new MedicalWorkstationConfigurationDemo.UI.MainForm.StoreClientSessionProxy();
                      cmd = new InstanceCStoreCommand(proxy, ds, agent);

                      proxy.AffectedSOPInstance = ds.GetValue<string>(DicomTag.SOPInstanceUID, string.Empty);
                      proxy.AbstractClass = ds.GetValue<string>(DicomTag.SOPClassUID, string.Empty);

                      ds.InsertElementAndSetValue(DicomTag.MediaStorageSOPInstanceUID, proxy.AffectedSOPInstance);
                      cmd.Execute();
                      ImageCountUpdate();
               }
            }
          }
      }

      const int ImageCountResources = 2;
      const int ImageCountMG = 4;
      const int ImageCountCR = 2;
      const int ImageCountFMX = 18;
      const int ImageCountMRI = 46;
      const int TotalImageCount = ImageCountResources + ImageCountMG + ImageCountCR + ImageCountFMX + ImageCountMRI;

      private static int _currentImageCount = 0;

      public static void AddDefaultImages(Configuration configGlobalPacs)
      {
         ImageCountReset();

         MainForm.StoreClientSessionProxy proxy = null;
         InstanceCStoreCommand cmd = null;
         IStorageDataAccessAgent agent = DataAccessFactory.GetInstance(new StorageDataAccessConfigurationView(configGlobalPacs, DicomDemoSettingsManager.ProductNameStorageServer, null)).CreateDataAccessAgent<IStorageDataAccessAgent>();

         for (int i = 1; i < ImageCountResources+1; i++)
         {
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(string.Format("CSPacsDatabaseConfigurationDemo.Resources.{0}.dcm", i)))
            {
               using (DicomDataSet ds = new DicomDataSet())
               {
                  ds.Load(stream, DicomDataSetLoadFlags.None);

                  proxy = new MainForm.StoreClientSessionProxy();
                  cmd = new InstanceCStoreCommand(proxy, ds, agent);

                  proxy.AffectedSOPInstance = ds.GetValue<string>(DicomTag.SOPInstanceUID, string.Empty);
                  proxy.AbstractClass = ds.GetValue<string>(DicomTag.SOPClassUID, string.Empty);

                  ds.InsertElementAndSetValue(DicomTag.MediaStorageSOPInstanceUID, proxy.AffectedSOPInstance);
                  cmd.Execute();

                  ImageCountUpdate();
               }
            }
         }
#if(LEADTOOLS_V19_OR_LATER)
        InsertDefaultImages("mg", ImageCountMG, configGlobalPacs);
        InsertDefaultImages("cr", ImageCountCR, configGlobalPacs);
        InsertDefaultImages("FMX18.de", ImageCountFMX, 2, configGlobalPacs);
#endif

#if (LEADTOOLS_V20_OR_LATER)
         InsertDefaultImages("MRI.mri_", ImageCountMRI, 2, configGlobalPacs);
#endif
      }
   }
}
