namespace Leadtools.Demos.Workstation
{
   partial class MainForm
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
         this.workStationContainerControl = new Leadtools.Demos.Workstation.WorkStationContainer();
         this.SuspendLayout();
         // 
         // workStationContainerControl
         // 
         this.workStationContainerControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
         this.workStationContainerControl.Dock = System.Windows.Forms.DockStyle.Fill;
         this.workStationContainerControl.ForeColor = System.Drawing.Color.White;
         this.workStationContainerControl.Location = new System.Drawing.Point(0, 0);
         this.workStationContainerControl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
         this.workStationContainerControl.Name = "workStationContainerControl";
         this.workStationContainerControl.Size = new System.Drawing.Size(773, 570);
         this.workStationContainerControl.TabIndex = 0;
         this.workStationContainerControl.Load += new System.EventHandler(this.workStationContainerControl_Load);
         // 
         // MainForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(773, 570);
         this.Controls.Add(this.workStationContainerControl);
         this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
         this.Name = "MainForm";
         this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
         this.ResumeLayout(false);

      }

      #endregion

      private Leadtools.Demos.Workstation.WorkStationContainer workStationContainerControl ;
   }
}