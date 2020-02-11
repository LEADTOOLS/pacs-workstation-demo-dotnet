namespace Leadtools.Demos.Workstation
{
   partial class StorageListenerService
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StorageListenerService));
         this.MainToolStrip = new System.Windows.Forms.ToolStrip();
         this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
         this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
         this.StartAddInsToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
         this.toolStripButton1 = new System.Windows.Forms.ToolStripSeparator();
         this.ServiceATitleLabel = new System.Windows.Forms.Label();
         this.IpAddressLabel = new System.Windows.Forms.Label();
         this.ServerPortLabel = new System.Windows.Forms.Label();
         this.IpLabel = new System.Windows.Forms.Label();
         this.PortLabel = new System.Windows.Forms.Label();
         this.LEADStorageServiceAELabel = new System.Windows.Forms.Label();
         this.ServiceTabControl = new System.Windows.Forms.TabControl();
         this.AeTitlesTabPage = new System.Windows.Forms.TabPage();
         this.ClientsToolStripContainer = new System.Windows.Forms.ToolStripContainer();
         this.AeTitlesListView = new System.Windows.Forms.ListView();
         this.AeTitleColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         this.HostnameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         this.PortColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         this.SecurePortColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         this.AeTitleToolStrip = new System.Windows.Forms.ToolStrip();
         this.WorkstationDisplayNameLabel = new System.Windows.Forms.Label();
         this.PortUsageHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         this.ServerSecurePictureBox = new System.Windows.Forms.PictureBox();
         this.AddAeTitleToolStripButton = new System.Windows.Forms.ToolStripButton();
         this.EditAeTitleToolStripButton = new System.Windows.Forms.ToolStripButton();
         this.DeleteAeTitleToolStripButton = new System.Windows.Forms.ToolStripButton();
         this.EditServerToolStripButton = new System.Windows.Forms.ToolStripButton();
         this.ToolStripButtonStart = new System.Windows.Forms.ToolStripButton();
         this.PauseToolStripButton = new System.Windows.Forms.ToolStripButton();
         this.StopToolStripButton = new System.Windows.Forms.ToolStripButton();
         this.AddServerToolStripButton = new System.Windows.Forms.ToolStripButton();
         this.DeleteServerToolStripButton = new System.Windows.Forms.ToolStripButton();
         this.EventLogViewerToolStripButton = new System.Windows.Forms.ToolStripButton();
         this.MainToolStrip.SuspendLayout();
         this.ServiceTabControl.SuspendLayout();
         this.AeTitlesTabPage.SuspendLayout();
         this.ClientsToolStripContainer.ContentPanel.SuspendLayout();
         this.ClientsToolStripContainer.TopToolStripPanel.SuspendLayout();
         this.ClientsToolStripContainer.SuspendLayout();
         this.AeTitleToolStrip.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.ServerSecurePictureBox)).BeginInit();
         this.SuspendLayout();
         // 
         // MainToolStrip
         // 
         this.MainToolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
         this.MainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditServerToolStripButton,
            this.ToolStripSeparator1,
            this.ToolStripButtonStart,
            this.PauseToolStripButton,
            this.StopToolStripButton,
            this.ToolStripSeparator2,
            this.AddServerToolStripButton,
            this.DeleteServerToolStripButton,
            this.StartAddInsToolStripSeparator,
            this.EventLogViewerToolStripButton,
            this.toolStripButton1});
         this.MainToolStrip.Location = new System.Drawing.Point(0, 0);
         this.MainToolStrip.Name = "MainToolStrip";
         this.MainToolStrip.Size = new System.Drawing.Size(552, 39);
         this.MainToolStrip.TabIndex = 0;
         this.MainToolStrip.Text = "toolStrip1";
         // 
         // ToolStripSeparator1
         // 
         this.ToolStripSeparator1.Name = "ToolStripSeparator1";
         this.ToolStripSeparator1.Size = new System.Drawing.Size(6, 39);
         // 
         // ToolStripSeparator2
         // 
         this.ToolStripSeparator2.Name = "ToolStripSeparator2";
         this.ToolStripSeparator2.Size = new System.Drawing.Size(6, 39);
         // 
         // StartAddInsToolStripSeparator
         // 
         this.StartAddInsToolStripSeparator.Name = "StartAddInsToolStripSeparator";
         this.StartAddInsToolStripSeparator.Size = new System.Drawing.Size(6, 39);
         // 
         // toolStripButton1
         // 
         this.toolStripButton1.Name = "toolStripButton1";
         this.toolStripButton1.Size = new System.Drawing.Size(6, 39);
         // 
         // ServiceATitleLabel
         // 
         this.ServiceATitleLabel.AutoSize = true;
         this.ServiceATitleLabel.Location = new System.Drawing.Point(5, 71);
         this.ServiceATitleLabel.Name = "ServiceATitleLabel";
         this.ServiceATitleLabel.Size = new System.Drawing.Size(63, 13);
         this.ServiceATitleLabel.TabIndex = 2;
         this.ServiceATitleLabel.Text = "Service AE:";
         // 
         // IpAddressLabel
         // 
         this.IpAddressLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.IpAddressLabel.Location = new System.Drawing.Point(89, 89);
         this.IpAddressLabel.Name = "IpAddressLabel";
         this.IpAddressLabel.Size = new System.Drawing.Size(105, 13);
         this.IpAddressLabel.TabIndex = 5;
         // 
         // ServerPortLabel
         // 
         this.ServerPortLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.ServerPortLabel.Location = new System.Drawing.Point(89, 106);
         this.ServerPortLabel.Name = "ServerPortLabel";
         this.ServerPortLabel.Size = new System.Drawing.Size(98, 13);
         this.ServerPortLabel.TabIndex = 7;
         // 
         // IpLabel
         // 
         this.IpLabel.AutoSize = true;
         this.IpLabel.Location = new System.Drawing.Point(5, 89);
         this.IpLabel.Name = "IpLabel";
         this.IpLabel.Size = new System.Drawing.Size(61, 13);
         this.IpLabel.TabIndex = 4;
         this.IpLabel.Text = "IP Address:";
         // 
         // PortLabel
         // 
         this.PortLabel.AutoSize = true;
         this.PortLabel.Location = new System.Drawing.Point(5, 106);
         this.PortLabel.Name = "PortLabel";
         this.PortLabel.Size = new System.Drawing.Size(29, 13);
         this.PortLabel.TabIndex = 6;
         this.PortLabel.Text = "Port:";
         // 
         // LEADStorageServiceAELabel
         // 
         this.LEADStorageServiceAELabel.AutoSize = true;
         this.LEADStorageServiceAELabel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
         this.LEADStorageServiceAELabel.Location = new System.Drawing.Point(89, 71);
         this.LEADStorageServiceAELabel.Name = "LEADStorageServiceAELabel";
         this.LEADStorageServiceAELabel.Size = new System.Drawing.Size(0, 13);
         this.LEADStorageServiceAELabel.TabIndex = 3;
         // 
         // ServiceTabControl
         // 
         this.ServiceTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.ServiceTabControl.Controls.Add(this.AeTitlesTabPage);
         this.ServiceTabControl.Location = new System.Drawing.Point(7, 138);
         this.ServiceTabControl.Name = "ServiceTabControl";
         this.ServiceTabControl.SelectedIndex = 0;
         this.ServiceTabControl.Size = new System.Drawing.Size(539, 346);
         this.ServiceTabControl.TabIndex = 8;
         // 
         // AeTitlesTabPage
         // 
         this.AeTitlesTabPage.Controls.Add(this.ClientsToolStripContainer);
         this.AeTitlesTabPage.Location = new System.Drawing.Point(4, 22);
         this.AeTitlesTabPage.Name = "AeTitlesTabPage";
         this.AeTitlesTabPage.Padding = new System.Windows.Forms.Padding(3);
         this.AeTitlesTabPage.Size = new System.Drawing.Size(531, 320);
         this.AeTitlesTabPage.TabIndex = 1;
         this.AeTitlesTabPage.Text = "Client Nodes";
         this.AeTitlesTabPage.UseVisualStyleBackColor = true;
         // 
         // ClientsToolStripContainer
         // 
         this.ClientsToolStripContainer.BottomToolStripPanelVisible = false;
         // 
         // ClientsToolStripContainer.ContentPanel
         // 
         this.ClientsToolStripContainer.ContentPanel.Controls.Add(this.AeTitlesListView);
         this.ClientsToolStripContainer.ContentPanel.Size = new System.Drawing.Size(525, 275);
         this.ClientsToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
         this.ClientsToolStripContainer.LeftToolStripPanelVisible = false;
         this.ClientsToolStripContainer.Location = new System.Drawing.Point(3, 3);
         this.ClientsToolStripContainer.Name = "ClientsToolStripContainer";
         this.ClientsToolStripContainer.RightToolStripPanelVisible = false;
         this.ClientsToolStripContainer.Size = new System.Drawing.Size(525, 314);
         this.ClientsToolStripContainer.TabIndex = 4;
         this.ClientsToolStripContainer.Text = "toolStripContainer1";
         // 
         // ClientsToolStripContainer.TopToolStripPanel
         // 
         this.ClientsToolStripContainer.TopToolStripPanel.Controls.Add(this.AeTitleToolStrip);
         // 
         // AeTitlesListView
         // 
         this.AeTitlesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.AeTitleColumnHeader,
            this.HostnameColumnHeader,
            this.PortColumnHeader,
            this.SecurePortColumnHeader,
            this.PortUsageHeader});
         this.AeTitlesListView.Dock = System.Windows.Forms.DockStyle.Fill;
         this.AeTitlesListView.FullRowSelect = true;
         this.AeTitlesListView.GridLines = true;
         this.AeTitlesListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
         this.AeTitlesListView.HideSelection = false;
         this.AeTitlesListView.Location = new System.Drawing.Point(0, 0);
         this.AeTitlesListView.MultiSelect = false;
         this.AeTitlesListView.Name = "AeTitlesListView";
         this.AeTitlesListView.Size = new System.Drawing.Size(525, 275);
         this.AeTitlesListView.TabIndex = 0;
         this.AeTitlesListView.UseCompatibleStateImageBehavior = false;
         this.AeTitlesListView.View = System.Windows.Forms.View.Details;
         // 
         // AeTitleColumnHeader
         // 
         this.AeTitleColumnHeader.Text = "AE Title";
         this.AeTitleColumnHeader.Width = 103;
         // 
         // HostnameColumnHeader
         // 
         this.HostnameColumnHeader.Text = "Address/Host Name";
         this.HostnameColumnHeader.Width = 118;
         // 
         // PortColumnHeader
         // 
         this.PortColumnHeader.Text = "Port";
         this.PortColumnHeader.Width = 42;
         // 
         // SecurePortColumnHeader
         // 
         this.SecurePortColumnHeader.Text = "Secure Port";
         this.SecurePortColumnHeader.Width = 76;
         // 
         // AeTitleToolStrip
         // 
         this.AeTitleToolStrip.Dock = System.Windows.Forms.DockStyle.None;
         this.AeTitleToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
         this.AeTitleToolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
         this.AeTitleToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddAeTitleToolStripButton,
            this.EditAeTitleToolStripButton,
            this.DeleteAeTitleToolStripButton});
         this.AeTitleToolStrip.Location = new System.Drawing.Point(0, 0);
         this.AeTitleToolStrip.Name = "AeTitleToolStrip";
         this.AeTitleToolStrip.Size = new System.Drawing.Size(525, 39);
         this.AeTitleToolStrip.Stretch = true;
         this.AeTitleToolStrip.TabIndex = 0;
         // 
         // WorkstationDisplayNameLabel
         // 
         this.WorkstationDisplayNameLabel.AutoSize = true;
         this.WorkstationDisplayNameLabel.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
         this.WorkstationDisplayNameLabel.Location = new System.Drawing.Point(3, 43);
         this.WorkstationDisplayNameLabel.Name = "WorkstationDisplayNameLabel";
         this.WorkstationDisplayNameLabel.Size = new System.Drawing.Size(0, 17);
         this.WorkstationDisplayNameLabel.TabIndex = 1;
         // 
         // PortUsageHeader
         // 
         this.PortUsageHeader.Text = "Port Usage";
         this.PortUsageHeader.Width = 97;
         // 
         // ServerSecurePictureBox
         // 
         this.ServerSecurePictureBox.Image = ((System.Drawing.Image)(resources.GetObject("ServerSecurePictureBox.Image")));
         this.ServerSecurePictureBox.Location = new System.Drawing.Point(74, 68);
         this.ServerSecurePictureBox.Name = "ServerSecurePictureBox";
         this.ServerSecurePictureBox.Size = new System.Drawing.Size(16, 16);
         this.ServerSecurePictureBox.TabIndex = 9;
         this.ServerSecurePictureBox.TabStop = false;
         this.ServerSecurePictureBox.Visible = false;
         // 
         // AddAeTitleToolStripButton
         // 
         this.AddAeTitleToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.AddAeTitleToolStripButton.Image = global::Leadtools.Demos.Workstation.Properties.Resources.client_add_32;
         this.AddAeTitleToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.AddAeTitleToolStripButton.Name = "AddAeTitleToolStripButton";
         this.AddAeTitleToolStripButton.Size = new System.Drawing.Size(36, 36);
         this.AddAeTitleToolStripButton.Text = "Add Client";
         // 
         // EditAeTitleToolStripButton
         // 
         this.EditAeTitleToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.EditAeTitleToolStripButton.Image = global::Leadtools.Demos.Workstation.Properties.Resources.client_edit_32;
         this.EditAeTitleToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.EditAeTitleToolStripButton.Name = "EditAeTitleToolStripButton";
         this.EditAeTitleToolStripButton.Size = new System.Drawing.Size(36, 36);
         this.EditAeTitleToolStripButton.Text = "Update Client";
         // 
         // DeleteAeTitleToolStripButton
         // 
         this.DeleteAeTitleToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.DeleteAeTitleToolStripButton.Image = global::Leadtools.Demos.Workstation.Properties.Resources.client_remove_32;
         this.DeleteAeTitleToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.DeleteAeTitleToolStripButton.Name = "DeleteAeTitleToolStripButton";
         this.DeleteAeTitleToolStripButton.Size = new System.Drawing.Size(36, 36);
         this.DeleteAeTitleToolStripButton.Text = "Delete Client";
         // 
         // EditServerToolStripButton
         // 
         this.EditServerToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.EditServerToolStripButton.Image = global::Leadtools.Demos.Workstation.Properties.Resources.server_edit_32;
         this.EditServerToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.EditServerToolStripButton.Name = "EditServerToolStripButton";
         this.EditServerToolStripButton.Size = new System.Drawing.Size(36, 36);
         this.EditServerToolStripButton.Text = "toolStripButton2";
         this.EditServerToolStripButton.ToolTipText = "Edit Server";
         // 
         // ToolStripButtonStart
         // 
         this.ToolStripButtonStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.ToolStripButtonStart.Image = global::Leadtools.Demos.Workstation.Properties.Resources.server_start_32;
         this.ToolStripButtonStart.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.ToolStripButtonStart.Name = "ToolStripButtonStart";
         this.ToolStripButtonStart.Size = new System.Drawing.Size(36, 36);
         this.ToolStripButtonStart.Text = "toolStripButton4";
         this.ToolStripButtonStart.ToolTipText = "Start Server";
         // 
         // PauseToolStripButton
         // 
         this.PauseToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.PauseToolStripButton.Image = global::Leadtools.Demos.Workstation.Properties.Resources.server_pause_32;
         this.PauseToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.PauseToolStripButton.Name = "PauseToolStripButton";
         this.PauseToolStripButton.Size = new System.Drawing.Size(36, 36);
         this.PauseToolStripButton.Text = "toolStripButton5";
         this.PauseToolStripButton.ToolTipText = "Pause Server";
         // 
         // StopToolStripButton
         // 
         this.StopToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.StopToolStripButton.Image = global::Leadtools.Demos.Workstation.Properties.Resources.server_stop_32;
         this.StopToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.StopToolStripButton.Name = "StopToolStripButton";
         this.StopToolStripButton.Size = new System.Drawing.Size(36, 36);
         this.StopToolStripButton.Text = "Stop";
         this.StopToolStripButton.ToolTipText = "Stop Server";
         // 
         // AddServerToolStripButton
         // 
         this.AddServerToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.AddServerToolStripButton.Image = global::Leadtools.Demos.Workstation.Properties.Resources.server_add_32;
         this.AddServerToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.AddServerToolStripButton.Name = "AddServerToolStripButton";
         this.AddServerToolStripButton.Size = new System.Drawing.Size(36, 36);
         this.AddServerToolStripButton.Text = "Add Server";
         this.AddServerToolStripButton.ToolTipText = "Add Server";
         // 
         // DeleteServerToolStripButton
         // 
         this.DeleteServerToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.DeleteServerToolStripButton.Image = global::Leadtools.Demos.Workstation.Properties.Resources.server_delete_32;
         this.DeleteServerToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.DeleteServerToolStripButton.Name = "DeleteServerToolStripButton";
         this.DeleteServerToolStripButton.Size = new System.Drawing.Size(36, 36);
         this.DeleteServerToolStripButton.Text = "Delete Server";
         this.DeleteServerToolStripButton.ToolTipText = "Delete Server";
         // 
         // EventLogViewerToolStripButton
         // 
         this.EventLogViewerToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.EventLogViewerToolStripButton.Image = global::Leadtools.Demos.Workstation.Properties.Resources.EventLogViewer;
         this.EventLogViewerToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.EventLogViewerToolStripButton.Name = "EventLogViewerToolStripButton";
         this.EventLogViewerToolStripButton.Size = new System.Drawing.Size(36, 36);
         this.EventLogViewerToolStripButton.Text = "Event Log Viewer";
         this.EventLogViewerToolStripButton.ToolTipText = "Event Log Viewer";
         // 
         // StorageListenerService
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
         this.Controls.Add(this.ServerSecurePictureBox);
         this.Controls.Add(this.WorkstationDisplayNameLabel);
         this.Controls.Add(this.ServiceTabControl);
         this.Controls.Add(this.LEADStorageServiceAELabel);
         this.Controls.Add(this.IpAddressLabel);
         this.Controls.Add(this.ServerPortLabel);
         this.Controls.Add(this.IpLabel);
         this.Controls.Add(this.PortLabel);
         this.Controls.Add(this.ServiceATitleLabel);
         this.Controls.Add(this.MainToolStrip);
         this.ForeColor = System.Drawing.Color.White;
         this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
         this.Name = "StorageListenerService";
         this.Size = new System.Drawing.Size(552, 488);
         this.MainToolStrip.ResumeLayout(false);
         this.MainToolStrip.PerformLayout();
         this.ServiceTabControl.ResumeLayout(false);
         this.AeTitlesTabPage.ResumeLayout(false);
         this.ClientsToolStripContainer.ContentPanel.ResumeLayout(false);
         this.ClientsToolStripContainer.TopToolStripPanel.ResumeLayout(false);
         this.ClientsToolStripContainer.TopToolStripPanel.PerformLayout();
         this.ClientsToolStripContainer.ResumeLayout(false);
         this.ClientsToolStripContainer.PerformLayout();
         this.AeTitleToolStrip.ResumeLayout(false);
         this.AeTitleToolStrip.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.ServerSecurePictureBox)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      public System.Windows.Forms.ToolStripButton EditServerToolStripButton;
      public System.Windows.Forms.ToolStripButton ToolStripButtonStart;
      public System.Windows.Forms.ToolStripButton PauseToolStripButton;
      public System.Windows.Forms.ToolStripButton StopToolStripButton;
      public System.Windows.Forms.ToolStripButton AddServerToolStripButton;
      public System.Windows.Forms.ToolStripButton DeleteServerToolStripButton;
      public System.Windows.Forms.ToolStrip MainToolStrip;
      public System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
      public System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
      public System.Windows.Forms.Label ServiceATitleLabel;
      public System.Windows.Forms.Label IpAddressLabel;
      public System.Windows.Forms.Label ServerPortLabel;
      public System.Windows.Forms.Label IpLabel;
      public System.Windows.Forms.Label PortLabel;
      public System.Windows.Forms.Label LEADStorageServiceAELabel;
      public System.Windows.Forms.TabControl ServiceTabControl;
      public System.Windows.Forms.TabPage AeTitlesTabPage;
      public System.Windows.Forms.ToolStripContainer ClientsToolStripContainer;
      public System.Windows.Forms.ListView AeTitlesListView;
      public System.Windows.Forms.ColumnHeader AeTitleColumnHeader;
      public System.Windows.Forms.ColumnHeader HostnameColumnHeader;
      public System.Windows.Forms.ColumnHeader PortColumnHeader;
      public System.Windows.Forms.ColumnHeader SecurePortColumnHeader;
      public System.Windows.Forms.ToolStrip AeTitleToolStrip;
      public System.Windows.Forms.ToolStripButton AddAeTitleToolStripButton;
      public System.Windows.Forms.ToolStripButton EditAeTitleToolStripButton;
      public System.Windows.Forms.ToolStripButton DeleteAeTitleToolStripButton;
      public System.Windows.Forms.Label WorkstationDisplayNameLabel;
      public System.Windows.Forms.ToolStripSeparator StartAddInsToolStripSeparator;
      public System.Windows.Forms.ToolStripButton EventLogViewerToolStripButton;
      private System.Windows.Forms.ToolStripSeparator toolStripButton1;
      private System.Windows.Forms.PictureBox ServerSecurePictureBox;
      private System.Windows.Forms.ColumnHeader PortUsageHeader;
   }
}
