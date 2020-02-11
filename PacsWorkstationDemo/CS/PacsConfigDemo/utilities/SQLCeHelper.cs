// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
// ===============================================================================
// Microsoft Data Access Application Block for .NET
// http://msdn.microsoft.com/library/en-us/dnbda/html/daab-rm.asp
//
// SQLHelper.cs
//
// This file contains the implementations of the SqlCeHelper and SqlCeHelperParameterCache
// classes.
//
// For more information see the Data Access Application Block Implementation Overview. 
// ===============================================================================
// Release history
// VERSION	DESCRIPTION
//   2.0	Added support for FillDataset, UpdateDataset and "Param" helper methods
//
// SqlServerCe: Removed all functions related to stored procedures and XmlReader
//
// ===============================================================================
// Copyright (C) 2000-2001 Microsoft Corporation
// All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR
// FITNESS FOR A PARTICULAR PURPOSE.
// ==============================================================================

using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlServerCe;
using System.Collections;
using System.Xml;

namespace PACSConfigDemo
{
    /// <summary>
    /// The SqlCeHelper class is intended to encapsulate high performance, scalable best practices for 
    /// common uses of SqlCeClient
    /// </summary>
    public sealed class SqlCeHelper
    {
        #region private utility methods & constructors

        // Since this class provides only static methods, make the default constructor private to prevent 
        // instances from being created with "new SqlCeHelper()"
        private SqlCeHelper() { }

