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
using Leadtools.Dicom.Common.DataTypes;
using Leadtools.Demos.Workstation.Configuration;

namespace Leadtools.Demos.Workstation
{
   partial class PacsMediaInformationView : UserControl, IPacsMediaInformationView
   {
      #region Public
         
         #region Methods
         
            public PacsMediaInformationView ( )
            {
               InitializeComponent ( ) ;
               
               _mediaTitleErrorProv = new ErrorProvider ( this ) ;
            }
            
            public void Initialize ( MediaType[] mediaTypes, RequestPriority[] priorities ) 
            {
               MediaTypeComboBox.DataSource = mediaTypes ;
               PriorityComboBox.DataSource  = priorities;
               IncludeDisplayApplication    = true ;
               
               VerifyServerButton.Click                  += new EventHandler ( VerifyServerButton_Click ) ;
               SendMediaRequestButton.Click              += new EventHandler ( SendMediaRequestButton_Click ) ;
               MediaTitleTextBox.TextChanged             += new EventHandler ( MediaTitleTextBox_TextChanged ) ;
               ServerAEComboBox.SelectionChangeCommitted += new EventHandler ( ServerAEComboBox_SelectionChangeCommitted ) ;
            }

            public void SetPacsServers ( ScpInfo[] servers )
            {
               ServerAEComboBox.DataSource = servers ;
            }
            
            public void ReportMediaCreationFailure ( string error ) 
            {
               ThreadSafeMessager.ShowError (  error ) ;
            }
            
            public void ReportMediaCreationSuccess ( ) 
            {
               ThreadSafeMessager.ShowInformation (  "Media request sent successfully." ) ;
            }
            
            public void ReportServerVerificationSuccess ( ) 
            {
               ThreadSafeMessager.ShowInformation (  "Connection succeeded" ) ;
            }
            public void ReportServerVerificationFailure ( string errorMessage ) 
            {
               ThreadSafeMessager.ShowWarning (  errorMessage ) ;
            }
            
            public void MediaTitleValidationError ( string errorText )
            {
               _mediaTitleErrorProv.SetError ( MediaTitleTextBox, errorText ) ;
            }
            
         
         #endregion
         
         #region Properties
         
            public string MediaTitle
            {
               get 
               {
                  return MediaTitleTextBox.Text ;
               }
               
               set 
               {
                  MediaTitleTextBox.Text = value ;
               }
            }
            
            public string LabelText
            {
               get 
               {
                  return LabelTextTextBox.Text ;
               }
               
               set 
               {
                  LabelTextTextBox.Text = value ;
               }
            }
            
            public int NumberOfCopies
            {
               get 
               {
                  return (int) NumberOfCopiesNumericUpDown.Value ;
               }
               
               set 
               {
                  NumberOfCopiesNumericUpDown.Value = value ;
               }
            }
            
            public RequestPriority Prioirty
            {
               get 
               {
                  if ( null == PriorityComboBox.SelectedItem ) 
                  {
                     return RequestPriority.Undefined ;
                  }
                  
                  return (RequestPriority) PriorityComboBox.SelectedItem ;
               }
               
               set 
               {
                  PriorityComboBox.SelectedItem = value ;
               }
            }
            
            public MediaType MediaType
            {
               get 
               {
                  if ( null == MediaTypeComboBox.SelectedItem ) 
                  {
                     return MediaType.Default ;
                  }

                  return (MediaType) MediaTypeComboBox.SelectedItem ;
               }
               
               set 
               {
                  MediaTypeComboBox.SelectedItem = value ;
               }
            }
            
            public ScpInfo SelectedServer
            {
               get 
               {
                  return ServerAEComboBox.SelectedItem as ScpInfo;
               }
               
               set 
               {
                  ServerAEComboBox.SelectedItem = value ;
               }
            }
            
            public bool IncludeDisplayApplication
            {
               get 
               {
                  return IncludeDisplayApplicationCheckBox.Checked ;
               }
               
               set 
               {
                  IncludeDisplayApplicationCheckBox.Checked = value ;
               }
            }
            
            public bool ClearInstancesAfterRequest
            {
               get 
               {
                  return ClearDicomInstancesCheckBox.Checked ;
               }
               
               set 
               {
                  ClearDicomInstancesCheckBox.Checked = value ;
               }
            }
            
