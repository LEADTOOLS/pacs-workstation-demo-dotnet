// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System ;
using System.ComponentModel; 
using System.Net ;
using System.Windows.Forms ;
using Leadtools.Demos.Workstation.Configuration ;
using Leadtools.Medical.Winforms;
using Leadtools.Annotations;
using Leadtools.Annotations.Engine;


namespace Leadtools.Demos.Workstation
{
   public partial class ClientConfiguration : UserControl
   {
      #region Public
   
         #region Methods

            public ClientConfiguration ( )
            {
               try
               {
                  InitializeComponent ( ) ;
                  
                  Init ( ) ;
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  throw ;
               }
            }
            
         #endregion
   
         #region Properties
         
            public bool CanChangeClientInfo
            {
               get
               {
                  return _canChangeClientInfo ;
               }
               
               set
               {
                  _canChangeClientInfo = value ;
                  
                  WorkstationAETextBox.Enabled = value ;
                  WorkstationPortMaskedTextBox.Enabled = value ;
                  
                  if ( !value )
                  {
                     CanChangeForceClientInfo = false ;  
                  }
               }
            }
            
            public bool CanChangeForceClientInfo
            {
               get
               {
                  return ForceToAllClientsCheckBox.Enabled ;
               }
               
               set
               {
                  ForceToAllClientsCheckBox.Enabled = value ;
               }
            }
   
         #endregion
   
         #region Events
   
         #endregion
   
         #region Data Types Definition
   
         #endregion
   
         #region Callbacks
   
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
         
            private void Init ( ) 
            {
               try
               {
                  string hostName ;
                  IPAddress [ ] localIpAddress ;
                  
                  
                  CanChangeClientInfo = true ;
                  __UpdatingConfig    = false ;
                  
                  hostName = Dns.GetHostName ( ) ;
                  
                  WorkstationHostaddressComboBox.Items.Add ( hostName ) ;
                  
                  if ( ConfigurationData.WorkstationClient.Address == hostName ) 
                  {
                     WorkstationHostaddressComboBox.SelectedItem = hostName ;
                  }
                  
                  localIpAddress = Dns.GetHostAddresses ( hostName ) ;
                  
                  foreach ( IPAddress address in localIpAddress ) 
                  {
                     if ( address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork ) 
                     {
                        WorkstationHostaddressComboBox.Items.Add ( address.ToString ( ) ) ;
                        
                        if ( ConfigurationData.WorkstationClient.Address == address.ToString ( ) )
                        {
                           WorkstationHostaddressComboBox.SelectedItem = address.ToString ( ) ;
                        }
                     }
                  }

                  AlwaysSaveSessionRadioButton.Tag     = SaveOptions.AlwaysSave ;
                  NeverSaveSessionRadioButton.Tag      = SaveOptions.NeverSave ;
                  PromptUserSaveSessionRadioButton.Tag = SaveOptions.PromptUser ;
                  
                  MeasurmentsUnitComboBox.Items.AddRange ( Enum.GetNames ( typeof ( AnnUnit ) ) ) ;
                  
                  ReadConfiguration ( ) ;
                  
                  this.Load += new EventHandler ( ClientConfiguration_Load ) ;
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  throw exception ;
               }
            }

