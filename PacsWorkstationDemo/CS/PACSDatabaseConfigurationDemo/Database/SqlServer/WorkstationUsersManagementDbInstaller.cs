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
   class WorkstationUsersManagementSqlInstaller
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
               SqlCommand command = new SqlCommand(DatabaseScripts.WorkstationUsersDatabase, connection);
               command.Transaction = transaction;
               command.ExecuteNonQuery();

               VersionSqlInstaller.CreateVersionTable(connection, transaction);
#if (LEADTOOLS_V19_OR_LATER)
               VersionSqlInstaller.InsertVersionTable(connection, transaction, ConfigurationData.DataAccessLayerVersionNames.UserManagement, 1, 4);
#else
               VersionSqlInstaller.InsertVersionTable(connection, transaction, ConfigurationData.DataAccessLayerVersionNames.UserManagement, 1, 1);
#endif

               command.Dispose();

               transaction.Commit();
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
         public const string WorkstationUsersDatabase = @"
CREATE TABLE [dbo].[Users](
   [UserName] [nvarchar](16) NOT NULL,
   [Password] [nvarchar](64) NOT NULL,
   [IsAdmin] [bit] NOT NULL CONSTRAINT [DF_Users_IsAdmin]  DEFAULT ((0)),
   [UseCardReader] [bit] NOT NULL,
   [FriendlyName] [nvarchar](256) NULL, " + 
#if (LEADTOOLS_V20_OR_LATER)
   @"[UserType] [nvarchar](50) NULL, "+
   @"[ExtendedName] [nvarchar](360) NULL, " +
#endif
 @"CONSTRAINT [PK_Users_1] PRIMARY KEY CLUSTERED 
(
   [UserName] ASC
) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [UseCardReader]
";
                                          
         
      }
   }
}
