// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;

namespace MedicalWorkstationConfigurationDemo
{
   class DicomStorageServerOptionsSqlCeInstaller
   {
      public static void InstallDatabase(string connectionString)
      {
         Stream stream;

         stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(Constants.StorageServerOptionsDb);

         if (null != stream && stream.Length > 0)
         {
            string sqlCeDatabaseName;
            byte[] data;


            sqlCeDatabaseName = connectionString.Replace("Data Source=", "");

            using ( FileStream fs = new FileStream(sqlCeDatabaseName, FileMode.Create) ) 
            {
               data = new byte[stream.Length];

               stream.Read(data, 0, (int)stream.Length);

               fs.Write(data, 0, (int)stream.Length);

               fs.Close();
            }
            SqlCeUtils.UpgradeDatabase(connectionString);
         }
         else
         {
            throw new InvalidOperationException("Database not found.");
         }
      }

      private class Constants
      {
         public const string StorageServerOptionsDb = "CSPacsDatabaseConfigurationDemo.Database.SqlCe.StorageServerOptions.sdf";
      }
   }
}
