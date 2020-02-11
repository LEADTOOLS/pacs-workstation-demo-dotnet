// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Data;
using System.Data.SqlClient ;
using System.Linq;
using System.Text ;
using System.Windows.Forms ;
using Leadtools.Demos;
using System.Threading;
using System.Collections.Specialized;
using Leadtools.Demos.Sql;
using System.Runtime.InteropServices;

namespace MedicalWorkstationConfigurationDemo.UI
{
   partial class SqlServerConfiguration : UserControl
   {
      #region Public
         
         #region Methods
            
            public SqlServerConfiguration()
            {
               InitializeComponent ( ) ;
               
               Init ( ) ;
               
               RegisterEvents ( ) ;
            }
            
            public bool IsWindowsAuthentication()
            {
                return windowsAuthenticationRadioButton.Checked;
            }
            
            public void SetComponentFocus ( ConnectionStringCommponents component ) 
            {
               switch ( component )
               {
                  case ConnectionStringCommponents.Database:
                  {
                     serverDatabaseComboBox.Focus ( ) ;
                     
                     return ;
                  }
                  
                  case ConnectionStringCommponents.Integrated:
                  {
                     windowsAuthenticationRadioButton.Focus ( ) ;
                     
                     return ;
                  }
                  
                  case ConnectionStringCommponents.Password:
                  {
                     passwordTextBox.Focus ( ) ;
                     
                     return ;
                  }
                  
                  case ConnectionStringCommponents.Server:
                  {
                     serverDatabaseComboBox.Focus ( ) ;
                     
                     return ;
                  }
                  
                  case ConnectionStringCommponents.UserName:
                  {
                     userNameTextBox.Focus ( ) ;
                     
                     return ;
                  }
                  
                  default:
                  {
                     serverDatabaseComboBox.Focus ( ) ;
                     
                     return ;
                  }
               }
            }
            
         #endregion
         
         #region Properties
         
            public string DefaultDatabaseName
            {
               get
               {
                  return _defaultDatabaseName ;
               }
               
               set
               {
                  _defaultDatabaseName = value ;
                  
                  if ( serverDatabaseComboBox.Text.Length == 0 )
                  {
                     serverDatabaseComboBox.Text = value ;
                  }
               }
            }
         
            public DbConfigurationMode Mode
            {
               get
               {
                  return _configMode ;
               }
               
               set
               {
                  _configMode = value ;
                  
                  if ( _configMode == DbConfigurationMode.Configure )
                  {
                     serverDatabaseComboBox.DropDownStyle = ComboBoxStyle.DropDownList ;
                  }
                  else
                  {
                     serverDatabaseComboBox.DropDownStyle = ComboBoxStyle.Simple ;
                  }
               }
            }
            
            public string DataSource
            {
               get
               {
                  return serverNameCoboBox.Text;
               }
            }
            
            
            public string ConnectionString
            {
               get
               {
                  SqlConnectionStringBuilder connectionBuilder ;
                  
                  
                  connectionBuilder = new SqlConnectionStringBuilder ( ) ;
                  
                  connectionBuilder.DataSource = serverNameCoboBox.Text ;
                  connectionBuilder.IntegratedSecurity = windowsAuthenticationRadioButton.Checked ;
                  connectionBuilder.UserID             = userNameTextBox.Text ;
                  connectionBuilder.Password           = passwordTextBox.Text ;
                  connectionBuilder.InitialCatalog     = serverDatabaseComboBox.Text ;
                  connectionBuilder.Pooling            = true ;                  
                  connectionBuilder.FailoverPartner    = textBoxPartner.Text;

                  return connectionBuilder.ConnectionString ;
               }
               
               set
               {
                  SqlConnectionStringBuilder connectionBuilder ;
                  
                  
                  if ( string.IsNullOrEmpty ( value ) )
                  {
                     ClearConnectionSettings ( ) ;
                  }
                  else
                  {
                     connectionBuilder = new SqlConnectionStringBuilder ( value ) ;
                     
                     if ( string.IsNullOrEmpty ( connectionBuilder.DataSource ) )
                     {
                        ClearConnectionSettings ( )  ;
                     }
                     else
                     {
                        serverNameCoboBox.Text                   = connectionBuilder.DataSource ;
                        windowsAuthenticationRadioButton.Checked = connectionBuilder.IntegratedSecurity ;
                        userNameTextBox.Text                     = connectionBuilder.UserID ;
                        passwordTextBox.Text                     = connectionBuilder.Password ;                        
                        textBoxPartner.Text                      = connectionBuilder.FailoverPartner;
                        
                        if ( Mode == DbConfigurationMode.Configure )
                        {
                           try
                           {
                              if (Program.ShouldEnumerateSqlServers)
                              {
                                 serverDatabaseComboBox.DataSource = SqlUtilities.GetDatabaseList(connectionBuilder);
                                 serverDatabaseComboBox.SelectedItem = connectionBuilder.InitialCatalog;
                              }
                           }
                     catch {}
                        }
                        else
                        {
                           serverDatabaseComboBox.Text = connectionBuilder.InitialCatalog ;
                        }
                        
                        sqlAuthenticationRadioButton.Checked = !windowsAuthenticationRadioButton.Checked ;
                     }
                  }
               }
            }

