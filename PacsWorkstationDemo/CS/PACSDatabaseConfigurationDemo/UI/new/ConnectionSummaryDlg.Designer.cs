namespace CSPacsDatabaseConfigurationDemo.UI.New
{
   partial class ConnectionSummaryDlg
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
         this.panel2 = new System.Windows.Forms.Panel();
         this.OKButton = new System.Windows.Forms.Button();
         this.RejectButton = new System.Windows.Forms.Button();
         this.groupBox1 = new System.Windows.Forms.GroupBox();
         this.connectionSummary = new MedicalWorkstationConfigurationDemo.UI.ConnectionSummary();
         this.panel1.SuspendLayout();
         this.panel2.SuspendLayout();
         this.SuspendLayout();
         // 
         // panel1
         // 
         this.panel1.Controls.Add(this.connectionSummary);
         this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel1.Location = new System.Drawing.Point(0, 0);
         this.panel1.Name = "panel1";
         this.panel1.Size = new System.Drawing.Size(597, 484);
         this.panel1.TabIndex = 1;
         // 
         // panel2
         // 
         this.panel2.Controls.Add(this.OKButton);
         this.panel2.Controls.Add(this.RejectButton);
         this.panel2.Controls.Add(this.groupBox1);
         this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.panel2.Location = new System.Drawing.Point(0, 484);
         this.panel2.Name = "panel2";
         this.panel2.Size = new System.Drawing.Size(597, 40);
         this.panel2.TabIndex = 2;
         // 
         // OKButton
         // 
         this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.OKButton.Location = new System.Drawing.Point(435, 9);
         this.OKButton.Name = "OKButton";
         this.OKButton.Size = new System.Drawing.Size(75, 23);
         this.OKButton.TabIndex = 2;
         this.OKButton.Text = "OK";
         this.OKButton.UseVisualStyleBackColor = true;
         // 
         // RejectButton
         // 
         this.RejectButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.RejectButton.Location = new System.Drawing.Point(516, 8);
         this.RejectButton.Name = "RejectButton";
         this.RejectButton.Size = new System.Drawing.Size(75, 23);
         this.RejectButton.TabIndex = 1;
         this.RejectButton.Text = "Cancel";
         this.RejectButton.UseVisualStyleBackColor = true;
         // 
         // groupBox1
         // 
         this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
         this.groupBox1.Location = new System.Drawing.Point(0, 0);
         this.groupBox1.Name = "groupBox1";
         this.groupBox1.Size = new System.Drawing.Size(597, 3);
         this.groupBox1.TabIndex = 0;
         this.groupBox1.TabStop = false;
         // 
         // connectionSummary
         // 
         this.connectionSummary.Dock = System.Windows.Forms.DockStyle.Fill;
         this.connectionSummary.Location = new System.Drawing.Point(0, 0);
         this.connectionSummary.Name = "connectionSummary";
         this.connectionSummary.Padding = new System.Windows.Forms.Padding(5);
         this.connectionSummary.Size = new System.Drawing.Size(597, 484);
         this.connectionSummary.Summary = "";
         this.connectionSummary.TabIndex = 1;
         // 
         // ConnectionSummaryDlg
         // 
         this.AcceptButton = this.OKButton;
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.CancelButton = this.RejectButton;
         this.ClientSize = new System.Drawing.Size(597, 524);
         this.Controls.Add(this.panel1);
         this.Controls.Add(this.panel2);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "ConnectionSummaryDlg";
         this.ShowIcon = false;
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
         this.Text = "Connection Summary";
         this.panel1.ResumeLayout(false);
         this.panel2.ResumeLayout(false);
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.Panel panel2;
      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.Button OKButton;
      private System.Windows.Forms.Button RejectButton;
      public MedicalWorkstationConfigurationDemo.UI.ConnectionSummary connectionSummary;

   }
}