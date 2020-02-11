using System;
namespace Leadtools.Demos.Workstation
{
   partial class PacsMediaInformationView
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
            if ( null != DeActivated ) 
            {
               DeActivated ( this, EventArgs.Empty ) ;
            }
            
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
         this.groupBox1 = new System.Windows.Forms.GroupBox();
         this.LabelTextTextBox = new System.Windows.Forms.TextBox();
         this.label6 = new System.Windows.Forms.Label();
         this.MediaTypeComboBox = new System.Windows.Forms.ComboBox();
         this.label4 = new System.Windows.Forms.Label();
         this.PriorityComboBox = new System.Windows.Forms.ComboBox();
         this.label3 = new System.Windows.Forms.Label();
         this.NumberOfCopiesNumericUpDown = new System.Windows.Forms.NumericUpDown();
         this.label2 = new System.Windows.Forms.Label();
         this.MediaTitleTextBox = new System.Windows.Forms.TextBox();
         this.label1 = new System.Windows.Forms.Label();
         this.groupBox2 = new System.Windows.Forms.GroupBox();
         this.VerifyServerButton = new System.Windows.Forms.Button();
         this.ServerAEComboBox = new System.Windows.Forms.ComboBox();
         this.label5 = new System.Windows.Forms.Label();
         this.SendMediaRequestButton = new System.Windows.Forms.Button();
         this.ClearDicomInstancesCheckBox = new System.Windows.Forms.CheckBox();
         this.IncludeDisplayApplicationCheckBox = new System.Windows.Forms.CheckBox();
         this.groupBox1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.NumberOfCopiesNumericUpDown)).BeginInit();
         this.groupBox2.SuspendLayout();
         this.SuspendLayout();
         // 
         // groupBox1
         // 
         this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.groupBox1.Controls.Add(this.LabelTextTextBox);
         this.groupBox1.Controls.Add(this.label6);
         this.groupBox1.Controls.Add(this.MediaTypeComboBox);
         this.groupBox1.Controls.Add(this.label4);
         this.groupBox1.Controls.Add(this.PriorityComboBox);
         this.groupBox1.Controls.Add(this.label3);
         this.groupBox1.Controls.Add(this.NumberOfCopiesNumericUpDown);
         this.groupBox1.Controls.Add(this.label2);
         this.groupBox1.Controls.Add(this.MediaTitleTextBox);
         this.groupBox1.Controls.Add(this.label1);
         this.groupBox1.Location = new System.Drawing.Point(3, 3);
         this.groupBox1.Name = "groupBox1";
         this.groupBox1.Size = new System.Drawing.Size(319, 159);
         this.groupBox1.TabIndex = 0;
         this.groupBox1.TabStop = false;
         this.groupBox1.Text = "Media Information";
         // 
         // LabelTextTextBox
         // 
         this.LabelTextTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.LabelTextTextBox.Location = new System.Drawing.Point(110, 128);
         this.LabelTextTextBox.Name = "LabelTextTextBox";
         this.LabelTextTextBox.Size = new System.Drawing.Size(201, 20);
         this.LabelTextTextBox.TabIndex = 9;
         // 
         // label6
         // 
         this.label6.AutoSize = true;
         this.label6.Location = new System.Drawing.Point(7, 128);
         this.label6.Name = "label6";
         this.label6.Size = new System.Drawing.Size(60, 13);
         this.label6.TabIndex = 8;
         this.label6.Text = "Label Text:";
         // 
         // MediaTypeComboBox
         // 
         this.MediaTypeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.MediaTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.MediaTypeComboBox.FormattingEnabled = true;
         this.MediaTypeComboBox.Location = new System.Drawing.Point(112, 101);
         this.MediaTypeComboBox.Name = "MediaTypeComboBox";
         this.MediaTypeComboBox.Size = new System.Drawing.Size(120, 21);
         this.MediaTypeComboBox.TabIndex = 7;
         // 
         // label4
         // 
         this.label4.AutoSize = true;
         this.label4.Location = new System.Drawing.Point(9, 101);
         this.label4.Name = "label4";
         this.label4.Size = new System.Drawing.Size(66, 13);
         this.label4.TabIndex = 6;
         this.label4.Text = "Media Type:";
         // 
         // PriorityComboBox
         // 
         this.PriorityComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.PriorityComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.PriorityComboBox.FormattingEnabled = true;
         this.PriorityComboBox.Location = new System.Drawing.Point(112, 74);
         this.PriorityComboBox.Name = "PriorityComboBox";
         this.PriorityComboBox.Size = new System.Drawing.Size(120, 21);
         this.PriorityComboBox.TabIndex = 5;
         // 
         // label3
         // 
         this.label3.AutoSize = true;
         this.label3.Location = new System.Drawing.Point(9, 74);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(41, 13);
         this.label3.TabIndex = 4;
         this.label3.Text = "Priority:";
         // 
         // NumberOfCopiesNumericUpDown
         // 
         this.NumberOfCopiesNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.NumberOfCopiesNumericUpDown.Location = new System.Drawing.Point(112, 47);
         this.NumberOfCopiesNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
         this.NumberOfCopiesNumericUpDown.Name = "NumberOfCopiesNumericUpDown";
         this.NumberOfCopiesNumericUpDown.Size = new System.Drawing.Size(120, 20);
         this.NumberOfCopiesNumericUpDown.TabIndex = 3;
         this.NumberOfCopiesNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(9, 47);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(96, 13);
         this.label2.TabIndex = 2;
         this.label2.Text = "Number Of Copies:";
         // 
         // MediaTitleTextBox
         // 
         this.MediaTitleTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.MediaTitleTextBox.Location = new System.Drawing.Point(112, 22);
         this.MediaTitleTextBox.Name = "MediaTitleTextBox";
         this.MediaTitleTextBox.Size = new System.Drawing.Size(120, 20);
         this.MediaTitleTextBox.TabIndex = 1;
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(9, 22);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(62, 13);
         this.label1.TabIndex = 0;
         this.label1.Text = "Media Title:";
         // 
         // groupBox2
         // 
         this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.groupBox2.Controls.Add(this.VerifyServerButton);
         this.groupBox2.Controls.Add(this.ServerAEComboBox);
         this.groupBox2.Controls.Add(this.label5);
         this.groupBox2.Location = new System.Drawing.Point(3, 168);
         this.groupBox2.Name = "groupBox2";
         this.groupBox2.Size = new System.Drawing.Size(319, 57);
         this.groupBox2.TabIndex = 1;
         this.groupBox2.TabStop = false;
         this.groupBox2.Text = "Server Information";
         // 
         // VerifyServerButton
         // 
         this.VerifyServerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.VerifyServerButton.Location = new System.Drawing.Point(238, 19);
         this.VerifyServerButton.Name = "VerifyServerButton";
         this.VerifyServerButton.Size = new System.Drawing.Size(75, 23);
         this.VerifyServerButton.TabIndex = 2;
         this.VerifyServerButton.Text = "Verify";
         this.VerifyServerButton.UseVisualStyleBackColor = true;
         // 
         // ServerAEComboBox
         // 
         this.ServerAEComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.ServerAEComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.ServerAEComboBox.FormattingEnabled = true;
         this.ServerAEComboBox.Location = new System.Drawing.Point(73, 19);
         this.ServerAEComboBox.Name = "ServerAEComboBox";
         this.ServerAEComboBox.Size = new System.Drawing.Size(159, 21);
         this.ServerAEComboBox.TabIndex = 1;
         // 
         // label5
         // 
         this.label5.AutoSize = true;
         this.label5.Location = new System.Drawing.Point(9, 22);
         this.label5.Name = "label5";
         this.label5.Size = new System.Drawing.Size(58, 13);
         this.label5.TabIndex = 0;
         this.label5.Text = "Server AE:";
         // 
         // SendMediaRequestButton
         // 
         this.SendMediaRequestButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.SendMediaRequestButton.Location = new System.Drawing.Point(186, 275);
         this.SendMediaRequestButton.Name = "SendMediaRequestButton";
         this.SendMediaRequestButton.Size = new System.Drawing.Size(130, 29);
         this.SendMediaRequestButton.TabIndex = 3;
         this.SendMediaRequestButton.Text = "Send Create Request";
         this.SendMediaRequestButton.UseVisualStyleBackColor = true;
         // 
         // ClearDicomInstancesCheckBox
         // 
         this.ClearDicomInstancesCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.ClearDicomInstancesCheckBox.AutoSize = true;
         this.ClearDicomInstancesCheckBox.Location = new System.Drawing.Point(3, 253);
         this.ClearDicomInstancesCheckBox.Name = "ClearDicomInstancesCheckBox";
         this.ClearDicomInstancesCheckBox.Size = new System.Drawing.Size(317, 17);
         this.ClearDicomInstancesCheckBox.TabIndex = 2;
         this.ClearDicomInstancesCheckBox.Text = "Clear DICOM Instances after successfully sending the request";
         this.ClearDicomInstancesCheckBox.UseVisualStyleBackColor = true;
         // 
         // IncludeDisplayApplicationCheckBox
         // 
         this.IncludeDisplayApplicationCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.IncludeDisplayApplicationCheckBox.AutoSize = true;
         this.IncludeDisplayApplicationCheckBox.Location = new System.Drawing.Point(3, 229);
         this.IncludeDisplayApplicationCheckBox.Name = "IncludeDisplayApplicationCheckBox";
         this.IncludeDisplayApplicationCheckBox.Size = new System.Drawing.Size(290, 17);
         this.IncludeDisplayApplicationCheckBox.TabIndex = 4;
         this.IncludeDisplayApplicationCheckBox.Text = "Request display application to be included on the Media";
         this.IncludeDisplayApplicationCheckBox.UseVisualStyleBackColor = true;
         // 
         // MediaInformationView
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.IncludeDisplayApplicationCheckBox);
         this.Controls.Add(this.ClearDicomInstancesCheckBox);
         this.Controls.Add(this.SendMediaRequestButton);
         this.Controls.Add(this.groupBox2);
         this.Controls.Add(this.groupBox1);
         this.Name = "MediaInformationView";
         this.Size = new System.Drawing.Size(325, 310);
         this.groupBox1.ResumeLayout(false);
         this.groupBox1.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.NumberOfCopiesNumericUpDown)).EndInit();
         this.groupBox2.ResumeLayout(false);
         this.groupBox2.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.ComboBox MediaTypeComboBox;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.ComboBox PriorityComboBox;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.NumericUpDown NumberOfCopiesNumericUpDown;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.TextBox MediaTitleTextBox;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.GroupBox groupBox2;
      private System.Windows.Forms.Button VerifyServerButton;
      private System.Windows.Forms.ComboBox ServerAEComboBox;
      private System.Windows.Forms.Label label5;
      private System.Windows.Forms.Button SendMediaRequestButton;
      private System.Windows.Forms.CheckBox ClearDicomInstancesCheckBox;
      private System.Windows.Forms.TextBox LabelTextTextBox;
      private System.Windows.Forms.Label label6;
      private System.Windows.Forms.CheckBox IncludeDisplayApplicationCheckBox;

   }
}
