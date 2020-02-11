// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.IO;
using Leadtools.Demos.Sql;
using CSPacsDatabaseConfigurationDemo.UI;

namespace MedicalWorkstationConfigurationDemo
{
   class MediaCreationSqlInstaller
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
               CreateMediaCreationTables ( connection, transaction ) ;
               
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

      public static void CreateMediaCreationTables(SqlConnection connection, SqlTransaction transaction)
      {
         SqlCommand command;


         {//Create Tables
            command = new SqlCommand(DatabaseScripts.MediaCreationDatabase, connection);

            command.Transaction = transaction;

            command.ExecuteNonQuery();
         }//Create Tables
         
         VersionSqlInstaller.CreateVersionTable(connection, transaction);
         VersionSqlInstaller.InsertVersionTable(connection, transaction, ConfigurationData.DataAccessLayerVersionNames.Media, 1, 2);

         command.Dispose();
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
         
         public const string MediaCreationDatabase =
                                                //                                             @" CREATE TABLE [dbo].[DbVersion](
                                                //                                                   [Major] [int] NOT NULL,
                                                //                                                   [Minor] [int] NOT NULL,
                                                //                                                 CONSTRAINT [PK_DICOMServerDbVersion] PRIMARY KEY CLUSTERED 
                                                //                                                (
                                                //                                                   [Major] ASC,
                                                //                                                   [Minor] ASC
                                                //                                                ) ON [PRIMARY]
                                                //                                                ) ON [PRIMARY]

                                                @" CREATE TABLE [MediaInformation] (
                                                   [SopInstanceUID] [nvarchar] (64) NOT NULL ,
                                                   [StorageFileSetID] [nvarchar] (16) NULL ,
                                                   [StorageFileSetUID] [nvarchar] (64)  NULL ,
                                                   [UseLabeInfoFromInstances] [bit] NULL ,
                                                   [LabelText] [nvarchar] (4000)  NULL ,
                                                   [LabelStyleSelection] [nvarchar] (16)  NULL ,
                                                   [BarcodeValue] [varchar](max)  NULL ,
                                                   [BarcodeSymbology] [nvarchar] (16)  NULL ,
                                                   [MediaDisposition] [varchar](max)  NULL ,
                                                   [AllowMediaSplitting] [bit] NULL ,
                                                   [AllowLossyCompression] [bit] NULL ,
                                                   [IncludeNonDicomObjects] [tinyint] NULL ,
                                                   [IncludeDisplayApplication] [bit] NULL ,
                                                   [PreserveAfterCreation] [bit] NULL ,
                                                   [NumberOfCopies] [int] NOT NULL CONSTRAINT [DF_MediaInformation_NumberOfCopies] DEFAULT (1),
                                                   [RequestPriority] [tinyint] NULL ,
                                                   [ExecutionStatus] [tinyint] NOT NULL ,
                                                   [ExecutionStatusInfo] [tinyint] NOT NULL ,
                                                   [TotalNumberOfMediaCreated] [int] NOT NULL CONSTRAINT [DF_MediaInformation_TotalNumberOfMediaCreated] DEFAULT (0),
                                                   [InstanceCreationDate] [datetime] NOT NULL CONSTRAINT [DF_MediaInformation_InstanceCreationDate] DEFAULT (getdate()),
                                                   [InstanceCreationTime] [datetime] NOT NULL CONSTRAINT [DF_MediaInformation_InstanceCreationTime] DEFAULT (getdate()),
                                                   [StatusUpdateDate] [datetime] NOT NULL CONSTRAINT [DF_MediaInformation_SatusUpdateDate] DEFAULT (getdate()),
                                                   CONSTRAINT [PK_MediaInformation] PRIMARY KEY  CLUSTERED 
                                                   (
                                                      [SopInstanceUID]
                                                   )  ON [PRIMARY] 
                                                ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
                                                
                                                CREATE TABLE [ReferencedCompositeInstances] (
                                                   [MediaObjectSopInstanceUID] [nvarchar] (64)  NOT NULL ,
                                                   [SopInstanceUID] [nvarchar] (64)  NOT NULL ,
                                                   [SopClassUID] [nvarchar] (64)  NOT NULL ,
                                                   [RequestedApplicationProfile] [nvarchar] (64)  NOT NULL ,
                                                   CONSTRAINT [PK_ReferencedCompositeInstances] PRIMARY KEY  CLUSTERED 
                                                   (
                                                      [MediaObjectSopInstanceUID],
                                                      [SopInstanceUID],
                                                      [SopClassUID]
                                                   )  ON [PRIMARY] ,
                                                   CONSTRAINT [FK_ReferencedCompositeInstances_MediaInformation] FOREIGN KEY 
                                                   (
                                                      [MediaObjectSopInstanceUID]
                                                   ) REFERENCES [MediaInformation] (
                                                      [SopInstanceUID]
                                                   ) ON DELETE CASCADE  ON UPDATE CASCADE 
                                                ) ON [PRIMARY]
                                                
                                                CREATE TABLE [ReferencedMediaStorage] (
                                                   [MediaObjectSopInstanceUID] [nvarchar] (64)  NOT NULL ,
                                                   [StorageFileSetUID] [nvarchar] (64)  NOT NULL ,
                                                   [StorageFileSetID] [nvarchar] (16)  NOT NULL ,
                                                   CONSTRAINT [FK_ReferencedMediaStorage_MediaInformation] FOREIGN KEY 
                                                   (
                                                      [MediaObjectSopInstanceUID]
                                                   ) REFERENCES [MediaInformation] (
                                                      [SopInstanceUID]
                                                   ) ON DELETE CASCADE  ON UPDATE CASCADE 
                                                ) ON [PRIMARY]
                                                                                                
                                                CREATE TABLE [FailedCompositeInstances] (
                                                   [MediaObjectSopInstanceUID] [nvarchar] (64)  NOT NULL ,
                                                   [SopInstanceUID] [nvarchar] (64)  NOT NULL ,
                                                   [SopClassUID] [nvarchar] (64)  NOT NULL ,
                                                   [RequestedApplicationProfile] [nvarchar] (64)  NOT NULL ,
                                                   [FailureReason] [int] NOT NULL ,
                                                   CONSTRAINT [PK_FailedCompositeInstances] PRIMARY KEY  CLUSTERED 
                                                   (
                                                      [MediaObjectSopInstanceUID],
                                                      [SopInstanceUID],
                                                      [SopClassUID]
                                                   )  ON [PRIMARY] ,
                                                   CONSTRAINT [FK_FailedCompositeInstances_MediaInformation] FOREIGN KEY 
                                                   (
                                                      [MediaObjectSopInstanceUID]
                                                   ) REFERENCES [MediaInformation] (
                                                      [SopInstanceUID]
                                                   ) ON DELETE CASCADE  ON UPDATE CASCADE 
                                                ) ON [PRIMARY]
                                                
                                                CREATE TABLE [FailedCompositeAttributes] (
                                                   [MediaObjectSopInstanceUID] [nvarchar] (64)  NOT NULL ,
                                                   [SopInstanceUID] [nvarchar] (64)  NOT NULL ,
                                                   [SopClassUID] [nvarchar] (64)  NOT NULL ,
                                                   [Attribute] [bigint] NOT NULL ,
                                                   CONSTRAINT [PK_FailedCompositeAttributes] PRIMARY KEY  CLUSTERED 
                                                   (
                                                      [MediaObjectSopInstanceUID],
                                                      [SopInstanceUID],
                                                      [SopClassUID],
                                                      [Attribute]
                                                   )  ON [PRIMARY] ,
                                                   CONSTRAINT [FK_FailedCompositeAttributes_FailedCompositeInstances] FOREIGN KEY 
                                                   (
                                                      [MediaObjectSopInstanceUID],
                                                      [SopInstanceUID],
                                                      [SopClassUID]
                                                   ) REFERENCES [FailedCompositeInstances] (
                                                      [MediaObjectSopInstanceUID],
                                                      [SopInstanceUID],
                                                      [SopClassUID]
                                                   ) ON DELETE CASCADE  ON UPDATE CASCADE 
                                                ) ON [PRIMARY]
                                                
                                                CREATE TABLE [MediaLocation] (
                                                   [MediaObjectSopInstanceUID] [nvarchar] (64)  NOT NULL ,
                                                   [Location] [nvarchar] (300)  NOT NULL ,
                                                   CONSTRAINT [PK_MediaLocation] PRIMARY KEY  CLUSTERED 
                                                   (
                                                      [MediaObjectSopInstanceUID]
                                                   )  ON [PRIMARY] ,
                                                   CONSTRAINT [FK_MediaLocation_MediaInformation] FOREIGN KEY 
                                                   (
                                                      [MediaObjectSopInstanceUID]
                                                   ) REFERENCES [MediaInformation] (
                                                      [SopInstanceUID]
                                                   ) ON DELETE CASCADE  ON UPDATE CASCADE 
                                                ) ON [PRIMARY]
                                             ";

      }
   }
}
