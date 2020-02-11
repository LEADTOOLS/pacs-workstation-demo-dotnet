namespace Leadtools.Demos.Workstation
{
   partial class ImagesDownloadDialog
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImagesDownloadDialog));
         this.label1 = new System.Windows.Forms.Label();
         this.linkLabel1 = new System.Windows.Forms.LinkLabel();
         this.button1 = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // label1
         // 
         this.label1.Location = new System.Drawing.Point(11, 10);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(378, 54);
         this.label1.TabIndex = 0;
         this.label1.Text = resources.GetString("label1.Text");
         // 
         // linkLabel1
         // 
         this.linkLabel1.AutoSize = true;
         this.linkLabel1.Location = new System.Drawing.Point(11, 94);
         this.linkLabel1.Name = "linkLabel1";
         this.linkLabel1.Size = new System.Drawing.Size(254, 13);
         this.linkLabel1.TabIndex = 1;
         this.linkLabel1.TabStop = true;
         this.linkLabel1.Text = "Click here to download LEADTOOLS sample images";
         this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
         // 
         // button1
         // 
         this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.button1.Location = new System.Drawing.Point(316, 137);
         this.button1.Name = "button1";
         this.button1.Size = new System.Drawing.Size(75, 23);
         this.button1.TabIndex = 3;
         this.button1.Text = "OK";
         this.button1.UseVisualStyleBackColor = true;
         // 
         // ImagesDownloadDialog
         // 
         this.AcceptButton = this.button1;
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.CancelButton = this.button1;
         this.ClientSize = new System.Drawing.Size(400, 167);
         this.Controls.Add(this.button1);
         this.Controls.Add(this.linkLabel1);
         this.Controls.Add(this.label1);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "ImagesDownloadDialog";
         this.ShowIcon = false;
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.LinkLabel linkLabel1;
      private System.Windows.Forms.Button button1;
   }
}