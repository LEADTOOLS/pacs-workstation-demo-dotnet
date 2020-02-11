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
using System.Diagnostics;
using Leadtools.Demos.Workstation.Configuration;

namespace Leadtools.Demos.Workstation
{
   public partial class ImagesDownloadDialog : Form
   {
      public ImagesDownloadDialog()
      {
         InitializeComponent();
         
         this.Text = ConfigurationData.ApplicationName ;
      }

      private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
      {
         Process.Start ( @"ftp://ftp.leadtools.com/pub/3d/" ) ;
      }
   }
}
