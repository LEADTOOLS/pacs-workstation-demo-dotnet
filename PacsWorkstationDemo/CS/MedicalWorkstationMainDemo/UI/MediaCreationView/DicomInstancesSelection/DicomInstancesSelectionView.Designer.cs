namespace Leadtools.Demos.Workstation
{
   partial class DicomInstancesSelectionView
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
         this.AddAllButton = new System.Windows.Forms.Button();
         this.RemoveAllButton = new System.Windows.Forms.Button();
         this.EveryOtherImageButton = new System.Windows.Forms.Button();
         this.ClearButton = new System.Windows.Forms.Button();
         this.StudyNodesTreeView = new Leadtools.Demos.Workstation.MyTreeView();
         this.SuspendLayout();
         // 
         // AddAllButton
         // 
         this.AddAllButton.Location = new System.Drawing.Point(255, 3);
         this.AddAllButton.Name = "AddAllButton";
         this.AddAllButton.Size = new System.Drawing.Size(107, 23);
         this.AddAllButton.TabIndex = 2;
         this.AddAllButton.Text = "Add All";
         this.AddAllButton.UseVisualStyleBackColor = true;
         // 
         // RemoveAllButton
         // 
         this.RemoveAllButton.Location = new System.Drawing.Point(255, 30);
         this.RemoveAllButton.Name = "RemoveAllButton";
         this.RemoveAllButton.Size = new System.Drawing.Size(107, 23);
         this.RemoveAllButton.TabIndex = 3;
         this.RemoveAllButton.Text = "Remove All";
         this.RemoveAllButton.UseVisualStyleBackColor = true;
         // 
         // EveryOtherImageButton
         // 
         this.EveryOtherImageButton.Location = new System.Drawing.Point(255, 59);
         this.EveryOtherImageButton.Name = "EveryOtherImageButton";
         this.EveryOtherImageButton.Size = new System.Drawing.Size(107, 23);
         this.EveryOtherImageButton.TabIndex = 4;
         this.EveryOtherImageButton.Text = "Every other Image";
         this.EveryOtherImageButton.UseVisualStyleBackColor = true;
         // 
         // ClearButton
         // 
         this.ClearButton.Location = new System.Drawing.Point(255, 88);
         this.ClearButton.Name = "ClearButton";
         this.ClearButton.Size = new System.Drawing.Size(107, 23);
         this.ClearButton.TabIndex = 5;
         this.ClearButton.Text = "Clear";
         this.ClearButton.UseVisualStyleBackColor = true;
         // 
         // StudyNodesTreeView
         // 
         this.StudyNodesTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                     | System.Windows.Forms.AnchorStyles.Left)));
         this.StudyNodesTreeView.CheckBoxes = true;
         this.StudyNodesTreeView.HideSelection = false;
         this.StudyNodesTreeView.Location = new System.Drawing.Point(0, 0);
         this.StudyNodesTreeView.Name = "StudyNodesTreeView";
         this.StudyNodesTreeView.Size = new System.Drawing.Size(249, 273);
         this.StudyNodesTreeView.TabIndex = 0;
         // 
         // DicomInstancesSelectionView
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.ClearButton);
         this.Controls.Add(this.EveryOtherImageButton);
         this.Controls.Add(this.RemoveAllButton);
         this.Controls.Add(this.AddAllButton);
         this.Controls.Add(this.StudyNodesTreeView);
         this.Name = "DicomInstancesSelectionView";
         this.Size = new System.Drawing.Size(365, 278);
         this.ResumeLayout(false);

      }

      #endregion

      private MyTreeView StudyNodesTreeView;
      private System.Windows.Forms.Button AddAllButton;
      private System.Windows.Forms.Button RemoveAllButton;
      private System.Windows.Forms.Button EveryOtherImageButton;
      private System.Windows.Forms.Button ClearButton;
   }
}
