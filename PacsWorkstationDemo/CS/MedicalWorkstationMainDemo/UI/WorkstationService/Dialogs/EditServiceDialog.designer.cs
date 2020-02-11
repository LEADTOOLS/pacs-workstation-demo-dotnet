namespace Leadtools.Demos.Workstation
{
    partial class EditServiceDialog
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
           this.components = new System.ComponentModel.Container();
           this.AeTitleLabel = new System.Windows.Forms.Label();
           this.ServerAETextBox = new System.Windows.Forms.TextBox();
           this.DescriptionLabel = new System.Windows.Forms.Label();
           this.ServerDescriptionTextBox = new System.Windows.Forms.TextBox();
           this.IpAddressLabel = new System.Windows.Forms.Label();
           this.PortLabel = new System.Windows.Forms.Label();
           this.ClientTimeoutNumericUpDown = new System.Windows.Forms.NumericUpDown();
           this.ClientTimeoutLabel = new System.Windows.Forms.Label();
           this.MaxClientsLabel = new System.Windows.Forms.Label();
           this.ServerAllowAnonymousCheckBox = new System.Windows.Forms.CheckBox();
           this.OkButton = new System.Windows.Forms.Button();
           this.CancelDialogButton = new System.Windows.Forms.Button();
           this.ServerSecureCheckBox = new System.Windows.Forms.CheckBox();
           this.ImplementationClassTextBox = new System.Windows.Forms.TextBox();
           this.ImplementationClassLabel = new System.Windows.Forms.Label();
           this.ImplementationVersionNameTextBox = new System.Windows.Forms.TextBox();
           this.ImplementationVersionNameLabel = new System.Windows.Forms.Label();
           this.TemporaryDirectoryTextBox = new System.Windows.Forms.TextBox();
           this.TemporaryDirectoryLabel = new System.Windows.Forms.Label();
           this.TempFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
           this.TempFolderBrowseButton = new System.Windows.Forms.Button();
           this.ServerIpComboBox = new System.Windows.Forms.ComboBox();
           this.ServerTabControl = new System.Windows.Forms.TabControl();
           this.SettingsTabPage = new System.Windows.Forms.TabPage();
           this.ServerMaxClientsMaskedNumeric = new System.Windows.Forms.NumericUpDown();
           this.ServerPortMaskedNumeric = new System.Windows.Forms.NumericUpDown();
           this.DisplayNameLabel = new System.Windows.Forms.Label();
           this.DisplayNameTextBox = new System.Windows.Forms.TextBox();
           this.AllowMultipleConnectionsChcekBox = new System.Windows.Forms.CheckBox();
           this.AdvancedTabPage = new System.Windows.Forms.TabPage();
           this.ServiceNameLabel = new System.Windows.Forms.Label();
           this.ServiceNameTextBox = new System.Windows.Forms.TextBox();
           this.StartModeComboBox = new System.Windows.Forms.ComboBox();
           this.StartModeLabel = new System.Windows.Forms.Label();
           this.SocketOptionsGroupBox = new System.Windows.Forms.GroupBox();
           this.SendBufferNumericUpDown = new System.Windows.Forms.NumericUpDown();
           this.ReceiveBufferNumericUpDown = new System.Windows.Forms.NumericUpDown();
           this.SendBufferLabel = new System.Windows.Forms.Label();
           this.ReceiveBufferLabel = new System.Windows.Forms.Label();
           this.NoDelayCheckBox = new System.Windows.Forms.CheckBox();
           this.MaxPduLengthMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
           this.MaxPduLabel = new System.Windows.Forms.Label();
           this.TimeoutGroupBox = new System.Windows.Forms.GroupBox();
           this.ReconnectTimeoutNumericUpDown = new System.Windows.Forms.NumericUpDown();
           this.AddInTimeoutNumericUpDown = new System.Windows.Forms.NumericUpDown();
           this.AddInTimeoutLabel = new System.Windows.Forms.Label();
           this.ReconnectTimeoutLabel = new System.Windows.Forms.Label();
           this.ErrorLabel = new System.Windows.Forms.Label();
           this.RestartLabel = new System.Windows.Forms.Label();
           this.ControlsErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
           ((System.ComponentModel.ISupportInitialize)(this.ClientTimeoutNumericUpDown)).BeginInit();
           this.ServerTabControl.SuspendLayout();
           this.SettingsTabPage.SuspendLayout();
           ((System.ComponentModel.ISupportInitialize)(this.ServerMaxClientsMaskedNumeric)).BeginInit();
           ((System.ComponentModel.ISupportInitialize)(this.ServerPortMaskedNumeric)).BeginInit();
           this.AdvancedTabPage.SuspendLayout();
           this.SocketOptionsGroupBox.SuspendLayout();
           ((System.ComponentModel.ISupportInitialize)(this.SendBufferNumericUpDown)).BeginInit();
           ((System.ComponentModel.ISupportInitialize)(this.ReceiveBufferNumericUpDown)).BeginInit();
           this.TimeoutGroupBox.SuspendLayout();
           ((System.ComponentModel.ISupportInitialize)(this.ReconnectTimeoutNumericUpDown)).BeginInit();
           ((System.ComponentModel.ISupportInitialize)(this.AddInTimeoutNumericUpDown)).BeginInit();
           ((System.ComponentModel.ISupportInitialize)(this.ControlsErrorProvider)).BeginInit();
           this.SuspendLayout();
           // 
           // AeTitleLabel
           // 
           this.AeTitleLabel.AutoSize = true;
           this.AeTitleLabel.Location = new System.Drawing.Point(6, 10);
           this.AeTitleLabel.Name = "AeTitleLabel";
           this.AeTitleLabel.Size = new System.Drawing.Size(47, 13);
           this.AeTitleLabel.TabIndex = 0;
           this.AeTitleLabel.Text = "AE Title:";
           // 
           // ServerAETextBox
           // 
           this.ServerAETextBox.Location = new System.Drawing.Point(9, 26);
           this.ServerAETextBox.Name = "ServerAETextBox";
           this.ServerAETextBox.Size = new System.Drawing.Size(251, 20);
           this.ServerAETextBox.TabIndex = 1;
           this.ServerAETextBox.TextChanged += new System.EventHandler(this.ServerAE_TextChanged);
           this.ServerAETextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ServerAE_KeyPress);
           // 
           // DescriptionLabel
           // 
           this.DescriptionLabel.AutoSize = true;
           this.DescriptionLabel.Location = new System.Drawing.Point(6, 52);
           this.DescriptionLabel.Name = "DescriptionLabel";
           this.DescriptionLabel.Size = new System.Drawing.Size(63, 13);
           this.DescriptionLabel.TabIndex = 2;
           this.DescriptionLabel.Text = "Description:";
           // 
           // ServerDescriptionTextBox
           // 
           this.ServerDescriptionTextBox.Location = new System.Drawing.Point(9, 69);
           this.ServerDescriptionTextBox.Multiline = true;
           this.ServerDescriptionTextBox.Name = "ServerDescriptionTextBox";
           this.ServerDescriptionTextBox.Size = new System.Drawing.Size(251, 102);
           this.ServerDescriptionTextBox.TabIndex = 3;
           // 
           // IpAddressLabel
           // 
           this.IpAddressLabel.AutoSize = true;
           this.IpAddressLabel.Location = new System.Drawing.Point(6, 175);
           this.IpAddressLabel.Name = "IpAddressLabel";
           this.IpAddressLabel.Size = new System.Drawing.Size(61, 13);
           this.IpAddressLabel.TabIndex = 4;
           this.IpAddressLabel.Text = "IP Address:";
           // 
           // PortLabel
           // 
           this.PortLabel.AutoSize = true;
           this.PortLabel.Location = new System.Drawing.Point(6, 221);
           this.PortLabel.Name = "PortLabel";
           this.PortLabel.Size = new System.Drawing.Size(29, 13);
           this.PortLabel.TabIndex = 6;
           this.PortLabel.Text = "Port:";
           // 
           // ClientTimeoutNumericUpDown
           // 
           this.ClientTimeoutNumericUpDown.Location = new System.Drawing.Point(9, 32);
           this.ClientTimeoutNumericUpDown.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
           this.ClientTimeoutNumericUpDown.Name = "ClientTimeoutNumericUpDown";
           this.ClientTimeoutNumericUpDown.Size = new System.Drawing.Size(74, 20);
           this.ClientTimeoutNumericUpDown.TabIndex = 21;
           // 
           // ClientTimeoutLabel
           // 
           this.ClientTimeoutLabel.AutoSize = true;
           this.ClientTimeoutLabel.Location = new System.Drawing.Point(6, 16);
           this.ClientTimeoutLabel.Name = "ClientTimeoutLabel";
           this.ClientTimeoutLabel.Size = new System.Drawing.Size(36, 13);
           this.ClientTimeoutLabel.TabIndex = 20;
           this.ClientTimeoutLabel.Text = "Client:";
           // 
           // MaxClientsLabel
           // 
           this.MaxClientsLabel.AutoSize = true;
           this.MaxClientsLabel.Location = new System.Drawing.Point(137, 221);
           this.MaxClientsLabel.Name = "MaxClientsLabel";
           this.MaxClientsLabel.Size = new System.Drawing.Size(64, 13);
           this.MaxClientsLabel.TabIndex = 8;
           this.MaxClientsLabel.Text = "Max Clients:";
           // 
           // ServerAllowAnonymousCheckBox
           // 
           this.ServerAllowAnonymousCheckBox.AutoSize = true;
           this.ServerAllowAnonymousCheckBox.Location = new System.Drawing.Point(277, 217);
           this.ServerAllowAnonymousCheckBox.Name = "ServerAllowAnonymousCheckBox";
           this.ServerAllowAnonymousCheckBox.Size = new System.Drawing.Size(109, 17);
           this.ServerAllowAnonymousCheckBox.TabIndex = 20;
           this.ServerAllowAnonymousCheckBox.Text = "Allow Anonymous";
           this.ServerAllowAnonymousCheckBox.UseVisualStyleBackColor = true;
           // 
           // OkButton
           // 
           this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
           this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
           this.OkButton.Location = new System.Drawing.Point(376, 328);
           this.OkButton.Name = "OkButton";
           this.OkButton.Size = new System.Drawing.Size(75, 23);
           this.OkButton.TabIndex = 3;
           this.OkButton.Text = "OK";
           this.OkButton.UseVisualStyleBackColor = true;
           // 
           // CancelDialogButton
           // 
           this.CancelDialogButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
           this.CancelDialogButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
           this.CancelDialogButton.Location = new System.Drawing.Point(457, 328);
           this.CancelDialogButton.Name = "CancelDialogButton";
           this.CancelDialogButton.Size = new System.Drawing.Size(75, 23);
           this.CancelDialogButton.TabIndex = 4;
           this.CancelDialogButton.Text = "Cancel";
           this.CancelDialogButton.UseVisualStyleBackColor = true;
           // 
           // ServerSecureCheckBox
           // 
           this.ServerSecureCheckBox.AutoSize = true;
           this.ServerSecureCheckBox.Location = new System.Drawing.Point(277, 195);
           this.ServerSecureCheckBox.Name = "ServerSecureCheckBox";
           this.ServerSecureCheckBox.Size = new System.Drawing.Size(60, 17);
           this.ServerSecureCheckBox.TabIndex = 19;
           this.ServerSecureCheckBox.Text = "Secure";
           this.ServerSecureCheckBox.UseVisualStyleBackColor = true;
           // 
           // ImplementationClassTextBox
           // 
           this.ImplementationClassTextBox.Location = new System.Drawing.Point(277, 68);
           this.ImplementationClassTextBox.MaxLength = 64;
           this.ImplementationClassTextBox.Name = "ImplementationClassTextBox";
           this.ImplementationClassTextBox.Size = new System.Drawing.Size(251, 20);
           this.ImplementationClassTextBox.TabIndex = 13;
           // 
           // ImplementationClassLabel
           // 
           this.ImplementationClassLabel.AutoSize = true;
           this.ImplementationClassLabel.Location = new System.Drawing.Point(274, 52);
           this.ImplementationClassLabel.Name = "ImplementationClassLabel";
           this.ImplementationClassLabel.Size = new System.Drawing.Size(131, 13);
           this.ImplementationClassLabel.TabIndex = 12;
           this.ImplementationClassLabel.Text = "Implementation Class UID:";
           // 
           // ImplementationVersionNameTextBox
           // 
           this.ImplementationVersionNameTextBox.Location = new System.Drawing.Point(277, 107);
           this.ImplementationVersionNameTextBox.MaxLength = 16;
           this.ImplementationVersionNameTextBox.Name = "ImplementationVersionNameTextBox";
           this.ImplementationVersionNameTextBox.Size = new System.Drawing.Size(251, 20);
           this.ImplementationVersionNameTextBox.TabIndex = 15;
           // 
           // ImplementationVersionNameLabel
           // 
           this.ImplementationVersionNameLabel.AutoSize = true;
           this.ImplementationVersionNameLabel.Location = new System.Drawing.Point(274, 91);
           this.ImplementationVersionNameLabel.Name = "ImplementationVersionNameLabel";
           this.ImplementationVersionNameLabel.Size = new System.Drawing.Size(150, 13);
           this.ImplementationVersionNameLabel.TabIndex = 14;
           this.ImplementationVersionNameLabel.Text = "Implementation Version Name:";
           // 
           // TemporaryDirectoryTextBox
           // 
           this.TemporaryDirectoryTextBox.Location = new System.Drawing.Point(277, 146);
           this.TemporaryDirectoryTextBox.Name = "TemporaryDirectoryTextBox";
           this.TemporaryDirectoryTextBox.Size = new System.Drawing.Size(217, 20);
           this.TemporaryDirectoryTextBox.TabIndex = 17;
           this.TemporaryDirectoryTextBox.TextChanged += new System.EventHandler(this.TemporaryDirectory_TextChanged);
           this.TemporaryDirectoryTextBox.Leave += new System.EventHandler(this.TemporaryDirectory_Leave);
           // 
           // TemporaryDirectoryLabel
           // 
           this.TemporaryDirectoryLabel.AutoSize = true;
           this.TemporaryDirectoryLabel.Location = new System.Drawing.Point(274, 130);
           this.TemporaryDirectoryLabel.Name = "TemporaryDirectoryLabel";
           this.TemporaryDirectoryLabel.Size = new System.Drawing.Size(105, 13);
           this.TemporaryDirectoryLabel.TabIndex = 16;
           this.TemporaryDirectoryLabel.Text = "Temporary Directory:";
           // 
           // TempFolderBrowseButton
           // 
           this.TempFolderBrowseButton.Image = global::Leadtools.Demos.Workstation.Properties.Resources.folder;
           this.TempFolderBrowseButton.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
           this.TempFolderBrowseButton.Location = new System.Drawing.Point(495, 143);
           this.TempFolderBrowseButton.Name = "TempFolderBrowseButton";
           this.TempFolderBrowseButton.Size = new System.Drawing.Size(32, 24);
           this.TempFolderBrowseButton.TabIndex = 18;
           this.TempFolderBrowseButton.UseVisualStyleBackColor = true;
           this.TempFolderBrowseButton.Click += new System.EventHandler(this.buttonFolderBrowse_Click);
           // 
           // ServerIpComboBox
           // 
           this.ServerIpComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
           this.ServerIpComboBox.FormattingEnabled = true;
           this.ServerIpComboBox.Location = new System.Drawing.Point(9, 191);
           this.ServerIpComboBox.Name = "ServerIpComboBox";
           this.ServerIpComboBox.Size = new System.Drawing.Size(251, 21);
           this.ServerIpComboBox.TabIndex = 5;
           // 
           // ServerTabControl
           // 
           this.ServerTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                       | System.Windows.Forms.AnchorStyles.Left)
                       | System.Windows.Forms.AnchorStyles.Right)));
           this.ServerTabControl.Controls.Add(this.SettingsTabPage);
           this.ServerTabControl.Controls.Add(this.AdvancedTabPage);
           this.ServerTabControl.Location = new System.Drawing.Point(0, 0);
           this.ServerTabControl.Name = "ServerTabControl";
           this.ServerTabControl.SelectedIndex = 0;
           this.ServerTabControl.Size = new System.Drawing.Size(542, 294);
           this.ServerTabControl.TabIndex = 0;
           // 
           // SettingsTabPage
           // 
           this.SettingsTabPage.Controls.Add(this.ServerMaxClientsMaskedNumeric);
           this.SettingsTabPage.Controls.Add(this.ServerPortMaskedNumeric);
           this.SettingsTabPage.Controls.Add(this.DisplayNameLabel);
           this.SettingsTabPage.Controls.Add(this.DisplayNameTextBox);
           this.SettingsTabPage.Controls.Add(this.AllowMultipleConnectionsChcekBox);
           this.SettingsTabPage.Controls.Add(this.TemporaryDirectoryLabel);
           this.SettingsTabPage.Controls.Add(this.ServerIpComboBox);
           this.SettingsTabPage.Controls.Add(this.AeTitleLabel);
           this.SettingsTabPage.Controls.Add(this.TempFolderBrowseButton);
           this.SettingsTabPage.Controls.Add(this.ServerAETextBox);
           this.SettingsTabPage.Controls.Add(this.DescriptionLabel);
           this.SettingsTabPage.Controls.Add(this.ServerDescriptionTextBox);
           this.SettingsTabPage.Controls.Add(this.IpAddressLabel);
           this.SettingsTabPage.Controls.Add(this.PortLabel);
           this.SettingsTabPage.Controls.Add(this.TemporaryDirectoryTextBox);
           this.SettingsTabPage.Controls.Add(this.MaxClientsLabel);
           this.SettingsTabPage.Controls.Add(this.ImplementationVersionNameTextBox);
           this.SettingsTabPage.Controls.Add(this.ImplementationVersionNameLabel);
           this.SettingsTabPage.Controls.Add(this.ServerAllowAnonymousCheckBox);
           this.SettingsTabPage.Controls.Add(this.ImplementationClassTextBox);
           this.SettingsTabPage.Controls.Add(this.ImplementationClassLabel);
           this.SettingsTabPage.Controls.Add(this.ServerSecureCheckBox);
           this.SettingsTabPage.Location = new System.Drawing.Point(4, 22);
           this.SettingsTabPage.Name = "SettingsTabPage";
           this.SettingsTabPage.Padding = new System.Windows.Forms.Padding(3);
           this.SettingsTabPage.Size = new System.Drawing.Size(534, 268);
           this.SettingsTabPage.TabIndex = 0;
           this.SettingsTabPage.Text = "Settings";
           this.SettingsTabPage.UseVisualStyleBackColor = true;
           // 
           // ServerMaxClientsMaskedNumeric
           // 
           this.ServerMaxClientsMaskedNumeric.Location = new System.Drawing.Point(140, 237);
           this.ServerMaxClientsMaskedNumeric.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
           this.ServerMaxClientsMaskedNumeric.Name = "ServerMaxClientsMaskedNumeric";
           this.ServerMaxClientsMaskedNumeric.Size = new System.Drawing.Size(120, 20);
           this.ServerMaxClientsMaskedNumeric.TabIndex = 9;
           this.ServerMaxClientsMaskedNumeric.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
           // 
           // ServerPortMaskedNumeric
           // 
           this.ServerPortMaskedNumeric.Location = new System.Drawing.Point(9, 237);
           this.ServerPortMaskedNumeric.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
           this.ServerPortMaskedNumeric.Name = "ServerPortMaskedNumeric";
           this.ServerPortMaskedNumeric.Size = new System.Drawing.Size(120, 20);
           this.ServerPortMaskedNumeric.TabIndex = 7;
           this.ServerPortMaskedNumeric.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
           // 
           // DisplayNameLabel
           // 
           this.DisplayNameLabel.AutoSize = true;
           this.DisplayNameLabel.Location = new System.Drawing.Point(274, 10);
           this.DisplayNameLabel.Name = "DisplayNameLabel";
           this.DisplayNameLabel.Size = new System.Drawing.Size(114, 13);
           this.DisplayNameLabel.TabIndex = 10;
           this.DisplayNameLabel.Text = "Service Display Name:";
           // 
           // DisplayNameTextBox
           // 
           this.DisplayNameTextBox.Location = new System.Drawing.Point(277, 26);
           this.DisplayNameTextBox.Name = "DisplayNameTextBox";
           this.DisplayNameTextBox.Size = new System.Drawing.Size(251, 20);
           this.DisplayNameTextBox.TabIndex = 11;
           this.DisplayNameTextBox.TextChanged += new System.EventHandler(this.DisplayName_TextChanged);
           this.DisplayNameTextBox.Leave += new System.EventHandler(this.DisplayName_Leave);
           // 
           // AllowMultipleConnectionsChcekBox
           // 
           this.AllowMultipleConnectionsChcekBox.AutoSize = true;
           this.AllowMultipleConnectionsChcekBox.Location = new System.Drawing.Point(277, 239);
           this.AllowMultipleConnectionsChcekBox.Name = "AllowMultipleConnectionsChcekBox";
           this.AllowMultipleConnectionsChcekBox.Size = new System.Drawing.Size(124, 17);
           this.AllowMultipleConnectionsChcekBox.TabIndex = 21;
           this.AllowMultipleConnectionsChcekBox.Text = "Multiple Connections";
           this.AllowMultipleConnectionsChcekBox.UseVisualStyleBackColor = true;
           // 
           // AdvancedTabPage
           // 
           this.AdvancedTabPage.Controls.Add(this.ServiceNameLabel);
           this.AdvancedTabPage.Controls.Add(this.ServiceNameTextBox);
           this.AdvancedTabPage.Controls.Add(this.StartModeComboBox);
           this.AdvancedTabPage.Controls.Add(this.StartModeLabel);
           this.AdvancedTabPage.Controls.Add(this.SocketOptionsGroupBox);
           this.AdvancedTabPage.Controls.Add(this.MaxPduLengthMaskedTextBox);
           this.AdvancedTabPage.Controls.Add(this.MaxPduLabel);
           this.AdvancedTabPage.Controls.Add(this.TimeoutGroupBox);
           this.AdvancedTabPage.Location = new System.Drawing.Point(4, 22);
           this.AdvancedTabPage.Name = "AdvancedTabPage";
           this.AdvancedTabPage.Padding = new System.Windows.Forms.Padding(3);
           this.AdvancedTabPage.Size = new System.Drawing.Size(534, 268);
           this.AdvancedTabPage.TabIndex = 1;
           this.AdvancedTabPage.Text = "Advanced";
           this.AdvancedTabPage.UseVisualStyleBackColor = true;
           // 
           // ServiceNameLabel
           // 
           this.ServiceNameLabel.AutoSize = true;
           this.ServiceNameLabel.Location = new System.Drawing.Point(9, 12);
           this.ServiceNameLabel.Name = "ServiceNameLabel";
           this.ServiceNameLabel.Size = new System.Drawing.Size(77, 13);
           this.ServiceNameLabel.TabIndex = 5;
           this.ServiceNameLabel.Text = "Service Name:";
           // 
           // ServiceNameTextBox
           // 
           this.ServiceNameTextBox.Enabled = false;
           this.ServiceNameTextBox.Location = new System.Drawing.Point(8, 30);
           this.ServiceNameTextBox.Name = "ServiceNameTextBox";
           this.ServiceNameTextBox.Size = new System.Drawing.Size(248, 20);
           this.ServiceNameTextBox.TabIndex = 6;
           // 
           // StartModeComboBox
           // 
           this.StartModeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
           this.StartModeComboBox.FormattingEnabled = true;
           this.StartModeComboBox.Items.AddRange(new object[] {
            "Automatic",
            "Manual",
            "Disabled"});
           this.StartModeComboBox.Location = new System.Drawing.Point(12, 195);
           this.StartModeComboBox.Name = "StartModeComboBox";
           this.StartModeComboBox.Size = new System.Drawing.Size(160, 21);
           this.StartModeComboBox.TabIndex = 31;
           // 
           // StartModeLabel
           // 
           this.StartModeLabel.AutoSize = true;
           this.StartModeLabel.Location = new System.Drawing.Point(9, 174);
           this.StartModeLabel.Name = "StartModeLabel";
           this.StartModeLabel.Size = new System.Drawing.Size(62, 13);
           this.StartModeLabel.TabIndex = 30;
           this.StartModeLabel.Text = "Start Mode:";
           // 
           // SocketOptionsGroupBox
           // 
           this.SocketOptionsGroupBox.Controls.Add(this.SendBufferNumericUpDown);
           this.SocketOptionsGroupBox.Controls.Add(this.ReceiveBufferNumericUpDown);
           this.SocketOptionsGroupBox.Controls.Add(this.SendBufferLabel);
           this.SocketOptionsGroupBox.Controls.Add(this.ReceiveBufferLabel);
           this.SocketOptionsGroupBox.Controls.Add(this.NoDelayCheckBox);
           this.SocketOptionsGroupBox.Location = new System.Drawing.Point(277, 101);
           this.SocketOptionsGroupBox.Name = "SocketOptionsGroupBox";
           this.SocketOptionsGroupBox.Size = new System.Drawing.Size(251, 66);
           this.SocketOptionsGroupBox.TabIndex = 29;
           this.SocketOptionsGroupBox.TabStop = false;
           this.SocketOptionsGroupBox.Text = "Socket Options";
           // 
           // SendBufferNumericUpDown
           // 
           this.SendBufferNumericUpDown.Location = new System.Drawing.Point(165, 31);
           this.SendBufferNumericUpDown.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
           this.SendBufferNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
           this.SendBufferNumericUpDown.Name = "SendBufferNumericUpDown";
           this.SendBufferNumericUpDown.Size = new System.Drawing.Size(76, 20);
           this.SendBufferNumericUpDown.TabIndex = 21;
           this.SendBufferNumericUpDown.Value = new decimal(new int[] {
            29696,
            0,
            0,
            0});
           // 
           // ReceiveBufferNumericUpDown
           // 
           this.ReceiveBufferNumericUpDown.Location = new System.Drawing.Point(84, 31);
           this.ReceiveBufferNumericUpDown.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
           this.ReceiveBufferNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
           this.ReceiveBufferNumericUpDown.Name = "ReceiveBufferNumericUpDown";
           this.ReceiveBufferNumericUpDown.Size = new System.Drawing.Size(76, 20);
           this.ReceiveBufferNumericUpDown.TabIndex = 20;
           this.ReceiveBufferNumericUpDown.Value = new decimal(new int[] {
            29696,
            0,
            0,
            0});
           // 
           // SendBufferLabel
           // 
           this.SendBufferLabel.AutoSize = true;
           this.SendBufferLabel.Location = new System.Drawing.Point(162, 15);
           this.SendBufferLabel.Name = "SendBufferLabel";
           this.SendBufferLabel.Size = new System.Drawing.Size(66, 13);
           this.SendBufferLabel.TabIndex = 18;
           this.SendBufferLabel.Text = "Send Buffer:";
           // 
           // ReceiveBufferLabel
           // 
           this.ReceiveBufferLabel.AutoSize = true;
           this.ReceiveBufferLabel.Location = new System.Drawing.Point(80, 15);
           this.ReceiveBufferLabel.Name = "ReceiveBufferLabel";
           this.ReceiveBufferLabel.Size = new System.Drawing.Size(81, 13);
           this.ReceiveBufferLabel.TabIndex = 16;
           this.ReceiveBufferLabel.Text = "Receive Buffer:";
           // 
           // NoDelayCheckBox
           // 
           this.NoDelayCheckBox.AutoSize = true;
           this.NoDelayCheckBox.Location = new System.Drawing.Point(7, 34);
           this.NoDelayCheckBox.Name = "NoDelayCheckBox";
           this.NoDelayCheckBox.Size = new System.Drawing.Size(70, 17);
           this.NoDelayCheckBox.TabIndex = 0;
           this.NoDelayCheckBox.Text = "No Delay";
           this.NoDelayCheckBox.UseVisualStyleBackColor = true;
           // 
           // MaxPduLengthMaskedTextBox
           // 
           this.MaxPduLengthMaskedTextBox.HidePromptOnLeave = true;
           this.MaxPduLengthMaskedTextBox.Location = new System.Drawing.Point(277, 74);
           this.MaxPduLengthMaskedTextBox.Mask = "#########";
           this.MaxPduLengthMaskedTextBox.Name = "MaxPduLengthMaskedTextBox";
           this.MaxPduLengthMaskedTextBox.PromptChar = '#';
           this.MaxPduLengthMaskedTextBox.Size = new System.Drawing.Size(251, 20);
           this.MaxPduLengthMaskedTextBox.TabIndex = 28;
           this.MaxPduLengthMaskedTextBox.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
           // 
           // MaxPduLabel
           // 
           this.MaxPduLabel.AutoSize = true;
           this.MaxPduLabel.Location = new System.Drawing.Point(274, 58);
           this.MaxPduLabel.Name = "MaxPduLabel";
           this.MaxPduLabel.Size = new System.Drawing.Size(92, 13);
           this.MaxPduLabel.TabIndex = 27;
           this.MaxPduLabel.Text = "PDU Max Length:";
           // 
           // TimeoutGroupBox
           // 
           this.TimeoutGroupBox.Controls.Add(this.ReconnectTimeoutNumericUpDown);
           this.TimeoutGroupBox.Controls.Add(this.AddInTimeoutNumericUpDown);
           this.TimeoutGroupBox.Controls.Add(this.ClientTimeoutLabel);
           this.TimeoutGroupBox.Controls.Add(this.AddInTimeoutLabel);
           this.TimeoutGroupBox.Controls.Add(this.ClientTimeoutNumericUpDown);
           this.TimeoutGroupBox.Controls.Add(this.ReconnectTimeoutLabel);
           this.TimeoutGroupBox.Location = new System.Drawing.Point(9, 101);
           this.TimeoutGroupBox.Name = "TimeoutGroupBox";
           this.TimeoutGroupBox.Size = new System.Drawing.Size(251, 66);
           this.TimeoutGroupBox.TabIndex = 26;
           this.TimeoutGroupBox.TabStop = false;
           this.TimeoutGroupBox.Text = "Timeouts (Seconds)";
           // 
           // ReconnectTimeoutNumericUpDown
           // 
           this.ReconnectTimeoutNumericUpDown.Location = new System.Drawing.Point(89, 32);
           this.ReconnectTimeoutNumericUpDown.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
           this.ReconnectTimeoutNumericUpDown.Name = "ReconnectTimeoutNumericUpDown";
           this.ReconnectTimeoutNumericUpDown.Size = new System.Drawing.Size(74, 20);
           this.ReconnectTimeoutNumericUpDown.TabIndex = 23;
           // 
           // AddInTimeoutNumericUpDown
           // 
           this.AddInTimeoutNumericUpDown.Location = new System.Drawing.Point(169, 32);
           this.AddInTimeoutNumericUpDown.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
           this.AddInTimeoutNumericUpDown.Name = "AddInTimeoutNumericUpDown";
           this.AddInTimeoutNumericUpDown.Size = new System.Drawing.Size(74, 20);
           this.AddInTimeoutNumericUpDown.TabIndex = 25;
           // 
           // AddInTimeoutLabel
           // 
           this.AddInTimeoutLabel.AutoSize = true;
           this.AddInTimeoutLabel.Location = new System.Drawing.Point(166, 16);
           this.AddInTimeoutLabel.Name = "AddInTimeoutLabel";
           this.AddInTimeoutLabel.Size = new System.Drawing.Size(38, 13);
           this.AddInTimeoutLabel.TabIndex = 24;
           this.AddInTimeoutLabel.Text = "AddIn:";
           // 
           // ReconnectTimeoutLabel
           // 
           this.ReconnectTimeoutLabel.AutoSize = true;
           this.ReconnectTimeoutLabel.Location = new System.Drawing.Point(86, 16);
           this.ReconnectTimeoutLabel.Name = "ReconnectTimeoutLabel";
           this.ReconnectTimeoutLabel.Size = new System.Drawing.Size(63, 13);
           this.ReconnectTimeoutLabel.TabIndex = 22;
           this.ReconnectTimeoutLabel.Text = "Reconnect:";
           // 
           // ErrorLabel
           // 
           this.ErrorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
           this.ErrorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           this.ErrorLabel.ForeColor = System.Drawing.Color.Red;
           this.ErrorLabel.Location = new System.Drawing.Point(4, 337);
           this.ErrorLabel.Name = "ErrorLabel";
           this.ErrorLabel.Size = new System.Drawing.Size(366, 14);
           this.ErrorLabel.TabIndex = 2;
           this.ErrorLabel.Text = "Error";
           // 
           // RestartLabel
           // 
           this.RestartLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
           this.RestartLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           this.RestartLabel.ForeColor = System.Drawing.Color.Red;
           this.RestartLabel.Location = new System.Drawing.Point(4, 308);
           this.RestartLabel.Name = "RestartLabel";
           this.RestartLabel.Size = new System.Drawing.Size(366, 14);
           this.RestartLabel.TabIndex = 1;
           this.RestartLabel.Text = "Error";
           // 
           // ControlsErrorProvider
           // 
           this.ControlsErrorProvider.ContainerControl = this;
           // 
           // EditServiceDialog
           // 
           this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
           this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
           this.CancelButton = this.CancelDialogButton;
           this.ClientSize = new System.Drawing.Size(542, 356);
           this.Controls.Add(this.RestartLabel);
           this.Controls.Add(this.ErrorLabel);
           this.Controls.Add(this.ServerTabControl);
           this.Controls.Add(this.CancelDialogButton);
           this.Controls.Add(this.OkButton);
           this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
           this.MaximizeBox = false;
           this.MinimizeBox = false;
           this.Name = "EditServiceDialog";
           this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
           this.Text = "Add New Server";
           this.Load += new System.EventHandler(this.EditServerDialog_Load);
           this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditServerDialog_FormClosing);
           ((System.ComponentModel.ISupportInitialize)(this.ClientTimeoutNumericUpDown)).EndInit();
           this.ServerTabControl.ResumeLayout(false);
           this.SettingsTabPage.ResumeLayout(false);
           this.SettingsTabPage.PerformLayout();
           ((System.ComponentModel.ISupportInitialize)(this.ServerMaxClientsMaskedNumeric)).EndInit();
           ((System.ComponentModel.ISupportInitialize)(this.ServerPortMaskedNumeric)).EndInit();
           this.AdvancedTabPage.ResumeLayout(false);
           this.AdvancedTabPage.PerformLayout();
           this.SocketOptionsGroupBox.ResumeLayout(false);
           this.SocketOptionsGroupBox.PerformLayout();
           ((System.ComponentModel.ISupportInitialize)(this.SendBufferNumericUpDown)).EndInit();
           ((System.ComponentModel.ISupportInitialize)(this.ReceiveBufferNumericUpDown)).EndInit();
           this.TimeoutGroupBox.ResumeLayout(false);
           this.TimeoutGroupBox.PerformLayout();
           ((System.ComponentModel.ISupportInitialize)(this.ReconnectTimeoutNumericUpDown)).EndInit();
           ((System.ComponentModel.ISupportInitialize)(this.AddInTimeoutNumericUpDown)).EndInit();
           ((System.ComponentModel.ISupportInitialize)(this.ControlsErrorProvider)).EndInit();
           this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Label AeTitleLabel;
        protected System.Windows.Forms.TextBox ServerAETextBox;
        protected System.Windows.Forms.Label DescriptionLabel;
        protected System.Windows.Forms.TextBox ServerDescriptionTextBox;
        protected System.Windows.Forms.Label IpAddressLabel;
        protected System.Windows.Forms.Label PortLabel;
        protected System.Windows.Forms.NumericUpDown ClientTimeoutNumericUpDown;
        protected System.Windows.Forms.Label ClientTimeoutLabel;
        protected System.Windows.Forms.Label MaxClientsLabel;
        protected System.Windows.Forms.CheckBox ServerAllowAnonymousCheckBox;
        protected System.Windows.Forms.Button OkButton;
        protected System.Windows.Forms.CheckBox ServerSecureCheckBox;
        protected System.Windows.Forms.TextBox ImplementationClassTextBox;
        protected System.Windows.Forms.Label ImplementationClassLabel;
        protected System.Windows.Forms.TextBox ImplementationVersionNameTextBox;
        protected System.Windows.Forms.Label ImplementationVersionNameLabel;
        protected System.Windows.Forms.Label TemporaryDirectoryLabel;
        protected System.Windows.Forms.TextBox TemporaryDirectoryTextBox;
        protected System.Windows.Forms.Button TempFolderBrowseButton;
        protected System.Windows.Forms.FolderBrowserDialog TempFolderBrowserDialog;
        protected System.Windows.Forms.ComboBox ServerIpComboBox;
        protected System.Windows.Forms.TabControl ServerTabControl;
        protected System.Windows.Forms.TabPage SettingsTabPage;
        protected System.Windows.Forms.TabPage AdvancedTabPage;
        protected System.Windows.Forms.NumericUpDown ReconnectTimeoutNumericUpDown;
        protected System.Windows.Forms.Label ReconnectTimeoutLabel;
        protected System.Windows.Forms.NumericUpDown AddInTimeoutNumericUpDown;
        protected System.Windows.Forms.Label AddInTimeoutLabel;
        protected System.Windows.Forms.GroupBox TimeoutGroupBox;
        protected System.Windows.Forms.Label MaxPduLabel;
        protected System.Windows.Forms.GroupBox SocketOptionsGroupBox;
        protected System.Windows.Forms.MaskedTextBox MaxPduLengthMaskedTextBox;
        protected System.Windows.Forms.Label SendBufferLabel;
        protected System.Windows.Forms.Label ReceiveBufferLabel;
        protected System.Windows.Forms.CheckBox NoDelayCheckBox;
        protected System.Windows.Forms.ComboBox StartModeComboBox;
        protected System.Windows.Forms.Label StartModeLabel;
        protected System.Windows.Forms.Label ErrorLabel;
        protected System.Windows.Forms.Button CancelDialogButton;
        protected System.Windows.Forms.CheckBox AllowMultipleConnectionsChcekBox;
        protected System.Windows.Forms.Label RestartLabel;
        protected System.Windows.Forms.NumericUpDown SendBufferNumericUpDown;
        protected System.Windows.Forms.NumericUpDown ReceiveBufferNumericUpDown;
        protected System.Windows.Forms.ErrorProvider ControlsErrorProvider;
        private System.Windows.Forms.Label ServiceNameLabel;
        private System.Windows.Forms.TextBox ServiceNameTextBox;
        protected System.Windows.Forms.Label DisplayNameLabel;
        protected System.Windows.Forms.TextBox DisplayNameTextBox;
        private System.Windows.Forms.NumericUpDown ServerPortMaskedNumeric;
        private System.Windows.Forms.NumericUpDown ServerMaxClientsMaskedNumeric;

    }
}