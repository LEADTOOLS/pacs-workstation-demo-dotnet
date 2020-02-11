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
   class ThreadSafeMessager
   {
      private delegate void ShowMessageHandler          ( string message ) ;
      private delegate DialogResult ShowQuestionHandler ( string message, MessageBoxButtons buttons ) ;
      
      public static Control Owner
      {
         get ;
         set ;
      }
      
      public static void ShowError ( string message ) 
      {
         if ( Owner.InvokeRequired )
         {
            ShowMessageHandler showMesage ;
            
            
            showMesage = new ShowMessageHandler ( ShowError ) ;
            
            Owner.Invoke ( showMesage, new object [ ] { message } ) ;
         }
         else
         {
            Messager.ShowError ( Owner, message ) ;
         }
      }
      
      public static void ShowWarning ( string message ) 
      {
         if ( Owner.InvokeRequired )
         {
            ShowMessageHandler showMesage ;
            
            
            showMesage = new ShowMessageHandler ( ShowWarning ) ;
            
            Owner.Invoke ( showMesage, new object [ ] { message } ) ;
         }
         else
         {
            Messager.ShowWarning ( Owner, message ) ;
         }
      }
      
      public static void ShowInformation ( string message ) 
      {
         if ( Owner.InvokeRequired )
         {
            ShowMessageHandler showMesage ;
            
            
            showMesage = new ShowMessageHandler ( ShowInformation ) ;
            
            Owner.Invoke ( showMesage, new object [ ] { message } ) ;
         }
         else
         {
            Messager.ShowInformation ( Owner, message ) ;
         }
      }
      
      public static DialogResult ShowQuestion ( string message, MessageBoxButtons buttons ) 
      {
         if ( Owner.InvokeRequired )
         {
            ShowQuestionHandler showMesage ;
            
            
            showMesage = new ShowQuestionHandler ( ShowQuestion ) ;
            
            return (DialogResult)Owner.Invoke ( showMesage, new object [ ] { message } ) ;
         }
         else
         {
            return Messager.ShowQuestion ( Owner, message, buttons  ) ;
         }
      }
   }
}
