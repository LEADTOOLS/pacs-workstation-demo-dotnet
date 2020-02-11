namespace Leadtools.Demos.Workstation
{
   partial class WorkstationConfiguration
   {
      /// <summary> 
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary> 
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Component Designer generated code

      /// <summary> 
      /// Required method for Designer support - do not modify 
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.components = new System.ComponentModel.Container();
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorkstationConfiguration));
         this.ConfigurationTabControl = new System.Windows.Forms.TabControl();
         this.WorkstationClientTabPage = new System.Windows.Forms.TabPage();
         this.PACSTabPage = new System.Windows.Forms.TabPage();
         this.LowerPanel = new System.Windows.Forms.Panel();
         this.SeparatorGroupBox = new System.Windows.Forms.GroupBox();
         this.SaveChangesButton = new System.Windows.Forms.Button();
         this.UpperPanel = new System.Windows.Forms.Panel();
         this.ControlsAreaPanel = new System.Windows.Forms.Panel();
         this.WorkstationToolStrip = new System.Windows.Forms.ToolStrip();
         this.WorkstationClientToolStripButton = new System.Windows.Forms.ToolStripButton();
         this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
         this.PACSToolStripButton = new System.Windows.Forms.ToolStripButton();
         this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
         this.DicomSecurityToolStripButton = new System.Windows.Forms.ToolStripButton();
         this.GenericToolTip = new System.Windows.Forms.ToolTip(this.components);
         this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
         this.ConfigurationTabControl.SuspendLayout();
         this.LowerPanel.SuspendLayout();
         this.UpperPanel.SuspendLayout();
         this.WorkstationToolStrip.SuspendLayout();
         this.SuspendLayout();
         // 
         // ConfigurationTabControl
         // 
         this.ConfigurationTabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
         this.ConfigurationTabControl.Controls.Add(this.WorkstationClientTabPage);
         this.ConfigurationTabControl.Controls.Add(this.PACSTabPage);
         this.ConfigurationTabControl.ItemSize = new System.Drawing.Size(116, 30);
         this.ConfigurationTabControl.Location = new System.Drawing.Point(341, 87);
         this.ConfigurationTabControl.Name = "ConfigurationTabControl";
         this.ConfigurationTabControl.SelectedIndex = 0;
         this.ConfigurationTabControl.Size = new System.Drawing.Size(527, 415);
         this.ConfigurationTabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
         this.ConfigurationTabControl.TabIndex = 6;
         // 
         // WorkstationClientTabPage
         // 
         this.WorkstationClientTabPage.BackColor = System.Drawing.Color.SteelBlue;
         this.WorkstationClientTabPage.Location = new System.Drawing.Point(4, 34);
         this.WorkstationClientTabPage.Name = "WorkstationClientTabPage";
         this.WorkstationClientTabPage.Padding = new System.Windows.Forms.Padding(3);
         this.WorkstationClientTabPage.Size = new System.Drawing.Size(519, 377);
         this.WorkstationClientTabPage.TabIndex = 0;
         this.WorkstationClientTabPage.Text = "Workstation Client";
         this.WorkstationClientTabPage.UseVisualStyleBackColor = true;
         // 
         // PACSTabPage
         // 
         this.PACSTabPage.BackColor = System.Drawing.Color.SteelBlue;
         this.PACSTabPage.Location = new System.Drawing.Point(4, 34);
         this.PACSTabPage.Name = "PACSTabPage";
         this.PACSTabPage.Padding = new System.Windows.Forms.Padding(3);
         this.PACSTabPage.Size = new System.Drawing.Size(519, 377);
         this.PACSTabPage.TabIndex = 1;
         this.PACSTabPage.Text = "PACS";
         this.PACSTabPage.UseVisualStyleBackColor = true;
         // 
         // LowerPanel
         // 
         this.LowerPanel.Controls.Add(this.SeparatorGroupBox);
         this.LowerPanel.Controls.Add(this.SaveChangesButton);
         this.LowerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.LowerPanel.Location = new System.Drawing.Point(0, 501);
         this.LowerPanel.Name = "LowerPanel";
         this.LowerPanel.Size = new System.Drawing.Size(922, 65);
         this.LowerPanel.TabIndex = 7;
         // 
         // SeparatorGroupBox
         // 
         this.SeparatorGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
         this.SeparatorGroupBox.Location = new System.Drawing.Point(0, 0);
         this.SeparatorGroupBox.Name = "SeparatorGroupBox";
         this.SeparatorGroupBox.Size = new System.Drawing.Size(922, 3);
         this.SeparatorGroupBox.TabIndex = 0;
         this.SeparatorGroupBox.TabStop = false;
         // 
         // SaveChangesButton
         // 
         this.SaveChangesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.SaveChangesButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
         this.SaveChangesButton.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
         this.SaveChangesButton.ForeColor = System.Drawing.Color.White;
         this.SaveChangesButton.Image = ((System.Drawing.Image)(resources.GetObject("SaveChangesButton.Image")));
         this.SaveChangesButton.Location = new System.Drawing.Point(861, 3);
         this.SaveChangesButton.Name = "SaveChangesButton";
         this.SaveChangesButton.Size = new System.Drawing.Size(58, 58);
         this.SaveChangesButton.TabIndex = 1;
         this.GenericToolTip.SetToolTip(this.SaveChangesButton, "Save");
         this.SaveChangesButton.UseVisualStyleBackColor = false;
         // 
         // UpperPanel
         // 
         this.UpperPanel.Controls.Add(this.ControlsAreaPanel);
         this.UpperPanel.Controls.Add(this.WorkstationToolStrip);
         this.UpperPanel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.UpperPanel.Location = new System.Drawing.Point(0, 0);
         this.UpperPanel.Name = "UpperPanel";
         this.UpperPanel.Size = new System.Drawing.Size(922, 566);
         this.UpperPanel.TabIndex = 8;
         // 
         // ControlsAreaPanel
         // 
         this.ControlsAreaPanel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.ControlsAreaPanel.Location = new System.Drawing.Point(0, 55);
         this.ControlsAreaPanel.Name = "ControlsAreaPanel";
         this.ControlsAreaPanel.Size = new System.Drawing.Size(922, 511);
         this.ControlsAreaPanel.TabIndex = 1;
         // 
         // WorkstationToolStrip
         // 
         this.WorkstationToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.WorkstationClientToolStripButton,
            this.ToolStripSeparator1,
            this.PACSToolStripButton,
            this.ToolStripSeparator2,
            this.DicomSecurityToolStripButton,
            this.toolStripSeparator3});
         this.WorkstationToolStrip.Location = new System.Drawing.Point(0, 0);
         this.WorkstationToolStrip.Name = "WorkstationToolStrip";
         this.WorkstationToolStrip.Size = new System.Drawing.Size(922, 55);
         this.WorkstationToolStrip.TabIndex = 0;
         this.WorkstationToolStrip.Text = "Workstation Client";
         // 
         // WorkstationClientToolStripButton
         // 
         this.WorkstationClientToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.WorkstationClientToolStripButton.Font = new System.Drawing.Font("Tahoma", 8.400001F, System.Drawing.FontStyle.Bold);
         this.WorkstationClientToolStripButton.ForeColor = System.Drawing.Color.SteelBlue;
         this.WorkstationClientToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("WorkstationClientToolStripButton.Image")));
         this.WorkstationClientToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
         this.WorkstationClientToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.WorkstationClientToolStripButton.Name = "WorkstationClientToolStripButton";
         this.WorkstationClientToolStripButton.Size = new System.Drawing.Size(52, 52);
         this.WorkstationClientToolStripButton.Text = "Workstation Client";
         // 
         // ToolStripSeparator1
         // 
         this.ToolStripSeparator1.Name = "ToolStripSeparator1";
         this.ToolStripSeparator1.Size = new System.Drawing.Size(6, 55);
         // 
         // PACSToolStripButton
         // 
         this.PACSToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.PACSToolStripButton.Font = new System.Drawing.Font("Tahoma", 8.400001F, System.Drawing.FontStyle.Bold);
         this.PACSToolStripButton.ForeColor = System.Drawing.Color.SteelBlue;
         this.PACSToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("PACSToolStripButton.Image")));
         this.PACSToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
         this.PACSToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.PACSToolStripButton.Name = "PACSToolStripButton";
         this.PACSToolStripButton.Size = new System.Drawing.Size(52, 52);
         this.PACSToolStripButton.Text = "Remote PACS";
         // 
         // ToolStripSeparator2
         // 
         this.ToolStripSeparator2.Name = "ToolStripSeparator2";
         this.ToolStripSeparator2.Size = new System.Drawing.Size(6, 55);
         // 
         // DicomSecurityToolStripButton
         // 
         this.DicomSecurityToolStripButton.AutoSize = false;
         this.DicomSecurityToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.DicomSecurityToolStripButton.Image = global::Leadtools.Demos.Workstation.Properties.Resources.Lock_Icon_32;
         this.DicomSecurityToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
         this.DicomSecurityToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.DicomSecurityToolStripButton.Name = "DicomSecurityToolStripButton";
         this.DicomSecurityToolStripButton.Size = new System.Drawing.Size(52, 52);
         this.DicomSecurityToolStripButton.Text = "DICOM Security";
         this.DicomSecurityToolStripButton.ToolTipText = "DICOM Security";
         // 
         // toolStripSeparator3
         // 
         this.toolStripSeparator3.Name = "toolStripSeparator3";
         this.toolStripSeparator3.Size = new System.Drawing.Size(6, 55);
         // 
         // WorkstationConfiguration
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
         this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.Controls.Add(this.LowerPanel);
         this.Controls.Add(this.UpperPanel);
         this.Font = new System.Drawing.Font("Arial", 8F);
         this.ForeColor = System.Drawing.Color.White;
         this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
         this.Name = "WorkstationConfiguration";
         this.Size = new System.Drawing.Size(922, 566);
         this.ConfigurationTabControl.ResumeLayout(false);
         this.LowerPanel.ResumeLayout(false);
         this.UpperPanel.ResumeLayout(false);
         this.UpperPanel.PerformLayout();
         this.WorkstationToolStrip.ResumeLayout(false);
         this.WorkstationToolStrip.PerformLayout();
         this.ResumeLayout(false);

      }

      #endregion

      protected System.Windows.Forms.Button SaveChangesButton;
      protected System.Windows.Forms.TabControl ConfigurationTabControl;
      protected System.Windows.Forms.TabPage WorkstationClientTabPage;
      protected System.Windows.Forms.TabPage PACSTabPage;
      protected System.Windows.Forms.Panel LowerPanel;
      protected System.Windows.Forms.GroupBox SeparatorGroupBox;
      protected System.Windows.Forms.Panel UpperPanel;
      protected System.Windows.Forms.Panel ControlsAreaPanel;
      protected System.Windows.Forms.ToolStrip WorkstationToolStrip;
      protected System.Windows.Forms.ToolStripButton WorkstationClientToolStripButton;
      protected System.Windows.Forms.ToolStripButton PACSToolStripButton;
      protected System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
      protected System.Windows.Forms.ToolTip GenericToolTip;
      protected System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
      private System.Windows.Forms.ToolStripButton DicomSecurityToolStripButton;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
   }
}
