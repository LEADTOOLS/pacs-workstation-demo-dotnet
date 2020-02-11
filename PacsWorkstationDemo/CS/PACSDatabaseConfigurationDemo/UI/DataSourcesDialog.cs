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
using System.Windows.Forms;

namespace MedicalWorkstationConfigurationDemo.UI
{
   public partial class DataSourcesDialog : Form
   {
      public DataSourcesDialog()
      {
         InitializeComponent();
         
         Init ( ) ;
      }
      
      private void Init ( )
      {
         DataTable dataSources ;
         SupportedProviders providers ;
         
         dataSources = new DataTable ( ) ;
         
         dataSources.Columns.Add ( "SourceName", typeof ( string ) ) ;
         dataSources.Columns.Add ( "SourceValue", typeof ( string ) ) ;
         dataSources.Columns.Add ( "Description", typeof ( string ) ) ;
         
         providers = ConfigurationData.GetSupportedProvider ( ) ;
         
         if ( ( providers & SupportedProviders.SqlClient ) == SupportedProviders.SqlClient )
         {
            dataSources.Rows.Add ( new object [ ] { "Microsoft SQL Server", 
                                                    ConnectionProviders.SqlServerProvider.Name,
                                                    "Use this selection to connect to Microsoft SQL Server 2000 or above using the .NET Framework Data Provider for SQL Server." } ) ;
         }
          
         if ( ( providers & SupportedProviders.SqlServerCe ) == SupportedProviders.SqlServerCe )
         {
            dataSources.Rows.Add ( new object [ ] { "Microsoft SQL Server Compact 3.5", 
                                                    ConnectionProviders.SqlCeProvider.Name,
                                                    "Use this selection to connect to Microsoft SQL Compact 3.5 database file." } ) ;
         }
      
         dataSourceListBox.DataSource    = dataSources ;
         dataSourceListBox.DisplayMember = "SourceName" ;
         dataSourceListBox.ValueMember   = "SourceValue" ;
         
         descriptionLabel.DataBindings.Add ( new Binding ( "Text", dataSources, "Description" ) ) ;
         
         ConnectionProviders[] connectionProviders ;
         
         
         connectionProviders = ConnectionProviders.FromProvider ( providers ) ;
         
         if ( connectionProviders != null && connectionProviders.Length > 0 ) 
         {
            if ( connectionProviders.Contains ( ConnectionProviders.SqlCeProvider ) )
            {
               dataSourceListBox.SelectedValue = ConnectionProviders.SqlCeProvider.Name ;
            }
            else if ( connectionProviders.Contains ( ConnectionProviders.SqlServerProvider ) )
            {
               dataSourceListBox.SelectedValue = ConnectionProviders.SqlServerProvider.Name ;
            }
         }
      }
      
      public string SelectedProvider
      {
         get
         {
            if ( null != dataSourceListBox.SelectedValue ) 
            {
               return dataSourceListBox.SelectedValue.ToString ( ) ;
            }
            else
            {
               return null ;
            }
         }
         
         set
         {
            if ( null != dataSourceListBox.SelectedValue ) 
            {
               dataSourceListBox.SelectedValue = value ;
            }
         }
      }
      
      public string SelectedSource
      {
         get
         {
            if ( null != dataSourceListBox.SelectedItem ) 
            {
               return ( ( DataRowView ) dataSourceListBox.SelectedItem ) [ "SourceName" ].ToString ( ) ;
            }
            else
            {
               return null ;
            }
         }
         
         set
         {
            if ( null != dataSourceListBox.SelectedItem ) 
            {
               dataSourceListBox.SelectedItem = value ;
            }
         }
      }
   }
}
