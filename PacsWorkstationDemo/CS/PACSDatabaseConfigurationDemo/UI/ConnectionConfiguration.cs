// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
//using System.Data.SqlServerCe ;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;
using System.IO;
using Leadtools.Demos;
using Leadtools.Demos.Sql;
using System.Diagnostics;


namespace MedicalWorkstationConfigurationDemo.UI
{
   public partial class ConnectionConfiguration : UserControl
   {
      #region Public

      #region Methods

      public ConnectionConfiguration()
      {
         InitializeComponent();

         Init();

         RegisterEvents();
      }

      public void SetConnectionString(string connectionString, string providerName)
      {
         if (providerName == ConnectionProviders.SqlCeProvider.Name)
         {
            sqlCeConfiguration.ConnectionString = connectionString;
         }
         else if (providerName == ConnectionProviders.SqlServerProvider.Name)
         {
            sqlServerConfiguration.ConnectionString = connectionString;
         }

         _datasrouces.SelectedProvider = providerName;

         DisplayProviderOptions(_datasrouces.SelectedSource,
                                  _datasrouces.SelectedProvider);
      }

      public void ClearConnectionString()
      {
         sqlCeConfiguration.ConnectionString = string.Empty;
         sqlServerConfiguration.ConnectionString = string.Empty;
      }

      public string DefaultSqlCeDatabaseName
      {
         get
         {
            return _defaultSqlCeDatabaseName;
         }

         set
         {
            sqlCeConfiguration.DefaultSqlCeDatabaseName = value;

            _defaultSqlCeDatabaseName = value;
         }
      }
      private string _defaultSqlCeDatabaseName;

      public string DefaultSqlServerDatabaseName
      {
         get
         {
            return _defaultSqlServerDatabaseName;
         }

         set
         {
            sqlServerConfiguration.DefaultDatabaseName = value;
            _defaultSqlServerDatabaseName = value;
         }
      }
      public string _defaultSqlServerDatabaseName;

      public bool IsDatabaseLocationAlreadyUsed(List<string> databaseLocations, string connectionString, string inputDatabase, out string connectionErrorMessage)
      {
         connectionErrorMessage = string.Empty;
         bool alreadyUsed = false;
         foreach (string location in databaseLocations)
         {
            if (string.Compare(location, connectionString, true) == 0)
               alreadyUsed = true;
         }

         if (alreadyUsed)
         {
            connectionErrorMessage = string.Format("This database has already been assigned.  Please choose a different name, or click the 'Back' button.\n\n{0}", inputDatabase);
         }
         return alreadyUsed;
      }

      public bool IsConnectionStringValidSqlCe(List<string> databaseLocations, out string connectionErrorMessage)
      {
          connectionErrorMessage = string.Empty;
          try
          {
              SqlCeConnection connection;

              if (IsDatabaseLocationAlreadyUsed(databaseLocations, ConnectionString, sqlCeConfiguration.ConnectionString, out connectionErrorMessage))
              {
                  return false;
              }

              using (connection = new SqlCeConnection(sqlCeConfiguration.ConnectionString))
              {

                  connection.Open();
                  connection.Close();

                  connectionErrorMessage = string.Empty;

                  return true;

              }
          }
          catch (Exception exception)
          {
              connectionErrorMessage = exception.Message;

              return false;
          }
      }