            private void ReadConfiguration ( )
            {
               if ( __UpdatingConfig ) 
               {
                  return ;
               }
               
               this.SuspendLayout ( )  ;
               
               try
               {
                  WorkstationAETextBox.Text                 = ConfigurationData.WorkstationClient.AETitle ;
                  WorkstationPortMaskedTextBox.Text         = ConfigurationData.WorkstationClient.Port.ToString ( ) ;
                  WorkstationSecureCheckBox.Checked         = ConfigurationData.WorkstationClient.Secure;
                  MoveToWsClientRadioButton.Checked         = ConfigurationData.MoveToWSClient ;
                  MoveToWsServiceRadioButton.Checked        = !ConfigurationData.MoveToWSClient ;
                  WorkstationServiceAETitleTextBox.Text     = ConfigurationData.WorkstationServiceAE ;
                  ForceToAllClientsCheckBox.Checked         = ConfigurationData.SetClientToAllWorkstations ;
                  EnableDebugCheckBox.Checked               = ConfigurationData.Debugging.Enable ;
                  DisplayDetailDebugMsgCheckBox.Checked     = ConfigurationData.Debugging.DisplayDetailedErrors ;
                  DebugLogFileCheckBox.Checked              = ConfigurationData.Debugging.GenerateLogFile ;
                  DebugLogFileNameTextBox.Text              = ConfigurationData.Debugging.LogFileName ;
                  UseCompressionCheckBox.Checked            = ConfigurationData.Compression.Enable ;
                  LossyCompressionRadioButton.Checked       = ConfigurationData.Compression.Lossy ;
                  EnableLazyLoadingCheckBox.Checked         = ConfigurationData.ViewerLazyLoading.Enable ;
                  LazyLoadingHiddenImagesMaskedTextBox.Text = ConfigurationData.ViewerLazyLoading.HiddenImages.ToString ( ) ;
                  AutoSizeOverlayTextCheckBox.Checked       = ConfigurationData.ViewerAutoSizeOverlayText ;
                  FixedOverlayTextSizeNumericUpDown.Value   = ConfigurationData.ViewerOverlayTextSize ;
                  FixedOverlayTextSizeNumericUpDown.Enabled = !ConfigurationData.ViewerAutoSizeOverlayText ;
                  FullScreenModeCheckBox.Checked            = ConfigurationData.RunFullScreen ;
                  ContinueRetireveOnDuplicateCheckBox.Checked = ConfigurationData.ContinueRetrieveOnDuplicateInstance ;
                  
                  LosslessCompressionRadioButton.Checked       = !LossyCompressionRadioButton.Checked ;
                  CompressionGroupBox.Enabled                  = UseCompressionCheckBox.Checked ;
                  DebugGroupBox.Enabled                        = EnableDebugCheckBox.Checked ;
                  LazyLoadingHiddenImagesMaskedTextBox.Enabled = EnableLazyLoadingCheckBox.Checked ;
                  
                  AlwaysSaveSessionRadioButton.Checked     = ConfigurationData.SaveSessionBehavior == SaveOptions.AlwaysSave ;
                  NeverSaveSessionRadioButton.Checked      = ConfigurationData.SaveSessionBehavior == SaveOptions.NeverSave ;
                  PromptUserSaveSessionRadioButton.Checked = ConfigurationData.SaveSessionBehavior == SaveOptions.PromptUser ;
                  
                  AnnotationColorButton.BackColor      = ConfigurationData.AnnotationDefaultColor ;
                  MeasurmentsUnitComboBox.SelectedItem = ConfigurationData.MeasurementUnit.ToString ( ) ;
                  
                  HandleDicomRetrieveUI ( ) ;
               }
               finally
               {
                  this.ResumeLayout ( ) ;
               }
            }

            private void HandleDicomRetrieveUI()
            {
               ContinueRetireveOnDuplicateCheckBox.Enabled = ConfigurationData.MoveToWSClient ;
               WorkstationServiceAETitleTextBox.Enabled    = !ConfigurationData.MoveToWSClient ;
               
            }
                  
