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
   class MedicalWorkstationSqlInstaller
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
               SqlCommand command;

               // logging
               command = new SqlCommand(DicomLoggingSqlInstaller.DatabaseScripts.LoggingDatabase, connection);
               command.Transaction = transaction;
               command.ExecuteNonQuery();
               
               // storage
               command = new SqlCommand(DicomStorageSqlInstaller.DatabaseScripts.StorageDatabase, connection);
               command.Transaction = transaction;
               command.ExecuteNonQuery();

#if (LEADTOOLS_V19_OR_LATER)
               // options
               command = new SqlCommand(DicomStorageServerOptionsSqlInstaller.DatabaseScripts.StorageServerOptionsDatabase, connection);
               command.Transaction = transaction;
               command.ExecuteNonQuery();
#endif // #if (LEADTOOLS_V19_OR_LATER)
               // media creation
               command = new SqlCommand(MediaCreationSqlInstaller.DatabaseScripts.MediaCreationDatabase, connection);
               command.Transaction = transaction;
               command.ExecuteNonQuery();

               // workstation creation
               command = new SqlCommand(WorkstationSqlInstaller.DatabaseScripts.WorkstationDatabase, connection);
               command.Transaction = transaction;
               command.ExecuteNonQuery();
               
               // workstation users management creation
               command = new SqlCommand(WorkstationUsersManagementSqlInstaller.DatabaseScripts.WorkstationUsersDatabase, connection);
               command.Transaction = transaction;
               command.ExecuteNonQuery();
               
               // version
               VersionSqlInstaller.CreateVersionTable(connection, transaction);
               VersionSqlInstaller.InsertVersionTable(connection, transaction, ConfigurationData.DataAccessLayerVersionNames.Logging, 1, 6);
#if (LEADTOOLS_V19_OR_LATER)
               VersionSqlInstaller.InsertVersionTable(connection, transaction, ConfigurationData.DataAccessLayerVersionNames.Storage, 1, 13);
#else
               VersionSqlInstaller.InsertVersionTable(connection, transaction, ConfigurationData.DataAccessLayerVersionNames.Storage, 1, 6);
#endif // (LEADTOOLS_V19_OR_LATER)               
               VersionSqlInstaller.InsertVersionTable(connection, transaction, ConfigurationData.DataAccessLayerVersionNames.Media, 1, 2);
               VersionSqlInstaller.InsertVersionTable(connection, transaction, ConfigurationData.DataAccessLayerVersionNames.Workstation, 1, 5);
#if (LEADTOOLS_V19_OR_LATER)
               VersionSqlInstaller.InsertVersionTable(connection, transaction, ConfigurationData.DataAccessLayerVersionNames.UserManagement, 1, 4);
#else
               VersionSqlInstaller.InsertVersionTable(connection, transaction, ConfigurationData.DataAccessLayerVersionNames.UserManagement, 1, 1);
#endif
               VersionSqlInstaller.InsertVersionTable(connection, transaction, ConfigurationData.DataAccessLayerVersionNames.MedicalWorkstation, 1, 0);

#if (LEADTOOLS_V19_OR_LATER)
                VersionSqlInstaller.InsertVersionTable(connection, transaction, ConfigurationData.DataAccessLayerVersionNames.Options, 1, 2);
