// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Leadtools.Demos.Sql;
using System.Threading;
using MedicalWorkstationConfigurationDemo.UI;
using System.Reflection;

namespace CSPacsDatabaseConfigurationDemo.UI.New
{
   public partial class ChooseDefaultSqlServerDialog : Form
   {
      public ChooseDefaultSqlServerDialog()
      {
         InitializeComponent();
      }
      
      private bool _validated = false;

      private void ChooseDefaultSqlServerDialog_Load(object sender, EventArgs e)
      {
         sqlServerConfiguration1.DatabaseVisible = false;
         sqlServerConfiguration1.SqlMirroringVisible = false;
         radioButtonSqlServer.Checked = true;

         sqlServerConfiguration1.SettingsChanged += new EventHandler(sqlServerConfiguration1_SettingsChanged);
         radioButtonSqlServer.CheckedChanged += new EventHandler(radioButtonSqlServer_CheckedChanged);
         
         InitializeWarning();
         UpdateUI();
      }
      
      void InitializeWarning()
      {         
         notRecommendedWarning.Initialize();
         notRecommendedWarning.Top = sqlServerConfiguration1.Top;
         notRecommendedWarning.Left = sqlServerConfiguration1.Left;
         notRecommendedWarning.Width = sqlServerConfiguration1.Width;
         notRecommendedWarning.Height = sqlServerConfiguration1.Height;
      }

      void UpdateUI()
      {
         if (radioButtonSqlServer.Checked)
         {
            sqlServerConfiguration1.Show();
            buttonValidate.Show();
            labelValidate.Show();
            buttonOk.Enabled = _validated;
            notRecommendedWarning.Hide();
         }
         else
         {
            sqlServerConfiguration1.Hide();
            buttonValidate.Hide();
            labelValidate.Hide();
            buttonOk.Enabled = true;
            notRecommendedWarning.Show();
         }
      }
      
      void radioButtonSqlServer_CheckedChanged(object sender, EventArgs e)
      {
         UpdateUI();
      }

      void sqlServerConfiguration1_SettingsChanged(object sender, EventArgs e)
      {
         if (VerifyConnectionString)
         {
            _validated = false;
            buttonOk.Enabled = false;
         }
      }
      
      private bool _verifyConnectionString = false;
      public bool VerifyConnectionString
      {
         get
         {
            return _verifyConnectionString;
         }
         set
         {
            _verifyConnectionString = value;
            buttonOk.Enabled = !_verifyConnectionString;
         }
      }

      public string GetConnectionString()
      {
         return sqlServerConfiguration1.ConnectionString;
      }
            
      public string GetConnectionString(string initialCatalog)
      {
         SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder(sqlServerConfiguration1.ConnectionString);
         sb.InitialCatalog = initialCatalog;
         return sb.ConnectionString;
      }
      
      public string GetSqlServerInstance()
      {
         return sqlServerConfiguration1.GetDataSource();
      }
      
      private string _connectionErrorMessage = string.Empty;
      private string _sqlServerVersion = string.Empty;

      private bool ThreadTest(string connectionString)
      {
         string connectionErrorMessage;
         _sqlServerVersion = string.Empty;

         if (VerifyConnectionString)
         {
            if (SqlUtilities.TestSQLConnectionString(connectionString, out connectionErrorMessage, out _sqlServerVersion))
            {
               return true;
            }
            else
            {
               _connectionErrorMessage = connectionErrorMessage;
               return false;
            }
         }

         return true;
      }

      private void buttonValidate_Click(object sender, EventArgs e)
      {
         string connectionString = GetConnectionString("master");
         bool bThreadTestResult = false;
         AutoResetEvent enumServersEvent = new AutoResetEvent(false);

         Exception error = null;
         if (VerifyConnectionString)
         {
            Thread validateConnectionStringThread = new Thread(delegate()
                      {
                         try
                         {
                            bThreadTestResult = ThreadTest(connectionString);
                         }
                         catch (Exception exp)
                         {
                            error = exp;
                         }
                         finally
                         {
                            enumServersEvent.Set();
                         }

                      });

            validateConnectionStringThread.Start();
            EnumenratingSQLServersDialog enumDlg = new EnumenratingSQLServersDialog();
            enumDlg.WaitMessage = "Validating Connection String...";

            enumDlg.Show(this);
            enumServersEvent.WaitOne();
            enumDlg.Close();
            if (bThreadTestResult)
            {
                string sMsg = "Connection String is valid";
                MessageBoxIcon messageBoxIcon = MessageBoxIcon.None;

                if (SqlUtilities.IsSqlServer2012OrLater(_sqlServerVersion) && sqlServerConfiguration1.IsWindowsAuthentication())
                {
                    sMsg = "It is recommended to select the 'Use SQL Server Authentication' option.\n\n" +
                          "You are connecting to a version of SQL Server 2012 or later, where  NT_AUTHORITY\\System is no longer installed by default.  This can cause problems with the DICOM Server Components";
                    messageBoxIcon = MessageBoxIcon.Warning;
                }
               buttonOk.Enabled = true;
               _validated = true;
               MessageBox.Show(sMsg, "Connection String Validation", MessageBoxButtons.OK, messageBoxIcon  );
            }
            else
            {
               _validated = false;
               MessageBox.Show(_connectionErrorMessage, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
         }
      }
      
      private SqlDataSourceEnum _sqlDataSourceType = SqlDataSourceEnum.SqlServer;
            
      public SqlDataSourceEnum SqlDataSourceType
      {
         get
         {
            return _sqlDataSourceType;
         }
      }

      private void buttonOk_Click(object sender, EventArgs e)
      {
         if (radioButtonSqlServer.Checked)
         {
            _sqlDataSourceType = SqlDataSourceEnum.SqlServer;
         }
         else
         {
            _sqlDataSourceType = SqlDataSourceEnum.SqlServerCompact;
         }
      }
   }
   
      public enum SqlDataSourceEnum 
      {
         SqlServer = 0,
         SqlServerCompact = 1
      };
}
