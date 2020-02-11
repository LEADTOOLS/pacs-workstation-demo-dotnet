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

namespace MedicalWorkstationConfigurationDemo.UI
{
   public partial class EnumenratingSQLServersDialog : Form
   {
      public EnumenratingSQLServersDialog()
      {
         InitializeComponent();
         
         this.Text = ConfigurationData.ApplicationName ;
      }
      
      public string WaitMessage
      {
         get
         {
            return label1.Text;
         }
         
         set
         {
            label1.Text = value;
         }
      }
   }
}
