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
using System.Net ;
using System.Windows.Forms;
using System.Management;
using Leadtools.Dicom.AddIn.Common;
using System.IO;
using Leadtools.DicomDemos;

namespace Leadtools.Demos.Workstation
{
   public partial class EditServiceDialog : Form
   {
      public enum EditMode 
      {
         AddServer,
         EditServer 
      }
      
      private ServerSettings _Settings;
      private EditMode _mode ;
      
      public ServerSettings Settings
      {
         get
         {
            return _Settings ;
         }
            
         internal set
         {
            _Settings = value ;
         }
      }
        
      public string ServiceName
      {
         get
         {
            return ServiceNameTextBox.Text ;
         }
         
         set
         {
            ServiceNameTextBox.Text = value ;
         }
      }
      
      public EditMode Mode 
      {
         get
         {
            return _mode ;
         }
         
         set
         {
            _mode = value ;
         }
      }
        
        public EditServiceDialog( ) : this ( null ) 
        {
            
        }
        
        public EditServiceDialog( ServerSettings settings )
        {
            InitializeComponent();

            Icon = WorkstationUtils.GetApplicationIcon ( ) ;
            
            ServerAETextBox.Validating += new CancelEventHandler ( ServerAE_Validating ) ;
            ServerAETextBox.Validated  += new EventHandler ( ServerAETextBox_Validated ) ;

            ServerSecureCheckBox.CheckedChanged += ServerSecureCheckBox_CheckedChanged;

            Settings = settings ;
        }

      private void ServerSecureCheckBox_CheckedChanged(object sender, EventArgs e)
      {
         if (ServerSecureCheckBox.Checked)
         {
            if (Utils.VerifyOpensslVersion(this) == false)
            {
               ServerSecureCheckBox.Checked = false;
            }
         }
      }

      void ServerAE_Validating(object sender, CancelEventArgs e)
        {
            if ( ServerAETextBox.Text .Length == 0 ) 
            {
               ControlsErrorProvider.SetError ( ServerAETextBox, "AE Title can't be empty." ) ;
               
               e.Cancel = true ;
            }
            else if ( ServerAETextBox.Text .Length > 16 ) 
            {
               ControlsErrorProvider.SetError ( ServerAETextBox, "AE Title must be less than 16 characters." ) ;
               
               e.Cancel = true ;
            }
            else 
            {
               ControlsErrorProvider.SetError ( ServerAETextBox, string.Empty ) ;
            }
        }
        
      void ServerAETextBox_Validated(object sender, EventArgs e)
      {
         if ( Mode == EditMode.AddServer ) 
         {
            ServiceNameTextBox.Text = ServerAETextBox.Text ;
         }
      }

