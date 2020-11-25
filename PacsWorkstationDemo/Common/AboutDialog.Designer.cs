namespace Leadtools.Demos
{
   partial class AboutDialog
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutDialog));
         this._btnOk = new System.Windows.Forms.Button();
         this._gbHelp = new System.Windows.Forms.GroupBox();
         this._labelInformation = new System.Windows.Forms.Label();
         this._labelSupportPhone = new System.Windows.Forms.Label();
         this._labelWebSupport = new System.Windows.Forms.Label();
         this._labelEmail = new System.Windows.Forms.Label();
         this._labelTechnicalSupport = new System.Windows.Forms.Label();
         this._tb1 = new System.Windows.Forms.TextBox();
         this._lblWebSite = new System.Windows.Forms.Label();
         this._pb1 = new System.Windows.Forms.PictureBox();
         this._gbHelp.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this._pb1)).BeginInit();
         this.SuspendLayout();
         // 
         // _btnOk
         // 
         this._btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
         resources.ApplyResources(this._btnOk, "_btnOk");
         this._btnOk.Name = "_btnOk";
         // 
         // _gbHelp
         // 
         this._gbHelp.Controls.Add(this._labelInformation);
         this._gbHelp.Controls.Add(this._labelSupportPhone);
         this._gbHelp.Controls.Add(this._labelWebSupport);
         this._gbHelp.Controls.Add(this._labelEmail);
         this._gbHelp.Controls.Add(this._labelTechnicalSupport);
         this._gbHelp.Controls.Add(this._tb1);
         this._gbHelp.Controls.Add(this._lblWebSite);
         this._gbHelp.Controls.Add(this._pb1);
         this._gbHelp.FlatStyle = System.Windows.Forms.FlatStyle.System;
         resources.ApplyResources(this._gbHelp, "_gbHelp");
         this._gbHelp.Name = "_gbHelp";
         this._gbHelp.TabStop = false;
         // 
         // _labelInformation
         // 
         resources.ApplyResources(this._labelInformation, "_labelInformation");
         this._labelInformation.Name = "_labelInformation";
         // 
         // _labelSupportPhone
         // 
         resources.ApplyResources(this._labelSupportPhone, "_labelSupportPhone");
         this._labelSupportPhone.Name = "_labelSupportPhone";
         // 
         // _labelWebSupport
         // 
         resources.ApplyResources(this._labelWebSupport, "_labelWebSupport");
         this._labelWebSupport.Name = "_labelWebSupport";
         // 
         // _labelEmail
         // 
         resources.ApplyResources(this._labelEmail, "_labelEmail");
         this._labelEmail.Name = "_labelEmail";
         // 
         // _labelTechnicalSupport
         // 
         resources.ApplyResources(this._labelTechnicalSupport, "_labelTechnicalSupport");
         this._labelTechnicalSupport.Name = "_labelTechnicalSupport";
         // 
         // _tb1
         // 
         this._tb1.AcceptsReturn = true;
         this._tb1.BorderStyle = System.Windows.Forms.BorderStyle.None;
         resources.ApplyResources(this._tb1, "_tb1");
         this._tb1.Name = "_tb1";
         this._tb1.ReadOnly = true;
         this._tb1.TabStop = false;
         // 
         // _lblWebSite
         // 
         resources.ApplyResources(this._lblWebSite, "_lblWebSite");
         this._lblWebSite.Name = "_lblWebSite";
         // 
         // _pb1
         // 
         resources.ApplyResources(this._pb1, "_pb1");
         this._pb1.Name = "_pb1";
         this._pb1.TabStop = false;
         // 
         // AboutDialog
         // 
         this.AcceptButton = this._btnOk;
         resources.ApplyResources(this, "$this");
         this.CancelButton = this._btnOk;
         this.Controls.Add(this._gbHelp);
         this.Controls.Add(this._btnOk);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "AboutDialog";
         this.ShowInTaskbar = false;
         this.Load += new System.EventHandler(this.AboutDialog_Load);
         this._gbHelp.ResumeLayout(false);
         this._gbHelp.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this._pb1)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.PictureBox _pb1;
      private System.Windows.Forms.GroupBox _gbHelp;
      private System.Windows.Forms.Label _lblWebSite;
      private System.Windows.Forms.TextBox _tb1;
      private System.Windows.Forms.Button _btnOk;
   }
}