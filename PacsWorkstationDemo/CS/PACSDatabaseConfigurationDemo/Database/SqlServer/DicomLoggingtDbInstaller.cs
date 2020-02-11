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
   class DicomLoggingSqlInstaller
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

      private static void CreateSqlTables ( string connectionString )
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
               
               
               {//Create Tables
                  command = new SqlCommand ( DatabaseScripts.LoggingDatabase, connection ) ;
                  
                  command.Transaction = transaction ;
                  
                  command.ExecuteNonQuery ( ) ;
               }
               
             VersionSqlInstaller.CreateVersionTable(connection, transaction);
             VersionSqlInstaller.InsertVersionTable(connection, transaction, ConfigurationData.DataAccessLayerVersionNames.Logging, 1, 6);
               
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
//         public const string InsertVersionTable = @"
//               INSERT INTO [dbo].[DbVersion]
//               VALUES
//               (
//                  1,
//                  0
//               )
//         ";
         
         public const string LoggingDatabase =
                                 //                                 @" CREATE TABLE [dbo].[DbVersion](
                                 //                                    [Major] [int] NOT NULL,
                                 //                                    [Minor] [int] NOT NULL,
                                 //                                  CONSTRAINT [PK_DICOMServerDbVersion] PRIMARY KEY CLUSTERED 
                                 //                                 (
                                 //                                    [Major] ASC,
                                 //                                    [Minor] ASC
                                 //                                 ) ON [PRIMARY]
                                 //                                 ) ON [PRIMARY]

                                 @" CREATE TABLE [dbo].[DICOMServerEventLog](
                                 [EventID] [bigint] IDENTITY(0,1) NOT NULL,
                                 [ServerAETitle] [varchar](16) NULL,
                                 [ServerIPAddress] [varchar](50) NULL,
                                 [ServerPort] [int] NULL,
                                 [ClientAETitle] [varchar](16) NULL,
                                 [ClientHostAddress] [varchar](50) NULL,
                                 [ClientPort] [int] NULL,
                                 [Command] [nvarchar](15) NOT NULL CONSTRAINT [DF_DICOMServerEventLog_Command]  DEFAULT ((0)),
                                 [EventDateTime] [datetime] NULL,
                                 [Type] [varchar](15) NOT NULL,
                                 [MessageDirection] [varchar](10) NOT NULL,
                                 [Description] [nvarchar](max) NULL,
                                 [Dataset] [varbinary](max) NULL,
                                 [CustomInformation] [nvarchar](max) NULL,
                                 [DatasetPath] nvarchar(400) NULL,
                                 [CustomType] nvarchar(15) NULL,
                               CONSTRAINT [PK_DICOM_Server_Event_Log] PRIMARY KEY CLUSTERED 
                              (
                                 [EventID] ASC
                              ) ON [PRIMARY]
                              ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
                              ";
                                          
         
      }
   }
}
