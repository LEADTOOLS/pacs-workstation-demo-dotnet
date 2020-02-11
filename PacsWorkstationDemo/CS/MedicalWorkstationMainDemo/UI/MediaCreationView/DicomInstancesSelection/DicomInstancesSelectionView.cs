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
using Leadtools.Medical.Workstation;

namespace Leadtools.Demos.Workstation
{
   public partial class DicomInstancesSelectionView : UserControl, IDicomInstancesSelectionView
   {
      public DicomInstancesSelectionView()
      {
         InitializeComponent ( ) ;

         __CheckedInstances = new List<ClientQueryDataSet.ImagesRow> ( ) ;
         
         _patientsNodes =  new Dictionary<string,TreeNode>( ) ;
         _studiesNodes  = new Dictionary<string,TreeNode> ( ) ;
         _seriesNodes   = new Dictionary<string,TreeNode>  ( ) ;
         _imagesNodes   = new Dictionary<string,TreeNode> ( ) ;
         
         
         StudyNodesTreeView.ExpandAll ( ) ;
         
         AddAllButton.Tag          = new Action <TreeNode> ( CheckNode ) ;
         RemoveAllButton.Tag       = new Action <TreeNode> ( UncheckNode ) ;
         EveryOtherImageButton.Tag = new Action <TreeNode> ( CheckEveryOther ) ;
         
         StudyNodesTreeView.AfterCheck += new TreeViewEventHandler ( StudyNodesTreeView_AfterCheck ) ;
         
         AddAllButton.Click            += new EventHandler ( CheckActionAllButton_Click ) ;
         RemoveAllButton.Click         += new EventHandler ( CheckActionAllButton_Click ) ;
         EveryOtherImageButton.Click   += new EventHandler ( CheckActionSelectedButton_Click ) ;
         ClearButton.Click             += new EventHandler ( ClearButton_Click ) ;
         
      }
      
      public void ClearItems ( )
      {
         StudyNodesTreeView.Nodes.Clear ( ) ;
         __CheckedInstances.Clear ( ) ;
         
         _patientsNodes.Clear ( ) ;
         _studiesNodes.Clear  ( ) ;
         _seriesNodes.Clear   ( ) ;
         _imagesNodes.Clear   ( ) ;
      }
      
      void CheckActionAllButton_Click ( object sender, EventArgs e )
      {
         try
         {
            foreach ( TreeNode node in StudyNodesTreeView.Nodes )
            {
               ApplyAction ( node, ( Action <TreeNode> ) ( ( Button ) sender ).Tag ) ;
            }
         }
         catch ( Exception exception ) 
         {
            ThreadSafeMessager.ShowError (  exception.Message ) ;
         }
      }

      void CheckActionSelectedButton_Click ( object sender, EventArgs e )
      {
         try
         {
            if ( null != StudyNodesTreeView.SelectedNode ) 
            {
               ApplyAction ( StudyNodesTreeView.SelectedNode, ( Action <TreeNode> ) ( ( Button ) sender ).Tag ) ;
            }
         }
         catch ( Exception exception ) 
         {
            ThreadSafeMessager.ShowError (  exception.Message ) ;
         }
      }
      
      void ClearButton_Click ( object sender, EventArgs e )
      {
         try
         {
            ClearItems ( ) ;
         }
         catch ( Exception exception ) 
         {
            ThreadSafeMessager.ShowError (  exception.Message ) ;
         }
      }
      
      private void CheckNode ( TreeNode node )
      {
         if ( node.Checked != true ) 
         {
            node.Checked = true ;
         }
      }
      
      private void UncheckNode ( TreeNode node )
      {
         if ( node.Checked != false ) 
         {
            node.Checked = false ;
         }
      }
      
      private void CheckEveryOther ( TreeNode node )
      {
         bool check ;
         
         
         check = node.Index % 2 == 0 ;
         
         if ( node.Checked != check ) 
         {
            node.Checked = check ;
         }
      }
      
      private void ApplyAction ( TreeNode selectedNode, Action <TreeNode> action )
      {
         if ( selectedNode.Nodes.Count == 0 ) 
         {
            action ( selectedNode ) ;
         }
         else
         {
            foreach ( TreeNode node in selectedNode.Nodes ) 
            {
               ApplyAction ( node, action ) ;
            }
         }
      }

