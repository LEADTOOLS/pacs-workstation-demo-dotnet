namespace CSPacsDatabaseConfigurationDemo.UI.New
{
   partial class NotRecommendedWarning
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
         this.groupBoxNotRecommended = new System.Windows.Forms.GroupBox();
         this.textBoxNotRecommended = new System.Windows.Forms.TextBox();
         this.pictureBoxWarning = new System.Windows.Forms.PictureBox();
         this.groupBoxNotRecommended.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWarning)).BeginInit();
         this.SuspendLayout();
         // 
         // groupBoxNotRecommended
         // 
         this.groupBoxNotRecommended.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                     | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.groupBoxNotRecommended.Controls.Add(this.textBoxNotRecommended);
         this.groupBoxNotRecommended.Controls.Add(this.pictureBoxWarning);
         this.groupBoxNotRecommended.Location = new System.Drawing.Point(3, 3);
         this.groupBoxNotRecommended.Name = "groupBoxNotRecommended";
         this.groupBoxNotRecommended.Size = new System.Drawing.Size(367, 240);
         this.groupBoxNotRecommended.TabIndex = 17;
         this.groupBoxNotRecommended.TabStop = false;
         // 
         // textBoxNotRecommended
         // 
         this.textBoxNotRecommended.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                     | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.textBoxNotRecommended.BorderStyle = System.Windows.Forms.BorderStyle.None;
         this.textBoxNotRecommended.Location = new System.Drawing.Point(154, 31);
         this.textBoxNotRecommended.Multiline = true;
         this.textBoxNotRecommended.Name = "textBoxNotRecommended";
         this.textBoxNotRecommended.ReadOnly = true;
         this.textBoxNotRecommended.Size = new System.Drawing.Size(134, 197);
         this.textBoxNotRecommended.TabIndex = 1;
         // 
         // pictureBoxWarning
         // 
         this.pictureBoxWarning.Location = new System.Drawing.Point(24, 20);
         this.pictureBoxWarning.Name = "pictureBoxWarning";
         this.pictureBoxWarning.Size = new System.Drawing.Size(123, 113);
         this.pictureBoxWarning.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
         this.pictureBoxWarning.TabIndex = 0;
         this.pictureBoxWarning.TabStop = false;
         // 
         // NotRecommendedWarning
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.groupBoxNotRecommended);
         this.Name = "NotRecommendedWarning";
         this.Size = new System.Drawing.Size(373, 246);
         this.groupBoxNotRecommended.ResumeLayout(false);
         this.groupBoxNotRecommended.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWarning)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.GroupBox groupBoxNotRecommended;
      private System.Windows.Forms.TextBox textBoxNotRecommended;
      private System.Windows.Forms.PictureBox pictureBoxWarning;
   }
}
