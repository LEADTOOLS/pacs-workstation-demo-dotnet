// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Leadtools.Demos.Workstation
{
   interface IWorkstationContainer
   {
      void SetHelpNamescpace   ( string helpNamespace ) ;
      void OnFullScreenChanged ( bool isFullScreen ) ;
      
      Control DisplayContainer
      {
         get ;
      }
      
      bool CanSearch
      {
         set ;
      }
      
      bool CanViewImages
      {
         set ;
      }
      
      bool CanConfigure
      {
         set ;
      }
      
      bool CanManageUsers
      {
         set ;
      }
      
      bool CanManageService
      {
         set ;
      }
      
      bool CanCreateMedia
      {
         set ;
      }
      
      bool CanViewQueueManager
      {
         set ;
      }
      
      bool CanDisplayHelp
      {
         set ;
      }
      
      bool CanToggleFullScreen
      {
         set ;
      }
      
      event EventHandler Load ;
      
      event EventHandler DoSearch ;
      
      event EventHandler DoDisplayViewer ;
      
      event EventHandler DoConfigure ;
      
      event EventHandler DoManageUsers ;
      
      event EventHandler DoManageService ;
      
      event EventHandler DoCreateMedia ;
      
      event EventHandler DoViewQueueManager ;
      
      event EventHandler DoDisplayHelp ;
      
      event EventHandler DoToggleFullScreen ;
      
      event EventHandler ExitFullScreen ;

      void OnDisplayedControlChanged ( string uiElement ) ;
   }
}
