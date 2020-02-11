namespace MedicalWorkstationConfigurationDemo.UI
{
   partial class SqlCeConfiguration
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
         this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
         this.panel1 = new System.Windows.Forms.Panel();
         this.browseDbButton = new System.Windows.Forms.Button();
         this.databaseTextBox = new System.Windows.Forms.TextBox();
         this.label1 = new System.Windows.Forms.Label();
         this.panel2 = new System.Windows.Forms.Panel();
         this.label2 = new System.Windows.Forms.Label();
         this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
         this.panel1.SuspendLayout();
         this.panel2.SuspendLayout();
         this.SuspendLayout();
         // 
         // openFileDialog1
         // 
         this.openFileDialog1.Filter = "SQL CE Database|*.sdf";
         this.openFileDialog1.RestoreDirectory = true;
         // 
         // panel1
         // 
         this.panel1.Controls.Add(this.browseDbButton);
         this.panel1.Controls.Add(this.databaseTextBox);
         this.panel1.Controls.Add(this.label1);
         this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel1.Location = new System.Drawing.Point(0, 0);
         this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
         this.panel1.Name = "panel1";
         this.panel1.Size = new System.Drawing.Size(501, 82);
         this.panel1.TabIndex = 0;
         // 
         // browseDbButton
         // 
         this.browseDbButton.Location = new System.Drawing.Point(3, 47);
         this.browseDbButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
         this.browseDbButton.Name = "browseDbButton";
         this.browseDbButton.Size = new System.Drawing.Size(105, 29);
         this.browseDbButton.TabIndex = 2;
         this.browseDbButton.Text = "Browse...";
         this.browseDbButton.UseVisualStyleBackColor = true;
         // 
         // databaseTextBox
         // 
         this.databaseTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.databaseTextBox.Location = new System.Drawing.Point(3, 23);
         this.databaseTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
         this.databaseTextBox.Name = "databaseTextBox";
         this.databaseTextBox.ReadOnly = true;
         this.databaseTextBox.Size = new System.Drawing.Size(493, 20);
         this.databaseTextBox.TabIndex = 1;
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(1, 5);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(75, 13);
         this.label1.TabIndex = 0;
         this.label1.Text = "&Database File:";
         // 
         // panel2
         // 
         this.panel2.Controls.Add(this.label2);
         this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.panel2.Location = new System.Drawing.Point(0, 82);
         this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
         this.panel2.Name = "panel2";
         this.panel2.Size = new System.Drawing.Size(501, 54);
         this.panel2.TabIndex = 1;
         // 
         // label2
         // 
         this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                     | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.label2.ForeColor = System.Drawing.Color.Blue;
         this.label2.Location = new System.Drawing.Point(6, 3);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(489, 47);
         this.label2.TabIndex = 0;
         this.label2.Text = "You can\'t connect to a SQL Server Compact database located on remote machine. Sel" +
             "ect a database located on the local machine.";
         // 
         // saveFileDialog1
         // 
         this.saveFileDialog1.Filter = "SQL CE Database|*.sdf";
         this.saveFileDialog1.RestoreDirectory = true;
         // 
         // SqlCeConfiguration
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.panel1);
         this.Controls.Add(this.panel2);
         this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
         this.Name = "SqlCeConfiguration";
         this.Size = new System.Drawing.Size(501, 136);
         this.panel1.ResumeLayout(false);
         this.panel1.PerformLayout();
         this.panel2.ResumeLayout(false);
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.OpenFileDialog openFileDialog1;
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.Button browseDbButton;
      private System.Windows.Forms.TextBox databaseTextBox;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Panel panel2;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.SaveFileDialog saveFileDialog1;
   }
}
