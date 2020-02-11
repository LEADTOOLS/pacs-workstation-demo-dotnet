// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System ;
using System.Collections.Generic ;
using System.ComponentModel ;
using System.Drawing ;
using System.Data ;
using System.Text ;
using System.Windows.Forms ;
using System.Threading ;
using Leadtools.Dicom ;
using Leadtools.Dicom.Scu.Common ;
using Leadtools.Medical.Workstation.Client ;
using Leadtools.Demos.Workstation.Configuration ;
using Leadtools.Medical.Workstation.DataAccessLayer;
using Leadtools.Medical.DataAccessLayer;
using Leadtools.Medical.Workstation.DataAccessLayer.Configuration;
using Leadtools.Medical.Workstation;
//using Leadtools.Demos.Workstation.UI.Search.QueryDataSet;


namespace Leadtools.Demos.Workstation
{
   public partial class SearchStudies : UserControl
   {
      #region Public
   
         #region Methods

            public SearchStudies ( )
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
            
            public void QueryStudies ( FindQuery query, DicomClientMode mode ) 
            {
               FillQueryFields ( query ) ;
               
               DoSearchStudies ( DicomClientFactory.CreateQueryClient ( mode ), query ) ;
            }

            private void FillQueryFields ( FindQuery query )
            {
               PersonNameComponent patientName = GetPersonNameComponents ( query.PatientName ) ;
               PersonNameComponent referDrName = GetPersonNameComponents ( query.ReferringPhysiciansName ) ;
               
               PatientLastNameTextBox.Text = patientName.FamilyName ;
               PatientGivenNameTextBox.Text = patientName.GivenName ;
               PatientIDTextBox.Text   = query.PatientId ;
               StudiesIDTextBox.Text   = query.StudyId ;
               
               AccessionNumberTextBox.Text = query.AccessionNumber ;
               RefDrLastNameTextBox.Text = referDrName.FamilyName ;
               RefDrGivenNameTextBox.Text = referDrName.GivenName ;
               
               if ( query.StudyDate.StartDate.HasValue || query.StudyTime.StartTime.HasValue )
               {
                  StudyFromDateTimePicker.Value = GetFromDate ( query ) ;
                  StudyFromDateTimePicker.Checked = true ;
               }
               else
               {
                  StudyFromDateTimePicker.Checked = false ;
               }
               
               if ( query.StudyDate.EndDate.HasValue || query.StudyTime.EndTime.HasValue )
               {
                  StudyToDateTimePicker.Value   = GetToDate ( query ) ;
                  StudyToDateTimePicker.Checked = true ;
               }
               else
               {
                  StudyToDateTimePicker.Checked = false ;
               }
               
               //query.Modalities
               
               ModalitiesCheckedComboBox.ClearCheckedItems ( ) ;
               ModalitiesSelectAllCheckBox.Checked = false ;
               
            }

            private static DateTime GetFromDate ( FindQuery query )
            {
               DateTime studyDateFrom ;
               
               
               if ( query.StudyDate.StartDate.HasValue && query.StudyTime.StartTime.HasValue )
               {
                  studyDateFrom = new DateTime ( query.StudyDate.StartDate.Value.Year,
                                                 query.StudyDate.StartDate.Value.Month,
                                                 query.StudyDate.StartDate.Value.Day,
                                                 query.StudyTime.StartTime.Value.Hour,
                                                 query.StudyTime.StartTime.Value.Minute,
                                                 query.StudyTime.StartTime.Value.Seconds);
               }
               else if ( query.StudyDate.StartDate.HasValue )
               {
                  studyDateFrom = new DateTime ( query.StudyDate.StartDate.Value.Year,
                                                 query.StudyDate.StartDate.Value.Month,
                                                 query.StudyDate.StartDate.Value.Day,
                                                 0,
                                                 0,
                                                 0 ) ;
               }
               else
               {
                  studyDateFrom = new DateTime ( 1,
                                                 1,
                                                 1,
                                                 query.StudyTime.StartTime.Value.Hour,
                                                 query.StudyTime.StartTime.Value.Minute,
                                                 query.StudyTime.StartTime.Value.Seconds ) ;
               }
               
               return studyDateFrom;
            }
            
            private static DateTime GetToDate ( FindQuery query )
            {
               DateTime studyDateTo ;
               
               if (query.StudyDate.EndDate.HasValue && query.StudyTime.EndTime.HasValue)
               {
                  studyDateTo = new DateTime ( query.StudyDate.EndDate.Value.Year,
                                               query.StudyDate.EndDate.Value.Month,
                                               query.StudyDate.EndDate.Value.Day,
                                               query.StudyTime.EndTime.Value.Hour,
                                               query.StudyTime.EndTime.Value.Minute,
                                               query.StudyTime.EndTime.Value.Seconds ) ;
               }
               else if (query.StudyDate.StartDate.HasValue)
               {
                  studyDateTo = new DateTime ( query.StudyDate.EndDate.Value.Year,
                                               query.StudyDate.EndDate.Value.Month,
                                               query.StudyDate.EndDate.Value.Day,
                                               0,
                                               0,
                                               0 ) ;
               }
               else
               {
                  studyDateTo = new DateTime ( 1,
                                               1,
                                               1,
                                               query.StudyTime.EndTime.Value.Hour,
                                               query.StudyTime.EndTime.Value.Minute,
                                               query.StudyTime.EndTime.Value.Seconds);
               }
               
               return studyDateTo;
            }
            
            private PersonNameComponent GetPersonNameComponents ( string nameString )
            {
               string [] nameComponents ;
               PersonNameComponent personName ;
               
               
               personName = new PersonNameComponent ( ) ;
               
               if ( !string.IsNullOrEmpty ( nameString ) )
               {
                  nameComponents = nameString.Split ( '^' ) ;
                  
                  if ( nameComponents.Length > 0 )
                  {
                     personName.FamilyName = nameComponents [ 0 ] ;
                  }
                  
                  if ( nameComponents.Length > 1 ) 
                  {
                     personName.GivenName = nameComponents [ 1 ] ;
                  }
                  
                  if ( nameComponents.Length > 2 ) 
                  {
                     personName.MiddleName = nameComponents [ 2 ] ;
                  }
                  
                  if ( nameComponents.Length > 3 ) 
                  {
                     personName.NamePrefix = nameComponents [ 3 ] ;
                  }
                  
                  if ( nameComponents.Length > 4 ) 
                  {
                     personName.NameSuffix = nameComponents [ 4 ] ;
                  }
               }
               
               return personName ;
            }

         #endregion
   
         #region Properties
         
         #endregion
   
         #region Events
         
            public event EventHandler DisplayViewer ;
            public event EventHandler <SearchCompletedEventArgs> StudySearchCompleted ;
   
         #endregion
   
         #region Data Types Definition
         
            public class SearchEventArgs : EventArgs
            {
               public SearchEventArgs ( FindQuery query ) 
               {
                  Query = query ;
               }
               
               public FindQuery Query
               {
                  get
                  {
                     return _query ;
                  }
                  
                  private set
                  {
                     _query = value ;
                  }
               }
               
               private FindQuery _query ;
            }
            
            private class PersonNameComponent
            {
               public string FamilyName ;
               public string GivenName ;
               public string MiddleName ;
               public string NamePrefix ;
               public string NameSuffix ;
            }
   
         #endregion
   
         #region Callbacks
         
