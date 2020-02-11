namespace CSPacsDatabaseConfigurationDemo.UI
{
   partial class MainForm2
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

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.components = new System.ComponentModel.Container();
         System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("");
         this.panel3 = new System.Windows.Forms.Panel();
         this.buttonClose = new System.Windows.Forms.Button();
         this.labelConfigure = new System.Windows.Forms.Label();
         this.listViewStatus = new System.Windows.Forms.ListView();
         this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         this.groupBox1 = new System.Windows.Forms.GroupBox();
         this.btnApplyConfiguration = new System.Windows.Forms.Button();
         this.panel1 = new System.Windows.Forms.Panel();
         this.HeaderDescriptionLabel = new System.Windows.Forms.Label();
         this.groupBox2 = new System.Windows.Forms.GroupBox();
         this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
         this.clearStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.searchAllSQLServersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.panel2 = new System.Windows.Forms.Panel();
         this.mainOptions1 = new CSPacsDatabaseConfigurationDemo.UI.New.MainOptions();
         this.addDefaultImagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.panel3.SuspendLayout();
         this.panel1.SuspendLayout();
         this.contextMenuStrip.SuspendLayout();
         this.panel2.SuspendLayout();
         this.SuspendLayout();
         // 
         // panel3
         // 
         this.panel3.Controls.Add(this.buttonClose);
         this.panel3.Controls.Add(this.labelConfigure);
         this.panel3.Controls.Add(this.listViewStatus);
         this.panel3.Controls.Add(this.groupBox1);
         this.panel3.Controls.Add(this.btnApplyConfiguration);
         this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel3.Location = new System.Drawing.Point(0, 0);
         this.panel3.Name = "panel3";
         this.panel3.Size = new System.Drawing.Size(694, 724);
         this.panel3.TabIndex = 1;
         // 
         // buttonClose
         // 
         this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.buttonClose.Location = new System.Drawing.Point(616, 696);
         this.buttonClose.Name = "buttonClose";
         this.buttonClose.Size = new System.Drawing.Size(75, 23);
         this.buttonClose.TabIndex = 12;
         this.buttonClose.Text = "&Close";
         this.buttonClose.UseVisualStyleBackColor = true;
         // 
         // labelConfigure
         // 
         this.labelConfigure.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.labelConfigure.AutoSize = true;
         this.labelConfigure.Location = new System.Drawing.Point(99, 701);
         this.labelConfigure.Name = "labelConfigure";
         this.labelConfigure.Size = new System.Drawing.Size(74, 13);
         this.labelConfigure.TabIndex = 7;
         this.labelConfigure.Text = "labelConfigure";
         // 
         // listViewStatus
         // 
         this.listViewStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.listViewStatus.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
         this.listViewStatus.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
         this.listViewStatus.Location = new System.Drawing.Point(4, 488);
         this.listViewStatus.Name = "listViewStatus";
         this.listViewStatus.Size = new System.Drawing.Size(687, 202);
         this.listViewStatus.TabIndex = 5;
         this.listViewStatus.UseCompatibleStateImageBehavior = false;
         this.listViewStatus.View = System.Windows.Forms.View.Details;
         // 
         // columnHeader1
         // 
         this.columnHeader1.Text = "Status";
         this.columnHeader1.Width = 600;
         // 
         // groupBox1
         // 
         this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
         this.groupBox1.Location = new System.Drawing.Point(0, 0);
         this.groupBox1.Name = "groupBox1";
         this.groupBox1.Size = new System.Drawing.Size(694, 10);
         this.groupBox1.TabIndex = 2;
         this.groupBox1.TabStop = false;
         // 
         // btnApplyConfiguration
         // 
         this.btnApplyConfiguration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.btnApplyConfiguration.Location = new System.Drawing.Point(4, 696);
         this.btnApplyConfiguration.Name = "btnApplyConfiguration";
         this.btnApplyConfiguration.Size = new System.Drawing.Size(88, 23);
         this.btnApplyConfiguration.TabIndex = 1;
         this.btnApplyConfiguration.Text = "Configure";
         this.btnApplyConfiguration.UseVisualStyleBackColor = true;
         // 
         // panel1
         // 
         this.panel1.BackColor = System.Drawing.Color.White;
         this.panel1.Controls.Add(this.HeaderDescriptionLabel);
         this.panel1.Controls.Add(this.groupBox2);
         this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
         this.panel1.Location = new System.Drawing.Point(0, 0);
         this.panel1.Name = "panel1";
         this.panel1.Size = new System.Drawing.Size(694, 60);
         this.panel1.TabIndex = 5;
         // 
         // HeaderDescriptionLabel
         // 
         this.HeaderDescriptionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.HeaderDescriptionLabel.ForeColor = System.Drawing.Color.SteelBlue;
         this.HeaderDescriptionLabel.Location = new System.Drawing.Point(12, 9);
         this.HeaderDescriptionLabel.Name = "HeaderDescriptionLabel";
         this.HeaderDescriptionLabel.Size = new System.Drawing.Size(531, 41);
         this.HeaderDescriptionLabel.TabIndex = 2;
         this.HeaderDescriptionLabel.Text = "This application is used to configure the PACS Framework databases.";
         this.HeaderDescriptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // groupBox2
         // 
         this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.groupBox2.Location = new System.Drawing.Point(0, 57);
         this.groupBox2.Name = "groupBox2";
         this.groupBox2.Size = new System.Drawing.Size(694, 3);
         this.groupBox2.TabIndex = 1;
         this.groupBox2.TabStop = false;
         this.groupBox2.Text = "groupBox2";
         // 
         // contextMenuStrip
         // 
         this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearStatusToolStripMenuItem,
            this.searchAllSQLServersToolStripMenuItem,
            this.addDefaultImagesToolStripMenuItem});
         this.contextMenuStrip.Name = "contextMenuStrip";
         this.contextMenuStrip.Size = new System.Drawing.Size(189, 92);
         // 
         // clearStatusToolStripMenuItem
         // 
         this.clearStatusToolStripMenuItem.Name = "clearStatusToolStripMenuItem";
         this.clearStatusToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
         this.clearStatusToolStripMenuItem.Text = "Clear Status";
         this.clearStatusToolStripMenuItem.Click += new System.EventHandler(this.clearStatusToolStripMenuItem_Click);
         // 
         // searchAllSQLServersToolStripMenuItem
         // 
         this.searchAllSQLServersToolStripMenuItem.Name = "searchAllSQLServersToolStripMenuItem";
         this.searchAllSQLServersToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
         this.searchAllSQLServersToolStripMenuItem.Text = "Search all SQL Servers";
         this.searchAllSQLServersToolStripMenuItem.Click += new System.EventHandler(this.searchAllSQLServersToolStripMenuItem_Click);
         // 
         // panel2
         // 
         this.panel2.AutoSize = true;
         this.panel2.Controls.Add(this.mainOptions1);
         this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
         this.panel2.Location = new System.Drawing.Point(0, 60);
         this.panel2.Name = "panel2";
         this.panel2.Size = new System.Drawing.Size(694, 423);
         this.panel2.TabIndex = 2;

         // 
         // addDefaultImagesToolStripMenuItem
         // 
         this.addDefaultImagesToolStripMenuItem.Name = "addDefaultImagesToolStripMenuItem";
         this.addDefaultImagesToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
         this.addDefaultImagesToolStripMenuItem.Text = "Add Default Images";
         // 

         // 
         // mainOptions1
         // 
         this.mainOptions1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.mainOptions1.ConfigGlobalPacs = null;
         this.mainOptions1.ConfigMachine = null;
         this.mainOptions1.DefaultSqlConnectionStringBuilder = new System.Data.SqlClient.SqlConnectionStringBuilder("");
         this.mainOptions1.LabelCreateNote = "Newly created databases overwrite older databases of the same name.";
         this.mainOptions1.Location = new System.Drawing.Point(3, 0);
         this.mainOptions1.Name = "mainOptions1";
         this.mainOptions1.Size = new System.Drawing.Size(682, 420);
         this.mainOptions1.SupportedDatabases = ((MedicalWorkstationConfigurationDemo.DatabaseComponents)(((MedicalWorkstationConfigurationDemo.DatabaseComponents.Worklist | MedicalWorkstationConfigurationDemo.DatabaseComponents.StorageServer) 
            | MedicalWorkstationConfigurationDemo.DatabaseComponents.MedicalWorkstation)));
         this.mainOptions1.TabIndex = 0;
         // 
         // MainForm2
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(694, 724);
         this.ContextMenuStrip = this.contextMenuStrip;
         this.Controls.Add(this.panel2);
         this.Controls.Add(this.panel1);
         this.Controls.Add(this.panel3);
         this.Name = "MainForm2";
         this.Text = "Database Configuration Application";
         this.panel3.ResumeLayout(false);
         this.panel3.PerformLayout();
         this.panel1.ResumeLayout(false);
         this.contextMenuStrip.ResumeLayout(false);
         this.panel2.ResumeLayout(false);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Panel panel3;
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.Label HeaderDescriptionLabel;
      private System.Windows.Forms.GroupBox groupBox2;
      private System.Windows.Forms.Button btnApplyConfiguration;
      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.ListView listViewStatus;
      private System.Windows.Forms.ColumnHeader columnHeader1;
      private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
      private System.Windows.Forms.ToolStripMenuItem clearStatusToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem searchAllSQLServersToolStripMenuItem;
      private System.Windows.Forms.Button buttonClose;
      private System.Windows.Forms.Label labelConfigure;
      private New.MainOptions mainOptions1;
      private System.Windows.Forms.Panel panel2;
      private System.Windows.Forms.ToolStripMenuItem addDefaultImagesToolStripMenuItem;
   }
}