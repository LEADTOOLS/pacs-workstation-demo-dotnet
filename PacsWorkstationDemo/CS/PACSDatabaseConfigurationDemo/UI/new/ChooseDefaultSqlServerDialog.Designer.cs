namespace CSPacsDatabaseConfigurationDemo.UI.New
{
   partial class ChooseDefaultSqlServerDialog
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
         this.buttonOk = new System.Windows.Forms.Button();
         this.panel1 = new System.Windows.Forms.Panel();
         this.HeaderDescriptionLabel2 = new System.Windows.Forms.Label();
         this.HeaderDescriptionLabel1 = new System.Windows.Forms.Label();
         this.groupBox2 = new System.Windows.Forms.GroupBox();
         this.buttonCancel = new System.Windows.Forms.Button();
         this.buttonValidate = new System.Windows.Forms.Button();
         this.labelValidate = new System.Windows.Forms.Label();
         this.groupBoxDataSource = new System.Windows.Forms.GroupBox();
         this.labelNotRecommended = new System.Windows.Forms.Label();
         this.labelRecommended = new System.Windows.Forms.Label();
         this.radioButtonSqlServerCompact = new System.Windows.Forms.RadioButton();
         this.radioButtonSqlServer = new System.Windows.Forms.RadioButton();
         this.notRecommendedWarning = new CSPacsDatabaseConfigurationDemo.UI.New.NotRecommendedWarning();
         this.sqlServerConfiguration1 = new MedicalWorkstationConfigurationDemo.UI.SqlServerConfiguration();
         this.panel1.SuspendLayout();
         this.groupBoxDataSource.SuspendLayout();
         this.SuspendLayout();
         // 
         // buttonOk
         // 
         this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.buttonOk.Location = new System.Drawing.Point(439, 474);
         this.buttonOk.Name = "buttonOk";
         this.buttonOk.Size = new System.Drawing.Size(75, 23);
         this.buttonOk.TabIndex = 6;
         this.buttonOk.Text = "&OK";
         this.buttonOk.UseVisualStyleBackColor = true;
         this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
         // 
         // panel1
         // 
         this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.panel1.BackColor = System.Drawing.Color.White;
         this.panel1.Controls.Add(this.HeaderDescriptionLabel2);
         this.panel1.Controls.Add(this.HeaderDescriptionLabel1);
         this.panel1.Controls.Add(this.groupBox2);
         this.panel1.Location = new System.Drawing.Point(0, 0);
         this.panel1.Name = "panel1";
         this.panel1.Size = new System.Drawing.Size(603, 95);
         this.panel1.TabIndex = 0;
         // 
         // HeaderDescriptionLabel2
         // 
         this.HeaderDescriptionLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.HeaderDescriptionLabel2.ForeColor = System.Drawing.Color.SteelBlue;
         this.HeaderDescriptionLabel2.Location = new System.Drawing.Point(12, 45);
         this.HeaderDescriptionLabel2.Name = "HeaderDescriptionLabel2";
         this.HeaderDescriptionLabel2.Size = new System.Drawing.Size(578, 44);
         this.HeaderDescriptionLabel2.TabIndex = 1;
         this.HeaderDescriptionLabel2.Text = "Select a SQL Server instance from the list below to be used as the default, and c" +
             "lick \'Validate\'.  This can be changed on the next screen.";
         this.HeaderDescriptionLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // HeaderDescriptionLabel1
         // 
         this.HeaderDescriptionLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.HeaderDescriptionLabel1.ForeColor = System.Drawing.Color.SteelBlue;
         this.HeaderDescriptionLabel1.Location = new System.Drawing.Point(12, 7);
         this.HeaderDescriptionLabel1.Name = "HeaderDescriptionLabel1";
         this.HeaderDescriptionLabel1.Size = new System.Drawing.Size(579, 36);
         this.HeaderDescriptionLabel1.TabIndex = 0;
         this.HeaderDescriptionLabel1.Text = "This application is used to configure the PACS Framework databases.";
         this.HeaderDescriptionLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // groupBox2
         // 
         this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.groupBox2.Location = new System.Drawing.Point(0, 92);
         this.groupBox2.Name = "groupBox2";
         this.groupBox2.Size = new System.Drawing.Size(603, 3);
         this.groupBox2.TabIndex = 2;
         this.groupBox2.TabStop = false;
         this.groupBox2.Text = "groupBox2";
         // 
         // buttonCancel
         // 
         this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.buttonCancel.Location = new System.Drawing.Point(516, 474);
         this.buttonCancel.Name = "buttonCancel";
         this.buttonCancel.Size = new System.Drawing.Size(75, 23);
         this.buttonCancel.TabIndex = 7;
         this.buttonCancel.Text = "Cancel";
         this.buttonCancel.UseVisualStyleBackColor = true;
         // 
         // buttonValidate
         // 
         this.buttonValidate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.buttonValidate.Location = new System.Drawing.Point(15, 475);
         this.buttonValidate.Name = "buttonValidate";
         this.buttonValidate.Size = new System.Drawing.Size(75, 23);
         this.buttonValidate.TabIndex = 4;
         this.buttonValidate.Text = "Validate";
         this.buttonValidate.UseVisualStyleBackColor = true;
         this.buttonValidate.Click += new System.EventHandler(this.buttonValidate_Click);
         // 
         // labelValidate
         // 
         this.labelValidate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.labelValidate.AutoSize = true;
         this.labelValidate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.labelValidate.ForeColor = System.Drawing.Color.Green;
         this.labelValidate.Location = new System.Drawing.Point(96, 480);
         this.labelValidate.Name = "labelValidate";
         this.labelValidate.Size = new System.Drawing.Size(228, 13);
         this.labelValidate.TabIndex = 5;
         this.labelValidate.Text = "<== Click \'Validate\' and then click \'OK\'";
         // 
         // groupBoxDataSource
         // 
         this.groupBoxDataSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.groupBoxDataSource.Controls.Add(this.labelNotRecommended);
         this.groupBoxDataSource.Controls.Add(this.labelRecommended);
         this.groupBoxDataSource.Controls.Add(this.radioButtonSqlServerCompact);
         this.groupBoxDataSource.Controls.Add(this.radioButtonSqlServer);
         this.groupBoxDataSource.Location = new System.Drawing.Point(4, 102);
         this.groupBoxDataSource.Name = "groupBoxDataSource";
         this.groupBoxDataSource.Size = new System.Drawing.Size(599, 66);
         this.groupBoxDataSource.TabIndex = 1;
         this.groupBoxDataSource.TabStop = false;
         this.groupBoxDataSource.Text = "Data Source:";
         // 
         // labelNotRecommended
         // 
         this.labelNotRecommended.AutoSize = true;
         this.labelNotRecommended.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.labelNotRecommended.ForeColor = System.Drawing.Color.Red;
         this.labelNotRecommended.Location = new System.Drawing.Point(147, 45);
         this.labelNotRecommended.Name = "labelNotRecommended";
         this.labelNotRecommended.Size = new System.Drawing.Size(439, 13);
         this.labelNotRecommended.TabIndex = 3;
         this.labelNotRecommended.Text = "Not Recommended:  HTML5 Medical Viewer cannot be used with this option";
         // 
         // labelRecommended
         // 
         this.labelRecommended.AutoSize = true;
         this.labelRecommended.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.labelRecommended.ForeColor = System.Drawing.Color.Green;
         this.labelRecommended.Location = new System.Drawing.Point(148, 21);
         this.labelRecommended.Name = "labelRecommended";
         this.labelRecommended.Size = new System.Drawing.Size(374, 13);
         this.labelRecommended.TabIndex = 1;
         this.labelRecommended.Text = "Recommended: All toolkit features available, optimized for speed";
         // 
         // radioButtonSqlServerCompact
         // 
         this.radioButtonSqlServerCompact.AutoSize = true;
         this.radioButtonSqlServerCompact.Location = new System.Drawing.Point(4, 43);
         this.radioButtonSqlServerCompact.Name = "radioButtonSqlServerCompact";
         this.radioButtonSqlServerCompact.Size = new System.Drawing.Size(143, 17);
         this.radioButtonSqlServerCompact.TabIndex = 2;
         this.radioButtonSqlServerCompact.TabStop = true;
         this.radioButtonSqlServerCompact.Text = "SQL Server Compact 3.5";
         this.radioButtonSqlServerCompact.UseVisualStyleBackColor = true;
         // 
         // radioButtonSqlServer
         // 
         this.radioButtonSqlServer.AutoSize = true;
         this.radioButtonSqlServer.Location = new System.Drawing.Point(4, 19);
         this.radioButtonSqlServer.Name = "radioButtonSqlServer";
         this.radioButtonSqlServer.Size = new System.Drawing.Size(80, 17);
         this.radioButtonSqlServer.TabIndex = 0;
         this.radioButtonSqlServer.TabStop = true;
         this.radioButtonSqlServer.Text = "SQL Server";
         this.radioButtonSqlServer.UseVisualStyleBackColor = true;
         // 
         // notRecommendedWarning
         // 
         this.notRecommendedWarning.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.notRecommendedWarning.Location = new System.Drawing.Point(472, 213);
         this.notRecommendedWarning.Name = "notRecommendedWarning";
         this.notRecommendedWarning.Size = new System.Drawing.Size(89, 118);
         this.notRecommendedWarning.TabIndex = 3;
         // 
         // sqlServerConfiguration1
         // 
         this.sqlServerConfiguration1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                     | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.sqlServerConfiguration1.ConnectionString = "Data Source=;Failover Partner=;Initial Catalog=;Integrated Security=True;User ID=" +
             ";Password=;Pooling=True";
         this.sqlServerConfiguration1.Cursor = System.Windows.Forms.Cursors.Default;
         this.sqlServerConfiguration1.DatabaseVisible = true;
         this.sqlServerConfiguration1.DefaultDatabaseName = null;
         this.sqlServerConfiguration1.Location = new System.Drawing.Point(0, 165);
         this.sqlServerConfiguration1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
         this.sqlServerConfiguration1.Mode = MedicalWorkstationConfigurationDemo.UI.DbConfigurationMode.Configure;
         this.sqlServerConfiguration1.Name = "sqlServerConfiguration1";
         this.sqlServerConfiguration1.Size = new System.Drawing.Size(600, 296);
         this.sqlServerConfiguration1.SqlMirroringVisible = true;
         this.sqlServerConfiguration1.TabIndex = 2;
         // 
         // ChooseDefaultSqlServerDialog
         // 
         this.AcceptButton = this.buttonOk;
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.CancelButton = this.buttonCancel;
         this.ClientSize = new System.Drawing.Size(603, 509);
         this.Controls.Add(this.buttonCancel);
         this.Controls.Add(this.buttonOk);
         this.Controls.Add(this.notRecommendedWarning);
         this.Controls.Add(this.groupBoxDataSource);
         this.Controls.Add(this.labelValidate);
         this.Controls.Add(this.buttonValidate);
         this.Controls.Add(this.panel1);
         this.Controls.Add(this.sqlServerConfiguration1);
         this.Name = "ChooseDefaultSqlServerDialog";
         this.Text = "Choose Default SQL Server Instance";
         this.Load += new System.EventHandler(this.ChooseDefaultSqlServerDialog_Load);
         this.panel1.ResumeLayout(false);
         this.groupBoxDataSource.ResumeLayout(false);
         this.groupBoxDataSource.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private MedicalWorkstationConfigurationDemo.UI.SqlServerConfiguration sqlServerConfiguration1;
      private System.Windows.Forms.Button buttonOk;
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.Label HeaderDescriptionLabel1;
      private System.Windows.Forms.GroupBox groupBox2;
      private System.Windows.Forms.Label HeaderDescriptionLabel2;
      private System.Windows.Forms.Button buttonCancel;
      private System.Windows.Forms.Button buttonValidate;
      private System.Windows.Forms.Label labelValidate;
      private System.Windows.Forms.GroupBox groupBoxDataSource;
      private System.Windows.Forms.RadioButton radioButtonSqlServerCompact;
      private System.Windows.Forms.RadioButton radioButtonSqlServer;
      private System.Windows.Forms.Label labelNotRecommended;
      private System.Windows.Forms.Label labelRecommended;
      private NotRecommendedWarning notRecommendedWarning;
   }
}