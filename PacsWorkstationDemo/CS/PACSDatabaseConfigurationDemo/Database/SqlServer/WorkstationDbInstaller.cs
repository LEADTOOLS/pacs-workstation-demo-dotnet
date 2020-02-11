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
   class WorkstationSqlInstaller
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
         
         
         using ( connection = new SqlConnection ( connectionString ) )
         {
            connection.Open ( ) ;
            
            transaction = connection.BeginTransaction ( ) ;
            
            try 
            {
               SqlCommand command ;
               
               {//Create Tables
                  command = new SqlCommand ( DatabaseScripts.WorkstationDatabase, connection ) ;
                  
                  command.Transaction = transaction ;
                  
                  command.ExecuteNonQuery ( ) ;
               }
               
               VersionSqlInstaller.CreateVersionTable(connection,transaction);
               VersionSqlInstaller.InsertVersionTable(connection, transaction, ConfigurationData.DataAccessLayerVersionNames.Workstation, 1, 5);
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
//                  5
//               )
//         ";
         
//Db Version 1.0
//         public const string InsertVersionTable = @"
//               INSERT INTO [dbo].[DbVersion]
//               VALUES
//               (
//                  1,
//                  0
//               )
//         ";
         
         public const string WorkstationDatabase =
                                               //                                             @" CREATE TABLE [dbo].[DbVersion](
                                               //                                                   [Major] [int] NOT NULL,
                                               //                                                   [Minor] [int] NOT NULL,
                                               //                                                 CONSTRAINT [PK_DICOMServerDbVersion] PRIMARY KEY CLUSTERED 
                                               //                                                (
                                               //                                                   [Major] ASC,
                                               //                                                   [Minor] ASC
                                               //                                                ) ON [PRIMARY]
                                               //                                                ) ON [PRIMARY]

                                               @"  CREATE TABLE [dbo].[Configuration](
                                                [Component] [nvarchar](64) NOT NULL,
                                                [Data] [varchar](max) NULL,
                                                 CONSTRAINT [PK_Configuration] PRIMARY KEY CLUSTERED 
                                                (
                                                   [Component] ASC
                                                ) ON [PRIMARY]
                                                ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

                                                CREATE TABLE [dbo].[SeriesVolume](
                                                   [VolumeNumber] [int] NOT NULL,
                                                   [SeriesInstanceUID] [nvarchar](64) NOT NULL,
                                                   [VolumeFilePath] [nvarchar](300)  NULL,
                                                   [VolumeStateFilePath] [nvarchar](300)  NULL,
                                                   [UserIdentifier] [nvarchar](max) NULL,
                                                 CONSTRAINT [PK_SeriesVolume] PRIMARY KEY CLUSTERED 
                                                (
                                                   [VolumeNumber] ASC,
                                                   [SeriesInstanceUID] ASC
                                                ) ON [PRIMARY]
                                                ) ON [PRIMARY]
--V1.5
                                                CREATE TABLE [dbo].[ObjectAnnotation] (
                                                   [AnnotationNumber] [int] NOT NULL,
                                                   [SopInstanceUID] [nvarchar] (64) NOT NULL,
                                                   [Description] [nvarchar] (400) NULL,
                                                   [FilePath] [nvarchar] (300) NOT NULL,
                                                CONSTRAINT [PK_AnnotationSopInstance] PRIMARY KEY CLUSTERED 
                                                (
                                                   [AnnotationNumber] ASC,
                                                   [SopInstanceUID] ASC
                                                ) ON [PRIMARY]
                                                ) ON [PRIMARY]
                                                ";
                                          
         
      }
   }
}
