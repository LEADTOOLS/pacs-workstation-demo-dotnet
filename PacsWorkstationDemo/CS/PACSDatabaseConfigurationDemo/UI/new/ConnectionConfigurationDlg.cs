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
using Leadtools.Demos;

namespace CSPacsDatabaseConfigurationDemo.UI.New
{
   public partial class ConnectionConfigurationDlg : Form
   {
      public ConnectionConfigurationDlg()
      {
         InitializeComponent();

         OKButton.Click += new EventHandler(OKButton_Click);
      }

      public string Title
      {
         get
         {
            return HeaderDescriptionLabel.Text ;
         }
         
         set
         {
            HeaderDescriptionLabel.Text = value ;
         }
      }

      void OKButton_Click(object sender, EventArgs e)
      {
         string connectionErrorMessage ;
         List <string> databaseLocations = new List<string> ( ) ;
         
         if (!connectionConfigurationControl.IsConnectionStringValid(out connectionErrorMessage, databaseLocations))
         {
            DialogResult = DialogResult.None ;
            
            if ( !string.IsNullOrEmpty ( connectionErrorMessage ) )
            {
               Messager.ShowError ( this, connectionErrorMessage ) ;
            }
            
            return ;
         }
      }
   }
}
