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
using System.Windows.Forms;
using System.IO;

namespace Leadtools.Demos.Workstation
{
   public partial class WorkstationSplashScreen : Form
   {
      public WorkstationSplashScreen()
      {
         InitializeComponent();
         
         BackgroundImage = GetSplashScreenImage ( ) ;
      }

      private Image GetSplashScreenImage ( )
      {
         string splashScreenPath = Application.StartupPath ;
         
         
         splashScreenPath = Path.Combine ( splashScreenPath, "splash.png" ) ;
         
         if ( File.Exists ( splashScreenPath ) )
         {
            try
            {
               return Image.FromFile ( splashScreenPath ) ;
            }
            catch ( Exception ) 
            {
               return global::Leadtools.Demos.Workstation.Properties.Resources.SplashScreen_MedWorkViewer ;
            }
         }
         else
         {
            return global::Leadtools.Demos.Workstation.Properties.Resources.SplashScreen_MedWorkViewer ;
         }
      }
   }
}