      public bool IsConnectionStringValid(out string connectionErrorMessage, List<string> databaseLocations)
      {
         try
         {
            if (Mode == DbConfigurationMode.Create)
            {
               if (sqlServerConfiguration.Visible)
               {
                  SqlConnectionStringBuilder csB;


                  csB = new SqlConnectionStringBuilder(sqlServerConfiguration.ConnectionString);

                  if (string.IsNullOrEmpty(csB.InitialCatalog))
                  {
                     connectionErrorMessage = "Database name is empty";

                     return false;
                  }
                  else
                  {
                     string dbName;
                     string fop;


                     dbName = csB.InitialCatalog;
                     fop = csB.FailoverPartner;

                     csB.InitialCatalog = string.Empty;
                     csB.FailoverPartner = string.Empty;

                     if (SqlUtilities.TestSQLConnectionString(csB.ConnectionString, out connectionErrorMessage))
                     {
                        csB.InitialCatalog = dbName;
                        csB.FailoverPartner = fop;

                        if (IsDatabaseLocationAlreadyUsed(databaseLocations, ConnectionString, csB.InitialCatalog, out connectionErrorMessage))
                        {
                           return false;
                        }

                        else if (SqlUtilities.DatabaseExist(csB))
                        {
                           DialogResult deleteExistent;


                           deleteExistent = Messager.ShowQuestion(this,
                                                                    "The database you selected already exists.\n\nDo you want to delete the existing database and create a new one?\n\n" +
                                                                    "Click 'Yes' to delete the existing database or click 'No' to change the database name.",
                                                                    MessageBoxButtons.YesNo);

                           if (deleteExistent == DialogResult.No)
                           {
                              connectionErrorMessage = string.Empty;//string.Format ( "Database {0} already exists. Please select a different database name.", dbName ) ;

                              sqlServerConfiguration.SetComponentFocus(SqlServerConfiguration.ConnectionStringCommponents.Database);

                              return false;
                           }
                           else
                           {
                              connectionErrorMessage = string.Empty;

                              return true;
                           }
                        }
                        else
                        {
                           connectionErrorMessage = string.Empty;

                           return true;
                        }
                     }
                     else
                     {
                        sqlServerConfiguration.SetComponentFocus(SqlServerConfiguration.ConnectionStringCommponents.Server);

                        return false;
                     }
                  }
               }
               else
               {
                  connectionErrorMessage = string.Empty;

                  if (String.IsNullOrEmpty(sqlCeConfiguration.ConnectionString))
                  {
                     connectionErrorMessage = "Database name is empty";

                     return false;
                  }
                  else if (IsDatabaseLocationAlreadyUsed(databaseLocations, ConnectionString, sqlCeConfiguration.DatabaseLocation, out connectionErrorMessage))
                  {
                     return false;
                  }
                  else if (File.Exists(sqlCeConfiguration.DatabaseLocation))
                  {
                     DialogResult deleteExistent;


                     deleteExistent = Messager.ShowQuestion(this,
                                                              "The database you selected already exists.\n\nDo you want to delete the existing database and create a new one?\n\n" +
                                                              "Click 'Yes' to delete the existing database or click 'No' to change the database name.",
                                                              MessageBoxButtons.YesNo);

                     if (deleteExistent == DialogResult.No)
                     {
                        connectionErrorMessage = string.Empty; //string.Format ( "Database {0} already exists. Please select a different database name.", sqlCeConfiguration.DatabaseLocation ) ;

                        return false;
                     }
                     else
                     {
                        return true;
                     }
                  }
                  else
                  {
                     return true;
                  }
               }
            }
            else
            {
               if (sqlServerConfiguration.Visible)
               {

                  SqlConnectionStringBuilder csB = new SqlConnectionStringBuilder(sqlServerConfiguration.ConnectionString);

                  if (string.IsNullOrEmpty(csB.InitialCatalog))
                  {
                     connectionErrorMessage = "Database name is empty";

                     return false;
                  }

                  csB.Pooling = false;

                  if (IsDatabaseLocationAlreadyUsed(databaseLocations, ConnectionString, csB.InitialCatalog, out connectionErrorMessage))
                  {
                     return false;
                  }

                  return SqlUtilities.TestSQLConnectionString(csB.ConnectionString, out connectionErrorMessage);
               }
               else
               {
                   return IsConnectionStringValidSqlCe(databaseLocations, out connectionErrorMessage);
               }
            }
         }
         catch (Exception exception)
         {
            System.Diagnostics.Debug.Assert(false, exception.Message);

            throw;
         }

      }

      #endregion

      #region Properties

      public bool CanChangeProvider
      {
         get
         {
            return btnChange.Visible;
         }
         set
         {
            btnChange.Visible = value;
         }
      }

      public string ConnectionString
      {
         get
         {
            if ( DataProvider == ConnectionProviders.SqlServerProvider.Name)
            {
               return sqlServerConfiguration.ConnectionString;
            }
            else
            {
               return sqlCeConfiguration.ConnectionString;
            }
         }
      }

      public string DataProvider
      {
         get
         {
            return (string)txtDataSource.Tag;
         }
      }
      
      public string DataSource
      {
         get
         {
            if ( DataProvider == ConnectionProviders.SqlServerProvider.Name)
            {
               return sqlServerConfiguration.DataSource;
            }
            else
            {
               return sqlCeConfiguration.DataSource;
            }
         }
      }


