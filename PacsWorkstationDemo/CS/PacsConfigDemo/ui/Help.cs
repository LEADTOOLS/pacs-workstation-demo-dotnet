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
using Leadtools.DicomDemos;

namespace PACSConfigDemo
{
   public partial class HelpDialog : Form
   {
      public HelpDialog()
      {
         InitializeComponent();
      }

      public HelpDialog(string serverAE, bool showHelpCheckBox)
      {
         _serverAE = serverAE;
         _showHelpCheckBox = showHelpCheckBox;

         InitializeComponent();
      }

      private bool _showHelpCheckBox;
      private bool _checkBoxNoShowAgainResult;

      public bool CheckBoxNoShowAgainResult
      {
         get { return _checkBoxNoShowAgainResult; }
         set { _checkBoxNoShowAgainResult = value; }
      }
      private string _serverAE = string.Empty;

      private void ColorText(string text, Color color)
      {
         _richTextBoxHelp.SelectionColor = color;
         _richTextBoxHelp.SelectedText = text;
         _richTextBoxHelp.SelectionColor = Color.Black;
      }

      private void BoldText(string text)
      {
         Font font = _richTextBoxHelp.SelectionFont;
         Font fontBold = new Font(font.Name, font.Size, FontStyle.Bold);
         _richTextBoxHelp.SelectionFont = fontBold;
         _richTextBoxHelp.SelectedText = text;
         _richTextBoxHelp.SelectionFont = font;
      }

      private void HelpDialog_Load(object sender, EventArgs e)
      {
         string platform = "32 bit";
if (!DicomDemoSettingsManager.Is64Process())
   platform = " (32 bit)";
else
         platform = "64 bit";

         Font originalFont = _richTextBoxHelp.SelectionFont;

         //_richTextBoxHelp.Font = new Font(Font.FontFamily, 10);
         _pictureBox.Image = System.Drawing.SystemIcons.Information.ToBitmap();

         _richTextBoxHelp.Clear();
         _richTextBoxHelp.SelectionColor = Color.Black;
         string sMsg = string.Format("The {0} is used to configure the {1} PACS Framework demos.  To use the default settings, just click the ", Program._demoName, platform);
         _richTextBoxHelp.SelectedText = sMsg;
         BoldText("Configure");
         _richTextBoxHelp.SelectedText = " button.\n\n\n\n";

         string steps = string.Format("Steps for configuring the {0} PACS Framework demos:\n\n", platform);
         BoldText(steps);

         ColorText("(Optional) ", Color.Blue); ;
         _richTextBoxHelp.SelectedText = "Enter  server settings (AE Title and port) where the servers will listen. This defines three DICOM services that are created when you click the  ";
         BoldText("Configure");
         _richTextBoxHelp.SelectedText = " button. Check the ";
         BoldText("Start Server");
         _richTextBoxHelp.SelectedText = " check boxes if you want the DICOM services started after being created.\n\n";

         ColorText("(Optional) ", Color.Blue); ;
         _richTextBoxHelp.SelectedText = " Enter the client settings (AE Title and port) where the clients will listen.  This configures the high level DICOM client demos, and the workstation demo to communicate with the DICOM servers defined in the ";
         BoldText("Server Settings.\n\n");

         ColorText("(Required) ", Color.Red); ;
         _richTextBoxHelp.SelectedText = "Click the ";
         BoldText("Configure");
         _richTextBoxHelp.SelectedText = " button to create the DICOM services, and configure the client demos.\n\n";

         ColorText("(Optional) ", Color.Blue); ;
         _richTextBoxHelp.SelectedText = "Click the Remove button to display a dialog that allows you to remove one or more of the DICOM services that you have created.\n\n\n\n";

         BoldText("After configuring the PACS Framework demos:\n\n");
         _richTextBoxHelp.SelectedText = "Run the CSDicomHighLevelStoreDemo to store images to the DICOM server, the workstation server.\n\n";
         _richTextBoxHelp.SelectedText = "Run the CSDicomHighLevelClientDemo to retrieve the stored images from the DICOM server, or the workstation server.\n\n";
         _richTextBoxHelp.SelectedText = "Run the CSMedicalWorkstationMainDemo to retrieve the stored images from the DICOM server.\n";
         _richTextBoxHelp.SelectionBullet = false;

         _checkBoxNoShowAgain.Visible = _showHelpCheckBox;
         buttonOK.Select();
      }

      private void HelpDialog_FormClosed(object sender, FormClosedEventArgs e)
      {
         CheckBoxNoShowAgainResult = _checkBoxNoShowAgain.Checked;
      }
   }
}
