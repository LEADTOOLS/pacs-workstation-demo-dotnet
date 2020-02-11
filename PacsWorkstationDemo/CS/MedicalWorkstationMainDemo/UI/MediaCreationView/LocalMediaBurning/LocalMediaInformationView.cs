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
using Leadtools.Medical.Workstation.UI;

namespace Leadtools.Demos.Workstation
{
   public partial class LocalMediaInformationView : UserControl, ILocalMediaInformationView
   {
      public LocalMediaInformationView()
      {
         InitializeComponent();
      }

      private void Init()
      {
         BrowseMediaFolderButton.Click += new EventHandler ( BrowseMediaFolderButton_Click ) ;
         BrowseViewerButton.Click      += new EventHandler ( BrowseViewerButton_Click ) ;
         PrepareMediaButton.Click      += new EventHandler ( PrepareMediaButton_Click ) ;
         BurnButton.Click              += new EventHandler ( BurnButton_Click ) ;
         MediaTitleTextBox.TextChanged += new EventHandler ( MediaTitleTextBox_TextChanged ) ;
         MediaBaseFolderTextBox.TextChanged += new EventHandler(MediaBaseFolderTextBox_TextChanged);
         IncludeViewerCheckBox.CheckedChanged += new EventHandler(IncludeViewerCheckBox_CheckedChanged);
         
         CreateAutoRunCheckBox.CheckedChanged += new EventHandler ( CreateAutoRunCheckBox_CheckedChanged ) ;
      }

      public void MediaTitleValidationError ( string errorText )
      {
         _mediaTitleErrorProv.SetError ( MediaTitleTextBox, errorText ) ;
      }
      
      public void SetMediaCreationWarning ( string message )
      {
         WarningLabel.Text = message ;
      }
      
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
            
      public string MediaBaseFolder
      {
         get { return MediaBaseFolderTextBox.Text ; }
         set 
         {
            MediaBaseFolderTextBox.Text = value ;
         }
      }
      
      public string ViewerDirectory
      {
         get { return ViewerDirectoryTextBox.Text ; }
         set { ViewerDirectoryTextBox.Text = value ; }
      }
      
      public string MediaDirectory
      {
         get 
         {
            return MediaDirectoryTextBox.Text ;
         }
         
         set 
         {
            MediaDirectoryTextBox.Text = value ;
         }
      }
      
      public bool IncludeViewer
      {
         get 
         {
            return IncludeViewerCheckBox.Checked ;
         }
         
         set 
         {
            IncludeViewerCheckBox.Checked = value ;
            
            BrowseViewerButton.Enabled    = IncludeViewerCheckBox.Checked ;
            CreateAutoRunCheckBox.Enabled = IncludeViewerCheckBox.Checked ;
         }
      }
      
      public bool CreateAutoRun
      {
         get 
         {
            return CreateAutoRunCheckBox.Checked ;
         }
         
         set 
         {
            CreateAutoRunCheckBox.Checked = value ;
         }
      }
      
      public bool CanPrepareMedia
      {
         get
         {
            return PrepareMediaButton.Enabled ;
         }
         
         set
         {
            PrepareMediaButton.Enabled = value ;
         }
      }
      
      public bool CanBurnMedia
      {
         get
         {
            return BurnButton.Enabled ;
         }
         
         set
         {
            BurnButton.Enabled = value ;
         }
      }
      
      public string ViewerSize
      {
         get 
         {
            return ViewerSizeTextBox.Text ;
         }
         
         set 
         {
            ViewerSizeTextBox.Text = value ;
         }
      }
      
      public string TotalSize
      {
         get 
         {
            return TotalSizeTextBox.Text ;
         }
         
         set 
         {
            TotalSizeTextBox.Text = value ;
         }
      }

      public bool ClearInstancesAfterRequest
      {
         get
         {
            return ClearDicomInstancesCheckBox.Checked;
         }

         set
         {
            ClearDicomInstancesCheckBox.Checked = value;
         }
      }
      
