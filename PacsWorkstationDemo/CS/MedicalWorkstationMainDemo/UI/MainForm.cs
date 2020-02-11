// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms ;
using System.Diagnostics ;
using System.Linq ;
using Leadtools.Medical.Winforms ;
using Leadtools.Medical.Workstation ;
using Leadtools.Medical.Workstation.UI ;
using Leadtools.Medical.Workstation.UI.Commands;
using Leadtools.Demos.Workstation.Configuration;
using Leadtools.DicomDemos;
using System.IO;
using Leadtools.Medical.Workstation.DataAccessLayer;
using Leadtools.Dicom.Scu.Common;
using Leadtools.Medical.DataAccessLayer;
using Leadtools.Medical.Workstation.DataAccessLayer.Configuration;
using System.Net;
using Leadtools.Dicom;


namespace Leadtools.Demos.Workstation
{
   public partial class MainForm: Form
   {
      public MainForm()
      {
         InitializeComponent ( ) ;

         this.Load += new EventHandler(MainForm_Load);
      }

      void MainForm_Load ( object sender, EventArgs e )
      {
         this.Activate ( ) ;
      }

      public override object InitializeLifetimeService()
      {
         return null ;
      }
      
      public WorkStationContainer WorkStationContainerControl
      {
         get 
         {
            return workStationContainerControl ;
         }
      }

      private void workStationContainerControl_Load(object sender, EventArgs e)
      {

      }
   }
}