      void StudyNodesTreeView_AfterCheck ( object sender, TreeViewEventArgs e )
      {
         try
         {
            VerifyInstanceCheckState ( e.Node ) ;
            
            if ( isUpdating )
            {
               return ;
            }
            
            isUpdating = true ;
            
            try
            {
               ApplyCheckToChilds ( e.Node ) ;
               
               ApplyCheckToParents ( e.Node ) ;
            }
            finally
            {
               isUpdating = false ;
            }
         }
         catch ( Exception exception ) 
         {
            ThreadSafeMessager.ShowError (  exception.Message ) ;
         }
      }

      private void VerifyInstanceCheckState ( TreeNode treeNode )
      {
         if ( treeNode.Tag is ClientQueryDataSet.ImagesRow ) 
         {
            if ( treeNode.Checked ) 
            {
               ClientQueryDataSet.ImagesRow image ;
               
               
               image = (ClientQueryDataSet.ImagesRow) treeNode.Tag ;
               
               if ( !__CheckedInstances.Contains ( image) )
               {
                  __CheckedInstances.Add ( (ClientQueryDataSet.ImagesRow) treeNode.Tag ) ;
               }
            }
            else
            {
               __CheckedInstances.Remove ( (ClientQueryDataSet.ImagesRow) treeNode.Tag ) ;
            }
         }
      }

      private void ApplyCheckToParents ( TreeNode treeNode )
      {
         if ( treeNode.Parent != null ) 
         {
            if ( !treeNode.Checked )
            {
               if ( treeNode.Parent.Checked != treeNode.Checked )
               {
                  treeNode.Parent.Checked = treeNode.Checked ;
               }
               
               ApplyCheckToParents ( treeNode.Parent ) ;
            }
            else
            {
               if ( IsAllSiblingsChecked ( treeNode ) )
               {
                  if ( treeNode.Parent.Checked != treeNode.Checked )
                  {
                     treeNode.Parent.Checked = treeNode.Checked ;
                  }
                  
                  ApplyCheckToParents ( treeNode.Parent ) ;
               }
            }
         }
      }

      private bool IsAllSiblingsChecked(TreeNode treeNode)
      {
         foreach ( TreeNode node in treeNode.Parent.Nodes ) 
         {
            if ( !node.Checked ) 
            {
               return false ;
            }
         }
         
         return true ;
      }

      private void ApplyCheckToChilds ( TreeNode treeNode )
      {
         foreach ( TreeNode childNode in treeNode.Nodes ) 
         {
            if ( childNode.Checked != treeNode.Checked )
            {
               childNode.Checked = treeNode.Checked ;
            }
            
            ApplyCheckToChilds ( childNode ) ;
         }
      }
      
      public void AddSeries
      ( 
         ClientQueryDataSet.StudiesRow studyInformation,  
         ClientQueryDataSet.SeriesRow series,  
         ClientQueryDataSet.ImagesRow[] images
      ) 
      {
         TreeNode patientNode ;
         
         
         patientNode = FindPatientNode ( studyInformation ) ;
         
         if ( null != patientNode ) 
         {
            TreeNode studyNode = FindStudyNode ( studyInformation, patientNode ) ;
            
            if ( studyNode != null )
            {
               TreeNode seriesNode = FindSeriesNode ( series, studyNode ) ;
               
               if ( null != seriesNode ) 
               {
                  foreach ( ClientQueryDataSet.ImagesRow instanceInfo in images )
                  {
                     TreeNode instanceNode = FindInstanceNode ( instanceInfo, seriesNode ) ;
                     
                     if ( null == instanceNode ) 
                     {
                        CreateInstanceNode ( instanceInfo, seriesNode ) ;
                     }
                  }
               }
               else
               {
                  CreateSeriesNode ( series, images, studyNode ) ;
               }
            }
            else
            {
               CreateStudyNode ( studyInformation, series, images, patientNode ) ;
            }
         }
         else
         {
            CreatePatientNode ( studyInformation, series, images ) ;
         }
      }
      