            public event EventHandler <SearchEventArgs>        FindStudiesQuery ;
            public event EventHandler <SearchEventArgs>        FindSeriesQuery ;
            public event EventHandler <ProcessSeriesEventArgs> LoadSeries ;
            public event EventHandler <ProcessSeriesEventArgs> AddSeriesToMediaBurning ;
            public event EventHandler <ProcessSeriesEventArgs> AddSeriesToLocalMediaBurning ;
            public event EventHandler <StoreSeriesEventArgs>   StoreSeries ;
            public event EventHandler <StoreSeriesEventArgs>   RetrieveSeries ;
   
         #endregion
   
      #endregion
   
      #region Protected
   
         #region Methods

            private const int WM_KEYDOWN = 0x0100 ;

            protected override bool ProcessKeyPreview ( ref Message m )
            {
               if ( m.Msg == WM_KEYDOWN && m.WParam.ToInt32 ( ) == (int) ConsoleKey.Enter && SearchButton.Enabled )
               {
                  btnSearch_Click ( this, EventArgs.Empty ) ;
               }
               
               return base.ProcessKeyPreview ( ref m ) ;
            }
   
            protected virtual void DoSearchStudies ( QueryClient client ) 
            {
               DoSearchStudies ( client, GetQueryParams ( ) ) ;
            }
            
            protected virtual void DoSearchStudies ( QueryClient client, FindQuery query ) 
            {
               try
               {
                  UpdateSearchStartedUI ( ) ;
                  
                  ClientQueryDataSet studies ;
                  
                  
                  if ( PacsServerRadioButton.Checked )
                  {
                     _lastSearchServer = ConfigurationData.ActivePacs ;
                  }
                  else
                  {
                     _lastSearchServer = null ;
                  }
                  
                  activeClient = client ;
                  
                  OnFindStudies ( this, new SearchEventArgs ( query ) ) ;
                  
                  studies = ConvertStudiesToDotNetADO ( client.FindStudies ( query ) ) ;
                  
                  if ( InvokeRequired ) 
                  {
                     Invoke ( new ParameterizedThreadStart ( UpdateStudies ), studies ) ; 
                  }
                  else
                  {
                     UpdateStudies ( studies ) ;
                  }
                  
                  OnStudySearchCompleted ( this, new SearchCompletedEventArgs ( query, studies ) ) ;
               }
               catch ( Leadtools.Dicom.Scu.Common.ClientCommunicationCanceled exception )
               {
                  ThreadSafeMessager.ShowError ( 
                                                 exception.Message ) ;
               }
               catch ( Leadtools.Dicom.Scu.Common.ClientConnectionException exception ) 
               {
                  ThreadSafeMessager.ShowError ( 
                                                 string.Format ( "Error retrieving studies.\n{0}\nDICOM Code: {1}\n{2}", 
                                                                  exception.Message,
                                                                  exception.Code,
                                                                  DicomException.GetCodeMessage ( exception.Code ) ) ) ;
               
                  ShowDetailedError ( exception ) ;
                  
                  return ;
               }
               catch ( Leadtools.Dicom.Scu.Common.ClientAssociationException exception ) 
               {
                  ThreadSafeMessager.ShowError (  string.Format ( "Error retrieving studies.\n{0}\nDICOM Reason: {1}\n{2}", 
                                                                  exception.Message,
                                                                  exception.Reason, 
                                                                  WorkstationUtils.GetAssociationReasonMessage ( exception.Reason ) ) ) ;
               
                  ShowDetailedError ( exception ) ;
                  
                  return ;
               }
               catch ( Leadtools.Dicom.Scu.Common.ClientCommunicationException exception ) 
               {
                  ThreadSafeMessager.ShowError ( "Error retrieving studies.\n" + exception.Message + 
                                                 "\nDICOM Status: " + exception.Status ) ;
               
                  ShowDetailedError ( exception ) ;
                  
                  return ;
               }
               catch ( DicomException exception )
               {
                  ThreadSafeMessager.ShowError ( "Error retrieving studies.\n" + 
                                                 exception.Message ) ;
               
                  ShowDetailedError ( exception ) ;
                  
                  return ;
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  ThreadSafeMessager.ShowError (  "Error retrieving studies." ) ;
               
                  ShowDetailedError ( exception ) ;
               }
               finally
               {
                  try
                  {
                     UpdateSearchCompletedUI ( ) ;
                  }
                  catch {}
               }

            }

            protected void OnStudySearchCompleted 
            ( 
               object sender,
               SearchCompletedEventArgs e
            )
            {
               if ( null != StudySearchCompleted ) 
               {
                  StudySearchCompleted ( sender, e ) ;
               }
            }
            
            protected virtual void OnFindStudies ( object sender, SearchEventArgs query ) 
            {
               if ( null != FindStudiesQuery )
               {
                  FindStudiesQuery ( sender, query ) ;
               }
            }
            
            protected virtual void OnFindSeries ( object sender, SearchEventArgs query ) 
            {
               if ( null != FindSeriesQuery )
               {
                  FindSeriesQuery ( sender, query ) ;
               }
            }
            
            protected virtual void OnLoadSeries ( object sender, ProcessSeriesEventArgs args ) 
            {
               if ( null != LoadSeries ) 
               {
                  LoadSeries ( sender, args ) ;
               }
            }
            
            protected virtual void OnStoreSeries (object sender, StoreSeriesEventArgs args )
            {
               if ( StoreSeries != null ) 
               {
                  StoreSeries ( sender, args ) ;
               }
            }
            
            protected virtual void OnRetrieveSeries ( object sender, StoreSeriesEventArgs args )
            {
               if ( RetrieveSeries != null ) 
               {
                  RetrieveSeries ( sender, args ) ;
               }
            }
            
            protected virtual void OnAddSeriesToMediaBurning ( ProcessSeriesEventArgs series )
            {
               if ( null != AddSeriesToMediaBurning )
               {
                  AddSeriesToMediaBurning ( this, series ) ;
               }
            }
            
            protected virtual void OnAddSeriesToLocalMediaBurning ( ProcessSeriesEventArgs series )
            {
               if ( null != AddSeriesToLocalMediaBurning )
               {
                  AddSeriesToLocalMediaBurning ( this, series ) ;
               }
            }
         
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
                  StudiesDataGridView.AutoGenerateColumns = false ;
                  SeriesDataGridView.AutoGenerateColumns  = false ;
                  CancelSearchButton.Enabled              = false ;
                  StudyToDateTimePicker.Checked            = false ;
                  StudyFromDateTimePicker.Checked          = false ;
                  
                  Leadtools.Medical.Winforms.Control.CheckedComboBox.FillModalities ( ModalitiesCheckedComboBox ) ;
                  
