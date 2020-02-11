namespace MedicalWorkstationConfigurationDemo.UI
{
   partial class ConnectionSummary
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
         this.panel2 = new System.Windows.Forms.Panel();
         this.SummaryRichTextBox = new System.Windows.Forms.RichTextBox();
         this.panel2.SuspendLayout();
         this.SuspendLayout();
         // 
         // panel2
         // 
         this.panel2.Controls.Add(this.SummaryRichTextBox);
         this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel2.Location = new System.Drawing.Point(5, 5);
         this.panel2.Name = "panel2";
         this.panel2.Size = new System.Drawing.Size(586, 337);
         this.panel2.TabIndex = 1;
         // 
         // SummaryRichTextBox
         // 
         this.SummaryRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
         this.SummaryRichTextBox.Location = new System.Drawing.Point(0, 0);
         this.SummaryRichTextBox.Name = "SummaryRichTextBox";
         this.SummaryRichTextBox.Size = new System.Drawing.Size(586, 337);
         this.SummaryRichTextBox.TabIndex = 0;
         this.SummaryRichTextBox.Text = "";
         // 
         // ConnectionSummary
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.panel2);
         this.Name = "ConnectionSummary";
         this.Padding = new System.Windows.Forms.Padding(5);
         this.Size = new System.Drawing.Size(596, 347);
         this.panel2.ResumeLayout(false);
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.Panel panel2;
      private System.Windows.Forms.RichTextBox SummaryRichTextBox;
   }
}