        private void EditServerDialog_Load(object sender, EventArgs e)
        {
            ErrorLabel.Text = string.Empty ; 
            LoadIps ( ) ;
            
            if ( _Settings == null )
            {
                ClientTimeoutNumericUpDown.Value = 300;
                ServerPortMaskedNumeric.Value = 205;
                ServerMaxClientsMaskedNumeric.Value = 5 ;
                StartModeComboBox.Text = "Automatic";
                ReconnectTimeoutNumericUpDown.Value = 300;
                AddInTimeoutNumericUpDown.Value = 300;
                Text = "Add New Server" ;
                RestartLabel.Text = string.Empty;
                TemporaryDirectoryTextBox.Text = Path.GetTempPath();                
            }
            else
            {
               if ( Mode == EditMode.AddServer ) 
               {
                  Text = "Add New Server" ;
               }
               else
               {
                  Text = "Edit Server" + " [" + _Settings.AETitle + "]";
               }
                
                ServerAETextBox.Text                 = _Settings.AETitle;
                ServerAllowAnonymousCheckBox.Checked = _Settings.AllowAnonymous;
                ServerDescriptionTextBox.Text        = _Settings.Description;
                
                if ( ServerIpComboBox.Items.Contains ( _Settings.IpAddress ) )
                {
                  ServerIpComboBox.SelectedItem = _Settings.IpAddress;
               }
                
                ServerMaxClientsMaskedNumeric.Value = _Settings.MaxClients ;
                ServerPortMaskedNumeric.Value = _Settings.Port;
                ServerSecureCheckBox.Checked = _Settings.Secure;
                ClientTimeoutNumericUpDown.Value = Convert.ToDecimal(_Settings.ClientTimeout);
                TemporaryDirectoryTextBox.Text = _Settings.TemporaryDirectory;
                ImplementationClassTextBox.Text = _Settings.ImplementationClass;
                ImplementationVersionNameTextBox.Text = _Settings.ImplementationVersionName;                       
                DisplayNameTextBox.Text = _Settings.DisplayName;
                MaxPduLengthMaskedTextBox.Text = _Settings.MaxPduLength.ToString();
                ReconnectTimeoutNumericUpDown.Value = Convert.ToDecimal(_Settings.ReconnectTimeout);
                AddInTimeoutNumericUpDown.Value = Convert.ToDecimal(_Settings.AddInTimeout);
                NoDelayCheckBox.Checked = _Settings.NoDelay;
                ReceiveBufferNumericUpDown.Text = _Settings.ReceiveBufferSize.ToString();
                SendBufferNumericUpDown.Text = _Settings.SendBufferSize.ToString();
                StartModeComboBox.Text = _Settings.StartMode;
                AllowMultipleConnectionsChcekBox.Checked = _Settings.AllowMultipleConnections;
                if (StartModeComboBox.SelectedIndex == -1)
                    StartModeComboBox.SelectedIndex = 0;
                RestartLabel.Text = "Server will need to be restarted for changes to take effect";
            }


            ServerAETextBox.Focus();
            
            ServerAE_TextChanged ( null, null ) ;
        }        

        private void EditServerDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                if (_Settings == null)
                {
                    _Settings = new ServerSettings();
                }

                _Settings.AETitle = ServerAETextBox.Text;
                _Settings.AllowAnonymous = ServerAllowAnonymousCheckBox.Checked;
                _Settings.Description = ServerDescriptionTextBox.Text;
                _Settings.IpAddress = ServerIpComboBox.Text;
                _Settings.MaxClients = ( int) ServerMaxClientsMaskedNumeric.Value ;
                _Settings.Port = ( int ) ServerPortMaskedNumeric.Value;
                _Settings.Secure = ServerSecureCheckBox.Checked;
                _Settings.ClientTimeout = Convert.ToInt32(ClientTimeoutNumericUpDown.Value);
                _Settings.TemporaryDirectory = TemporaryDirectoryTextBox.Text;
                _Settings.ImplementationClass = ImplementationClassTextBox.Text;
                _Settings.ImplementationVersionName = ImplementationVersionNameTextBox.Text;            
                _Settings.DisplayName = DisplayNameTextBox.Text;
                _Settings.ReconnectTimeout = Convert.ToInt32(ReconnectTimeoutNumericUpDown.Value);
                _Settings.AddInTimeout = Convert.ToInt32(AddInTimeoutNumericUpDown.Value);
                _Settings.NoDelay = NoDelayCheckBox.Checked;
                _Settings.AllowMultipleConnections = AllowMultipleConnectionsChcekBox.Checked;

                if (MaxPduLengthMaskedTextBox.Text.Length == 0)
                    _Settings.MaxPduLength = -1;
                else
                    _Settings.MaxPduLength = Convert.ToInt32(MaxPduLengthMaskedTextBox.Text);

                if (ReceiveBufferNumericUpDown.Text.Length == 0)
                    _Settings.ReceiveBufferSize = 29696;
                else
                    _Settings.ReceiveBufferSize = Convert.ToInt32(ReceiveBufferNumericUpDown.Text);

                if (SendBufferNumericUpDown.Text.Length == 0)
                    _Settings.SendBufferSize = 29696;
                else
                    _Settings.SendBufferSize = Convert.ToInt32(SendBufferNumericUpDown.Text);

