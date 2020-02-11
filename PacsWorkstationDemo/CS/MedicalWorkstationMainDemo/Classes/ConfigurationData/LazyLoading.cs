// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Collections.Generic;
using System.Text;

namespace Leadtools.Demos.Workstation.Configuration
{
   public class LazyLoading
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
            }
         }
      }
      
      
      public int HiddenImages
      {
         get
         {
            return _hiddenImages ;
         }
         
         set
         {
            if ( value != _hiddenImages )
            {
               _hiddenImages = value ;
               
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
      
      private bool _enable = false ;
      private int  _hiddenImages = 0 ;
   }
}
