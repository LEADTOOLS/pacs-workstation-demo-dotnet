namespace Leadtools.Demos.Workstation
{
   partial class MediaBurningManagerView
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
         this.CloseButton = new System.Windows.Forms.Button();
         this.groupBox2 = new System.Windows.Forms.GroupBox();
         this.panel2 = new System.Windows.Forms.Panel();
         this.tabControl1 = new System.Windows.Forms.TabControl();
         this.PacsTabPage = new System.Windows.Forms.TabPage();
         this.MediaInformationControl = new Leadtools.Demos.Workstation.PacsMediaInformationView();
         this.LocalTabPage = new System.Windows.Forms.TabPage();
         this.localMediaBurningView1 = new Leadtools.Demos.Workstation.LocalMediaInformationView();
         this.groupBox1 = new System.Windows.Forms.GroupBox();
         this.DicomInstancesSelectionControl = new Leadtools.Demos.Workstation.DicomInstancesSelectionView();
         this.panel1.SuspendLayout();
         this.panel2.SuspendLayout();
         this.tabControl1.SuspendLayout();
         this.PacsTabPage.SuspendLayout();
         this.LocalTabPage.SuspendLayout();
         this.groupBox1.SuspendLayout();
         this.SuspendLayout();
         // 
         // panel1
         // 
         this.panel1.Controls.Add(this.CloseButton);
         this.panel1.Controls.Add(this.groupBox2);
         this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.panel1.Location = new System.Drawing.Point(0, 376);
         this.panel1.Name = "panel1";
         this.panel1.Size = new System.Drawing.Size(764, 36);
         this.panel1.TabIndex = 3;
         // 
         // CloseButton
         // 
         this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.CloseButton.Location = new System.Drawing.Point(677, 7);
         this.CloseButton.Name = "CloseButton";
         this.CloseButton.Size = new System.Drawing.Size(75, 23);
         this.CloseButton.TabIndex = 2;
         this.CloseButton.Text = "Close";
         this.CloseButton.UseVisualStyleBackColor = true;
         // 
         // groupBox2
         // 
         this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
         this.groupBox2.Location = new System.Drawing.Point(0, 0);
         this.groupBox2.Name = "groupBox2";
         this.groupBox2.Size = new System.Drawing.Size(764, 3);
         this.groupBox2.TabIndex = 1;
         this.groupBox2.TabStop = false;
         // 
         // panel2
         // 
         this.panel2.Controls.Add(this.tabControl1);
         this.panel2.Controls.Add(this.groupBox1);
         this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel2.Location = new System.Drawing.Point(0, 0);
         this.panel2.Name = "panel2";
         this.panel2.Size = new System.Drawing.Size(764, 376);
         this.panel2.TabIndex = 4;
         // 
         // tabControl1
         // 
         this.tabControl1.Controls.Add(this.PacsTabPage);
         this.tabControl1.Controls.Add(this.LocalTabPage);
         this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.tabControl1.Location = new System.Drawing.Point(371, 0);
         this.tabControl1.Name = "tabControl1";
         this.tabControl1.SelectedIndex = 0;
         this.tabControl1.Size = new System.Drawing.Size(393, 376);
         this.tabControl1.TabIndex = 5;
         // 
         // PacsTabPage
         // 
         this.PacsTabPage.Controls.Add(this.MediaInformationControl);
         this.PacsTabPage.Location = new System.Drawing.Point(4, 22);
         this.PacsTabPage.Name = "PacsTabPage";
         this.PacsTabPage.Padding = new System.Windows.Forms.Padding(3);
         this.PacsTabPage.Size = new System.Drawing.Size(385, 313);
         this.PacsTabPage.TabIndex = 0;
         this.PacsTabPage.Text = "PACS";
         this.PacsTabPage.UseVisualStyleBackColor = true;
         // 
         // MediaInformationControl
         // 
         this.MediaInformationControl.ClearInstancesAfterRequest = false;
         this.MediaInformationControl.Dock = System.Windows.Forms.DockStyle.Fill;
         this.MediaInformationControl.IncludeDisplayApplication = false;
         this.MediaInformationControl.LabelText = "";
         this.MediaInformationControl.Location = new System.Drawing.Point(3, 3);
         this.MediaInformationControl.MediaTitle = "";
         this.MediaInformationControl.MediaType = Leadtools.Demos.Workstation.MediaType.Default;
         this.MediaInformationControl.Name = "MediaInformationControl";
         this.MediaInformationControl.NumberOfCopies = 1;
         this.MediaInformationControl.Prioirty = Leadtools.Dicom.Common.DataTypes.RequestPriority.Undefined;
         this.MediaInformationControl.SelectedServer = null;
         this.MediaInformationControl.Size = new System.Drawing.Size(379, 307);
         this.MediaInformationControl.TabIndex = 5;
         // 
         // LocalTabPage
         // 
         this.LocalTabPage.Controls.Add(this.localMediaBurningView1);
         this.LocalTabPage.Location = new System.Drawing.Point(4, 22);
         this.LocalTabPage.Name = "LocalTabPage";
         this.LocalTabPage.Padding = new System.Windows.Forms.Padding(3);
         this.LocalTabPage.Size = new System.Drawing.Size(385, 350);
         this.LocalTabPage.TabIndex = 1;
         this.LocalTabPage.Text = "Local";
         this.LocalTabPage.UseVisualStyleBackColor = true;
         // 
         // localMediaBurningView1
         // 
         this.localMediaBurningView1.CanBurnMedia = true;
         this.localMediaBurningView1.CanPrepareMedia = true;
         this.localMediaBurningView1.CreateAutoRun = false;
         this.localMediaBurningView1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.localMediaBurningView1.IncludeViewer = false;
         this.localMediaBurningView1.Location = new System.Drawing.Point(3, 3);
         this.localMediaBurningView1.MediaBaseFolder = "";
         this.localMediaBurningView1.MediaDirectory = "";
         this.localMediaBurningView1.MediaTitle = "";
         this.localMediaBurningView1.Name = "localMediaBurningView1";
         this.localMediaBurningView1.Size = new System.Drawing.Size(379, 344);
         this.localMediaBurningView1.TabIndex = 0;
         this.localMediaBurningView1.TotalSize = "";
         this.localMediaBurningView1.ViewerDirectory = "";
         this.localMediaBurningView1.ViewerSize = "";
         // 
         // groupBox1
         // 
         this.groupBox1.Controls.Add(this.DicomInstancesSelectionControl);
         this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
         this.groupBox1.Location = new System.Drawing.Point(0, 0);
         this.groupBox1.Name = "groupBox1";
         this.groupBox1.Size = new System.Drawing.Size(371, 376);
         this.groupBox1.TabIndex = 3;
         this.groupBox1.TabStop = false;
         this.groupBox1.Text = "DICOM Instances";
         // 
         // DicomInstancesSelectionControl
         // 
         this.DicomInstancesSelectionControl.Dock = System.Windows.Forms.DockStyle.Fill;
         this.DicomInstancesSelectionControl.Location = new System.Drawing.Point(3, 16);
         this.DicomInstancesSelectionControl.Name = "DicomInstancesSelectionControl";
         this.DicomInstancesSelectionControl.Size = new System.Drawing.Size(365, 357);
         this.DicomInstancesSelectionControl.TabIndex = 0;
         // 
         // MediaBurningManagerView
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(764, 412);
         this.ControlBox = false;
         this.Controls.Add(this.panel2);
         this.Controls.Add(this.panel1);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "MediaBurningManagerView";
         this.ShowIcon = false;
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
         this.Text = "Media Burning Manager";
         this.panel1.ResumeLayout(false);
         this.panel2.ResumeLayout(false);
         this.tabControl1.ResumeLayout(false);
         this.PacsTabPage.ResumeLayout(false);
         this.LocalTabPage.ResumeLayout(false);
         this.groupBox1.ResumeLayout(false);
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.GroupBox groupBox2;
      private System.Windows.Forms.Panel panel2;
      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.Button CloseButton;
      private DicomInstancesSelectionView DicomInstancesSelectionControl;
      private System.Windows.Forms.TabControl tabControl1;
      private System.Windows.Forms.TabPage PacsTabPage;
      private PacsMediaInformationView MediaInformationControl;
      private System.Windows.Forms.TabPage LocalTabPage;
      private LocalMediaInformationView localMediaBurningView1;

   }
}