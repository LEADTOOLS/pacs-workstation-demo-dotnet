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
   public static class MyCallbacks
   {
      public static bool MyDatabaseCallback(DatabaseArgs args)
      {
         if (args.state == DatabaseState.Working)
         {
            string message = (args.elapsedMilliseconds / 1000) + " seconds";
            MyLogger.ShowWaiting(message);
         }
         else if (args.state == DatabaseState.Finished)
         {
            MyLogger.ShowFinished();
         }
         return true;
      }
   }

   public class DicomStorageServerSqlInstaller
   {
      public static void InstallDatabase ( string connectionString ) 
      {
         string message;
         SqlConnectionStringBuilder csB = new SqlConnectionStringBuilder ( connectionString ) ;
         if (SqlUtilities.DatabaseExist(csB))
         {
            message = string.Format("--- Deleting Previous Database [{0}]...", csB.InitialCatalog);
            MyLogger.ShowMessage(message);
            SqlUtilities.DeleteDbIfExists(csB, 1000, MyCallbacks.MyDatabaseCallback);
         }

         message = string.Format("--- Creating New Database [{0}]...", csB.InitialCatalog);
         MyLogger.ShowMessage(message);

         SqlUtilities.CreateDatabase ( csB, 1000, MyCallbacks.MyDatabaseCallback) ;
         
         CreateSqlTables ( connectionString ) ;
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

               // Create Tables
               command = new SqlCommand(DatabaseScripts.StorageServerDatabase, connection);
               command.Transaction = transaction;
               command.ExecuteNonQuery();
               
               // media creation -- only in LEAD Toolkit
               // command = new SqlCommand(MediaCreationSqlInstaller.DatabaseScripts.MediaCreationDatabase, connection);
               // command.Transaction = transaction;
               // command.ExecuteNonQuery();

               // Worklist creation -- only in LEAD Toolkit
               // command = new SqlCommand(WorklistSqlInstaller.DatabaseScripts.WorklistDatabase, connection);
               // command.Transaction = transaction;
               // command.ExecuteNonQuery();
               
               // version
               VersionSqlInstaller.CreateVersionTable(connection, transaction);
               VersionSqlInstaller.InsertVersionTable(connection, transaction, ConfigurationData.DataAccessLayerVersionNames.Logging, 1, 6);
#if (LEADTOOLS_V20_OR_LATER)
               VersionSqlInstaller.InsertVersionTable(connection, transaction, ConfigurationData.DataAccessLayerVersionNames.AeManagement, 1, 4);
#elif (LEADTOOLS_V19_OR_LATER_MEDICAL_CLIENT_FRIENDLY_NAME) || (LEADTOOLS_V19_OR_LATER)
               VersionSqlInstaller.InsertVersionTable(connection, transaction, ConfigurationData.DataAccessLayerVersionNames.AeManagement, 1, 1);
#else
               VersionSqlInstaller.InsertVersionTable(connection, transaction, ConfigurationData.DataAccessLayerVersionNames.AeManagement, 1, 0);
#endif
               VersionSqlInstaller.InsertVersionTable(connection, transaction, ConfigurationData.DataAccessLayerVersionNames.Forward, 1, 0);

#if (LEADTOOLS_V19_OR_LATER)
               VersionSqlInstaller.InsertVersionTable(connection, transaction, ConfigurationData.DataAccessLayerVersionNames.Options, 1, 2);
#endif // #if (LEADTOOLS_V19_OR_LATER)

#if (LEADTOOLS_V20_OR_LATER)
               VersionSqlInstaller.InsertVersionTable(connection, transaction, ConfigurationData.DataAccessLayerVersionNames.UserManagement, 1, 4);
#else
               VersionSqlInstaller.InsertVersionTable(connection, transaction, ConfigurationData.DataAccessLayerVersionNames.UserManagement, 1, 1);
#endif

#if (LEADTOOLS_V19_OR_LATER)
               VersionSqlInstaller.InsertVersionTable(connection, transaction, ConfigurationData.DataAccessLayerVersionNames.Storage, 1, 13);
#elif (LEADTOOLS_V19_OR_LATER_MEDICAL_EXTERNAL_STORE) || (LEADTOOLS_V19_OR_LATER)
               VersionSqlInstaller.InsertVersionTable(connection, transaction, ConfigurationData.DataAccessLayerVersionNames.Storage, 1, 8);
#elif (LEADTOOLS_V19_OR_LATER_STORE_AE_TITLE) || (LEADTOOLS_V18_OR_LATER)
               VersionSqlInstaller.InsertVersionTable(connection, transaction, ConfigurationData.DataAccessLayerVersionNames.Storage, 1, 7);
#else
               VersionSqlInstaller.InsertVersionTable(connection, transaction, ConfigurationData.DataAccessLayerVersionNames.Storage, 1, 6);
#endif // #if (LEADTOOLS_V19_OR_LATER_MEDICAL_EXTERNAL_STORE) || (LEADTOOLS_V19_OR_LATER)
               VersionSqlInstaller.InsertVersionTable(connection, transaction, ConfigurationData.DataAccessLayerVersionNames.Permissions, 1, 6);
               VersionSqlInstaller.InsertVersionTable(connection, transaction, ConfigurationData.DataAccessLayerVersionNames.AePermissions, 1, 0);
               
               VersionSqlInstaller.InsertVersionTable(connection, transaction, ConfigurationData.DataAccessLayerVersionNames.JobsDownload, 1, 1);
               VersionSqlInstaller.InsertVersionTable(connection, transaction, ConfigurationData.DataAccessLayerVersionNames.PatientAccess, 1, 0);
               VersionSqlInstaller.InsertVersionTable(connection, transaction, ConfigurationData.DataAccessLayerVersionNames.ExportLayout, 1, 1);


               command.Dispose ( ) ;
               
               transaction.Commit ( ) ;
            }
            catch ( Exception) 
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
         
         public const string StorageServerDatabase = @"" +
         @"-- Script Date: 10/12/2011 5:05 PM  - Generated by ExportSqlCe version 3.5.1.7
-- Database information:
-- locale identifier: 1033
-- encryption mode: 
-- case sensitive: False
-- Database: D:\Examples\PACSFramework\CS\PACSDatabaseConfigurationDemo\Database\SqlCe\StorageServer.sdf
-- ServerVersion: 3.5.8080.0
-- DatabaseSize: 741376
-- Created: 10/12/2011 4:54 PM

-- User Table information:
-- Number of tables: 27
-- AeInfo: 0 row(s)
-- AePermissions: 0 row(s)
-- AePermissionsList: 3 row(s)
-- AeRelation: -1 row(s)
-- DbVersion: 1 row(s)
-- DICOMServerEventLog: 0 row(s)
-- Forward: 0 row(s)
-- ImageInstance: 0 row(s)
-- Instance: 0 row(s)
-- NameOfPhysicianReadingStudy: 0 row(s)
-- Options: 0 row(s)
-- OtherPatientIDs: 0 row(s)
-- OtherPatientNames: 0 row(s)
-- OtherStudyNumbers: 0 row(s)
-- PasswordHistory: 0 row(s)
-- Patient: 0 row(s)
-- ProcedureCodeSequence: 0 row(s)
-- ReferencedImages: 0 row(s)
-- ReferencedPatientSequence: 0 row(s)
-- ReferencedPerformedProcedureStepSequence: 0 row(s)
-- ReferencedStudySequence: 0 row(s)
-- RequestAttributeSequence: 0 row(s)
-- Series: 0 row(s)
-- Study: 0 row(s)
-- UserPermissions: 0 row(s)
-- UserPermissionsList: 7 row(s)
-- Users: 0 row(s)

CREATE TABLE [AeInfo] (
  [Address] nvarchar(46) NULL
, [AETitle] nvarchar(16) NOT NULL
, [Port] int NULL
, [SecurePort] int NULL
, [VerifyAddress] bit NULL
, [Mask] nvarchar(46) NULL
, [VerifyMask] bit NULL
, [UseSecurePort] bit NULL" + 
#if (LEADTOOLS_V19_OR_LATER_MEDICAL_CLIENT_FRIENDLY_NAME) || (LEADTOOLS_V19_OR_LATER)
@",[FriendlyName] nvarchar(100) NULL" +
#endif
#if (LEADTOOLS_V20_OR_LATER)
@",PortUsage [int] Not NULL Default 1" +
@",LastAccessDate [datetime] NULL Default NULL" +
#endif
@") " +

@"CREATE TABLE [AePermissions] (
  [UserName] nvarchar(16) NOT NULL
, [Permission] nvarchar(100) NOT NULL
)

CREATE TABLE [AePermissionsList] (
  [Permission] nvarchar(100) NOT NULL
, [Description] nvarchar(100) NULL
)

CREATE TABLE [AeRelation] (
  [AETitle] nvarchar(16) NOT NULL
, [RelatedAETitle] nvarchar(16) NOT NULL
, [Relation] int NULL
)

CREATE TABLE [DICOMServerEventLog] (
  [EventID] bigint NOT NULL  IDENTITY (91,1)
, [ServerAETitle] nvarchar(16) NULL
, [ServerIPAddress] nvarchar(50) NULL
, [ServerPort] int NULL
, [ClientAETitle] nvarchar(16) NULL
, [ClientHostAddress] nvarchar(50) NULL
, [ClientPort] int NULL
, [Command] nvarchar(15) NOT NULL DEFAULT ((0))
, [EventDateTime] datetime NULL
, [Type] nvarchar(15) NOT NULL
, [MessageDirection] nvarchar(10) NOT NULL
, [Description] nvarchar(max) NULL
, [Dataset] varbinary(max) NULL
, [CustomInformation] nvarchar(max) NULL
, [DatasetPath] nvarchar(400) NULL
, [CustomType] nvarchar(15) NULL
)

CREATE TABLE [Forward] (
  [SOPInstanceUID] nvarchar(64) NOT NULL
, [ForwardDate] datetime NULL
, [ExpireDate] datetime NULL
)

CREATE TABLE [ImageInstance] (
  [SOPInstanceUID] nvarchar(64) NOT NULL
, [ImageRows] int NULL
, [ImageColumns] int NULL
, [BitsAllocated] int NULL
, [NumberOfFrames] int NULL" +
#if (LEADTOOLS_V19_OR_LATER)
    @", [EchoNumber] [int] NULL" +
    @", [FrameOfReferenceUID] [nvarchar](64) NULL" +
    @", [SequenceName] [nvarchar](16) NULL" +
    @", [ImagePositionPatient] [nvarchar](256) NULL" +
    @", [ImageOrientationPatient] [nvarchar](256) NULL" +
    @", [PixelSpacing] [nvarchar](256) NULL " + 
#endif
@")

CREATE TABLE [Instance] (
  [SOPInstanceUID] nvarchar(64) NOT NULL
, [InstanceNumber] int NULL
, [ReferencedFile] nvarchar(400) NULL
, [TransferSyntax] nvarchar(64) NULL
, [SOPClassUID] nvarchar(64) NULL
, [StationName] nvarchar(16) NULL
, [SeriesInstanceUID] nvarchar(64) NOT NULL
, [ReceiveDate] datetime NULL
, [RetrieveAETitle] nvarchar(16) NULL" + 
#if (LEADTOOLS_V19_OR_LATER_MEDICAL_EXTERNAL_STORE) || (LEADTOOLS_V19_OR_LATER)
@", [StoreToken] [nvarchar](400) NULL" + 
@", [ExternalStoreGuid] [nvarchar](400) NULL" + 
#endif // #if (LEADTOOLS_V19_OR_LATER_MEDICAL_EXTERNAL_STORE) || (LEADTOOLS_V19_OR_LATER)
#if (LEADTOOLS_V19_OR_LATER_STORE_AE_TITLE) || (LEADTOOLS_V18_OR_LATER)
@", [StoreAETitle] [nvarchar](16) NULL " +
#endif
@")

CREATE TABLE [NameOfPhysicianReadingStudy] (
  [PhysicianFamilyName] nvarchar(64) NULL
, [PhysicianGivenName] nvarchar(63) NULL
, [PhysicianMiddleName] nvarchar(62) NULL
, [PhysicianNamePrefix] nvarchar(61) NULL
, [PhysicianNameSuffix] nvarchar(60) NULL
, [StudyInstanceUID] nvarchar(64) NOT NULL
)

CREATE TABLE [Options] (
  [Key] nvarchar(100) NOT NULL
, [Value] nvarchar(max) NULL
)

CREATE TABLE [OtherPatientIDs] (
  [PatientID] nvarchar(64) NOT NULL
, [OtherPatientID] nvarchar(64) NOT NULL
)

CREATE TABLE [OtherPatientNames] (
  [FamilyName] nvarchar(64) NULL
, [GivenName] nvarchar(63) NULL
, [MiddleName] nvarchar(62) NULL
, [NamePrefix] nvarchar(61) NULL
, [NameSuffix] nvarchar(60) NULL
, [PatientID] nvarchar(64) NOT NULL
, [OrderNumber] int NOT NULL  IDENTITY (1,1)
)

CREATE TABLE [OtherStudyNumbers] (
  [OtherStudyNumber] numeric(18,0) NOT NULL
, [StudyInstanceUID] nvarchar(64) NOT NULL
)

CREATE TABLE [PasswordHistory] (
  [User] nvarchar(16) NOT NULL
, [Password] nvarchar(64) NOT NULL
, [AddDate] datetime NULL
, [Id] bigint NOT NULL  IDENTITY (1,1)
)

CREATE TABLE [Patient] (
  [PatientID] nvarchar(64) NOT NULL
, [FamilyName] nvarchar(64) NULL
, [GivenName] nvarchar(63) NULL
, [MiddleName] nvarchar(62) NULL
, [NamePrefix] nvarchar(61) NULL
, [NameSuffix] nvarchar(60) NULL
, [BirthDate] datetime NULL
, [Sex] nvarchar(50) NULL
, [EthnicGroup] nvarchar(50) NULL
, [Comments] nvarchar(max) NULL
, [RetrieveAETitle] nvarchar(16) NULL
, [IssuerOfPatientID] nvarchar(64) NULL
)

CREATE TABLE [ProcedureCodeSequence] (
  [StudyInstanceUID] nvarchar(64) NOT NULL
, [CodeValue] nvarchar(16) NOT NULL
, [CodingSchemaDesignator] nvarchar(16) NOT NULL
, [CodeMeaning] nvarchar(64) NULL
, [CodingSchemaVersion] nvarchar(16) NULL
)

CREATE TABLE [ReferencedImages] (
  [SOPInstanceUID] nvarchar(64) NOT NULL
, [AutoNumber] int NOT NULL  IDENTITY (1,1)
, [Width] int NOT NULL DEFAULT ((0))
, [Height] int NOT NULL DEFAULT ((0))
, [Format] nvarchar(50) NOT NULL
, [QualityFactor] int NOT NULL DEFAULT ((0))
, [FrameIndex] int NOT NULL DEFAULT ((0))
, [ReferencedFile] nvarchar(400) NOT NULL
, [Thumbnail] bit NOT NULL DEFAULT ((0))
)

CREATE TABLE [ReferencedPatientSequence] (
  [PatientID] nvarchar(64) NOT NULL
, [ReferencedSOPInstanceUID] nvarchar(64) NOT NULL
, [ReferencedSOPClassUID] nvarchar(64) NOT NULL
)

CREATE TABLE [ReferencedPerformedProcedureStepSequence] (
  [SeriesInstanceUID] nvarchar(64) NOT NULL
, [ReferencedSOPClassUID] nvarchar(64) NOT NULL
, [ReferencedSOPInstanceUID] nvarchar(64) NOT NULL
)

CREATE TABLE [ReferencedStudySequence] (
  [StudyInstanceUID] nvarchar(64) NOT NULL
, [ReferencedSOPInstanceUID] nvarchar(64) NOT NULL
, [ReferencedSOPClassUID] nvarchar(64) NOT NULL
)

CREATE TABLE [RequestAttributeSequence] (
  [SeriesInstanceUID] nvarchar(64) NOT NULL
, [RequestedProcedureID] nvarchar(16) NOT NULL
, [ScheduledProcedureStepID] nvarchar(16) NOT NULL
)

CREATE TABLE [Series] (
  [SeriesInstanceUID] nvarchar(64) NOT NULL
, [Modality] nvarchar(16) NULL
, [SeriesNumber] int NULL
, [SeriesDate] datetime NULL
, [SeriesDescription] nvarchar(64) NULL
, [InstitutionName] nvarchar(64) NULL
, [StudyInstanceUID] nvarchar(64) NOT NULL
, [ReceiveDate] datetime NULL
, [RetrieveAETitle] nvarchar(16) NULL
, [PerformedProcedureStepID] nvarchar(16) NULL
, [PerformedProcedureStartDate] datetime NULL
, [BodyPartExamined] nvarchar(16) NULL
, [Laterality] nvarchar(16) NULL
, [ProtocolName] nvarchar(64) NULL
)

CREATE TABLE [Study] (
  [StudyInstanceUID] nvarchar(64) NOT NULL
, [StudyDate] datetime NULL
, [AccessionNumber] nvarchar(50) NULL
, [StudyID] nvarchar(50) NULL
, [ReferDrFamilyName] nvarchar(64) NULL
, [ReferDrGivenName] nvarchar(63) NULL
, [ReferDrMiddleName] nvarchar(62) NULL
, [ReferDrNamePrefix] nvarchar(61) NULL
, [ReferDrNameSuffix] nvarchar(60) NULL
, [StudyDescription] nvarchar(64) NULL
, [AdmittingDiagnosesDesc] nvarchar(64) NULL
, [PatientAge] nvarchar(4) NULL
, [PatientSize] numeric(18,0) NULL
, [PatientWeight] numeric(18,0) NULL
, [Occupation] nvarchar(16) NULL
, [AdditionalPatientHistory] nvarchar(max) NULL
, [InterpretationAuthor] nvarchar(64) NULL
, [PatientID] nvarchar(64) NULL
, [RetrieveAETitle] nvarchar(16) NULL
)

CREATE TABLE [PresentationInstance] (
	[SOPInstanceUID] [nvarchar] (64) NOT NULL ,
	[ContentLabel] [nvarchar] (50) NULL ,
	[CreationDate] [datetime] NULL ,
	[ContentDescription] [nvarchar] (64) NULL ,
	[ContentCreatorFamilyName] [nvarchar] (64) NULL ,
	[ContentCreatorGivenName] [nvarchar] (63) NULL ,
	[ContentCreatorMiddleName] [nvarchar] (62) NULL ,
	[ContentCreatorNamePrefix] [nvarchar] (61) NULL ,
	[ContentCreatorNameSuffix] [nchar] (60) NULL 
)

CREATE TABLE [ReferencedSeriesSequence] (
	[SeriesInstanceUID] [nvarchar] (64) NOT NULL ,
	[SOPInstanceUID] [nvarchar] (64) NOT NULL 
)

CREATE TABLE [ReferencedImageSequence] (
	[SeriesInstanceUID] [nvarchar] (64) NOT NULL ,
	[SOPInstanceUID] [nvarchar] (64) NOT NULL ,
	[ReferencedSOPInstanceUID] [nvarchar] (64) NOT NULL ,
	[ReferencedSOPClassUID] [nvarchar] (64) NOT NULL ,
	[ItemIndex] [numeric](18, 0) IDENTITY (1, 1) NOT NULL 
) 

CREATE TABLE [UserPermissions] (
  [UserName] nvarchar(16) NOT NULL
, [Permission] nvarchar(50) NOT NULL
)

CREATE TABLE [UserPermissionsList] (
  [Permission] nvarchar(50) NOT NULL
, [Description] nvarchar(100) NULL
)

CREATE TABLE [Users] (
  [UserName] nvarchar(16) NOT NULL
, [Password] nvarchar(64) NOT NULL
, [IsAdmin] bit NOT NULL DEFAULT ((0))
, [Expires] datetime NULL
, [UseCardReader] [bit] NOT NULL,
  [FriendlyName] [nvarchar](256) NULL," +
#if (LEADTOOLS_V20_OR_LATER)
   @"[UserType] [nvarchar](50) NULL, " +
   @"[ExtendedName] [nvarchar](360) NULL, " +
#endif
@")

CREATE TABLE [RolesList](
	[RoleName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](100) NULL,
 CONSTRAINT [PK_RolesList] PRIMARY KEY CLUSTERED 
(
   [RoleName] ASC
) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [UserRoles](
	[UserName] [nvarchar](16) NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[UserName] ASC,
	[RoleName] ASC
)ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[RolesPermissions](
	[RoleName] [nvarchar](50) NOT NULL,
	[Permission] [nvarchar](50) NOT NULL
) ON [PRIMARY]

CREATE TABLE [JobsQueue](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[type] [nvarchar](100) NOT NULL,
	[object] [varchar](max) NOT NULL,
	[status] [int] NOT NULL,
	[completedstatus] [int] NOT NULL,
	[timestamp] [datetime] NULL,
	[error] [varchar](max) NULL,
	[userdata] [varchar](max) NULL,
	[retries] [int] NOT NULL,
	[owner] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_jobsQueue] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


CREATE TABLE [dbo].[RolesAeMapping](
	[RoleName] [nvarchar](50) NOT NULL,
	[AETitle] [nvarchar](16) NOT NULL
) ON [PRIMARY]

CREATE TABLE [RolesPatients] (
            [RoleID] [varchar] (128) NULL ,
            [PatientID] [nvarchar] (64) NULL ,
            [UserID] [varchar] (128) NULL ,
            [ID] [int] IDENTITY (1, 1) NOT NULL ,
            CONSTRAINT [PK_RolesPatients] PRIMARY KEY  CLUSTERED 
            (
               [ID]
            )  ON [PRIMARY] ,

) ON [PRIMARY]" +

#if (LEADTOOLS_V20_OR_LATER)
@"
CREATE TABLE [dbo].[ExportLayout](
	[SOPInstanceUID] [nvarchar](64) NOT NULL,
	[ExportDate] [datetime] NULL,
	[ReferencedFile] [nvarchar](400) NULL,
 CONSTRAINT [PK_ExportLayout] PRIMARY KEY CLUSTERED 
   (
	[SOPInstanceUID] ASC
   )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
   ) ON [PRIMARY]
" +
#endif // #if (LEADTOOLS_V20_OR_LATER)

#if (LEADTOOLS_V19_OR_LATER_MEDICAL_EXTERNAL_STORE) || (LEADTOOLS_V19_OR_LATER)
@"
CREATE TABLE [dbo].[ExternalStore](
	[SOPInstanceUID] [nvarchar](64) NOT NULL,
	[StoreDate] [datetime] NULL,
	[ExpireDate] [datetime] NULL,
	[StoreToken] [nvarchar](400) NULL,
	[ExternalStoreGuid] [nvarchar](400) NULL,
 CONSTRAINT [PK_ExternalStore] PRIMARY KEY CLUSTERED 
(
[SOPInstanceUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
" + 
#endif // #if (LEADTOOLS_V19_OR_LATER_MEDICAL_EXTERNAL_STORE) || (LEADTOOLS_V19_OR_LATER)

#if (LEADTOOLS_V19_OR_LATER)
 @"
                IF NOT EXISTS (   SELECT name FROM sys.stats WHERE object_id = OBJECT_ID( 'dbo.HangingProtocol'))
                BEGIN
                CREATE TABLE [dbo].[HangingProtocol] (
                   [SOPClassUID] [nvarchar](64) NOT NULL,
                   [SOPInstanceUID] [nvarchar](64) NOT NULL,
                   [Name] [nvarchar](16) NOT NULL,
                   [Description] [nvarchar](64) NOT NULL,
                   [Level] [nvarchar](16) NOT NULL,
                   [Creator] [nvarchar](64) NOT NULL,
                   [CreationDateTime] [datetime] NULL,
                   [UserGroupName] [nvarchar](64) NULL,
                   [NumberOfPriorsReferenced] [int] NOT NULL,
                   [SpecificCharacterSet] [nvarchar](16) NULL,
                   [ReferencedFile] [nvarchar](400) PRIMARY KEY CLUSTERED 
                   ([SOPInstanceUID] ASC) WITH (
                      PAD_INDEX = OFF,
                      STATISTICS_NORECOMPUTE = OFF,
                      IGNORE_DUP_KEY = OFF,
                      ALLOW_ROW_LOCKS = ON,
                      ALLOW_PAGE_LOCKS = ON
                      ) ON [PRIMARY]
                                   ) ON [PRIMARY]
                END
                
                IF NOT EXISTS (   SELECT name FROM sys.stats WHERE object_id = OBJECT_ID( 'dbo.HangingProtocolDefinitonSequence'))
                BEGIN
                  CREATE TABLE [dbo].[HangingProtocolDefinitonSequence](
                     [DefinitionSequenceGuid] [nvarchar](64) NOT NULL,
                     [SOPInstanceUID] [nvarchar](64) NOT NULL,
                     [Modality] [nvarchar](16) NOT NULL,
                     [Laterality] [nvarchar](16) NULL,
                     [StudyDescription] [nvarchar](64) NULL,
                     [BodyPartExamined] [nvarchar](16) NULL,
                     [ProtocolName] [nvarchar](64) NULL,
                  CONSTRAINT [PK_DefinitionSequenceGuid] PRIMARY KEY CLUSTERED ([DefinitionSequenceGuid] ASC) WITH (
                     PAD_INDEX = OFF,
                     STATISTICS_NORECOMPUTE = OFF,
                     IGNORE_DUP_KEY = OFF,
                     ALLOW_ROW_LOCKS = ON,
                     ALLOW_PAGE_LOCKS = ON
                    ) ON [PRIMARY]
                   ) ON [PRIMARY] 
                END
                 
                IF NOT EXISTS (SELECT * FROM sys.objects o WHERE o.object_id = object_id(N'[dbo].[FK_HangingProtocolDefinitonSequence_HangingProtocol]') AND OBJECTPROPERTY(o.object_id, N'IsForeignKey') = 1)
                BEGIN
                  ALTER TABLE [dbo].[HangingProtocolDefinitonSequence]  WITH CHECK ADD  CONSTRAINT [FK_HangingProtocolDefinitonSequence_HangingProtocol] FOREIGN KEY ([SOPInstanceUID])
                     REFERENCES [dbo].[HangingProtocol] ([SOPInstanceUID])
                  ON UPDATE CASCADE
                  ON DELETE CASCADE
                END
                
                IF NOT EXISTS (SELECT * FROM sys.objects o WHERE o.object_id = object_id(N'[dbo].[FK_HangingProtocolDefinitonSequence_HangingProtocol]') AND OBJECTPROPERTY(o.object_id, N'IsForeignKey') = 1)    
                BEGIN
                  ALTER TABLE [dbo].[HangingProtocolDefinitonSequence] CHECK CONSTRAINT [FK_HangingProtocolDefinitonSequence_HangingProtocol]
                END
                    
                IF NOT EXISTS (   SELECT name FROM sys.stats WHERE object_id = OBJECT_ID( 'dbo.HangingProtocolAnatomicRegionSequence'))
                BEGIN
                  CREATE TABLE [dbo].[HangingProtocolAnatomicRegionSequence](
                     [DefinitionSequenceGuid] [nvarchar](64) NOT NULL,
                     [CodeValue] [nvarchar](16) NOT NULL,
                     [CodingSchemeDesignator] [nvarchar](16) NOT NULL,  
                     [CodingSchemeVersion] [nvarchar](16) NULL,
                     [CodeMeaning] [nvarchar](64) NOT NULL )
                
                
                  ALTER TABLE [dbo].[HangingProtocolAnatomicRegionSequence]  WITH CHECK ADD  CONSTRAINT [FK_HangingProtocolAnatomicRegionSequence_HangingProtocolDefinitonSequence] FOREIGN KEY ([DefinitionSequenceGuid])
                     REFERENCES [dbo].[HangingProtocolDefinitonSequence] ([DefinitionSequenceGuid])
                     ON UPDATE CASCADE
                     ON DELETE CASCADE
                    
                     ALTER TABLE [dbo].[HangingProtocolAnatomicRegionSequence] CHECK CONSTRAINT [FK_HangingProtocolAnatomicRegionSequence_HangingProtocolDefinitonSequence]
                END
                                  
                IF NOT EXISTS (   SELECT name FROM sys.stats WHERE object_id = OBJECT_ID( 'dbo.HangingProtocolProcedureCodeSequence'))
                BEGIN  
                  CREATE TABLE [dbo].[HangingProtocolProcedureCodeSequence](
                     [DefinitionSequenceGuid] [nvarchar](64) NOT NULL,
                     [CodeValue] [nvarchar](16) NOT NULL,
                     [CodingSchemeDesignator] [nvarchar](16) NOT NULL,  
                     [CodingSchemeVersion] [nvarchar](16) NULL,
                     [CodeMeaning] [nvarchar](64) NOT NULL)
                    
                  ALTER TABLE [dbo].[HangingProtocolProcedureCodeSequence]  WITH CHECK ADD  CONSTRAINT [FK_HangingProtocolProcedureCodeSequence_HangingProtocolDefinitonSequence] FOREIGN KEY ([DefinitionSequenceGuid])
                     REFERENCES [dbo].[HangingProtocolDefinitonSequence] ([DefinitionSequenceGuid])
                     ON UPDATE CASCADE
                     ON DELETE CASCADE
                    
                    ALTER TABLE [dbo].[HangingProtocolProcedureCodeSequence] CHECK CONSTRAINT [FK_HangingProtocolProcedureCodeSequence_HangingProtocolDefinitonSequence]
                END
                  
                IF NOT EXISTS (   SELECT name FROM sys.stats WHERE object_id = OBJECT_ID( 'dbo.HangingProtocolReasonForRequestedProcedureCodeSequence'))
                BEGIN 
                  CREATE TABLE [dbo].[HangingProtocolReasonForRequestedProcedureCodeSequence](
                     [DefinitionSequenceGuid] [nvarchar](64) NOT NULL,
                     [CodeValue] [nvarchar](16) NOT NULL,
                     [CodingSchemeDesignator] [nvarchar](16) NOT NULL,  
                     [CodingSchemeVersion] [nvarchar](16) NULL,
                     [CodeMeaning] [nvarchar](64) NOT NULL)
                    
                  ALTER TABLE [dbo].[HangingProtocolReasonForRequestedProcedureCodeSequence]  WITH CHECK ADD  CONSTRAINT [FK_HangingProtocolReasonForRequestedProcedureCodeSequence_HangingProtocolDefinitonSequence] FOREIGN KEY([DefinitionSequenceGuid])
                     REFERENCES [dbo].[HangingProtocolDefinitonSequence] ([DefinitionSequenceGuid])
                     ON UPDATE CASCADE
                     ON DELETE CASCADE
                    
                  ALTER TABLE [dbo].[HangingProtocolReasonForRequestedProcedureCodeSequence] CHECK CONSTRAINT [FK_HangingProtocolReasonForRequestedProcedureCodeSequence_HangingProtocolDefinitonSequence]
                END
" + 
#endif // (LEADTOOLS_V19_OR_LATER)

 @"
INSERT INTO [AePermissionsList] ([Permission],[Description]) VALUES (N'Overwrite',N'Overwrite')

INSERT INTO [AePermissionsList] ([Permission],[Description]) VALUES (N'Update',N'Update')

INSERT INTO [AePermissionsList] ([Permission],[Description]) VALUES (N'Delete',N'Delete')

INSERT INTO [UserPermissionsList] ([Permission],[Description]) VALUES (N'Admin',N'Allow to administer Storage Server')

INSERT INTO [UserPermissionsList] ([Permission],[Description]) VALUES (N'PatientUpdaterEdit',N'Allow to edit using patient updater')

INSERT INTO [UserPermissionsList] ([Permission],[Description]) VALUES (N'PatientUpdaterDelete',N'Allow to delete using patient updater')

INSERT INTO [UserPermissionsList] ([Permission],[Description]) VALUES (N'PatientUpdaterAdmin',N'Allow to administer patient updater')

INSERT INTO [UserPermissionsList] ([Permission],[Description]) VALUES (N'CanDeleteFromDatabase',N'Allow user to delete from storage database')

INSERT INTO [UserPermissionsList] ([Permission],[Description]) VALUES (N'CanChangeServerSettings',N'Allow user to modify server settings')

INSERT INTO [UserPermissionsList] ([Permission],[Description]) VALUES (N'CanEmptyDatabase',N'Allow user to empty the storage database')

INSERT INTO [UserPermissionsList] ([Permission],[Description]) SELECT 'MWV.CanDownloadImages','Allow users to Move images from remote PACS.' WHERE NOT EXISTS (select [Permission] from [UserPermissionsList] where [Permission]='MWV.CanDownloadImages' )
INSERT INTO [UserPermissionsList] ([Permission],[Description]) SELECT 'MWV.CanRetrieve','Allow users to request DICOM DataSet through the web interface.' WHERE NOT EXISTS (select [Permission] from [UserPermissionsList] where [Permission]='MWV.CanRetrieve' )
INSERT INTO [UserPermissionsList] ([Permission],[Description]) SELECT 'MWV.CanQueryPACS','Allow users to query remote PACS through the web interface.' WHERE NOT EXISTS (select [Permission] from [UserPermissionsList] where [Permission]='MWV.CanQueryPACS' )
INSERT INTO [UserPermissionsList] ([Permission],[Description]) SELECT 'MWV.CanQuery','Allow users to query local images through the web interface.' WHERE NOT EXISTS (select [Permission] from [UserPermissionsList] where [Permission]='MWV.CanQuery' )
INSERT INTO [UserPermissionsList] ([Permission],[Description]) SELECT 'MWV.CanManageUsers','Allow users to manage other users through the web interface.' WHERE NOT EXISTS (select [Permission] from [UserPermissionsList] where [Permission]='MWV.CanManageUsers' )
INSERT INTO [UserPermissionsList] ([Permission],[Description]) SELECT 'MWV.CanManageRoles','Allow users to manage other users through the web interface.' WHERE NOT EXISTS (select [Permission] from [UserPermissionsList] where [Permission]='MWV.CanManageRoles' )
INSERT INTO [UserPermissionsList] ([Permission],[Description]) SELECT 'MWV.CanViewAnnotations', 'Allow users to load annotations.'WHERE NOT EXISTS (select [Permission] from [UserPermissionsList] where [Permission]='MWV.CanViewAnnotations' )
INSERT INTO [UserPermissionsList] ([Permission],[Description]) SELECT 'MWV.CanStore', 'Allow users to save annotations.'WHERE NOT EXISTS (select [Permission] from [UserPermissionsList] where [Permission]='MWV.CanStore' )
INSERT INTO [UserPermissionsList] ([Permission],[Description]) SELECT 'MWV.CanStoreAnnotations', 'Allow users to save annotations.'WHERE NOT EXISTS (select [Permission] from [UserPermissionsList] where [Permission]='MWV.CanStoreAnnotations' )
INSERT INTO [UserPermissionsList] ([Permission],[Description]) SELECT 'MWV.CanDeleteDownloadInfo', 'Allow users to delete jobs in download queue.'WHERE NOT EXISTS (select [Permission] from [UserPermissionsList] where [Permission]='MWV.CanDeleteDownloadInfo' )
INSERT INTO [UserPermissionsList] ([Permission],[Description]) SELECT 'MWV.CanDeleteAnnotations', 'Allow users to save annotations.'WHERE NOT EXISTS (select [Permission] from [UserPermissionsList] where [Permission]='MWV.CanDeleteAnnotations' )
INSERT INTO [UserPermissionsList] ([Permission],[Description]) SELECT 'MWV.CanManageRemotePACS', 'Allow users to manage remote PACS servers.'WHERE NOT EXISTS (select [Permission] from [UserPermissionsList] where [Permission]='MWV.CanManageRemotePACS' )
INSERT INTO [UserPermissionsList] ([Permission],[Description]) SELECT 'MWV.CanExport', 'Allow users to export DICOM files.'WHERE NOT EXISTS (select [Permission] from [UserPermissionsList] where [Permission]='MWV.CanExport' )

ALTER TABLE [AeInfo] ADD CONSTRAINT [PK__AeInfo__0000000000000021] PRIMARY KEY ([AETitle])

ALTER TABLE [AePermissions] ADD CONSTRAINT [PK__AePermissions__00000000000000AB] PRIMARY KEY ([UserName],[Permission])

ALTER TABLE [AePermissionsList] ADD CONSTRAINT [PK_AePermissionsList] PRIMARY KEY ([Permission])

ALTER TABLE [DICOMServerEventLog] ADD CONSTRAINT [PK_EventID] PRIMARY KEY ([EventID])

ALTER TABLE [Forward] ADD CONSTRAINT [PK_Forward] PRIMARY KEY ([SOPInstanceUID])

ALTER TABLE [ImageInstance] ADD CONSTRAINT [PK_SOPInstanceUID1] PRIMARY KEY ([SOPInstanceUID])

ALTER TABLE [Instance] ADD CONSTRAINT [PK_SOPInstanceUID2] PRIMARY KEY ([SOPInstanceUID])

ALTER TABLE [NameOfPhysicianReadingStudy] ADD CONSTRAINT [PK_StudyInstanceUID3] PRIMARY KEY ([StudyInstanceUID])

ALTER TABLE [Options] ADD CONSTRAINT [PK_Options] PRIMARY KEY ([Key])

ALTER TABLE [OtherPatientIDs] ADD CONSTRAINT [PK_PatientID_OtherPatientID] PRIMARY KEY ([PatientID],[OtherPatientID])

ALTER TABLE [OtherPatientNames] ADD CONSTRAINT [PK_OrderNumber] PRIMARY KEY ([OrderNumber])

ALTER TABLE [OtherStudyNumbers] ADD CONSTRAINT [PK_OtherStudyNumber_StudyInstanceUID] PRIMARY KEY ([OtherStudyNumber],[StudyInstanceUID])

ALTER TABLE [PasswordHistory] ADD CONSTRAINT [PK__PasswordHistory__000000000000006E] PRIMARY KEY ([User],[Password],[Id])

ALTER TABLE [Patient] ADD CONSTRAINT [PK_PatientID4] PRIMARY KEY ([PatientID])

ALTER TABLE [ProcedureCodeSequence] ADD CONSTRAINT [PK_StudyInstanceUID_CodeValue_CodingSchemaDesignator] PRIMARY KEY ([StudyInstanceUID],[CodeValue],[CodingSchemaDesignator])

ALTER TABLE [ReferencedImages] ADD CONSTRAINT [PK_SOPInstanceUID_AutoNumber] PRIMARY KEY ([SOPInstanceUID],[AutoNumber])

ALTER TABLE [ReferencedPatientSequence] ADD CONSTRAINT [PK_PatientID5] PRIMARY KEY ([PatientID])

ALTER TABLE [ReferencedPerformedProcedureStepSequence] ADD CONSTRAINT [PK_SeriesInstanceUID6] PRIMARY KEY ([SeriesInstanceUID])

ALTER TABLE [ReferencedStudySequence] ADD CONSTRAINT [PK_StudyInstanceUID_ReferencedSOPInstanceUID] PRIMARY KEY ([StudyInstanceUID],[ReferencedSOPInstanceUID])

ALTER TABLE [RequestAttributeSequence] ADD CONSTRAINT [PK_SeriesInstanceUID_RequestedProcedureID_ScheduledProcedureStepID] PRIMARY KEY ([SeriesInstanceUID],[RequestedProcedureID],[ScheduledProcedureStepID])

ALTER TABLE [Series] ADD CONSTRAINT [PK_SeriesInstanceUID7] PRIMARY KEY ([SeriesInstanceUID])

ALTER TABLE [Study] ADD CONSTRAINT [PK_StudyInstanceUID8] PRIMARY KEY ([StudyInstanceUID])

ALTER TABLE [UserPermissions] ADD CONSTRAINT [PK_UserPermissions] PRIMARY KEY ([UserName],[Permission])

ALTER TABLE [Users] ADD CONSTRAINT [PK_UserName] PRIMARY KEY ([UserName])

ALTER TABLE [dbo].[UserPermissions]  WITH CHECK ADD  CONSTRAINT [FK_UserPermissions_Users] FOREIGN KEY([UserName])
   REFERENCES [dbo].[Users] ([UserName])
   ON UPDATE CASCADE
   ON DELETE CASCADE

ALTER TABLE [dbo].[UserPermissions] CHECK CONSTRAINT [FK_UserPermissions_Users]

ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Users] FOREIGN KEY([UserName])
   REFERENCES [dbo].[Users] ([UserName])
   ON UPDATE CASCADE
   ON DELETE CASCADE

ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Users]

ALTER TABLE [UserPermissionsList] ADD CONSTRAINT [PK_UserPermissionsList] PRIMARY KEY ([Permission])

CREATE UNIQUE INDEX [UQ__Forward__000000000000B72B] ON [Forward] ([SOPInstanceUID] ASC)

CREATE UNIQUE INDEX [UQ__Options__0000000000000019] ON [Options] ([Key] ASC)

ALTER TABLE [Forward] ADD CONSTRAINT [FK_Forward_Instance] FOREIGN KEY ([SOPInstanceUID]) REFERENCES [Instance]([SOPInstanceUID]) ON DELETE CASCADE ON UPDATE CASCADE

ALTER TABLE [ImageInstance] ADD CONSTRAINT [FK_ImageInstance_Instance] FOREIGN KEY ([SOPInstanceUID]) REFERENCES [Instance]([SOPInstanceUID]) ON DELETE CASCADE ON UPDATE CASCADE

ALTER TABLE [Instance] ADD CONSTRAINT [FK_Images_Series] FOREIGN KEY ([SeriesInstanceUID]) REFERENCES [Series]([SeriesInstanceUID]) ON DELETE CASCADE ON UPDATE CASCADE

ALTER TABLE [NameOfPhysicianReadingStudy] ADD CONSTRAINT [FK_NameOfPhysicianReadingStudy_Studies] FOREIGN KEY ([StudyInstanceUID]) REFERENCES [Study]([StudyInstanceUID]) ON DELETE CASCADE ON UPDATE CASCADE

ALTER TABLE [OtherPatientIDs] ADD CONSTRAINT [FK_OtherPatientIDs_Patient] FOREIGN KEY ([PatientID]) REFERENCES [Patient]([PatientID]) ON DELETE CASCADE ON UPDATE CASCADE

ALTER TABLE [OtherPatientNames] ADD CONSTRAINT [FK_OtherPatientNames_Patient] FOREIGN KEY ([PatientID]) REFERENCES [Patient]([PatientID]) ON DELETE CASCADE ON UPDATE CASCADE

ALTER TABLE [OtherStudyNumbers] ADD CONSTRAINT [FK_OtherStudyNumbers_Studies] FOREIGN KEY ([StudyInstanceUID]) REFERENCES [Study]([StudyInstanceUID]) ON DELETE CASCADE ON UPDATE CASCADE

ALTER TABLE [ProcedureCodeSequence] ADD CONSTRAINT [FK_ProcedureCodeSequence_Studies] FOREIGN KEY ([StudyInstanceUID]) REFERENCES [Study]([StudyInstanceUID]) ON DELETE CASCADE ON UPDATE CASCADE

ALTER TABLE [ReferencedImages] ADD CONSTRAINT [FK_ReferencedImages_Instance] FOREIGN KEY ([SOPInstanceUID]) REFERENCES [Instance]([SOPInstanceUID]) ON DELETE CASCADE ON UPDATE CASCADE

ALTER TABLE [ReferencedPatientSequence] ADD CONSTRAINT [FK_ReferencedPatientSequence_Patient] FOREIGN KEY ([PatientID]) REFERENCES [Patient]([PatientID]) ON DELETE CASCADE ON UPDATE CASCADE

ALTER TABLE [ReferencedPerformedProcedureStepSequence] ADD CONSTRAINT [FK_ReferencedPerformedProcedure_Series] FOREIGN KEY ([SeriesInstanceUID]) REFERENCES [Series]([SeriesInstanceUID]) ON DELETE CASCADE ON UPDATE CASCADE

ALTER TABLE [ReferencedStudySequence] ADD CONSTRAINT [FK_ReferencedStudySequence_Studies] FOREIGN KEY ([StudyInstanceUID]) REFERENCES [Study]([StudyInstanceUID]) ON DELETE CASCADE ON UPDATE CASCADE

ALTER TABLE [RequestAttributeSequence] ADD CONSTRAINT [FK_RequestAttributeSequence_Series] FOREIGN KEY ([SeriesInstanceUID]) REFERENCES [Series]([SeriesInstanceUID]) ON DELETE CASCADE ON UPDATE CASCADE

ALTER TABLE [Series] ADD CONSTRAINT [FK_Series_Studies] FOREIGN KEY ([StudyInstanceUID]) REFERENCES [Study]([StudyInstanceUID]) ON DELETE CASCADE ON UPDATE CASCADE

ALTER TABLE [Study] ADD CONSTRAINT [FK_Study_Patient] FOREIGN KEY ([PatientID]) REFERENCES [Patient]([PatientID]) ON DELETE CASCADE ON UPDATE CASCADE

             
ALTER TABLE [PresentationInstance] ADD CONSTRAINT [PK_PresentationState] PRIMARY KEY ( [SOPInstanceUID] )

ALTER TABLE [PresentationInstance] ADD CONSTRAINT [FK_PresentationState_Instance] FOREIGN KEY ( [SOPInstanceUID] ) REFERENCES [Instance] ( [SOPInstanceUID] ) ON DELETE CASCADE  ON UPDATE CASCADE 

ALTER TABLE [ReferencedSeriesSequence] ADD CONSTRAINT [PK_ReferencedSeriesSequence_1] PRIMARY KEY  ( [SeriesInstanceUID], [SOPInstanceUID] )

ALTER TABLE [ReferencedSeriesSequence] ADD CONSTRAINT [FK_ReferencedSeriesSequence_PresentationInstance] FOREIGN KEY ([SOPInstanceUID]) REFERENCES [PresentationInstance] ([SOPInstanceUID]) ON DELETE CASCADE  ON UPDATE CASCADE 

ALTER TABLE [ReferencedImageSequence] ADD CONSTRAINT [PK_ReferencedImageSequence] PRIMARY KEY  CLUSTERED ([SeriesInstanceUID],[SOPInstanceUID],[ReferencedSOPInstanceUID])  

ALTER TABLE [ReferencedImageSequence] ADD CONSTRAINT [FK_ReferencedImageSequence_ReferencedSeriesSequence] FOREIGN KEY ([SeriesInstanceUID],[SOPInstanceUID]) REFERENCES [ReferencedSeriesSequence] ([SeriesInstanceUID],[SOPInstanceUID]) ON DELETE CASCADE  ON UPDATE CASCADE 

ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [UseCardReader]
" + 
#if (LEADTOOLS_V19_OR_LATER_MEDICAL_EXTERNAL_STORE) || (LEADTOOLS_V19_OR_LATER)
@"
ALTER TABLE [ExternalStore]  WITH CHECK ADD  CONSTRAINT [FK_ExternalStore_Instance] FOREIGN KEY([SOPInstanceUID])
REFERENCES [Instance] ([SOPInstanceUID])
ON UPDATE CASCADE
ON DELETE CASCADE

ALTER TABLE [ExternalStore] CHECK CONSTRAINT [FK_ExternalStore_Instance]
" +

#if (LEADTOOLS_V20_OR_LATER)
@"
ALTER TABLE [dbo].[ExportLayout]  WITH CHECK ADD  CONSTRAINT [FK_ExportLayout_Instance] FOREIGN KEY([SOPInstanceUID])
   REFERENCES [dbo].[Instance] ([SOPInstanceUID])
   ON UPDATE CASCADE
   ON DELETE CASCADE

ALTER TABLE [dbo].[ExportLayout] CHECK CONSTRAINT [FK_ExportLayout_Instance]
" +
#endif // #if (LEADTOOLS_V20_OR_LATER)


#endif // #if (LEADTOOLS_V19_OR_LATER_MEDICAL_EXTERNAL_STORE) || (LEADTOOLS_V19_OR_LATER)

@"
CREATE STATISTICS [_dta_stat_Instance1] ON [dbo].[Instance]([InstanceNumber], [SeriesInstanceUID])

CREATE STATISTICS [_dta_stat_Instance_2] ON [dbo].[Instance]([SeriesInstanceUID], [SOPInstanceUID])

CREATE STATISTICS [_dta_stat_Series] ON [dbo].[Series]([SeriesInstanceUID], [StudyInstanceUID])

CREATE STATISTICS [_dta_stat_Study] ON [dbo].[Study]([StudyInstanceUID], [PatientID])

CREATE NONCLUSTERED INDEX [IX_Instance] ON [dbo].[Instance] 
(
	[SeriesInstanceUID] ASC
)WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]

CREATE NONCLUSTERED INDEX [IX_Series] ON [dbo].[Series] 
(
	[StudyInstanceUID] ASC
)WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]

CREATE NONCLUSTERED INDEX [IX_Study] ON [dbo].[Study] 
(
	[PatientID] ASC,
	[StudyInstanceUID] ASC
)WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]

" + 

#if (LEADTOOLS_V19_OR_LATER)

 @"
               IF NOT EXISTS (   SELECT name FROM sys.stats WHERE object_id = OBJECT_ID( 'dbo.ReasonForRequestedProcedureCodeSequence'))
               BEGIN
                 CREATE TABLE [ReasonForRequestedProcedureCodeSequence] (
                    [SeriesInstanceUID] nvarchar(64) NOT NULL
                  , [CodeValue] nvarchar(16) NOT NULL
                  , [CodingSchemeDesignator] nvarchar(16) NOT NULL
                  , [CodeMeaning] nvarchar(64) NULL
                  , [CodingSchemeVersion] nvarchar(16) NULL)

                 ALTER TABLE [ReasonForRequestedProcedureCodeSequence] ADD CONSTRAINT [PK_SeriesInstanceUID_CodeValue_CodingSchemaDesignator] PRIMARY KEY ([SeriesInstanceUID],[CodeValue],[CodingSchemeDesignator])
                 ALTER TABLE [ReasonForRequestedProcedureCodeSequence] ADD CONSTRAINT [FK_ReasonForRequestedProcedureCodeSequence_Series] FOREIGN KEY ([SeriesInstanceUID]) REFERENCES [Series]([SeriesInstanceUID]) ON DELETE CASCADE ON UPDATE CASCADE
               END


               IF NOT EXISTS (   SELECT name FROM sys.stats WHERE object_id = OBJECT_ID( 'dbo.AnatomicRegionSequence'))
               BEGIN
                  CREATE TABLE [AnatomicRegionSequence] (
                     [SOPInstanceUID] nvarchar(64) NOT NULL
                   , [CodeValue] nvarchar(16) NOT NULL
                   , [CodingSchemeDesignator] nvarchar(16) NOT NULL
                   , [CodeMeaning] nvarchar(64) NULL
                   , [CodingSchemeVersion] nvarchar(16) NULL)

                  ALTER TABLE [AnatomicRegionSequence] ADD CONSTRAINT [PK_SOPInstanceUID_CodeValue_CodingSchemaDesignator] PRIMARY KEY ([SOPInstanceUID],[CodeValue],[CodingSchemeDesignator])
                  ALTER TABLE [AnatomicRegionSequence] ADD CONSTRAINT [FK_AnatomicRegionSequence_Instance] FOREIGN KEY ([SOPInstanceUID]) REFERENCES [Instance]([SOPInstanceUID]) ON DELETE CASCADE ON UPDATE CASCADE
               END
" +
#endif // #if (LEADTOOLS_V19_OR_LATER)

#if (LEADTOOLS_V20_OR_LATER)
       @"
      IF NOT EXISTS (   SELECT name FROM sys.stats WHERE object_id = OBJECT_ID( 'dbo.[MetadataJson]'))
                CREATE TABLE [dbo].[MetadataJson](
	                  [SOPInstanceUID] [nvarchar](64) NOT NULL,
	                  [Data] [nvarchar](max) NULL,
                   CONSTRAINT [PK_MetadataJson] PRIMARY KEY CLUSTERED 
                   (
	                  [SOPInstanceUID] ASC
                     )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

               ALTER TABLE [dbo].[MetadataJson]  WITH CHECK ADD FOREIGN KEY([SOPInstanceUID])
               REFERENCES [dbo].[Instance] ([SOPInstanceUID])
               ON UPDATE CASCADE
               ON DELETE CASCADE

      IF NOT EXISTS (   SELECT name FROM sys.stats WHERE object_id = OBJECT_ID( 'dbo.[MetadataXml]'))
                CREATE TABLE [dbo].[MetadataXml](
	                [SOPInstanceUID] [nvarchar](64) NOT NULL,
	                [Data] [nvarchar](max) NULL,
                   CONSTRAINT [PK_MetadataXml] PRIMARY KEY CLUSTERED 
                   (
	                  [SOPInstanceUID] ASC
                     )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

               ALTER TABLE [dbo].[MetadataXml]  WITH CHECK ADD FOREIGN KEY([SOPInstanceUID])
               REFERENCES [dbo].[Instance] ([SOPInstanceUID])
               ON UPDATE CASCADE
               ON DELETE CASCADE" +
         
 #endif
         
 "";
      }
   }
}
