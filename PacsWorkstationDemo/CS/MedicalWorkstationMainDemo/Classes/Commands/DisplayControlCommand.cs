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
   public class DisplayControlCommand : ICommand
   {
      public DisplayControlCommand ( Control container, Control topControl )
      {
         _container = container ;
         _topControl = topControl ;
         
         if ( !_container.Controls.Contains ( _topControl ) )
         {
            _topControl.Visible = false ;
            
            _container.Controls.Add ( _topControl ) ;
         }
      }
      
      public void Execute()
      {
         if ( !CanExecute ( ) )
         {
            return ;
         }
         
         OnDisplayControlExecuting ( this, new DisplayControlEventArgs ( _container, _topControl ) ) ;
         
         foreach ( Control displayedControl in _container.Controls ) 
         {
            if ( displayedControl.Visible )
            {
               displayedControl.Visible = false ;
               
               break ;
            }
         }
         
         //_container.Controls.Clear ( ) ;
         
         _topControl.Dock = DockStyle.Fill ;
         
         _container.BackColor = _topControl.BackColor ;
         
         _topControl.Visible = true ;
         
         if ( !_container.Controls.Contains ( _topControl ) ) 
         {
            _container.Controls.Add ( _topControl )  ;
         }
         
         OnDisplayControlExecuted ( this, new DisplayControlEventArgs ( _container, _topControl ) ) ;
      }
      
      public bool CanExecute ( ) 
      {
         return !( _container.Controls.Count == 1 && 
                  _container.Controls [ 0 ] == _topControl ) ;
      }

      public event EventHandler <DisplayControlEventArgs> DisplayControlExecuting ;
      public event EventHandler <DisplayControlEventArgs> DisplayControlExecuted ;
      
      protected virtual void OnDisplayControlExecuting ( object sender, DisplayControlEventArgs e ) 
      {
         if ( null != DisplayControlExecuting ) 
         {
            DisplayControlExecuting ( sender, e ) ;
         }
      }
      
      protected virtual void OnDisplayControlExecuted ( object sender, DisplayControlEventArgs e ) 
      {
         if ( null != DisplayControlExecuted ) 
         {
            DisplayControlExecuted ( sender, e ) ;
         }
      }
      
      private Control _container ;
      private Control _topControl ;
   }
   
   public class DisplayControlEventArgs : EventArgs
   {
      public DisplayControlEventArgs ( Control parentControl, Control displayControl ) 
      {
         ParentControl  = parentControl ;
         DisplayControl = displayControl ;
      }
      
      public Control ParentControl 
      {
         get
         {
            return _parentControl ;
         }
         
         private set
         {
            _parentControl = value ;
         } 
      }
      
      public Control DisplayControl 
      {
         get
         {
            return _displayControl ;
         }
         
         private set
         {
            _displayControl = value ;
         }
      }
      
      private Control _parentControl ;
      private Control _displayControl ;
   }
}
