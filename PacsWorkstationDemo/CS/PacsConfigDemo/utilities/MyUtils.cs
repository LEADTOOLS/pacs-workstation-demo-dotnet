// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using Leadtools.Dicom.Server.Admin;
using System.ServiceProcess;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using Leadtools.Dicom;
using System.Reflection;
using System.Runtime.InteropServices;
using PACSConfigDemo.UI;
using System.Collections;
using System.Data;
using Leadtools.DicomDemos;
using Leadtools.Demos.StorageServer.DataTypes;
using System.Data.SqlServerCe;

namespace PACSConfigDemo
{
   static class MyUtils
   {
      //static string _baseDir = string.Empty;
      public static AeTitles _aeTitles = new AeTitles();
      public static ArrayList _clientList = new ArrayList();

      static public string GetDefaultIp()
      {
         ManagementObjectSearcher query = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = 'TRUE'");
         ManagementObjectCollection queryCollection = query.Get();

         foreach (ManagementObject mo in queryCollection)
         {
            if (queryCollection.Count > 0)
            {
               string[] addresses = (string[])mo["IPAddress"];

               foreach (string ip in addresses)
               {
                  if (!ip.Contains(":") && (ip != "0.0.0.0"))
                     return ip;
               }
            }
         }
         return string.Empty;
      }

      static private bool StopOneDicomServer(DicomService service)
      {
         try
         {
            bool stopped = false;
            if (service != null)
            {
               //if (service.Status == ServiceControllerStatus.Running || service.Status == ServiceControllerStatus.Paused)
               if (service.Status != ServiceControllerStatus.Stopped)
               {
                  //DateTime s = DateTime.Now;
                  //DateTime e = DateTime.Now + TimeSpan.FromSeconds(60);
                  stopped = true;


                  try
                  {
                     service.Stop();
                     service.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(15));
                  }
                  catch (Exception)
                  {
                     stopped = false;
                  }

                  //while (service.Status != ServiceControllerStatus.Stopped)
                  //{
                  //   Application.DoEvents();
                  //   if (DateTime.Now > e)
                  //   {
                  //      //ShowError("Failed to stop DICOM service.");
                  //      stopped = false;
                  //      break;
                  //   }
                  //}

                  //if (!stopped)
                  //{
                  //   //ShowError("DICOM Service not uninstalled: Service Still Running");
                  //}
               }
               else
               {
                  stopped = true;
               }
            }
            return stopped;
         }
         catch (Exception e)
         {
            System.Diagnostics.Debug.Assert(false, e.ToString());

            return false;
         }
      }

      // Just deleting files, not the directories
      static public void DeleteDirectory(string target_dir)
      {
         // Changed this to not delete the service directory
         // On some OS, deleting the server directory leaves it in a 'state' 
         // where the folder remains but cannot be deleted until reboot!

         string[] files = Directory.GetFiles(target_dir);
         //string[] dirs = Directory.GetDirectories(target_dir);

         foreach (string file in files)
         {
            File.SetAttributes(file, FileAttributes.Normal);
            File.Delete(file);
         }

         //foreach (string dir in dirs)
         //{
         //   DeleteDirectory(dir);
         //}

         //Directory.Delete(target_dir, false);
      }

      static public bool IsPortAvailable(string ip, int port)
      {
         TcpClient client = new TcpClient();
         try
         {
            client.Connect(ip, port);
            client.Close();
            return false;
         }
         catch
         {
            return true;
         }
      }

      // Functions for creating the DICOM Server Service
      
      //static public void CopyAddIns ( string[] addins, DicomService service)
      //{
      //CopyAddIns(addins, service, "addins");
      //}



      private const uint ERROR_SHARING_VIOLATION = 0x80070020;
           
      static public void CopyAddIns (List<List<string>> addins, DicomService service, string addinsFolderName)
      {
         if (addins == null)
           return;

         string destDir = Path.Combine(service.ServiceDirectory, addinsFolderName);
         if (!Directory.Exists(destDir))
         {
            Directory.CreateDirectory(destDir);
         }

         foreach (List<string> addinPathList in addins)
         {
            bool successfulCopy = false;

            foreach (string addin in addinPathList)
            {
               FileInfo f = new FileInfo(addin);
               string newFile = Path.Combine(destDir, f.Name);

               if (!File.Exists(addin))
                  continue;

               try
               {
                  if (!File.Exists(newFile))
                  {
                     File.Copy(addin, newFile, true);
                  }
                  successfulCopy = true;
                  break;
               }
               catch (IOException e)
               {

                  int hResult = Marshal.GetHRForException(e);
                  //
                  // If the addin is being used by another process we will not report and
                  // error.
                  //
                  if (!e.Message.Contains("being used by another process") && (uint)hResult != ERROR_SHARING_VIOLATION)
                  {
                     throw e;
                  }
                  successfulCopy = true;
               }
            }

            if (successfulCopy == false)
            {
               string errorMessage;

               if (addinPathList.Count > 1)
               {
                  errorMessage = "At least one file must exist: " + string.Join(",", addinPathList.ToArray());
               }
               else
               {
                  errorMessage = "File missing: " + string.Join(",", addinPathList.ToArray());
               }
               throw new FileNotFoundException(errorMessage);
            }

         }
      }

