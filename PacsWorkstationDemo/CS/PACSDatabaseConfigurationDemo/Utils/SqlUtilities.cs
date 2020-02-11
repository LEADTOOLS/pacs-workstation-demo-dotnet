// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System ;
using System.Data ;
using System.Data.SqlClient ;
using System.Text ; 
using System.Collections.Specialized ;
using System.Runtime.InteropServices ;
using Microsoft.Win32 ;
using System.Data.Sql;


namespace Leadtools.Demos.Sql
{
   /// <summary>
   /// Summary description for SqlUtilities.
   /// </summary>
   internal class SqlUtilities
   {
      
      #region Public 
      
         #region Methods
            
            private SqlUtilities ( ) { }
            
            
            public static StringCollection GetServerList ( )
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
               catch (Exception )
               {
                  throw ;
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
                  StringCollection databaseNames ;
                  SqlConnection connection ;
                  SqlCommand    command ;
                  SqlDataReader reader ;
                  
                  
                  databaseNames = new StringCollection ( ) ;
                  connection    = new SqlConnection ( connectionBuilder.ConnectionString ) ;
                  command       = new SqlCommand ( Constants.SqlScript.SelectDatabasesName, connection ) ;
                  
                  connection.Open ( ) ;
                  
                  using ( connection )
                  {
                     reader = command.ExecuteReader ( ) ;
                     
                     using ( reader ) 
                     {
                        while ( reader.Read ( ) )
                        { 
                           databaseNames.Add ( reader.GetString ( 0 ) ) ;
                        }
                     }
                  }
                  
                  return databaseNames ;
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false ) ;
                  
