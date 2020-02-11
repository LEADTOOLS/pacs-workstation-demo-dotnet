// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.IO ;
using System.Windows.Forms;
using Leadtools.Demos;

namespace MedicalWorkstationConfigurationDemo.UI
{
   partial class SqlCeConfiguration : UserControl
   {
      #region Public
         
         #region Methods
         
            public SqlCeConfiguration()
            {
               InitializeComponent ( ) ;
               
               RegisterEvents ( );
            }
            
         #endregion
         
         #region Properties
         
            public string DefaultSqlCeDatabaseName
            {
               get
               {
                  return _defaultSqlCeDatabaseName ;
               }
               
               set
               {
                  _defaultSqlCeDatabaseName = value ;
                  
                  if ( databaseTextBox.Text.Length == 0 )
                  {
                     databaseTextBox.Text = value ;
                  }
               }
            }
         
            public string ConnectionString
            {
               get 
               {
                  if ( databaseTextBox.Text.Length > 0 )
                  {
                     return string.Format ( "Data Source={0}", databaseTextBox.Text ) ;
                  }
                  else
                  {
                     return string.Empty ;
                  }
               }
               
               set
               {
                  databaseTextBox.Text = value.Replace ( "Data Source=", "" ) ;
                  
                  if ( databaseTextBox.Text.Length == 0 )
                  {
                     databaseTextBox.Text = DefaultSqlCeDatabaseName ;
                  }
               }
            }
            
            public string DatabaseLocation
            {
               get
               {
                  return databaseTextBox.Text ; 
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
         
            private void RegisterEvents ( )
            {
               browseDbButton.Click += new EventHandler ( browseDbButton_Click ) ;
            }
            
            private bool IsUncPath ( string path ) 
            {
               return ( path.StartsWith ( @"\\" ) ) ;
            }

         #endregion
         
         #region Properties
            
         #endregion
         
         #region Events
         
            void browseDbButton_Click ( object sender, EventArgs e )
            {
               FileDialog databaseFileDlg ;
               
               
               if ( Mode == DbConfigurationMode.Configure ) 
               {
                  databaseFileDlg = openFileDialog1 ;
                  
                  //openFileDialog1.CheckFileExists = true ;
                  //openFileDialog1.CheckPathExists = true ;
               }
               else
               {
                  databaseFileDlg = saveFileDialog1 ;
                  //openFileDialog1.CheckFileExists = false ;
                  //openFileDialog1.CheckPathExists = false ;
               }
               
               databaseFileDlg.FileName = databaseTextBox.Text ;
               
               if ( databaseTextBox.Text.Length > 0 )
               {
                  databaseFileDlg.InitialDirectory = Path.GetDirectoryName ( databaseTextBox.Text ) ;
               }
               
               
               if ( databaseFileDlg.ShowDialog ( ) == DialogResult.OK ) 
               {
                  if ( IsUncPath ( databaseFileDlg.FileName ) )
                  {
                     Messager.ShowError ( this, "You can't connect to SQL Server CE database located on remote machine. Use a local machine database or choose another database source option." ) ;
                     
                     return ;
                  }
                  
                  databaseTextBox.Text = databaseFileDlg.FileName ;
               }
            }
            
         #endregion
         
         #region Data Members
         
            private DbConfigurationMode _configMode ;
            private string              _defaultSqlCeDatabaseName ;
            
         #endregion
         
         #region Data Types Definition
            
         #endregion
         
      #endregion
      
      #region internal
         
         #region Methods
            
         #endregion
         
         #region Properties
            public string DataSource
            {
               get
               {
                  return "SQL Server Compact";
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

   }
}
