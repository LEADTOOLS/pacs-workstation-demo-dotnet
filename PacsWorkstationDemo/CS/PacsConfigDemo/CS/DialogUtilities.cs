// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;

namespace Leadtools.Demos
{
   public static class DialogUtilities
   {
      private static string _strNotLessThan;
      private static string _strNotGreaterThan;
      private static string _strOK;
      private static string _strCancel;

      static DialogUtilities()
      {
         if (DemosGlobalization.GetCurrentThreadLanguage() == GlobalizationLanguage.Japanese)
         {
            _strNotLessThan = "should not be less than";
            _strNotGreaterThan = "should not be greater than";
            _strOK = "OK";
            _strCancel = "Cancel";
         }
         else
         {
            _strNotLessThan = "should not be less than";
            _strNotGreaterThan = "should not be greater than";
            _strOK = "OK";
            _strCancel = "Cancel";
         }
      }

      public static bool ParseInteger(TextBox textBox, string name, int min, bool useMin, int max, bool useMax, bool cancelDialog, out int value)
      {
         try
         {
            value = int.Parse(textBox.Text);

            if (useMin && value < min)
               return Fail(textBox.FindForm(), cancelDialog, string.Format("'{0}' {1} {2}", name, _strNotLessThan, min));

            if (useMax && value > max)
               return Fail(textBox.FindForm(), cancelDialog, string.Format("'{0}' {1} {2}", name, _strNotGreaterThan, max));

            return true;
         }
         catch (Exception ex)
         {
            value = 0;
            return Fail(textBox.FindForm(), cancelDialog, ex.Message);
         }
      }

      private static bool Fail(Form form, bool cancelDialog, string message)
      {
         Messager.ShowWarning(form, message);

         if (cancelDialog)
            form.DialogResult = DialogResult.None;

         return false;
      }

      public static void NumericOnLeave(object sender)
      {
         NumericUpDown num = sender as NumericUpDown;
         if (num.Value < num.Minimum)
            num.Value = num.Minimum;
         else if (num.Value > num.Maximum)
            num.Value = num.Maximum;
      }

      public static void SetNumericValue(NumericUpDown num, int value)
      {
         num.Value = Math.Max(num.Minimum, Math.Min(num.Maximum, value));
      }

      // Fix for the font issue in Windows 98 (Q326219)
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
      [DllImport("msvcrt.dll", EntryPoint = "_controlfp", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
      private static extern int _controlfp(int IN_New, int IN_Mask);
      private const int _MCW_EW = 0x0008001f;
      private const int _EM_INVALID = 0x00000010;

      static public void RunFPU()
      {
         try
         {
            _controlfp(_MCW_EW, _EM_INVALID);
         }
         catch
         {
         }
      }

      // System.Windows.Forms.PrintPreviewDialog has a bug on Windows 98 that causes a crash.  Search groups.google.com for an explanation and a potential fix
      public static bool CanRunPrintPreview
      {
         get
         {
            OperatingSystem os = Environment.OSVersion;
            return (os.Platform != PlatformID.Win32Windows);
         }
      }

      public static DialogResult InputBox(string title, string promptText, ref string value)
      {
         Form form = new Form();
         Label label = new Label();
         TextBox textBox = new TextBox();
         Button buttonOk = new Button();
         Button buttonCancel = new Button();

         form.Text = title;
         label.Text = promptText;
         textBox.Text = value;

         buttonOk.Text = _strOK;
         buttonCancel.Text =_strCancel;
         buttonOk.DialogResult = DialogResult.OK;
         buttonCancel.DialogResult = DialogResult.Cancel;

         label.SetBounds(9, 20, 372, 13);
         textBox.SetBounds(12, 36, 372, 20);
         buttonOk.SetBounds(228, 72, 75, 23);
         buttonCancel.SetBounds(309, 72, 75, 23);

         label.AutoSize = true;
         textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
         buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
         buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

         form.ClientSize = new Size(396, 107);
         form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
         form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
         form.FormBorderStyle = FormBorderStyle.FixedDialog;
         form.StartPosition = FormStartPosition.CenterScreen;
         form.MinimizeBox = false;
         form.MaximizeBox = false;
         form.AcceptButton = buttonOk;
         form.CancelButton = buttonCancel;

         DialogResult dialogResult = form.ShowDialog();
         value = textBox.Text;
         return dialogResult;
      }
   }
}