                  this.Load += new EventHandler ( SearchStudies_Load ) ;
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  throw ;
               }
            }
         
            private void RegisterEvents ( ) 
            {
               try
               {
                  this.VisibleChanged   += new EventHandler ( SearchStudies_VisibleChanged ) ;
                  SearchButton.Click       += new EventHandler ( btnSearch_Click ) ;
                  ClearSearchButton.Click        += new EventHandler ( btnClear_Click ) ;
                  CancelSearchButton.Click       += new EventHandler ( btnCancel_Click ) ;

                  StudiesDataGridView.SelectionChanged      += new EventHandler ( dgvStudies_SelectionChanged ) ;
                  StudyFromDateTimePicker.ValueChanged       += new EventHandler ( dtpkStudyFrom_ValueChanged ) ;
                  StudyToDateTimePicker.ValueChanged         += new EventHandler ( dtpkStudyTo_ValueChanged ) ;
                  StudyFromDateTimePicker.EnabledChanged     += new EventHandler ( dtpkStudyFrom_EnabledChanged ) ;
                  StudyToDateTimePicker.EnabledChanged       += new EventHandler ( dtpkStudyTo_EnabledChanged ) ;
                  LocalDatabaseRadioButton.CheckedChanged += new EventHandler ( rbtnLocalDatabase_CheckedChanged ) ;
                  PacsServersComboBox.SelectedValueChanged  += new EventHandler ( cmbServers_SelectedValueChanged ) ;

                  ModalitiesCheckedComboBox.CheckChanged += new EventHandler(ModalitiesCheckedComboBox_CheckChanged);
                  ModalitiesSelectAllCheckBox.CheckStateChanged += new EventHandler ( chkSelectAll_CheckStateChanged ) ; 
                  
                  StudiesDataGridView.RowContextMenuStripNeeded += new DataGridViewRowContextMenuStripNeededEventHandler ( dgvStudies_RowContextMenuStripNeeded ) ;
                  SeriesDataGridView.RowContextMenuStripNeeded  += new DataGridViewRowContextMenuStripNeededEventHandler ( dgvSeries_RowContextMenuStripNeeded ) ;

                  StudiesDataGridView.CellMouseDown += new DataGridViewCellMouseEventHandler ( dgvStudies_CellMouseDown ) ;
                  SeriesDataGridView.CellMouseDown  += new DataGridViewCellMouseEventHandler ( dgvSeries_CellMouseDown ) ;

                  StudiesContextMenuStrip.Opening += new CancelEventHandler ( cmnuLocalToolStrip_Opening ) ;
                  SeriesContextMenuStrip.Opening  += new CancelEventHandler ( cmnuLocalToolStrip_Opening ) ;


                  StudiesContextMenuStrip.Closed += new ToolStripDropDownClosedEventHandler ( cmnuLocalToolStrip_Closed ) ;
                  SeriesContextMenuStrip.Closed  += new ToolStripDropDownClosedEventHandler ( cmnuLocalToolStrip_Closed ) ;

                  StoreStudyToolStripMenuItem.Click                        += new EventHandler ( storeStudyToolStripMenuItem_Click ) ;
                  StoreStudyToServersToolStripMenuItem.DropDownItemClicked += new ToolStripItemClickedEventHandler ( storeStudyToServersToolStripMenuItem_DropDownItemClicked ) ;

                  AddStudyToViewerToolStripMenuItem.Click += new System.EventHandler(this.addStudyToViewerToolStripMenuItem_Click);
                  OpenStudyInViewerToolStripMenuItem.Click += new System.EventHandler(this.openStudyInViewerToolStripMenuItem_Click);
                  AddToMediaBurningManagerToolStripMenuItem.Click += new EventHandler(AddToMediaBurningManagerToolStripMenuItem_Click);
                  AddToLocalMediaBurningManagerToolStripMenuItem.Click += new EventHandler(AddToLocalMediaBurningManagerToolStripMenuItem_Click);
                  SeriesDataGridView.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvSeries_CellMouseDoubleClick);
                  ViewSeriesToolStripMenuItem.Click += new System.EventHandler(this.ViewSeriesToolStripMenuItem_Click);
                  OpenInViewerToolStripMenuItem.Click += new System.EventHandler(this.openInViewerToolStripMenuItem_Click);
                  AddStudiesToQueuetoolStripMenuItem.Click += new System.EventHandler(this.addStudiesToQueuetoolStripMenuItem_Click);
                  AddSeriesToQueuetoolStripMenuItem.Click += new System.EventHandler(this.addSeriesToQueuetoolStripMenuItem_Click);

                  ConfigurationData.ValueChanged += new EventHandler ( ConfigurationData_ValueChanged ) ;
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  throw ;
               }
            }

            private void AddSelectedSeriesToStorageQueue ( ScpInfo storagePacs ) 
            {
               try
               {
                  if ( SeriesDataGridView.SelectedRows.Count == 0 )
                  {
                     return ;
                  }
                  
                  foreach ( DataGridViewRow row in SeriesDataGridView.SelectedRows )
                  {
                     ClientQueryDataSet.StudiesRow studyRow ;
                     ClientQueryDataSet.SeriesRow  seriesRow ;
                     StoreSeriesEventArgs          series ;
                     
                     
                     seriesRow = ( row.DataBoundItem as DataRowView ).Row as ClientQueryDataSet.SeriesRow ;
                     studyRow = ( ( ClientQueryDataSet ) StudiesDataGridView.DataSource ).Studies.FindByStudyInstanceUID ( seriesRow.StudyInstanceUID ) ;
                     
                     series = new StoreSeriesEventArgs ( storagePacs,
                                                         studyRow,
                                                         seriesRow ) ;
                     
                     OnStoreSeries ( this, series ) ;
                  }
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  throw ;
               }
            }

            private void OnDisplayViewer ( object sender, EventArgs e ) 
            {
               try
               {
                  if ( null != DisplayViewer ) 
                  {
                     DisplayViewer ( sender, e ) ;
                  }
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  throw ;
               }
            }
            
            private void FixDateTimePickersMinMaxValues ( ) 
            {
               try
               {
               
                  StudyFromDateTimePicker.ValueChanged   -= new EventHandler ( dtpkStudyFrom_ValueChanged ) ;
                  StudyToDateTimePicker.ValueChanged     -= new EventHandler ( dtpkStudyTo_ValueChanged ) ;
                  StudyFromDateTimePicker.EnabledChanged -= new EventHandler ( dtpkStudyFrom_EnabledChanged ) ;
                  StudyToDateTimePicker.EnabledChanged   -= new EventHandler ( dtpkStudyTo_EnabledChanged ) ;
                  
                  if ( StudyFromDateTimePicker.Checked )
                  {
                     if ( StudyToDateTimePicker.Checked )
                     {
                        StudyFromDateTimePicker.MaxDate = StudyToDateTimePicker.Value ;
                     }
                     else
                     {
                        StudyFromDateTimePicker.MaxDate = DateTimePicker.MaximumDateTime ;
                     }
                  }
                  
                  if ( StudyToDateTimePicker.Checked ) 
                  {
                     if ( StudyFromDateTimePicker.Checked )
                     {
                        StudyToDateTimePicker.MinDate = StudyFromDateTimePicker.Value ;
                     }
                     else
                     {
                        StudyToDateTimePicker.MinDate = DateTimePicker.MinimumDateTime ;
                     }
                  }
                  
                  
                  StudyFromDateTimePicker.ValueChanged   += new EventHandler ( dtpkStudyFrom_ValueChanged ) ;
                  StudyToDateTimePicker.ValueChanged     += new EventHandler ( dtpkStudyTo_ValueChanged ) ;
                  StudyFromDateTimePicker.EnabledChanged += new EventHandler ( dtpkStudyFrom_EnabledChanged ) ;
                  StudyToDateTimePicker.EnabledChanged   += new EventHandler ( dtpkStudyTo_EnabledChanged ) ;
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  throw ;
               }
            }
            
            private void UpdateStudies ( object data ) 
            {
               try
               {
                  ClientQueryDataSet studies = ( ClientQueryDataSet ) data ;
                  
                  StudiesDataGridView.DataSource = studies ;
                  
                  if ( studies.Tables.Count != 0 ) 
                  {
                     StudiesDataGridView.DataMember = studies.Studies.TableName ;
                     
                     if ( studies.Studies.Rows.Count == 0 )
                     {
                        ThreadSafeMessager.ShowInformation (  "No studies found matching your search criteria." ) ;
                     }
                  }
                  else
                  {
                     ThreadSafeMessager.ShowInformation (  "No studies found matching your search criteria." ) ;
                  }
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  throw ;
               }
            }
            
            private void UpdateSearchStartedUI ( ) 
            {
               try
               {
                  if ( InvokeRequired ) 
                  {
                     this.Invoke ( new MethodInvoker ( UpdateSearchStartedUI ) ) ;
                  }
                  else
                  {
                     SearchButton.Enabled = false ;
                     CancelSearchButton.Enabled = true ;
                     Cursor.Current = Cursors.WaitCursor ;
                  }
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  throw ;
               }
            }
            
            private void UpdateSearchCompletedUI ( ) 
            {
               try
               {
                  if ( InvokeRequired ) 
                  {
                     this.Invoke ( new MethodInvoker ( UpdateSearchCompletedUI ) ) ;
                  }
                  else
                  {
                     SearchButton.Enabled = true ;
                     CancelSearchButton.Enabled = false ;
                     Cursor.Current = Cursors.Default ;
                  }
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  throw ;
               }
            }
            
            private void BindSeries ( object seriesObject ) 
            {
               try
               {
                  ClientQueryDataSet          series ;
                  IWorkstationDataAccessAgent dataAccess ;
               
               
                  series = ( ClientQueryDataSet ) seriesObject ;
                  
                  SeriesDataGridView.DataSource = series ;
                  
                  if ( 0 !=- series.Tables.Count )
                  {
                     SeriesDataGridView.DataMember = series.Series.TableName ;
                  }
                  
                  dataAccess = DataAccessServices.GetDataAccessService<IWorkstationDataAccessAgent> ( ) ;
                  
                  if ( null != dataAccess ) 
                  {
                     foreach ( DataGridViewRow seriesGridRow in SeriesDataGridView.Rows ) 
                     {
                        if ( dataAccess.HasVolume ( seriesGridRow.Cells [ SeriesInstanceUIDDataGridViewTextBoxColumn.Name ].Value.ToString ( ) ) )
                        {
                           seriesGridRow.Cells [ VolumeReadyColumn.Name ].Value = Leadtools.Demos.Workstation.Properties.Resources.Green.ToBitmap ( ) ;
                        }
                        else
                        {
                           seriesGridRow.Cells [ VolumeReadyColumn.Name ].Value = Leadtools.Demos.Workstation.Properties.Resources.Red.ToBitmap ( ) ;
                        }
                     }
                  }
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  throw ;
               }
            }

            private delegate FindQuery FindQueryHandler ( ) ;
            
            private FindQuery GetQueryParams ( ) 
            {
             
               if ( InvokeRequired ) 
               {
                  return this.Invoke ( new FindQueryHandler ( GetQueryParams ) ) as FindQuery ;
               }
               else
               {
                  string patientName = string.Empty ;
                  string physName    = string.Empty ;
                  FindQuery query ;
                  
                  
                  patientName = PatientLastNameTextBox.Text.TrimEnd ( '*' ) + "*" ;
                  physName    = RefDrLastNameTextBox.Text.TrimEnd ( '*' ) + "*" ;
                  
                  if ( !string.IsNullOrEmpty ( PatientGivenNameTextBox.Text ) )
                  {
                     patientName += "^" + PatientGivenNameTextBox.Text.TrimEnd ( '*' ) + "*" ;
                  }
                  
                  if ( !string.IsNullOrEmpty ( RefDrGivenNameTextBox.Text ) )
                  {
                     physName += "^" + RefDrGivenNameTextBox.Text.TrimEnd ( '*' ) + "*" ;
                  }
                  
                  query = new FindQuery ( ) ;
                  
                  
                  query.PatientName             = patientName ;
                  query.PatientId               = PatientIDTextBox.Text ;
                  query.StudyId                 = StudiesIDTextBox.Text ;
                  
                  if ( ModalitiesCheckedComboBox.Text.Length > 0 )
                  {
                     query.Modalities.AddRange ( ModalitiesCheckedComboBox.Text.Split ( ',')) ;
                  }
                  
                  query.AccessionNumber         = AccessionNumberTextBox.Text ;
                  query.ReferringPhysiciansName = physName ;
                  
                  if ( StudyFromDateTimePicker.Checked ) 
                  {
                     query.StudyDate.StartDate = StudyFromDateTimePicker.Value ;
                  }
                  
                  if ( ( StudyToDateTimePicker.Checked ) )
                  {
                     query.StudyDate.EndDate = StudyToDateTimePicker.Value ;
                  }
                  
                  if ( StudyFromDateTimePicker.Checked )
                  {
                     query.StudyTime.StartTime = new Leadtools.Dicom.Common.DataTypes.Time ( StudyFromDateTimePicker.Value ) ;
                  }
                  
                  if ( StudyToDateTimePicker.Checked )
                  {
                     query.StudyTime.EndTime = new Leadtools.Dicom.Common.DataTypes.Time ( StudyToDateTimePicker.Value ) ;
                  }
                  
                  return query ;
               }
            }
            
            private void ShowDetailedError(Exception exception)
            {
               if ( WorkstationMessager.DetailedError )
               {
                  WorkstationMessager.ShowDetailedError ( this, exception.Message, exception ) ;
               }
            }
            
            private ClientQueryDataSet ConvertStudiesToDotNetADO ( DicomDataSet[] studyDataSet ) 
            {
               try
               {
                  ClientQueryDataSet resultSet ;
                  
                  
                  resultSet = new ClientQueryDataSet ( ) ;
                  
                  foreach ( DicomDataSet study in studyDataSet ) 
                  {
                     resultSet.Studies.AddStudiesRow ( GetStringValue ( study, DicomTag.PatientID ),
                                                       GetStringValue ( study, DicomTag.PatientName ),
                                                       GetStringValue ( study, DicomTag.PatientBirthDate ),
                                                       GetStringValue ( study, DicomTag.PatientSex ),
                                                       GetStringValue ( study, DicomTag.StudyDescription ),
                                                       GetStringValue ( study, DicomTag.StudyDate ),
                                                       GetStringValue ( study, DicomTag.AccessionNumber ),
                                                       GetStringValue ( study, DicomTag.ReferringPhysicianName ),
                                                       GetStringValue ( study, DicomTag.StudyInstanceUID ) ) ;
                     study.Dispose ( ) ;
                  }
                  
                  return resultSet ;
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  throw ;
               }
            }
            
            private ClientQueryDataSet ConvertSeriesToDotNetADO ( DicomDataSet [] seriesDataSet, string studyInstanceUID  ) 
            {
               try
               {
                  ClientQueryDataSet resultSet ;
                  

                  resultSet = new ClientQueryDataSet ( ) ;
                  
                  foreach ( DicomDataSet series in seriesDataSet )
                  {
                     resultSet.Series.AddSeriesRow ( GetStringValue ( series, DicomTag.SeriesNumber ),
                                                     GetStringValue ( series, DicomTag.SeriesDescription ),
                                                     GetStringValue ( series, DicomTag.Modality ),
                                                     GetStringValue ( series, DicomTag.SeriesDate ),
                                                     GetStringValue ( series, DicomTag.SeriesTime ),
                                                     GetStringValue ( series, DicomTag.NumberOfSeriesRelatedInstances ),
                                                     studyInstanceUID,
                                                     GetStringValue ( series, DicomTag.SeriesInstanceUID ) ) ;
                  
                     series.Dispose ( ) ;
                  }
                  
                  return resultSet ;
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  throw ;
               }
            }
            
            private string GetStringValue ( DicomDataSet dicomDataSet, long dicomTag )
            {
               DicomElement element ;
               
               element = dicomDataSet.FindFirstElement ( null, dicomTag, true ) ;
               
               if ( null != element && element.Length > 0 )
               {
                  return dicomDataSet.GetConvertValue ( element ) ;
               }
               else
               {
                  return string.Empty ;
               }
            }
            
            private static bool CanRetrieve ( )
            {
               return ( ConfigurationData.MoveToWSClient || 
                       ( !string.IsNullOrEmpty(ConfigurationData.WorkstationServiceAE) ) ) ;
            }
            
            private void UpdateQuerySourceUIState ( )
            {
               LocalDatabaseRadioButton.Enabled = ConfigurationData.SupportLocalQueriesStore ;
               PacsServerRadioButton.Enabled    = ConfigurationData.SupportDicomCommunication && ConfigurationData.PACS.Count != 0 ;
               
               if ( ConfigurationData.PACS.Count == 0 )
               {
                  PacsServersComboBox.Items.Clear ( ) ;
               }
               
               SearchSourceGroupBox.Visible = __DisplayQuerySource ;
            }
            
            private bool PerformStudiesSearch ( out FindQuery query )
            {
               string[] args  = System.Environment.GetCommandLineArgs ( ) ;
               bool     found = false ;
               
               
               query = new FindQuery ( ) ;
               
               foreach ( string arg in args ) 
               {
                  if ( arg.IndexOf ( "PatientId", StringComparison.OrdinalIgnoreCase ) == 0 )
                  {
                     query.PatientId = GetValueFromArg ( arg ) ;
                     
                     found = true ;
                     
                     break ;
                  }
               }
               
               return ( found || ConfigurationData.AutoQuery ) ;
            }
            
            private string GetValueFromArg ( string arg ) 
            {
               string [] tokens ;
               
               
               tokens = arg.Split ( '=' ) ;
               
               if ( tokens.Length == 2 ) 
               {
                  return tokens [ 1 ].Trim ( ) ;
               }
               else
               {
                  return null ;
               }
            }
            
            private void AddSeriesToMediaBurningManager ( )
            {
               try
               {
                  if ( SeriesDataGridView.SelectedRows.Count == 0 )
                  {
                     return ;
                  }
                  
                  foreach ( DataGridViewRow row in SeriesDataGridView.SelectedRows )
                  {
                     ClientQueryDataSet.StudiesRow studyRow ;
                     ClientQueryDataSet.SeriesRow  seriesRow ;
                     ProcessSeriesEventArgs        series ;
                     
                     
                     seriesRow = ( row.DataBoundItem as DataRowView ).Row as ClientQueryDataSet.SeriesRow ;
                     studyRow = ( ( ClientQueryDataSet ) StudiesDataGridView.DataSource ).Studies.FindByStudyInstanceUID ( seriesRow.StudyInstanceUID ) ;
                     
                     series = new ProcessSeriesEventArgs ( studyRow,
                                                         seriesRow ) ;
                     
                     
                     OnAddSeriesToMediaBurning ( series ) ;
                  }
               }
               catch ( Exception exception ) 
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
               }
            }
            
            private void AddSeriesToLocalMediaBurningManager ( )
            {
               try
               {
                  if ( SeriesDataGridView.SelectedRows.Count == 0 )
                  {
                     return ;
                  }
                  
                  foreach ( DataGridViewRow row in SeriesDataGridView.SelectedRows )
                  {
                     ClientQueryDataSet.StudiesRow studyRow ;
                     ClientQueryDataSet.SeriesRow  seriesRow ;
                     ProcessSeriesEventArgs        series ;
                     
                     
                     seriesRow = ( row.DataBoundItem as DataRowView ).Row as ClientQueryDataSet.SeriesRow ;
                     studyRow = ( ( ClientQueryDataSet ) StudiesDataGridView.DataSource ).Studies.FindByStudyInstanceUID ( seriesRow.StudyInstanceUID ) ;
                     
                     series = new ProcessSeriesEventArgs ( studyRow,
                                                         seriesRow ) ;
                     
                     
                     OnAddSeriesToLocalMediaBurning ( series ) ;
                  }
               }
               catch ( Exception exception ) 
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
               }
            }

         #endregion
   
         #region Properties
         
            private bool __DisplayQuerySource
            {
               get
               {
                  return ConfigurationData.SupportLocalQueriesStore || 
                         ConfigurationData.SupportDicomCommunication ;
               }
            }
            
         #endregion

         #region Events
         
            void SearchStudies_VisibleChanged(object sender, EventArgs e)
            {
               try
               {
                  if ( !Visible ) 
                  {
                     return ;
                  }

                  InitPACS ( ) ;
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
               }
            }

            private void InitPACS ( ) 
            {
               if ( ConfigurationData.PACS.Count == 0 && ConfigurationData.SupportLocalQueriesStore )
               {
                  LocalDatabaseRadioButton.Checked = true ;
               }
               
               UpdateQuerySourceUIState ( ) ;
               
               PacsServersComboBox.Items.Clear ( ) ;
               
               foreach ( ScpInfo server in ConfigurationData.PACS )
               {
                  PacsServersComboBox.Items.Add ( server ) ;
               }
               
               if ( PacsServersComboBox.Items.Count > 0 )
               {
                  if ( null != _lastSearchServer && PacsServersComboBox.Items.Contains ( _lastSearchServer ) )
                  {
                     PacsServersComboBox.SelectedItem = _lastSearchServer ;
                  }
                  else if ( null != ConfigurationData.ActivePacs )
                  {
                     PacsServersComboBox.SelectedItem = ConfigurationData.ActivePacs ;
                  }
                  else if ( null != ConfigurationData.DefaultQueryRetrieveServer  && PacsServersComboBox.Items.Contains ( ConfigurationData.DefaultQueryRetrieveServer ) )
                  {
                     PacsServersComboBox.SelectedItem = ConfigurationData.DefaultQueryRetrieveServer ;
                  }
                  else
                  {
                     PacsServersComboBox.SelectedIndex = 0 ;
                  }
               }
            }

         
            void btnSearch_Click ( object sender, EventArgs e )
            {
               Cursor.Current = Cursors.WaitCursor ;
               
               try
               {
                  StudiesDataGridView.DataSource = null ;
                  SeriesDataGridView.DataSource  = null ;
                  
                  QueryClient client =  DicomClientFactory.CreateQueryClient ( ) ;
                  
                  
                  Thread searchThread = new Thread ( delegate()
                  {
                     try
                     {
                        DoSearchStudies ( client ) ;
                     }
                     catch ( Exception exception )
                     {
                        System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                     }
                     } ) ;
                  
                  searchThread.IsBackground = true ;
                  searchThread.Start ( ) ;
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  ThreadSafeMessager.ShowError (  "Error retrieving studies.\n" + exception.Message ) ;

                  ShowDetailedError ( exception)  ;
               }
               finally
               {
                  Cursor.Current = Cursors.Default ;
               }
            }

            void btnClear_Click(object sender, EventArgs e)
            {
               try
               {
                  PatientLastNameTextBox.Text = string.Empty ;
                  PatientGivenNameTextBox.Text = string.Empty ;
                  PatientIDTextBox.Text   = string.Empty ;
                  StudiesIDTextBox.Text   = string.Empty ;
                  ModalitiesCheckedComboBox.ClearCheckedItems ( ) ;
                  AccessionNumberTextBox.Text = string.Empty ;
                  RefDrLastNameTextBox.Text = string.Empty ;
                  RefDrGivenNameTextBox.Text = string.Empty ;
                  StudyFromDateTimePicker.Checked = false ;
                  StudyToDateTimePicker.Checked   = false ;
                  ModalitiesSelectAllCheckBox.Checked = false ;

                  StudiesDataGridView.DataSource = null ;
                  StudiesDataGridView.DataMember = string.Empty ;
                  
                  SeriesDataGridView.DataSource = null ;
                  SeriesDataGridView.DataMember = string.Empty ;
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
               }
            }
            
            void btnCancel_Click ( object sender, EventArgs e )
            {
               try
               {
                  if ( null != activeClient ) 
                  {
                     activeClient.CancelFind ( ) ;
                     
                     activeClient = null ;
                  }
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
               }
            }

            void dtpkStudyFrom_ValueChanged ( object sender, EventArgs e )
            {
               try
               {
                  FixDateTimePickersMinMaxValues ( ) ;
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
               }
            }
            
            void dtpkStudyTo_ValueChanged ( object sender, EventArgs e )
            {
               try
               {
                  FixDateTimePickersMinMaxValues ( ) ;
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
               }
            }
            
            void dtpkStudyFrom_EnabledChanged(object sender, EventArgs e)
            {
               try
               {
                  FixDateTimePickersMinMaxValues ( ) ;
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
               }
            }
            
            void dtpkStudyTo_EnabledChanged ( object sender, EventArgs e )
            {
               try
               {
                  FixDateTimePickersMinMaxValues ( ) ;
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
               }
            }
            
            void dgvStudies_SelectionChanged ( object sender, EventArgs e )
            {
               Cursor.Current = Cursors.WaitCursor ;
               
               try
               {
                  object studyInstanceUID ;
                  
                  
                  if ( StudiesDataGridView.SelectedRows.Count == 0 )
                  {
                     return ;
                  }
                  
                  if ( null != StudiesDataGridView.SelectedRows [ 0 ].Cells [ StudyInstanceUIDDataGridViewTextBoxColumn.Name ] )
                  {
                     studyInstanceUID = StudiesDataGridView.SelectedRows [ 0 ].Cells [ StudyInstanceUIDDataGridViewTextBoxColumn.Name ].Value ;
                  }
                  else
                  {
                     return ;
                  }
                  
                  if ( null != studyInstanceUID )
                  {
                     QueryClient        client ;
                     ClientQueryDataSet series ;
                     
                     
                     client =  DicomClientFactory.CreateQueryClient ( ) ;
                     
                     try
                     {
                        FindQuery query ;
                        
                        
                        query = new FindQuery ( ) ;
                        
                        OnFindSeries ( this, new SearchEventArgs ( query ) ) ;
                        
                        query.StudyInstanceUID = studyInstanceUID.ToString ( ) ;
                        
                        series = ConvertSeriesToDotNetADO ( client.FindSeries ( query ), query.StudyInstanceUID ) ;
                     }
                     catch ( Leadtools.Dicom.Scu.Common.ClientCommunicationCanceled exception )
                     {
                        ThreadSafeMessager.ShowError ( 
                                                       exception.Message ) ;
                                             
                        return ;
                     }
                     catch ( Leadtools.Dicom.Scu.Common.ClientConnectionException exception ) 
                     {
                        ThreadSafeMessager.ShowError ( 
                                                       string.Format ( "Error retrieving studies.\n{0}\nDICOM Code: {1}\n{2}", 
                                                                        exception.Message,
                                                                        exception.Code,
                                                                        DicomException.GetCodeMessage ( exception.Code ) ) ) ;
                     
                        ShowDetailedError ( exception ) ;
                        
                        return ;
                     }
                     catch ( Leadtools.Dicom.Scu.Common.ClientAssociationException exception ) 
                     {
                        ThreadSafeMessager.ShowError ( 
                                                        string.Format ( "Error retrieving studies.\n{0}\nDICOM Reason: {1}\n{2}", 
                                                                        exception.Message,
                                                                        exception.Reason, 
                                                                        WorkstationUtils.GetAssociationReasonMessage ( exception.Reason ) ) ) ;
                     
                        ShowDetailedError ( exception ) ;
                        
                        return ;
                     }
                     catch ( Leadtools.Dicom.Scu.Common.ClientCommunicationException exception ) 
                     {
                        ThreadSafeMessager.ShowError ( 
                                                       "Error retrieving series.\n" + exception.Message + 
                                                       "\nDICOM Status: " + exception.Status ) ;
                     
                        ShowDetailedError ( exception ) ;
                        
                        return ;
                     }                     
                     catch ( DicomException exception )
                     {
                        ThreadSafeMessager.ShowError ( 
                                                       "Error retrieving series.\n" + 
                                                        exception.Message ) ;
                     
                        ShowDetailedError ( exception ) ;
                        
                        return ;
                     }
                     catch ( Exception exception )
                     {
                        System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                       
                        ThreadSafeMessager.ShowError (  "Error retrieving series.\n" + exception.Message ) ;
                     
                        ShowDetailedError ( exception ) ;
                        
                        return ;
                     }
                     
                     if ( InvokeRequired )
                     {
                        this.Invoke ( new ParameterizedThreadStart ( BindSeries ), series ) ;
                     }
                     else
                     {
                        BindSeries ( series ) ;
                     }
                  }
               }
               catch ( Exception exception )
               {
                  ThreadSafeMessager.ShowError (  "Error retrieving data.\n" + exception.Message ) ;
               
                  ShowDetailedError ( exception ) ;
               }
               finally
               {
                  Cursor.Current = Cursors.Default ;
               }
            }
            
            
            private void ViewSeriesToolStripMenuItem_Click
            (
               object sender, 
               EventArgs e
            )
            {
               try
               {
                  if ( SeriesDataGridView.SelectedRows.Count == 0 )
                  {
                     return ;
                  }
                  
                  foreach ( DataGridViewRow row in SeriesDataGridView.SelectedRows )
                  {
                      ClientQueryDataSet.SeriesRow seriesRow = ( row.DataBoundItem as DataRowView ).Row as ClientQueryDataSet.SeriesRow ;
                      ClientQueryDataSet.StudiesRow studyRow = ( ( ClientQueryDataSet ) StudiesDataGridView.DataSource ).Studies.FindByStudyInstanceUID ( seriesRow.StudyInstanceUID ) ;
                      
                     
                     OnLoadSeries ( this, new ProcessSeriesEventArgs  ( studyRow, seriesRow ) ) ;
                  }
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  ThreadSafeMessager.ShowError (  "Error loading series." ) ;
               
                  ShowDetailedError ( exception ) ;
                  
                  return ;
               }
            }
            
            private void openInViewerToolStripMenuItem_Click(object sender, EventArgs e)
            {
               try
               {
                  OnDisplayViewer ( this, new EventArgs ( ) ) ;
                  
                  ViewSeriesToolStripMenuItem_Click ( sender, e ) ;
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
               }
            }
            
            private void dgvSeries_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
            {
               try
               {
                  if ( _lastSearchServer == null ) 
                  {
                     OnDisplayViewer ( this, EventArgs.Empty ) ;
                     
                     ViewSeriesToolStripMenuItem_Click ( sender, e ) ;
                  }
                  else
                  {
                     addSeriesToQueuetoolStripMenuItem_Click ( this, new EventArgs ( ) ) ;
                  }
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
               }
            }

            private void addStudyToViewerToolStripMenuItem_Click ( object sender, EventArgs e )
            {
               try
               {
                  SeriesDataGridView.SelectAll ( ) ;
                  
                  ViewSeriesToolStripMenuItem_Click ( sender, e ) ;
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
               }
            }
            
            private void openStudyInViewerToolStripMenuItem_Click(object sender, EventArgs e)
            {
               try
               {
                  OnDisplayViewer ( sender, e ) ;
                  
                  addStudyToViewerToolStripMenuItem_Click ( sender, e ) ;
                  
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
               }
            }
            
            private void addStudiesToQueuetoolStripMenuItem_Click(object sender, EventArgs e)
            {
               try
               {
                  SeriesDataGridView.SelectAll ( ) ;
                  
                  addSeriesToQueuetoolStripMenuItem_Click ( this, new EventArgs ( ) ) ;
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
               }
            }
            
            private void AddToMediaBurningManagerToolStripMenuItem_Click ( object sender, EventArgs e )
            {
               try
               {
                  if ( _applyToAllSeries ) 
                  {
                     SeriesDataGridView.SelectAll ( ) ;
                  }
                  
                  AddSeriesToMediaBurningManager ( ) ;
               }
               catch ( Exception exception ) 
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
               }
            }
            
            private void AddToLocalMediaBurningManagerToolStripMenuItem_Click ( object sender, EventArgs e )
            {
               try
               {
                  if ( _applyToAllSeries ) 
                  {
                     SeriesDataGridView.SelectAll ( ) ;
                  }
                  
                  AddSeriesToLocalMediaBurningManager ( ) ;
               }
               catch ( Exception exception ) 
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
               }
            }
            
            private void addSeriesToQueuetoolStripMenuItem_Click ( object sender, EventArgs e )
            {
               try
               {
                  if ( SeriesDataGridView.SelectedRows.Count == 0 )
                  {
                     return ;
                  }
                  
                  foreach ( DataGridViewRow row in SeriesDataGridView.SelectedRows )
                  {
                     ClientQueryDataSet.StudiesRow studyRow ;
                     ClientQueryDataSet.SeriesRow  seriesRow ;
                     StoreSeriesEventArgs          series ;
                     
                     
                     seriesRow = ( row.DataBoundItem as DataRowView ).Row as ClientQueryDataSet.SeriesRow ;
                     studyRow = ( ( ClientQueryDataSet ) StudiesDataGridView.DataSource ).Studies.FindByStudyInstanceUID ( seriesRow.StudyInstanceUID ) ;
                     
                     series = new StoreSeriesEventArgs ( ConfigurationData.ActivePacs,
                                                         studyRow,
                                                         seriesRow ) ;
                     
                     
                     OnRetrieveSeries  ( this, series ) ;
                  }
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
               }
            }
            
            private void chkSelectAll_CheckStateChanged ( object sender, EventArgs e )
            {
               try
               {
                  ModalitiesCheckedComboBox.CheckChanged -= new EventHandler ( ModalitiesCheckedComboBox_CheckChanged ) ;
                  
                  if ( ( ModalitiesSelectAllCheckBox.Checked ) && ( ModalitiesSelectAllCheckBox.CheckState == CheckState.Checked ) )
                  {
                     ModalitiesCheckedComboBox.CheckAllItems ( ) ;
                  }
                  else if ( ( !ModalitiesSelectAllCheckBox.Checked ) && ( ModalitiesSelectAllCheckBox.CheckState == CheckState.Unchecked ) )
                  {
                     ModalitiesCheckedComboBox.ClearCheckedItems ( ) ;
                  }
                  
                  ModalitiesCheckedComboBox.CheckChanged += new EventHandler ( ModalitiesCheckedComboBox_CheckChanged ) ;
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
               }
            }

            private void ModalitiesCheckedComboBox_CheckChanged(object sender, EventArgs e)
            {
               try
               {
                  ModalitiesSelectAllCheckBox.CheckStateChanged -= new EventHandler ( chkSelectAll_CheckStateChanged ) ; 
                  
                  if ( ModalitiesCheckedComboBox.GetCheckedItemsCount ( ) == ModalitiesCheckedComboBox.Items.Count )
                  {
                     ModalitiesSelectAllCheckBox.Checked = true ;
                     ModalitiesSelectAllCheckBox.CheckState = CheckState.Checked ;
                  }
                  else if ( ModalitiesCheckedComboBox.GetCheckedItemsCount ( ) == 0 )
                  {
                     ModalitiesSelectAllCheckBox.Checked = false ;
                     ModalitiesSelectAllCheckBox.CheckState = CheckState.Unchecked ;
                  }
                  else
                  {
                     ModalitiesSelectAllCheckBox.Checked = true ;
                     ModalitiesSelectAllCheckBox.CheckState = CheckState.Indeterminate ;
                  }
                  
                  ModalitiesSelectAllCheckBox.CheckStateChanged += new EventHandler ( chkSelectAll_CheckStateChanged ) ; 
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
               }

            }
            
            void dgvStudies_RowContextMenuStripNeeded
            (
               object sender, 
               DataGridViewRowContextMenuStripNeededEventArgs e
            )
            {
               try
               {
                  if ( _lastSearchServer != null ) 
                  {
                     AddStudiesToQueuetoolStripMenuItem.Enabled = CanRetrieve ( ) ;
                     
                     e.ContextMenuStrip = ServerStudiesContextMenuStrip ;
                  }
                  else
                  {
                     e.ContextMenuStrip = StudiesContextMenuStrip ;
                  }
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
               }
            }

            void dgvSeries_RowContextMenuStripNeeded(object sender, DataGridViewRowContextMenuStripNeededEventArgs e)
            {
               try
               {
                  if ( _lastSearchServer != null ) 
                  {
                     AddSeriesToQueuetoolStripMenuItem.Enabled = CanRetrieve ( ) ;
                     
                     e.ContextMenuStrip = ServerSeriesContextMenuStrip ;
                  }
                  else
                  {
                     e.ContextMenuStrip = SeriesContextMenuStrip ;
                  }
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
               }
            }
            
            void dgvStudies_CellMouseDown ( object sender, DataGridViewCellMouseEventArgs e )
            {
               try
               {
                  DataGridView studiesGrid ;
                  
                  
                  studiesGrid = ( DataGridView ) sender ;
                  
                  if ( e.ColumnIndex >= 0 && 
                       e.RowIndex >= 0  && 
                       e.Button == MouseButtons.Right ) 
                  {
                     if ( studiesGrid.SelectedRows.Count > 0 && 
                          studiesGrid.Rows.IndexOf ( studiesGrid.SelectedRows [ 0 ] ) == e.RowIndex )
                     {
                        return ;
                     }
                     
                     if ( ( Control.ModifierKeys & Keys.Control ) != Keys.Control )
                     {
                        studiesGrid.ClearSelection ( ) ;
                     }
                     
                     studiesGrid.Rows [ e.RowIndex ].Selected = true ;
                  }
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
               }
            }
            
            void dgvSeries_CellMouseDown ( object sender, DataGridViewCellMouseEventArgs e )
            {
               try
               {
                  if ( e.ColumnIndex >= 0 && 
                       e.RowIndex >= 0  && 
                       e.Button == MouseButtons.Right ) 
                  {
                     ( ( DataGridView ) sender ).Rows [ e.RowIndex ].Selected = true ;
                  }
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
               }
            }
            
            void cmnuLocalToolStrip_Opening ( object sender, CancelEventArgs e )
            {
               try
               {
                  if ( ConfigurationData.SupportLocalQueriesStore )
                  {
                     ToolStripManager.Merge ( LocalMediaBurningContextMenuStrip, 
                                              (ToolStrip)sender ) ;
                  }
                  
                  if ( ConfigurationData.SupportDicomCommunication )
                  {
                     ToolStripManager.Merge ( ServersStudyStoreContextMenuStrip, 
                                              ( ToolStrip ) sender ) ;
                  }
                  
                  if ( ConfigurationData.PACS.Count == 0 ) 
                  {
                     StoreStudyToolStripMenuItem.Enabled          = false ;
                     StoreStudyToServersToolStripMenuItem.Enabled = false ;
                  }
                  else
                  {
                     ScpInfo storePacs = null ;
                     
                     
                     StoreStudyToolStripMenuItem.Enabled          = true ;
                     StoreStudyToServersToolStripMenuItem.Enabled = true ;
                     
                     
                     if ( ConfigurationData.DefaultStorageServer != null ) 
                     {
                        storePacs = ConfigurationData.DefaultStorageServer ;
                     }
                     else if ( ConfigurationData.ActivePacs != null ) 
                     {
                        storePacs = ConfigurationData.ActivePacs ;
                     }
                     else
                     {
                        storePacs = ConfigurationData.PACS [ 0 ] ;
                     }
                     
                     ServersStudyStoreContextMenuStrip.Enabled = true ;
                     
                     if ( ( (ContextMenuStrip) sender ).SourceControl == StudiesDataGridView )
                     {
                        _applyToAllSeries = true ;
                        
                        StoreStudyToolStripMenuItem.Text = string.Format ( "Store Study ({0})",
                                                                           storePacs.AETitle ) ;
                     }
                     else
                     {
                        _applyToAllSeries = false ;
                        
                        StoreStudyToolStripMenuItem.Text = string.Format ( "Store Series ({0})",
                                                                           storePacs.AETitle ) ;
                     }
                                                                        
                     StoreStudyToolStripMenuItem.Tag = storePacs ;

                     StoreStudyToServersToolStripMenuItem.DropDownItems.Clear ( ) ;
                     
                     foreach ( ScpInfo scp in ConfigurationData.PACS ) 
                     {
                        ToolStripItem pacsItem ;
                        
                        
                        pacsItem = StoreStudyToServersToolStripMenuItem.DropDownItems.Add ( scp.AETitle ) ;
                        
                        pacsItem.Tag = scp ;
                     }
                  }
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
               }
            }
            
            void cmnuLocalToolStrip_Closed
            ( 
               object sender, 
               ToolStripDropDownClosedEventArgs e 
            )
            {
               try
               {
                  ToolStripManager.RevertMerge ( ( ToolStrip ) sender ) ;
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
               }
            }
            
            void storeStudyToolStripMenuItem_Click ( object sender, EventArgs e )
            {
               try
               {
                 
                  if ( _applyToAllSeries == true )
                  {
                     SeriesDataGridView.SelectAll ( ) ;
                  }

                  AddSelectedSeriesToStorageQueue ( ( ScpInfo ) StoreStudyToolStripMenuItem.Tag ) ;
                  
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  ThreadSafeMessager.ShowError (  exception.Message ) ;
               }
            }
            
            void storeStudyToServersToolStripMenuItem_DropDownItemClicked
            (
               object sender, 
               ToolStripItemClickedEventArgs e
            )
            {
               try
               {
                  if ( _applyToAllSeries == true )
                  {
                     SeriesDataGridView.SelectAll ( ) ;
                  }

                  AddSelectedSeriesToStorageQueue ( ( ScpInfo ) e.ClickedItem.Tag ) ;
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  ThreadSafeMessager.ShowError (   exception.Message ) ;
               }
            }
            
            void ConfigurationData_ValueChanged ( object sender, EventArgs e )
            {
               try
               {
                  LocalDatabaseRadioButton.Checked = ( ConfigurationData.ClientBrowsingMode == DicomClientMode.LocalDb ) ;
                  PacsServerRadioButton.Checked    = ( ConfigurationData.ClientBrowsingMode == DicomClientMode.Pacs ) ;
                  
                  PacsServersComboBox.Enabled = PacsServerRadioButton.Checked ;
                  
                  UpdateQuerySourceUIState ( ) ;

                  UpdateStudyDateQueryUI ( ) ;
               }
               catch ( Exception ) 
               {}
            }

            private void UpdateStudyDateQueryUI()
            {
               StudyFromDateTimePicker.Enabled = ConfigurationData.ClientBrowsingMode != DicomClientMode.DicomDir ;
               StudyToDateTimePicker.Enabled   = ConfigurationData.ClientBrowsingMode != DicomClientMode.DicomDir ;
               StudyFromDateTimePicker.Checked = ( ConfigurationData.ClientBrowsingMode != DicomClientMode.DicomDir ) ? StudyFromDateTimePicker.Checked : false ;
               StudyToDateTimePicker.Checked   = ( ConfigurationData.ClientBrowsingMode != DicomClientMode.DicomDir ) ? StudyToDateTimePicker.Checked : false ;
            }

            
            void SearchStudies_Load ( object sender, EventArgs e )
            {
               try
               {
                  InitPACS ( ) ;
                  
                  LocalDatabaseRadioButton.Checked = ( ConfigurationData.ClientBrowsingMode == DicomClientMode.LocalDb ) ;
                  PacsServerRadioButton.Checked    = ( ConfigurationData.ClientBrowsingMode == DicomClientMode.Pacs ) ;
                  
                  PacsServersComboBox.Enabled = PacsServerRadioButton.Checked ;

                  UpdateQuerySourceUIState ( ) ;
                  
                  UpdateStudyDateQueryUI ( ) ;
                  
                  RegisterEvents ( ) ;
                  
                  try
                  {
                     FindQuery query ;
                           
                     if ( PerformStudiesSearch ( out query ) )
                     {
                        QueryStudies ( query, ConfigurationData.ClientBrowsingMode ) ;
                     }
                  }
                  catch ( Exception exception )
                  {
                     ThreadSafeMessager.ShowError ( exception.Message ) ;
                  }
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
               }
            }
            
            void rbtnLocalDatabase_CheckedChanged(object sender, EventArgs e)
            {
               try
               {
                  if ( __DisplayQuerySource )
                  {
                     ConfigurationData.ClientBrowsingMode = ( LocalDatabaseRadioButton.Checked ) ? DicomClientMode.LocalDb : DicomClientMode.Pacs ;
                     PacsServersComboBox.Enabled          = PacsServerRadioButton.Checked ;
                  }
                  
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  throw ;
               }
            }
            
            void cmbServers_SelectedValueChanged(object sender, EventArgs e)
            {
               ConfigurationData.ActivePacs = ( ScpInfo ) PacsServersComboBox.SelectedItem ;
            }
            
         #endregion
   
         #region Data Members
         
            private bool _applyToAllSeries = false ;
            QueryClient  activeClient = null  ;
            ScpInfo      _lastSearchServer ;
   
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
   
   public class SearchCompletedEventArgs : EventArgs
   {
      public SearchCompletedEventArgs ( FindQuery query, ClientQueryDataSet result ) 
      {
         Query  = query ;
         Result = result ;
      }
      
      public FindQuery Query
      {
         get 
         {
            return _query ;
         }
         
         private set 
         {
            _query = value ;
         }
      }
      
      public ClientQueryDataSet Result
      {
         get
         {
            return _result ;
         }
         
         private set
         {
            _result = value ;
         }
      }
      
      private FindQuery          _query ;
      private ClientQueryDataSet _result ;
   }
}
