namespace MedicalWorkstationConfigurationDemo.UI
{
   partial class MainForm
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
         this.splitContainer1 = new System.Windows.Forms.SplitContainer();
         this.connectionSummary1 = new MedicalWorkstationConfigurationDemo.UI.ConnectionSummary();
         this.connectionConfiguration1 = new MedicalWorkstationConfigurationDemo.UI.ConnectionConfiguration();
         this.databaseOptions1 = new MedicalWorkstationConfigurationDemo.UI.DatabaseOptions();
         this.panel1 = new System.Windows.Forms.Panel();
         this.HeaderDescriptionLabel = new System.Windows.Forms.Label();
         this.groupBox2 = new System.Windows.Forms.GroupBox();
         this.groupBox1 = new System.Windows.Forms.GroupBox();
         this.ButtonBack = new System.Windows.Forms.Button();
         this.ButtonNext = new System.Windows.Forms.Button();
         this.ButtonCancel = new System.Windows.Forms.Button();
         this.splitContainer1.Panel1.SuspendLayout();
         this.splitContainer1.Panel2.SuspendLayout();
         this.splitContainer1.SuspendLayout();
         this.panel1.SuspendLayout();
         this.SuspendLayout();
         // 
         // splitContainer1
         // 
         this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.splitContainer1.IsSplitterFixed = true;
         this.splitContainer1.Location = new System.Drawing.Point(0, 0);
         this.splitContainer1.Name = "splitContainer1";
         this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
         // 
         // splitContainer1.Panel1
         // 
         this.splitContainer1.Panel1.Controls.Add(this.connectionSummary1);
         this.splitContainer1.Panel1.Controls.Add(this.connectionConfiguration1);
         this.splitContainer1.Panel1.Controls.Add(this.databaseOptions1);
         this.splitContainer1.Panel1.Controls.Add(this.panel1);
         // 
         // splitContainer1.Panel2
         // 
         this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
         this.splitContainer1.Panel2.Controls.Add(this.ButtonBack);
         this.splitContainer1.Panel2.Controls.Add(this.ButtonNext);
         this.splitContainer1.Panel2.Controls.Add(this.ButtonCancel);
         this.splitContainer1.Size = new System.Drawing.Size(711, 563);
         this.splitContainer1.SplitterDistance = 515;
         this.splitContainer1.SplitterWidth = 3;
         this.splitContainer1.TabIndex = 0;
         // 
         // connectionSummary1
         // 
         this.connectionSummary1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.connectionSummary1.Location = new System.Drawing.Point(0, 76);
         this.connectionSummary1.Name = "connectionSummary1";
         this.connectionSummary1.Padding = new System.Windows.Forms.Padding(5);
         this.connectionSummary1.Size = new System.Drawing.Size(711, 439);
         this.connectionSummary1.Summary = "";
         this.connectionSummary1.TabIndex = 3;
         this.connectionSummary1.Visible = false;
         // 
         // connectionConfiguration1
         // 
         this.connectionConfiguration1.CanChangeProvider = true;
         this.connectionConfiguration1.DefaultSqlCeDatabaseName = null;
         this.connectionConfiguration1.DefaultSqlServerDatabaseName = null;
         this.connectionConfiguration1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.connectionConfiguration1.Location = new System.Drawing.Point(0, 76);
         this.connectionConfiguration1.Mode = MedicalWorkstationConfigurationDemo.UI.DbConfigurationMode.Configure;
         this.connectionConfiguration1.Name = "connectionConfiguration1";
         this.connectionConfiguration1.Size = new System.Drawing.Size(711, 439);
         this.connectionConfiguration1.TabIndex = 0;
         // 
         // databaseOptions1
         // 
         this.databaseOptions1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.databaseOptions1.Location = new System.Drawing.Point(0, 76);
         this.databaseOptions1.LoggingDbSelected = false;
         this.databaseOptions1.MediaCreationDbSelected = false;
         this.databaseOptions1.Mode = MedicalWorkstationConfigurationDemo.UI.DbConfigurationMode.Create;
         this.databaseOptions1.Name = "databaseOptions1";
         this.databaseOptions1.PatientUpdaterDbSelected = false;
         this.databaseOptions1.Size = new System.Drawing.Size(711, 439);
         this.databaseOptions1.StorageDbSelected = false;
         this.databaseOptions1.StorageServerDbSelected = false;
         this.databaseOptions1.StorageServerOptionsDbSelected = false;
         this.databaseOptions1.SupportedDatabases = MedicalWorkstationConfigurationDemo.DatabaseComponents.None;
         this.databaseOptions1.TabIndex = 1;
         this.databaseOptions1.UserManagementDbSelected = false;
         this.databaseOptions1.WorklistDbSelected = true;
         this.databaseOptions1.WorkstationDbSelected = true;
         this.databaseOptions1.WorkstationUserName = "";
         this.databaseOptions1.WorkstationUserPassword = "";
         // 
         // panel1
         // 
         this.panel1.BackColor = System.Drawing.Color.White;
         this.panel1.Controls.Add(this.HeaderDescriptionLabel);
         this.panel1.Controls.Add(this.groupBox2);
         this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
         this.panel1.Location = new System.Drawing.Point(0, 0);
         this.panel1.Name = "panel1";
         this.panel1.Size = new System.Drawing.Size(711, 76);
         this.panel1.TabIndex = 4;
         // 
         // HeaderDescriptionLabel
         // 
         this.HeaderDescriptionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.HeaderDescriptionLabel.ForeColor = System.Drawing.Color.SteelBlue;
         this.HeaderDescriptionLabel.Location = new System.Drawing.Point(12, 16);
         this.HeaderDescriptionLabel.Name = "HeaderDescriptionLabel";
         this.HeaderDescriptionLabel.Size = new System.Drawing.Size(687, 46);
         this.HeaderDescriptionLabel.TabIndex = 2;
         this.HeaderDescriptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // groupBox2
         // 
         this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.groupBox2.Location = new System.Drawing.Point(0, 73);
         this.groupBox2.Name = "groupBox2";
         this.groupBox2.Size = new System.Drawing.Size(711, 3);
         this.groupBox2.TabIndex = 1;
         this.groupBox2.TabStop = false;
         this.groupBox2.Text = "groupBox2";
         // 
         // groupBox1
         // 
         this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
         this.groupBox1.Location = new System.Drawing.Point(0, 0);
         this.groupBox1.Name = "groupBox1";
         this.groupBox1.Size = new System.Drawing.Size(711, 3);
         this.groupBox1.TabIndex = 4;
         this.groupBox1.TabStop = false;
         // 
         // ButtonBack
         // 
         this.ButtonBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.ButtonBack.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.ButtonBack.Enabled = false;
         this.ButtonBack.Location = new System.Drawing.Point(431, 8);
         this.ButtonBack.Name = "ButtonBack";
         this.ButtonBack.Size = new System.Drawing.Size(75, 23);
         this.ButtonBack.TabIndex = 2;
         this.ButtonBack.Text = "Back";
         this.ButtonBack.UseVisualStyleBackColor = true;
         // 
         // ButtonNext
         // 
         this.ButtonNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.ButtonNext.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.ButtonNext.Location = new System.Drawing.Point(512, 8);
         this.ButtonNext.Name = "ButtonNext";
         this.ButtonNext.Size = new System.Drawing.Size(75, 23);
         this.ButtonNext.TabIndex = 1;
         this.ButtonNext.Text = "Next";
         this.ButtonNext.UseVisualStyleBackColor = true;
         // 
         // ButtonCancel
         // 
         this.ButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.ButtonCancel.Location = new System.Drawing.Point(629, 8);
         this.ButtonCancel.Name = "ButtonCancel";
         this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
         this.ButtonCancel.TabIndex = 0;
         this.ButtonCancel.Text = "Cancel";
         this.ButtonCancel.UseVisualStyleBackColor = true;
         this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
         // 
         // MainForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.CancelButton = this.ButtonCancel;
         this.ClientSize = new System.Drawing.Size(711, 563);
         this.Controls.Add(this.splitContainer1);
         this.MaximizeBox = false;
         this.Name = "MainForm";
         this.ShowIcon = false;
         this.Text = "Database Configuration Wizard";
         this.splitContainer1.Panel1.ResumeLayout(false);
         this.splitContainer1.Panel2.ResumeLayout(false);
         this.splitContainer1.ResumeLayout(false);
         this.panel1.ResumeLayout(false);
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.SplitContainer splitContainer1;
      private System.Windows.Forms.Button ButtonCancel;
      private System.Windows.Forms.Button ButtonBack;
      private System.Windows.Forms.Button ButtonNext;
      private ConnectionConfiguration connectionConfiguration1;
      private DatabaseOptions databaseOptions1;
      private System.Windows.Forms.GroupBox groupBox1;
      private ConnectionSummary connectionSummary1;
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.GroupBox groupBox2;
      private System.Windows.Forms.Label HeaderDescriptionLabel;
   }
}