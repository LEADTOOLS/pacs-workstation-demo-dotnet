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

namespace Leadtools.Demos.Workstation
{
   public partial class EventLogViewerDialog : Form
   {
      public EventLogViewerDialog ( )
      {
         InitializeComponent();
         
         Icon = WorkstationUtils.GetApplicationIcon ( ) ;
      }
   }
}
