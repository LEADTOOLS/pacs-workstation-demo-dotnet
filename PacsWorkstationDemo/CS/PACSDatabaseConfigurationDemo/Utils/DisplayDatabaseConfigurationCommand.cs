// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalWorkstationConfigurationDemo.UI;

namespace MedicalWorkstationConfigurationDemo
{
   delegate DatabaseComponents SelectedComponentsDelegate ( ) ;
   
   class DisplayComponentConfigurationCommand
   {
      public DisplayComponentConfigurationCommand 
      ( 
         SelectedComponentsDelegate selectedComponents,
         DatabaseComponents component,
         ConnectionConfiguration configControl 
      ) 
      {
         SelectedComponents = selectedComponents ;
         Database           = component ;
         ConfigControl      = configControl ;
      }
      
      public void Execute ( ) 
      {
         OnExecuting ( ) ;
         
         ConfigControl.Visible = true ;
         
         ConfigControl.BringToFront ( ) ;
         
         OnExecuted ( ) ;
      }
      
      public bool CanExecute ( ) 
      {
         DatabaseComponents selected ;
         
         
         selected = SelectedComponents ( ) ;
         
         return ( selected & Database ) == Database ;
      }
      
      public SelectedComponentsDelegate SelectedComponents
      {
         get
         {
            return _selectedComponents ;
         }
         
         set
         {
            _selectedComponents = value ;
         }
      }
      
      public DatabaseComponents Database
      {
         get
         {
            return _database ;
         }
         
         set
         {
            _database = value ;
         }
      }
      
      public ConnectionConfiguration ConfigControl
      {
         get
         {
            return _configControl ;
         }
         
         set
         {
            _configControl = value ;
         }
      }
      
      public event EventHandler Executing ;
      public event EventHandler Executed ;
      
      private void OnExecuting ( ) 
      {
         if ( null != Executing ) 
         {
            Executing ( this, EventArgs.Empty ) ;
         }
      }
      
      private void OnExecuted ( ) 
      {
         if ( null != Executed ) 
         {
            Executed ( this, EventArgs.Empty ) ;
         }
      }
      
      private SelectedComponentsDelegate _selectedComponents ;
      private DatabaseComponents         _database ;
      private ConnectionConfiguration    _configControl ;
   }
}
