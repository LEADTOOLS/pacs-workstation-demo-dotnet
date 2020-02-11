// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace MedicalWorkstationConfigurationDemo.UI
{
   public partial class DatabaseOptions : UserControl
   {
      #region Public

      #region Methods

      public DatabaseOptions()
      {
         InitializeComponent();

         Init();

         RegisterEvents();
      }

      public DatabaseComponents SupportedDatabases
      {
         get
         {
            return _supportedDatabases;
         }

         set
         {
            _supportedDatabases = value;

            SupportedDatabasesChanged();

         }
      }

      private void RealignCheckboxes()
      {
         int diff = 20;
         int top = StorageServerDbCheckBox.Top;
         int currentTop = top;

         foreach (Control database in __DatbasesOptions)
         {
            diff = database.Height + 3;
            DatabaseComponents currentDb;
            bool isSupported;

            currentDb = (DatabaseComponents)database.Tag;
            isSupported = (SupportedDatabases & currentDb) == currentDb;
            database.Visible = isSupported;

            if (isSupported == true)
            {
               database.Top = currentTop;
               currentTop += diff;
            }
         }
      }

      private void SupportedDatabasesChanged()
      {
         foreach (Control database in __DatbasesOptions)
         {
            DatabaseComponents currentDb;
            bool isSupported;

            currentDb = (DatabaseComponents)database.Tag;
            isSupported = (SupportedDatabases & currentDb) == currentDb;

            if ( database is CheckBox )
            {
               ((CheckBox)database).Checked = isSupported;
            }
            else
            {
               foreach ( CheckBox dbCheck in database.Controls.OfType<CheckBox> ( ) )
               {
                  dbCheck.Checked = isSupported ;
               }
            }
            
            database.Enabled = isSupported;
         }
         RealignCheckboxes();
      }

      #endregion

      #region Properties

      public DbConfigurationMode Mode
      {
         get
         {
            return (CreateNewDbRadioButton.Checked) ? DbConfigurationMode.Create : DbConfigurationMode.Configure;

         }

         set
         {
            CreateNewDbRadioButton.Checked = (value == DbConfigurationMode.Create);
            ConnectToDbRadioButton.Checked = !CreateNewDbRadioButton.Checked;
         }
      }
      
      public DbConfigurationMode OptionsDatabaseMode
      {
         get
         {
            return (CreateOptionsDatabaseRadioButton.Checked) ? DbConfigurationMode.Create : DbConfigurationMode.Configure;
         }
      }

      public bool StorageServerDbSelected
      {
         get
         {
            return StorageServerDbCheckBox.Checked;
         }

         set
         {
            StorageServerDbCheckBox.Checked = value;
         }
      }

      public bool StorageServerOptionsDbSelected
      {
         get
         {
            return StorageServerOptionsDbCheckBox.Checked;
         }

         set
         {
            StorageServerOptionsDbCheckBox.Checked = value;
         }
      }

      public bool PatientUpdaterDbSelected
      {
         get
         {
            return PatientUpdaterDbCheckBox.Checked;
         }

         set
         {
            PatientUpdaterDbCheckBox.Checked = value;
         }
      }

      public bool LoggingDbSelected
      {
         get
         {
            return LoggingDbCheckBox.Checked;
         }

         set
         {
            LoggingDbCheckBox.Checked = value;
         }
      }

      public bool StorageDbSelected
      {
         get
         {
            return StorageDbCheckBox.Checked;
         }

         set
         {
            StorageDbCheckBox.Checked = value;
         }
      }

      public bool WorklistDbSelected
      {
         get
         {
            return WorklistDbCheckBox.Checked;
         }

         set
         {
            WorklistDbCheckBox.Checked = value;
         }
      }

      public bool MediaCreationDbSelected
      {
         get
         {
            return MediaCreationDbCheckBox.Checked;
         }

         set
         {
            MediaCreationDbCheckBox.Checked = value;
         }
      }

      public bool WorkstationDbSelected
      {
         get
         {
            return WorkstationDbCheckBox.Checked;
         }

         set
         {
            WorkstationDbCheckBox.Checked = value;
         }
      }

      public bool UserManagementDbSelected
      {
         get
         {
            return UserManagementDbCheckBox.Checked;
         }

         set
         {
            UserManagementDbCheckBox.Checked = value;
         }
      }

      public string WorkstationUserName
      {
         get
         {
            return WorkstationUserNameTextBox.Text;
         }

         set
         {
            WorkstationUserNameTextBox.Text = value;
         }
      }

      public string WorkstationUserPassword
      {
         get
         {
            return WorkstationUserPasswordTextBox.Text;
         }

         set
         {
            WorkstationUserPasswordTextBox.Text = value;
         }
      }
      
      public string WorkstationUserPasswordConfirm
      {
         get
         {
            return WorkstationUserPasswordConfirmTextBox.Text;
         }

         set
         {
            WorkstationUserPasswordConfirmTextBox.Text = value;
         }
      }

      #endregion

      #region Events

      #endregion

      #region Data Types Definition

      #endregion

      #region Callbacks

      public event EventHandler ValueChanged;

      #endregion

      #endregion

      #region Protected

      #region Methods

      #endregion

      #region Properties

      #endregion

      #region Events

      #endregion

      #region Data Members

      #endregion

      #region Data Types Definition

      #endregion

      #endregion

      #region Private

      #region Methods

      private void Init()
      {
         string userName;


         userName = Environment.UserName;

         __DatbasesOptions = new List<Control>();

         StorageServerDbCheckBox.Tag = DatabaseComponents.StorageServer;

         LoadBalancePanel.Tag = DatabaseComponents.StorageServerOptions;
         PatientUpdaterDbCheckBox.Tag = DatabaseComponents.PatientUpdater;

         LoggingDbCheckBox.Tag = DatabaseComponents.Logging;
         StorageDbCheckBox.Tag = DatabaseComponents.Storage;
         WorklistDbCheckBox.Tag = DatabaseComponents.Worklist;
         MediaCreationDbCheckBox.Tag = DatabaseComponents.MediaCreation;
         WorkstationDbCheckBox.Tag = DatabaseComponents.Workstation;
         UserManagementDbCheckBox.Tag = DatabaseComponents.UserManagement;

         __DatbasesOptions.Add(StorageServerDbCheckBox);
         __DatbasesOptions.Add(LoadBalancePanel);
         __DatbasesOptions.Add(LoggingDbCheckBox);
         __DatbasesOptions.Add(StorageDbCheckBox);
         __DatbasesOptions.Add(WorklistDbCheckBox);
         __DatbasesOptions.Add(MediaCreationDbCheckBox);
         __DatbasesOptions.Add(WorkstationDbCheckBox);
         __DatbasesOptions.Add(UserManagementDbCheckBox);

         __DatbasesOptions.Add(PatientUpdaterDbCheckBox);


         SupportedDatabases = DatabaseComponents.None;

         WorkstationUserNameTextBox.Text = userName;
         WorkstationUserPasswordTextBox.Text = userName;
         WorkstationUserPasswordConfirmTextBox.Text = userName;

         if (!(ConnectionProviders.FromProvider(ConfigurationData.GetSupportedProvider()).Length > 1))
         {
            ShowPasswordCheckBox.Checked = false;
            ShowPasswordCheckBox.Visible = false;
         }

         UserManagementDbCheckBox_CheckedChanged(this, EventArgs.Empty);
         ShowPasswordCheckBox_CheckedChanged(this, EventArgs.Empty);
      }

      private void RegisterEvents()
      {
         StorageServerDbCheckBox.CheckedChanged += new EventHandler(UIValueChanged_CheckedChanged);
         StorageServerOptionsDbCheckBox.CheckedChanged += new EventHandler(UIValueChanged_CheckedChanged);
         PatientUpdaterDbCheckBox.CheckedChanged += new EventHandler(UIValueChanged_CheckedChanged);
         CreateNewDbRadioButton.CheckedChanged += new EventHandler(UIValueChanged_CheckedChanged);
         LoggingDbCheckBox.CheckedChanged += new EventHandler(UIValueChanged_CheckedChanged);
         StorageDbCheckBox.CheckedChanged += new EventHandler(UIValueChanged_CheckedChanged);
         WorklistDbCheckBox.CheckedChanged += new EventHandler(UIValueChanged_CheckedChanged);
         MediaCreationDbCheckBox.CheckedChanged += new EventHandler(UIValueChanged_CheckedChanged);
         WorkstationDbCheckBox.CheckedChanged += new EventHandler(UIValueChanged_CheckedChanged);
         UserManagementDbCheckBox.CheckedChanged += new EventHandler(UIValueChanged_CheckedChanged);
         WorkstationUserNameTextBox.TextChanged += new EventHandler(UIValueChanged_CheckedChanged);
         WorkstationUserPasswordTextBox.TextChanged += new EventHandler(UIValueChanged_CheckedChanged);
         WorkstationUserPasswordConfirmTextBox.TextChanged += new EventHandler(UIValueChanged_CheckedChanged);

         CreateNewDbRadioButton.CheckedChanged += new EventHandler(CreateNewDbRadioButton_CheckedChanged);
         UserManagementDbCheckBox.CheckedChanged += new EventHandler(UserManagementDbCheckBox_CheckedChanged);
         StorageServerDbCheckBox.CheckedChanged += new EventHandler(StorageServerDbCheckBox_CheckedChanged);
         ShowPasswordCheckBox.CheckedChanged += new EventHandler(ShowPasswordCheckBox_CheckedChanged);

         StorageServerOptionsDbCheckBox.CheckedChanged += new EventHandler(StorageServerOptionsDbCheckBox_CheckedChanged);
      }

      void StorageServerOptionsDbCheckBox_CheckedChanged(object sender, EventArgs e)
      {
         ConnectToOptionsDatabaseRadioButton.Enabled = StorageServerOptionsDbCheckBox.Checked ;
         CreateOptionsDatabaseRadioButton.Enabled = StorageServerOptionsDbCheckBox.Checked ;
      }

      private void HandleUserCredentialsEnableState()
      {
         if (InvokeRequired)
         {
            Invoke(new MethodInvoker(HandleUserCredentialsEnableState));
         }
         else
         {
            // If patient Updater is the only supported componenet, disable "Create new Databases"
            if (SupportedDatabases == DatabaseComponents.PatientUpdater || DisableCreateFromConfiguration ( ) )
            {
               ConnectToDbRadioButton.Checked = true;
               CreateNewDbRadioButton.Enabled = false;
            }

            bool supportedUserManagementDb = UserManagementDbCheckBox.Checked && CreateNewDbRadioButton.Checked && ((SupportedDatabases & DatabaseComponents.UserManagement) == DatabaseComponents.UserManagement);
            bool supportedStorageServerDb = StorageServerDbCheckBox.Checked && CreateNewDbRadioButton.Checked && ((SupportedDatabases & DatabaseComponents.StorageServer) == DatabaseComponents.StorageServer);
            bool enabled = supportedUserManagementDb || supportedStorageServerDb;
            WorkstationUserNameTextBox.Enabled = enabled;
            WorkstationUserPasswordTextBox.Enabled = enabled;
            WorkstationUserPasswordConfirmTextBox.Enabled = enabled;
            ShowPasswordCheckBox.Enabled = enabled;

            DatabaseComponents patientUpdaterDb = (DatabaseComponents)PatientUpdaterDbCheckBox.Tag;
            bool bPatientUpdaterSupported = (SupportedDatabases & patientUpdaterDb) == patientUpdaterDb;
            PatientUpdaterDbCheckBox.Enabled = ConnectToDbRadioButton.Checked && bPatientUpdaterSupported;

            DatabaseComponents storageServerOptionsDb = (DatabaseComponents)LoadBalancePanel.Tag;
            bool bStorageServerOptionsSupported = (SupportedDatabases & storageServerOptionsDb) == storageServerOptionsDb;
            LoadBalancePanel.Enabled = ConnectToDbRadioButton.Checked && bStorageServerOptionsSupported && StorageServerDbCheckBox.Checked;

            if (CreateNewDbRadioButton.Checked)
            {
               // Create new databases
               StorageServerOptionsDbCheckBox.Checked = false;
               PatientUpdaterDbCheckBox.Checked = false;
            }
         }
      }

      public static bool DisableCreateFromConfiguration()
      {
         string disableOption = ConfigurationManager.AppSettings [ "DisableCreate" ] ;
         
         if ( string.IsNullOrEmpty ( disableOption ) )
         {
            return false ;
         }
         
         bool disableCreate = false ;
         
         if ( bool.TryParse ( disableOption, out disableCreate ) ) 
         {
            return disableCreate ;
         }
         else
         {
            return false ;
         }
      }

      public static bool DefaultConnectToExistingDatabaseFromConfiguration()
      {
         string defaultOption = ConfigurationManager.AppSettings["DefaultConnectToExistingDatabase"];

         if (string.IsNullOrEmpty(defaultOption))
         {
            return false;
         }

         bool defaultConnectToExistingDabase = false;

         if (bool.TryParse(defaultOption, out defaultConnectToExistingDabase))
         {
            return defaultConnectToExistingDabase;
         }
         else
         {
            return false;
         }
      }

      private void OnValueChanged()
      {
         if (null != ValueChanged)
         {
            ValueChanged(this, EventArgs.Empty);
         }
      }

      #endregion

      #region Properties

      private List<Control> __DatbasesOptions
      {
         get;
         set;
      }

      #endregion

      #region Events

      void UIValueChanged_CheckedChanged(object sender, EventArgs e)
      {
         try
         {
            OnValueChanged();
         }
         catch (Exception)
         { }
      }

      void CreateNewDbRadioButton_CheckedChanged(object sender, EventArgs e)
      {
         try
         {
            HandleUserCredentialsEnableState();
         }
         catch (Exception)
         { }
      }

      void UserManagementDbCheckBox_CheckedChanged(object sender, EventArgs e)
      {
         try
         {
            HandleUserCredentialsEnableState();
         }
         catch (Exception)
         { }
      }

      void StorageServerDbCheckBox_CheckedChanged(object sender, EventArgs e)
      {
         try
         {
            HandleUserCredentialsEnableState();
         }
         catch (Exception)
         { }
      }

      void ShowPasswordCheckBox_CheckedChanged(object sender, EventArgs e)
      {
         try
         {
            if (ShowPasswordCheckBox.Checked)
            {
               WorkstationUserPasswordTextBox.PasswordChar = char.MinValue;
               WorkstationUserPasswordConfirmTextBox.PasswordChar = char.MinValue;
            }
            else
            {
               WorkstationUserPasswordTextBox.PasswordChar = '*';
               WorkstationUserPasswordConfirmTextBox.PasswordChar = '*';
            }
         }
         catch (Exception)
         { }
      }

      #endregion

      #region Data Members

      private DatabaseComponents _supportedDatabases;
      #endregion

      private void DatabaseOptions_VisibleChanged(object sender, EventArgs e)
      {
         HandleUserCredentialsEnableState();
      }

      #region Data Types Definition

      #endregion

      #endregion

      #region internal

      #region Methods

      #endregion

      #region Properties

      #endregion

      #region Events

      #endregion

      #region Data Types Definition

      #endregion

      #region Callbacks

      #endregion

      #endregion
   }
}
