namespace MedicalWorkstationConfigurationDemo.UI
{
   partial class DataSourcesDialog
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
         this.label2 = new System.Windows.Forms.Label();
         this.groupBox1 = new System.Windows.Forms.GroupBox();
         this.descriptionLabel = new System.Windows.Forms.Label();
         this.dataSourceListBox = new System.Windows.Forms.ListBox();
         this.cancelButton = new System.Windows.Forms.Button();
         this.okButton = new System.Windows.Forms.Button();
         this.groupBox1.SuspendLayout();
         this.SuspendLayout();
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(3, 3);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(68, 13);
         this.label2.TabIndex = 0;
         this.label2.Text = "Data &source:";
         // 
         // groupBox1
         // 
         this.groupBox1.Controls.Add(this.descriptionLabel);
         this.groupBox1.Location = new System.Drawing.Point(216, 19);
         this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
         this.groupBox1.Name = "groupBox1";
         this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
         this.groupBox1.Size = new System.Drawing.Size(183, 152);
         this.groupBox1.TabIndex = 2;
         this.groupBox1.TabStop = false;
         this.groupBox1.Text = "Description:";
         // 
         // descriptionLabel
         // 
         this.descriptionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.descriptionLabel.Location = new System.Drawing.Point(3, 15);
         this.descriptionLabel.Name = "descriptionLabel";
         this.descriptionLabel.Size = new System.Drawing.Size(177, 135);
         this.descriptionLabel.TabIndex = 0;
         // 
         // dataSourceListBox
         // 
         this.dataSourceListBox.FormattingEnabled = true;
         this.dataSourceListBox.Location = new System.Drawing.Point(5, 24);
         this.dataSourceListBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
         this.dataSourceListBox.Name = "dataSourceListBox";
         this.dataSourceListBox.Size = new System.Drawing.Size(206, 147);
         this.dataSourceListBox.TabIndex = 1;
         // 
         // cancelButton
         // 
         this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.cancelButton.Location = new System.Drawing.Point(335, 183);
         this.cancelButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
         this.cancelButton.Name = "cancelButton";
         this.cancelButton.Size = new System.Drawing.Size(64, 29);
         this.cancelButton.TabIndex = 5;
         this.cancelButton.Text = "Cancel";
         this.cancelButton.UseVisualStyleBackColor = true;
         // 
         // okButton
         // 
         this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.okButton.Location = new System.Drawing.Point(267, 183);
         this.okButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
         this.okButton.Name = "okButton";
         this.okButton.Size = new System.Drawing.Size(64, 29);
         this.okButton.TabIndex = 4;
         this.okButton.Text = "OK";
         this.okButton.UseVisualStyleBackColor = true;
         // 
         // DataSourcesDialog
         // 
         this.AcceptButton = this.okButton;
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.CancelButton = this.cancelButton;
         this.ClientSize = new System.Drawing.Size(410, 217);
         this.Controls.Add(this.okButton);
         this.Controls.Add(this.cancelButton);
         this.Controls.Add(this.label2);
         this.Controls.Add(this.groupBox1);
         this.Controls.Add(this.dataSourceListBox);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "DataSourcesDialog";
         this.ShowIcon = false;
         this.ShowInTaskbar = false;
         this.Text = "Change Data Source";
         this.groupBox1.ResumeLayout(false);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.Label descriptionLabel;
      private System.Windows.Forms.ListBox dataSourceListBox;
      private System.Windows.Forms.Button cancelButton;
      private System.Windows.Forms.Button okButton;
   }
}
