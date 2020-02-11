namespace Leadtools.Demos.Workstation
{
   partial class EventLogViewerDialog
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
         this.eventLogViewer1 = new Leadtools.Medical.Winforms.EventLogViewer();
         this.SuspendLayout();
         // 
         // eventLogViewer1
         // 
         this.eventLogViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.eventLogViewer1.Location = new System.Drawing.Point(0, 0);
         this.eventLogViewer1.Name = "eventLogViewer1";
         this.eventLogViewer1.Size = new System.Drawing.Size(708, 625);
         this.eventLogViewer1.TabIndex = 0;
         // 
         // EventLogViewerDialog
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(708, 625);
         this.Controls.Add(this.eventLogViewer1);
         this.Name = "EventLogViewerDialog";
         this.Text = "Event Log Viewer ";
         this.ResumeLayout(false);

      }

      #endregion

      private Leadtools.Medical.Winforms.EventLogViewer eventLogViewer1;
   }
}