      public DbConfigurationMode Mode
      {
         get
         {
            return _configMode;
         }

         set
         {
            sqlCeConfiguration.Mode = value;
            sqlServerConfiguration.Mode = value;

            _configMode = value;
         }
      }

      public bool DatabaseVisible
      {
         get
         {
            return sqlServerConfiguration.DatabaseVisible;
         }

         set
         {
            sqlServerConfiguration.DatabaseVisible = value;
         }
      }

      public bool SqlMirroringVisible
      {
         get
         {
            return sqlServerConfiguration.SqlMirroringVisible;
         }

         set
         {
            sqlServerConfiguration.SqlMirroringVisible = value;
         }
      }
      
       public event EventHandler SettingsChanged;

            private void OnSettingsChanged(object sender, EventArgs e)
            {
               try
               {
                  if (SettingsChanged != null)
                  {
                     SettingsChanged(sender, e);
                  }
               }
               catch (Exception)
               {
                  System.Diagnostics.Debug.Assert(false);
               }
            }

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

      private void Init()
      {
         _datasrouces = new DataSourcesDialog();

         Mode = DbConfigurationMode.Configure;
         
         InitNotRecommendedWarning();

         DisplayProviderOptions(_datasrouces.SelectedSource,
                                  _datasrouces.SelectedProvider);
      }

      private void InitNotRecommendedWarning()
      {
         notRecommendedWarning.Initialize();
         notRecommendedWarning.Left = sqlServerConfiguration.Left;
         notRecommendedWarning.Top = sqlServerConfiguration.Top;
         notRecommendedWarning.Width = sqlServerConfiguration.Width;
         notRecommendedWarning.Height = sqlServerConfiguration.Height;
         notRecommendedWarning.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
      }

      private void RegisterEvents()
      {
         btnChange.Click += new EventHandler(btnChange_Click);
         btnHelp.Click += new EventHandler(btnHelp_Click);
         sqlServerConfiguration.SettingsChanged += new EventHandler(sqlServerConfiguration_SettingsChanged);
      }
      
      public string GetConnectionString()
      {
         return sqlServerConfiguration.ConnectionString;
      }

      public string GetDataSource()
      {
         return sqlServerConfiguration.GetDataSource();
      }

      void sqlServerConfiguration_SettingsChanged(object sender, EventArgs e)
      {
         OnSettingsChanged(sender, e);
      }

      private void DisplayProviderOptions
      (
         string sourceName,
         string dataProvider
      )
      {
         txtDataSource.Text = sourceName;
         txtDataSource.Tag = dataProvider;

         sqlCeConfiguration.Mode = Mode;
         sqlServerConfiguration.Mode = Mode;

         if (dataProvider == ConnectionProviders.SqlServerProvider.Name)
         {
            sqlCeConfiguration.Visible = false;

            sqlServerConfiguration.Visible = true;
            notRecommendedWarning.Hide();
         }
         else if (dataProvider == ConnectionProviders.SqlCeProvider.Name)
         {
            sqlCeConfiguration.Visible = true;
            sqlServerConfiguration.Visible = false;
            notRecommendedWarning.Show();
         }

      }

      #endregion

      #region Properties

      #endregion

      #region Events

      void btnChange_Click(object sender, EventArgs e)
      {
         _datasrouces.SelectedSource = txtDataSource.Text;

         if (_datasrouces.ShowDialog() == DialogResult.OK)
         {
            sqlServerConfiguration.ConnectionString = string.Empty;
            sqlCeConfiguration.ConnectionString = string.Empty;

            sqlServerConfiguration.DefaultDatabaseName = DefaultSqlServerDatabaseName;
            sqlCeConfiguration.DefaultSqlCeDatabaseName = DefaultSqlCeDatabaseName;

            DisplayProviderOptions(_datasrouces.SelectedSource,
                                     _datasrouces.SelectedProvider);
         }
      }

      void btnHelp_Click(object sender, EventArgs e)
      {
         Process.Start("https://www.leadtools.com/support/guides/azure-sql-troubleshooting-guide.pdf");
         return;
      }

      #endregion

      #region Data Members

      private DataSourcesDialog _datasrouces;
      private DbConfigurationMode _configMode;

      #endregion

      private void ConnectionConfiguration_Load(object sender, EventArgs e)
      {
         if (btnChange.Visible == false)
         {
            label1.Text = "Enter information to connect to the data source.";
         }
      }

      #region Data Types Definition

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