            public bool CanVerify
            {
               set
               {
                  VerifyServerButton.Enabled = value ;
               }
            }
            
            public bool CanSendCreateRequest
            {
               set
               {
                  SendMediaRequestButton.Enabled = value ;
               }
            }
            
         #endregion
         
         #region Events
            

            public event EventHandler MediaTitleChanged;
            public event EventHandler DeActivated;
            
         #endregion
         
         #region Data Types Definition
            
         #endregion
         
         #region Callbacks
         
            public event EventHandler VerifySelectedServer ;
            public event EventHandler SendMediaCreationRequest ;
            public event EventHandler SelectedServerChanged ;
            
         #endregion
         
      #endregion
      
      #region Protected
         
         #region Methods
         
            protected virtual void OnMediaTitleChanged ( ) 
            {
               if ( null != MediaTitleChanged ) 
               {
                  MediaTitleChanged ( this, EventArgs.Empty ) ;
               }
            }
            
         #endregion
         
         #region Properties
            
         #endregion
         
         #region Events
            
         #endregion
         
         #region Data Members
            
         #endregion
         
         #region Data Types Definition
            
         #endregion
         
      #endregion
      
      #region Private
         
         #region Methods
         
            private void  OnVerifySelectedServer ( ) 
            {
               if ( null != VerifySelectedServer ) 
               {
                  VerifySelectedServer ( this, EventArgs.Empty ) ;
               }
            }
            
            private void OnSendMediaCreationRequest ( )
            {
               if ( null != SendMediaCreationRequest )
               {
                  SendMediaCreationRequest  ( this, EventArgs.Empty ) ;
               }
            }
            
         #endregion
         
         #region Properties
            
         #endregion
         
         #region Events
         
            void ServerAEComboBox_SelectionChangeCommitted ( object sender, EventArgs e )
            {
               try
               {
                  if ( null != SelectedServerChanged ) 
                  {
                     SelectedServerChanged ( this, e ) ;
                  }
               }
               catch ( Exception exception ) 
               {
                  ThreadSafeMessager.ShowError (  exception.Message ) ;
               }
            }

            void SendMediaRequestButton_Click(object sender, EventArgs e)
            {
               try
               {
                  this.Cursor = Cursors.WaitCursor ;
                  
                  OnSendMediaCreationRequest ( ) ;
               }
               catch ( Exception exception ) 
               {
                  ThreadSafeMessager.ShowError (  exception.Message ) ;
               }
               finally
               {
                  this.Cursor = Cursors.Default ;
               }
            }

            void VerifyServerButton_Click(object sender, EventArgs e)
            {
               try
               {
                  this.Cursor = Cursors.WaitCursor ;
                  
                  OnVerifySelectedServer ( ) ;
               }
               catch ( Exception exception ) 
               {
                  ThreadSafeMessager.ShowError (  exception.Message ) ;
               }
               finally
               {
                  this.Cursor = Cursors.Default ;
               }
            }
            
            void MediaTitleTextBox_TextChanged ( object sender, EventArgs e )
            {
               try
               {
                  _mediaTitleErrorProv.SetError ( MediaTitleTextBox, null ) ;
                  
                  OnMediaTitleChanged ( ) ;
               }
               catch ( Exception exception ) 
               {
                  ThreadSafeMessager.ShowError (  exception.Message ) ;
               }
            }
            
         #endregion
         
         #region Data Members
         
            ErrorProvider _mediaTitleErrorProv ;
            
         #endregion
         
         #region Data Types Definition
            
         #endregion
         
      #endregion
      
      #region internal
         
         #region Methods
            
         #endregion
         
         #region Properties
            
         #endregion
         
         #region Events
            
         #endregion
         
         #region Data Types Definition
            
         #endregion
         
         #region Callbacks
            
         #endregion
         
      #endregion

      #region IWorkstationView Members

      public void ActivateView(IWin32Window owner)
      {
         this.Focus ( ) ;
      }

      public void EnsureVisible(IWin32Window owner)
      {
         this.Visible = true ;
      }

      #endregion
   }
}