            private void ClearConnectionSettings()
            {
               userNameTextBox.Text                     = string.Empty ;
               passwordTextBox.Text                     = string.Empty ;
               serverDatabaseComboBox.Text              = DefaultDatabaseName ;
               windowsAuthenticationRadioButton.Checked = true ;
               textBoxPartner.Text                      = string.Empty;               
            }
            
            public string GetDataSource()
            {
               return serverNameCoboBox.Text;
            }
            
         #endregion
         
         #region Events
            
         #endregion
         
         #region Data Types Definition
         
            public enum ConnectionStringCommponents
            {
               Server,
               Database,
               Integrated,
               UserName,
               Password
            }
            
         #endregion
         
         #region Callbacks
            
         #endregion
         
      #endregion
      
      #region Protected
         
         #region Methods
         
            private void Init ( ) 
            {
               userInfoPanel.DataBindings.Add ( new Binding ( "Enabled", sqlAuthenticationRadioButton, "Checked" ) ) ;
               Mode = DbConfigurationMode.Configure ;               
            }
               
            private void RegisterEvents ( ) 
            {
               this.Load                       += new EventHandler(SqlServerConfiguration_Load);
               refreshServersButton.Click      += new EventHandler(SqlServerConfiguration_Load);
               serverDatabaseComboBox.DropDown += new EventHandler(serverNameCoboBox_DropDown);
               serverNameCoboBox.SelectionChangeCommitted += new EventHandler(serverNameCoboBox_SelectionChangeCommitted);
               windowsAuthenticationRadioButton.CheckedChanged += new EventHandler(windowsAuthenticationRadioButton_CheckedChanged);
               userNameTextBox.TextChanged += new EventHandler(userNameTextBox_TextChanged);
               passwordTextBox.TextChanged += new EventHandler(passwordTextBox_TextChanged);
            }

            void passwordTextBox_TextChanged(object sender, EventArgs e)
            {
               OnSettingsChanged(sender, e);
            }

            void userNameTextBox_TextChanged(object sender, EventArgs e)
            {
               OnSettingsChanged(sender, e);
            }

            void windowsAuthenticationRadioButton_CheckedChanged(object sender, EventArgs e)
            {
               OnSettingsChanged(sender, e);
            }

            void serverNameCoboBox_SelectionChangeCommitted(object sender, EventArgs e)
            {
               OnSettingsChanged(sender, e);
            }

         #endregion
         
         #region Properties
         
         private static StringCollection servers = null ;

            
         #endregion
         
         #region Events
         
