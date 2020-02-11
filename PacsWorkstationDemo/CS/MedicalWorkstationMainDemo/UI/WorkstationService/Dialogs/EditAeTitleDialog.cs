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
using System.Text.RegularExpressions;
using Leadtools.Dicom.AddIn.Common;
using Leadtools.Dicom;
using Leadtools.DicomDemos;

namespace Leadtools.Demos.Workstation
{
    public partial class EditAeTitleDialog : Form
    {
        private AeInfo _AeInfo = null;

      public bool ServerSecure
      {
         get;
         set;
      }

        public AeInfo AeInfo
        {
            get
            {
                return _AeInfo;
            }
            
            set
            {
               _AeInfo = value ;
            }
        }

        public EditAeTitleDialog ( )
        {
            InitializeComponent();
            
            Icon = WorkstationUtils.GetApplicationIcon ( ) ;
            
            AETitleTextBox.Validating += new CancelEventHandler(AETitle_Validating);
        }

        void AETitle_Validating(object sender, CancelEventArgs e)
        {
            if ( AETitleTextBox.Text .Length == 0 ) 
            {
               AeErrorProvider.SetError ( AETitleTextBox, "AE Title can't be empty." ) ;
               
               e.Cancel = true ;
            }
            else if ( AETitleTextBox.Text .Length > 16 ) 
            {
               AeErrorProvider.SetError ( AETitleTextBox, "AE Title must be less than 16 characters." ) ;
               
               e.Cancel = true ;
            }
            else 
            {
               AeErrorProvider.SetError ( AETitleTextBox, string.Empty ) ;
            }
        }

      private void EditAeTitleDialog_Load ( object sender, EventArgs e )
      {
         comboBoxClientPortSelection.Items.Add(ClientPortUsageType.Unsecure);
         comboBoxClientPortSelection.Items.Add(ClientPortUsageType.Secure);
         comboBoxClientPortSelection.Items.Add(ClientPortUsageType.SameAsServer);

         if (_AeInfo == null)
         {
            Text = "Add AE Title" ;
            comboBoxClientPortSelection.SelectedItem = ClientPortUsageType.Unsecure;
         }
         else
         {             
            Text = "Edit AE Title";
            AETitleTextBox.Text  = _AeInfo.AETitle;
            HostNameTextBox.Text = _AeInfo.Address;
            
            PortNumericUpDown.Value = Convert.ToDecimal(_AeInfo.Port);
            SecurePortNumericUpDown.Value = Convert.ToDecimal(_AeInfo.SecurePort);
            comboBoxClientPortSelection.SelectedItem = _AeInfo.ClientPortUsage;
         }

         comboBoxClientPortSelection.SelectedIndexChanged += ComboBoxClientPortSelection_SelectedIndexChanged;

         UpdateClientPortIcons();

         AETitle_TextChanged(null, EventArgs.Empty);
      }

      void UpdateClientPortIcons()
      {
         if (comboBoxClientPortSelection.SelectedItem is ClientPortUsageType)
         {
            ClientPortUsageType portUsage = (ClientPortUsageType)comboBoxClientPortSelection.SelectedItem;
            switch (portUsage)
            {
               case ClientPortUsageType.Unsecure:
                  pictureBoxUnsecurePort.Visible = true;
                  pictureBoxSecurePort.Visible = false;
                  break;
               case ClientPortUsageType.Secure:
                  pictureBoxUnsecurePort.Visible = false;
                  pictureBoxSecurePort.Visible = true;
                  break;
               case ClientPortUsageType.SameAsServer:
                  pictureBoxUnsecurePort.Visible = !this.ServerSecure;
                  pictureBoxSecurePort.Visible = this.ServerSecure;
                  break;
            }
         }
      }

      private void ComboBoxClientPortSelection_SelectedIndexChanged(object sender, EventArgs e)
      {
         ClientPortUsageType portUsage = (ClientPortUsageType)comboBoxClientPortSelection.SelectedItem;
         if (portUsage == ClientPortUsageType.Secure || portUsage == ClientPortUsageType.SameAsServer)
         {
            if (Utils.VerifyOpensslVersion(this) == false)
            {
               comboBoxClientPortSelection.SelectedIndex = 0;
               return;
            }
         }
         UpdateClientPortIcons();
      }

      private void EditAeTitleDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                if (_AeInfo == null)
                {
                    _AeInfo = new AeInfo();
                }

                _AeInfo.AETitle = AETitleTextBox.Text;
                _AeInfo.Address = HostNameTextBox.Text;                    
                _AeInfo.Port    = Convert.ToInt32(PortNumericUpDown.Value);
                _AeInfo.SecurePort = Convert.ToInt32(SecurePortNumericUpDown.Value);  

                if(comboBoxClientPortSelection.SelectedItem is ClientPortUsageType)
                {
                   ClientPortUsageType clientPortUsage = (ClientPortUsageType)comboBoxClientPortSelection.SelectedItem;

                   if (clientPortUsage != ClientPortUsageType.None)
                   {
                      _AeInfo.ClientPortUsage = clientPortUsage;
                   }
               }

                _AeInfo.LastAccessDate = DateTime.Now;
            }
        }


        private void AETitle_TextChanged(object sender, EventArgs e)
        {
            OkButton.Enabled = AETitleTextBox.Text != string.Empty;
        }
    }
}
