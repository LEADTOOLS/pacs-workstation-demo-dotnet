// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using Leadtools.Demos;
namespace Leadtools.Demos
{
   /// <summary>
   /// Summary description for AboutDialog.
   /// </summary>
   public class AboutDialog : Form
   {
      private System.Windows.Forms.Button _btnOk;
      private TextBox _tb1;
      private PictureBox _pb1;
      private LinkLabel _lblWebSite;
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      public AboutDialog(string demoName)
      {
         //
         // Required for Windows Form Designer support
         //
         InitializeComponent();

         _demoName = demoName;
      }

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      protected override void Dispose(bool disposing)
      {
         if (disposing)
         {
            if (components != null)
            {
               components.Dispose();
            }
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
         this._tb1 = new System.Windows.Forms.TextBox();
         this._pb1 = new System.Windows.Forms.PictureBox();
         this._lblWebSite = new System.Windows.Forms.LinkLabel();
         ((System.ComponentModel.ISupportInitialize)(this._pb1)).BeginInit();
         this.SuspendLayout();
         // 
         // _btnOk
         // 
         this._btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
         resources.ApplyResources(this._btnOk, "_btnOk");
         this._btnOk.Name = "_btnOk";
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
         // _pb1
         // 
         resources.ApplyResources(this._pb1, "_pb1");
         this._pb1.Name = "_pb1";
         this._pb1.TabStop = false;
         // 
         // _lblWebSite
         // 
         resources.ApplyResources(this._lblWebSite, "_lblWebSite");
         this._lblWebSite.Name = "_lblWebSite";
         this._lblWebSite.TabStop = true;
         this._lblWebSite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ll_LinkClicked);
         // 
         // AboutDialog
         // 
         this.AcceptButton = this._btnOk;
         resources.ApplyResources(this, "$this");
         this.CancelButton = this._btnOk;
         this.Controls.Add(this._lblWebSite);
         this.Controls.Add(this._tb1);
         this.Controls.Add(this._pb1);
         this.Controls.Add(this._btnOk);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "AboutDialog";
         this.ShowInTaskbar = false;
         this.Load += new System.EventHandler(this.AboutDialog_Load);
         ((System.ComponentModel.ISupportInitialize)(this._pb1)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }
      #endregion

      private string _demoName;

      private static bool Is64Process()
      {
         return IntPtr.Size == 8;
      }

      private void AboutDialog_Load(object sender, System.EventArgs e)
      {
         string platform = "32";
         if (!Is64Process())
            platform = "32";
         else
            platform = "64";
         _tb1.Text = string.Format("LEADTOOLS .NET C# {0} Demo {1}{2}{2}{2}Copyright © 1991-2019 ALL RIGHTS RESERVED.{2}LEAD Technologies, Inc.", _demoName, platform, Environment.NewLine);
         CenterToParent();
      }

      private void ll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
      {
         LinkLabel ll = sender as LinkLabel;
         if (ll != null)
            Process.Start(ll.Text);
      }
   }
}