            private void RegisterEvents ( ) 
            {
               try
               {
                  WorkstationAETextBox.Validating  += new CancelEventHandler ( txtWorkstationAE_Validating ) ;
                  WorkstationAETextBox.TextChanged += new EventHandler       ( WorkstationAETextBox_TextChanged ) ;

                  WorkstationPortMaskedTextBox.TextChanged += new EventHandler(WorkstationPortMaskedTextBox_TextChanged);
                  //WorkstationPortMaskedTextBox.Validated                 += new EventHandler ( txtWorkstationPort_Validated ) ;
                  WorkstationHostaddressComboBox.SelectedIndexChanged += new EventHandler ( cmbWorkstationIPaddress_SelectedIndexChanged ) ;

                  WorkstationSecureCheckBox.CheckedChanged += new EventHandler(WorkstationSecureCheckBox_CheckedChanged);
                  ForceToAllClientsCheckBox.CheckedChanged += new EventHandler ( ForceToAllClientsCheckBox_CheckedChanged ) ;
                  MoveToWsClientRadioButton.CheckedChanged += new EventHandler ( MoveToWsClientRadioButton_CheckedChanged ) ;
                  
                  ContinueRetireveOnDuplicateCheckBox.CheckedChanged += new EventHandler ( chkContinueRetireve_CheckedChanged ) ;

                  EnableDebugCheckBox.CheckedChanged += new EventHandler  ( chkEnableDebug_CheckedChanged ) ;
                  DisplayDetailDebugMsgCheckBox.CheckedChanged    += new EventHandler  ( chkDebugMsg_CheckedChanged ) ;
                  DebugLogFileCheckBox.CheckedChanged     += new EventHandler  ( chkLogFile_CheckedChanged ) ;
                  DebugLogFileNameTextBox.Validating     += new CancelEventHandler( txtLogFileName_Validating );
                  DebugLogFileNameTextBox.Validated      += new EventHandler  ( txtLogFileName_Validated )  ;
                  
                  UseCompressionCheckBox.CheckedChanged    += new EventHandler ( chkUseCompression_CheckedChanged ) ;
                  LossyCompressionRadioButton.CheckedChanged += new EventHandler ( rbtnLossyCompression_CheckedChanged ) ;
                  
                  EnableLazyLoadingCheckBox.CheckedChanged += new EventHandler ( chkLazyLoading_CheckedChanged ) ;
                  LazyLoadingHiddenImagesMaskedTextBox.TextChanged += new EventHandler    ( mtxtHiddenImages_TextChanged ) ;

                  AutoSizeOverlayTextCheckBox.CheckedChanged     += new EventHandler ( AutoSizeOverlayTextCheckBox_CheckedChanged ) ;
                  FixedOverlayTextSizeNumericUpDown.ValueChanged += new EventHandler ( FixedOverlayTextSizeNumericUpDown_ValueChanged ) ;

                  FullScreenModeCheckBox.CheckedChanged += new EventHandler ( FullScreenModeCheckBox_CheckedChanged ) ;
                  
                  DebugLogFileButton.Click += new EventHandler ( btnLogFile_Click ) ;

                  AlwaysSaveSessionRadioButton.CheckedChanged     += new EventHandler ( AlwaysSaveSessionRadioButton_CheckedChanged ) ;
                  NeverSaveSessionRadioButton.CheckedChanged      += new EventHandler ( AlwaysSaveSessionRadioButton_CheckedChanged ) ;
                  PromptUserSaveSessionRadioButton.CheckedChanged += new EventHandler ( AlwaysSaveSessionRadioButton_CheckedChanged ) ;

                  DisplayOrientationButton.Click += new EventHandler ( DisplayOrientationButton_Click ) ;

                  AnnotationColorButton.Click += new EventHandler(AnnotationColorButton_Click);
                  AnnotationColorButton.BackColorChanged           += new EventHandler ( AnnotationColorButton_BackColorChanged ) ;
                  MeasurmentsUnitComboBox.SelectionChangeCommitted += new EventHandler ( MeasurmentsUnitComboBox_SelectionChangeCommitted ) ;
                  
                  ConfigurationData.ValueChanged += new EventHandler ( ConfigurationData_ValueChanged ) ;
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  throw exception ;
               }
            }

            void AnnotationColorButton_Click(object sender, EventArgs e)
            {
               try
               {
                  using ( ColorDialog colorDlg = new ColorDialog ( ) )
                  {
                     colorDlg.Color = AnnotationColorButton.BackColor ;
                     
                     if ( colorDlg.ShowDialog ( this ) == DialogResult.OK ) 
                     {
                        AnnotationColorButton.BackColor = colorDlg.Color ;
                     }
                  }
               }
               catch{}
            }

            void MeasurmentsUnitComboBox_SelectionChangeCommitted(object sender, EventArgs e)
            {
               try
               {
                  __UpdatingConfig = false ;
                  
                  ConfigurationData.MeasurementUnit = (AnnUnit) Enum.Parse ( typeof ( AnnUnit ), MeasurmentsUnitComboBox.SelectedItem.ToString ( ) ) ;
               }
               catch{}
               finally
               {
                  __UpdatingConfig = false ;
               }
            }

            void AnnotationColorButton_BackColorChanged(object sender, EventArgs e)
            {
               try
               {
                  __UpdatingConfig = true ;
                  
                  ConfigurationData.AnnotationDefaultColor = AnnotationColorButton.BackColor ;
               }
               catch{}
               finally
               {
                  __UpdatingConfig = false ;
               }
            }

            void txtWorkstationAE_Validating(object sender, CancelEventArgs e)
            {
               if ( WorkstationAETextBox.Text .Length == 0 ) 
               {
                  GenericErrorProvider.SetError ( WorkstationAETextBox, "AE Title can't be empty." ) ;
                  
                  e.Cancel = true ;
               }
               else if ( WorkstationAETextBox.Text .Length > 16 ) 
               {
                  GenericErrorProvider.SetError ( WorkstationAETextBox, "AE Title must be less than 16 characters." ) ;
                  
                  e.Cancel = true ;
               }
               else 
               {
                  GenericErrorProvider.SetError ( WorkstationAETextBox, string.Empty ) ;
               }
            }
            
