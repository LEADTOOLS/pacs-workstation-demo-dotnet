namespace MedicalWorkstationConfigurationDemo.UI
{
   partial class DatabaseOptions
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
         this.panel1 = new System.Windows.Forms.Panel();
         this.ConnectToDbRadioButton = new System.Windows.Forms.RadioButton();
         this.CreateNewDbRadioButton = new System.Windows.Forms.RadioButton();
         this.ConfigreDbDescLabel = new System.Windows.Forms.Label();
         this.NewDbOptionDescLabel = new System.Windows.Forms.Label();
         this.panel2 = new System.Windows.Forms.Panel();
         this.WorkstationUserPasswordConfirmTextBox = new System.Windows.Forms.TextBox();
         this.label2 = new System.Windows.Forms.Label();
         this.LoadBalancePanel = new System.Windows.Forms.Panel();
         this.ConnectToOptionsDatabaseRadioButton = new System.Windows.Forms.RadioButton();
         this.StorageServerOptionsDbCheckBox = new System.Windows.Forms.CheckBox();
         this.CreateOptionsDatabaseRadioButton = new System.Windows.Forms.RadioButton();
         this.PatientUpdaterDbCheckBox = new System.Windows.Forms.CheckBox();
         this.StorageServerDbCheckBox = new System.Windows.Forms.CheckBox();
         this.label1 = new System.Windows.Forms.Label();
         this.UserNameLabel = new System.Windows.Forms.Label();
         this.MediaCreationDbCheckBox = new System.Windows.Forms.CheckBox();
         this.ShowPasswordCheckBox = new System.Windows.Forms.CheckBox();
         this.WorkstationUserPasswordTextBox = new System.Windows.Forms.TextBox();
         this.WorkstationUserNameTextBox = new System.Windows.Forms.TextBox();
         this.WorkstationDbCheckBox = new System.Windows.Forms.CheckBox();
         this.UserManagementDbCheckBox = new System.Windows.Forms.CheckBox();
         this.WorklistDbCheckBox = new System.Windows.Forms.CheckBox();
         this.StorageDbCheckBox = new System.Windows.Forms.CheckBox();
         this.LoggingDbCheckBox = new System.Windows.Forms.CheckBox();
         this.panel1.SuspendLayout();
         this.panel2.SuspendLayout();
         this.LoadBalancePanel.SuspendLayout();
         this.SuspendLayout();
         // 
         // panel1
         // 
         this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.panel1.Controls.Add(this.ConnectToDbRadioButton);
         this.panel1.Controls.Add(this.CreateNewDbRadioButton);
         this.panel1.Controls.Add(this.ConfigreDbDescLabel);
         this.panel1.Controls.Add(this.NewDbOptionDescLabel);
         this.panel1.Location = new System.Drawing.Point(4, 7);
         this.panel1.Name = "panel1";
         this.panel1.Size = new System.Drawing.Size(522, 153);
         this.panel1.TabIndex = 0;
         // 
         // ConnectToDbRadioButton
         // 
         this.ConnectToDbRadioButton.AutoSize = true;
         this.ConnectToDbRadioButton.Location = new System.Drawing.Point(13, 117);
         this.ConnectToDbRadioButton.Name = "ConnectToDbRadioButton";
         this.ConnectToDbRadioButton.Size = new System.Drawing.Size(170, 17);
         this.ConnectToDbRadioButton.TabIndex = 3;
         this.ConnectToDbRadioButton.Text = "Connect to Existing Databases";
         this.ConnectToDbRadioButton.UseVisualStyleBackColor = true;
         // 
         // CreateNewDbRadioButton
         // 
         this.CreateNewDbRadioButton.AutoSize = true;
         this.CreateNewDbRadioButton.Checked = true;
         this.CreateNewDbRadioButton.Location = new System.Drawing.Point(13, 42);
         this.CreateNewDbRadioButton.Name = "CreateNewDbRadioButton";
         this.CreateNewDbRadioButton.Size = new System.Drawing.Size(135, 17);
         this.CreateNewDbRadioButton.TabIndex = 1;
         this.CreateNewDbRadioButton.TabStop = true;
         this.CreateNewDbRadioButton.Text = "Create New Databases";
         this.CreateNewDbRadioButton.UseVisualStyleBackColor = true;
         // 
         // ConfigreDbDescLabel
         // 
         this.ConfigreDbDescLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.ConfigreDbDescLabel.Location = new System.Drawing.Point(10, 77);
         this.ConfigreDbDescLabel.Name = "ConfigreDbDescLabel";
         this.ConfigreDbDescLabel.Size = new System.Drawing.Size(503, 38);
         this.ConfigreDbDescLabel.TabIndex = 2;
         this.ConfigreDbDescLabel.Text = "If you have created the databases before and would like to configure the componen" +
             "ts to connect to an existing database, select the “Connect to Existing Databases" +
             "” option.";
         // 
         // NewDbOptionDescLabel
         // 
         this.NewDbOptionDescLabel.AutoSize = true;
         this.NewDbOptionDescLabel.Location = new System.Drawing.Point(10, 13);
         this.NewDbOptionDescLabel.Name = "NewDbOptionDescLabel";
         this.NewDbOptionDescLabel.Size = new System.Drawing.Size(405, 13);
         this.NewDbOptionDescLabel.TabIndex = 0;
         this.NewDbOptionDescLabel.Text = "If this is the first time that you have used this wizard, select “Create New Data" +
             "bases”.";
         // 
         // panel2
         // 
         this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.panel2.Controls.Add(this.WorkstationUserPasswordConfirmTextBox);
         this.panel2.Controls.Add(this.label2);
         this.panel2.Controls.Add(this.LoadBalancePanel);
         this.panel2.Controls.Add(this.PatientUpdaterDbCheckBox);
         this.panel2.Controls.Add(this.StorageServerDbCheckBox);
         this.panel2.Controls.Add(this.label1);
         this.panel2.Controls.Add(this.UserNameLabel);
         this.panel2.Controls.Add(this.MediaCreationDbCheckBox);
         this.panel2.Controls.Add(this.ShowPasswordCheckBox);
         this.panel2.Controls.Add(this.WorkstationUserPasswordTextBox);
         this.panel2.Controls.Add(this.WorkstationUserNameTextBox);
         this.panel2.Controls.Add(this.WorkstationDbCheckBox);
         this.panel2.Controls.Add(this.UserManagementDbCheckBox);
         this.panel2.Controls.Add(this.WorklistDbCheckBox);
         this.panel2.Controls.Add(this.StorageDbCheckBox);
         this.panel2.Controls.Add(this.LoggingDbCheckBox);
         this.panel2.Location = new System.Drawing.Point(4, 166);
         this.panel2.Name = "panel2";
         this.panel2.Size = new System.Drawing.Size(522, 251);
         this.panel2.TabIndex = 1;
         // 
         // WorkstationUserPasswordConfirmTextBox
         // 
         this.WorkstationUserPasswordConfirmTextBox.Location = new System.Drawing.Point(371, 42);
         this.WorkstationUserPasswordConfirmTextBox.Name = "WorkstationUserPasswordConfirmTextBox";
         this.WorkstationUserPasswordConfirmTextBox.Size = new System.Drawing.Size(136, 20);
         this.WorkstationUserPasswordConfirmTextBox.TabIndex = 14;
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(275, 45);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(94, 13);
         this.label2.TabIndex = 13;
         this.label2.Text = "Confirm Password:";
         // 
         // LoadBalancePanel
         // 
         this.LoadBalancePanel.Controls.Add(this.ConnectToOptionsDatabaseRadioButton);
         this.LoadBalancePanel.Controls.Add(this.StorageServerOptionsDbCheckBox);
         this.LoadBalancePanel.Controls.Add(this.CreateOptionsDatabaseRadioButton);
         this.LoadBalancePanel.Location = new System.Drawing.Point(32, 21);
         this.LoadBalancePanel.Name = "LoadBalancePanel";
         this.LoadBalancePanel.Size = new System.Drawing.Size(203, 67);
         this.LoadBalancePanel.TabIndex = 1;
         // 
         // ConnectToOptionsDatabaseRadioButton
         // 
         this.ConnectToOptionsDatabaseRadioButton.AutoSize = true;
         this.ConnectToOptionsDatabaseRadioButton.Location = new System.Drawing.Point(20, 47);
         this.ConnectToOptionsDatabaseRadioButton.Name = "ConnectToOptionsDatabaseRadioButton";
         this.ConnectToOptionsDatabaseRadioButton.Size = new System.Drawing.Size(165, 17);
         this.ConnectToOptionsDatabaseRadioButton.TabIndex = 2;
         this.ConnectToOptionsDatabaseRadioButton.Text = "Connect to Options Database";
         this.ConnectToOptionsDatabaseRadioButton.UseVisualStyleBackColor = true;
         // 
         // StorageServerOptionsDbCheckBox
         // 
         this.StorageServerOptionsDbCheckBox.AutoSize = true;
         this.StorageServerOptionsDbCheckBox.Checked = true;
         this.StorageServerOptionsDbCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
         this.StorageServerOptionsDbCheckBox.Location = new System.Drawing.Point(3, 3);
         this.StorageServerOptionsDbCheckBox.Name = "StorageServerOptionsDbCheckBox";
         this.StorageServerOptionsDbCheckBox.Size = new System.Drawing.Size(92, 17);
         this.StorageServerOptionsDbCheckBox.TabIndex = 0;
         this.StorageServerOptionsDbCheckBox.Text = "Load Balance";
         this.StorageServerOptionsDbCheckBox.UseVisualStyleBackColor = true;
         // 
         // CreateOptionsDatabaseRadioButton
         // 
         this.CreateOptionsDatabaseRadioButton.AutoSize = true;
         this.CreateOptionsDatabaseRadioButton.Checked = true;
         this.CreateOptionsDatabaseRadioButton.Location = new System.Drawing.Point(20, 24);
         this.CreateOptionsDatabaseRadioButton.Name = "CreateOptionsDatabaseRadioButton";
         this.CreateOptionsDatabaseRadioButton.Size = new System.Drawing.Size(144, 17);
         this.CreateOptionsDatabaseRadioButton.TabIndex = 1;
         this.CreateOptionsDatabaseRadioButton.TabStop = true;
         this.CreateOptionsDatabaseRadioButton.Text = "Create Options Database";
         this.CreateOptionsDatabaseRadioButton.UseVisualStyleBackColor = true;
         // 
         // PatientUpdaterDbCheckBox
         // 
         this.PatientUpdaterDbCheckBox.AutoSize = true;
         this.PatientUpdaterDbCheckBox.Checked = true;
         this.PatientUpdaterDbCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
         this.PatientUpdaterDbCheckBox.Location = new System.Drawing.Point(13, 215);
         this.PatientUpdaterDbCheckBox.Name = "PatientUpdaterDbCheckBox";
         this.PatientUpdaterDbCheckBox.Size = new System.Drawing.Size(149, 17);
         this.PatientUpdaterDbCheckBox.TabIndex = 8;
         this.PatientUpdaterDbCheckBox.Text = "Patient Updater Database";
         this.PatientUpdaterDbCheckBox.UseVisualStyleBackColor = true;
         // 
         // StorageServerDbCheckBox
         // 
         this.StorageServerDbCheckBox.AutoSize = true;
         this.StorageServerDbCheckBox.Checked = true;
         this.StorageServerDbCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
         this.StorageServerDbCheckBox.Location = new System.Drawing.Point(13, 3);
         this.StorageServerDbCheckBox.Name = "StorageServerDbCheckBox";
         this.StorageServerDbCheckBox.Size = new System.Drawing.Size(97, 17);
         this.StorageServerDbCheckBox.TabIndex = 0;
         this.StorageServerDbCheckBox.Text = "Storage Server";
         this.StorageServerDbCheckBox.UseVisualStyleBackColor = true;
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(275, 25);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(56, 13);
         this.label1.TabIndex = 11;
         this.label1.Text = "Password:";
         // 
         // UserNameLabel
         // 
         this.UserNameLabel.AutoSize = true;
         this.UserNameLabel.Location = new System.Drawing.Point(275, 5);
         this.UserNameLabel.Name = "UserNameLabel";
         this.UserNameLabel.Size = new System.Drawing.Size(63, 13);
         this.UserNameLabel.TabIndex = 9;
         this.UserNameLabel.Text = "User Name:";
         // 
         // MediaCreationDbCheckBox
         // 
         this.MediaCreationDbCheckBox.AutoSize = true;
         this.MediaCreationDbCheckBox.Checked = true;
         this.MediaCreationDbCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
         this.MediaCreationDbCheckBox.Location = new System.Drawing.Point(13, 155);
         this.MediaCreationDbCheckBox.Name = "MediaCreationDbCheckBox";
         this.MediaCreationDbCheckBox.Size = new System.Drawing.Size(146, 17);
         this.MediaCreationDbCheckBox.TabIndex = 5;
         this.MediaCreationDbCheckBox.Text = "Media Creation Database";
         this.MediaCreationDbCheckBox.UseVisualStyleBackColor = true;
         // 
         // ShowPasswordCheckBox
         // 
         this.ShowPasswordCheckBox.AutoSize = true;
         this.ShowPasswordCheckBox.Checked = true;
         this.ShowPasswordCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
         this.ShowPasswordCheckBox.Location = new System.Drawing.Point(371, 84);
         this.ShowPasswordCheckBox.Name = "ShowPasswordCheckBox";
         this.ShowPasswordCheckBox.Size = new System.Drawing.Size(102, 17);
         this.ShowPasswordCheckBox.TabIndex = 15;
         this.ShowPasswordCheckBox.Text = "Show Password";
         this.ShowPasswordCheckBox.UseVisualStyleBackColor = true;
         // 
         // WorkstationUserPasswordTextBox
         // 
         this.WorkstationUserPasswordTextBox.Location = new System.Drawing.Point(371, 21);
         this.WorkstationUserPasswordTextBox.Name = "WorkstationUserPasswordTextBox";
         this.WorkstationUserPasswordTextBox.PasswordChar = '*';
         this.WorkstationUserPasswordTextBox.Size = new System.Drawing.Size(136, 20);
         this.WorkstationUserPasswordTextBox.TabIndex = 12;
         // 
         // WorkstationUserNameTextBox
         // 
         this.WorkstationUserNameTextBox.Location = new System.Drawing.Point(371, 1);
         this.WorkstationUserNameTextBox.Name = "WorkstationUserNameTextBox";
         this.WorkstationUserNameTextBox.Size = new System.Drawing.Size(136, 20);
         this.WorkstationUserNameTextBox.TabIndex = 10;
         // 
         // WorkstationDbCheckBox
         // 
         this.WorkstationDbCheckBox.AutoSize = true;
         this.WorkstationDbCheckBox.Checked = true;
         this.WorkstationDbCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
         this.WorkstationDbCheckBox.Location = new System.Drawing.Point(13, 175);
         this.WorkstationDbCheckBox.Name = "WorkstationDbCheckBox";
         this.WorkstationDbCheckBox.Size = new System.Drawing.Size(132, 17);
         this.WorkstationDbCheckBox.TabIndex = 6;
         this.WorkstationDbCheckBox.Text = "Workstation Database";
         this.WorkstationDbCheckBox.UseVisualStyleBackColor = true;
         // 
         // UserManagementDbCheckBox
         // 
         this.UserManagementDbCheckBox.AutoSize = true;
         this.UserManagementDbCheckBox.Checked = true;
         this.UserManagementDbCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
         this.UserManagementDbCheckBox.Location = new System.Drawing.Point(13, 195);
         this.UserManagementDbCheckBox.Name = "UserManagementDbCheckBox";
         this.UserManagementDbCheckBox.Size = new System.Drawing.Size(222, 17);
         this.UserManagementDbCheckBox.TabIndex = 7;
         this.UserManagementDbCheckBox.Text = "Workstation User Management Database";
         this.UserManagementDbCheckBox.UseVisualStyleBackColor = true;
         // 
         // WorklistDbCheckBox
         // 
         this.WorklistDbCheckBox.AutoSize = true;
         this.WorklistDbCheckBox.Checked = true;
         this.WorklistDbCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
         this.WorklistDbCheckBox.Location = new System.Drawing.Point(13, 135);
         this.WorklistDbCheckBox.Name = "WorklistDbCheckBox";
         this.WorklistDbCheckBox.Size = new System.Drawing.Size(154, 17);
         this.WorklistDbCheckBox.TabIndex = 4;
         this.WorklistDbCheckBox.Text = "DICOM Worklist Database";
         this.WorklistDbCheckBox.UseVisualStyleBackColor = true;
         // 
         // StorageDbCheckBox
         // 
         this.StorageDbCheckBox.AutoSize = true;
         this.StorageDbCheckBox.Checked = true;
         this.StorageDbCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
         this.StorageDbCheckBox.Location = new System.Drawing.Point(13, 115);
         this.StorageDbCheckBox.Name = "StorageDbCheckBox";
         this.StorageDbCheckBox.Size = new System.Drawing.Size(150, 17);
         this.StorageDbCheckBox.TabIndex = 3;
         this.StorageDbCheckBox.Text = "DICOM Storage Database";
         this.StorageDbCheckBox.UseVisualStyleBackColor = true;
         // 
         // LoggingDbCheckBox
         // 
         this.LoggingDbCheckBox.AutoSize = true;
         this.LoggingDbCheckBox.Checked = true;
         this.LoggingDbCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
         this.LoggingDbCheckBox.Location = new System.Drawing.Point(13, 95);
         this.LoggingDbCheckBox.Name = "LoggingDbCheckBox";
         this.LoggingDbCheckBox.Size = new System.Drawing.Size(113, 17);
         this.LoggingDbCheckBox.TabIndex = 2;
         this.LoggingDbCheckBox.Text = "Logging Database";
         this.LoggingDbCheckBox.UseVisualStyleBackColor = true;
         // 
         // DatabaseOptions
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.panel2);
         this.Controls.Add(this.panel1);
         this.Name = "DatabaseOptions";
         this.Size = new System.Drawing.Size(527, 420);
         this.VisibleChanged += new System.EventHandler(this.DatabaseOptions_VisibleChanged);
         this.panel1.ResumeLayout(false);
         this.panel1.PerformLayout();
         this.panel2.ResumeLayout(false);
         this.panel2.PerformLayout();
         this.LoadBalancePanel.ResumeLayout(false);
         this.LoadBalancePanel.PerformLayout();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.RadioButton ConnectToDbRadioButton;
      private System.Windows.Forms.RadioButton CreateNewDbRadioButton;
      private System.Windows.Forms.Label ConfigreDbDescLabel;
      private System.Windows.Forms.Label NewDbOptionDescLabel;
      private System.Windows.Forms.Panel panel2;
      private System.Windows.Forms.CheckBox UserManagementDbCheckBox;
      private System.Windows.Forms.CheckBox WorklistDbCheckBox;
      private System.Windows.Forms.CheckBox StorageDbCheckBox;
      private System.Windows.Forms.CheckBox LoggingDbCheckBox;
      private System.Windows.Forms.CheckBox WorkstationDbCheckBox;
      private System.Windows.Forms.TextBox WorkstationUserPasswordTextBox;
      private System.Windows.Forms.TextBox WorkstationUserNameTextBox;
      private System.Windows.Forms.CheckBox ShowPasswordCheckBox;
      private System.Windows.Forms.CheckBox MediaCreationDbCheckBox;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Label UserNameLabel;
      private System.Windows.Forms.CheckBox StorageServerDbCheckBox;
      private System.Windows.Forms.CheckBox StorageServerOptionsDbCheckBox;
      private System.Windows.Forms.CheckBox PatientUpdaterDbCheckBox;
      private System.Windows.Forms.Panel LoadBalancePanel;
      private System.Windows.Forms.RadioButton ConnectToOptionsDatabaseRadioButton;
      private System.Windows.Forms.RadioButton CreateOptionsDatabaseRadioButton;
      private System.Windows.Forms.TextBox WorkstationUserPasswordConfirmTextBox;
      private System.Windows.Forms.Label label2;
   }
}
