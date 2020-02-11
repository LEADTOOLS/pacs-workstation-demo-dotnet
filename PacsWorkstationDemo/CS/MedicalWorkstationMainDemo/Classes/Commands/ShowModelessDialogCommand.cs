// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System ;
using System.Collections.Generic ;
using System.Text ;
using System.Windows.Forms ;
using Leadtools.Medical.Workstation.Commands;


namespace Leadtools.Demos.Workstation
{
   public class ShowModelessDialogCommand : ICommand
   {
      private IWin32Window _parent ;
      private Form         _dialog ;
      
      public ShowModelessDialogCommand ( IWin32Window parent, Form dialog )
      {
         _parent = parent ;
         _dialog = dialog ;
      }
      
      public virtual void Execute ( ) 
      {
         if ( _dialog.Visible ) 
         {
            _dialog.Focus ( ) ;
         }
         else
         {
            _dialog.Show ( _parent ) ;
         }         
      }
      
      public virtual bool CanExecute ( ) 
      {
         return ( _dialog != null ) ;
      }
   }
}
