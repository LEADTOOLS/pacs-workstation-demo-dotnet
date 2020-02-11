namespace Leadtools.Demos.Workstation
{
   partial class ClientConfiguration
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
         this.components = new System.ComponentModel.Container();
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientConfiguration));
         this.CompressionGroupBox = new System.Windows.Forms.GroupBox();
         this.LossyCompressionRadioButton = new System.Windows.Forms.RadioButton();
         this.LosslessCompressionRadioButton = new System.Windows.Forms.RadioButton();
         this.DebugGroupBox = new System.Windows.Forms.GroupBox();
         this.DebugLogFileButton = new System.Windows.Forms.Button();
         this.DebugLogFileNameTextBox = new System.Windows.Forms.TextBox();
         this.DebugLogFileCheckBox = new System.Windows.Forms.CheckBox();
         this.DisplayDetailDebugMsgCheckBox = new System.Windows.Forms.CheckBox();
         this.DicomClientGroupBox = new System.Windows.Forms.GroupBox();
         this.ForceToAllClientsCheckBox = new System.Windows.Forms.CheckBox();
         this.WorkstationHostaddressComboBox = new System.Windows.Forms.ComboBox();
         this.WorkstationPortLabel = new System.Windows.Forms.Label();
         this.WorkstationPortMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
         this.WorkstationIPLabel = new System.Windows.Forms.Label();
         this.DicomClientDescriptionLabel = new System.Windows.Forms.Label();
         this.WorstationAELabel = new System.Windows.Forms.Label();
         this.WorkstationAETextBox = new System.Windows.Forms.TextBox();
         this.EnableDebugCheckBox = new System.Windows.Forms.CheckBox();
         this.BrowseLogFileDialog = new System.Windows.Forms.SaveFileDialog();
         this.ContinueRetireveOnDuplicateCheckBox = new System.Windows.Forms.CheckBox();
         this.DicomRetrieveGroupBox = new System.Windows.Forms.GroupBox();
         this.label1 = new System.Windows.Forms.Label();
         this.WorkstationServiceAETitleTextBox = new System.Windows.Forms.TextBox();
         this.MoveToWsServiceRadioButton = new System.Windows.Forms.RadioButton();
         this.MoveToWsClientRadioButton = new System.Windows.Forms.RadioButton();
         this.GenericErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
         this.groupBox1 = new System.Windows.Forms.GroupBox();
         this.groupBox2 = new System.Windows.Forms.GroupBox();
         this.MeasurmentsUnitComboBox = new System.Windows.Forms.ComboBox();
         this.label3 = new System.Windows.Forms.Label();
         this.AnnotationColorButton = new System.Windows.Forms.Button();
         this.label2 = new System.Windows.Forms.Label();
         this.DisplayOrientationButton = new System.Windows.Forms.Button();
         this.FullScreenModeCheckBox = new System.Windows.Forms.CheckBox();
         this.ViewerOverlayTextSizeGroupBox = new System.Windows.Forms.GroupBox();
         this.FixedOverlayTextSizeNumericUpDown = new System.Windows.Forms.NumericUpDown();
         this.FixedOverlayTextSizeLabel = new System.Windows.Forms.Label();
         this.AutoSizeOverlayTextCheckBox = new System.Windows.Forms.CheckBox();
         this.LazyLoadingGroupBox = new System.Windows.Forms.GroupBox();
         this.EnableLazyLoadingCheckBox = new System.Windows.Forms.CheckBox();
         this.LazyLoadingHiddenImagesMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
         this.LazyLoadingHiddenLabel = new System.Windows.Forms.Label();
         this.LazyLoadingLoadLable = new System.Windows.Forms.Label();
         this.LazyLoadingDescriptionLabel = new System.Windows.Forms.Label();
         this.UseCompressionCheckBox = new System.Windows.Forms.CheckBox();
         this.SaveSessionGroupBox = new System.Windows.Forms.GroupBox();
         this.PromptUserSaveSessionRadioButton = new System.Windows.Forms.RadioButton();
         this.NeverSaveSessionRadioButton = new System.Windows.Forms.RadioButton();
         this.AlwaysSaveSessionRadioButton = new System.Windows.Forms.RadioButton();
         this.SaveUserSessionLabel = new System.Windows.Forms.Label();
         this.WorkstationSecureCheckBox = new System.Windows.Forms.CheckBox();
         this.CompressionGroupBox.SuspendLayout();
         this.DebugGroupBox.SuspendLayout();
         this.DicomClientGroupBox.SuspendLayout();
         this.DicomRetrieveGroupBox.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.GenericErrorProvider)).BeginInit();
         this.groupBox1.SuspendLayout();
         this.groupBox2.SuspendLayout();
         this.ViewerOverlayTextSizeGroupBox.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.FixedOverlayTextSizeNumericUpDown)).BeginInit();
         this.LazyLoadingGroupBox.SuspendLayout();
         this.SaveSessionGroupBox.SuspendLayout();
         this.SuspendLayout();
         // 
         // CompressionGroupBox
         // 
         this.CompressionGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.CompressionGroupBox.Controls.Add(this.LossyCompressionRadioButton);
         this.CompressionGroupBox.Controls.Add(this.LosslessCompressionRadioButton);
         this.CompressionGroupBox.Location = new System.Drawing.Point(510, 151);
         this.CompressionGroupBox.Name = "CompressionGroupBox";
         this.CompressionGroupBox.Size = new System.Drawing.Size(473, 75);
         this.CompressionGroupBox.TabIndex = 1;
         this.CompressionGroupBox.TabStop = false;
         // 
         // LossyCompressionRadioButton
         // 
         this.LossyCompressionRadioButton.AutoSize = true;
         this.LossyCompressionRadioButton.Location = new System.Drawing.Point(18, 49);
         this.LossyCompressionRadioButton.Name = "LossyCompressionRadioButton";
         this.LossyCompressionRadioButton.Size = new System.Drawing.Size(52, 17);
         this.LossyCompressionRadioButton.TabIndex = 1;
         this.LossyCompressionRadioButton.TabStop = true;
         this.LossyCompressionRadioButton.Text = "Lossy";
         this.LossyCompressionRadioButton.UseVisualStyleBackColor = true;
         // 
         // LosslessCompressionRadioButton
         // 
         this.LosslessCompressionRadioButton.AutoSize = true;
         this.LosslessCompressionRadioButton.Location = new System.Drawing.Point(18, 26);
         this.LosslessCompressionRadioButton.Name = "LosslessCompressionRadioButton";
         this.LosslessCompressionRadioButton.Size = new System.Drawing.Size(65, 17);
         this.LosslessCompressionRadioButton.TabIndex = 0;
         this.LosslessCompressionRadioButton.TabStop = true;
         this.LosslessCompressionRadioButton.Text = "Lossless";
         this.LosslessCompressionRadioButton.UseVisualStyleBackColor = true;
         // 
         // DebugGroupBox
         // 
         this.DebugGroupBox.Controls.Add(this.DebugLogFileButton);
         this.DebugGroupBox.Controls.Add(this.DebugLogFileNameTextBox);
         this.DebugGroupBox.Controls.Add(this.DebugLogFileCheckBox);
         this.DebugGroupBox.Controls.Add(this.DisplayDetailDebugMsgCheckBox);
         this.DebugGroupBox.Location = new System.Drawing.Point(9, 405);
         this.DebugGroupBox.Name = "DebugGroupBox";
         this.DebugGroupBox.Size = new System.Drawing.Size(494, 124);
         this.DebugGroupBox.TabIndex = 4;
         this.DebugGroupBox.TabStop = false;
         // 
         // DebugLogFileButton
         // 
         this.DebugLogFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.DebugLogFileButton.BackColor = System.Drawing.SystemColors.Control;
         this.DebugLogFileButton.CausesValidation = false;
         this.DebugLogFileButton.ForeColor = System.Drawing.Color.Black;
         this.DebugLogFileButton.Location = new System.Drawing.Point(457, 72);
         this.DebugLogFileButton.Name = "DebugLogFileButton";
         this.DebugLogFileButton.Size = new System.Drawing.Size(29, 28);
         this.DebugLogFileButton.TabIndex = 3;
         this.DebugLogFileButton.Text = "...";
         this.DebugLogFileButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
         this.DebugLogFileButton.UseVisualStyleBackColor = false;
         // 
         // DebugLogFileNameTextBox
         // 
         this.DebugLogFileNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.DebugLogFileNameTextBox.Location = new System.Drawing.Point(59, 77);
         this.DebugLogFileNameTextBox.Name = "DebugLogFileNameTextBox";
         this.DebugLogFileNameTextBox.Size = new System.Drawing.Size(392, 20);
         this.DebugLogFileNameTextBox.TabIndex = 2;
         // 
         // DebugLogFileCheckBox
         // 
         this.DebugLogFileCheckBox.AutoSize = true;
         this.DebugLogFileCheckBox.CausesValidation = false;
         this.DebugLogFileCheckBox.Location = new System.Drawing.Point(36, 54);
         this.DebugLogFileCheckBox.Name = "DebugLogFileCheckBox";
         this.DebugLogFileCheckBox.Size = new System.Drawing.Size(234, 17);
         this.DebugLogFileCheckBox.TabIndex = 1;
         this.DebugLogFileCheckBox.Text = "Generate Log file for DICOM communication";
         this.DebugLogFileCheckBox.UseVisualStyleBackColor = true;
         // 
         // DisplayDetailDebugMsgCheckBox
         // 
         this.DisplayDetailDebugMsgCheckBox.AutoSize = true;
         this.DisplayDetailDebugMsgCheckBox.Location = new System.Drawing.Point(36, 27);
         this.DisplayDetailDebugMsgCheckBox.Name = "DisplayDetailDebugMsgCheckBox";
         this.DisplayDetailDebugMsgCheckBox.Size = new System.Drawing.Size(227, 17);
         this.DisplayDetailDebugMsgCheckBox.TabIndex = 0;
         this.DisplayDetailDebugMsgCheckBox.Text = "Display detailed debug messages on errors";
         this.DisplayDetailDebugMsgCheckBox.UseVisualStyleBackColor = true;
         // 
         // DicomClientGroupBox
         // 
         this.DicomClientGroupBox.Controls.Add(this.WorkstationSecureCheckBox);
         this.DicomClientGroupBox.Controls.Add(this.ForceToAllClientsCheckBox);
         this.DicomClientGroupBox.Controls.Add(this.WorkstationHostaddressComboBox);
         this.DicomClientGroupBox.Controls.Add(this.WorkstationPortLabel);
         this.DicomClientGroupBox.Controls.Add(this.WorkstationPortMaskedTextBox);
         this.DicomClientGroupBox.Controls.Add(this.WorkstationIPLabel);
         this.DicomClientGroupBox.Controls.Add(this.DicomClientDescriptionLabel);
         this.DicomClientGroupBox.Controls.Add(this.WorstationAELabel);
         this.DicomClientGroupBox.Controls.Add(this.WorkstationAETextBox);
         this.DicomClientGroupBox.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
         this.DicomClientGroupBox.ForeColor = System.Drawing.Color.White;
         this.DicomClientGroupBox.Location = new System.Drawing.Point(6, 9);
         this.DicomClientGroupBox.Name = "DicomClientGroupBox";
         this.DicomClientGroupBox.Size = new System.Drawing.Size(497, 217);
         this.DicomClientGroupBox.TabIndex = 0;
         this.DicomClientGroupBox.TabStop = false;
         this.DicomClientGroupBox.Text = "DICOM Client";
         // 
         // ForceToAllClientsCheckBox
         // 
         this.ForceToAllClientsCheckBox.AutoSize = true;
         this.ForceToAllClientsCheckBox.Location = new System.Drawing.Point(13, 159);
         this.ForceToAllClientsCheckBox.Name = "ForceToAllClientsCheckBox";
         this.ForceToAllClientsCheckBox.Size = new System.Drawing.Size(298, 17);
         this.ForceToAllClientsCheckBox.TabIndex = 5;
         this.ForceToAllClientsCheckBox.Text = "Force AE Title and Port to all Workstation Clients";
         this.ForceToAllClientsCheckBox.UseVisualStyleBackColor = true;
         // 
         // WorkstationHostaddressComboBox
         // 
         this.WorkstationHostaddressComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.WorkstationHostaddressComboBox.Font = new System.Drawing.Font("Tahoma", 8F);
         this.WorkstationHostaddressComboBox.FormattingEnabled = true;
         this.WorkstationHostaddressComboBox.Location = new System.Drawing.Point(89, 99);
         this.WorkstationHostaddressComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
         this.WorkstationHostaddressComboBox.Name = "WorkstationHostaddressComboBox";
         this.WorkstationHostaddressComboBox.Size = new System.Drawing.Size(205, 21);
         this.WorkstationHostaddressComboBox.TabIndex = 4;
         // 
         // WorkstationPortLabel
         // 
         this.WorkstationPortLabel.AutoSize = true;
         this.WorkstationPortLabel.Font = new System.Drawing.Font("Tahoma", 8F);
         this.WorkstationPortLabel.Location = new System.Drawing.Point(10, 131);
         this.WorkstationPortLabel.Name = "WorkstationPortLabel";
         this.WorkstationPortLabel.Size = new System.Drawing.Size(31, 13);
         this.WorkstationPortLabel.TabIndex = 11;
         this.WorkstationPortLabel.Text = "Port:";
         // 
         // WorkstationPortMaskedTextBox
         // 
         this.WorkstationPortMaskedTextBox.Font = new System.Drawing.Font("Tahoma", 8F);
         this.WorkstationPortMaskedTextBox.Location = new System.Drawing.Point(89, 128);
         this.WorkstationPortMaskedTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
         this.WorkstationPortMaskedTextBox.Mask = "000000";
         this.WorkstationPortMaskedTextBox.Name = "WorkstationPortMaskedTextBox";
         this.WorkstationPortMaskedTextBox.PromptChar = '*';
         this.WorkstationPortMaskedTextBox.Size = new System.Drawing.Size(110, 20);
         this.WorkstationPortMaskedTextBox.TabIndex = 12;
         // 
         // WorkstationIPLabel
         // 
         this.WorkstationIPLabel.AutoSize = true;
         this.WorkstationIPLabel.Font = new System.Drawing.Font("Tahoma", 8F);
         this.WorkstationIPLabel.Location = new System.Drawing.Point(11, 101);
         this.WorkstationIPLabel.Name = "WorkstationIPLabel";
         this.WorkstationIPLabel.Size = new System.Drawing.Size(63, 13);
         this.WorkstationIPLabel.TabIndex = 3;
         this.WorkstationIPLabel.Text = "IP Address:";
         // 
         // DicomClientDescriptionLabel
         // 
         this.DicomClientDescriptionLabel.Font = new System.Drawing.Font("Tahoma", 8F);
         this.DicomClientDescriptionLabel.Location = new System.Drawing.Point(11, 25);
         this.DicomClientDescriptionLabel.Name = "DicomClientDescriptionLabel";
         this.DicomClientDescriptionLabel.Size = new System.Drawing.Size(480, 34);
         this.DicomClientDescriptionLabel.TabIndex = 0;
         this.DicomClientDescriptionLabel.Text = "The Workstation Client will perform DICOM Query/Retrieve against DICOM servers an" +
    "d allow you to store DICOM Objects to configured PACS.";
         // 
         // WorstationAELabel
         // 
         this.WorstationAELabel.AutoSize = true;
         this.WorstationAELabel.Font = new System.Drawing.Font("Tahoma", 8F);
         this.WorstationAELabel.Location = new System.Drawing.Point(10, 73);
         this.WorstationAELabel.Name = "WorstationAELabel";
         this.WorstationAELabel.Size = new System.Drawing.Size(47, 13);
         this.WorstationAELabel.TabIndex = 1;
         this.WorstationAELabel.Text = "AE Title:";
         // 
         // WorkstationAETextBox
         // 
         this.WorkstationAETextBox.Font = new System.Drawing.Font("Tahoma", 8F);
         this.WorkstationAETextBox.Location = new System.Drawing.Point(89, 71);
         this.WorkstationAETextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
         this.WorkstationAETextBox.Name = "WorkstationAETextBox";
         this.WorkstationAETextBox.Size = new System.Drawing.Size(205, 20);
         this.WorkstationAETextBox.TabIndex = 2;
         // 
         // EnableDebugCheckBox
         // 
         this.EnableDebugCheckBox.AutoSize = true;
         this.EnableDebugCheckBox.CausesValidation = false;
         this.EnableDebugCheckBox.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
         this.EnableDebugCheckBox.ForeColor = System.Drawing.Color.White;
         this.EnableDebugCheckBox.Location = new System.Drawing.Point(16, 404);
         this.EnableDebugCheckBox.Name = "EnableDebugCheckBox";
         this.EnableDebugCheckBox.Size = new System.Drawing.Size(138, 17);
         this.EnableDebugCheckBox.TabIndex = 5;
         this.EnableDebugCheckBox.Text = "Enable DEBUG mode";
         this.EnableDebugCheckBox.UseVisualStyleBackColor = true;
         // 
         // BrowseLogFileDialog
         // 
         this.BrowseLogFileDialog.Filter = "Text File (*.txt)|*.txt|Alll Files|*.*";
         // 
         // ContinueRetireveOnDuplicateCheckBox
         // 
         this.ContinueRetireveOnDuplicateCheckBox.AutoSize = true;
         this.ContinueRetireveOnDuplicateCheckBox.Font = new System.Drawing.Font("Tahoma", 8F);
         this.ContinueRetireveOnDuplicateCheckBox.ForeColor = System.Drawing.Color.White;
         this.ContinueRetireveOnDuplicateCheckBox.Location = new System.Drawing.Point(43, 48);
         this.ContinueRetireveOnDuplicateCheckBox.Name = "ContinueRetireveOnDuplicateCheckBox";
         this.ContinueRetireveOnDuplicateCheckBox.Size = new System.Drawing.Size(366, 17);
         this.ContinueRetireveOnDuplicateCheckBox.TabIndex = 0;
         this.ContinueRetireveOnDuplicateCheckBox.Text = "Continue retrieving images from PACS when duplicate instances found.";
         this.ContinueRetireveOnDuplicateCheckBox.UseVisualStyleBackColor = true;
         // 
         // DicomRetrieveGroupBox
         // 
         this.DicomRetrieveGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.DicomRetrieveGroupBox.Controls.Add(this.label1);
         this.DicomRetrieveGroupBox.Controls.Add(this.WorkstationServiceAETitleTextBox);
         this.DicomRetrieveGroupBox.Controls.Add(this.MoveToWsServiceRadioButton);
         this.DicomRetrieveGroupBox.Controls.Add(this.MoveToWsClientRadioButton);
         this.DicomRetrieveGroupBox.Controls.Add(this.ContinueRetireveOnDuplicateCheckBox);
         this.DicomRetrieveGroupBox.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
         this.DicomRetrieveGroupBox.ForeColor = System.Drawing.Color.White;
         this.DicomRetrieveGroupBox.Location = new System.Drawing.Point(510, 9);
         this.DicomRetrieveGroupBox.Name = "DicomRetrieveGroupBox";
         this.DicomRetrieveGroupBox.Size = new System.Drawing.Size(473, 133);
         this.DicomRetrieveGroupBox.TabIndex = 3;
         this.DicomRetrieveGroupBox.TabStop = false;
         this.DicomRetrieveGroupBox.Text = "DICOM Retrieve";
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Font = new System.Drawing.Font("Tahoma", 8F);
         this.label1.Location = new System.Drawing.Point(45, 100);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(47, 13);
         this.label1.TabIndex = 13;
         this.label1.Text = "AE Title:";
         // 
         // WorkstationServiceAETitleTextBox
         // 
         this.WorkstationServiceAETitleTextBox.Font = new System.Drawing.Font("Tahoma", 8F);
         this.WorkstationServiceAETitleTextBox.Location = new System.Drawing.Point(96, 98);
         this.WorkstationServiceAETitleTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
         this.WorkstationServiceAETitleTextBox.Name = "WorkstationServiceAETitleTextBox";
         this.WorkstationServiceAETitleTextBox.ReadOnly = true;
         this.WorkstationServiceAETitleTextBox.Size = new System.Drawing.Size(205, 20);
         this.WorkstationServiceAETitleTextBox.TabIndex = 14;
         // 
         // MoveToWsServiceRadioButton
         // 
         this.MoveToWsServiceRadioButton.AutoSize = true;
         this.MoveToWsServiceRadioButton.Location = new System.Drawing.Point(18, 71);
         this.MoveToWsServiceRadioButton.Name = "MoveToWsServiceRadioButton";
         this.MoveToWsServiceRadioButton.Size = new System.Drawing.Size(325, 17);
         this.MoveToWsServiceRadioButton.TabIndex = 10;
         this.MoveToWsServiceRadioButton.TabStop = true;
         this.MoveToWsServiceRadioButton.Text = "Move DICOM Objects to Workstation Listener Service";
         this.MoveToWsServiceRadioButton.UseVisualStyleBackColor = true;
         // 
         // MoveToWsClientRadioButton
         // 
         this.MoveToWsClientRadioButton.AutoSize = true;
         this.MoveToWsClientRadioButton.Location = new System.Drawing.Point(18, 22);
         this.MoveToWsClientRadioButton.Name = "MoveToWsClientRadioButton";
         this.MoveToWsClientRadioButton.Size = new System.Drawing.Size(266, 17);
         this.MoveToWsClientRadioButton.TabIndex = 9;
         this.MoveToWsClientRadioButton.TabStop = true;
         this.MoveToWsClientRadioButton.Text = "Move DICOM Objects to Workstation Client";
         this.MoveToWsClientRadioButton.UseVisualStyleBackColor = true;
         // 
         // GenericErrorProvider
         // 
         this.GenericErrorProvider.ContainerControl = this;
         // 
         // groupBox1
         // 
         this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.groupBox1.Controls.Add(this.groupBox2);
         this.groupBox1.Controls.Add(this.DisplayOrientationButton);
         this.groupBox1.Controls.Add(this.FullScreenModeCheckBox);
         this.groupBox1.Controls.Add(this.ViewerOverlayTextSizeGroupBox);
         this.groupBox1.Controls.Add(this.LazyLoadingGroupBox);
         this.groupBox1.ForeColor = System.Drawing.Color.White;
         this.groupBox1.Location = new System.Drawing.Point(9, 233);
         this.groupBox1.Name = "groupBox1";
         this.groupBox1.Size = new System.Drawing.Size(974, 165);
         this.groupBox1.TabIndex = 9;
         this.groupBox1.TabStop = false;
         this.groupBox1.Text = "Viewer Options";
         // 
         // groupBox2
         // 
         this.groupBox2.Controls.Add(this.MeasurmentsUnitComboBox);
         this.groupBox2.Controls.Add(this.label3);
         this.groupBox2.Controls.Add(this.AnnotationColorButton);
         this.groupBox2.Controls.Add(this.label2);
         this.groupBox2.ForeColor = System.Drawing.Color.White;
         this.groupBox2.Location = new System.Drawing.Point(792, 19);
         this.groupBox2.Name = "groupBox2";
         this.groupBox2.Size = new System.Drawing.Size(176, 94);
         this.groupBox2.TabIndex = 13;
         this.groupBox2.TabStop = false;
         this.groupBox2.Text = "Annotations/Measurments";
         // 
         // MeasurmentsUnitComboBox
         // 
         this.MeasurmentsUnitComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.MeasurmentsUnitComboBox.FormattingEnabled = true;
         this.MeasurmentsUnitComboBox.Location = new System.Drawing.Point(48, 52);
         this.MeasurmentsUnitComboBox.Name = "MeasurmentsUnitComboBox";
         this.MeasurmentsUnitComboBox.Size = new System.Drawing.Size(121, 21);
         this.MeasurmentsUnitComboBox.TabIndex = 3;
         // 
         // label3
         // 
         this.label3.AutoSize = true;
         this.label3.Location = new System.Drawing.Point(8, 55);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(29, 13);
         this.label3.TabIndex = 2;
         this.label3.Text = "Unit:";
         // 
         // AnnotationColorButton
         // 
         this.AnnotationColorButton.BackColor = System.Drawing.Color.Yellow;
         this.AnnotationColorButton.ForeColor = System.Drawing.Color.Black;
         this.AnnotationColorButton.Location = new System.Drawing.Point(48, 19);
         this.AnnotationColorButton.Name = "AnnotationColorButton";
         this.AnnotationColorButton.Size = new System.Drawing.Size(75, 23);
         this.AnnotationColorButton.TabIndex = 1;
         this.AnnotationColorButton.Text = "...";
         this.AnnotationColorButton.UseVisualStyleBackColor = false;
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(8, 24);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(34, 13);
         this.label2.TabIndex = 0;
         this.label2.Text = "Color:";
         // 
         // DisplayOrientationButton
         // 
         this.DisplayOrientationButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
         this.DisplayOrientationButton.Location = new System.Drawing.Point(501, 119);
         this.DisplayOrientationButton.Name = "DisplayOrientationButton";
         this.DisplayOrientationButton.Size = new System.Drawing.Size(120, 27);
         this.DisplayOrientationButton.TabIndex = 12;
         this.DisplayOrientationButton.Text = "Display Orientation...";
         this.DisplayOrientationButton.UseVisualStyleBackColor = false;
         // 
         // FullScreenModeCheckBox
         // 
         this.FullScreenModeCheckBox.AutoSize = true;
         this.FullScreenModeCheckBox.Location = new System.Drawing.Point(501, 96);
         this.FullScreenModeCheckBox.Name = "FullScreenModeCheckBox";
         this.FullScreenModeCheckBox.Size = new System.Drawing.Size(285, 17);
         this.FullScreenModeCheckBox.TabIndex = 11;
         this.FullScreenModeCheckBox.Text = "Run Workstation Viewer in full screen mode on start up";
         this.FullScreenModeCheckBox.UseVisualStyleBackColor = true;
         // 
         // ViewerOverlayTextSizeGroupBox
         // 
         this.ViewerOverlayTextSizeGroupBox.Controls.Add(this.FixedOverlayTextSizeNumericUpDown);
         this.ViewerOverlayTextSizeGroupBox.Controls.Add(this.FixedOverlayTextSizeLabel);
         this.ViewerOverlayTextSizeGroupBox.Controls.Add(this.AutoSizeOverlayTextCheckBox);
         this.ViewerOverlayTextSizeGroupBox.ForeColor = System.Drawing.Color.White;
         this.ViewerOverlayTextSizeGroupBox.Location = new System.Drawing.Point(501, 19);
         this.ViewerOverlayTextSizeGroupBox.Name = "ViewerOverlayTextSizeGroupBox";
         this.ViewerOverlayTextSizeGroupBox.Size = new System.Drawing.Size(285, 68);
         this.ViewerOverlayTextSizeGroupBox.TabIndex = 10;
         this.ViewerOverlayTextSizeGroupBox.TabStop = false;
         // 
         // FixedOverlayTextSizeNumericUpDown
         // 
         this.FixedOverlayTextSizeNumericUpDown.Location = new System.Drawing.Point(181, 27);
         this.FixedOverlayTextSizeNumericUpDown.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
         this.FixedOverlayTextSizeNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
         this.FixedOverlayTextSizeNumericUpDown.Name = "FixedOverlayTextSizeNumericUpDown";
         this.FixedOverlayTextSizeNumericUpDown.Size = new System.Drawing.Size(56, 20);
         this.FixedOverlayTextSizeNumericUpDown.TabIndex = 2;
         this.FixedOverlayTextSizeNumericUpDown.Value = new decimal(new int[] {
            14,
            0,
            0,
            0});
         // 
         // FixedOverlayTextSizeLabel
         // 
         this.FixedOverlayTextSizeLabel.AutoSize = true;
         this.FixedOverlayTextSizeLabel.Location = new System.Drawing.Point(10, 29);
         this.FixedOverlayTextSizeLabel.Name = "FixedOverlayTextSizeLabel";
         this.FixedOverlayTextSizeLabel.Size = new System.Drawing.Size(168, 13);
         this.FixedOverlayTextSizeLabel.TabIndex = 1;
         this.FixedOverlayTextSizeLabel.Text = "Use fixed size for the overlay text: ";
         // 
         // AutoSizeOverlayTextCheckBox
         // 
         this.AutoSizeOverlayTextCheckBox.AutoSize = true;
         this.AutoSizeOverlayTextCheckBox.Location = new System.Drawing.Point(5, -2);
         this.AutoSizeOverlayTextCheckBox.Name = "AutoSizeOverlayTextCheckBox";
         this.AutoSizeOverlayTextCheckBox.Size = new System.Drawing.Size(134, 17);
         this.AutoSizeOverlayTextCheckBox.TabIndex = 0;
         this.AutoSizeOverlayTextCheckBox.Text = "Auto Size Overlay Text";
         this.AutoSizeOverlayTextCheckBox.UseVisualStyleBackColor = true;
         // 
         // LazyLoadingGroupBox
         // 
         this.LazyLoadingGroupBox.Controls.Add(this.EnableLazyLoadingCheckBox);
         this.LazyLoadingGroupBox.Controls.Add(this.LazyLoadingHiddenImagesMaskedTextBox);
         this.LazyLoadingGroupBox.Controls.Add(this.LazyLoadingHiddenLabel);
         this.LazyLoadingGroupBox.Controls.Add(this.LazyLoadingLoadLable);
         this.LazyLoadingGroupBox.Controls.Add(this.LazyLoadingDescriptionLabel);
         this.LazyLoadingGroupBox.Location = new System.Drawing.Point(10, 19);
         this.LazyLoadingGroupBox.Name = "LazyLoadingGroupBox";
         this.LazyLoadingGroupBox.Size = new System.Drawing.Size(484, 127);
         this.LazyLoadingGroupBox.TabIndex = 9;
         this.LazyLoadingGroupBox.TabStop = false;
         // 
         // EnableLazyLoadingCheckBox
         // 
         this.EnableLazyLoadingCheckBox.AutoSize = true;
         this.EnableLazyLoadingCheckBox.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
         this.EnableLazyLoadingCheckBox.Location = new System.Drawing.Point(6, 0);
         this.EnableLazyLoadingCheckBox.Name = "EnableLazyLoadingCheckBox";
         this.EnableLazyLoadingCheckBox.Size = new System.Drawing.Size(180, 17);
         this.EnableLazyLoadingCheckBox.TabIndex = 0;
         this.EnableLazyLoadingCheckBox.Text = "Enable Viewer Lazy Loading";
         this.EnableLazyLoadingCheckBox.UseVisualStyleBackColor = true;
         // 
         // LazyLoadingHiddenImagesMaskedTextBox
         // 
         this.LazyLoadingHiddenImagesMaskedTextBox.HidePromptOnLeave = true;
         this.LazyLoadingHiddenImagesMaskedTextBox.Location = new System.Drawing.Point(70, 78);
         this.LazyLoadingHiddenImagesMaskedTextBox.Mask = "00000";
         this.LazyLoadingHiddenImagesMaskedTextBox.Name = "LazyLoadingHiddenImagesMaskedTextBox";
         this.LazyLoadingHiddenImagesMaskedTextBox.PromptChar = '*';
         this.LazyLoadingHiddenImagesMaskedTextBox.Size = new System.Drawing.Size(54, 20);
         this.LazyLoadingHiddenImagesMaskedTextBox.TabIndex = 3;
         this.LazyLoadingHiddenImagesMaskedTextBox.ValidatingType = typeof(int);
         // 
         // LazyLoadingHiddenLabel
         // 
         this.LazyLoadingHiddenLabel.AutoSize = true;
         this.LazyLoadingHiddenLabel.Location = new System.Drawing.Point(129, 81);
         this.LazyLoadingHiddenLabel.Name = "LazyLoadingHiddenLabel";
         this.LazyLoadingHiddenLabel.Size = new System.Drawing.Size(227, 13);
         this.LazyLoadingHiddenLabel.TabIndex = 4;
         this.LazyLoadingHiddenLabel.Text = "hidden images above and under the scrollbars.";
         // 
         // LazyLoadingLoadLable
         // 
         this.LazyLoadingLoadLable.AutoSize = true;
         this.LazyLoadingLoadLable.Location = new System.Drawing.Point(30, 81);
         this.LazyLoadingLoadLable.Name = "LazyLoadingLoadLable";
         this.LazyLoadingLoadLable.Size = new System.Drawing.Size(31, 13);
         this.LazyLoadingLoadLable.TabIndex = 2;
         this.LazyLoadingLoadLable.Text = "Load";
         // 
         // LazyLoadingDescriptionLabel
         // 
         this.LazyLoadingDescriptionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.LazyLoadingDescriptionLabel.Location = new System.Drawing.Point(7, 24);
         this.LazyLoadingDescriptionLabel.Name = "LazyLoadingDescriptionLabel";
         this.LazyLoadingDescriptionLabel.Size = new System.Drawing.Size(469, 46);
         this.LazyLoadingDescriptionLabel.TabIndex = 1;
         this.LazyLoadingDescriptionLabel.Text = resources.GetString("LazyLoadingDescriptionLabel.Text");
         // 
         // UseCompressionCheckBox
         // 
         this.UseCompressionCheckBox.AutoSize = true;
         this.UseCompressionCheckBox.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
         this.UseCompressionCheckBox.ForeColor = System.Drawing.Color.White;
         this.UseCompressionCheckBox.Location = new System.Drawing.Point(519, 149);
         this.UseCompressionCheckBox.Name = "UseCompressionCheckBox";
         this.UseCompressionCheckBox.Size = new System.Drawing.Size(378, 17);
         this.UseCompressionCheckBox.TabIndex = 14;
         this.UseCompressionCheckBox.Text = "Use compression when storing DICOM files into DICOM Servers";
         this.UseCompressionCheckBox.UseVisualStyleBackColor = true;
         // 
         // SaveSessionGroupBox
         // 
         this.SaveSessionGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.SaveSessionGroupBox.Controls.Add(this.PromptUserSaveSessionRadioButton);
         this.SaveSessionGroupBox.Controls.Add(this.NeverSaveSessionRadioButton);
         this.SaveSessionGroupBox.Controls.Add(this.AlwaysSaveSessionRadioButton);
         this.SaveSessionGroupBox.Controls.Add(this.SaveUserSessionLabel);
         this.SaveSessionGroupBox.ForeColor = System.Drawing.Color.White;
         this.SaveSessionGroupBox.Location = new System.Drawing.Point(510, 405);
         this.SaveSessionGroupBox.Name = "SaveSessionGroupBox";
         this.SaveSessionGroupBox.Size = new System.Drawing.Size(473, 124);
         this.SaveSessionGroupBox.TabIndex = 15;
         this.SaveSessionGroupBox.TabStop = false;
         this.SaveSessionGroupBox.Text = "Save User Session";
         // 
         // PromptUserSaveSessionRadioButton
         // 
         this.PromptUserSaveSessionRadioButton.AutoSize = true;
         this.PromptUserSaveSessionRadioButton.Location = new System.Drawing.Point(18, 97);
         this.PromptUserSaveSessionRadioButton.Name = "PromptUserSaveSessionRadioButton";
         this.PromptUserSaveSessionRadioButton.Size = new System.Drawing.Size(83, 17);
         this.PromptUserSaveSessionRadioButton.TabIndex = 18;
         this.PromptUserSaveSessionRadioButton.TabStop = true;
         this.PromptUserSaveSessionRadioButton.Text = "Prompt User";
         this.PromptUserSaveSessionRadioButton.UseVisualStyleBackColor = true;
         // 
         // NeverSaveSessionRadioButton
         // 
         this.NeverSaveSessionRadioButton.AutoSize = true;
         this.NeverSaveSessionRadioButton.Location = new System.Drawing.Point(18, 73);
         this.NeverSaveSessionRadioButton.Name = "NeverSaveSessionRadioButton";
         this.NeverSaveSessionRadioButton.Size = new System.Drawing.Size(54, 17);
         this.NeverSaveSessionRadioButton.TabIndex = 17;
         this.NeverSaveSessionRadioButton.TabStop = true;
         this.NeverSaveSessionRadioButton.Text = "Never";
         this.NeverSaveSessionRadioButton.UseVisualStyleBackColor = true;
         // 
         // AlwaysSaveSessionRadioButton
         // 
         this.AlwaysSaveSessionRadioButton.AutoSize = true;
         this.AlwaysSaveSessionRadioButton.Location = new System.Drawing.Point(18, 49);
         this.AlwaysSaveSessionRadioButton.Name = "AlwaysSaveSessionRadioButton";
         this.AlwaysSaveSessionRadioButton.Size = new System.Drawing.Size(58, 17);
         this.AlwaysSaveSessionRadioButton.TabIndex = 16;
         this.AlwaysSaveSessionRadioButton.TabStop = true;
         this.AlwaysSaveSessionRadioButton.Text = "Always";
         this.AlwaysSaveSessionRadioButton.UseVisualStyleBackColor = true;
         // 
         // SaveUserSessionLabel
         // 
         this.SaveUserSessionLabel.AutoSize = true;
         this.SaveUserSessionLabel.Location = new System.Drawing.Point(10, 27);
         this.SaveUserSessionLabel.Name = "SaveUserSessionLabel";
         this.SaveUserSessionLabel.Size = new System.Drawing.Size(386, 13);
         this.SaveUserSessionLabel.TabIndex = 0;
         this.SaveUserSessionLabel.Text = "When user exits the workstation, do you want to save the session configuration?";
         // 
         // WorkstationSecureCheckBox
         // 
         this.WorkstationSecureCheckBox.AutoSize = true;
         this.WorkstationSecureCheckBox.Location = new System.Drawing.Point(204, 130);
         this.WorkstationSecureCheckBox.Name = "WorkstationSecureCheckBox";
         this.WorkstationSecureCheckBox.Size = new System.Drawing.Size(98, 17);
         this.WorkstationSecureCheckBox.TabIndex = 13;
         this.WorkstationSecureCheckBox.Text = "Secure (TLS)";
         this.WorkstationSecureCheckBox.UseVisualStyleBackColor = true;
         // 
         // ClientConfiguration
         // 
         this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
         this.Controls.Add(this.SaveSessionGroupBox);
         this.Controls.Add(this.UseCompressionCheckBox);
         this.Controls.Add(this.groupBox1);
         this.Controls.Add(this.DicomRetrieveGroupBox);
         this.Controls.Add(this.EnableDebugCheckBox);
         this.Controls.Add(this.DicomClientGroupBox);
         this.Controls.Add(this.CompressionGroupBox);
         this.Controls.Add(this.DebugGroupBox);
         this.ForeColor = System.Drawing.Color.White;
         this.Name = "ClientConfiguration";
         this.Size = new System.Drawing.Size(986, 534);
         this.CompressionGroupBox.ResumeLayout(false);
         this.CompressionGroupBox.PerformLayout();
         this.DebugGroupBox.ResumeLayout(false);
         this.DebugGroupBox.PerformLayout();
         this.DicomClientGroupBox.ResumeLayout(false);
         this.DicomClientGroupBox.PerformLayout();
         this.DicomRetrieveGroupBox.ResumeLayout(false);
         this.DicomRetrieveGroupBox.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.GenericErrorProvider)).EndInit();
         this.groupBox1.ResumeLayout(false);
         this.groupBox1.PerformLayout();
         this.groupBox2.ResumeLayout(false);
         this.groupBox2.PerformLayout();
         this.ViewerOverlayTextSizeGroupBox.ResumeLayout(false);
         this.ViewerOverlayTextSizeGroupBox.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.FixedOverlayTextSizeNumericUpDown)).EndInit();
         this.LazyLoadingGroupBox.ResumeLayout(false);
         this.LazyLoadingGroupBox.PerformLayout();
         this.SaveSessionGroupBox.ResumeLayout(false);
         this.SaveSessionGroupBox.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      protected System.Windows.Forms.GroupBox DicomClientGroupBox;
      protected System.Windows.Forms.GroupBox CompressionGroupBox;
      protected System.Windows.Forms.RadioButton LossyCompressionRadioButton;
      protected System.Windows.Forms.RadioButton LosslessCompressionRadioButton;
      protected System.Windows.Forms.GroupBox DebugGroupBox;
      protected System.Windows.Forms.Button DebugLogFileButton;
      protected System.Windows.Forms.TextBox DebugLogFileNameTextBox;
      protected System.Windows.Forms.CheckBox DebugLogFileCheckBox;
      protected System.Windows.Forms.CheckBox DisplayDetailDebugMsgCheckBox;
      protected System.Windows.Forms.ComboBox WorkstationHostaddressComboBox;
      protected System.Windows.Forms.Label WorkstationIPLabel;
      protected System.Windows.Forms.Label DicomClientDescriptionLabel;
      protected System.Windows.Forms.Label WorstationAELabel;
      protected System.Windows.Forms.TextBox WorkstationAETextBox;
      protected System.Windows.Forms.CheckBox EnableDebugCheckBox;
      protected System.Windows.Forms.SaveFileDialog BrowseLogFileDialog;
      protected System.Windows.Forms.ErrorProvider GenericErrorProvider;
      protected System.Windows.Forms.GroupBox DicomRetrieveGroupBox;
      protected System.Windows.Forms.CheckBox ContinueRetireveOnDuplicateCheckBox;
      protected System.Windows.Forms.GroupBox LazyLoadingGroupBox;
      protected System.Windows.Forms.CheckBox EnableLazyLoadingCheckBox;
      protected System.Windows.Forms.MaskedTextBox LazyLoadingHiddenImagesMaskedTextBox;
      protected System.Windows.Forms.Label LazyLoadingHiddenLabel;
      protected System.Windows.Forms.Label LazyLoadingLoadLable;
      protected System.Windows.Forms.Label LazyLoadingDescriptionLabel;
      protected System.Windows.Forms.CheckBox AutoSizeOverlayTextCheckBox;
      protected System.Windows.Forms.Label FixedOverlayTextSizeLabel;
      protected System.Windows.Forms.GroupBox groupBox1;
      protected System.Windows.Forms.GroupBox ViewerOverlayTextSizeGroupBox;
      protected System.Windows.Forms.NumericUpDown FixedOverlayTextSizeNumericUpDown;
      private System.Windows.Forms.CheckBox FullScreenModeCheckBox;
      protected System.Windows.Forms.Label WorkstationPortLabel;
      protected System.Windows.Forms.MaskedTextBox WorkstationPortMaskedTextBox;
      private System.Windows.Forms.RadioButton MoveToWsServiceRadioButton;
      private System.Windows.Forms.RadioButton MoveToWsClientRadioButton;
      protected System.Windows.Forms.Label label1;
      protected System.Windows.Forms.TextBox WorkstationServiceAETitleTextBox;
      protected System.Windows.Forms.CheckBox UseCompressionCheckBox;
      private System.Windows.Forms.CheckBox ForceToAllClientsCheckBox;
      private System.Windows.Forms.GroupBox SaveSessionGroupBox;
      private System.Windows.Forms.RadioButton PromptUserSaveSessionRadioButton;
      private System.Windows.Forms.RadioButton NeverSaveSessionRadioButton;
      private System.Windows.Forms.RadioButton AlwaysSaveSessionRadioButton;
      private System.Windows.Forms.Label SaveUserSessionLabel;
      private System.Windows.Forms.Button DisplayOrientationButton;
      private System.Windows.Forms.GroupBox groupBox2;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Button AnnotationColorButton;
      private System.Windows.Forms.ComboBox MeasurmentsUnitComboBox;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.CheckBox WorkstationSecureCheckBox;
   }
}