#endif // #if (LEADTOOLS_V19_OR_LATER)

               // Cleanup
               command.Dispose();

               transaction.Commit();
            }
            catch (Exception)
            {
               transaction.Rollback();

               throw;
            }
            finally
            {
               transaction.Dispose();
            }
         }
      }
      
      private abstract class DatabaseScripts
      {
         //         public const string InsertVersionTable = @"
         //               INSERT INTO [dbo].[DbVersion]
         //               VALUES
         //               (
         //                  1,
         //                  5
         //               )
         //         ";

         //         public const string MedicalWorkstationDatabase = @"" +
         //@"--- Version Database: " + 
         //          @" CREATE TABLE [dbo].[DbVersion](
         //                                    [Major] [int] NOT NULL,
         //                                    [Minor] [int] NOT NULL,
         //                                  CONSTRAINT [PK_DICOMServerDbVersion] PRIMARY KEY CLUSTERED 
         //                                 (
         //                                    [Major] ASC,
         //                                    [Minor] ASC
         //                                 ) ON [PRIMARY]
         //                                 ) ON [PRIMARY] " + 

         //@"--- logging Database: " + 
         //                                 @" CREATE TABLE [dbo].[DICOMServerEventLog](
         //                                 [EventID] [bigint] IDENTITY(0,1) NOT NULL,
         //                                 [ServerAETitle] [varchar](16) NULL,
         //                                 [ServerIPAddress] [varchar](50) NULL,
         //                                 [ServerPort] [int] NULL,
         //                                 [ClientAETitle] [varchar](16) NULL,
         //                                 [ClientHostAddress] [varchar](50) NULL,
         //                                 [ClientPort] [int] NULL,
         //                                 [Command] [nvarchar](15) NOT NULL CONSTRAINT [DF_DICOMServerEventLog_Command]  DEFAULT ((0)),
         //                                 [EventDateTime] [datetime] NULL,
         //                                 [Type] [varchar](15) NOT NULL,
         //                                 [MessageDirection] [varchar](10) NOT NULL,
         //                                 [Description] [nvarchar](max) NULL,
         //                                 [Dataset] [varbinary](max) NULL,
         //                                 [CustomInformation] [nvarchar](max) NULL,
         //                               CONSTRAINT [PK_DICOM_Server_Event_Log] PRIMARY KEY CLUSTERED 
         //                              (
         //                                 [EventID] ASC
         //                              ) ON [PRIMARY]
         //                              ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
         //                              " + 

         //@"--- Storage Database: " + 
         //                                    @" CREATE TABLE [dbo].[Patient](
         //                                    [PatientID] [nvarchar](64) NOT NULL,
         //                                    [FamilyName] [nvarchar](64) NULL,
         //                                    [GivenName] [nvarchar](63) NULL,
         //                                    [MiddleName] [nvarchar](62) NULL,
         //                                    [NamePrefix] [nvarchar](61) NULL,
         //                                    [NameSuffix] [nvarchar](60) NULL,
         //                                    [BirthDate] [datetime] NULL,
         //                                    [Sex] [nvarchar](50) NULL,
         //                                    [EthnicGroup] [nvarchar](50) NULL,
         //                                    [Comments] [nvarchar](max) NULL,
         //                                    [RetrieveAETitle] [nvarchar](16) NULL,
         //                                    [IssuerOfPatientID] [nvarchar](64) NULL,
         //                                  CONSTRAINT [PK_Patient] PRIMARY KEY CLUSTERED 
         //                                 (
         //                                    [PatientID] ASC
         //                                 ) ON [PRIMARY]
         //                                 ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
         //                                
         //                              CREATE TABLE [dbo].[OtherPatientIDs](
         //                                 [PatientID] [nvarchar](64) NOT NULL,
         //                                 [OtherPatientID] [nvarchar](64) NOT NULL,
         //                               CONSTRAINT [PK_OtherPatientIDs] PRIMARY KEY CLUSTERED 
         //                              (
         //                                 [PatientID] ASC,
         //                                 [OtherPatientID] ASC
         //                              ) ON [PRIMARY]
         //                              ) ON [PRIMARY]
         //
         //                              
         //                              ALTER TABLE [dbo].[OtherPatientIDs]  WITH NOCHECK ADD  CONSTRAINT [FK_OtherPatientIDs_Patient] FOREIGN KEY([PatientID])
         //                              REFERENCES [dbo].[Patient] ([PatientID])
         //                              ON UPDATE CASCADE
         //                              ON DELETE CASCADE
         //                              NOT FOR REPLICATION 
         //                              
         //                              ALTER TABLE [dbo].[OtherPatientIDs] CHECK CONSTRAINT [FK_OtherPatientIDs_Patient]       
         //                              
         //                              CREATE TABLE [dbo].[OtherPatientNames](
         //                                 [FamilyName] [nvarchar](64) NULL,
         //                                 [GivenName] [nvarchar](63) NULL,
         //                                 [MiddleName] [nvarchar](62) NULL,
         //                                 [NamePrefix] [nvarchar](61) NULL,
         //                                 [NameSuffix] [nvarchar](60) NULL,
         //                                 [PatientID] [nvarchar](64) NOT NULL,
         //                                 [OrderNumber] [int] NOT NULL IDENTITY (1,1),
         //                               CONSTRAINT [PK_OtherPatientNames] PRIMARY KEY CLUSTERED 
         //                              (
         //                                 [OrderNumber] ASC
         //                              ) ON [PRIMARY]
         //                              ) ON [PRIMARY]
         //
         //                              
         //                              ALTER TABLE [dbo].[OtherPatientNames]  WITH NOCHECK ADD  CONSTRAINT [FK_OtherPatientNames_Patient] FOREIGN KEY([PatientID])
         //                              REFERENCES [dbo].[Patient] ([PatientID])
         //                              ON UPDATE CASCADE
         //                              ON DELETE CASCADE
         //                              
         //                              ALTER TABLE [dbo].[OtherPatientNames] CHECK CONSTRAINT [FK_OtherPatientNames_Patient]
         //                              
         //                              
         //                              CREATE TABLE [dbo].[ReferencedPatientSequence](
         //                                 [PatientID] [nvarchar](64) NOT NULL,
         //                                 [ReferencedSOPInstanceUID] [nvarchar](64) NOT NULL,
         //                                 [ReferencedSOPClassUID] [nvarchar](64) NOT NULL,
         //                               CONSTRAINT [PK_ReferencedPatientSequence] PRIMARY KEY CLUSTERED 
         //                              (
         //                                 [PatientID] ASC
         //                              ) ON [PRIMARY]
         //                              ) ON [PRIMARY]
         //
         //                              
         //                              ALTER TABLE [dbo].[ReferencedPatientSequence]  WITH NOCHECK ADD  CONSTRAINT [FK_ReferencedPatientSequence_Patient] FOREIGN KEY([PatientID])
         //                              REFERENCES [dbo].[Patient] ([PatientID])
         //                              ON UPDATE CASCADE
         //                              ON DELETE CASCADE
         //                              
         //                              ALTER TABLE [dbo].[ReferencedPatientSequence] CHECK CONSTRAINT [FK_ReferencedPatientSequence_Patient]
         //                              
         //                              
         //                              CREATE TABLE [dbo].[Study](
         //                                 [StudyInstanceUID] [nvarchar](64) NOT NULL,
         //                                 [StudyDate] [datetime] NULL,
         //                                 [AccessionNumber] [nvarchar](50) NULL,
         //                                 [StudyID] [nvarchar](50) NULL,
         //                                 [ReferDrFamilyName] [nvarchar](64) NULL,
         //                                 [ReferDrGivenName] [nvarchar](63) NULL,
         //                                 [ReferDrMiddleName] [nvarchar](62) NULL,
         //                                 [ReferDrNamePrefix] [nvarchar](61) NULL,
         //                                 [ReferDrNameSuffix] [nvarchar](60) NULL,
         //                                 [StudyDescription] [nvarchar](64) NULL,
         //                                 [AdmittingDiagnosesDesc] [nvarchar](64) NULL,
         //                                 [PatientAge] [nvarchar](4) NULL,
         //                                 [PatientSize] [numeric](18, 0) NULL,
         //                                 [PatientWeight] [numeric](18, 0) NULL,
         //                                 [Occupation] [nvarchar](16) NULL,
         //                                 [AdditionalPatientHistory] [nvarchar](max) NULL,
         //                                 [InterpretationAuthor] [nvarchar](64) NULL,
         //                                 [PatientID] [nvarchar](64) NULL,
         //                                 [RetrieveAETitle] [nvarchar](16) NULL,
         //                               CONSTRAINT [PK_Studies] PRIMARY KEY CLUSTERED 
         //                              (
         //                                 [StudyInstanceUID] ASC
         //                              ) ON [PRIMARY]
         //                              ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
         //
         //                              
         //                              ALTER TABLE [dbo].[Study]  WITH NOCHECK ADD  CONSTRAINT [FK_Study_Patient] FOREIGN KEY([PatientID])
         //                              REFERENCES [dbo].[Patient] ([PatientID])
         //                              ON UPDATE CASCADE
         //                              ON DELETE CASCADE
         //                              
         //                              ALTER TABLE [dbo].[Study] CHECK CONSTRAINT [FK_Study_Patient]
         //                              
         //                              CREATE TABLE [dbo].[ProcedureCodeSequence](
         //                                 [StudyInstanceUID] [nvarchar](64) NOT NULL,
         //                                 [CodeValue] [nvarchar](16) NOT NULL,
         //                                 [CodingSchemaDesignator] [nvarchar](16) NOT NULL,
         //                                 [CodeMeaning] [nvarchar](64) NULL,
         //                                 [CodingSchemaVersion] [nvarchar](16) NULL,
         //                               CONSTRAINT [PK_ProcedureCodeSequence] PRIMARY KEY CLUSTERED 
         //                              (
         //                                 [StudyInstanceUID] ASC,
         //                                 [CodeValue] ASC,
         //                                 [CodingSchemaDesignator] ASC
         //                              ) ON [PRIMARY]
         //                              ) ON [PRIMARY]
         //
         //                              
         //                              ALTER TABLE [dbo].[ProcedureCodeSequence]  WITH NOCHECK ADD  CONSTRAINT [FK_ProcedureCodeSequence_Studies] FOREIGN KEY([StudyInstanceUID])
         //                              REFERENCES [dbo].[Study] ([StudyInstanceUID])
         //                              ON UPDATE CASCADE
         //                              ON DELETE CASCADE
         //                              
         //                              ALTER TABLE [dbo].[ProcedureCodeSequence] CHECK CONSTRAINT [FK_ProcedureCodeSequence_Studies]
         //                              
         //                              CREATE TABLE [dbo].[NameOfPhysicianReadingStudy](
         //                                 [PhysicianFamilyName] [nvarchar](64) NULL,
         //                                 [PhysicianGivenName] [nvarchar](63) NULL,
         //                                 [PhysicianMiddleName] [nvarchar](62) NULL,
         //                                 [PhysicianNamePrefix] [nvarchar](61) NULL,
         //                                 [PhysicianNameSuffix] [nvarchar](60) NULL,
         //                                 [StudyInstanceUID] [nvarchar](64) NOT NULL,
         //                               CONSTRAINT [PK_NameOfPhysicianReadingStudy] PRIMARY KEY CLUSTERED 
         //                              (
         //                                 [StudyInstanceUID] ASC
         //                              ) ON [PRIMARY]
         //                              ) ON [PRIMARY]
         //
         //                              
         //                              ALTER TABLE [dbo].[NameOfPhysicianReadingStudy]  WITH NOCHECK ADD  CONSTRAINT [FK_NameOfPhysicianReadingStudy_Studies] FOREIGN KEY([StudyInstanceUID])
         //                              REFERENCES [dbo].[Study] ([StudyInstanceUID])
         //                              ON UPDATE CASCADE
         //                              ON DELETE CASCADE
         //                              
         //                              ALTER TABLE [dbo].[NameOfPhysicianReadingStudy] CHECK CONSTRAINT [FK_NameOfPhysicianReadingStudy_Studies]
         //                              
         //                              CREATE TABLE [dbo].[ReferencedStudySequence](
         //                                 [StudyInstanceUID] [nvarchar](64) NOT NULL,
         //                                 [ReferencedSOPInstanceUID] [nvarchar](64) NOT NULL,
         //                                 [ReferencedSOPClassUID] [nvarchar](64) NOT NULL,
         //                               CONSTRAINT [PK_ReferencedStudySequence] PRIMARY KEY CLUSTERED 
         //                              (
         //                                 [StudyInstanceUID] ASC,
         //                                 [ReferencedSOPInstanceUID] ASC
         //                              ) ON [PRIMARY]
         //                              ) ON [PRIMARY]
         //
         //                              
         //                              ALTER TABLE [dbo].[ReferencedStudySequence]  WITH NOCHECK ADD  CONSTRAINT [FK_ReferencedStudySequence_Studies] FOREIGN KEY([StudyInstanceUID])
         //                              REFERENCES [dbo].[Study] ([StudyInstanceUID])
         //                              ON UPDATE CASCADE
         //                              ON DELETE CASCADE
         //                              
         //                              ALTER TABLE [dbo].[ReferencedStudySequence] CHECK CONSTRAINT [FK_ReferencedStudySequence_Studies]
         //                              
         //                              CREATE TABLE [dbo].[OtherStudyNumbers](
         //                                 [OtherStudyNumber] [decimal](18, 0) NOT NULL,
         //                                 [StudyInstanceUID] [nvarchar](64) NOT NULL,
         //                               CONSTRAINT [PK_OtherStudyNumbers] PRIMARY KEY CLUSTERED 
         //                              (
         //                                 [OtherStudyNumber] ASC,
         //                                 [StudyInstanceUID] ASC
         //                              ) ON [PRIMARY]
         //                              ) ON [PRIMARY]
         //
         //                              
         //                              ALTER TABLE [dbo].[OtherStudyNumbers]  WITH NOCHECK ADD  CONSTRAINT [FK_OtherStudyNumbers_Studies] FOREIGN KEY([StudyInstanceUID])
         //                              REFERENCES [dbo].[Study] ([StudyInstanceUID])
         //                              ON UPDATE CASCADE
         //                              ON DELETE CASCADE
         //                              
         //                              ALTER TABLE [dbo].[OtherStudyNumbers] CHECK CONSTRAINT [FK_OtherStudyNumbers_Studies]
         //                              
         //                              CREATE TABLE [dbo].[Series](
         //                                 [SeriesInstanceUID] [nvarchar](64) NOT NULL,
         //                                 [Modality] [nvarchar](16) NULL,
         //                                 [SeriesNumber] [int] NULL,
         //                                 [SeriesDate] [datetime] NULL,
         //                                 [SeriesDescription] [nvarchar](64) NULL,
         //                                 [InstitutionName] [nvarchar](64) NULL,
         //                                 [StudyInstanceUID] [nvarchar](64) NOT NULL,
         //                                 [ReceiveDate] [datetime] NULL,
         //                                 [RetrieveAETitle] [nvarchar](16) NULL,
         //                                 [PerformedProcedureStepID] [nvarchar](16) NULL,
         //                                 [PerformedProcedureStartDate] [datetime] NULL,
         //                                 [BodyPartExamined] [nvarchar](16) NULL,
         //                               CONSTRAINT [PK_Series] PRIMARY KEY CLUSTERED 
         //                              (
         //                                 [SeriesInstanceUID] ASC
         //                              ) ON [PRIMARY]
         //                              ) ON [PRIMARY]
         //
         //                              
         //                              ALTER TABLE [dbo].[Series]  WITH NOCHECK ADD  CONSTRAINT [FK_Series_Studies] FOREIGN KEY([StudyInstanceUID])
         //                              REFERENCES [dbo].[Study] ([StudyInstanceUID])
         //                              ON UPDATE CASCADE
         //                              ON DELETE CASCADE
         //                              
         //                              ALTER TABLE [dbo].[Series] CHECK CONSTRAINT [FK_Series_Studies]
         //                              
         //                              CREATE TABLE [dbo].[ReferencedPerformedProcedureStepSequence](
         //                                 [SeriesInstanceUID] [nvarchar](64) NOT NULL,
         //                                 [ReferencedSOPClassUID] [nvarchar](64) NOT NULL,
         //                                 [ReferencedSOPInstanceUID] [nvarchar](64) NOT NULL,
         //                               CONSTRAINT [PK_ReferencedPerformedProcedure] PRIMARY KEY CLUSTERED 
         //                              (
         //                                 [SeriesInstanceUID] ASC
         //                              ) ON [PRIMARY]
         //                              ) ON [PRIMARY]
         //
         //                              
         //                              ALTER TABLE [dbo].[ReferencedPerformedProcedureStepSequence]  WITH NOCHECK ADD  CONSTRAINT [FK_ReferencedPerformedProcedure_Series] FOREIGN KEY([SeriesInstanceUID])
         //                              REFERENCES [dbo].[Series] ([SeriesInstanceUID])
         //                              ON UPDATE CASCADE
         //                              ON DELETE CASCADE
         //                              
         //                              ALTER TABLE [dbo].[ReferencedPerformedProcedureStepSequence] CHECK CONSTRAINT [FK_ReferencedPerformedProcedure_Series]
         //                              
         //                              CREATE TABLE [dbo].[RequestAttributeSequence](
         //                                 [SeriesInstanceUID] [nvarchar](64) NOT NULL,
         //                                 [RequestedProcedureID] [nvarchar](16) NOT NULL,
         //                                 [ScheduledProcedureStepID] [nvarchar](16) NOT NULL,
         //                               CONSTRAINT [PK_RequestAttributeSequence] PRIMARY KEY CLUSTERED 
         //                              (
         //                                 [SeriesInstanceUID] ASC,
         //                                 [RequestedProcedureID] ASC,
         //                                 [ScheduledProcedureStepID] ASC
         //                              ) ON [PRIMARY]
         //                              ) ON [PRIMARY]
         //
         //                              
         //                              ALTER TABLE [dbo].[RequestAttributeSequence]  WITH NOCHECK ADD  CONSTRAINT [FK_RequestAttributeSequence_Series] FOREIGN KEY([SeriesInstanceUID])
         //                              REFERENCES [dbo].[Series] ([SeriesInstanceUID])
         //                              ON UPDATE CASCADE
         //                              ON DELETE CASCADE
         //                              NOT FOR REPLICATION 
         //                              
         //                              ALTER TABLE [dbo].[RequestAttributeSequence] CHECK CONSTRAINT [FK_RequestAttributeSequence_Series]
         //                              
         //                              CREATE TABLE [dbo].[Instance](
         //                                 [SOPInstanceUID] [nvarchar](64) NOT NULL,
         //                                 [InstanceNumber] [int] NULL,
         //                                 [ReferencedFile] [nvarchar](400) NULL,
         //                                 [TransferSyntax] [nvarchar](64) NULL,
         //                                 [SOPClassUID] [nvarchar](64) NULL,
         //                                 [StationName] [nvarchar](16) NULL,
         //                                 [SeriesInstanceUID] [nvarchar](64) NOT NULL,
         //                                 [ReceiveDate] [datetime] NULL,
         //                                 [RetrieveAETitle] [nvarchar](16) NULL,
         //                               CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED 
         //                              (
         //                                 [SOPInstanceUID] ASC
         //                              ) ON [PRIMARY]
         //                              ) ON [PRIMARY]
         //
         //                              
         //                              ALTER TABLE [dbo].[Instance]  WITH NOCHECK ADD  CONSTRAINT [FK_Images_Series] FOREIGN KEY([SeriesInstanceUID])
         //                              REFERENCES [dbo].[Series] ([SeriesInstanceUID])
         //                              ON UPDATE CASCADE
         //                              ON DELETE CASCADE
         //                              
         //                              ALTER TABLE [dbo].[Instance] CHECK CONSTRAINT [FK_Images_Series]
         //                              
         //                              CREATE TABLE [dbo].[ImageInstance](
         //                                 [SOPInstanceUID] [nvarchar](64) NOT NULL,
         //                                 [ImageRows] [int] NULL,
         //                                 [ImageColumns] [int] NULL,
         //                                 [BitsAllocated] [int] NULL,
         //                                 [NumberOfFrames] [int] NULL,
         //                               CONSTRAINT [PK_ImageInstance] PRIMARY KEY CLUSTERED 
         //                              (
         //                                 [SOPInstanceUID] ASC
         //                              ) ON [PRIMARY]
         //                              ) ON [PRIMARY]
         //
         //                              
         //                              ALTER TABLE [dbo].[ImageInstance]  WITH CHECK ADD  CONSTRAINT [FK_ImageInstance_Instance] FOREIGN KEY([SOPInstanceUID])
         //                              REFERENCES [dbo].[Instance] ([SOPInstanceUID])
         //                              ON UPDATE CASCADE
         //                              ON DELETE CASCADE
         //                              
         //                              ALTER TABLE [dbo].[ImageInstance] CHECK CONSTRAINT [FK_ImageInstance_Instance]
         //                              
         //                              CREATE TABLE [dbo].[ReferencedImages](
         //                                 [SOPInstanceUID] [nvarchar](64) NOT NULL,
         //                                 [AutoNumber] [int] IDENTITY(1,1) NOT NULL,
         //                                 [Width] [int] NOT NULL CONSTRAINT [DF_ReferencedImages_Width]  DEFAULT ((0)),
         //                                 [Height] [int] NOT NULL CONSTRAINT [DF_ReferencedImages_Height]  DEFAULT ((0)),
         //                                 [Format] [nvarchar](50) NOT NULL,
         //                                 [QualityFactor] [int] NOT NULL CONSTRAINT [DF_ReferencedImages_QualityFactor]  DEFAULT ((0)),
         //                                 [FrameIndex] [int] NOT NULL CONSTRAINT [DF_ReferencedImages_FrameIndex]  DEFAULT ((0)),
         //                                 [ReferencedFile] [nvarchar](400) NOT NULL,
         //                                 [Thumbnail] [bit] NOT NULL CONSTRAINT [DF_ReferencedImages_Thumbnail]  DEFAULT ((0)),
         //                               CONSTRAINT [PK_ReferencedImages] PRIMARY KEY CLUSTERED 
         //                              (
         //                                 [SOPInstanceUID] ASC,
         //                                 [AutoNumber] ASC
         //                              ) ON [PRIMARY]
         //                              ) ON [PRIMARY]
         //
         //                              
         //                              ALTER TABLE [dbo].[ReferencedImages]  WITH CHECK ADD  CONSTRAINT [FK_ReferencedImages_Instance] FOREIGN KEY([SOPInstanceUID])
         //                              REFERENCES [dbo].[Instance] ([SOPInstanceUID])
         //                              ON UPDATE CASCADE
         //                              ON DELETE CASCADE
         //                              
         //                              ALTER TABLE [dbo].[ReferencedImages] CHECK CONSTRAINT [FK_ReferencedImages_Instance]
         //                              " +

         //@"--- Workstation Database: " + 

         //"";
      }
   }
}
