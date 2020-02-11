namespace MedicalWorkstationConfigurationDemo.UI
{
   partial class ConnectionConfiguration
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
         this.panel2 = new System.Windows.Forms.Panel();
         this.panel1 = new System.Windows.Forms.Panel();
         this.txtDataSource = new System.Windows.Forms.TextBox();
         this.label2 = new System.Windows.Forms.Label();
         this.label1 = new System.Windows.Forms.Label();
         this.btnChange = new System.Windows.Forms.Button();
         this.btnHelp = new System.Windows.Forms.Button();
         this.notRecommendedWarning = new CSPacsDatabaseConfigurationDemo.UI.New.NotRecommendedWarning();
         this.sqlServerConfiguration = new MedicalWorkstationConfigurationDemo.UI.SqlServerConfiguration();
         this.sqlCeConfiguration = new MedicalWorkstationConfigurationDemo.UI.SqlCeConfiguration();
         this.panel2.SuspendLayout();
         this.panel1.SuspendLayout();
         this.SuspendLayout();
         // 
         // panel2
         // 
         this.panel2.Controls.Add(this.notRecommendedWarning);
         this.panel2.Controls.Add(this.sqlServerConfiguration);
         this.panel2.Controls.Add(this.sqlCeConfiguration);
         this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel2.Location = new System.Drawing.Point(0, 119);
         this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
         this.panel2.Name = "panel2";
         this.panel2.Size = new System.Drawing.Size(571, 259);
         this.panel2.TabIndex = 1;
         // 
         // panel1
         // 
         this.panel1.Controls.Add(this.btnHelp);
         this.panel1.Controls.Add(this.txtDataSource);
         this.panel1.Controls.Add(this.btnChange);
         this.panel1.Controls.Add(this.label2);
         this.panel1.Controls.Add(this.label1);
         this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
         this.panel1.Location = new System.Drawing.Point(0, 0);
         this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
         this.panel1.Name = "panel1";
         this.panel1.Size = new System.Drawing.Size(571, 119);
         this.panel1.TabIndex = 3;
         // 
         // txtDataSource
         // 
         this.txtDataSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.txtDataSource.Location = new System.Drawing.Point(6, 88);
         this.txtDataSource.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
         this.txtDataSource.Name = "txtDataSource";
         this.txtDataSource.ReadOnly = true;
         this.txtDataSource.Size = new System.Drawing.Size(446, 20);
         this.txtDataSource.TabIndex = 8;
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(3, 67);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(70, 13);
         this.label2.TabIndex = 6;
         this.label2.Text = "&Data Source:";
         // 
         // label1
         // 
         this.label1.Location = new System.Drawing.Point(3, 13);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(469, 50);
         this.label1.TabIndex = 5;
         this.label1.Text = "Enter information to connect to the selected data source or click \"Change\" to cho" +
    "ose a different data source.  Click the \'Help\' button if you are connecting to a" +
    " Microsoft Azure SQL Server.";
         // 
         // btnChange
         // 
         this.btnChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.btnChange.Location = new System.Drawing.Point(483, 87);
         this.btnChange.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
         this.btnChange.Name = "btnChange";
         this.btnChange.Size = new System.Drawing.Size(81, 23);
         this.btnChange.TabIndex = 7;
         this.btnChange.Text = "&Change...";
         this.btnChange.UseVisualStyleBackColor = true;
         // 
         // btnHelp
         // 
         this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.btnHelp.Location = new System.Drawing.Point(483, 48);
         this.btnHelp.Name = "btnHelp";
         this.btnHelp.Size = new System.Drawing.Size(81, 23);
         this.btnHelp.TabIndex = 9;
         this.btnHelp.Text = "&Help";
         this.btnHelp.UseVisualStyleBackColor = true;
         // 
         // notRecommendedWarning
         // 
         this.notRecommendedWarning.Location = new System.Drawing.Point(421, 73);
         this.notRecommendedWarning.Name = "notRecommendedWarning";
         this.notRecommendedWarning.Size = new System.Drawing.Size(110, 91);
         this.notRecommendedWarning.TabIndex = 2;
         // 
         // sqlServerConfiguration
         // 
         this.sqlServerConfiguration.ConnectionString = "Data Source=;Failover Partner=;Initial Catalog=;Integrated Security=True;User ID=" +
    ";Password=;Pooling=True";
         this.sqlServerConfiguration.Cursor = System.Windows.Forms.Cursors.Default;
         this.sqlServerConfiguration.DatabaseVisible = true;
         this.sqlServerConfiguration.DefaultDatabaseName = null;
         this.sqlServerConfiguration.Dock = System.Windows.Forms.DockStyle.Fill;
         this.sqlServerConfiguration.Location = new System.Drawing.Point(0, 0);
         this.sqlServerConfiguration.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
         this.sqlServerConfiguration.Mode = MedicalWorkstationConfigurationDemo.UI.DbConfigurationMode.Configure;
         this.sqlServerConfiguration.Name = "sqlServerConfiguration";
         this.sqlServerConfiguration.Size = new System.Drawing.Size(571, 259);
         this.sqlServerConfiguration.SqlMirroringVisible = true;
         this.sqlServerConfiguration.TabIndex = 0;
         // 
         // sqlCeConfiguration
         // 
         this.sqlCeConfiguration.ConnectionString = "";
         this.sqlCeConfiguration.DefaultSqlCeDatabaseName = null;
         this.sqlCeConfiguration.Dock = System.Windows.Forms.DockStyle.Fill;
         this.sqlCeConfiguration.Location = new System.Drawing.Point(0, 0);
         this.sqlCeConfiguration.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
         this.sqlCeConfiguration.Mode = MedicalWorkstationConfigurationDemo.UI.DbConfigurationMode.Configure;
         this.sqlCeConfiguration.Name = "sqlCeConfiguration";
         this.sqlCeConfiguration.Size = new System.Drawing.Size(571, 259);
         this.sqlCeConfiguration.TabIndex = 1;
         // 
         // ConnectionConfiguration
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.panel2);
         this.Controls.Add(this.panel1);
         this.Name = "ConnectionConfiguration";
         this.Size = new System.Drawing.Size(571, 378);
         this.Load += new System.EventHandler(this.ConnectionConfiguration_Load);
         this.panel2.ResumeLayout(false);
         this.panel1.ResumeLayout(false);
         this.panel1.PerformLayout();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.Panel panel2;
      private SqlCeConfiguration sqlCeConfiguration;
      private SqlServerConfiguration sqlServerConfiguration;
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.TextBox txtDataSource;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Label label1;
      private CSPacsDatabaseConfigurationDemo.UI.New.NotRecommendedWarning notRecommendedWarning;
      private System.Windows.Forms.Button btnHelp;
      private System.Windows.Forms.Button btnChange;
   }
}
