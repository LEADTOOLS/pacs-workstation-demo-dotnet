﻿namespace Leadtools.Demos.Workstation
{
   partial class TaskDialog
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
         this.label1 = new System.Windows.Forms.Label();
         this.progressBar1 = new System.Windows.Forms.ProgressBar();
         this.label2 = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.label1.Location = new System.Drawing.Point(6, 15);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(79, 20);
         this.label1.TabIndex = 3;
         this.label1.Text = "Working...";
         // 
         // progressBar1
         // 
         this.progressBar1.Location = new System.Drawing.Point(6, 60);
         this.progressBar1.Name = "progressBar1";
         this.progressBar1.Size = new System.Drawing.Size(312, 16);
         this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
         this.progressBar1.TabIndex = 2;
         // 
         // label2
         // 
         this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.label2.Location = new System.Drawing.Point(-7, 52);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(406, 1);
         this.label2.TabIndex = 4;
         // 
         // TaskDialog
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(324, 100);
         this.ControlBox = false;
         this.Controls.Add(this.label2);
         this.Controls.Add(this.label1);
         this.Controls.Add(this.progressBar1);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
         this.Name = "TaskDialog";
         this.ShowIcon = false;
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
         this.Load += new System.EventHandler(this.TaskDialog_Load);
         this.Shown += new System.EventHandler(this.TaskDialog_Shown);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.ProgressBar progressBar1;
      private System.Windows.Forms.Label label2;
   }
}