// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace MedicalWorkstationConfigurationDemo
{
   public static class VersionSqlInstaller
   {
      private const string InsertVersionTableString = @"
      INSERT INTO [DbVersion]([Name],[Major],[Minor]) VALUES ('{0}','{1}','{2}')
      ";

      private const string VersionDatabaseString = @"
CREATE TABLE [DbVersion] (
  [Major] int NULL
, [Minor] int NULL
, [Name] nvarchar(100) NOT NULL
);
";

      public static void CreateVersionTable(SqlConnection connection, SqlTransaction transaction)
      {
         using (SqlCommand command = new SqlCommand(VersionSqlInstaller.VersionDatabaseString, connection))
         {
            command.Transaction = transaction;
            command.ExecuteNonQuery();
         }
      }

      public static void InsertVersionTable(SqlConnection connection, SqlTransaction transaction, string name, int major, int minor)
      {
         string insertString = string.Format(InsertVersionTableString, name, major, minor);

         using (SqlCommand command = new SqlCommand(insertString, connection))
         {
            command.Transaction = transaction;
            command.ExecuteNonQuery();
         }
      }
   }
}