            void WorkstationAETextBox_TextChanged(object sender, EventArgs e)
            {
               try
               {
                  __UpdatingConfig = true ;
                  ConfigurationData.WorkstationClient.AETitle = WorkstationAETextBox.Text ;
               }
               catch {}
               finally
               {
                  __UpdatingConfig = false ;
               }
            }
   
            
         #endregion
   
         #region Properties
         
            private bool __UpdatingConfig
            {
               get ;
               set ;
            }
   
         #endregion
   
         #region Events
         
            void ClientConfiguration_Load ( object sender, EventArgs e )
            {
               try
               {
                  ReadConfiguration ( ) ;
                  
                  if ( WorkstationHostaddressComboBox.SelectedItem == null && WorkstationHostaddressComboBox.Items.Count > 0 ) 
                  {
                     try
                     {
                        __UpdatingConfig = true ;
                        
                        WorkstationHostaddressComboBox.SelectedIndex = 0 ;
                        
                        ConfigurationData.WorkstationClient.Address = WorkstationHostaddressComboBox.Items [ 0 ].ToString ( ) ;
                     }
                     finally
                     {
                        __UpdatingConfig = false ;
                     }
                  }
                  
                  RegisterEvents ( ) ;
               }
               catch ( Exception ) 
               {}
            }

            void ConfigurationData_ValueChanged ( object sender, EventArgs e )
            {
               ReadConfiguration ( ) ;
            }
            
            void cmbWorkstationIPaddress_SelectedIndexChanged(object sender, EventArgs e)
            {
               try
               {
                  ConfigurationData.WorkstationClient.Address = WorkstationHostaddressComboBox.SelectedItem.ToString ( ) ;
               }
               catch ( Exception ex ) 
               {
                  System.Diagnostics.Debug.Assert ( false, ex.Message ) ;
               }
               finally
               {
                  __UpdatingConfig = false ;
               }
            }
            
            void WorkstationPortMaskedTextBox_TextChanged ( object sender, EventArgs e )
            {
               try
               {
                  __UpdatingConfig = true ;
                  
                  ConfigurationData.WorkstationClient.Port = int.Parse ( WorkstationPortMaskedTextBox.Text ) ;
               }
               catch ( Exception ex ) 
               {
                  System.Diagnostics.Debug.Assert ( false, ex.Message ) ;
               }
               finally
               {
                  __UpdatingConfig = false ;
               }
            }
            
            void chkDebugMsg_CheckedChanged ( object sender, EventArgs e )
            {
               try
               {
                  ConfigurationData.Debugging.DisplayDetailedErrors = DisplayDetailDebugMsgCheckBox.Checked ;
               }
               catch ( Exception ex ) 
               {
                  System.Diagnostics.Debug.Assert ( false, ex.Message ) ;
               }
               finally
               {
                  __UpdatingConfig = false ;
               }
            }
            
            void chkEnableDebug_CheckedChanged(object sender, EventArgs e)
            {
               try
               {
                  __UpdatingConfig = true ;
                  
                  ConfigurationData.Debugging.Enable = EnableDebugCheckBox.Checked ;
                  
                  DebugGroupBox.Enabled = EnableDebugCheckBox.Checked ;
                  
                  if ( DebugLogFileCheckBox.Checked )
                  {
                     DebugLogFileNameTextBox.Focus ( ) ;
                  }
                  else
                  {
                     GenericErrorProvider.SetError ( DebugLogFileNameTextBox, "" ) ;
                  }
               }
               catch ( Exception ex ) 
               {
                  System.Diagnostics.Debug.Assert ( false, ex.Message ) ;
               }
               finally
               {
                  __UpdatingConfig = false ;
               }
            }
            
            void chkLogFile_CheckedChanged(object sender, EventArgs e)
            {
               try
               {
                  __UpdatingConfig = true ;
                  
                  ConfigurationData.Debugging.GenerateLogFile = DebugLogFileCheckBox.Checked ;
                  
                  if ( DebugLogFileCheckBox.Checked )
                  {
                     DebugLogFileNameTextBox.Focus ( ) ;
                  }
                  else
                  {
                     GenericErrorProvider.SetError ( DebugLogFileNameTextBox, "" ) ;
                  }
               }
               catch ( Exception ex ) 
               {
                  System.Diagnostics.Debug.Assert ( false, ex.Message ) ;
               }
               finally
               {
                  __UpdatingConfig = false ;
               }
            }
            
