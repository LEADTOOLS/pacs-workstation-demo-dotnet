// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Leadtools.Dicom.Server.Admin;
using Leadtools.DicomDemos;

namespace PACSConfigDemo
{
   public partial class RemoveDialog : Form
   {

      private ServiceAdministrator _serviceAdmin;

      public RemoveDialog()
      {
         InitializeComponent();

         //Application.Idle += new EventHandler(Application_Idle);
      }

      //void Application_Idle(object sender, EventArgs e)
      //{
      //   bool removeAll = true;

      //   foreach (KeyValuePair<string, DicomService> kv in _serviceAdmin.Services)
      //   {
      //      if ( ( kv.Value.Status != System.ServiceProcess.ServiceControllerStatus.Paused ) &&
      //           ( kv.Value.Status != System.ServiceProcess.ServiceControllerStatus.Running ) &&
      //           ( kv.Value.Status != System.ServiceProcess.ServiceControllerStatus.Stopped) )
      //      {
      //         removeAll = false;

      //         foreach (ListViewItem item in listViewServers.CheckedItems)
      //         {
      //            if (item.Text == kv.Key)
      //            {
      //               item.Checked = false;
      //            }
      //         }
      //      }
      //   }

      //   buttonRemoveChecked.Enabled = removeAll;
      //   buttonRemoveAll.Enabled = removeAll;
      //}


      public ServiceAdministrator ServiceAdmin
      {
         get { return _serviceAdmin; }
         set { _serviceAdmin = value; }
      }

      private void ClearErrors()
      {
         errorProvider.SetError(labelError, "");
         labelError.Text = "";
         labelMessage.Text = "";
         Application.DoEvents();
      }

      private void ShowError(string sError)
      {
         errorProvider.SetError(labelError, sError);
         labelError.ForeColor = Color.Red;
         labelError.Text = sError;
         Application.DoEvents();
      }

      private void ShowMessage(string sMsg)
      {
         labelMessage.ForeColor = Color.Blue;
         labelMessage.Text = sMsg;
         //MainForm.AddItemWithColor(sMsg, labelMessage.ForeColor);
         Application.DoEvents();
      }

      private void EnableDialogItems(bool bEnable)
      {
         if (bEnable)
         {
            bool bEnableRemoveAll = listViewServers.Items.Count > 0;
            buttonRemoveAll.Enabled = bEnableRemoveAll;
            toolStripMenuItemRemoveAll.Enabled = bEnableRemoveAll;

            bool bEnableRemoveChecked = false;
            foreach (ListViewItem item in listViewServers.Items)
            {
               if (item.Checked)
                  bEnableRemoveChecked = true;
            }
            buttonRemoveChecked.Enabled = bEnableRemoveChecked;
            toolStripMenuItemRemoveChecked.Enabled = bEnableRemoveChecked;
         }
         else
         {
            buttonRemoveAll.Enabled = false;
            toolStripMenuItemRemoveAll.Enabled = false;
            buttonRemoveChecked.Enabled = false;
            toolStripMenuItemRemoveChecked.Enabled = false;
         }
         buttonClose.Enabled = bEnable;
         Application.DoEvents();
      }