                _Settings.StartMode = StartModeComboBox.Text;                
            }
        }

         private void LoadIps()
         {
            string hostName ;
            IPAddress [ ] localIpAddress ;
            
            
            hostName = Dns.GetHostName ( ) ;
            
            ServerIpComboBox.Items.Add ( hostName ) ;
            
            localIpAddress = Dns.GetHostAddresses ( hostName ) ;
            
            foreach ( IPAddress address in localIpAddress ) 
            {
               if ( address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork ) 
               {
                  ServerIpComboBox.Items.Add ( address.ToString ( ) ) ;
               }
            }

            if (ServerIpComboBox.Items.Count > 0 )
            {
                ServerIpComboBox.SelectedIndex = 0 ;
            }            
            
            ServerIpComboBox.Enabled = ServerIpComboBox.Items.Count > 1;
        }

        private void buttonFolderBrowse_Click(object sender, EventArgs e)
        {
            TempFolderBrowserDialog.SelectedPath = TemporaryDirectoryTextBox.Text;
            if (TempFolderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                TemporaryDirectoryTextBox.Text = TempFolderBrowserDialog.SelectedPath;
            }
        }

        private bool IsDirectoryValid()
        {
            if (TemporaryDirectoryTextBox.Text != string.Empty)
            {
                string f = TemporaryDirectoryTextBox.Text;

                if(f.IndexOfAny(Path.GetInvalidPathChars())!=-1)
                    return false;
            }

            return true;
        }        

        private void TemporaryDirectory_TextChanged(object sender, EventArgs e)
        {
            if (!IsDirectoryValid())
            {
                OkButton.Enabled = false;
                ErrorLabel.Text = "Invalid directory name";
            }
            else if(TemporaryDirectoryTextBox.Text!=string.Empty && !Directory.Exists(TemporaryDirectoryTextBox.Text))
            {
                OkButton.Enabled = false;
                ErrorLabel.Text = "Temporary directory doesn't exist";
            }
            else
            {
                OkButton.Enabled = true;
                ErrorLabel.Text = string.Empty;
            }
        }

        private void TemporaryDirectory_Leave(object sender, EventArgs e)
        {
            if (TemporaryDirectoryTextBox.Text == string.Empty)
                return;

            if (!IsDirectoryValid() || !Directory.Exists(TemporaryDirectoryTextBox.Text))
                TemporaryDirectoryTextBox.Focus();
        }

        private void DisplayName_TextChanged(object sender, EventArgs e)
        {
            if (!IsDisplayNameValid())
            {
                OkButton.Enabled = false;
                if(DisplayNameTextBox.Text == string.Empty)
                    ErrorLabel.Text = "Service display name cannot be empty";
                else
                    ErrorLabel.Text = "Service display name already exist";
            }
            else
            {
                OkButton.Enabled = ServerAETextBox.Text.Length > 0;
                ErrorLabel.Text = string.Empty;
            }
        }

        private bool IsDisplayNameValid()
        {
            if (DisplayNameTextBox.Text == string.Empty && _Settings == null)
                return true;
            else if (DisplayNameTextBox.Text == string.Empty)
                return false;

            if (_Settings != null && _Settings.DisplayName == DisplayNameTextBox.Text)
                return true;

            return true ;
        }

        private void DisplayName_Leave(object sender, EventArgs e)
        {
            if (!IsDisplayNameValid())
                DisplayNameTextBox.Focus();
        }

        private void ServerAE_KeyPress(object sender, KeyPressEventArgs e)
        {
            //
            // AE Title shouldn't have spaces.  This will also become the install name
            //  of our service.
            //
            if (e.KeyChar == ' ')
            {
                e.Handled = true;
            }
        }

        private void ServerAE_TextChanged(object sender, EventArgs e)
        {            
            if (OkButton.Enabled)
            {
                DisplayName_TextChanged(null, EventArgs.Empty);
            }

            OkButton.Enabled = ServerAETextBox.Text.Length > 0;
        }
    }
}