            void txtLogFileName_Validating(object sender, CancelEventArgs e)
            {
               if ( DebugLogFileNameTextBox.Text.Length == 0 && EnableDebugCheckBox.Checked && DebugLogFileCheckBox.Checked )
               {
                  e.Cancel = true ;
                  
                  GenericErrorProvider.SetError ( DebugLogFileNameTextBox, "Enter valid file name." ) ;
               }
               else
               {
                  e.Cancel = false ;
                  
                  GenericErrorProvider.SetError ( DebugLogFileNameTextBox, "" ) ;
               }
            }
            
            void txtLogFileName_Validated ( object sender, EventArgs e )
            {
               try
               {
                  __UpdatingConfig = true ;
                  
                  ConfigurationData.Debugging.LogFileName = DebugLogFileNameTextBox.Text ;
                  
                  GenericErrorProvider.SetError ( DebugLogFileNameTextBox, "" ) ;
               }
               catch ( Exception ex ) 
               {
                  System.Diagnostics.Debug.Assert ( false, ex.Message ) ;
               }
               finally
               {
                  __UpdatingConfig = false ;
               }
            }
            
            void chkUseCompression_CheckedChanged ( object sender, EventArgs e )
            {
               try
               {
                  __UpdatingConfig = true ;
                  
                  ConfigurationData.Compression.Enable = UseCompressionCheckBox.Checked ;
                  
                  CompressionGroupBox.Enabled = UseCompressionCheckBox.Checked ;
               }
               catch ( Exception ex ) 
               {
                  System.Diagnostics.Debug.Assert ( false, ex.Message ) ;
               }
               finally
               {
                  __UpdatingConfig = false ;
               }
            }
            
            void rbtnLossyCompression_CheckedChanged ( object sender, EventArgs e )
            {
               try
               {
                  __UpdatingConfig = true ;
                  
                  ConfigurationData.Compression.Lossy = LossyCompressionRadioButton.Checked ;
               }
               catch ( Exception ex ) 
               {
                  System.Diagnostics.Debug.Assert ( false, ex.Message ) ;
               }
               finally
               {
                  __UpdatingConfig = false ;
               }
            }

            void WorkstationSecureCheckBox_CheckedChanged(object sender, EventArgs e)
            {
               try
               {
                  __UpdatingConfig = true;

                  ConfigurationData.WorkstationClient.Secure = WorkstationSecureCheckBox.Checked;
               }
               catch (Exception ex)
               {
                  System.Diagnostics.Debug.Assert(false, ex.Message);
               }
               finally
               {
                  __UpdatingConfig = false;
               }
            }


      void ForceToAllClientsCheckBox_CheckedChanged(object sender, EventArgs e)
            {  
               try
               {
                  __UpdatingConfig = true ;
                  
                  ConfigurationData.SetClientToAllWorkstations = ForceToAllClientsCheckBox.Checked ;
               }
               catch ( Exception ex ) 
               {
                  System.Diagnostics.Debug.Assert ( false, ex.Message ) ;
               }
               finally
               {
                  __UpdatingConfig = false ;
               }
            }
            
            void MoveToWsClientRadioButton_CheckedChanged ( object sender, EventArgs e )
            {
               try
               {
                  __UpdatingConfig = true ;

                  ConfigurationData.MoveToWSClient = MoveToWsClientRadioButton.Checked ;

                  HandleDicomRetrieveUI();
               }
               catch ( Exception ex ) 
               {
                  System.Diagnostics.Debug.Assert ( false, ex.Message ) ;
               }
               finally
               {
                  __UpdatingConfig = false ;
               }
            }
            
            void chkContinueRetireve_CheckedChanged(object sender, EventArgs e)
            {
               try
               {
                  __UpdatingConfig = true ;
                  
                  ConfigurationData.ContinueRetrieveOnDuplicateInstance = ContinueRetireveOnDuplicateCheckBox.Checked ;
               }
               catch ( Exception ex ) 
               {
                  System.Diagnostics.Debug.Assert ( false, ex.Message ) ;
               }
               finally
               {
                  __UpdatingConfig = false ;
               }
            }
            
            void btnLogFile_Click(object sender, EventArgs e)
            {
               try
               {
                  if ( BrowseLogFileDialog.ShowDialog ( ) == DialogResult.OK ) 
                  {
                     DebugLogFileNameTextBox.Text = BrowseLogFileDialog.FileName ;
                     
                     GenericErrorProvider.SetError ( DebugLogFileNameTextBox, "" ) ;
                  }
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
               }
            }
         
