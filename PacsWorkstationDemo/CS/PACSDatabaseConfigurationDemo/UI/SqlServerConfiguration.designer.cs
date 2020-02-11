namespace MedicalWorkstationConfigurationDemo.UI
{
   partial class SqlServerConfiguration
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
         this.textBoxPartner = new System.Windows.Forms.TextBox();
         this.groupBox1 = new System.Windows.Forms.GroupBox();
         this.groupBoxSqlMirroring = new System.Windows.Forms.GroupBox();
         this.label5 = new System.Windows.Forms.Label();
         this.userInfoPanel = new System.Windows.Forms.Panel();
         this.passwordTextBox = new System.Windows.Forms.TextBox();
         this.userNameTextBox = new System.Windows.Forms.TextBox();
         this.label4 = new System.Windows.Forms.Label();
         this.label3 = new System.Windows.Forms.Label();
         this.sqlAuthenticationRadioButton = new System.Windows.Forms.RadioButton();
         this.windowsAuthenticationRadioButton = new System.Windows.Forms.RadioButton();
         this.label2 = new System.Windows.Forms.Label();
         this.panel1 = new System.Windows.Forms.Panel();
         this.labelDataSource = new System.Windows.Forms.Label();
         this.refreshServersButton = new System.Windows.Forms.Button();
         this.serverNameCoboBox = new System.Windows.Forms.ComboBox();
         this.panel2 = new System.Windows.Forms.Panel();
         this.serverDatabaseComboBox = new System.Windows.Forms.ComboBox();
         this.labelDatabase = new System.Windows.Forms.Label();
         this.groupBox1.SuspendLayout();
         this.groupBoxSqlMirroring.SuspendLayout();
         this.userInfoPanel.SuspendLayout();
         this.panel1.SuspendLayout();
         this.panel2.SuspendLayout();
         this.SuspendLayout();
         // 
         // textBoxPartner
         // 
         this.textBoxPartner.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.textBoxPartner.Location = new System.Drawing.Point(108, 29);
         this.textBoxPartner.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
         this.textBoxPartner.Name = "textBoxPartner";
         this.textBoxPartner.Size = new System.Drawing.Size(351, 20);
         this.textBoxPartner.TabIndex = 1;
         // 
         // groupBox1
         // 
         this.groupBox1.Controls.Add(this.groupBoxSqlMirroring);
         this.groupBox1.Controls.Add(this.userInfoPanel);
         this.groupBox1.Controls.Add(this.sqlAuthenticationRadioButton);
         this.groupBox1.Controls.Add(this.windowsAuthenticationRadioButton);
         this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.groupBox1.Location = new System.Drawing.Point(0, 43);
         this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
         this.groupBox1.Name = "groupBox1";
         this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
         this.groupBox1.Size = new System.Drawing.Size(488, 197);
         this.groupBox1.TabIndex = 1;
         this.groupBox1.TabStop = false;
         this.groupBox1.Text = "Log On to the server:";
         // 
         // groupBoxSqlMirroring
         // 
         this.groupBoxSqlMirroring.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.groupBoxSqlMirroring.Controls.Add(this.label5);
         this.groupBoxSqlMirroring.Controls.Add(this.textBoxPartner);
         this.groupBoxSqlMirroring.Location = new System.Drawing.Point(6, 128);
         this.groupBoxSqlMirroring.Name = "groupBoxSqlMirroring";
         this.groupBoxSqlMirroring.Size = new System.Drawing.Size(473, 63);
         this.groupBoxSqlMirroring.TabIndex = 2;
         this.groupBoxSqlMirroring.TabStop = false;
         this.groupBoxSqlMirroring.Text = "Sql Mirroring";
         // 
         // label5
         // 
         this.label5.AutoSize = true;
         this.label5.Location = new System.Drawing.Point(6, 32);
         this.label5.Name = "label5";
         this.label5.Size = new System.Drawing.Size(84, 13);
         this.label5.TabIndex = 0;
         this.label5.Text = "Failover Partner:";
         // 
         // userInfoPanel
         // 
         this.userInfoPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.userInfoPanel.Controls.Add(this.passwordTextBox);
         this.userInfoPanel.Controls.Add(this.userNameTextBox);
         this.userInfoPanel.Controls.Add(this.label4);
         this.userInfoPanel.Controls.Add(this.label3);
         this.userInfoPanel.Location = new System.Drawing.Point(18, 65);
         this.userInfoPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
         this.userInfoPanel.Name = "userInfoPanel";
         this.userInfoPanel.Size = new System.Drawing.Size(461, 50);
         this.userInfoPanel.TabIndex = 2;
         // 
         // passwordTextBox
         // 
         this.passwordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.passwordTextBox.Location = new System.Drawing.Point(96, 28);
         this.passwordTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
         this.passwordTextBox.Name = "passwordTextBox";
         this.passwordTextBox.PasswordChar = '*';
         this.passwordTextBox.Size = new System.Drawing.Size(351, 20);
         this.passwordTextBox.TabIndex = 3;
         // 
         // userNameTextBox
         // 
         this.userNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.userNameTextBox.Location = new System.Drawing.Point(96, 2);
         this.userNameTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
         this.userNameTextBox.Name = "userNameTextBox";
         this.userNameTextBox.Size = new System.Drawing.Size(351, 20);
         this.userNameTextBox.TabIndex = 1;
         // 
         // label4
         // 
         this.label4.AutoSize = true;
         this.label4.Location = new System.Drawing.Point(3, 31);
         this.label4.Name = "label4";
         this.label4.Size = new System.Drawing.Size(56, 13);
         this.label4.TabIndex = 2;
         this.label4.Text = "&Password:";
         // 
         // label3
         // 
         this.label3.AutoSize = true;
         this.label3.Location = new System.Drawing.Point(3, 5);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(61, 13);
         this.label3.TabIndex = 0;
         this.label3.Text = "&User name:";
         // 
         // sqlAuthenticationRadioButton
         // 
         this.sqlAuthenticationRadioButton.AutoSize = true;
         this.sqlAuthenticationRadioButton.Location = new System.Drawing.Point(5, 47);
         this.sqlAuthenticationRadioButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
         this.sqlAuthenticationRadioButton.Name = "sqlAuthenticationRadioButton";
         this.sqlAuthenticationRadioButton.Size = new System.Drawing.Size(173, 17);
         this.sqlAuthenticationRadioButton.TabIndex = 1;
         this.sqlAuthenticationRadioButton.TabStop = true;
         this.sqlAuthenticationRadioButton.Text = "Use S&QL Server Authentication";
         this.sqlAuthenticationRadioButton.UseVisualStyleBackColor = true;
         // 
         // windowsAuthenticationRadioButton
         // 
         this.windowsAuthenticationRadioButton.AutoSize = true;
         this.windowsAuthenticationRadioButton.Checked = true;
         this.windowsAuthenticationRadioButton.Location = new System.Drawing.Point(5, 25);
         this.windowsAuthenticationRadioButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
         this.windowsAuthenticationRadioButton.Name = "windowsAuthenticationRadioButton";
         this.windowsAuthenticationRadioButton.Size = new System.Drawing.Size(165, 17);
         this.windowsAuthenticationRadioButton.TabIndex = 0;
         this.windowsAuthenticationRadioButton.TabStop = true;
         this.windowsAuthenticationRadioButton.Text = "Use &Windows Authentication ";
         this.windowsAuthenticationRadioButton.UseVisualStyleBackColor = true;
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(3, 0);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(70, 13);
         this.label2.TabIndex = 1;
         this.label2.Text = "S&erver name:";
         // 
         // panel1
         // 
         this.panel1.Controls.Add(this.labelDataSource);
         this.panel1.Controls.Add(this.refreshServersButton);
         this.panel1.Controls.Add(this.serverNameCoboBox);
         this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
         this.panel1.Location = new System.Drawing.Point(0, 0);
         this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
         this.panel1.Name = "panel1";
         this.panel1.Size = new System.Drawing.Size(488, 43);
         this.panel1.TabIndex = 0;
         // 
         // labelDataSource
         // 
         this.labelDataSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.labelDataSource.AutoSize = true;
         this.labelDataSource.Location = new System.Drawing.Point(375, 12);
         this.labelDataSource.Name = "labelDataSource";
         this.labelDataSource.Size = new System.Drawing.Size(110, 13);
         this.labelDataSource.TabIndex = 2;
         this.labelDataSource.Text = "<= Enter Data Source";
         // 
         // refreshServersButton
         // 
         this.refreshServersButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.refreshServersButton.Location = new System.Drawing.Point(400, 8);
         this.refreshServersButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
         this.refreshServersButton.Name = "refreshServersButton";
         this.refreshServersButton.Size = new System.Drawing.Size(81, 23);
         this.refreshServersButton.TabIndex = 1;
         this.refreshServersButton.Text = "&Refresh";
         this.refreshServersButton.UseVisualStyleBackColor = true;
         // 
         // serverNameCoboBox
         // 
         this.serverNameCoboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.serverNameCoboBox.FormattingEnabled = true;
         this.serverNameCoboBox.Location = new System.Drawing.Point(6, 9);
         this.serverNameCoboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
         this.serverNameCoboBox.Name = "serverNameCoboBox";
         this.serverNameCoboBox.Size = new System.Drawing.Size(363, 21);
         this.serverNameCoboBox.TabIndex = 0;
         // 
         // panel2
         // 
         this.panel2.Controls.Add(this.serverDatabaseComboBox);
         this.panel2.Controls.Add(this.labelDatabase);
         this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.panel2.Location = new System.Drawing.Point(0, 240);
         this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
         this.panel2.Name = "panel2";
         this.panel2.Size = new System.Drawing.Size(488, 55);
         this.panel2.TabIndex = 2;
         // 
         // serverDatabaseComboBox
         // 
         this.serverDatabaseComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.serverDatabaseComboBox.FormattingEnabled = true;
         this.serverDatabaseComboBox.Location = new System.Drawing.Point(10, 20);
         this.serverDatabaseComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
         this.serverDatabaseComboBox.Name = "serverDatabaseComboBox";
         this.serverDatabaseComboBox.Size = new System.Drawing.Size(455, 21);
         this.serverDatabaseComboBox.TabIndex = 1;
         // 
         // labelDatabase
         // 
         this.labelDatabase.AutoSize = true;
         this.labelDatabase.Location = new System.Drawing.Point(8, 3);
         this.labelDatabase.Name = "labelDatabase";
         this.labelDatabase.Size = new System.Drawing.Size(56, 13);
         this.labelDatabase.TabIndex = 0;
         this.labelDatabase.Text = "&Database:";
         // 
         // SqlServerConfiguration
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.groupBox1);
         this.Controls.Add(this.panel1);
         this.Controls.Add(this.panel2);
         this.Controls.Add(this.label2);
         this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
         this.Name = "SqlServerConfiguration";
         this.Size = new System.Drawing.Size(488, 295);
         this.groupBox1.ResumeLayout(false);
         this.groupBox1.PerformLayout();
         this.groupBoxSqlMirroring.ResumeLayout(false);
         this.groupBoxSqlMirroring.PerformLayout();
         this.userInfoPanel.ResumeLayout(false);
         this.userInfoPanel.PerformLayout();
         this.panel1.ResumeLayout(false);
         this.panel1.PerformLayout();
         this.panel2.ResumeLayout(false);
         this.panel2.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.RadioButton sqlAuthenticationRadioButton;
      private System.Windows.Forms.RadioButton windowsAuthenticationRadioButton;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Panel userInfoPanel;
      private System.Windows.Forms.TextBox passwordTextBox;
      private System.Windows.Forms.TextBox userNameTextBox;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.Panel panel2;
      private System.Windows.Forms.ComboBox serverDatabaseComboBox;
      private System.Windows.Forms.Label labelDatabase;
      private System.Windows.Forms.Button refreshServersButton;
      private System.Windows.Forms.ComboBox serverNameCoboBox;
      private System.Windows.Forms.GroupBox groupBoxSqlMirroring;
      private System.Windows.Forms.Label label5;
      private System.Windows.Forms.TextBox textBoxPartner;
      private System.Windows.Forms.Label labelDataSource;
   }
}
