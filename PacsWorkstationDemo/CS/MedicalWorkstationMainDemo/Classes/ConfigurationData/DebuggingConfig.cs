// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using Leadtools.Medical.Workstation;

namespace Leadtools.Demos.Workstation.Configuration
{
   public class DebuggingConfig
   {
      public bool Enable
      {
         get
         {
            return _enable ;
         }
         
         set
         {
            if ( value != _enable ) 
            {
               _enable = value ;
               
               OnValueChanged ( ) ;
               
               WorkstationMessager.DetailedError = Enable && DisplayDetailedErrors ;
            }
         }
      }
      
      
      public bool DisplayDetailedErrors
      {
         get
         {
            return _displayDetailedErrors ;
         }
         
         set
         {
            if ( value != _displayDetailedErrors )
            {
               _displayDetailedErrors = value ;
               
               OnValueChanged ( ) ;
               
               WorkstationMessager.DetailedError = Enable && DisplayDetailedErrors ;
            }
         }
      }
      
      public bool GenerateLogFile
      {
         get
         {
            return _generateLogFile ;
         }
         
         set
         {
            if ( value != _generateLogFile )
            {
               _generateLogFile = value ;
               
               OnValueChanged ( ) ;
            }
         }
      }
      
      public string LogFileName 
      {
         get
         {
            return _logFileName ;
         }
         
         set
         {
            if ( value != _logFileName )
            {
               _logFileName = value ;
               
               OnValueChanged ( ) ;
            }
         }
      }
      
      private void OnValueChanged ( ) 
      {
         if ( null != ValueChanged )
         {
            ValueChanged ( this, new EventArgs ( ) ) ;
         }
      }
      
      public event EventHandler ValueChanged ;
      
      private bool   _enable = false ;
      private bool   _displayDetailedErrors = false ;
      private bool   _generateLogFile = false ;
      private string _logFileName = string.Empty ;
   }
}