      static public string GetResourceFullName(string resName)
      {
         string fullName = null;
         resName = resName.Replace('\\', '.');
         foreach (string str in Assembly.GetExecutingAssembly().GetManifestResourceNames())
         {
            if (str.EndsWith(resName))
            {
               fullName = str;
               break;
            }
         }
         return fullName;
      }

        public static void LoadDicomDataSetFromStream(DicomDataSet ds, Stream stream,DicomDataSetFlags flags)
        {
            byte[] data = new byte[stream.Length];
            IntPtr ptr = Marshal.AllocHGlobal(Convert.ToInt32(stream.Length));

            if(ds==null)
                return;

            stream.Read(data, 0, Convert.ToInt32(stream.Length));
            Marshal.Copy(data, 0, ptr, Convert.ToInt32(stream.Length));
            ds.Load(ptr, stream.Length, flags);                
        }

      static public bool InitializeDatabase( DicomService service, out string errorString )
      {
         bool success = true;
         errorString = string.Empty;

         string ConnectionString = "Data Source='" + service.ServiceDirectory + "Dicom.sdf'";
         string ImageDirectory = service.ServiceDirectory + @"Images\";
         DicomDataSet ds = new DicomDataSet();

         if (!File.Exists(service.ServiceDirectory + @"Dicom.sdf"))
         {
            string fullname = GetResourceFullName(@"Dicom.sdf");
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fullname);

            if (stream != null)
            {
               FileStream fs = new FileStream(service.ServiceDirectory + "Dicom.sdf", FileMode.CreateNew);
               byte[] data = new byte[stream.Length];

               stream.Read(data, 0, (int)stream.Length);
               fs.Write(data, 0, (int)stream.Length);
               fs.Close();

               SqlCeUtils.UpgradeDatabase(ConnectionString);
            }
         }

