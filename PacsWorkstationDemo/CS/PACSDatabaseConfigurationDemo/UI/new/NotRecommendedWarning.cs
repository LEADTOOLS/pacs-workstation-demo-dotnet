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
using System.Reflection;

namespace CSPacsDatabaseConfigurationDemo.UI.New
{
   public partial class NotRecommendedWarning : UserControl
   {
      public NotRecommendedWarning()
      {
         InitializeComponent();
      }
      
      public void Initialize()
      {
         System.IO.Stream imageStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("CSPacsDatabaseConfigurationDemo.Resources.warning.png");
         if (imageStream != null)
         {
            pictureBoxWarning.Image = Image.FromStream(imageStream);
         }
         textBoxNotRecommended.Text = "The 'SQL Server Compact 3.5' option is NOT recommended.\r\n\r\n" +
         "* LEAD HTML5 Medical Viewer demo cannot be used with this option\r\n" + 
         "* Performance of the data access is much slower.\r\n\r\n" +
         "It is recommended to use the 'SQL Server' option instead.";
      }
   }
}
