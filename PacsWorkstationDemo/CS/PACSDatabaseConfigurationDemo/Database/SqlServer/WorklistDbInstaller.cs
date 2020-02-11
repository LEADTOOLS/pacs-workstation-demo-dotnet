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
   class WorklistSqlInstaller
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

         CreateSqlTablesAndFillData(connectionString);
      }

      private static void CreateSqlTablesAndFillData(string connectionString)
      {
         using (SqlConnection connection = new SqlConnection ( connectionString )  )
         {
            try
            {
               connection.Open();

               using (SqlTransaction transaction = connection.BeginTransaction())
               {
                  try
                  {
                     CreateWorklistTables(connection, transaction);
                     FillWorklistTables(connection, transaction);

                     transaction.Commit();
                  }
                  catch (Exception)
                  {
                     transaction.Rollback();

                     throw;
                  }
               }
            }
            finally
            {
               connection.Close();
            }
            

         }
      }

      private static void CreateWorklistTables(SqlConnection connection, SqlTransaction transaction)
      {
         using (SqlCommand command = new SqlCommand(DatabaseScripts.WorklistDatabase, connection))
         {
            command.Transaction = transaction;
            command.ExecuteNonQuery();
         }

         VersionSqlInstaller.CreateVersionTable(connection, transaction);
         VersionSqlInstaller.InsertVersionTable(connection, transaction, ConfigurationData.DataAccessLayerVersionNames.Worklist, 1, 1);
      }

      private static void FillWorklistTables(SqlConnection connection, SqlTransaction transaction)
      {
         
         {//Modalities
            using (SqlCommand command = new SqlCommand(DatabaseScripts.InsertModalities, connection))
            {
               command.Transaction = transaction;
               command.ExecuteNonQuery();
            }
         }

         {//Patient Sex
            using (SqlCommand command = new SqlCommand(DatabaseScripts.InsertPatientSex, connection))
            {
               command.Transaction = transaction;
               command.ExecuteNonQuery();
            }
         }
         
         {//PPStatus
            using (SqlCommand command = new SqlCommand(DatabaseScripts.InsertPPStatus, connection))
            {
               command.Transaction = transaction;
               command.ExecuteNonQuery();
            }
         }
      }

      private static void FillListTables(SqlConnection connection, SqlTransaction transaction)
      {
         SqlCommand command = null ;
         
         
         // version
         VersionSqlInstaller.CreateVersionTable(connection, transaction);
         VersionSqlInstaller.InsertVersionTable(connection, transaction, ConfigurationData.DataAccessLayerVersionNames.Worklist, 1, 0);

         
         {//Modalities
            command = new SqlCommand ( DatabaseScripts.InsertModalities, connection ) ;
            
            command.Transaction = transaction ;

            command.ExecuteNonQuery ( )  ;
         }
         
         {//Patient Sex
            command = new SqlCommand ( DatabaseScripts.InsertPatientSex, connection ) ;
            
            command.Transaction = transaction ;

            command.ExecuteNonQuery ( )  ;
         }
         
         {//PPStatus
            command = new SqlCommand ( DatabaseScripts.InsertPPStatus, connection ) ;
            
            command.Transaction = transaction ;

            command.ExecuteNonQuery ( )  ;
         }

         if ( null != command ) 
         {
            command.Dispose() ;
         }
      }
      
      public abstract class DatabaseScripts
      {
         public const string InsertModalities = @"
      INSERT INTO ModalityList
      VALUES
      (
         'CR'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'CT'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'MR'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'NM'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'US'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'OT'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'BI'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'CD'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'DD'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'DG'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'ES'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'LS'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'PT'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'RG'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'ST'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'TG'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'XA'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'RF'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'RTIMAGE'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'RTDOSE'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'RTSTRUCT'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'RTPLAN'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'RTRECORD'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'HC'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'DX'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'MG'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'IO'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'PX'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'GM'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'SM'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'XC'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'PR'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'AU'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'ECG'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'EPS'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'HD'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'SR'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'IVUS'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'OP'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'SMR'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'AR'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'KER'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'VA'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'SRF'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'OCT'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'LEN'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'OPV'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'OPM'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'OAM'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'RESP'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'KO'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'SEG'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'REG'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'OPT'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'BDUS'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'BMD'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'DOC'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'FID'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'OPR'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'DS'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'CF'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'DF'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'VF'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'AS'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'CS'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'EC'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'LP'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'FA'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'CP'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'DM'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'FS'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'MA'
      )
      INSERT INTO ModalityList
      VALUES
      (
         'MS'
      )
          " ;
          
          public const string InsertPatientSex = @"
      INSERT INTO PatientSexList
      VALUES
      (
         'M'
      )
      
      INSERT INTO PatientSexList
      VALUES
      (
         'F'
      )
      
      INSERT INTO PatientSexList
      VALUES
      (
         'O'
      )
      " ;
      
         public const string InsertPPStatus = @"
               INSERT INTO PPSStatusList
      VALUES
      (
         'IN PROGRESS'
      )
   
      INSERT INTO PPSStatusList
      VALUES
      (
         'DISCONTINUED'
      )
      
      INSERT INTO PPSStatusList
      VALUES
      (
         'COMPLETED'
      )
      " ;
      
         
         public const string WorklistDatabase =
         //         @" CREATE TABLE [dbo].[DbVersion](
         //                                    [Major] [int] NOT NULL,
         //                                    [Minor] [int] NOT NULL,
         //                                  CONSTRAINT [PK_DICOMServerDbVersion] PRIMARY KEY CLUSTERED 
         //                                 (
         //                                    [Major] ASC,
         //                                    [Minor] ASC
         //                                 ) ON [PRIMARY]
         //                                 ) ON [PRIMARY]

         @" CREATE TABLE [dbo].[ModalityList](
	                                 [Modality] [nvarchar](16) NOT NULL
                                  CONSTRAINT [PK_ModalityList] PRIMARY KEY CLUSTERED 
                              (
	                                 [Modality] ASC
                                 ) ON [PRIMARY]
                                 ) ON [PRIMARY]

                                 
                                 SET ANSI_PADDING OFF
         
                                 CREATE TABLE [dbo].[PatientSexList](
	                                 [PatientSex] [nvarchar](16) NOT NULL,
                                  CONSTRAINT [PK_PatientSexList] PRIMARY KEY CLUSTERED 
                                 (
	                                 [PatientSex] ASC
                                 ) ON [PRIMARY]
                                 ) ON [PRIMARY]
         
                                 
                                 SET ANSI_PADDING OFF
         
                                 CREATE TABLE [dbo].[PPSStatusList](
	                                 [Status] [nvarchar](16) NOT NULL,
                                  CONSTRAINT [PK_StatusList] PRIMARY KEY CLUSTERED 
                                 (
	                                 [Status] ASC
                                 ) ON [PRIMARY]
                                 ) ON [PRIMARY]


                                 SET ANSI_PADDING OFF
                                 
                                 CREATE TABLE [dbo].[Patient](
	                              [PatientID] [nvarchar](64) NOT NULL,
	                              [IssuerOfPatientID] [nvarchar](64)  NOT NULL,
	                              [PatientNameFamilyName] [nvarchar](64)  NULL,
	                              [PatientNameGivenName] [nvarchar](63)  NULL,
	                              [PatientNameMiddleName] [nvarchar](62)  NULL,
	                              [PatientNamePrefix] [nvarchar](61)  NULL,
	                              [PatientNameSuffix] [nvarchar](60)  NULL,
	                              [ConfidentialityConstraintonPatientDataDescription] [nvarchar](64)  NULL,
	                              [PatientBirthDate] [datetime] NULL,
	                              [PatientSex] [nvarchar](16)  NULL,
	                              [PatientWeight] [nvarchar](16)  NULL,
	                              [EthnicGroup] [nvarchar](16)  NULL,
	                              [PatientComments] [varchar](max)  NULL,
	                              [AdditionalPatientHistory] [varchar](max)  NULL,
	                              [PregnancyStatus] [int] NULL,
	                              [SpecialNeeds] [nvarchar](64)  NULL,
	                              [PatientState] [nvarchar](64)  NULL,
	                              [LastMenstrualDate] [datetime] NULL,
                               CONSTRAINT [PK_Patient_PatientID_IssuerOfPatientID] PRIMARY KEY CLUSTERED 
                              (
	                              [PatientID] ASC,
	                              [IssuerOfPatientID] ASC
                              ) ON [PRIMARY]
                              ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

                              
                              SET ANSI_PADDING OFF
                              
                              ALTER TABLE [dbo].[Patient]  WITH CHECK ADD  CONSTRAINT [FK_Patient_PatientSexList] FOREIGN KEY([PatientSex])
                              REFERENCES [dbo].[PatientSexList] ([PatientSex])
                              
                              ALTER TABLE [dbo].[Patient] CHECK CONSTRAINT [FK_Patient_PatientSexList]
                              
                              ALTER TABLE [dbo].[Patient]  WITH NOCHECK ADD  CONSTRAINT [Check_PatientName_Not_Empty] CHECK  ((isnull(datalength([PatientNameFamilyName]),0) + isnull(datalength([PatientNameGivenName]),0) + isnull(datalength([PatientNameMiddleName]),0) + isnull(datalength([PatientNamePrefix]),0) + isnull(datalength([PatientNameSuffix]),0) > 0))
                              
                              ALTER TABLE [dbo].[Patient] CHECK CONSTRAINT [Check_PatientName_Not_Empty]
                              
                              ALTER TABLE [dbo].[Patient]  WITH NOCHECK ADD  CONSTRAINT [Check_PatientName_Not_NULL] CHECK  (([PatientNameFamilyName] is not null or [PatientNameGivenName] is not null or [PatientNameMiddleName] is not null or [PatientNamePrefix] is not null or [PatientNameSuffix] is not null))
                              
                              ALTER TABLE [dbo].[Patient] CHECK CONSTRAINT [Check_PatientName_Not_NULL]
                              
                              ALTER TABLE [dbo].[Patient]  WITH NOCHECK ADD  CONSTRAINT [PregnancyStatus_Out_Of_Range] CHECK  (([PregnancyStatus] >= 0 and [PregnancyStatus] <= 65536))
                              
                              ALTER TABLE [dbo].[Patient] CHECK CONSTRAINT [PregnancyStatus_Out_Of_Range]

                              CREATE TABLE [dbo].[ContrastAllergies](
	                              [PatientID] [nvarchar](64)  NOT NULL,
	                              [IssuerOfPatientID] [nvarchar](64)  NOT NULL,
	                              [ContrastAllergy] [nvarchar](64)  NOT NULL,
                               CONSTRAINT [PK_ContrastAllergies_PatientID_IssuerOfPatientID_ContrastAllergy] PRIMARY KEY CLUSTERED 
                              (
	                              [PatientID] ASC,
	                              [IssuerOfPatientID] ASC,
	                              [ContrastAllergy] ASC
                              ) ON [PRIMARY]
                              ) ON [PRIMARY]

                              
                              SET ANSI_PADDING OFF
                              
                              ALTER TABLE [dbo].[ContrastAllergies]  WITH NOCHECK ADD  CONSTRAINT [FK_ContrastAllergies_PatientTable_PatientID_IssuerOfPatientID] FOREIGN KEY([PatientID], [IssuerOfPatientID])
                              REFERENCES [dbo].[Patient] ([PatientID], [IssuerOfPatientID])
                              ON UPDATE CASCADE
                              ON DELETE CASCADE
                              
                              ALTER TABLE [dbo].[ContrastAllergies] CHECK CONSTRAINT [FK_ContrastAllergies_PatientTable_PatientID_IssuerOfPatientID]
                              
                              CREATE TABLE [dbo].[MedicalAlerts](
	                              [PatientID] [nvarchar](64)  NOT NULL,
	                              [IssuerOfPatientID] [nvarchar](64)  NOT NULL,
	                              [MedicalAlert] [nvarchar](64)  NOT NULL,
                               CONSTRAINT [PK_MedicalAlerts_PatientID_IssuerOfPatientID_MedicalAlert] PRIMARY KEY CLUSTERED 
                              (
	                              [PatientID] ASC,
	                              [IssuerOfPatientID] ASC,
	                              [MedicalAlert] ASC
                              ) ON [PRIMARY]
                              ) ON [PRIMARY]

                              
                              SET ANSI_PADDING OFF
                              
                              ALTER TABLE [dbo].[MedicalAlerts]  WITH NOCHECK ADD  CONSTRAINT [FK_MedicalAlerts_Patient_PatientID_IssuerOfPatientID] FOREIGN KEY([PatientID], [IssuerOfPatientID])
                              REFERENCES [dbo].[Patient] ([PatientID], [IssuerOfPatientID])
                              ON UPDATE CASCADE
                              ON DELETE CASCADE
                              
                              ALTER TABLE [dbo].[MedicalAlerts] CHECK CONSTRAINT [FK_MedicalAlerts_Patient_PatientID_IssuerOfPatientID]

                              CREATE TABLE [dbo].[OtherPatientIDs](
	                              [PatientID] [nvarchar](64)  NOT NULL,
	                              [IssuerOfPatientID] [nvarchar](64)  NOT NULL,
	                              [OtherPatientID] [nvarchar](64)  NOT NULL,
                               CONSTRAINT [PK_OtherPatientIDs_PatientID_IssuerOfPatientID_OtherPatientID] PRIMARY KEY CLUSTERED 
                              (
	                              [PatientID] ASC,
	                              [IssuerOfPatientID] ASC,
	                              [OtherPatientID] ASC
                              ) ON [PRIMARY]
                              ) ON [PRIMARY]

                              
                              SET ANSI_PADDING OFF
                              
                              ALTER TABLE [dbo].[OtherPatientIDs]  WITH NOCHECK ADD  CONSTRAINT [FK_OtherPatientIDs_Patient_PatientID_IssuerOfPatientID] FOREIGN KEY([PatientID], [IssuerOfPatientID])
                              REFERENCES [dbo].[Patient] ([PatientID], [IssuerOfPatientID])
                              ON UPDATE CASCADE
                              ON DELETE CASCADE
                              
                              ALTER TABLE [dbo].[OtherPatientIDs] CHECK CONSTRAINT [FK_OtherPatientIDs_Patient_PatientID_IssuerOfPatientID]
                              
                              CREATE TABLE [dbo].[ImagingServiceRequest](
	                              [AccessionNumber] [nvarchar](16)  NOT NULL,
	                              [PatientID] [nvarchar](64)  NOT NULL,
	                              [IssuerOfPatientID] [nvarchar](64)  NOT NULL,
	                              [ImagingServiceRequestComments] [varchar](max)  NULL,
	                              [RequestingPhysicianFamilyName] [nvarchar](64)  NULL,
	                              [RequestingPhysicianGivenName] [nvarchar](63)  NULL,
	                              [RequestingPhysicianMiddleName] [nvarchar](62)  NULL,
	                              [RequestingPhysicianPrefix] [nvarchar](61)  NULL,
	                              [RequestingPhysicianSuffix] [nvarchar](60)  NULL,
	                              [ReferringPhysicianFamilyName] [nvarchar](64)  NULL,
	                              [ReferringPhysicianGivenName] [nvarchar](63)  NULL,
	                              [ReferringPhysicianMiddleName] [nvarchar](62)  NULL,
	                              [ReferringPhysicianPrefix] [nvarchar](61)  NULL,
	                              [ReferringPhysicianSuffix] [nvarchar](60)  NULL,
	                              [RequestingService] [nvarchar](64)  NULL,
	                              [PlacerOrderNumber_ImagingServiceRequest] [nvarchar](64)  NULL,
	                              [FillerOrderNumber_ImagingServiceRequest] [nvarchar](64)  NULL,
                               CONSTRAINT [PK_ImagingServiceRequest_AccessionNumber] PRIMARY KEY CLUSTERED 
                              (
	                              [AccessionNumber] ASC
                              ) ON [PRIMARY]
                              ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

                              
                              SET ANSI_PADDING OFF
                              
                              
                              ALTER TABLE [dbo].[ImagingServiceRequest]  WITH NOCHECK ADD  CONSTRAINT [FK_ImagingServiceRequest_Patient_PatientID_IssuerOfPatientID] FOREIGN KEY([PatientID], [IssuerOfPatientID])
                              REFERENCES [dbo].[Patient] ([PatientID], [IssuerOfPatientID])
                              ON UPDATE CASCADE
                              ON DELETE CASCADE
                              
                              ALTER TABLE [dbo].[ImagingServiceRequest] CHECK CONSTRAINT [FK_ImagingServiceRequest_Patient_PatientID_IssuerOfPatientID]
                              
                              CREATE TABLE [dbo].[Visit](
	                              [AdmissionID] [nvarchar](64)  NOT NULL,
	                              [CurrentPatientLocation] [nvarchar](64)  NULL,
                               CONSTRAINT [PK_Visit_AdmissionID] PRIMARY KEY CLUSTERED 
                              (
	                              [AdmissionID] ASC
                              ) ON [PRIMARY]
                              ) ON [PRIMARY]

                              
                              SET ANSI_PADDING OFF
                              
                              CREATE TABLE [dbo].[ReferencedPatientSequence](
	                              [AdmissionID] [nvarchar](64)  NOT NULL,
	                              [ReferencedSOPInstanceUID] [nvarchar](64)  NOT NULL,
	                              [ReferencedSOPClassUID] [nvarchar](64)  NOT NULL,
                               CONSTRAINT [PK_ReferencedPatientSequence_AdmissionID] PRIMARY KEY NONCLUSTERED 
                              (
	                              [AdmissionID] ASC
                              ) ON [PRIMARY]
                              ) ON [PRIMARY]

                              
                              SET ANSI_PADDING OFF
                              
                              
                              ALTER TABLE [dbo].[ReferencedPatientSequence]  WITH CHECK ADD  CONSTRAINT [FK_ReferencedPatientSequence_Visit_AdmissionID] FOREIGN KEY([AdmissionID])
                              REFERENCES [dbo].[Visit] ([AdmissionID])
                              ON UPDATE CASCADE
                              ON DELETE CASCADE
                              
                              ALTER TABLE [dbo].[ReferencedPatientSequence] CHECK CONSTRAINT [FK_ReferencedPatientSequence_Visit_AdmissionID]

                              
                              CREATE TABLE [dbo].[RequestedProcedure](
	                              [AccessionNumber] [nvarchar](16)  NOT NULL,
	                              [RequestedProcedureID] [nvarchar](16)  NOT NULL,
	                              [RequestedProcedureComments] [varchar](max)  NULL,
	                              [StudyInstanceUID] [nvarchar](64)  NOT NULL,
	                              [RequestedProcedureDescription] [nvarchar](64)  NOT NULL,
	                              [RequestedProcedurePriority] [nvarchar](16)  NULL,
	                              [PatientTransportArrangements] [nvarchar](64)  NULL,
	                              [AdmissionID] [nvarchar](64)  NULL,
                               CONSTRAINT [PK_RequestedProcedure_AccessionNumber_RequestedProcedureID] PRIMARY KEY CLUSTERED 
                              (
	                              [AccessionNumber] ASC,
	                              [RequestedProcedureID] ASC
                              ) ON [PRIMARY],
                               CONSTRAINT [Unique_RequestedProcedure_StudyInstanceUID] UNIQUE NONCLUSTERED 
                              (
	                              [StudyInstanceUID] ASC
                              ) ON [PRIMARY]
                              ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

                              
                              SET ANSI_PADDING OFF
                              
                              ALTER TABLE [dbo].[RequestedProcedure]  WITH CHECK ADD  CONSTRAINT [FK_RequestedProcedure_ImagingServiceRequest_AccessionNumber] FOREIGN KEY([AccessionNumber])
                              REFERENCES [dbo].[ImagingServiceRequest] ([AccessionNumber])
                              ON UPDATE CASCADE
                              ON DELETE CASCADE
                              
                              ALTER TABLE [dbo].[RequestedProcedure] CHECK CONSTRAINT [FK_RequestedProcedure_ImagingServiceRequest_AccessionNumber]
                              
                              ALTER TABLE [dbo].[RequestedProcedure]  WITH CHECK ADD  CONSTRAINT [FK_RequestedProcedure_Visit_AdmissionID] FOREIGN KEY([AdmissionID])
                              REFERENCES [dbo].[Visit] ([AdmissionID])
                              ON UPDATE CASCADE
                              
                              ALTER TABLE [dbo].[RequestedProcedure] CHECK CONSTRAINT [FK_RequestedProcedure_Visit_AdmissionID]
                                                            
                              
                              CREATE TABLE [dbo].[RequestedProcedureCodeSequence](
	                              [AccessionNumber] [nvarchar](16)  NOT NULL,
	                              [RequestedProcedureID] [nvarchar](16)  NOT NULL,
	                              [CodeValue] [nvarchar](16)  NOT NULL,
	                              [CodingSchemeDesignator] [nvarchar](16)  NOT NULL,
	                              [CodingSchemeVersion] [nvarchar](16)  NULL,
	                              [CodeMeaning] [nvarchar](64)  NULL,
                               CONSTRAINT [PK_RequestedProcedureCodeSequence_AccessionNumber_RequestedProcedureID] PRIMARY KEY CLUSTERED 
                              (
	                              [AccessionNumber] ASC,
	                              [RequestedProcedureID] ASC
                              ) ON [PRIMARY]
                              ) ON [PRIMARY]

                              
                              SET ANSI_PADDING OFF
                              
                              
                              ALTER TABLE [dbo].[RequestedProcedureCodeSequence]  WITH CHECK ADD  CONSTRAINT [FK_RequestedProcedureCodeSequence_RequestedProcedure_AccessionNumber_RequestedProcedureID] FOREIGN KEY([AccessionNumber], [RequestedProcedureID])
                              REFERENCES [dbo].[RequestedProcedure] ([AccessionNumber], [RequestedProcedureID])
                              ON UPDATE CASCADE
                              ON DELETE CASCADE
                              
                              ALTER TABLE [dbo].[RequestedProcedureCodeSequence] CHECK CONSTRAINT [FK_RequestedProcedureCodeSequence_RequestedProcedure_AccessionNumber_RequestedProcedureID]
                              
                              CREATE TABLE [dbo].[NamesOfIntendedRecipientsOfResults](
	                              [AutoNumber] [bigint] IDENTITY(0,1) NOT NULL,
	                              [AccessionNumber] [nvarchar](16)  NOT NULL,
	                              [RequestedProcedureID] [nvarchar](16)  NOT NULL,
	                              [FamilyNameOfIntendedRecipientOfResults] [nvarchar](64)  NULL,
	                              [GivenNameOfIntendedRecipientOfResults] [nvarchar](63)  NULL,
	                              [MiddleNameOfIntendedRecipientOfResults] [nvarchar](62)  NULL,
	                              [NamePrefixOfIntendedRecipientOfResults] [nvarchar](61)  NULL,
	                              [NameSuffixOfIntendedRecipientOfResults] [nvarchar](60)  NULL,
                               CONSTRAINT [PK_NamesOfIntendedRecipientsOfResults_AccessionNumber_RequestedProcedureID] PRIMARY KEY NONCLUSTERED 
                              (
	                              [AutoNumber] ASC,
	                              [AccessionNumber] ASC,
	                              [RequestedProcedureID] ASC
                              ) ON [PRIMARY],
                               CONSTRAINT [Unique_NamesofIntendedRecipientsofResults_NameOfIntendedRecipientOfResults] UNIQUE NONCLUSTERED 
                              (
	                              [AccessionNumber] ASC,
	                              [RequestedProcedureID] ASC,
	                              [FamilyNameOfIntendedRecipientOfResults] ASC,
	                              [GivenNameOfIntendedRecipientOfResults] ASC,
	                              [MiddleNameOfIntendedRecipientOfResults] ASC,
	                              [NamePrefixOfIntendedRecipientOfResults] ASC,
	                              [NameSuffixOfIntendedRecipientOfResults] ASC
                              ) ON [PRIMARY]
                              ) ON [PRIMARY]

                              
                              SET ANSI_PADDING OFF
                              
                              ALTER TABLE [dbo].[NamesOfIntendedRecipientsOfResults]  WITH CHECK ADD  CONSTRAINT [FK_NamesOfIntendedRecipientsOfResults_RequestedProcedure_AccessionNumber_RequestedProcedureID] FOREIGN KEY([AccessionNumber], [RequestedProcedureID])
                              REFERENCES [dbo].[RequestedProcedure] ([AccessionNumber], [RequestedProcedureID])
                              ON UPDATE CASCADE
                              ON DELETE CASCADE
                              
                              ALTER TABLE [dbo].[NamesOfIntendedRecipientsOfResults] CHECK CONSTRAINT [FK_NamesOfIntendedRecipientsOfResults_RequestedProcedure_AccessionNumber_RequestedProcedureID]

                              CREATE TABLE [dbo].[ReferencedStudySequence](
	                              [AccessionNumber] [nvarchar](16)  NOT NULL,
	                              [RequestedProcedureID] [nvarchar](16)  NOT NULL,
	                              [ReferencedSOPInstanceUID] [nvarchar](64)  NOT NULL,
	                              [ReferencedSOPClassUID] [nvarchar](64)  NOT NULL,
	                              [OrderNumber] [bigint] NULL,
                               CONSTRAINT [PK_ReferencedStudySequence_AccessionNumber_RequestedProcedureID_ReferencedSOPInstanceUID] PRIMARY KEY NONCLUSTERED 
                              (
	                              [AccessionNumber] ASC,
	                              [RequestedProcedureID] ASC,
	                              [ReferencedSOPInstanceUID] ASC
                              ) ON [PRIMARY]
                              ) ON [PRIMARY]

                              
                              SET ANSI_PADDING OFF
                              
                              
                              ALTER TABLE [dbo].[ReferencedStudySequence]  WITH CHECK ADD  CONSTRAINT [FK_ReferencedStudySequence_RequestedProcedure_AccessionNumber_RequestedProcedureID] FOREIGN KEY([AccessionNumber], [RequestedProcedureID])
                              REFERENCES [dbo].[RequestedProcedure] ([AccessionNumber], [RequestedProcedureID])
                              ON UPDATE CASCADE
                              ON DELETE CASCADE
                              
                              ALTER TABLE [dbo].[ReferencedStudySequence] CHECK CONSTRAINT [FK_ReferencedStudySequence_RequestedProcedure_AccessionNumber_RequestedProcedureID]                              
                              
                              CREATE TABLE [dbo].[ScheduledProcedureStep](
	                              [ScheduledProcedureStepID] [nvarchar](16)  NOT NULL,
	                              [AccessionNumber] [nvarchar](16)  NOT NULL,
	                              [RequestedProcedureID] [nvarchar](16)  NOT NULL,
	                              [ScheduledProcedureStepLocation] [nvarchar](16)  NULL,
	                              [ScheduledProcedureStepStartDate_Time] [datetime] NOT NULL,
	                              [ScheduledPerformingPhysicianNameFamilyName] [nvarchar](64)  NULL,
	                              [ScheduledPerformingPhysicianNameGivenName] [nvarchar](63)  NULL,
	                              [ScheduledPerformingPhysicianNameMiddleName] [nvarchar](62)  NULL,
	                              [ScheduledPerformingPhysicianNamePrefix] [nvarchar](61)  NULL,
	                              [ScheduledPerformingPhysicianNameSuffix] [nvarchar](60)  NULL,
	                              [Modality] [nvarchar](16)  NOT NULL,
	                              [ScheduledProcedureStepDescription] [nvarchar](64)  NOT NULL,
	                              [Pre_Medication] [nvarchar](64)  NULL,
	                              [RequestedContrastAgent] [nvarchar](64)  NULL,
                               CONSTRAINT [PK_ScheduledProcedureStep_ScheduledProcedureStepID] PRIMARY KEY CLUSTERED 
                              (
	                              [ScheduledProcedureStepID] ASC
                              ) ON [PRIMARY]
                              ) ON [PRIMARY]

                              
                              SET ANSI_PADDING OFF
                              
                              ALTER TABLE [dbo].[ScheduledProcedureStep]  WITH CHECK ADD  CONSTRAINT [FK_ScheduledProcedureStep_ModalityList] FOREIGN KEY([Modality])
                              REFERENCES [dbo].[ModalityList] ([Modality])
                              
                              ALTER TABLE [dbo].[ScheduledProcedureStep] CHECK CONSTRAINT [FK_ScheduledProcedureStep_ModalityList]
                              
                              ALTER TABLE [dbo].[ScheduledProcedureStep]  WITH CHECK ADD  CONSTRAINT [FK_ScheduledProcedureStep_RequestedProcedure_AccessionNumber_RequestedProcedureID] FOREIGN KEY([AccessionNumber], [RequestedProcedureID])
                              REFERENCES [dbo].[RequestedProcedure] ([AccessionNumber], [RequestedProcedureID])
                              ON UPDATE CASCADE
                              ON DELETE CASCADE
                              
                              ALTER TABLE [dbo].[ScheduledProcedureStep] CHECK CONSTRAINT [FK_ScheduledProcedureStep_RequestedProcedure_AccessionNumber_RequestedProcedureID]
                              
                              CREATE TABLE [dbo].[ScheduledProtocolCodeSequence](
	                              [ScheduledProcedureStepID] [nvarchar](16)  NOT NULL,
	                              [CodeValue] [nvarchar](16)  NOT NULL,
	                              [CodingSchemeDesignator] [nvarchar](16)  NOT NULL,
	                              [CodeMeaning] [nvarchar](64)  NULL,
	                              [CodingSchemeVersion] [nvarchar](16)  NULL,
	                              [OrderNumber] [bigint] NULL,
                               CONSTRAINT [PK_ScheduledProtocolCodeSequence_ScheduledProcedureStepID_CodeValue_CodingSchemeDesignator] PRIMARY KEY NONCLUSTERED 
                              (
	                              [ScheduledProcedureStepID] ASC,
	                              [CodeValue] ASC,
	                              [CodingSchemeDesignator] ASC
                              ) ON [PRIMARY]
                              ) ON [PRIMARY]

                              
                              SET ANSI_PADDING OFF
                              
                              
                              ALTER TABLE [dbo].[ScheduledProtocolCodeSequence]  WITH CHECK ADD  CONSTRAINT [FK_ScheduledProtocolCodeSequence_ScheduledProcedureStep_ScheduledProcedureStepID] FOREIGN KEY([ScheduledProcedureStepID])
                              REFERENCES [dbo].[ScheduledProcedureStep] ([ScheduledProcedureStepID])
                              ON UPDATE CASCADE
                              ON DELETE CASCADE
                              
                              ALTER TABLE [dbo].[ScheduledProtocolCodeSequence] CHECK CONSTRAINT [FK_ScheduledProtocolCodeSequence_ScheduledProcedureStep_ScheduledProcedureStepID]
                              
                              CREATE TABLE [dbo].[ScheduledStationAETitles](
	                              [ScheduledProcedureStepID] [nvarchar](16)  NOT NULL,
	                              [ScheduledStationAETitle] [nvarchar](16)  NOT NULL,
                               CONSTRAINT [PK_ScheduledStationAETitles_ScheduledProcedureStepID_ScheduledStationAETitle] PRIMARY KEY CLUSTERED 
                              (
	                              [ScheduledProcedureStepID] ASC,
	                              [ScheduledStationAETitle] ASC
                              ) ON [PRIMARY]
                              ) ON [PRIMARY]

                              
                              SET ANSI_PADDING OFF
                              
                              ALTER TABLE [dbo].[ScheduledStationAETitles]  WITH CHECK ADD  CONSTRAINT [FK_ScheduledStationAETitles_ScheduledProcedureStep_ScheduledProcedureStepID] FOREIGN KEY([ScheduledProcedureStepID])
                              REFERENCES [dbo].[ScheduledProcedureStep] ([ScheduledProcedureStepID])
                              ON UPDATE CASCADE
                              ON DELETE CASCADE
                              
                              ALTER TABLE [dbo].[ScheduledStationAETitles] CHECK CONSTRAINT [FK_ScheduledStationAETitles_ScheduledProcedureStep_ScheduledProcedureStepID]
                              
                              CREATE TABLE [dbo].[ScheduledStationNames](
	                              [ScheduledProcedureStepID] [nvarchar](16)  NOT NULL,
	                              [ScheduledStationName] [nvarchar](16)  NOT NULL,
                               CONSTRAINT [PK_ScheduledStationNames_ScheduledProcedureStepID_ScheduledStationName] PRIMARY KEY CLUSTERED 
                              (
	                              [ScheduledProcedureStepID] ASC,
	                              [ScheduledStationName] ASC
                              ) ON [PRIMARY]
                              ) ON [PRIMARY]

                              
                              SET ANSI_PADDING OFF
                              
                              ALTER TABLE [dbo].[ScheduledStationNames]  WITH CHECK ADD  CONSTRAINT [FK_ScheduledStationNames_ScheduledProcedureStep_ScheduledProcedureStepID] FOREIGN KEY([ScheduledProcedureStepID])
                              REFERENCES [dbo].[ScheduledProcedureStep] ([ScheduledProcedureStepID])
                              ON UPDATE CASCADE
                              ON DELETE CASCADE
                              
                              ALTER TABLE [dbo].[ScheduledStationNames] CHECK CONSTRAINT [FK_ScheduledStationNames_ScheduledProcedureStep_ScheduledProcedureStepID]
                              
                              
                              CREATE TABLE [dbo].[PPSInformation](
	                              [MPPSSOPInstanceUID] [nvarchar](64)  NOT NULL,
	                              [PerformedProcedureStepID] [nvarchar](16)  NOT NULL,
	                              [PerformedStationAETitle] [nvarchar](16)  NOT NULL,
	                              [PerformedStationName] [nvarchar](16)  NULL,
	                              [PerformedLocation] [nvarchar](16)  NULL,
	                              [PerformedProcedureStepStartDate] [datetime] NOT NULL,
	                              [PerformedProcedureStepStartTime] [datetime] NOT NULL,
	                              [PerformedProcedureStepStatus] [nvarchar](16)  NOT NULL,
	                              [PerformedProcedureStepDescription] [nvarchar](64)  NULL,
	                              [PerformedProcedureTypeDescription] [nvarchar](64)  NULL,
	                              [PerformedProcedureStepEndDate] [datetime] NULL,
	                              [PerformedProcedureStepEndTime] [datetime] NULL,
	                              [CommentsonthePerformedProcedureStep] [nvarchar](1024)  NULL,
	                              [Modality] [nvarchar](16)  NOT NULL,
	                              [StudyID] [nvarchar](16)  NULL,
	                              [StudyInstanceUID] [nvarchar](64)  NOT NULL,
                               CONSTRAINT [PK_PPSInformation] PRIMARY KEY CLUSTERED 
                              (
	                              [MPPSSOPInstanceUID] ASC
                              ) ON [PRIMARY]
                              ) ON [PRIMARY]

                              
                              SET ANSI_PADDING OFF
                              
                              
                              ALTER TABLE [dbo].[PPSInformation]  WITH CHECK ADD  CONSTRAINT [FK_PPSInformation_Modality] FOREIGN KEY([Modality])
                              REFERENCES [dbo].[ModalityList] ([Modality])
                              
                              ALTER TABLE [dbo].[PPSInformation] CHECK CONSTRAINT [FK_PPSInformation_Modality]
                              
                              ALTER TABLE [dbo].[PPSInformation]  WITH CHECK ADD  CONSTRAINT [FK_PPSInformation_ModalityList] FOREIGN KEY([Modality])
                              REFERENCES [dbo].[ModalityList] ([Modality])
                              
                              ALTER TABLE [dbo].[PPSInformation] CHECK CONSTRAINT [FK_PPSInformation_ModalityList]
                              
                              ALTER TABLE [dbo].[PPSInformation]  WITH CHECK ADD  CONSTRAINT [FK_PPSInformation_PPSStatusList] FOREIGN KEY([PerformedProcedureStepStatus])
                              REFERENCES [dbo].[PPSStatusList] ([Status])
                              
                              ALTER TABLE [dbo].[PPSInformation] CHECK CONSTRAINT [FK_PPSInformation_PPSStatusList]
                              
                              ALTER TABLE [dbo].[PPSInformation]  WITH CHECK ADD  CONSTRAINT [FK_PPSInformation_Status] FOREIGN KEY([PerformedProcedureStepStatus])
                              REFERENCES [dbo].[PPSStatusList] ([Status])
                              
                              ALTER TABLE [dbo].[PPSInformation] CHECK CONSTRAINT [FK_PPSInformation_Status]
                              
                              CREATE TABLE [dbo].[PPSRelationship](
	                              [ItmeOrderNumber] [bigint] NOT NULL,
	                              [MPPSSOPInstanceUID] [nvarchar](64)  NOT NULL,
	                              [ScheduledProcedureStepID] [nvarchar](16)  NULL,
                               CONSTRAINT [PK_PPSRelationship] PRIMARY KEY CLUSTERED 
                              (
	                              [ItmeOrderNumber] ASC,
	                              [MPPSSOPInstanceUID] ASC
                              ) ON [PRIMARY]
                              ) ON [PRIMARY]

                              
                              SET ANSI_PADDING OFF
                              
                              ALTER TABLE [dbo].[PPSRelationship]  WITH CHECK ADD  CONSTRAINT [FK_PPSRelationship_PPSInformation] FOREIGN KEY([MPPSSOPInstanceUID])
                              REFERENCES [dbo].[PPSInformation] ([MPPSSOPInstanceUID])
                              ON UPDATE CASCADE
                              ON DELETE CASCADE
                              
                              ALTER TABLE [dbo].[PPSRelationship] CHECK CONSTRAINT [FK_PPSRelationship_PPSInformation]
                              
                              ALTER TABLE [dbo].[PPSRelationship]  WITH CHECK ADD  CONSTRAINT [FK_PPSRelationship_ScheduledProcedureStep] FOREIGN KEY([ScheduledProcedureStepID])
                              REFERENCES [dbo].[ScheduledProcedureStep] ([ScheduledProcedureStepID])
                              ON UPDATE CASCADE
                              ON DELETE CASCADE
                              
                              ALTER TABLE [dbo].[PPSRelationship] CHECK CONSTRAINT [FK_PPSRelationship_ScheduledProcedureStep]
                              
                              CREATE TABLE [dbo].[PatientInfoforUnscheduledPPS](
	                              [MPPSSOPInstanceUID] [nvarchar](64)  NOT NULL,
	                              [PatientName] [nvarchar](64)  NULL,
	                              [PatientBirthDate] [datetime] NULL,
	                              [PatientSex] [nvarchar](16)  NULL,
	                              [PatientID] [nvarchar](64)  NULL,
	                              [IssuerofPatientID] [nvarchar](64)  NULL,
                               CONSTRAINT [PK_PatientInfoforUnscheduledPPS] PRIMARY KEY CLUSTERED 
                              (
	                              [MPPSSOPInstanceUID] ASC
                              ) ON [PRIMARY]
                              ) ON [PRIMARY]

                              
                              SET ANSI_PADDING OFF
                              
                              ALTER TABLE [dbo].[PatientInfoforUnscheduledPPS]  WITH CHECK ADD  CONSTRAINT [FK_Patient_relationship_PatientSexList] FOREIGN KEY([PatientSex])
                              REFERENCES [dbo].[PatientSexList] ([PatientSex])
                              
                              ALTER TABLE [dbo].[PatientInfoforUnscheduledPPS] CHECK CONSTRAINT [FK_Patient_relationship_PatientSexList]
                              
                              ALTER TABLE [dbo].[PatientInfoforUnscheduledPPS]  WITH CHECK ADD  CONSTRAINT [FK_PatientInfoforUnscheduledPPSTable_PPSInformation] FOREIGN KEY([MPPSSOPInstanceUID])
                              REFERENCES [dbo].[PPSInformation] ([MPPSSOPInstanceUID])
                              ON UPDATE CASCADE
                              ON DELETE CASCADE
                              
                              ALTER TABLE [dbo].[PatientInfoforUnscheduledPPS] CHECK CONSTRAINT [FK_PatientInfoforUnscheduledPPSTable_PPSInformation]
                              
                              CREATE TABLE [dbo].[ProcedureCodeSequence](
	                              [MPPSSOPInstanceUID] [nvarchar](64)  NOT NULL,
	                              [CodeValue] [nvarchar](16)  NOT NULL,
	                              [CodingSchemeDesignator] [nvarchar](16)  NOT NULL,
	                              [CodingSchemeVersion] [nvarchar](16)  NULL,
	                              [CodeMeaning] [nvarchar](64)  NULL,
	                              [OrderNumber] [bigint] NULL,
                               CONSTRAINT [PK_ProcedureCodeSequenceTable_SOPInstance_CodeValue_CodeDesignator] PRIMARY KEY NONCLUSTERED 
                              (
	                              [MPPSSOPInstanceUID] ASC,
	                              [CodeValue] ASC,
	                              [CodingSchemeDesignator] ASC
                              ) ON [PRIMARY]
                              ) ON [PRIMARY]

                              
                              SET ANSI_PADDING OFF
                              
                              ALTER TABLE [dbo].[ProcedureCodeSequence]  WITH CHECK ADD  CONSTRAINT [FK_ProcedureCodeSequenceTable_SOPInstance] FOREIGN KEY([MPPSSOPInstanceUID])
                              REFERENCES [dbo].[PPSInformation] ([MPPSSOPInstanceUID])
                              ON UPDATE CASCADE
                              ON DELETE CASCADE
                              
                              ALTER TABLE [dbo].[ProcedureCodeSequence] CHECK CONSTRAINT [FK_ProcedureCodeSequenceTable_SOPInstance]
                              
                              CREATE TABLE [dbo].[PPSDiscontinuationReasonCodeSequence](
	                              [MPPSSOPInstanceUID] [nvarchar](64)  NOT NULL,
	                              [CodeValue] [nvarchar](16)  NOT NULL,
	                              [CodingSchemeDesignator] [nvarchar](16)  NOT NULL,
	                              [CodingSchemeVersion] [nvarchar](16)  NULL,
	                              [CodeMeaning] [nvarchar](64)  NULL,
	                              [OrderNumber] [bigint] NULL,
                               CONSTRAINT [PK_PPSDiscontinuationReasonCodeSequenceTabl_SOPInstance_CodeValue_CodeDesignator] PRIMARY KEY NONCLUSTERED 
                              (
	                              [MPPSSOPInstanceUID] ASC,
	                              [CodeValue] ASC,
	                              [CodingSchemeDesignator] ASC
                              ) ON [PRIMARY]
                              ) ON [PRIMARY]

                              
                              SET ANSI_PADDING OFF
                              
                              
                              ALTER TABLE [dbo].[PPSDiscontinuationReasonCodeSequence]  WITH CHECK ADD  CONSTRAINT [FK_PPSDiscontinuationReasonCodeSequenceTabl_SOPInstance] FOREIGN KEY([MPPSSOPInstanceUID])
                              REFERENCES [dbo].[PPSInformation] ([MPPSSOPInstanceUID])
                              ON UPDATE CASCADE
                              ON DELETE CASCADE
                              
                              ALTER TABLE [dbo].[PPSDiscontinuationReasonCodeSequence] CHECK CONSTRAINT [FK_PPSDiscontinuationReasonCodeSequenceTabl_SOPInstance]
                              
                              CREATE TABLE [dbo].[PerformedProtocolCodeSequence](
	                              [MPPSSOPInstanceUID] [nvarchar](64)  NOT NULL,
	                              [CodeValue] [nvarchar](16)  NOT NULL,
	                              [CodingSchemeDesignator] [nvarchar](16)  NOT NULL,
	                              [CodingSchemeVersion] [nvarchar](16)  NULL,
	                              [CodeMeaning] [nvarchar](64)  NULL,
	                              [OrderNumber] [bigint] NULL,
                               CONSTRAINT [PK_PerformedProtocolCodeSequence_SOPInstance_CodeValue_CodeDesignator] PRIMARY KEY NONCLUSTERED 
                              (
	                              [MPPSSOPInstanceUID] ASC,
	                              [CodeValue] ASC,
	                              [CodingSchemeDesignator] ASC
                              ) ON [PRIMARY]
                              ) ON [PRIMARY]

                              
                              SET ANSI_PADDING OFF
                              
                              
                              ALTER TABLE [dbo].[PerformedProtocolCodeSequence]  WITH CHECK ADD  CONSTRAINT [FK_PerformedProtocolCodeSequence_SOPInstance] FOREIGN KEY([MPPSSOPInstanceUID])
                              REFERENCES [dbo].[PPSInformation] ([MPPSSOPInstanceUID])
                              ON UPDATE CASCADE
                              ON DELETE CASCADE
                              
                              ALTER TABLE [dbo].[PerformedProtocolCodeSequence] CHECK CONSTRAINT [FK_PerformedProtocolCodeSequence_SOPInstance]
                              
                              CREATE TABLE [dbo].[PerformedSeriesSequence](
	                              [SeriesInstanceUID] [nvarchar](64)  NOT NULL,
	                              [MPPSSOPInstanceUID] [nvarchar](64)  NOT NULL,
	                              [PerformingPhysicianName] [varchar](max)  NULL,
	                              [ProtocolName] [nvarchar](64)  NOT NULL,
	                              [OperatorName] [varchar](max)  NULL,
	                              [SeriesDescription] [nvarchar](64)  NULL,
	                              [RetrieveAETitle] [varchar](max)  NULL,
	                              [OrderNumber] [bigint] NULL,
                               CONSTRAINT [PK_PerformedSeriesSequence] PRIMARY KEY NONCLUSTERED 
                              (
	                              [SeriesInstanceUID] ASC,
	                              [MPPSSOPInstanceUID] ASC
                              ) ON [PRIMARY]
                              ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

                              
                              SET ANSI_PADDING OFF
                              
                              
                              ALTER TABLE [dbo].[PerformedSeriesSequence]  WITH CHECK ADD  CONSTRAINT [FK_PerformedSeriesSequence_PPSInformation] FOREIGN KEY([MPPSSOPInstanceUID])
                              REFERENCES [dbo].[PPSInformation] ([MPPSSOPInstanceUID])
                              ON UPDATE CASCADE
                              ON DELETE CASCADE
                              
                              ALTER TABLE [dbo].[PerformedSeriesSequence] CHECK CONSTRAINT [FK_PerformedSeriesSequence_PPSInformation]
                              
                              CREATE TABLE [dbo].[ReferencedImageSequence](
	                              [SeriesInstanceUID] [nvarchar](64)  NOT NULL,
	                              [MPPSSOPInstanceUID] [nvarchar](64)  NOT NULL,
	                              [ReferencedSOPInstanceUID] [nvarchar](64)  NOT NULL,
	                              [ReferencedSOPClassUID] [nvarchar](64)  NOT NULL,
	                              [OrderNumber] [bigint] NULL,
                               CONSTRAINT [PK_ReferencedImageSequence_Series_MPPSSop_ReferencedSOP] PRIMARY KEY NONCLUSTERED 
                              (
	                              [SeriesInstanceUID] ASC,
	                              [MPPSSOPInstanceUID] ASC,
	                              [ReferencedSOPInstanceUID] ASC
                              ) ON [PRIMARY]
                              ) ON [PRIMARY]

                              
                              SET ANSI_PADDING OFF
                              
                              
                              ALTER TABLE [dbo].[ReferencedImageSequence]  WITH CHECK ADD  CONSTRAINT [FK_ReferencedImageSequence_Series_MPPSSop] FOREIGN KEY([SeriesInstanceUID], [MPPSSOPInstanceUID])
                              REFERENCES [dbo].[PerformedSeriesSequence] ([SeriesInstanceUID], [MPPSSOPInstanceUID])
                              ON UPDATE CASCADE
                              ON DELETE CASCADE
                              
                              ALTER TABLE [dbo].[ReferencedImageSequence] CHECK CONSTRAINT [FK_ReferencedImageSequence_Series_MPPSSop]
                              
                              CREATE TABLE [dbo].[ReferencedNonImageCompositeSequence](
	                              [SeriesInstanceUID] [nvarchar](64)  NOT NULL,
	                              [MPPSSOPInstanceUID] [nvarchar](64)  NOT NULL,
	                              [ReferencedSOPInstanceUID] [nvarchar](64)  NOT NULL,
	                              [ReferencedSOPClassUID] [nvarchar](64)  NOT NULL,
	                              [OrderNumber] [bigint] NULL,
                               CONSTRAINT [PK_ReferencedNonImageCompositeSequence_Series_MPPSSop_ReferencedSOP] PRIMARY KEY NONCLUSTERED 
                              (
	                              [SeriesInstanceUID] ASC,
	                              [MPPSSOPInstanceUID] ASC,
	                              [ReferencedSOPInstanceUID] ASC
                              ) ON [PRIMARY]
                              ) ON [PRIMARY]

                              
                              SET ANSI_PADDING OFF
                              
                              ALTER TABLE [dbo].[ReferencedNonImageCompositeSequence]  WITH CHECK ADD  CONSTRAINT [FK_ReferencedNonImageCompositeSequence_Series_MPPSSop] FOREIGN KEY([SeriesInstanceUID], [MPPSSOPInstanceUID])
                              REFERENCES [dbo].[PerformedSeriesSequence] ([SeriesInstanceUID], [MPPSSOPInstanceUID])
                              ON UPDATE CASCADE
                              ON DELETE CASCADE
                              
                              ALTER TABLE [dbo].[ReferencedNonImageCompositeSequence] CHECK CONSTRAINT [FK_ReferencedNonImageCompositeSequence_Series_MPPSSop]
      ";
                                          
         
      }
   }
}