         try
         {
            for (int i = 1; i <= 5; i++)
            {
               string fullname = GetResourceFullName(string.Format("{0}.dcm", i));
               Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fullname);

               LoadDicomDataSetFromStream(ds, stream, DicomDataSetFlags.None);
               DB.Insert(DateTime.Now, ConnectionString, ImageDirectory, string.Empty, ds);
            }
         }
         catch (Exception ex)
         {
            success = false;
            errorString = ex.Message;
                throw ex;
         }
         return success;

      }

      static public void ClearAeTitles()
      {
         if (_aeTitles == null)
            _aeTitles = new AeTitles();
         _aeTitles.Clear();

         if (_clientList == null)
            _clientList = new ArrayList();
         _clientList.Clear();
      }

      static public void AddAeTitle(string clientae, string ipAddress, short clientport)
      {
         if (_clientList.Contains(clientae))
            return;

         _clientList.Add(clientae);

         AeTitles.AeTitleRow row = _aeTitles.AeTitle.NewAeTitleRow();
         row.AETitle = clientae;
         row.Address = ipAddress;
         row.Port = clientport;
         row.SecurePort = 0;
         _aeTitles.AeTitle.AddAeTitleRow(row);
      }


      static public void RemoveAeTitle(string clientae)
      {
         try
         {
            if (_clientList.Contains(clientae) == false)
               return;

            _clientList.Remove(clientae);

            DataRow rowToDelete = _aeTitles.Tables[0].Rows.Find(clientae);
            if (rowToDelete != null)
            {
               _aeTitles.Tables["AeTitle"].Rows.Remove(rowToDelete);
            }
         }
         catch (Exception)
         {
         }
      }

      static public bool WriteAeTitlesXml ( DicomService service )
      {
         try
         {
            if ( _aeTitles != null )
            {
               _aeTitles.AcceptChanges ( ) ;
               
               _aeTitles.WriteXml ( service.ServiceDirectory + "aetitles.xml" ) ;
            }
            
            return true ;
         }
         catch ( Exception )
         {
            throw ;
         }
      }


      static public void RemoveOneDicomServerFiles(string serviceDirectory)
      {
         int count = 0;
         while (count <= 3)
         {
            try
            {
               MyUtils.DeleteDirectory(serviceDirectory);
               break;
            }
            catch (Exception ex)
            {
               if (count == 3)
                  throw ex;
            }
            count++;
         }
      }

      static public bool UninstallOneDicomServer(DicomService service, bool removeFromBaseDirectoryOnly, ServiceAdministrator serviceAdmin )
      {
         bool success = false;
         try
         {
            if (service != null)
            {
               if (removeFromBaseDirectoryOnly)
               {
                  string serviceDir = Path.GetFullPath(service.ServiceDirectory ).ToLower();

                  if (!serviceDir.Contains(Program._baseDir.ToLower ( )))
                  {
                     return false;
                  }
                     
               }

               // If this fails to stop the service, we can still proceed to delete it
               // In this case, the service was marked as 'disabled', marked for deleted, and will be deleted on reboot 
               // (or if the service is stopped, it will be automatically deleted)
               StopOneDicomServer(service);

               success = true;
               string serviceDirectory = service.ServiceDirectory;
               serviceAdmin.SafeUnInstallService(service);

               // To remove the service directory and files, uncomment the code below
               if (success)
               {
                  try
                  {
                     RemoveOneDicomServerFiles(serviceDirectory);
                  }
                  catch (Exception)
                  {
                  }
               }

            }
         }
         catch (Exception e)
         {
            System.Diagnostics.Debug.Assert(false, e.ToString());
            success = false;
         }
         return success;
      }

      static public bool SafeUnInstallService(this ServiceAdministrator serviceAdmin, DicomService service)
      {
         bool success = true;
         if (service != null)
         {
            try
            {
               serviceAdmin.UnInstallService(service);
            }
            catch (Exception)
            {
               success = false;
            }
         }
         return success;
      }

      // returns true if uninstalled
      static public bool UninstallAllDicomServersSilent(ServiceAdministrator serviceAdmin)
      {
         if (serviceAdmin == null)
            return false;

         try
         {
            List<string> keys = new List<string>();

            foreach (KeyValuePair<string, DicomService> kv in serviceAdmin.Services)
            {
               keys.Add(kv.Key);
            }

            foreach (string key in keys)
            {
               DicomService service = serviceAdmin.Services[key];
               string serviceDir = service.ServiceDirectory.ToLower();

               UninstallOneDicomServer(service, false, serviceAdmin);
            }

            return true;
         }
         catch (Exception e)
         {
            System.Diagnostics.Debug.Assert(false, e.ToString());
            return false;
         }
      }

      static public void RemoveConfigurationFile(string filename)
      {
         try
         {
            string fullname = DicomDemoSettingsManager.GetSettingsFilename(filename, DicomDemoSettingsManager.InstallPlatform.win32);
            if (!string.IsNullOrEmpty(fullname))
               File.Delete(fullname);

            fullname = DicomDemoSettingsManager.GetSettingsFilename(filename, DicomDemoSettingsManager.InstallPlatform.x64);
            if (!string.IsNullOrEmpty(fullname))
               File.Delete(fullname);
         }
         catch (Exception)
         {
         }
      }

      static public void RemoveConfigurationFiles()
      {
         // Remove main client demo configurations
         string [] mainClientDemos = MainForm.GetMainClientDemos();
         foreach (string s in mainClientDemos)
         {
            RemoveConfigurationFile(s);
         }

         // Remove main store demo configurations
         string[] mainStoreDemos = MainForm.GetMainStoreDemos();
         foreach (string s in mainStoreDemos)
         {
            RemoveConfigurationFile(s);
         }

         string[] mainMwlDemos = MainForm.GetMainMwlDemos();
         foreach (string s in mainMwlDemos)
         {
            RemoveConfigurationFile(s);
         }

         string[] workstationDemos = MainForm.GetWorkstationDemos();
         foreach (string s in workstationDemos)
         {
            RemoveConfigurationFile(s);
         }

         string[] otherDemos = MainForm.GetOtherDemosWithConfigurationFiles();
         foreach (string s in otherDemos)
         {
            RemoveConfigurationFile(s);
         }

         // Remove pacs config demo configuration
         string sPacsConfig = MySettings.GetSettingsFilename();
         if (!string.IsNullOrEmpty(sPacsConfig))
         {

            //MessageBox.Show("Removing: " + sPacsConfig);
            File.Delete(sPacsConfig);

            if (sPacsConfig.Contains("32"))
               sPacsConfig = sPacsConfig.Replace("32", "64");
            else
               sPacsConfig = sPacsConfig.Replace("64", "32");
            File.Delete(sPacsConfig);
         }
      }
      
      public static string MyCombine(string d1, string d2, string d3)
      {
         string temp = Path.Combine(d1, d2);
         return Path.Combine(temp, d3);
         
      }

      public static void RemoveGlobalPacsConfig()
      {
         string globalPacsConfigPath = DicomDemoSettingsManager.GlobalPacsConfigFullFileName;
         string backupGlobalPacsConfigPath = GlobalPacsUpdater.BackupFile(globalPacsConfigPath);

         try
         {
            if (File.Exists(globalPacsConfigPath))
            {
               File.Delete(globalPacsConfigPath);
            }
         }
         catch (Exception)
         {
         }
      }

   }

   internal static class SqlCeUtils
   {
      public static void UpgradeDatabase(string connectionString)
      {
         try
         {
            using (SqlCeEngine sqlCeEngine = new SqlCeEngine(connectionString))
            {
               sqlCeEngine.Upgrade();
            }
         }
         catch (Exception ex)
         {
            if (ex.Message.ToLower().Contains("database upgrade is not required"))
            {
               // do nothing -- the database has already been upgraded
            }
            else
            {
               throw;
            }
         }
      }
   }
}
