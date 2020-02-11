// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using Leadtools.Demos;
using System.IO;
using System.Data.Sql;
using Leadtools.Dicom;
using System.Reflection;
using System.Configuration;

namespace Leadtools.Demos.Sql
{
   public enum DatabaseState
   {
      Starting,
      Working,
      Finished
   }

   public class DatabaseArgs
   {
      public DatabaseState state;
      public int elapsedMilliseconds;

      public DatabaseArgs()
      {
         state = DatabaseState.Starting;
         elapsedMilliseconds = 0;
      }
   }

   public delegate bool DatabaseCallback(DatabaseArgs args);

   internal class SqlUtilities
    {

        #region Public

        #region Methods

        private SqlUtilities() { }

        public static StringCollection GetServerList()
        {
            StringCollection sqlServers = new StringCollection();

            try
            {
                SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
                System.Data.DataTable servers = instance.GetDataSources();

                foreach (DataRow row in servers.Rows)
                {
                    string server = row[0].ToString();

                    if (String.IsNullOrEmpty(row[1].ToString()))
                    {
                        sqlServers.Add(server);
                    }
                    else
                    {
                        sqlServers.Add(string.Format(@"{0}\{1}", server, row[1].ToString()));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return sqlServers;
        }

      public static StringCollection GetDatabaseList
      (
         SqlConnectionStringBuilder connectionBuilder
      )
      {
         try
         {
            StringCollection databaseNames = new StringCollection();
            using (SqlConnection connection = new SqlConnection(connectionBuilder.ConnectionString))
            {
               SqlCommand command = new SqlCommand(Constants.SqlScript.SelectDatabasesName, connection);

               connection.Open();

               SqlDataReader reader = command.ExecuteReader();

               using (reader)
               {
                  while (reader.Read())
                  {
                     databaseNames.Add(reader.GetString(0));
                  }
               }

               return databaseNames;
            }
         }
            catch (Exception exception)
            {
                throw exception;
            }
        }

      public static void CreateDatabase(SqlConnectionStringBuilder connectionBuilder)
      {
         CreateDatabase(connectionBuilder, 0, null);
      }


      private static void UpdateCreateDatabaseCallback(DatabaseCallback cb, DatabaseArgs args, DatabaseState state)
      {
         args.state = state;
         if (args.state == DatabaseState.Starting)
         {
            args.elapsedMilliseconds = 0;
         }

         if (cb != null)
         {
            cb(args);
         }
      }
      

      public static void CreateDatabase(SqlConnectionStringBuilder connectionBuilder, int callbackFrequencyInMilliseconds, DatabaseCallback cb)
      {
         try
         {
            string databaseName;
            string fop;
            bool pooling;
            //  SqlConnection connection;
            string createDatabaseString;
            SqlCommand command;


            databaseName = connectionBuilder.InitialCatalog;
            pooling = connectionBuilder.Pooling;
            fop = connectionBuilder.FailoverPartner;

            connectionBuilder.InitialCatalog = string.Empty;
            connectionBuilder.Pooling = false;
            connectionBuilder.FailoverPartner = string.Empty;

            using (SqlConnection connection = new SqlConnection(connectionBuilder.ConnectionString))
            {
               // connection.ConnectionTimeout = 120;
               createDatabaseString = string.Format(Constants.SqlScript.CreateDatabase, databaseName);
               command = new SqlCommand(createDatabaseString, connection);

               // For Cloud based connections (i.e. Microsoft Azure), it takes longer to execute the command.
               if (connectionBuilder.ConnectionString.Contains("tcp:"))
               {
                  command.CommandTimeout = 120;
               }

               connectionBuilder.InitialCatalog = databaseName;
               connectionBuilder.Pooling = pooling;
               connectionBuilder.FailoverPartner = fop;

               connection.Open();

               DatabaseArgs args = new DatabaseArgs();
               UpdateCreateDatabaseCallback(cb, args, DatabaseState.Starting);

               IAsyncResult result = command.BeginExecuteNonQuery();
               while (!result.IsCompleted)
               {
                  System.Threading.Thread.Sleep(callbackFrequencyInMilliseconds);
                  args.elapsedMilliseconds = args.elapsedMilliseconds + callbackFrequencyInMilliseconds;
                  UpdateCreateDatabaseCallback(cb, args, DatabaseState.Working);
               }

               command.EndExecuteNonQuery(result);
               UpdateCreateDatabaseCallback(cb, args, DatabaseState.Finished);
            }
         }

         catch (Exception exception)
         {
            System.Diagnostics.Debug.Assert(false);

            throw exception;
         }
      }

      public static void DropDatabase(SqlConnectionStringBuilder connectionBuilder)
      {
         DropDatabase(connectionBuilder, 0, null);
      }

      public static void DropDatabase(SqlConnectionStringBuilder connectionBuilder, int callbackFrequencyInMilliseconds, DatabaseCallback cb)
      {
         try
         {
            string databaseName = connectionBuilder.InitialCatalog;
            bool pooling = connectionBuilder.Pooling;
            string fop = connectionBuilder.FailoverPartner;

            connectionBuilder.InitialCatalog = string.Empty;
            connectionBuilder.Pooling = false;
            connectionBuilder.FailoverPartner = string.Empty;

            using (SqlConnection connection = new SqlConnection(connectionBuilder.ConnectionString))
            {
               string createDatabaseString = string.Format(Constants.SqlScript.DropDatabase, databaseName);
               SqlCommand command = new SqlCommand(createDatabaseString, connection);

               connectionBuilder.InitialCatalog = databaseName;
               connectionBuilder.Pooling = pooling;
               connectionBuilder.FailoverPartner = fop;

               connection.Open();

               DatabaseArgs args = new DatabaseArgs();
               UpdateCreateDatabaseCallback(cb, args, DatabaseState.Starting);

               IAsyncResult result = command.BeginExecuteNonQuery();
               while (!result.IsCompleted)
               {
                  System.Threading.Thread.Sleep(callbackFrequencyInMilliseconds);
                  args.elapsedMilliseconds = args.elapsedMilliseconds + callbackFrequencyInMilliseconds;
                  UpdateCreateDatabaseCallback(cb, args, DatabaseState.Working);
               }

               command.EndExecuteNonQuery(result);
               UpdateCreateDatabaseCallback(cb, args, DatabaseState.Finished);
            }
         }
         catch (Exception exception)
         {
            System.Diagnostics.Debug.Assert(false);

            throw exception;
         }
      }

      public static bool DatabaseExist(SqlConnectionStringBuilder connectionBuilder)
        {
            bool exist = false;

            try
            {
                string databaseName;
                string fop;
                bool pooling;
                SqlConnection connection;
                string createDatabaseString;
                SqlCommand command;


                databaseName = connectionBuilder.InitialCatalog;
                pooling = connectionBuilder.Pooling;
                fop = connectionBuilder.FailoverPartner;

                connectionBuilder.InitialCatalog = string.Empty;
                connectionBuilder.Pooling = false;
                connectionBuilder.FailoverPartner = string.Empty;

                connection = new SqlConnection(connectionBuilder.ConnectionString);
                createDatabaseString = string.Format(Constants.SqlScript.SelectDatabase, databaseName);
                command = new SqlCommand(createDatabaseString, connection);

                connectionBuilder.InitialCatalog = databaseName;
                connectionBuilder.Pooling = pooling;
                connectionBuilder.FailoverPartner = fop;

                connection.Open();

                using (connection)
                {
                    object o = command.ExecuteScalar();

                    exist = o != null;
                }

            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.Assert(false);

                throw exception;
            }
            return exist;
        }

      private static void InternalCloseDatabaseConnections(
         string connectionString,
         string strDatabaseName
      )
      {
         string connectionErrorMessage;
         string version;
         TestSQLConnectionString(connectionString, out connectionErrorMessage, out version);

         string adoCommandText;
         if (IsSqlServer2012OrLater(version))
         {
            adoCommandText = Constants.ADOCommandText.KILL_ALL_CONNECTIONS_2012_OR_LATER;
         }
         else
         {
            adoCommandText = Constants.ADOCommandText.KILL_ALL_CONNECTIONS_2000_2005_2008;
         }

         try
         {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            using (sqlConnection)
            {
               SqlCommand ProcessCommand = new SqlCommand(String.Format(adoCommandText, strDatabaseName), sqlConnection);
               ProcessCommand.ExecuteNonQuery();
            }
         }
         catch (Exception)
         {
            // throw exception;
         }
      }

      public static void CloseDatabaseConnections(
         string connectionString,
         string strDatabaseName
      )
      {
         if (!SqlUtilities.IsDatabaseConnected(connectionString, strDatabaseName))
         {
            return;
         }

         InternalCloseDatabaseConnections(connectionString, strDatabaseName);
      }

      public static bool IsDatabaseConnected
        (
           string connectionString,
           string strDatabaseName
        )
        {
            try
            {
                SqlConnection sqlConnection = null;
                int processesCount = -1;


                sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                using (sqlConnection)
                {
                    SqlCommand ProcessCommand = null;


                    ProcessCommand = new SqlCommand(String.Format(Constants.ADOCommandText.SELECT_ACTIVE_PROCESSES_EXCEPT_CURRENT_COUNT, strDatabaseName),
                                                      sqlConnection);

                    processesCount = (int)ProcessCommand.ExecuteScalar();
                }

                return (processesCount > 0);
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.Assert(false);

                throw exception;
            }
        }

        public static bool IsSqlServer2012OrLater(string version)
        {
            return version.StartsWith("11.");
        }

        public static bool TestSQLConnectionString(string connectionString, out string connectionErrorMessage)
        {
            string version = string.Empty;
            bool ret = TestSQLConnectionString(connectionString, out connectionErrorMessage, out version);
            return ret;
        }


        public static bool TestSQLConnectionString
        (
           string connectionString,
           out string connectionErrorMessage,
           out string version
        )
        {
            version = string.Empty;
            try
            {
                System.Data.SqlClient.SqlConnection SQLConnectionTest = null;


                SQLConnectionTest = new System.Data.SqlClient.SqlConnection();

                SQLConnectionTest.ConnectionString = connectionString;

                SetConnectionPoolingState(ref connectionString, false);

                try
                {
                    SQLConnectionTest.Open();

                    connectionErrorMessage = string.Empty;
                    version = SQLConnectionTest.ServerVersion;

                    return true;
                }
                catch (Exception exception)
                {
                    connectionErrorMessage = exception.Message;

                    return false;
                }
                finally
                {
                    SQLConnectionTest.Close();
                }
            }
            catch (Exception exp)
            {
                System.Diagnostics.Debug.Assert(false);

                throw exp;
            }
        }



        public static bool IsLocalSQLServerInstance
        (
           string serverName
        )
        {
            try
            {
                RegistryKey installedSqlKey;
                string[] sqlServers;
                string hostName;
                object sqlServer = null;

                hostName = System.Net.Dns.GetHostName();

                if (serverName.ToLower() == hostName.ToLower())
                {
                    return true;
                }

                installedSqlKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Microsoft SQL Server\\");

                if (installedSqlKey != null)
                {
                    sqlServer = installedSqlKey.GetValue("InstalledInstances");
                }

                if (sqlServer == null)
                {
                    installedSqlKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Microsoft SQL Server\\");

                    if (installedSqlKey != null)
                    {
                        sqlServer = installedSqlKey.GetValue("InstalledInstances");
                    }
                }

                if (sqlServer == null)
                {
                    return false;
                }

                if (sqlServer is string[])
                {
                    sqlServers = (string[])sqlServer;
                }
                else
                {
                    sqlServers = sqlServer.ToString().Split(' ');
                }

                foreach (string localServer in sqlServers)
                {
                    if (string.Compare(hostName + "\\" + localServer, serverName, true) == 0)
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.Assert(false);

                throw exception;
            }
        }

        public static string[] GetLocalSQLServerInstances()
        {
            try
            {
                RegistryKey installedSqlKey;
                string[] sqlServers;
                string[] localSqlServers;
                string hostName;
                object sqlServer = null;

                hostName = System.Net.Dns.GetHostName();
#if FOR_DOTNET4
                // Try 64-bit registry first
                installedSqlKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                if (installedSqlKey != null)
                {
                    installedSqlKey = installedSqlKey.OpenSubKey("SOFTWARE\\Microsoft\\Microsoft SQL Server\\");
                }

                // Try 32-bit registry first
                if (installedSqlKey == null)
                {
                    installedSqlKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
                    if (installedSqlKey != null)
                    {
                        installedSqlKey = installedSqlKey.OpenSubKey("SOFTWARE\\Microsoft\\Microsoft SQL Server\\");
                    }
                }
#else
                  installedSqlKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Microsoft SQL Server\\");
#endif // #if FOR_DOTNET4

                if (installedSqlKey != null)
                {
                    sqlServer = installedSqlKey.GetValue("InstalledInstances");
                }

                if (sqlServer == null)
                {
                    installedSqlKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Microsoft SQL Server\\");

                    if (installedSqlKey != null)
                    {
                        sqlServer = installedSqlKey.GetValue("InstalledInstances");
                    }
                }

                if (sqlServer == null)
                {
                    return null;
                }

                if (null == sqlServer)
                {
                    return null;
                }

                if (sqlServer is string[])
                {
                    sqlServers = (string[])sqlServer;
                }
                else
                {
                    sqlServers = sqlServer.ToString().Split(' ');
                }

                localSqlServers = new string[sqlServers.Length];
                for (int serverIndex = 0; serverIndex < sqlServers.Length; serverIndex++)
                {
                    if (string.Compare("MSSQLSERVER", sqlServers[serverIndex]) == 0)
                    {
                        // This is default instance, which is case (2) below
                        // 
                        // MY-MACHINE-NAME\SQLEXPRESS  /* named version - correct */
                        // MY-MACHINE-NAME             /* unnamed version (default instance) - correct */
                        // MY-MACHINE-NAME\MSSQLSERVER /* unnamed version (default instance) - Wrong */
                        localSqlServers[serverIndex] = hostName;
                    }
                    else
                    {
                        localSqlServers[serverIndex] = Path.Combine(hostName, sqlServers[serverIndex]);
                    }
                }

                return localSqlServers;
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.Assert(false);

                throw exception;
            }
        }

        public static bool IsWindowsAuthentication
        (
           string connectionString
        )
        {
            try
            {
                SqlConnection Connection;


                Connection = new SqlConnection(connectionString);

                if ((-1 == Connection.ConnectionString.ToLower().IndexOf(Constants.ConnectionString.Integrated_Security.ToLower(), 0)) &&
                     (-1 == Connection.ConnectionString.ToLower().IndexOf(Constants.ConnectionString.Trusted_Connection.ToLower(), 0)))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.Assert(false);

                throw exception;
            }
        }

      public static void DeleteDbIfExists(SqlConnectionStringBuilder connectionBuilder)
      {
         DeleteDbIfExists(connectionBuilder, 0, null);
      }

        public static void DeleteDbIfExists(SqlConnectionStringBuilder csB, int callbackFrequencyInMilliseconds, DatabaseCallback cb)
      {
         if (SqlUtilities.DatabaseExist(csB))
         {
            bool databaseDropped = false;
            string dbName;
            string fop;

            dbName = csB.InitialCatalog;
            fop = csB.FailoverPartner;

            try
            {
               csB.InitialCatalog = string.Empty;
               csB.FailoverPartner = string.Empty;

               SqlUtilities.CloseDatabaseConnections(csB.ConnectionString, dbName);

               if (!SqlUtilities.IsDatabaseConnected(csB.ConnectionString, dbName))
               {
                  csB.InitialCatalog = dbName;
                  csB.FailoverPartner = fop;

                  SqlUtilities.DropDatabase(csB, callbackFrequencyInMilliseconds, cb);
                  databaseDropped = true;
               }
            }
            finally
            {
               csB.InitialCatalog = dbName;
               csB.FailoverPartner = fop;
            }

            if (databaseDropped == false)
            {
               try
               {
                  csB.InitialCatalog = dbName;
                  csB.FailoverPartner = fop;

                  SqlUtilities.DropDatabase(csB, callbackFrequencyInMilliseconds, cb);
                  databaseDropped = true;

               }
               catch (Exception)
               {
                   throw new InvalidOperationException(string.Format("Drop database failed. Users are connected to database {0}", dbName));
               }
            }
         }
      }

      #endregion

      #endregion

      #region Private

      #region Methods

      private static void SetConnectionPoolingState
        (
           ref string strConnectionString,
           bool bEnabled
        )
        {
            try
            {
                StringBuilder ConnectionStringBuilder;


                RemovePoolingKeyValue(ref strConnectionString);

                if (!strConnectionString.Trim().EndsWith(";"))
                {
                    strConnectionString += ";";
                }

                ConnectionStringBuilder = new StringBuilder(strConnectionString);

                if (bEnabled)
                {
                    ConnectionStringBuilder.Append(Constants.ConnectionString.POOLING_ENABLE);

                    strConnectionString = ConnectionStringBuilder.ToString();
                }
                else
                {
                    ConnectionStringBuilder.Append(Constants.ConnectionString.POOLING_DISABLE);

                    strConnectionString = ConnectionStringBuilder.ToString();
                }
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.Assert(false);

                throw exception;
            }
        }


        private static void RemovePoolingKeyValue
        (
           ref string strConnectionString
        )
        {
            try
            {
                int nStartIndexOfPool = -1;
                int nEndIndexOfPool = -1;


                strConnectionString = strConnectionString.ToLower();

                nStartIndexOfPool = strConnectionString.IndexOf(Constants.ConnectionString.POOLING_KEYWORD);

                if (-1 != nStartIndexOfPool)
                {
                    int nPoolingKeyValueLength = -1;


                    nEndIndexOfPool = strConnectionString.IndexOf(Constants.SpecialCharacters.SEMICOLON,
                                                                    nStartIndexOfPool);

                    if (-1 == nEndIndexOfPool)
                    {
                        nEndIndexOfPool = strConnectionString.Length - 1;
                    }

                    nPoolingKeyValueLength = (nEndIndexOfPool - nStartIndexOfPool) + 1;

                    strConnectionString = strConnectionString.Remove(nStartIndexOfPool,
                                                                       nPoolingKeyValueLength);
                }
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.Assert(false);

                throw exception;
            }
        }


        #endregion


        #region Data Types Definition

        private class Constants
        {
            public class ConnectionString
            {
                public const string POOLING_KEYWORD = "pooling";
                public const string POOLING_DISABLE = "pooling=false;";
                public const string POOLING_ENABLE = "pooling=true;";
                public const string Integrated_Security = "Integrated Security";
                public const string Trusted_Connection = "Trusted_Connection";
            }

            public class SpecialCharacters
            {
                public const string SEMICOLON = ";";
            }

            public class ADOCommandText
            {
                public const string SELECT_RECORDS_ALL = "SELECT * FROM {0}";
                public const string SELECT_RECORDS_TOP = "SELECT TOP {0} * FROM {1}";
                public const string SELECT_RECORDS_COUNT = "SELECT COUNT ( * ) FROM {0}";
                public const string SELECT_ACTIVE_PROCESSES_EXCEPT_CURRENT_COUNT = "SELECT Count ( Process.spid ) FROM MASTER.dbo.sysdatabases Db, Master.dbo.sysprocesses Process WHERE Db.dbid = Process.dbid AND Db.name = '{0}' AND Process.dbid != @@SPID";
                public const string SELECT_ACTIVE_PROCESSES_EXCEPT_CURRENT = "SELECT Process.spid FROM MASTER.dbo.sysdatabases Db, Master.dbo.sysprocesses Process WHERE Db.dbid = Process.dbid AND Db.name = '{0}' AND Process.dbid != @@SPID";
                public const string KILL_PROCESS = "EXEC KILL {0}";
                public const string KILL_ALL_CONNECTIONS_2012_OR_LATER = @"" +
                  @"    USE [master];                                                           " +
                  @"    DECLARE @kill varchar(8000) = '';                                       " +
                  @"    SELECT @kill = @kill + 'kill ' + CONVERT(varchar(5), session_id) + ';'  " +
                  @"    FROM sys.dm_exec_sessions                                               " +
                  @"    WHERE database_id = db_id('{0}')                                        " +
                  @"    EXEC(@kill);                                                            ";

                 public const string KILL_ALL_CONNECTIONS_2000_2005_2008 = @"" +
                 @"    USE [master];                                                           " +
                 @"    DECLARE @kill varchar(8000); SET @kill = '';                            " +
                 @"    SELECT @kill = @kill + 'kill ' + CONVERT(varchar(5), spid) + ';'        " +
                 @"    FROM master..sysprocesses                                               " +
                 @"    WHERE dbid = db_id('{0}')                                               " +
                 @"    EXEC(@kill);                                                            ";
         }

            public class SqlScript
            {
                public const string SelectTable = "SELECT COUNT ( * ) FROM dbo.sysobjects where id = object_id(N'{0}') and OBJECTPROPERTY(id, N'IsUserTable') = 1";
                public const string CreateDatabase = "CREATE DATABASE [{0}]";
                public const string DropDatabase = "DROP DATABASE [{0}]";
                public const string SelectDatabasesName = "SELECT Name from master.dbo.sysdatabases WHERE NAME NOT IN ('master','msdb','model','tempdb','reportserver','reportservertempdb','datacomsqlaudit','pubs','distribution','northwind')";
                public const string SelectDatabase = "SELECT Name from master.dbo.sysdatabases WHERE NAME NOT IN ('master','msdb','model','tempdb','reportserver','reportservertempdb','datacomsqlaudit','pubs','distribution','northwind') AND Name='{0}'";
            }
        }


        #endregion

        #endregion
    }

}
