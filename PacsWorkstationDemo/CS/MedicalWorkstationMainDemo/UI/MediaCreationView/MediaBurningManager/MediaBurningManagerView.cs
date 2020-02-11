// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Leadtools.Medical.Workstation.UI;
using Leadtools.Medical.Workstation.Interfaces.Views;

namespace Leadtools.Demos.Workstation
{
   public partial class MediaBurningManagerView : WorkstationModalViewBase, IWorkstationView, IMediaBurningManagerView<IPacsMediaInformationView>, IMediaBurningManagerView<ILocalMediaInformationView>
   {
      public MediaBurningManagerView()
      {
         InitializeComponent();

         CloseButton.Click += new EventHandler(CloseViewButton_Click);
      }

      void CloseViewButton_Click(object sender, EventArgs e)
      {
         OnCloseViewRequested ( ) ;
      }
      
      public event EventHandler CloseViewRequested ;
      
      public IPacsMediaInformationView MediaInformationView
      {
         get 
         {
            return MediaInformationControl ;
         }
      }
      
      public IDicomInstancesSelectionView DicomInstancesSelectionView
      {
         get
         {
            return DicomInstancesSelectionControl ;
         }
      }
      
      public bool EnableLocalMediaInfomration
      {
         get 
         {
            return localMediaBurningView1.Enabled ;
         }
         
         set
         {
            localMediaBurningView1.Enabled = value ;
         }
      }
      
      public bool EnablePacsMediaInfomration
      {
         get 
         {
            return MediaInformationControl.Enabled ;
         }
         
         set
         {
            MediaInformationControl.Enabled = value ;
         }
      }
      
      private void OnCloseViewRequested ( )
      {
         if ( null != CloseViewRequested )
         {
            CloseViewRequested ( this, EventArgs.Empty ) ;
         }
      }

      ILocalMediaInformationView IMediaBurningManagerView<ILocalMediaInformationView>.MediaInformationView
      {
         get { return localMediaBurningView1 ; }
      }

      public void ShowMedia ( MediaViewType mediaViewType )
      {
         switch ( mediaViewType ) 
         {
            case MediaViewType.Local:
            {
               tabControl1.SelectedTab = LocalTabPage ;
            }
            break ;
            
            case MediaViewType.Pacs:
            {
               tabControl1.SelectedTab = PacsTabPage ;
            }
            break ;
         }
      }
   }
   
   public enum MediaViewType
   {
      Local,
      Pacs
   }
}
