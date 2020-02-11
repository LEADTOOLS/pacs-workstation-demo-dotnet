namespace CSPacsDatabaseConfigurationDemo.UI.New
{
   partial class ConnectionConfigurationDlg
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
         this.panel1 = new System.Windows.Forms.Panel();
         this.groupBox1 = new System.Windows.Forms.GroupBox();
         this.RejectButton = new System.Windows.Forms.Button();
         this.OKButton = new System.Windows.Forms.Button();
         this.panel2 = new System.Windows.Forms.Panel();
         this.HeaderDescriptionLabel = new System.Windows.Forms.Label();
         this.groupBox2 = new System.Windows.Forms.GroupBox();
         this.connectionConfigurationControl = new MedicalWorkstationConfigurationDemo.UI.ConnectionConfiguration();
         this.panel1.SuspendLayout();
         this.panel2.SuspendLayout();
         this.SuspendLayout();
         // 
         // panel1
         // 
         this.panel1.Controls.Add(this.groupBox1);
         this.panel1.Controls.Add(this.RejectButton);
         this.panel1.Controls.Add(this.OKButton);
         this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.panel1.Location = new System.Drawing.Point(0, 583);
         this.panel1.Name = "panel1";
         this.panel1.Size = new System.Drawing.Size(713, 39);
         this.panel1.TabIndex = 1;
         // 
         // groupBox1
         // 
         this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
         this.groupBox1.Location = new System.Drawing.Point(0, 0);
         this.groupBox1.Name = "groupBox1";
         this.groupBox1.Size = new System.Drawing.Size(713, 3);
         this.groupBox1.TabIndex = 2;
         this.groupBox1.TabStop = false;
         // 
         // RejectButton
         // 
         this.RejectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.RejectButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.RejectButton.Location = new System.Drawing.Point(635, 8);
         this.RejectButton.Name = "RejectButton";
         this.RejectButton.Size = new System.Drawing.Size(75, 23);
         this.RejectButton.TabIndex = 1;
         this.RejectButton.Text = "Cancel";
         this.RejectButton.UseVisualStyleBackColor = true;
         // 
         // OKButton
         // 
         this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.OKButton.Location = new System.Drawing.Point(554, 8);
         this.OKButton.Name = "OKButton";
         this.OKButton.Size = new System.Drawing.Size(75, 23);
         this.OKButton.TabIndex = 0;
         this.OKButton.Text = "OK";
         this.OKButton.UseVisualStyleBackColor = true;
         // 
         // panel2
         // 
         this.panel2.BackColor = System.Drawing.Color.White;
         this.panel2.Controls.Add(this.HeaderDescriptionLabel);
         this.panel2.Controls.Add(this.groupBox2);
         this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
         this.panel2.Location = new System.Drawing.Point(0, 0);
         this.panel2.Name = "panel2";
         this.panel2.Size = new System.Drawing.Size(713, 89);
         this.panel2.TabIndex = 6;
         // 
         // HeaderDescriptionLabel
         // 
         this.HeaderDescriptionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.HeaderDescriptionLabel.ForeColor = System.Drawing.Color.SteelBlue;
         this.HeaderDescriptionLabel.Location = new System.Drawing.Point(12, 16);
         this.HeaderDescriptionLabel.Name = "HeaderDescriptionLabel";
         this.HeaderDescriptionLabel.Size = new System.Drawing.Size(495, 46);
         this.HeaderDescriptionLabel.TabIndex = 2;
         this.HeaderDescriptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // groupBox2
         // 
         this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.groupBox2.Location = new System.Drawing.Point(0, 86);
         this.groupBox2.Name = "groupBox2";
         this.groupBox2.Size = new System.Drawing.Size(713, 3);
         this.groupBox2.TabIndex = 1;
         this.groupBox2.TabStop = false;
         this.groupBox2.Text = "groupBox2";
         // 
         // connectionConfigurationControl
         // 
         this.connectionConfigurationControl.CanChangeProvider = true;
         this.connectionConfigurationControl.DefaultSqlCeDatabaseName = null;
         this.connectionConfigurationControl.DefaultSqlServerDatabaseName = null;
         this.connectionConfigurationControl.Dock = System.Windows.Forms.DockStyle.Fill;
         this.connectionConfigurationControl.Location = new System.Drawing.Point(0, 89);
         this.connectionConfigurationControl.Mode = MedicalWorkstationConfigurationDemo.UI.DbConfigurationMode.Configure;
         this.connectionConfigurationControl.Name = "connectionConfigurationControl";
         this.connectionConfigurationControl.Size = new System.Drawing.Size(713, 494);
         this.connectionConfigurationControl.TabIndex = 0;
         // 
         // ConnectionConfigurationDlg
         // 
         this.AcceptButton = this.OKButton;
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.CancelButton = this.RejectButton;
         this.ClientSize = new System.Drawing.Size(713, 622);
         this.Controls.Add(this.connectionConfigurationControl);
         this.Controls.Add(this.panel2);
         this.Controls.Add(this.panel1);
         this.Name = "ConnectionConfigurationDlg";
         this.ShowIcon = false;
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
         this.Text = "Connection Configuration";
         this.panel1.ResumeLayout(false);
         this.panel2.ResumeLayout(false);
         this.ResumeLayout(false);

      }

      #endregion

      public MedicalWorkstationConfigurationDemo.UI.ConnectionConfiguration connectionConfigurationControl;
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.Button RejectButton;
      private System.Windows.Forms.Button OKButton;
      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.Panel panel2;
      private System.Windows.Forms.Label HeaderDescriptionLabel;
      private System.Windows.Forms.GroupBox groupBox2;

   }
}