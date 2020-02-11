namespace PACSConfigDemo
{
   partial class RemoveDialog
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
         this.toolStripMenuItemRemoveAll = new System.Windows.Forms.ToolStripMenuItem();
         this.buttonClose = new System.Windows.Forms.Button();
         this.labelMessage = new System.Windows.Forms.Label();
         this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
         this.labelError = new System.Windows.Forms.Label();
         this.toolStripMenuItemRemoveChecked = new System.Windows.Forms.ToolStripMenuItem();
         this.buttonRemoveChecked = new System.Windows.Forms.Button();
         this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
         this.copyPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.columnHeaderPath = new System.Windows.Forms.ColumnHeader();
         this.listViewServers = new System.Windows.Forms.ListView();
         this.columnHeaderName = new System.Windows.Forms.ColumnHeader();
         this.columnHeaderPort = new System.Windows.Forms.ColumnHeader();
         this.columnHeaderStatus = new System.Windows.Forms.ColumnHeader();
         this.buttonRemoveAll = new System.Windows.Forms.Button();
         this.toolTip = new System.Windows.Forms.ToolTip(this.components);
         this.labelRemoveChecked = new System.Windows.Forms.Label();
         this.labelRemoveAll = new System.Windows.Forms.Label();
         ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
         this.contextMenuStrip.SuspendLayout();
         this.SuspendLayout();
         // 
         // toolStripMenuItemRemoveAll
         // 
         this.toolStripMenuItemRemoveAll.Name = "toolStripMenuItemRemoveAll";
         this.toolStripMenuItemRemoveAll.Size = new System.Drawing.Size(166, 22);
         this.toolStripMenuItemRemoveAll.Text = "Remove &All";
         this.toolStripMenuItemRemoveAll.Click += new System.EventHandler(this.toolStripMenuItemRemoveAll_Click);
         // 
         // buttonClose
         // 
         this.buttonClose.Location = new System.Drawing.Point(13, 380);
         this.buttonClose.Name = "buttonClose";
         this.buttonClose.Size = new System.Drawing.Size(106, 23);
         this.buttonClose.TabIndex = 12;
         this.buttonClose.Text = "&Close";
         this.buttonClose.UseVisualStyleBackColor = true;
         this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
         // 
         // labelMessage
         // 
         this.labelMessage.ForeColor = System.Drawing.Color.Blue;
         this.labelMessage.Location = new System.Drawing.Point(13, 265);
         this.labelMessage.Name = "labelMessage";
         this.labelMessage.Size = new System.Drawing.Size(561, 28);
         this.labelMessage.TabIndex = 11;
         this.labelMessage.Text = "labelMessage";
         // 
         // errorProvider
         // 
         this.errorProvider.ContainerControl = this;
         // 
         // labelError
         // 
         this.labelError.ForeColor = System.Drawing.Color.Red;
         this.labelError.Location = new System.Drawing.Point(13, 230);
         this.labelError.Name = "labelError";
         this.labelError.Size = new System.Drawing.Size(561, 31);
         this.labelError.TabIndex = 10;
         this.labelError.Text = "labelError";
         // 
         // toolStripMenuItemRemoveChecked
         // 
         this.toolStripMenuItemRemoveChecked.Name = "toolStripMenuItemRemoveChecked";
         this.toolStripMenuItemRemoveChecked.Size = new System.Drawing.Size(166, 22);
         this.toolStripMenuItemRemoveChecked.Text = "Remove &Checked";
         this.toolStripMenuItemRemoveChecked.Click += new System.EventHandler(this.toolStripMenuItemRemoveChecked_Click);
         // 
         // buttonRemoveChecked
         // 
         this.buttonRemoveChecked.Location = new System.Drawing.Point(13, 310);
         this.buttonRemoveChecked.Name = "buttonRemoveChecked";
         this.buttonRemoveChecked.Size = new System.Drawing.Size(106, 23);
         this.buttonRemoveChecked.TabIndex = 7;
         this.buttonRemoveChecked.Text = "Remove &Checked";
         this.buttonRemoveChecked.UseVisualStyleBackColor = true;
         this.buttonRemoveChecked.Click += new System.EventHandler(this.buttonRemoveChecked_Click);
         // 
         // contextMenuStrip
         // 
         this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemRemoveChecked,
            this.toolStripMenuItemRemoveAll,
            this.copyPathToolStripMenuItem});
         this.contextMenuStrip.Name = "contextMenuStrip";
         this.contextMenuStrip.Size = new System.Drawing.Size(167, 70);
         // 
         // copyPathToolStripMenuItem
         // 
         this.copyPathToolStripMenuItem.Name = "copyPathToolStripMenuItem";
         this.copyPathToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
         this.copyPathToolStripMenuItem.Text = "&Copy Path";
         this.copyPathToolStripMenuItem.Click += new System.EventHandler(this.copyPathToolStripMenuItem_Click);
         // 
         // columnHeaderPath
         // 
         this.columnHeaderPath.Text = "Path";
         // 
         // listViewServers
         // 
         this.listViewServers.CheckBoxes = true;
         this.listViewServers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderPort,
            this.columnHeaderStatus,
            this.columnHeaderPath});
         this.listViewServers.ContextMenuStrip = this.contextMenuStrip;
         this.listViewServers.FullRowSelect = true;
         this.listViewServers.GridLines = true;
         this.listViewServers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
         this.listViewServers.Location = new System.Drawing.Point(13, 12);
         this.listViewServers.MultiSelect = false;
         this.listViewServers.Name = "listViewServers";
         this.listViewServers.Size = new System.Drawing.Size(578, 206);
         this.listViewServers.TabIndex = 9;
         this.listViewServers.UseCompatibleStateImageBehavior = false;
         this.listViewServers.View = System.Windows.Forms.View.Details;
         this.listViewServers.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewServers_ItemChecked);
         this.listViewServers.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listViewServers_ItemSelectionChanged);
         // 
         // columnHeaderName
         // 
         this.columnHeaderName.Text = "Name";
         // 
         // columnHeaderPort
         // 
         this.columnHeaderPort.Text = "Port";
         // 
         // columnHeaderStatus
         // 
         this.columnHeaderStatus.Text = "Status";
         // 
         // buttonRemoveAll
         // 
         this.buttonRemoveAll.Location = new System.Drawing.Point(13, 345);
         this.buttonRemoveAll.Name = "buttonRemoveAll";
         this.buttonRemoveAll.Size = new System.Drawing.Size(106, 23);
         this.buttonRemoveAll.TabIndex = 8;
         this.buttonRemoveAll.Text = "Remove &All";
         this.buttonRemoveAll.UseVisualStyleBackColor = true;
         this.buttonRemoveAll.Click += new System.EventHandler(this.buttonRemoveAll_Click);
         // 
         // labelRemoveChecked
         // 
         this.labelRemoveChecked.AutoSize = true;
         this.labelRemoveChecked.Location = new System.Drawing.Point(144, 315);
         this.labelRemoveChecked.Name = "labelRemoveChecked";
         this.labelRemoveChecked.Size = new System.Drawing.Size(112, 13);
         this.labelRemoveChecked.TabIndex = 13;
         this.labelRemoveChecked.Text = "labelRemoveChecked";
         // 
         // labelRemoveAll
         // 
         this.labelRemoveAll.AutoSize = true;
         this.labelRemoveAll.Location = new System.Drawing.Point(144, 350);
         this.labelRemoveAll.Name = "labelRemoveAll";
         this.labelRemoveAll.Size = new System.Drawing.Size(80, 13);
         this.labelRemoveAll.TabIndex = 14;
         this.labelRemoveAll.Text = "labelRemoveAll";
         // 
         // RemoveDialog
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(603, 415);
         this.Controls.Add(this.labelRemoveAll);
         this.Controls.Add(this.labelRemoveChecked);
         this.Controls.Add(this.labelMessage);
         this.Controls.Add(this.buttonClose);
         this.Controls.Add(this.labelError);
         this.Controls.Add(this.listViewServers);
         this.Controls.Add(this.buttonRemoveChecked);
         this.Controls.Add(this.buttonRemoveAll);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "RemoveDialog";
         this.ShowIcon = false;
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
         this.Text = "Remove DICOM Server Services";
         this.Load += new System.EventHandler(this.RemoveDialog_Load);
         ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
         this.contextMenuStrip.ResumeLayout(false);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRemoveAll;
      private System.Windows.Forms.Button buttonClose;
      private System.Windows.Forms.Label labelMessage;
      private System.Windows.Forms.ErrorProvider errorProvider;
      private System.Windows.Forms.Label labelError;
      private System.Windows.Forms.Button buttonRemoveChecked;
      private System.Windows.Forms.ListView listViewServers;
      private System.Windows.Forms.ColumnHeader columnHeaderName;
      private System.Windows.Forms.ColumnHeader columnHeaderPort;
      private System.Windows.Forms.ColumnHeader columnHeaderStatus;
      private System.Windows.Forms.ColumnHeader columnHeaderPath;
      private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
      private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRemoveChecked;
      private System.Windows.Forms.Button buttonRemoveAll;
      private System.Windows.Forms.ToolTip toolTip;
      private System.Windows.Forms.ToolStripMenuItem copyPathToolStripMenuItem;
      private System.Windows.Forms.Label labelRemoveAll;
      private System.Windows.Forms.Label labelRemoveChecked;
   }
}