        /// <summary>
        /// This method is used to attach array of SqlCeParameters to a SqlCeCommand.
        /// 
        /// This method will assign a value of DbNull to any parameter with a direction of
        /// InputOutput and a value of null.  
        /// 
        /// This behavior will prevent default values from being used, but
        /// this will be the less common case than an intended pure output parameter (derived as InputOutput)
        /// where the user provided no input value.
        /// </summary>
        /// <param name="command">The command to which the parameters will be added</param>
        /// <param name="commandParameters">An array of SqlCeParameters to be added to command</param>
        private static void AttachParameters(SqlCeCommand command, SqlCeParameter[] commandParameters)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandParameters != null)
            {
                foreach (SqlCeParameter p in commandParameters)
                {
                    if (p != null)
                    {
                        // Check for derived output value with no value assigned
                        if ((p.Direction == ParameterDirection.InputOutput ||
                          p.Direction == ParameterDirection.Input) &&
                          (p.Value == null))
                        {
                            p.Value = DBNull.Value;
                        }
                        command.Parameters.Add(p);
                    }
                }
            }
        }

        /// <summary>
        /// This method assigns dataRow column values to an array of SqlCeParameters
        /// </summary>
        /// <param name="commandParameters">Array of SqlCeParameters to be assigned values</param>
        /// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values</param>
        private static void AssignParameterValues(SqlCeParameter[] commandParameters, DataRow dataRow)
        {
            if ((commandParameters == null) || (dataRow == null))
            {
                // Do nothing if we get no data
                return;
            }

            int i = 0;
            // Set the parameters values
            foreach (SqlCeParameter commandParameter in commandParameters)
            {
                // Check the parameter name
                if (commandParameter.ParameterName == null ||
                  commandParameter.ParameterName.Length <= 1)
                    throw new Exception(
                      string.Format(
                      "Please provide a valid parameter name on the parameter #{0}, the ParameterName property has the following value: '{1}'.",
                      i, commandParameter.ParameterName));
                if (dataRow.Table.Columns.IndexOf(commandParameter.ParameterName.Substring(1)) != -1)
                    commandParameter.Value = dataRow[commandParameter.ParameterName.Substring(1)];
                i++;
            }
        }

        /// <summary>
        /// This method assigns an array of values to an array of SqlCeParameters
        /// </summary>
        /// <param name="commandParameters">Array of SqlCeParameters to be assigned values</param>
        /// <param name="parameterValues">Array of objects holding the values to be assigned</param>
        private static void AssignParameterValues(SqlCeParameter[] commandParameters, object[] parameterValues)
        {
            if ((commandParameters == null) || (parameterValues == null))
            {
                // Do nothing if we get no data
                return;
            }

            // We must have the same number of values as we pave parameters to put them in
            if (commandParameters.Length != parameterValues.Length)
            {
                throw new ArgumentException("Parameter count does not match Parameter Value count.");
            }

            // Iterate through the SqlCeParameters, assigning the values from the corresponding position in the 
            // value array
            for (int i = 0, j = commandParameters.Length; i < j; i++)
            {
                // If the current array value derives from IDbDataParameter, then assign its Value property
                if (parameterValues[i] is IDbDataParameter)
                {
                    IDbDataParameter paramInstance = (IDbDataParameter)parameterValues[i];
                    if (paramInstance.Value == null)
                    {
                        commandParameters[i].Value = DBNull.Value;
                    }
                    else
                    {
                        commandParameters[i].Value = paramInstance.Value;
                    }
                }
                else if (parameterValues[i] == null)
                {
                    commandParameters[i].Value = DBNull.Value;
                }
                else
                {
                    commandParameters[i].Value = parameterValues[i];
                }
            }
        }

        /// <summary>
        /// This method opens (if necessary) and assigns a connection, transaction, command type and parameters 
        /// to the provided command
        /// </summary>
        /// <param name="command">The SqlCeCommand to be prepared</param>
        /// <param name="connection">A valid SqlCeConnection, on which to execute this command</param>
        /// <param name="transaction">A valid SqlCeTransaction, or 'null'</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlCeParameters to be associated with the command or 'null' if no parameters are required</param>
        /// <param name="mustCloseConnection"><c>true</c> if the connection was opened by the method, otherwose is false.</param>
        private static void PrepareCommand(SqlCeCommand command, SqlCeConnection connection, SqlCeTransaction transaction, string commandText, SqlCeParameter[] commandParameters, out bool mustCloseConnection)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

            // If the provided connection is not open, we will open it
            if (connection.State != ConnectionState.Open)
            {
                mustCloseConnection = true;
                connection.Open();
            }
            else
            {
                mustCloseConnection = false;
            }

            // Associate the connection with the command
            command.Connection = connection;

            // Set the command text (stored procedure name or SQL statement)
            command.CommandText = commandText;

            // If we were provided a transaction, assign it
            if (transaction != null)
            {
                if (transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
                command.Transaction = transaction;
            }

            // Attach the command parameters if they are provided
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
            return;
        }

        #endregion private utility methods & constructors

        public static SqlCeResultSet ExecuteResultSet(string connectionString, string Table)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");

            // Create & open a SqlCeConnection, and dispose of it after we are done
            SqlCeConnection connection = new SqlCeConnection(connectionString);
            SqlCeCommand cmd = null;

            connection.Open();
            cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.TableDirect;
            cmd.CommandText = Table;

            return cmd.ExecuteResultSet(ResultSetOptions.Updatable | ResultSetOptions.Scrollable);
        }

        #region CreateCommand
        /// <summary>
        /// Simplify the creation of a Sql command object by allowing
        /// optional parameters to be provided.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  SqlCommand command = CreateCommand(conn, "AddCustomer", "CustomerID", "CustomerName");
        /// </remarks>
        /// <param name="connection">A valid SqlConnection object</param>
        /// <param name="commandText">The command text</param>
        /// <param name="sourceColumns">An array of string to be assigned as the source columns of the stored procedure parameters</param>
        /// <returns>A valid SqlCommand object</returns>
        public static SqlCeCommand CreateCommand(SqlCeConnection connection, string commandText, params string[] sourceColumns)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

            // Create a SqlCommand
            SqlCeCommand cmd = new SqlCeCommand(commandText, connection);
            cmd.CommandType = CommandType.Text;

            // If we receive parameter values, we need to figure out where they go
            if ((sourceColumns != null) && (sourceColumns.Length > 0))
            {
                // Create the parameters
                SqlCeParameter[] commandParameters = new SqlCeParameter[sourceColumns.Length];

                // Assign the provided source columns to these parameters based on parameter order
                for (int index = 0; index < sourceColumns.Length; index++)
                {
                    string[] s = sourceColumns[index].Split(':');
                    commandParameters[index] = new SqlCeParameter();
                    commandParameters[index].ParameterName = "p" + index.ToString();
                    commandParameters[index].DbType = DbTypeParse(s[1]); //DbType.DbType.(DbType)Type.GetType("System.Data.DbType." +
                    commandParameters[index].SourceColumn = s[0];
                }

                // Attach the discovered parameters to the SqlCommand object
                AttachParameters(cmd, commandParameters);
            }
            return cmd;
        }

        private static DbType DbTypeParse(string s)
        {
            DbType t;
            switch (s)
            {
                case "Binary": t = DbType.Binary; break;
                case "Boolean": t = DbType.Boolean; break;
                case "Byte": t = DbType.Byte; break;
                case "Currency": t = DbType.Currency; break;
                case "DateTime": t = DbType.DateTime; break;
                case "Decimal": t = DbType.Decimal; break;
                case "Double": t = DbType.Double; break;
                case "Guid": t = DbType.Guid; break;
                case "Int16": t = DbType.Int16; break;
                case "Int32": t = DbType.Int32; break;
                case "Int64": t = DbType.Int64; break;
                case "Object": t = DbType.Object; break;
                case "SByte": t = DbType.SByte; break;
                case "Single": t = DbType.Single; break;
                case "String": t = DbType.String; break;
                case "UInt16": t = DbType.UInt16; break;
                case "UInt32": t = DbType.UInt32; break;
                case "UInt64": t = DbType.UInt64; break;
                case "VarNumeric": t = DbType.VarNumeric; break;
                default: t = DbType.String; break;
            }
            return t;
        }
        #endregion

        #region ExecuteNonQuery

        /// <summary>
        /// Execute a SqlCeCommand (that returns no resultset and takes no parameters) against the database specified in 
        /// the connection string
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders");
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a SqlCeConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(string connectionString, string commandText)
        {
            // Pass through the call providing null for the set of SqlCeParameters
            return ExecuteNonQuery(connectionString, commandText, (SqlCeParameter[])null);
        }

        /// <summary>
        /// Execute a SqlCeCommand (that returns no resultset) against the database specified in the connection string 
        /// using the provided parameters
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlCeParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a SqlCeConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlCeParamters used to execute the command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(string connectionString, string commandText, params SqlCeParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");

            // Create & open a SqlCeConnection, and dispose of it after we are done
            using (SqlCeConnection connection = new SqlCeConnection(connectionString))
            {
                connection.Open();

                // Call the overload that takes a connection in place of the connection string
                return ExecuteNonQuery(connection, commandText, commandParameters);
            }
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns no resultset) against the database specified in 
        /// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, "PublishOrders", 24, 36);
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a SqlConnection</param>
        /// <param name="spName">The name of the stored prcedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(string connectionString, string commandText, params object[] parameterValues)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Create the parameters
                SqlCeParameter[] commandParameters = new SqlCeParameter[parameterValues.Length];

                // Assign the provided source columns to these parameters based on parameter order
                for (int index = 0; index < parameterValues.Length; index++)
                    commandParameters[index] = new SqlCeParameter("p" + index.ToString(), parameterValues[index]);

                // Call the overload that takes an array of SqlParameters
                return ExecuteNonQuery(connectionString, commandText, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteNonQuery(connectionString, commandText);
            }
        }

        /// <summary>
        /// Execute a SqlCeCommand (that returns no resultset and takes no parameters) against the provided SqlCeConnection. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(conn, CommandType.StoredProcedure, "PublishOrders");
        /// </remarks>
        /// <param name="connection">A valid SqlCeConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(SqlCeConnection connection, string commandText)
        {
            // Pass through the call providing null for the set of SqlCeParameters
            return ExecuteNonQuery(connection, commandText, (SqlCeParameter[])null);
        }

        /// <summary>
        /// Execute a SqlCeCommand (that returns no resultset) against the specified SqlCeConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(conn, CommandType.StoredProcedure, "PublishOrders", new SqlCeParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">A valid SqlCeConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlCeParamters used to execute the command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(SqlCeConnection connection, string commandText, params SqlCeParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            // Create a command and prepare it for execution
            SqlCeCommand cmd = new SqlCeCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, connection, (SqlCeTransaction)null, commandText, commandParameters, out mustCloseConnection);

            // Finally, execute the command
            int retval = cmd.ExecuteNonQuery();

            // Detach the SqlCeParameters from the command object, so they can be used again
            cmd.Parameters.Clear();
            if (mustCloseConnection)
                connection.Close();
            return retval;
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns no resultset) against the specified SqlConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  int result = ExecuteNonQuery(conn, "PublishOrders", 24, 36);
        /// </remarks>
        /// <param name="connection">A valid SqlConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(SqlCeConnection connection, string commandText, params object[] parameterValues)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Create the parameters
                SqlCeParameter[] commandParameters = new SqlCeParameter[parameterValues.Length];

                // Assign the provided source columns to these parameters based on parameter order
                for (int index = 0; index < parameterValues.Length; index++)
                    commandParameters[index] = new SqlCeParameter("p" + index.ToString(), parameterValues[index]);

                // Call the overload that takes an array of SqlParameters
                return ExecuteNonQuery(connection, commandText, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteNonQuery(connection, commandText);
            }
        }

        /// <summary>
        /// Execute a SqlCeCommand (that returns no resultset and takes no parameters) against the provided SqlCeTransaction. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(trans, CommandType.StoredProcedure, "PublishOrders");
        /// </remarks>
        /// <param name="transaction">A valid SqlCeTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(SqlCeTransaction transaction, string commandText)
        {
            // Pass through the call providing null for the set of SqlCeParameters
            return ExecuteNonQuery(transaction, commandText, (SqlCeParameter[])null);
        }

        /// <summary>
        /// Execute a SqlCeCommand (that returns no resultset) against the specified SqlCeTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(trans, CommandType.StoredProcedure, "GetOrders", new SqlCeParameter("@prodid", 24));
        /// </remarks>
        /// <param name="transaction">A valid SqlCeTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlCeParamters used to execute the command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(SqlCeTransaction transaction, string commandText, params SqlCeParameter[] commandParameters)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

            // Create a command and prepare it for execution
            SqlCeCommand cmd = new SqlCeCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, transaction.Connection as SqlCeConnection, transaction, commandText, commandParameters, out mustCloseConnection);

            // Finally, execute the command
            int retval = cmd.ExecuteNonQuery();

            // Detach the SqlCeParameters from the command object, so they can be used again
            cmd.Parameters.Clear();
            return retval;
        }

        #endregion ExecuteNonQuery

        #region ExecuteDataset

        /// <summary>
        /// Execute a SqlCeCommand (that returns a resultset and takes no parameters) against the database specified in 
        /// the connection string. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(connString, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a SqlCeConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataset(string connectionString, string commandText)
        {
            // Pass through the call providing null for the set of SqlCeParameters
            return ExecuteDataset(connectionString, commandText, (SqlCeParameter[])null);
        }

        /// <summary>
        /// Execute a SqlCeCommand (that returns a resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(connString, CommandType.StoredProcedure, "GetOrders", new SqlCeParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a SqlCeConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlCeParamters used to execute the command</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataset(string connectionString, string commandText, params SqlCeParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");

            // Create & open a SqlCeConnection, and dispose of it after we are done
            using (SqlCeConnection connection = new SqlCeConnection(connectionString))
            {
                connection.Open();

                // Call the overload that takes a connection in place of the connection string
                return ExecuteDataset(connection, commandText, commandParameters);
            }
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the database specified in 
        /// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(connString, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a SqlConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataset(string connectionString, string commandText, params object[] parameterValues)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Create the parameters
                SqlCeParameter[] commandParameters = new SqlCeParameter[parameterValues.Length];

                // Assign the provided source columns to these parameters based on parameter order
                for (int index = 0; index < parameterValues.Length; index++)
                    commandParameters[index] = new SqlCeParameter("p" + index.ToString(), parameterValues[index]);

                // Call the overload that takes an array of SqlParameters
                return ExecuteDataset(connectionString, commandText, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteDataset(connectionString, commandText);
            }
        }

        /// <summary>
        /// Execute a SqlCeCommand (that returns a resultset and takes no parameters) against the provided SqlCeConnection. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(conn, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="connection">A valid SqlCeConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataset(SqlCeConnection connection, string commandText)
        {
            // Pass through the call providing null for the set of SqlCeParameters
            return ExecuteDataset(connection, commandText, (SqlCeParameter[])null);
        }

        /// <summary>
        /// Execute a SqlCeCommand (that returns a resultset) against the specified SqlCeConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(conn, CommandType.StoredProcedure, "GetOrders", new SqlCeParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">A valid SqlCeConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlCeParamters used to execute the command</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataset(SqlCeConnection connection, string commandText, params SqlCeParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            // Create a command and prepare it for execution
            SqlCeCommand cmd = new SqlCeCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, connection, (SqlCeTransaction)null, commandText, commandParameters, out mustCloseConnection);

            // Create the DataAdapter & DataSet
            SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);

            DataSet ds = new DataSet();

            // Fill the DataSet using default values for DataTable names, etc
            da.Fill(ds);

            // Detach the SqlCeParameters from the command object, so they can be used again
            cmd.Parameters.Clear();

            da.Dispose();

            if (mustCloseConnection)
                connection.Close();

            // Return the dataset
            return ds;
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(conn, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="connection">A valid SqlConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataset(SqlCeConnection connection, string commandText, params object[] parameterValues)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Create the parameters
                SqlCeParameter[] commandParameters = new SqlCeParameter[parameterValues.Length];

                // Assign the provided source columns to these parameters based on parameter order
                for (int index = 0; index < parameterValues.Length; index++)
                    commandParameters[index] = new SqlCeParameter("p" + index.ToString(), parameterValues[index]);

                // Call the overload that takes an array of SqlParameters
                return ExecuteDataset(connection, commandText, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteDataset(connection, commandText);
            }
        }

        /// <summary>
        /// Execute a SqlCeCommand (that returns a resultset and takes no parameters) against the provided SqlCeTransaction. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(trans, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="transaction">A valid SqlCeTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataset(SqlCeTransaction transaction, string commandText)
        {
            // Pass through the call providing null for the set of SqlCeParameters
            return ExecuteDataset(transaction, commandText, (SqlCeParameter[])null);
        }

        /// <summary>
        /// Execute a SqlCeCommand (that returns a resultset) against the specified SqlCeTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(trans, CommandType.StoredProcedure, "GetOrders", new SqlCeParameter("@prodid", 24));
        /// </remarks>
        /// <param name="transaction">A valid SqlCeTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlCeParamters used to execute the command</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataset(SqlCeTransaction transaction, string commandText, params SqlCeParameter[] commandParameters)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

            // Create a command and prepare it for execution
            SqlCeCommand cmd = new SqlCeCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, transaction.Connection as SqlCeConnection, transaction, commandText, commandParameters, out mustCloseConnection);

            // Create the DataAdapter & DataSet
            SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);

            DataSet ds = new DataSet();

            // Fill the DataSet using default values for DataTable names, etc
            da.Fill(ds);

            // Detach the SqlCeParameters from the command object, so they can be used again
            cmd.Parameters.Clear();

            da.Dispose();

            // Return the dataset
            return ds;
        }

        #endregion ExecuteDataset

        #region ExecuteReader

        /// <summary>
        /// This enum is used to indicate whether the connection was provided by the caller, or created by SqlCeHelper, so that
        /// we can set the appropriate CommandBehavior when calling ExecuteReader()
        /// </summary>
        private enum SqlCeConnectionOwnership
        {
            /// <summary>Connection is owned and managed by SqlCeHelper</summary>
            Internal,
            /// <summary>Connection is owned and managed by the caller</summary>
            External
        }

        /// <summary>
        /// Create and prepare a SqlCeCommand, and call ExecuteReader with the appropriate CommandBehavior.
        /// </summary>
        /// <remarks>
        /// If we created and opened the connection, we want the connection to be closed when the DataReader is closed.
        /// 
        /// If the caller provided the connection, we want to leave it to them to manage.
        /// </remarks>
        /// <param name="connection">A valid SqlCeConnection, on which to execute this command</param>
        /// <param name="transaction">A valid SqlCeTransaction, or 'null'</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlCeParameters to be associated with the command or 'null' if no parameters are required</param>
        /// <param name="connectionOwnership">Indicates whether the connection parameter was provided by the caller, or created by SqlCeHelper</param>
        /// <returns>SqlCeDataReader containing the results of the command</returns>
        private static SqlCeDataReader ExecuteReader(SqlCeConnection connection, SqlCeTransaction transaction, string commandText, SqlCeParameter[] commandParameters, SqlCeConnectionOwnership connectionOwnership)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            bool mustCloseConnection = false;
            // Create a command and prepare it for execution
            SqlCeCommand cmd = new SqlCeCommand();
            try
            {
                PrepareCommand(cmd, connection, transaction, commandText, commandParameters, out mustCloseConnection);

                // Create a reader
                SqlCeDataReader dataReader;

                // Call ExecuteReader with the appropriate CommandBehavior
                if (connectionOwnership == SqlCeConnectionOwnership.External)
                {
                    dataReader = cmd.ExecuteReader();
                }
                else
                {
                    dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }

                // Detach the SqlCeParameters from the command object, so they can be used again.
                // when the reader is closed, so if the parameters are detached from the command
                // then the SqlCeReader can't set its values. 
                // When this happen, the parameters can't be used again in other command.
                bool canClear = true;
                foreach (SqlCeParameter commandParameter in cmd.Parameters)
                {
                    if (commandParameter.Direction != ParameterDirection.Input)
                        canClear = false;
                }

                if (canClear)
                {
                    cmd.Parameters.Clear();
                }

                return dataReader;
            }
            catch
            {
                if (mustCloseConnection)
                    connection.Close();
                throw;
            }
        }

        /// <summary>
        /// Execute a SqlCeCommand (that returns a resultset and takes no parameters) against the database specified in 
        /// the connection string. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  SqlCeDataReader dr = ExecuteReader(connString, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a SqlCeConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>A SqlCeDataReader containing the resultset generated by the command</returns>
        public static SqlCeDataReader ExecuteReader(string connectionString, string commandText)
        {
            // Pass through the call providing null for the set of SqlCeParameters
            return ExecuteReader(connectionString, commandText, (SqlCeParameter[])null);
        }

        /// <summary>
        /// Execute a SqlCeCommand (that returns a resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  SqlCeDataReader dr = ExecuteReader(connString, CommandType.StoredProcedure, "GetOrders", new SqlCeParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a SqlCeConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlCeParamters used to execute the command</param>
        /// <returns>A SqlCeDataReader containing the resultset generated by the command</returns>
        public static SqlCeDataReader ExecuteReader(string connectionString, string commandText, params SqlCeParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            SqlCeConnection connection = null;
            try
            {
                connection = new SqlCeConnection(connectionString);
                connection.Open();

                // Call the private overload that takes an internally owned connection in place of the connection string
                return ExecuteReader(connection, null, commandText, commandParameters, SqlCeConnectionOwnership.Internal);
            }
            catch
            {
                // If we fail to return the SqlCeDataReader, we need to close the connection ourselves
                if (connection != null) connection.Close();
                throw;
            }

        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the database specified in 
        /// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  SqlDataReader dr = ExecuteReader(connString, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a SqlConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A SqlDataReader containing the resultset generated by the command</returns>
        public static SqlCeDataReader ExecuteReader(string connectionString, string commandText, params object[] parameterValues)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Create the parameters
                SqlCeParameter[] commandParameters = new SqlCeParameter[parameterValues.Length];

                // Assign the provided source columns to these parameters based on parameter order
                for (int index = 0; index < parameterValues.Length; index++)
                    commandParameters[index] = new SqlCeParameter("p" + index.ToString(), parameterValues[index]);

                return ExecuteReader(connectionString, commandText, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteReader(connectionString, commandText);
            }
        }

        /// <summary>
        /// Execute a SqlCeCommand (that returns a resultset and takes no parameters) against the provided SqlCeConnection. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  SqlCeDataReader dr = ExecuteReader(conn, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="connection">A valid SqlCeConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>A SqlCeDataReader containing the resultset generated by the command</returns>
        public static SqlCeDataReader ExecuteReader(SqlCeConnection connection, string commandText)
        {
            // Pass through the call providing null for the set of SqlCeParameters
            return ExecuteReader(connection, commandText, (SqlCeParameter[])null);
        }

        /// <summary>
        /// Execute a SqlCeCommand (that returns a resultset) against the specified SqlCeConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  SqlCeDataReader dr = ExecuteReader(conn, CommandType.StoredProcedure, "GetOrders", new SqlCeParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">A valid SqlCeConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlCeParamters used to execute the command</param>
        /// <returns>A SqlCeDataReader containing the resultset generated by the command</returns>
        public static SqlCeDataReader ExecuteReader(SqlCeConnection connection, string commandText, params SqlCeParameter[] commandParameters)
        {
            // Pass through the call to the private overload using a null transaction value and an externally owned connection
            return ExecuteReader(connection, (SqlCeTransaction)null, commandText, commandParameters, SqlCeConnectionOwnership.External);
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  SqlDataReader dr = ExecuteReader(conn, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="connection">A valid SqlConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the command</param>
        /// <returns>A SqlDataReader containing the resultset generated by the command</returns>
        public static SqlCeDataReader ExecuteReader(SqlCeConnection connection, string commandText, params object[] parameterValues)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Create the parameters
                SqlCeParameter[] commandParameters = new SqlCeParameter[parameterValues.Length];

                // Assign the provided source columns to these parameters based on parameter order
                for (int index = 0; index < parameterValues.Length; index++)
                    commandParameters[index] = new SqlCeParameter("p" + index.ToString(), parameterValues[index]);

                return ExecuteReader(connection, commandText, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteReader(connection, commandText);
            }
        }

        /// <summary>
        /// Execute a SqlCeCommand (that returns a resultset and takes no parameters) against the provided SqlCeTransaction. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  SqlCeDataReader dr = ExecuteReader(trans, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="transaction">A valid SqlCeTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>A SqlCeDataReader containing the resultset generated by the command</returns>
        public static SqlCeDataReader ExecuteReader(SqlCeTransaction transaction, string commandText)
        {
            // Pass through the call providing null for the set of SqlCeParameters
            return ExecuteReader(transaction, commandText, (SqlCeParameter[])null);
        }

        /// <summary>
        /// Execute a SqlCeCommand (that returns a resultset) against the specified SqlCeTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///   SqlCeDataReader dr = ExecuteReader(trans, CommandType.StoredProcedure, "GetOrders", new SqlCeParameter("@prodid", 24));
        /// </remarks>
        /// <param name="transaction">A valid SqlCeTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlCeParamters used to execute the command</param>
        /// <returns>A SqlCeDataReader containing the resultset generated by the command</returns>
        public static SqlCeDataReader ExecuteReader(SqlCeTransaction transaction, string commandText, params SqlCeParameter[] commandParameters)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

            // Pass through to private overload, indicating that the connection is owned by the caller
            return ExecuteReader(transaction.Connection as SqlCeConnection, transaction, commandText, commandParameters, SqlCeConnectionOwnership.External);
        }

        #endregion ExecuteReader

        #region ExecuteScalar

        /// <summary>
        /// Execute a SqlCeCommand (that returns a 1x1 resultset and takes no parameters) against the database specified in 
        /// the connection string. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(connString, CommandType.StoredProcedure, "GetOrderCount");
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a SqlCeConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(string connectionString, string commandText)
        {
            // Pass through the call providing null for the set of SqlCeParameters
            return ExecuteScalar(connectionString, commandText, (SqlCeParameter[])null);
        }

        /// <summary>
        /// Execute a SqlCeCommand (that returns a 1x1 resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(connString, CommandType.StoredProcedure, "GetOrderCount", new SqlCeParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a SqlCeConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlCeParamters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(string connectionString, string commandText, params SqlCeParameter[] commandParameters)
      {
         if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
         // Create & open a SqlCeConnection, and dispose of it after we are done
         using (SqlCeConnection connection = new SqlCeConnection(connectionString))
         {
            try
            {
               connection.Open();
            }
            catch (Exception ex)
            {
               if (ex.Message.Contains(@"SqlCeEngine.Upgrade"))
               {
                  using (SqlCeEngine sqlCeEngine = new SqlCeEngine(connectionString))
                  {
                     sqlCeEngine.Upgrade();
                  }
               }
               else
               {
                  throw;
               }
            }

            // Call the overload that takes a connection in place of the connection string
            return ExecuteScalar(connection, commandText, commandParameters);
         }
      }

      /// <summary>
      /// Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the database specified in 
      /// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
      /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
      /// </summary>
      /// <remarks>
      /// This method provides no access to output parameters or the stored procedure's return value parameter.
      /// 
      /// e.g.:  
      ///  int orderCount = (int)ExecuteScalar(connString, "GetOrderCount", 24, 36);
      /// </remarks>
      /// <param name="connectionString">A valid connection string for a SqlConnection</param>
      /// <param name="spName">The name of the stored procedure</param>
      /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
      /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
      public static object ExecuteScalar(string connectionString, string commandText, params object[] parameterValues)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Create the parameters
                SqlCeParameter[] commandParameters = new SqlCeParameter[parameterValues.Length];

                // Assign the provided source columns to these parameters based on parameter order
                for (int index = 0; index < parameterValues.Length; index++)
                    commandParameters[index] = new SqlCeParameter("p" + index.ToString(), parameterValues[index]);

                // Call the overload that takes an array of SqlParameters
                return ExecuteScalar(connectionString, commandText, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteScalar(connectionString, commandText);
            }
        }

        /// <summary>
        /// Execute a SqlCeCommand (that returns a 1x1 resultset and takes no parameters) against the provided SqlCeConnection. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(conn, CommandType.StoredProcedure, "GetOrderCount");
        /// </remarks>
        /// <param name="connection">A valid SqlCeConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(SqlCeConnection connection, string commandText)
        {
            // Pass through the call providing null for the set of SqlCeParameters
            return ExecuteScalar(connection, commandText, (SqlCeParameter[])null);
        }

        /// <summary>
        /// Execute a SqlCeCommand (that returns a 1x1 resultset) against the specified SqlCeConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(conn, CommandType.StoredProcedure, "GetOrderCount", new SqlCeParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">A valid SqlCeConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlCeParamters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(SqlCeConnection connection, string commandText, params SqlCeParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            // Create a command and prepare it for execution
            SqlCeCommand cmd = new SqlCeCommand();

            bool mustCloseConnection = false;
            PrepareCommand(cmd, connection, (SqlCeTransaction)null, commandText, commandParameters, out mustCloseConnection);

            // Execute the command & return the results
            object retval = cmd.ExecuteScalar();

            // Detach the SqlCeParameters from the command object, so they can be used again
            cmd.Parameters.Clear();

            if (mustCloseConnection)
                connection.Close();

            return retval;
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the specified SqlConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(conn, "GetOrderCount", 24, 36);
        /// </remarks>
        /// <param name="connection">A valid SqlConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(SqlCeConnection connection, string commandText, params object[] parameterValues)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Create the parameters
                SqlCeParameter[] commandParameters = new SqlCeParameter[parameterValues.Length];

                // Assign the provided source columns to these parameters based on parameter order
                for (int index = 0; index < parameterValues.Length; index++)
                    commandParameters[index] = new SqlCeParameter("p" + index.ToString(), parameterValues[index]);

                // Call the overload that takes an array of SqlParameters
                return ExecuteScalar(connection, commandText, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                return ExecuteScalar(connection, commandText);
            }
        }

        /// Execute a SqlCeCommand (that returns a 1x1 resultset and takes no parameters) against the provided SqlCeTransaction. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(trans, CommandType.StoredProcedure, "GetOrderCount");
        /// </remarks>
        /// <param name="transaction">A valid SqlCeTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(SqlCeTransaction transaction, string commandText)
        {
            // Pass through the call providing null for the set of SqlCeParameters
            return ExecuteScalar(transaction, commandText, (SqlCeParameter[])null);
        }

        /// <summary>
        /// Execute a SqlCeCommand (that returns a 1x1 resultset) against the specified SqlCeTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(trans, CommandType.StoredProcedure, "GetOrderCount", new SqlCeParameter("@prodid", 24));
        /// </remarks>
        /// <param name="transaction">A valid SqlCeTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlCeParamters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(SqlCeTransaction transaction, string commandText, params SqlCeParameter[] commandParameters)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

            // Create a command and prepare it for execution
            SqlCeCommand cmd = new SqlCeCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, transaction.Connection as SqlCeConnection, transaction, commandText, commandParameters, out mustCloseConnection);

            // Execute the command & return the results
            object retval = cmd.ExecuteScalar();

            // Detach the SqlCeParameters from the command object, so they can be used again
            cmd.Parameters.Clear();
            return retval;
        }

        #endregion ExecuteScalar

        #region FillDataset
        /// <summary>
        /// Execute a SqlCeCommand (that returns a resultset and takes no parameters) against the database specified in 
        /// the connection string. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  FillDataset(connString, CommandType.StoredProcedure, "GetOrders", ds, new string[] {"orders"});
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a SqlCeConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
        /// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
        /// by a user defined name (probably the actual table name)</param>
        public static void FillDataset(string connectionString, string commandText, DataSet dataSet, string[] tableNames)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            if (dataSet == null) throw new ArgumentNullException("dataSet");

            // Create & open a SqlCeConnection, and dispose of it after we are done
            using (SqlCeConnection connection = new SqlCeConnection(connectionString))
            {
                connection.Open();

                // Call the overload that takes a connection in place of the connection string
                FillDataset(connection, commandText, dataSet, tableNames);
            }
        }

        /// <summary>
        /// Execute a SqlCeCommand (that returns a resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  FillDataset(connString, CommandType.StoredProcedure, "GetOrders", ds, new string[] {"orders"}, new SqlCeParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a SqlCeConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlCeParamters used to execute the command</param>
        /// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
        /// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
        /// by a user defined name (probably the actual table name)
        /// </param>
        public static void FillDataset(string connectionString, string commandText, DataSet dataSet, string[] tableNames,
          params SqlCeParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            if (dataSet == null) throw new ArgumentNullException("dataSet");
            // Create & open a SqlCeConnection, and dispose of it after we are done
            using (SqlCeConnection connection = new SqlCeConnection(connectionString))
            {
                connection.Open();

                // Call the overload that takes a connection in place of the connection string
                FillDataset(connection, commandText, dataSet, tableNames, commandParameters);
            }
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the database specified in 
        /// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  FillDataset(connString, CommandType.StoredProcedure, "GetOrders", ds, new string[] {"orders"}, 24);
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a SqlConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
        /// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
        /// by a user defined name (probably the actual table name)
        /// </param>    
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        public static void FillDataset(string connectionString, string commandText, DataSet dataSet, string[] tableNames, params object[] parameterValues)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            if (dataSet == null) throw new ArgumentNullException("dataSet");

            // Create & open a SqlConnection, and dispose of it after we are done
            using (SqlCeConnection connection = new SqlCeConnection(connectionString))
            {
                connection.Open();

                // Call the overload that takes a connection in place of the connection string
                FillDataset(connection, commandText, dataSet, tableNames, parameterValues);
            }
        }

        /// <summary>
        /// Execute a SqlCeCommand (that returns a resultset and takes no parameters) against the provided SqlCeConnection. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  FillDataset(conn, CommandType.StoredProcedure, "GetOrders", ds, new string[] {"orders"});
        /// </remarks>
        /// <param name="connection">A valid SqlCeConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
        /// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
        /// by a user defined name (probably the actual table name)
        /// </param>    
        public static void FillDataset(SqlCeConnection connection,
          string commandText, DataSet dataSet, string[] tableNames)
        {
            FillDataset(connection, commandText, dataSet, tableNames, null);
        }

        /// <summary>
        /// Execute a SqlCeCommand (that returns a resultset) against the specified SqlCeConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  FillDataset(conn, CommandType.StoredProcedure, "GetOrders", ds, new string[] {"orders"}, new SqlCeParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">A valid SqlCeConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
        /// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
        /// by a user defined name (probably the actual table name)
        /// </param>
        /// <param name="commandParameters">An array of SqlCeParamters used to execute the command</param>
        public static void FillDataset(SqlCeConnection connection,
          string commandText, DataSet dataSet, string[] tableNames,
          params SqlCeParameter[] commandParameters)
        {
            FillDataset(connection, null, commandText, dataSet, tableNames, commandParameters);
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  FillDataset(conn, "GetOrders", ds, new string[] {"orders"}, 24, 36);
        /// </remarks>
        /// <param name="connection">A valid SqlConnection</param>
        /// <param name="spName">The name of the stored procedure</param>
        /// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
        /// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
        /// by a user defined name (probably the actual table name)
        /// </param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        public static void FillDataset(SqlCeConnection connection, string commandText, DataSet dataSet, string[] tableNames, params object[] parameterValues)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (dataSet == null) throw new ArgumentNullException("dataSet");
            if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Create the parameters
                SqlCeParameter[] commandParameters = new SqlCeParameter[parameterValues.Length];

                // Assign the provided source columns to these parameters based on parameter order
                for (int index = 0; index < parameterValues.Length; index++)
                    commandParameters[index] = new SqlCeParameter("p" + index.ToString(), parameterValues[index]);

                // Call the overload that takes an array of SqlParameters
                FillDataset(connection, commandText, dataSet, tableNames, commandParameters);
            }
            else
            {
                // Otherwise we can just call the SP without params
                FillDataset(connection, commandText, dataSet, tableNames);
            }
        }

        /// <summary>
        /// Execute a SqlCeCommand (that returns a resultset and takes no parameters) against the provided SqlCeTransaction. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  FillDataset(trans, CommandType.StoredProcedure, "GetOrders", ds, new string[] {"orders"});
        /// </remarks>
        /// <param name="transaction">A valid SqlCeTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
        /// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
        /// by a user defined name (probably the actual table name)
        /// </param>
        public static void FillDataset(SqlCeTransaction transaction,
          string commandText,
          DataSet dataSet, string[] tableNames)
        {
            FillDataset(transaction, commandText, dataSet, tableNames, null);
        }

        /// <summary>
        /// Execute a SqlCeCommand (that returns a resultset) against the specified SqlCeTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  FillDataset(trans, CommandType.StoredProcedure, "GetOrders", ds, new string[] {"orders"}, new SqlCeParameter("@prodid", 24));
        /// </remarks>
        /// <param name="transaction">A valid SqlCeTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
        /// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
        /// by a user defined name (probably the actual table name)
        /// </param>
        /// <param name="commandParameters">An array of SqlCeParamters used to execute the command</param>
        public static void FillDataset(SqlCeTransaction transaction,
          string commandText, DataSet dataSet, string[] tableNames,
          params SqlCeParameter[] commandParameters)
        {
            FillDataset(transaction.Connection as SqlCeConnection, transaction, commandText, dataSet, tableNames, commandParameters);
        }

        /// <summary>
        /// Private helper method that execute a SqlCeCommand (that returns a resultset) against the specified SqlCeTransaction and SqlCeConnection
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  FillDataset(conn, trans, CommandType.StoredProcedure, "GetOrders", ds, new string[] {"orders"}, new SqlCeParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">A valid SqlCeConnection</param>
        /// <param name="transaction">A valid SqlCeTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
        /// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
        /// by a user defined name (probably the actual table name)
        /// </param>
        /// <param name="commandParameters">An array of SqlCeParamters used to execute the command</param>
        private static void FillDataset(SqlCeConnection connection, SqlCeTransaction transaction,
          string commandText, DataSet dataSet, string[] tableNames,
          params SqlCeParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (dataSet == null) throw new ArgumentNullException("dataSet");

            // Create a command and prepare it for execution
            SqlCeCommand command = new SqlCeCommand();
            bool mustCloseConnection = false;
            PrepareCommand(command, connection, transaction, commandText, commandParameters, out mustCloseConnection);

            // Create the DataAdapter & DataSet
            SqlCeDataAdapter dataAdapter = new SqlCeDataAdapter(command);

            // Add the table mappings specified by the user
            if (tableNames != null && tableNames.Length > 0)
            {
                string tableName = "Table";
                for (int index = 0; index < tableNames.Length; index++)
                {
                    if (tableNames[index] == null || tableNames[index].Length == 0) throw new ArgumentException("The tableNames parameter must contain a list of tables, a value was provided as null or empty string.", "tableNames");
                    dataAdapter.TableMappings.Add(tableName, tableNames[index]);
                    tableName += (index + 1).ToString();
                }
            }

            // Fill the DataSet using default values for DataTable names, etc
            dataAdapter.Fill(dataSet);

            // Detach the SqlCeParameters from the command object, so they can be used again
            command.Parameters.Clear();

            dataAdapter.Dispose();

            if (mustCloseConnection)
                connection.Close();
        }
        #endregion

        #region UpdateDataset
        public static void UpdateDataset(SqlCeConnection connection, DataSet dataSet, string tableName)
        {
            UpdateDataset(connection, dataSet, tableName, "*");
        }

        public static void UpdateDataset(SqlCeConnection connection, DataSet dataSet, string tableName, string fields)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (dataSet == null) throw new ArgumentNullException("dataSet");
            if (tableName == null || tableName.Length == 0) throw new ArgumentNullException("tableName");
            if (fields == null || fields.Length == 0) throw new ArgumentNullException("fields");

            // Create a SqlCeDataAdapter, and dispose of it after we are done
            SqlCeDataAdapter dataAdapter = new SqlCeDataAdapter("SELECT " + fields + " FROM " + tableName, connection);

            // Set the data adapter commands
            SqlCeCommandBuilder commandBuilder = new SqlCeCommandBuilder(dataAdapter);
            dataAdapter.InsertCommand = commandBuilder.GetInsertCommand();
            dataAdapter.UpdateCommand = commandBuilder.GetUpdateCommand();
            dataAdapter.DeleteCommand = commandBuilder.GetDeleteCommand();

            // Update the dataset changes in the data source
            dataAdapter.Update(dataSet, tableName);

            // Commit all the changes made to the DataSet
            dataSet.AcceptChanges();

            dataAdapter.Dispose();
        }

        public static void UpdateDataset(string connectionString, DataSet dataSet, string tableName)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");

            using (SqlCeConnection connection = new SqlCeConnection(connectionString))
            {
                connection.Open();

                // Call the overload that takes a connection in place of the connection string
                UpdateDataset(connection, dataSet, tableName, "*");
            }            
        }

        public static void UpdateDataset(string connectionString, DataSet dataSet, string tableName, string fields)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");

            // Create & open a SqlCeConnection, and dispose of it after we are done
            using (SqlCeConnection connection = new SqlCeConnection(connectionString))
            {
                connection.Open();

                // Call the overload that takes a connection in place of the connection string
                UpdateDataset(connection, dataSet, tableName, fields);
            }
        }

        /// <summary>
        /// Executes the respective command for each inserted, updated, or deleted row in the DataSet.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  UpdateDataset(conn, insertCommand, deleteCommand, updateCommand, dataSet, "Order");
        /// </remarks>
        /// <param name="insertCommand">A valid transact-SQL statement or stored procedure to insert new records into the data source</param>
        /// <param name="deleteCommand">A valid transact-SQL statement or stored procedure to delete records from the data source</param>
        /// <param name="updateCommand">A valid transact-SQL statement or stored procedure used to update records in the data source</param>
        /// <param name="dataSet">The DataSet used to update the data source</param>
        /// <param name="tableName">The DataTable used to update the data source.</param>
        public static void UpdateDataset(SqlCeCommand insertCommand, SqlCeCommand deleteCommand, SqlCeCommand updateCommand, DataSet dataSet, string tableName)
        {
            if (insertCommand == null) throw new ArgumentNullException("insertCommand");
            if (deleteCommand == null) throw new ArgumentNullException("deleteCommand");
            if (updateCommand == null) throw new ArgumentNullException("updateCommand");
            if (tableName == null || tableName.Length == 0) throw new ArgumentNullException("tableName");

            // Create a SqlCeDataAdapter, and dispose of it after we are done
            SqlCeDataAdapter dataAdapter = new SqlCeDataAdapter();

            // Set the data adapter commands
            dataAdapter.UpdateCommand = updateCommand;
            dataAdapter.InsertCommand = insertCommand;
            dataAdapter.DeleteCommand = deleteCommand;

            // Update the dataset changes in the data source
            dataAdapter.Update(dataSet, tableName);

            // Commit all the changes made to the DataSet
            dataSet.AcceptChanges();

            dataAdapter.Dispose();
        }
        #endregion
    }

    /// <summary>
    /// SqlCeHelperParameterCache provides functions to leverage a static cache of procedure parameters, and the
    /// ability to discover parameters for stored procedures at run-time.
    /// </summary>
    public sealed class SqlCeHelperParameterCache
    {
        #region private methods, variables, and constructors

        //Since this class provides only static methods, make the default constructor private to prevent 
        //instances from being created with "new SqlCeHelperParameterCache()"
        private SqlCeHelperParameterCache() { }

        private static Hashtable paramCache = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// Deep copy of cached SqlCeParameter array
        /// </summary>
        /// <param name="originalParameters"></param>
        /// <returns></returns>
        private static SqlCeParameter[] CloneParameters(SqlCeParameter[] originalParameters)
        {
            SqlCeParameter[] clonedParameters = new SqlCeParameter[originalParameters.Length];

            for (int i = 0, j = originalParameters.Length; i < j; i++)
            {
                clonedParameters[i] = (SqlCeParameter)((ICloneable)originalParameters[i]).Clone();
            }

            return clonedParameters;
        }

        #endregion private methods, variables, and constructors

        #region caching functions

        /// <summary>
        /// Add parameter array to the cache
        /// </summary>
        /// <param name="connectionString">A valid connection string for a SqlCeConnection</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlCeParamters to be cached</param>
        public static void CacheParameterSet(string connectionString, string commandText, params SqlCeParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

            string hashKey = connectionString + ":" + commandText;

            paramCache[hashKey] = commandParameters;
        }

        /// <summary>
        /// Retrieve a parameter array from the cache
        /// </summary>
        /// <param name="connectionString">A valid connection string for a SqlCeConnection</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An array of SqlCeParamters</returns>
        public static SqlCeParameter[] GetCachedParameterSet(string connectionString, string commandText)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

            string hashKey = connectionString + ":" + commandText;

            SqlCeParameter[] cachedParameters = paramCache[hashKey] as SqlCeParameter[];
            if (cachedParameters == null)
            {
                return null;
            }
            else
            {
                return CloneParameters(cachedParameters);
            }
        }

        #endregion caching functions
    }
}