      private void SizeColumns()
      {
         if (listViewServers.Items.Count > 0)
         {
            // Size to content
            foreach (ColumnHeader header in listViewServers.Columns)
            {
               if (header.Text.Contains("Port"))
                  header.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
               else
                  header.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
         }
         else
         {
            // size to header
            foreach (ColumnHeader header in listViewServers.Columns)
               header.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
         }
      }

      private void DisplayServers()
      {
         if (_serviceAdmin == null)
            return;

         listViewServers.Items.Clear();

         List<string> keys = new List<string>();

         foreach (KeyValuePair<string, DicomService> kv in _serviceAdmin.Services)
         {
            keys.Add(kv.Key);
            ListViewItem item = null;
            item = listViewServers.Items.Add(kv.Key);
            item.SubItems.Add(kv.Value.Settings.Port.ToString());
            item.SubItems.Add(kv.Value.Status.ToString());
            item.SubItems.Add(kv.Value.ServiceDirectory);
         }
         SizeColumns();
         EnableDialogItems(true);
      }

      private void RemoveDialog_Load(object sender, EventArgs e)
      {
         // Set up the tooltips
         toolTip.AutoPopDelay = 5000;
         toolTip.InitialDelay = 1000;
         toolTip.ReshowDelay = 500;
         toolTip.ShowAlways = true;

         toolTip.SetToolTip(buttonRemoveChecked, "Remove the 'checked' DICOM Server Services.");
         toolTip.SetToolTip(buttonRemoveAll, "Remove all listed DICOM Server Services.");
         toolTip.SetToolTip(buttonClose, "Exit this program.");

         toolTip.SetToolTip(listViewServers, "List of all created sample DICOM Server Services.");

         labelRemoveChecked.Text = "<== Click to remove all of the checked DICOM services";
         labelRemoveAll.Text = "<== Click to remove ALL of the DICOM services";

         ClearErrors();
         
         
         DisplayServers ( ) ;
      }

      private int UninstallDicomServers(bool bChecked, bool removeFromBaseDirectoryOnly)
      {
         if (_serviceAdmin == null)
            return 0;

         int nCountRemoved = 0;
         try
         {
            List<DicomAE> servers = new List<DicomAE>();

            foreach (ListViewItem item in listViewServers.Items)
            {
               bool remove = (!bChecked || (bChecked && item.Checked));
               if (remove)
               {
                  DicomService service = null;

                  try
                  {
                     service = _serviceAdmin.Services[item.Text];
                  }
                  catch (Exception e)
                  {
                     ShowError(e.Message);
                  }

                  if (service != null)
                  {
                     ShowMessage("Removing: " + item.Text);
                     if (MyUtils.UninstallOneDicomServer(service, removeFromBaseDirectoryOnly, _serviceAdmin))
                     {
                        servers.Add(new DicomAE(service.Settings.AETitle, service.Settings.IpAddress, service.Settings.Port, service.Settings.ReconnectTimeout, service.Settings.Secure));

                        nCountRemoved++;
                     }
                  }
               }
            }

            UninstalledServers = servers.ToArray();
         }
         catch (Exception e)
         {
            ShowError(e.Message);
         }
         return nCountRemoved;
      }

      private int UninstallAllDicomServers()
      {
         return UninstallDicomServers(false, false);
      }

      private int UninstallCheckedDicomServers()
      {
         return UninstallDicomServers(true, false);
      }

      private void RemoveChecked()
      {
         ClearErrors();
         int nCountRemoved = 0;
         Cursor oldCursor = Cursor;
         Cursor = Cursors.WaitCursor;
         EnableDialogItems(false);
         try
         {
            nCountRemoved = UninstallCheckedDicomServers();
         }
         catch (Exception)
         {
         }
         DisplayServers();
         Cursor = oldCursor;
         string sMsg = string.Format("Total DICOM Server services removed: {0}", nCountRemoved);
         ShowMessage(sMsg);
         EnableDialogItems(true);
      }

      private void RemoveAll()
      {
         ClearErrors();
         int nCountRemoved = 0;
         EnableDialogItems(false);
         try
         {
            nCountRemoved = UninstallAllDicomServers();
         }
         catch
         {
         }
         DisplayServers();
         string sMsg = string.Format("Total DICOM Server services removed: {0}", nCountRemoved);
         ShowMessage(sMsg);
         EnableDialogItems(true);
      }

      private void listViewServers_ItemChecked(object sender, ItemCheckedEventArgs e)
      {
         EnableDialogItems(true);
      }

      private void listViewServers_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
      {
         EnableDialogItems(true);
      }

      private void buttonRemoveChecked_Click(object sender, EventArgs e)
      {
         RemoveChecked();
      }

      private void buttonRemoveAll_Click(object sender, EventArgs e)
      {
         RemoveAll();
      }

      private void toolStripMenuItemRemoveChecked_Click(object sender, EventArgs e)
      {
         RemoveChecked();
      }

      private void toolStripMenuItemRemoveAll_Click(object sender, EventArgs e)
      {
         RemoveAll();
      }

      private void buttonClose_Click(object sender, EventArgs e)
      {
         this.DialogResult = DialogResult.OK;
         this.Close();
      }

      private void copyPathToolStripMenuItem_Click(object sender, EventArgs e)
      {
         foreach (ListViewItem item in listViewServers.Items)
         {
            if (item.Selected)
            {
               Clipboard.SetText(item.SubItems[columnHeaderPath.Index].Text);
            }
         }
      }
      
      public DicomAE [] UninstalledServers
      {
         get
         {
            return _removedServers ;
         }
         
         private set
         {
            _removedServers = value ;
         }
      }
      
      private DicomAE [] _removedServers ;
   }
}