            void SqlServerConfiguration_Load ( object sender, EventArgs e )
            {
               refreshServersButton.Visible = Program.ShouldEnumerateSqlServers;
               labelDataSource.Visible = !Program.ShouldEnumerateSqlServers;
               this.Cursor = Cursors.WaitCursor ;
                              
               string selectedServer = serverNameCoboBox.Text ;
               
               try
               {
                  if (Program.ShouldEnumerateSqlServers)
                  {
                     if (!_DesignMode)
                     {
                        AutoResetEvent enumServersEvent = new AutoResetEvent(false);
                        Exception error = null;
                        Thread getServersThread;

                        getServersThread = new Thread(delegate()
                        {
                           try
                           {
                              servers = SqlUtilities.GetServerList();
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

                        if (servers == null)
                        {
                           getServersThread.Start();


                           EnumenratingSQLServersDialog enumDlg = new EnumenratingSQLServersDialog();

                           enumDlg.Show(this);

                           enumServersEvent.WaitOne();

                           enumDlg.Close();
                        }

                        if (null != error)
                        {
                           throw error;
                        }
                        else
                        {
                           string[] localServers = SqlUtilities.GetLocalSQLServerInstances();

                           if (servers.Count > 0)
                           {
                              serverNameCoboBox.DataSource = servers;

                              if (localServers!=null && localServers.Length > 0)
                              {
                                 serverNameCoboBox.Text = localServers[0];
                              }
                           }
                           else
                           {
                              serverNameCoboBox.DataSource = localServers;
                           }
                        }
                     }
                  }
               }
               catch ( Exception ex)
               {
                  System.Diagnostics.Debug.WriteLine(ex.Message);

                  serverNameCoboBox.DataSource = SqlUtilities.GetLocalSQLServerInstances ( ) ;
                  
                  Messager.ShowWarning ( this, 
                                         "The application can't enumerate the SQL Servers located in your network because \"Microsoft SQL Server 2008 Management Objects (SMO)\" is not installed.\n" + 
                                         "You can manually type the SQL Server name in the server name field or install Microsoft SMO from Microsoft site:\n" + 
                                         "http://www.microsoft.com/downloadS/details.aspx?familyid=C6C3E9EF-BA29-4A43-8D69-A2BED18FE73C&displaylang=en" ) ;
                                         
                  return ;
               }
               finally
               {
                  if ( !string.IsNullOrEmpty ( selectedServer ) )
                  {
                     serverNameCoboBox.Text = selectedServer ;
                  }
                  
                  this.Cursor = Cursors.Default ;
               }
            }
            
            private void PopulateDropDown()
            {
               try
               {
                  this.Cursor = Cursors.WaitCursor ;
                  
                  SqlConnectionStringBuilder connectionBuilder ;
                  
                  
                  connectionBuilder = new SqlConnectionStringBuilder ( ) ;
                  
                  connectionBuilder.DataSource = serverNameCoboBox.Text ;
                  connectionBuilder.IntegratedSecurity = windowsAuthenticationRadioButton.Checked ;
                  connectionBuilder.UserID             = userNameTextBox.Text ;
                  connectionBuilder.Password           = passwordTextBox.Text ;
                  
                  serverDatabaseComboBox.DataSource = SqlUtilities.GetDatabaseList ( connectionBuilder ) ;
               }
               catch ( Exception exp )
               {
                  serverDatabaseComboBox.DataSource = null ;
                     
                  serverDatabaseComboBox.Items.Clear ( ) ;

                  Messager.ShowError ( this, exp ) ;
                                    
                  System.Diagnostics.Debug.Assert ( false ) ;
               }
               finally
               {
                  this.Cursor = Cursors.Default ;
               }
            }
            
            private void serverNameCoboBox_DropDown ( object sender, EventArgs e )
            {
               PopulateDropDown();
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
         
         #region Data Members
            public bool DatabaseVisible
            {
               get
               {
                  return serverDatabaseComboBox.Visible;
               }
               
               set
               {
                  labelDatabase.Visible = value;
                  serverDatabaseComboBox.Visible = value;
               }
            }
            
            public bool SqlMirroringVisible
{
               get
               {
                  return groupBoxSqlMirroring.Visible;
               }
               
               set
               {
                  groupBoxSqlMirroring.Visible = value;
               }
            }
            
         #endregion
         
         #region Data Types Definition
            
         #endregion
         
      #endregion
      
      #region Private
         
         #region Methods
            
         #endregion
         
         #region Properties
         
            private bool _DesignMode
            {
               get
               {
                  return (this.GetService(typeof(IDesignerHost)) != null) || 
                         (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime);
               }
            }
            
         #endregion
         
         #region Events
            
         #endregion
         
         #region Data Members
         
            private DbConfigurationMode _configMode ;
            private string              _defaultDatabaseName ;
            
         #endregion           
         
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
