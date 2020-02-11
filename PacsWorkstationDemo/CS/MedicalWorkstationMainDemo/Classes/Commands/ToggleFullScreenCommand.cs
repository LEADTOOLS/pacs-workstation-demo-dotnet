// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System ;
using System.Collections.Generic ;
using System.Text ;
using System.Windows.Forms ;
using Leadtools.Medical.Workstation.UI;
using Leadtools.Medical.Workstation.Commands;


namespace Leadtools.Demos.Workstation
{
   class ToggleFullScreenCommand : ICommand
   {
      public ToggleFullScreenCommand ( Control container )
      {
         _container  = container ;
         _parentForm = container.FindForm ( ) ;
      }
      
      public void Execute ( )
      {
         try
         {
            if ( !CanExecute ( ) )
            {
               return ;
            }
            
            if ( _parentForm.FormBorderStyle == FormBorderStyle.None ) 
            {
               _parentForm.FormBorderStyle = FormBorderStyle.Sizable ;
               
               AutoHidePanel.ExpandTopLevelPanels ( ) ;
            }
            else
            {
               _parentForm.SuspendLayout ( ) ;
               _parentForm.FormBorderStyle = FormBorderStyle.None ;
               
               _parentForm.WindowState = FormWindowState.Normal ;
               _parentForm.WindowState = FormWindowState.Maximized ;
               
               AutoHidePanel.CollapseTopLevelPanels ( ) ;
               
               _parentForm.ResumeLayout ( ) ;

            }
            
            OnCommandExecuted ( ) ;
         }
         catch ( Exception exception )
         {
            System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                           
            throw ;
         }
      }

      private void OnCommandExecuted()
      {
         if ( null != CommandExecuted ) 
         {
            CommandExecuted ( this, EventArgs.Empty ) ;
         }
      }
      
      public event EventHandler CommandExecuted ;
      
      public bool CanExecute ( ) 
      {
         return _parentForm != null ;
      }
      
      public bool FullScreen 
      {
         get
         {
            if ( _parentForm == null ) 
            {
               return false ;
            }
            
            if ( _parentForm.FormBorderStyle == FormBorderStyle.None ) 
            {
               return true ;
            }
            else
            {
               return false ;
            }
         }
      }

      private Control _container ;
      private Form    _parentForm ;
   }
}