      private void CreatePatientNode 
      ( 
         ClientQueryDataSet.StudiesRow studyInformation, 
         ClientQueryDataSet.SeriesRow series, 
         ClientQueryDataSet.ImagesRow[] images
      ) 
      {
         string patientText ;
         
         
         if ( !studyInformation.IsPatientNameNull ( ) && 
              !string.IsNullOrEmpty ( studyInformation.PatientName ) )
         {
            patientText = studyInformation.PatientName ;
         }
         else if ( !studyInformation.IsPatientIDNull ( ) &&
                   !string.IsNullOrEmpty ( studyInformation.PatientID ) )
         {
            patientText = studyInformation.PatientID ;
         }
         else
         {
            patientText = "Unknown" ;
         }
         
         TreeNode patientNode = StudyNodesTreeView.Nodes.Add (  studyInformation.IsPatientNameNull ( ) ? "Unknown" : studyInformation.PatientName ) ;
         
         
         patientNode.Tag = studyInformation ;
         
         _patientsNodes.Add ( studyInformation.IsPatientIDNull ( )? "Unknown" : studyInformation.PatientID, patientNode ) ;
         
         CreateStudyNode ( studyInformation, series, images, patientNode ) ;
      }

      private void CreateStudyNode 
      ( 
         ClientQueryDataSet.StudiesRow studyInformation, 
         ClientQueryDataSet.SeriesRow series, 
         ClientQueryDataSet.ImagesRow[] images, 
         TreeNode parentNode 
      )
      {
         string studyText = string.Empty ;
         
         
         if ( !studyInformation.IsStudyDescriptionNull ( ) &&
              !string.IsNullOrEmpty ( studyInformation.StudyDescription ) )
         {
            studyText = studyInformation.StudyDescription ;
         }
         else 
         {
            studyText = studyInformation.StudyInstanceUID ;
         }
         
         TreeNode studyNode = parentNode.Nodes.Add ( studyText ) ;
         
         
         studyNode.Tag = studyInformation ;
         
         _studiesNodes.Add ( studyInformation.StudyInstanceUID, studyNode ) ;
         
         CreateSeriesNode ( series, images, studyNode ) ;
         
      }

      private void CreateSeriesNode 
      ( 
         ClientQueryDataSet.SeriesRow series, 
         ClientQueryDataSet.ImagesRow[] instances, 
         TreeNode studyNode 
      )
      {
         string seriesText = string.Empty ;
         TreeNode seriesNode ;
         
         if ( !series.IsSeriesNumberNull ( ) || !string.IsNullOrEmpty ( series.SeriesNumber ) )
         {
            seriesText += "#" + series.SeriesNumber + ": " ;
         }
         
         if ( !series.IsSeriesDescriptionNull ( ) && !string.IsNullOrEmpty ( series.SeriesDescription ) )
         {
            seriesText += series.SeriesDescription ;
         }
         
         if ( string.IsNullOrEmpty ( seriesText ) )
         {
            seriesText = series.SeriesInstanceUID ;
         }
         
         seriesNode = studyNode.Nodes.Add ( seriesText ) ;
         
         seriesNode.Tag = series ;
         
         _seriesNodes.Add ( series.SeriesInstanceUID, seriesNode ) ;
         
         foreach ( ClientQueryDataSet.ImagesRow instance in instances ) 
         {
            CreateInstanceNode ( instance, seriesNode ) ;
         }
      }

      private void CreateInstanceNode ( ClientQueryDataSet.ImagesRow instanceInfo, TreeNode seriesNode )
      {
         string instanceText ;
         TreeNode instanceNode ;
         
         
         if ( instanceInfo.IsInstanceNumberNull ( ) || 
              string.IsNullOrEmpty ( instanceInfo.InstanceNumber ) )
         {
            instanceText = instanceInfo.SOPInstanceUID ;
         }
         else
         {
            instanceText = instanceInfo.InstanceNumber ;
         }
         
         instanceNode = new TreeNode ( instanceText ) ;
         
         instanceNode.Tag = instanceInfo ;
         
         _imagesNodes.Add ( instanceInfo.SOPInstanceUID, instanceNode ) ;
         
         seriesNode.Nodes.Add ( instanceNode );
         
         instanceNode.Checked = __CheckedInstances.Contains ( instanceInfo ) ;
      }