                  throw exception ;
               }
            }
            
            public static void CreateDatabase(SqlConnectionStringBuilder connectionBuilder)
            {
               try
               {
                   string databaseName;
                   bool pooling;
                   SqlConnection connection;
                   string createDatabaseString;
                   SqlCommand command;


                   databaseName = connectionBuilder.InitialCatalog;
                   pooling = connectionBuilder.Pooling;

                   connectionBuilder.InitialCatalog = string.Empty;
                   connectionBuilder.Pooling = false;

                   connection = new SqlConnection(connectionBuilder.ConnectionString);
                   createDatabaseString = string.Format(Constants.SqlScript.CreateDatabase, databaseName);
                   command = new SqlCommand(createDatabaseString, connection);

                   connectionBuilder.InitialCatalog = databaseName;
                   connectionBuilder.Pooling = pooling;

                   connection.Open();
                     
                  using ( connection )
                  {
                     command.ExecuteNonQuery ( ) ;
                  }
                  
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false ) ;
                  
                  throw exception ;
               }
            }
            
            
            public static bool DatabaseExist ( SqlConnectionStringBuilder connectionBuilder )
            {
                bool exist = false;

                try
                {
                    string databaseName ;
                    bool pooling ;
                    SqlConnection connection ;
                    string createDatabaseString ;
                    SqlCommand command ;


                    databaseName = connectionBuilder.InitialCatalog ;
                    pooling = connectionBuilder.Pooling ;

                    connectionBuilder.InitialCatalog = string.Empty ;
                    connectionBuilder.Pooling        = false ;

                    connection           = new SqlConnection ( connectionBuilder.ConnectionString ) ;
                    createDatabaseString = string.Format     ( Constants.SqlScript.SelectDatabase, databaseName ) ;
                    command              = new SqlCommand    ( createDatabaseString, connection ) ;

                    connectionBuilder.InitialCatalog = databaseName ;
                    connectionBuilder.Pooling = pooling ;

                    connection.Open ( ) ;

                    using ( connection )
                    {
                        object o = command.ExecuteScalar ( ) ;

                        exist = o != null ;
                    }

                }
                catch ( Exception exception )
                {
                    System.Diagnostics.Debug.Assert ( false, exception.Message ) ;

                    throw ;
                }
                
                return exist ;
            }
            
            public static void DeleteDbIfExists ( SqlConnectionStringBuilder csB ) 
            {
               if ( SqlUtilities.DatabaseExist ( csB ) )
               {
                  string dbName ;
                  
                  dbName = csB.InitialCatalog ; 
                  
                  
                  try
                  {
                     csB.InitialCatalog = "" ;
                     
                     if(  !SqlUtilities.IsDatabaseConnected ( csB.ConnectionString, dbName ) )
                     {
                        csB.InitialCatalog = dbName ;
                        
                        SqlUtilities.DropDatabase ( csB ) ;
                     }
                     else
                     {
                        throw new InvalidOperationException ( string.Format ( "Drop database failed. Users are connected to database {0}", dbName ) ) ;
                     }
                  }
                  finally
                  {
                     csB.InitialCatalog = dbName ;
                  }
               }
            }
            
            public static void DropDatabase
            (
               SqlConnectionStringBuilder connectionBuilder
            )
            {
               try
               {
                  string        databaseName;
                  bool          pooling ;
                  SqlConnection connection ;
                  string        createDatabaseString ;
                  SqlCommand    command ;
                  
                  
                  databaseName = connectionBuilder.InitialCatalog ;
                  pooling      = connectionBuilder.Pooling ;

                  connectionBuilder.InitialCatalog = string.Empty;
                  connectionBuilder.Pooling        = false ;
                  
                  connection           = new SqlConnection ( connectionBuilder.ConnectionString ) ;
                  createDatabaseString = string.Format ( Constants.SqlScript.DropDatabase, databaseName ) ;
                  command              = new SqlCommand ( createDatabaseString, connection ) ;
                  
                  connectionBuilder.InitialCatalog = databaseName ;
                  connectionBuilder.Pooling         = pooling ;
                  
                  connection.Open ( ) ;
                     
                  using ( connection )
                  {
                     command.ExecuteNonQuery ( ) ;
                  }
                  
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false ) ;
                  
                  throw exception ;
               }
            }
            
            
            //public static void DisconnectCurrentDatabaseUsers
            //(
            //   SqlConnectionStringBuilder connectionBuilder
            //)
            //{
            //   try
            //   {
            //      string        databaseName ;
            //      SqlConnection connection ;
            //      Server sqlServer ;
                  
                  
            //      databaseName = connectionBuilder.InitialCatalog ;
                  
            //      connectionBuilder.InitialCatalog = "" ;
                  
            //      connection = new SqlConnection ( connectionBuilder.ConnectionString ) ;
                  
            //      connectionBuilder.InitialCatalog = databaseName ;
                  
            //      sqlServer = new Server ( new ServerConnection ( connection ) ) ;
                  
            //      sqlServer.KillAllProcesses ( databaseName ) ;
                  
            //      connection.Dispose ( ) ;
            //   }
            //   catch ( Exception exception )
            //   {
            //      System.Diagnostics.Debug.Assert ( false ) ;
                  
            //      throw exception ;
            //   }
            //}
            
            
            public static bool IsDatabaseConnected
            ( 
               string connectionString,
               string strDatabaseName
            )
            {
               try
               {
                  SqlConnection sqlConnection  = null ;
                  int           processesCount = -1 ;
                  
                  
                  sqlConnection = new SqlConnection ( connectionString ) ;
                  
                  sqlConnection.Open ( ) ;
                  
                  using ( sqlConnection )
                  {
                     SqlCommand ProcessCommand = null ;
                     
                     
                     ProcessCommand = new SqlCommand ( String.Format ( Constants.ADOCommandText.SELECT_ACTIVE_PROCESSES_EXCEPT_CURRENT_COUNT, strDatabaseName ),
                                                       sqlConnection ) ;
                     
                     processesCount = ( int ) ProcessCommand.ExecuteScalar ( ) ;
                  }
                  
                  return ( processesCount > 0 ) ;
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false ) ;
                  
                  throw exception ;
               }
            }
            
            public static bool TestSQLConnectionString
            ( 
               string connectionString,
               out string connectionErrorMessage
            ) 
            {
               try
               {
                  System.Data.SqlClient.SqlConnection SQLConnectionTest = null ;
                  
                  
                  SQLConnectionTest = new System.Data.SqlClient.SqlConnection ( ) ;
                  
                  SQLConnectionTest.ConnectionString = connectionString ;
                  
                  SetConnectionPoolingState ( ref connectionString, false ) ;
                  
                  try
                  {
                     SQLConnectionTest.Open ( ) ;
                     
                     connectionErrorMessage = string.Empty ;
                     
                     return true ;
                  }
                  catch ( Exception exception )
                  {
                     connectionErrorMessage = exception.Message ;
                     
                     return false ;
                  }   
                  finally
                  {
                     SQLConnectionTest.Close ( ) ;   
                  }
               }
               catch ( Exception exp )
               {
                   System.Diagnostics.Debug.Assert ( false ) ;
                           
                   throw exp ;
               }
            }
            
            
            
            public static bool IsLocalSQLServerInstance
            ( 
               string serverName
            ) 
            {
               try
               {
                  RegistryKey installedSqlKey ;
                  string [ ] sqlServers ;
                  string hostName ;
                  
                  
                  hostName = System.Net.Dns.GetHostName ( ) ;
                  
                  if ( serverName.ToLower ( ) == hostName.ToLower ( ) )
                  {
                     return true ;
                  }
                  
                  installedSqlKey = Registry.LocalMachine.OpenSubKey ( "SOFTWARE\\Microsoft\\Microsoft SQL Server\\" ) ;
                  
                  if ( null == installedSqlKey ) 
                  {
                     return false ;
                  }
                  
                  object sqlServer = installedSqlKey.GetValue ( "InstalledInstances" ) ;
                  
                  if ( null == sqlServer ) 
                  {
                     return false ;
                  }
                  
                  if ( sqlServer is string [ ] ) 
                  {
                     sqlServers = ( string [ ] ) sqlServer ;
                  }
                  else
                  {
                     sqlServers = sqlServer.ToString ( ).Split ( ' ' ) ;
                  }
                  
                  foreach ( string localServer in sqlServers )
                  {
                     if ( string.Compare ( hostName + "\\" + localServer, serverName, true ) == 0 )
                     {
                        return true ;
                     }
                  }
                  
                  return false ;
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false ) ;
                  
                  throw exception ;
               }
            }
            
            public static string [ ] GetLocalSQLServerInstances ( ) 
            {
               try
               {
                  RegistryKey installedSqlKey ;
                  string [ ] sqlServers ;
                  string [ ] localSqlServers ;
                  string hostName ;
                  
                  
                  hostName = System.Net.Dns.GetHostName ( ) ;
                  
                  installedSqlKey = Registry.LocalMachine.OpenSubKey ( "SOFTWARE\\Microsoft\\Microsoft SQL Server\\" ) ;
                  
                  if ( null == installedSqlKey ) 
                  {
                     return new string[0] ;
                  }
                  
                  object sqlServer = installedSqlKey.GetValue ( "InstalledInstances" ) ;
                  
                  if ( null == sqlServer ) 
                  {
                     return new string[0] ; ;
                  }
                  
                  if ( sqlServer is string [ ] ) 
                  {
                     sqlServers = ( string [ ] ) sqlServer ;
                  }
                  else
                  {
                     sqlServers = sqlServer.ToString ( ).Split ( ' ' ) ;
                  }
                  
                  
                  localSqlServers = new string [ sqlServers.Length ] ;
                  
                  for ( int serverIndex = 0; serverIndex < sqlServers.Length; serverIndex++ ) 
                  {
                     localSqlServers [ serverIndex ] = hostName + "\\" + sqlServers [ serverIndex ] ;
                  }
                  
                  return localSqlServers ;
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false ) ;
                  
                  throw exception ;
               }
            }
            
            public static bool IsWindowsAuthentication 
            ( 
               string connectionString
            )
            {
               try
               {
                  SqlConnection Connection ;
                  
                  
                  Connection = new SqlConnection ( connectionString ) ;
                  
                  if ( ( -1 == Connection.ConnectionString.ToLower ( ).IndexOf ( Constants.ConnectionString.Integrated_Security.ToLower ( ), 0 ) ) &&
                       ( -1 == Connection.ConnectionString.ToLower ( ).IndexOf ( Constants.ConnectionString.Trusted_Connection.ToLower ( ), 0 ) ) )
                  {
                     return false ;
                  }
                  else
                  {
                     return true ;
                  }
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false ) ;
                        
                  throw exception ;
               }
            }
            
            
         #endregion
         
         #region Properties
   
         #endregion
   
         #region Events
   
         #endregion
         
         #region Data Types Definition
   
         #endregion
   
         #region Callbacks
   
         #endregion
         
      #endregion
      
      #region Protected 
      
         #region Methods
   
         #endregion
         
         #region Properties
   
         #endregion
   
         #region Events
   
         #endregion
   
         #region Data Members
   
         #endregion
         
         #region Data Types Definition
   
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
                  StringBuilder ConnectionStringBuilder ;
                  
                  
                  RemovePoolingKeyValue ( ref strConnectionString ) ;
                                    
                  ConnectionStringBuilder = new StringBuilder ( strConnectionString ) ;
                  
                  if ( bEnabled ) 
                  {
                     ConnectionStringBuilder.Append ( Constants.ConnectionString.POOLING_ENABLE ) ;
                     
                     strConnectionString = ConnectionStringBuilder.ToString ( ) ;
                  }
                  else
                  {
                     ConnectionStringBuilder.Append ( Constants.ConnectionString.POOLING_DISABLE ) ;
                     
                     strConnectionString = ConnectionStringBuilder.ToString ( ) ;
                  }
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false ) ;
                  
                  throw exception ;
               }
            }
            
            
            private static void RemovePoolingKeyValue
            (
               ref string strConnectionString
            )
            {
               try
               {
                  int nStartIndexOfPool = -1 ;
                  int nEndIndexOfPool   = -1 ;
                              
                              
                  strConnectionString = strConnectionString.ToLower ( ) ;
                                
                  nStartIndexOfPool = strConnectionString.IndexOf ( Constants.ConnectionString.POOLING_KEYWORD ) ;
                              
                  if ( -1 != nStartIndexOfPool ) 
                  {
                     int nPoolingKeyValueLength = -1 ;
                     
                     
                     nEndIndexOfPool = strConnectionString.IndexOf ( Constants.SpecialCharacters.SEMICOLON,
                                                                     nStartIndexOfPool ) ;
                                 
                     if ( -1 == nEndIndexOfPool ) 
                     {
                        nEndIndexOfPool = strConnectionString.Length - 1 ;
                     }
                     
                     nPoolingKeyValueLength = ( nEndIndexOfPool - nStartIndexOfPool ) + 1 ;
                     
                     strConnectionString = strConnectionString.Remove ( nStartIndexOfPool,
                                                                        nPoolingKeyValueLength ) ;
                  }
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false ) ;
                  
                  throw exception ;
               }
            }
         
            
            //private static void KillProcess
            //(
            //   SqlConnectionStringBuilder connectionBuilder,
            //   int nProcessID
            //)
            //{
            //   try
            //   {
            //      DNSQLDMO.SQLServer ServerClass = null ;
                  
                  
            //      ServerClass = new DNSQLDMO.SQLServerClass ( ) ;
                  
            //      ServerClass.LoginSecure = connectionBuilder.IntegratedSecurity ;
                  
            //      ServerClass.Connect ( connectionBuilder.DataSource,
            //                            connectionBuilder.UserID,
            //                            connectionBuilder.Password) ;
                  
            //      ServerClass.KillProcess ( nProcessID ) ;
            //   }
            //   catch ( Exception exception )
            //   {
            //      System.Diagnostics.Debug.Assert ( false ) ;
                  
            //      throw exception ;
            //   }
            //}
            
            
         #endregion
            
         #region Properties
   
         #endregion
   
         #region Events
   
         #endregion
   
         #region Data Members
            
         #endregion
            
         #region Data Types Definition
            
            private class Constants
            {
               public class ConnectionString
               {
                  public const string POOLING_KEYWORD     = "pooling" ;
                  public const string POOLING_DISABLE     = "pooling=false;" ;       
                  public const string POOLING_ENABLE      = "pooling=true;" ;
                  public const string Integrated_Security = "Integrated Security" ;
                  public const string Trusted_Connection  = "Trusted_Connection" ;
               }
               
               public class SpecialCharacters
               {
                  public const string SEMICOLON = ";" ;
               }
               
               public class ADOCommandText
               {
                  public const string SELECT_RECORDS_ALL                           = "SELECT * FROM {0}" ;
                  public const string SELECT_RECORDS_TOP                           = "SELECT TOP {0} * FROM {1}" ;
                  public const string SELECT_RECORDS_COUNT                         = "SELECT COUNT ( * ) FROM {0}" ;
                  public const string SELECT_ACTIVE_PROCESSES_EXCEPT_CURRENT_COUNT = "SELECT Count ( Process.spid ) FROM MASTER.dbo.sysdatabases Db, Master.dbo.sysprocesses Process WHERE Db.dbid = Process.dbid AND Db.name = '{0}' AND Process.dbid != @@SPID" ;
                  public const string SELECT_ACTIVE_PROCESSES_EXCEPT_CURRENT       = "SELECT Process.spid FROM MASTER.dbo.sysdatabases Db, Master.dbo.sysprocesses Process WHERE Db.dbid = Process.dbid AND Db.name = '{0}' AND Process.dbid != @@SPID" ;
                  public const string KILL_PROCESS                                 = "EXEC KILL {0}" ;
               }
               
               public class SqlScript
               {
                  public const string SelectTable    = "SELECT COUNT ( * ) FROM dbo.sysobjects where id = object_id(N'{0}') and OBJECTPROPERTY(id, N'IsUserTable') = 1" ;
                  public const string CreateDatabase = "CREATE DATABASE [{0}]" ;
                  public const string DropDatabase   = "DROP DATABASE [{0}]" ;
                  public const string SelectDatabasesName = "select Name from master.dbo.sysdatabases" ;
                  public const string SelectDatabase      = "SELECT Name from master.dbo.sysdatabases WHERE Name='{0}'";
               }
            }
            
            
         #endregion
         
      #endregion
      
      #region internal
      
         #region Methods
   
         #endregion
            
         #region Properties
   
         #endregion
   
         #region Events
   
         #endregion
            
         #region Data Types Definition
   
         #endregion
   
         #region Callbacks
   
         #endregion
         
      #endregion
   }

}