      public event EventHandler MediaTitleChanged;
      public event EventHandler MediaBaseFolderChanged ;
      public event EventHandler DeActivated;
      public event EventHandler PrepareMedia ;
      public event EventHandler BurnMedia ;
      public event EventHandler CreateAutoRunChanged ;
      public event EventHandler ViewerDirectoryChanged ;
      public event EventHandler IncludeViewerChanged ;
      
      
      public void ActivateView(IWin32Window owner)
      {
         Init ( ) ;
         
         this.Focus ( ) ;
      }
      
      public void EnsureVisible (IWin32Window owner)
      {
         Visible = true ;
      }
      
      protected virtual void OnMediaTitleChanged ( ) 
      {
         if ( null != MediaTitleChanged ) 
         {
            MediaTitleChanged ( this, EventArgs.Empty ) ;
         }
      }
      
      void BrowseViewerButton_Click(object sender, EventArgs e)
      {
         try
         {
            using ( FolderBrowserDialog folderBrowse = new FolderBrowserDialog ( ) )
            {
               if ( folderBrowse.ShowDialog ( this ) == DialogResult.OK )
               {
                  ViewerDirectoryTextBox.Text = folderBrowse.SelectedPath ;
                  
                  if ( null != ViewerDirectoryChanged )
                  {
                     ViewerDirectoryChanged ( this, EventArgs.Empty ) ;
                  }
               }
            }
         }
         catch ( Exception exception )
         {
            ThreadSafeMessager.ShowError (  exception.Message ) ;
         }
      }

      void BrowseMediaFolderButton_Click(object sender, EventArgs e)
      {
         try
         {
            using ( FolderBrowserDialog folderBrowse = new FolderBrowserDialog ( ) )
            {
               if ( folderBrowse.ShowDialog ( this ) == DialogResult.OK )
               {
                  MediaBaseFolderTextBox.Text = folderBrowse.SelectedPath ;
               }
            }
         }
         catch ( Exception exception )
         {
            ThreadSafeMessager.ShowError (  exception.Message ) ;
         }
      }
      
      void PrepareMediaButton_Click ( object sender, EventArgs e )
      {
         try
         {
            this.Cursor = Cursors.WaitCursor ;
            
            if ( null != PrepareMedia ) 
            {
               PrepareMedia ( this, e ) ;
            }
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
      
      void BurnButton_Click(object sender, EventArgs e)
      {
         try
         {
            if ( null != BurnMedia ) 
            {
               BurnMedia(this, e);
            }
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
      
      void CreateAutoRunCheckBox_CheckedChanged(object sender, EventArgs e)
      {
         try
         {
            if ( null != CreateAutoRunChanged ) 
            {
               CreateAutoRunChanged ( this, e ) ;
            }
         }
         catch ( Exception exception )
         {
            ThreadSafeMessager.ShowError (  exception.Message ) ;
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
      
      void MediaBaseFolderTextBox_TextChanged(object sender, EventArgs e)
      {
         try
         {
            if ( null != MediaBaseFolderChanged ) 
            {
               MediaBaseFolderChanged ( this, e ) ;
            }
         }
         catch ( Exception exception )
         {
            ThreadSafeMessager.ShowError (  exception.Message ) ;
         }
      }
      
      void IncludeViewerCheckBox_CheckedChanged(object sender, EventArgs e)
      {
         try
         {
            BrowseViewerButton.Enabled    = IncludeViewerCheckBox.Checked ;
            CreateAutoRunCheckBox.Enabled = IncludeViewerCheckBox.Checked ;
            
            if ( null != IncludeViewerChanged ) 
            {
               IncludeViewerChanged ( this, e ) ;
            }
         }
         catch ( Exception exception )
         {
            ThreadSafeMessager.ShowError (  exception.Message ) ;
         }
      }
      
   }
}
