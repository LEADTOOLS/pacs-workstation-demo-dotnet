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
   public partial class ConnectionSummary : UserControl
   {
      public ConnectionSummary ( )
      {
         InitializeComponent ( ) ;
      }
      
      public string Summary
      {
         get
         {
            return SummaryRichTextBox.Text ;
         }
         
         set
         {
            SummaryRichTextBox.Text = value ;
         }
      }
   }
}
