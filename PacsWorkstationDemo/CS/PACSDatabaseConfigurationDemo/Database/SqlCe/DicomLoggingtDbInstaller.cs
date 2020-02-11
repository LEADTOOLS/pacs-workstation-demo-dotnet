// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Reflection;
using System.IO;
using System.Data.SqlServerCe;

namespace MedicalWorkstationConfigurationDemo
{
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

   class DicomLoggingSqlCeInstaller
   {
      public static void InstallDatabase ( string connectionString ) 
      {
         Stream   stream ;
         
         stream =  Assembly.GetExecutingAssembly ( ).GetManifestResourceStream ( Constants.LoggingDb ) ;
         
         if ( null != stream && stream.Length > 0 ) 
         {
            string sqlCeDatabaseName ;
            byte[]     data ;
         
         
            sqlCeDatabaseName = connectionString.Replace ( "Data Source=", "" ) ;
           
            using ( FileStream fs = new FileStream ( sqlCeDatabaseName, FileMode.Create ) )
            {
               data = new byte[stream.Length];

               stream.Read ( data, 0, (int)stream.Length ) ;
              
               fs.Write ( data, 0, (int) stream.Length ) ;
              
               fs.Close ( ) ;
            }

            SqlCeUtils.UpgradeDatabase(connectionString);
         }
         else
         {
            throw new InvalidOperationException ( "Database not found." ) ;
         }
      }
      
      private class Constants
      {
         public const string LoggingDb = "CSPacsDatabaseConfigurationDemo.Database.SqlCe.Logging.sdf" ;
      }
   }
}
