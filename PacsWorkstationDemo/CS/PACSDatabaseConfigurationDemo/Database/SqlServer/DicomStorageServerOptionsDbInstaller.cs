// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Leadtools.Demos.Sql;
using CSPacsDatabaseConfigurationDemo.UI;

namespace MedicalWorkstationConfigurationDemo
{
   class DicomStorageServerOptionsSqlInstaller
   {
      public static void InstallDatabase(string connectionString)
      {
         string message;
         SqlConnectionStringBuilder csB = new SqlConnectionStringBuilder(connectionString);
         if (SqlUtilities.DatabaseExist(csB))
         {
            message = string.Format("--- Deleting Previous Database [{0}]...", csB.InitialCatalog);
            MyLogger.ShowMessage(message);
            SqlUtilities.DeleteDbIfExists(csB, 1000, MyCallbacks.MyDatabaseCallback);
         }

         message = string.Format("--- Creating New Database [{0}]...", csB.InitialCatalog);
         MyLogger.ShowMessage(message);

         SqlUtilities.CreateDatabase(csB, 1000, MyCallbacks.MyDatabaseCallback);

         CreateSqlTables(connectionString);
      }


      private static void CreateSqlTables(string connectionString)
      {
         SqlConnection connection ;
         SqlTransaction transaction ;
         
         
         using ( connection = new SqlConnection ( connectionString )  )
         {
            connection.Open ( ) ;
            
            transaction = connection.BeginTransaction ( ) ;
            
            try 
            {
               SqlCommand command ;
               
               
               //Create Tables
               command = new SqlCommand ( DatabaseScripts.StorageServerOptionsDatabase, connection ) ;
               command.Transaction = transaction ;
               command.ExecuteNonQuery ( ) ;
               
               // version
               VersionSqlInstaller.CreateVersionTable(connection, transaction);
#if (LEADTOOLS_V19_OR_LATER)
               VersionSqlInstaller.InsertVersionTable(connection, transaction, ConfigurationData.DataAccessLayerVersionNames.Options, 1, 2);
#else
               VersionSqlInstaller.InsertVersionTable(connection, transaction, ConfigurationData.DataAccessLayerVersionNames.Options, 1, 0);
#endif // #if (LEADTOOLS_V19_OR_LATER)
               
               command.Dispose ( ) ;
               
               transaction.Commit ( ) ;
            }
            catch ( Exception ) 
            {
               transaction.Rollback ( ) ;
               
               throw ;
            }
            finally
            {
               transaction.Dispose ( ) ;
            }
         }
      }
      
      public abstract class DatabaseScripts
      {
         
         public const string StorageServerOptionsDatabase = @"" +
         @"
CREATE TABLE [Options] (
  [Key] nvarchar(100) NOT NULL
, [Value] nvarchar(max) NULL
);

ALTER TABLE [Options] ADD CONSTRAINT [PK_Options] PRIMARY KEY ([Key]);

CREATE UNIQUE INDEX [UQ__Options__0000000000000019] ON [Options] ([Key] ASC);

" + 
"";
      }
   }
}