            void chkLazyLoading_CheckedChanged ( object sender, EventArgs e )
            {
               try
               {
                  __UpdatingConfig = true ;
                  
                  ConfigurationData.ViewerLazyLoading.Enable = EnableLazyLoadingCheckBox.Checked ;
                  
                  LazyLoadingHiddenImagesMaskedTextBox.Enabled = EnableLazyLoadingCheckBox.Checked ;
               }
               catch ( Exception ex ) 
               {
                  System.Diagnostics.Debug.Assert ( false, ex.Message ) ;
               }
               finally
               {
                  __UpdatingConfig = false ;
               }
            }
            
            void mtxtHiddenImages_TextChanged(object sender, EventArgs e)
            {
               try
               {
                  __UpdatingConfig = true ;
                  
                  ConfigurationData.ViewerLazyLoading.HiddenImages = int.Parse ( LazyLoadingHiddenImagesMaskedTextBox.Text ) ;
               }
               catch ( Exception ex ) 
               {
                  System.Diagnostics.Debug.Assert ( false, ex.Message ) ;
               }
               finally
               {
                  __UpdatingConfig = false ;
               }
            }
            
            void AutoSizeOverlayTextCheckBox_CheckedChanged ( object sender, EventArgs e )
            {
               try
               {
                  __UpdatingConfig = true ;
                  
                  ConfigurationData.ViewerAutoSizeOverlayText = AutoSizeOverlayTextCheckBox.Checked ;
                  FixedOverlayTextSizeNumericUpDown.Enabled = !AutoSizeOverlayTextCheckBox.Checked ;
               }
               catch ( Exception ex ) 
               {
                  System.Diagnostics.Debug.Assert ( false, ex.Message ) ;
               }
               finally
               {
                  __UpdatingConfig = false ;
               }
            }
            
            void FixedOverlayTextSizeNumericUpDown_ValueChanged ( object sender, EventArgs e )
            {
               try
               {
                  __UpdatingConfig = true ;
                  
                  ConfigurationData.ViewerOverlayTextSize = ( int ) FixedOverlayTextSizeNumericUpDown.Value ;
               }
               catch ( Exception ex ) 
               {
                  System.Diagnostics.Debug.Assert ( false, ex.Message ) ;
               }
               finally
               {
                  __UpdatingConfig = false ;
               }
            }
            
            void FullScreenModeCheckBox_CheckedChanged(object sender, EventArgs e)
            {
               try
               {
                  __UpdatingConfig = true ;
                  
                  ConfigurationData.RunFullScreen  = FullScreenModeCheckBox.Checked ;
               }
               catch ( Exception ex ) 
               {
                  System.Diagnostics.Debug.Assert ( false, ex.Message ) ;
               }
               finally
               {
                  __UpdatingConfig = false ;
               }
            }
            
            void AlwaysSaveSessionRadioButton_CheckedChanged(object sender, EventArgs e)
            {
               try
               {
                  __UpdatingConfig = true ;
                  
                  if ( sender is RadioButton )
                  {
                     RadioButton saveSessionBehav ;
                     
                     
                     saveSessionBehav = ( RadioButton ) sender ;
                     
                     if ( saveSessionBehav.Tag is SaveOptions )
                     {
                        ConfigurationData.SaveSessionBehavior = ( SaveOptions ) saveSessionBehav.Tag ;
                     }
                  }
               }
               catch ( Exception ex ) 
               {
                  System.Diagnostics.Debug.Assert ( false, ex.Message ) ;
               }
               finally
               {
                  __UpdatingConfig = false ;
               }
            }
            
            void DisplayOrientationButton_Click(object sender, EventArgs e)
            {
               try
               {
                  OrientationConfigDialog orientationView ;
                  
                  
                  orientationView = new OrientationConfigDialog ( ) ;
                  
                  orientationView.Configuration = WorkstationShellController.Instance.DisplayOrientation ;
                  
                  if ( orientationView.ShowDialog ( ) == DialogResult.OK ) 
                  {
                     WorkstationShellController.Instance.UpdateDisplayOrientation ( orientationView.Configuration ) ;
                  }
               }
               catch ( Exception exception ) 
               {
                  ThreadSafeMessager.ShowError (  exception.Message ) ;
               }
            }
   
         #endregion
   
         #region Data Members
         
            private bool _canChangeClientInfo ;
   
         #endregion
   
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