      private TreeNode FindInstanceNode ( ClientQueryDataSet.ImagesRow instanceInfo, TreeNode seriesNode )
      {
         if ( _imagesNodes.ContainsKey ( instanceInfo.SOPInstanceUID ) )
         {
            return _imagesNodes [ instanceInfo.SOPInstanceUID ] ;
         }
         else
         {
            return null ;
         }
      }

      private TreeNode FindSeriesNode ( ClientQueryDataSet.SeriesRow series, TreeNode studyNode )
      {
         if ( _seriesNodes.ContainsKey ( series.SeriesInstanceUID ) )
         {
            return _seriesNodes [ series.SeriesInstanceUID ] ;
         }
         else
         {
            return null ;
         }
      }

      private TreeNode FindStudyNode ( ClientQueryDataSet.StudiesRow studyRow, TreeNode patientNode )
      {
         if ( _studiesNodes.ContainsKey ( studyRow.StudyInstanceUID ) )
         {
            return _studiesNodes [ studyRow.StudyInstanceUID ] ;
         }
         else
         {
            return null ;
         }
      }

      private TreeNode FindPatientNode ( ClientQueryDataSet.StudiesRow studyRow )
      {
         string patientID ;
         
         
         patientID = studyRow.IsPatientIDNull ( ) ? "Unknown" : studyRow.PatientID ;
         
         if ( _patientsNodes.ContainsKey ( patientID ) )
         {
            return _patientsNodes [ patientID ] ;
         }
         else
         {
            return null ;
         }
      }
      
      private bool isUpdating = false ;
      
      private IList <ClientQueryDataSet.ImagesRow> __CheckedInstances
      {
         get ;
         set ;
      }
      
      private Dictionary <string, TreeNode> _patientsNodes ;
      private Dictionary <string, TreeNode> _studiesNodes ;
      private Dictionary <string, TreeNode> _seriesNodes ;
      private Dictionary <string, TreeNode> _imagesNodes ;

      public void SetState ( IList<ClientQueryDataSet.ImagesRow> burningImages )
      {
         __CheckedInstances = burningImages ;
      }
   }
   
   public class StudyNodeInformation : StudyInformation
   {
      public StudyNodeInformation ( StudyInformation studyInfo ) 
      :base ( studyInfo.PatientID, studyInfo.StudyInstanceUID )
      {}
      
      
      public SeriesNodeInformation[] Series 
      {
         get ;
         set ;
      }
   }
   
   public class SeriesNodeInformation : SeriesInformation
   {
      public SeriesNodeInformation ( SeriesInformation seriesInfo ) 
      : base ( seriesInfo.PatientId, seriesInfo.StudyInstanceUID, seriesInfo.SeriesInstanceUID, seriesInfo.Description )
      {}
      
      public InstanceInformation [] Instances 
      {
         get ;
         set ;
      }
   }
   
   
    public class MyTreeView : TreeView
    {
        #region Constructors
        public MyTreeView()
        {
        }
        #endregion

        protected override void WndProc(ref Message m)
        {
            // Suppress WM_LBUTTONDBLCLK on checkbox
            if (m.Msg == 0x0203 && CheckBoxes && IsOnCheckBox(m))
                m.Result = IntPtr.Zero;
            else
                base.WndProc(ref m);
        }

        #region Double-click check
        private int GetXLParam(IntPtr lParam)
        {
            return lParam.ToInt32() & 0xffff;
        }

        private int GetYLParam(IntPtr lParam)
        {
            return lParam.ToInt32() >> 16;
        }

        private bool IsOnCheckBox(Message m)
        {
            int x = GetXLParam(m.LParam);
            int y = GetYLParam(m.LParam);
            TreeNode node = GetNodeAt(x, y);
            if (node == null)
                return false;
            int iconWidth = ImageList == null ? 0 : ImageList.ImageSize.Width;
            int right = node.Bounds.Left - iconWidth;
            int left = right - CHECKBOX_WIDTH;
            return left <= x && x <= right;
        }

        const int CHECKBOX_WIDTH = 16;
        #endregion
    }
}
