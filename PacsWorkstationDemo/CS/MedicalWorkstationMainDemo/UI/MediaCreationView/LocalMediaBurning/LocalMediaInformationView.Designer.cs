using System;
namespace Leadtools.Demos.Workstation
{
   partial class LocalMediaInformationView
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
         this.components = new System.ComponentModel.Container();
         this.label1 = new System.Windows.Forms.Label();
         this.MediaBaseFolderTextBox = new System.Windows.Forms.TextBox();
         this.BrowseMediaFolderButton = new System.Windows.Forms.Button();
         this.groupBox1 = new System.Windows.Forms.GroupBox();
         this.ViewerSizeTextBox = new System.Windows.Forms.TextBox();
         this.SizeLabel = new System.Windows.Forms.Label();
         this.BrowseViewerButton = new System.Windows.Forms.Button();
         this.ViewerDirectoryTextBox = new System.Windows.Forms.TextBox();
         this.CreateAutoRunCheckBox = new System.Windows.Forms.CheckBox();
         this.IncludeViewerCheckBox = new System.Windows.Forms.CheckBox();
         this.label4 = new System.Windows.Forms.Label();
         this.MediaTitleTextBox = new System.Windows.Forms.TextBox();
         this._mediaTitleErrorProv = new System.Windows.Forms.ErrorProvider(this.components);
         this.groupBox2 = new System.Windows.Forms.GroupBox();
         this.ClearDicomInstancesCheckBox = new System.Windows.Forms.CheckBox();
         this.WarningLabel = new System.Windows.Forms.Label();
         this.BurnButton = new System.Windows.Forms.Button();
         this.TotalSizeTextBox = new System.Windows.Forms.TextBox();
         this.label3 = new System.Windows.Forms.Label();
         this.label2 = new System.Windows.Forms.Label();
         this.PrepareMediaButton = new System.Windows.Forms.Button();
         this.MediaDirectoryTextBox = new System.Windows.Forms.TextBox();
         this.groupBox1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this._mediaTitleErrorProv)).BeginInit();
         this.groupBox2.SuspendLayout();
         this.SuspendLayout();
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(0, 40);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(197, 13);
         this.label1.TabIndex = 0;
         this.label1.Text = "Base folder where media will be created:";
         // 
         // MediaBaseFolderTextBox
         // 
         this.MediaBaseFolderTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.MediaBaseFolderTextBox.Location = new System.Drawing.Point(3, 59);
         this.MediaBaseFolderTextBox.Name = "MediaBaseFolderTextBox";
         this.MediaBaseFolderTextBox.ReadOnly = true;
         this.MediaBaseFolderTextBox.Size = new System.Drawing.Size(417, 20);
         this.MediaBaseFolderTextBox.TabIndex = 1;
         // 
         // BrowseMediaFolderButton
         // 
         this.BrowseMediaFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.BrowseMediaFolderButton.Location = new System.Drawing.Point(426, 55);
         this.BrowseMediaFolderButton.Name = "BrowseMediaFolderButton";
         this.BrowseMediaFolderButton.Size = new System.Drawing.Size(75, 23);
         this.BrowseMediaFolderButton.TabIndex = 2;
         this.BrowseMediaFolderButton.Text = "Browse...";
         this.BrowseMediaFolderButton.UseVisualStyleBackColor = true;
         // 
         // groupBox1
         // 
         this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.groupBox1.Controls.Add(this.ViewerSizeTextBox);
         this.groupBox1.Controls.Add(this.SizeLabel);
         this.groupBox1.Controls.Add(this.BrowseViewerButton);
         this.groupBox1.Controls.Add(this.ViewerDirectoryTextBox);
         this.groupBox1.Controls.Add(this.CreateAutoRunCheckBox);
         this.groupBox1.Controls.Add(this.IncludeViewerCheckBox);
         this.groupBox1.Location = new System.Drawing.Point(3, 85);
         this.groupBox1.Name = "groupBox1";
         this.groupBox1.Size = new System.Drawing.Size(505, 98);
         this.groupBox1.TabIndex = 3;
         this.groupBox1.TabStop = false;
         this.groupBox1.Text = "Viewer Options";
         // 
         // ViewerSizeTextBox
         // 
         this.ViewerSizeTextBox.Location = new System.Drawing.Point(42, 67);
         this.ViewerSizeTextBox.Name = "ViewerSizeTextBox";
         this.ViewerSizeTextBox.ReadOnly = true;
         this.ViewerSizeTextBox.Size = new System.Drawing.Size(62, 20);
         this.ViewerSizeTextBox.TabIndex = 6;
         // 
         // SizeLabel
         // 
         this.SizeLabel.AutoSize = true;
         this.SizeLabel.Location = new System.Drawing.Point(6, 70);
         this.SizeLabel.Name = "SizeLabel";
         this.SizeLabel.Size = new System.Drawing.Size(30, 13);
         this.SizeLabel.TabIndex = 5;
         this.SizeLabel.Text = "Size:";
         // 
         // BrowseViewerButton
         // 
         this.BrowseViewerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.BrowseViewerButton.Location = new System.Drawing.Point(423, 40);
         this.BrowseViewerButton.Name = "BrowseViewerButton";
         this.BrowseViewerButton.Size = new System.Drawing.Size(75, 23);
         this.BrowseViewerButton.TabIndex = 4;
         this.BrowseViewerButton.Text = "Browse...";
         this.BrowseViewerButton.UseVisualStyleBackColor = true;
         // 
         // ViewerDirectoryTextBox
         // 
         this.ViewerDirectoryTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.ViewerDirectoryTextBox.Location = new System.Drawing.Point(6, 44);
         this.ViewerDirectoryTextBox.Name = "ViewerDirectoryTextBox";
         this.ViewerDirectoryTextBox.ReadOnly = true;
         this.ViewerDirectoryTextBox.Size = new System.Drawing.Size(411, 20);
         this.ViewerDirectoryTextBox.TabIndex = 3;
         // 
         // CreateAutoRunCheckBox
         // 
         this.CreateAutoRunCheckBox.AutoSize = true;
         this.CreateAutoRunCheckBox.Location = new System.Drawing.Point(126, 19);
         this.CreateAutoRunCheckBox.Name = "CreateAutoRunCheckBox";
         this.CreateAutoRunCheckBox.Size = new System.Drawing.Size(137, 17);
         this.CreateAutoRunCheckBox.TabIndex = 1;
         this.CreateAutoRunCheckBox.Text = "Create Viewer AutoRun";
         this.CreateAutoRunCheckBox.UseVisualStyleBackColor = true;
         // 
         // IncludeViewerCheckBox
         // 
         this.IncludeViewerCheckBox.AutoSize = true;
         this.IncludeViewerCheckBox.Location = new System.Drawing.Point(6, 19);
         this.IncludeViewerCheckBox.Name = "IncludeViewerCheckBox";
         this.IncludeViewerCheckBox.Size = new System.Drawing.Size(96, 17);
         this.IncludeViewerCheckBox.TabIndex = 0;
         this.IncludeViewerCheckBox.Text = "Include Viewer";
         this.IncludeViewerCheckBox.UseVisualStyleBackColor = true;
         // 
         // label4
         // 
         this.label4.AutoSize = true;
         this.label4.Location = new System.Drawing.Point(0, 12);
         this.label4.Name = "label4";
         this.label4.Size = new System.Drawing.Size(62, 13);
         this.label4.TabIndex = 12;
         this.label4.Text = "Media Title:";
         // 
         // MediaTitleTextBox
         // 
         this.MediaTitleTextBox.Location = new System.Drawing.Point(68, 9);
         this.MediaTitleTextBox.Name = "MediaTitleTextBox";
         this.MediaTitleTextBox.Size = new System.Drawing.Size(124, 20);
         this.MediaTitleTextBox.TabIndex = 13;
         // 
         // _mediaTitleErrorProv
         // 
         this._mediaTitleErrorProv.ContainerControl = this;
         // 
         // groupBox2
         // 
         this.groupBox2.Controls.Add(this.ClearDicomInstancesCheckBox);
         this.groupBox2.Controls.Add(this.WarningLabel);
         this.groupBox2.Controls.Add(this.BurnButton);
         this.groupBox2.Controls.Add(this.TotalSizeTextBox);
         this.groupBox2.Controls.Add(this.label3);
         this.groupBox2.Controls.Add(this.label2);
         this.groupBox2.Controls.Add(this.PrepareMediaButton);
         this.groupBox2.Controls.Add(this.MediaDirectoryTextBox);
         this.groupBox2.Location = new System.Drawing.Point(3, 189);
         this.groupBox2.Name = "groupBox2";
         this.groupBox2.Size = new System.Drawing.Size(505, 147);
         this.groupBox2.TabIndex = 14;
         this.groupBox2.TabStop = false;
         this.groupBox2.Text = "Media Creation";
         // 
         // ClearDicomInstancesCheckBox
         // 
         this.ClearDicomInstancesCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.ClearDicomInstancesCheckBox.AutoSize = true;
         this.ClearDicomInstancesCheckBox.Location = new System.Drawing.Point(151, 96);
         this.ClearDicomInstancesCheckBox.Name = "ClearDicomInstancesCheckBox";
         this.ClearDicomInstancesCheckBox.Size = new System.Drawing.Size(193, 17);
         this.ClearDicomInstancesCheckBox.TabIndex = 15;
         this.ClearDicomInstancesCheckBox.Text = "Clear DICOM Instances when done";
         this.ClearDicomInstancesCheckBox.UseVisualStyleBackColor = true;
         // 
         // WarningLabel
         // 
         this.WarningLabel.AutoSize = true;
         this.WarningLabel.ForeColor = System.Drawing.Color.Blue;
         this.WarningLabel.Location = new System.Drawing.Point(9, 124);
         this.WarningLabel.Name = "WarningLabel";
         this.WarningLabel.Size = new System.Drawing.Size(0, 13);
         this.WarningLabel.TabIndex = 18;
         // 
         // BurnButton
         // 
         this.BurnButton.Location = new System.Drawing.Point(122, 19);
         this.BurnButton.Name = "BurnButton";
         this.BurnButton.Size = new System.Drawing.Size(104, 23);
         this.BurnButton.TabIndex = 17;
         this.BurnButton.Text = "Burn...";
         this.BurnButton.UseVisualStyleBackColor = true;
         // 
         // TotalSizeTextBox
         // 
         this.TotalSizeTextBox.Location = new System.Drawing.Point(72, 94);
         this.TotalSizeTextBox.Name = "TotalSizeTextBox";
         this.TotalSizeTextBox.ReadOnly = true;
         this.TotalSizeTextBox.Size = new System.Drawing.Size(62, 20);
         this.TotalSizeTextBox.TabIndex = 16;
         // 
         // label3
         // 
         this.label3.AutoSize = true;
         this.label3.Location = new System.Drawing.Point(9, 97);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(62, 13);
         this.label3.TabIndex = 15;
         this.label3.Text = "Media Size:";
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(9, 51);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(84, 13);
         this.label2.TabIndex = 14;
         this.label2.Text = "Media Directory:";
         // 
         // PrepareMediaButton
         // 
         this.PrepareMediaButton.Location = new System.Drawing.Point(12, 19);
         this.PrepareMediaButton.Name = "PrepareMediaButton";
         this.PrepareMediaButton.Size = new System.Drawing.Size(104, 23);
         this.PrepareMediaButton.TabIndex = 13;
         this.PrepareMediaButton.Text = "Prepare Media";
         this.PrepareMediaButton.UseVisualStyleBackColor = true;
         // 
         // MediaDirectoryTextBox
         // 
         this.MediaDirectoryTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.MediaDirectoryTextBox.Location = new System.Drawing.Point(12, 67);
         this.MediaDirectoryTextBox.Name = "MediaDirectoryTextBox";
         this.MediaDirectoryTextBox.ReadOnly = true;
         this.MediaDirectoryTextBox.Size = new System.Drawing.Size(484, 20);
         this.MediaDirectoryTextBox.TabIndex = 12;
         // 
         // LocalMediaInformationView
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.groupBox2);
         this.Controls.Add(this.MediaTitleTextBox);
         this.Controls.Add(this.label4);
         this.Controls.Add(this.groupBox1);
         this.Controls.Add(this.BrowseMediaFolderButton);
         this.Controls.Add(this.MediaBaseFolderTextBox);
         this.Controls.Add(this.label1);
         this.Name = "LocalMediaInformationView";
         this.Size = new System.Drawing.Size(514, 340);
         this.groupBox1.ResumeLayout(false);
         this.groupBox1.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this._mediaTitleErrorProv)).EndInit();
         this.groupBox2.ResumeLayout(false);
         this.groupBox2.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.TextBox MediaBaseFolderTextBox;
      private System.Windows.Forms.Button BrowseMediaFolderButton;
      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.Button BrowseViewerButton;
      private System.Windows.Forms.TextBox ViewerDirectoryTextBox;
      private System.Windows.Forms.CheckBox CreateAutoRunCheckBox;
      private System.Windows.Forms.CheckBox IncludeViewerCheckBox;
      private System.Windows.Forms.TextBox ViewerSizeTextBox;
      private System.Windows.Forms.Label SizeLabel;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.TextBox MediaTitleTextBox;
      private System.Windows.Forms.ErrorProvider _mediaTitleErrorProv;
      private System.Windows.Forms.GroupBox groupBox2;
      private System.Windows.Forms.Button BurnButton;
      private System.Windows.Forms.TextBox TotalSizeTextBox;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Button PrepareMediaButton;
      private System.Windows.Forms.TextBox MediaDirectoryTextBox;
      private System.Windows.Forms.Label WarningLabel;
      private System.Windows.Forms.CheckBox ClearDicomInstancesCheckBox;
   }
}
