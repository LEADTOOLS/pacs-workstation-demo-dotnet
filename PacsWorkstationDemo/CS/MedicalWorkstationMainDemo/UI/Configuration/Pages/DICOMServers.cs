// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System ;
using System.Collections.Generic ;
using System.Net;
using System.Windows.Forms ;
using Leadtools.Demos.Workstation.Configuration ;
using Leadtools.DicomDemos;


namespace Leadtools.Demos.Workstation
{
   public partial class DICOMServers : UserControl
   {
      #region Public
   
         #region Methods
         
            public DICOMServers ( )
            {
               try
               {
                  InitializeComponent ( ) ;
                  
                  Init ( ) ;
                  
                  RegisterEvents ( ) ;
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  throw ;
               }
            }
            
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
                  _valueChangedRegistered = false ;
                  
                  ReadConfiguration ( ) ;
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  throw exception ;
               }
            }

            private void RegisterEvents ( ) 
            {
               try
               {
                  this.Load                              += new EventHandler ( DICOMServers_Load ) ;
                  AddDicomServerToolStripButton.Click    += new EventHandler ( btnAddServer_Click ) ;
                  DeleteDicomServerToolStripButton.Click += new EventHandler ( btnDeleteServer_Click ) ;
                  
                  DICOMServersDataGridView.RowValidating    += new DataGridViewCellCancelEventHandler ( grdvDICOMServers_RowValidating ) ;
                  DICOMServersDataGridView.RowValidated     += new DataGridViewCellEventHandler       ( grdvDICOMServers_RowValidated ) ;
                  DICOMServersDataGridView.CellValidating += new DataGridViewCellValidatingEventHandler(grdvDICOMServers_CellValidating);
                  DICOMServersDataGridView.CellValidated    += new DataGridViewCellEventHandler       ( grdvDICOMServers_CellValidated ) ;
                  DICOMServersDataGridView.SelectionChanged += new EventHandler                       ( grdvDICOMServers_SelectionChanged ) ;
                  DICOMServersDataGridView.UserDeletedRow   += new DataGridViewRowEventHandler        ( grdvDICOMServers_UserDeletedRow ) ;
                  DICOMServersDataGridView.CellContentClick += new DataGridViewCellEventHandler       ( grdvDICOMServers_CellContentClick ) ;
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  throw exception ;
               }
            }

            private void ReadConfiguration ( )
            {
               if ( InvokeRequired ) 
               {
                  Invoke ( new MethodInvoker ( ReadConfiguration ) ) ;
                  
                  return ;
               }
               else
               {
                  UnregisterValueChanged (  ) ;
                  
                  try
                  {
                     __DICOMServers = ConfigurationData.PACS ;
                     
                     DICOMServersDataGridView.Rows.Clear ( ) ;
                     
                     foreach ( ScpInfo server in ConfigurationData.PACS ) 
                     {
                        bool defaultQueryretrieve ;
                        bool defaultStore ;
                        int rowIndex  ;
                        

                        defaultQueryretrieve = ( null != ConfigurationData.DefaultQueryRetrieveServer && ConfigurationData.DefaultQueryRetrieveServer.Equals ( server ) ) ;
                        defaultStore         = ( null != ConfigurationData.DefaultStorageServer && ConfigurationData.DefaultStorageServer.Equals ( server ) ) ;
                        
                        rowIndex = DICOMServersDataGridView.Rows.Add (  new object [ ] { server.AETitle, 
                                                                                 server.Address,
                                                                                 server.Port,
                                                                                 server.Timeout,
                                                                                 server.Secure,
                                                                                 defaultQueryretrieve,
                                                                                 defaultStore } ) ;
                                                                      
                        DICOMServersDataGridView.Rows [ rowIndex ].Tag = server ;
                     }
                     
                     DeleteDicomServerToolStripButton.Enabled = DICOMServersDataGridView.SelectedRows.Count > 0 ;
                  }
                  finally
                  {
                     RegisterValueChanged (  ) ;
                  }
               }
            }

            void grdvDICOMServers_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
            {
               try
               {
                  DataGridViewRow validatingRow ;
                  
                  
                  validatingRow = DICOMServersDataGridView.Rows [ e.RowIndex ] ;
                  
                  try
                  {
                     int number ;
                     
                     
                     
                     if ( ( e.ColumnIndex == AETitleColumn.Index ) && 
                          ( ( null == e.FormattedValue ) ||
                          ( string.IsNullOrEmpty ( e.FormattedValue.ToString ( ) ) ) ) ) 
                     {
                        validatingRow.ErrorText = "AE Title can't be empty" ; 
                        
                        e.Cancel = true ;
                     }
                     else if ( ( e.ColumnIndex == AETitleColumn.Index ) && 
                               ( ( null != e.FormattedValue &&
                                   e.FormattedValue.ToString ( ).Length > 16 ) ) )
                     {
                        validatingRow.ErrorText = "AE Title must be less than 16 characters" ; 
                        
                        e.Cancel = true ;
                     }
                     else if ( e.ColumnIndex == IPAddressColumn.Index )
                     {
                        if ( ( null == e.FormattedValue ) ||
                             ( string.IsNullOrEmpty ( e.FormattedValue.ToString ( ) ) ) )
                        {
                           validatingRow.ErrorText = "host can't be empty." ;
                           
                           e.Cancel = true ;
                        }
                        else
                        {
                           try
                           {
                              Utils.ResolveIPAddress ( validatingRow.Cells [ IPAddressColumn.Name ].EditedFormattedValue.ToString ( ) ) ;
                           }
                           catch ( Exception exception )
                           {
                              validatingRow.ErrorText = exception.Message ;
                              
                              e.Cancel = true ;
                           }
                        }
                     }
                     else if ( ( e.ColumnIndex == PortColumn.Index ) && 
                               ( null == e.FormattedValue ||
                                 string.IsNullOrEmpty ( e.FormattedValue.ToString ( ) ) || 
                                 !int.TryParse ( e.FormattedValue.ToString ( ).Replace ( ",", "" ), out number ) ) )
                     {
                        validatingRow.ErrorText = "Invalid Port number." ;
                        
                        e.Cancel = true ;
                     }
                     else if ( ( e.ColumnIndex == TimeoutColumn.Index ) && 
                               ( null == validatingRow.Cells [ TimeoutColumn.Name ].EditedFormattedValue ||
                                 string.IsNullOrEmpty ( e.FormattedValue.ToString ( ) ) || 
                                 !int.TryParse ( e.FormattedValue.ToString ( ).Replace ( ",", "" ), out number ) ) )
                     {
                        validatingRow.ErrorText = "Invalid timeout value." ;
                        
                        e.Cancel = true ;
                     }
                     else
                     {
                        validatingRow.ErrorText = "" ;
                     }
                  }
                  catch ( Exception exception )
                  {
                     e.Cancel = true ;
                     
                     validatingRow.ErrorText = exception.Message ;
                  }
               }
               catch ( Exception )
               {
                  e.Cancel = true ;
               }
            }
            
   
         #endregion
   
         #region Properties
         
            private IList <ScpInfo> __DICOMServers
            {
               get
               {
                  return _dicomServers ;
               }
               
               set
               {
                  _dicomServers = value ;
               }
            }

   
         #endregion
   
         #region Events
         
            void ConfigurationData_ValueChanged(object sender, EventArgs e)
            {
               try
               {
                  ReadConfiguration ( ) ;
               }
               catch ( Exception ) 
               {}
            }
            
            void DICOMServers_Load ( object sender, EventArgs e )
            {
               try
               {
                  if ( Visible ) 
                  {
                     ReadConfiguration ( ) ;
                  }
                  
                  RegisterValueChanged (  ) ;
               }
               catch ( Exception )
               {}
            }
         
            void btnAddServer_Click ( object sender, EventArgs e )
            {
               UnregisterValueChanged ( ) ;
               
               
               try
               {
                  if ( !this.Validate ( ) )
                  {
                     return ;
                  }
                  
                  ScpInfo scp ;
                  
                  
                  scp = new ScpInfo ( ) ;
                  
                  int rowIndex = DICOMServersDataGridView.Rows.Add ( ) ;
                  
                  DataGridViewRow pacsRow = DICOMServersDataGridView.Rows [ rowIndex ] ;
               
                  pacsRow.Tag      = scp ;
                  pacsRow.ReadOnly = false ;
                  pacsRow.Selected = true ;
                  
                  __DICOMServers.Add ( scp ) ;
                  
                  DICOMServersDataGridView.CurrentCell = pacsRow.Cells [  0 ] ;
                  DICOMServersDataGridView.ShowEditingIcon = true ;
                  DICOMServersDataGridView.BeginEdit ( false ) ;
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  
               }
               finally
               {
                  RegisterValueChanged ( ) ;
               }
            }
            
            void btnDeleteServer_Click ( object sender, EventArgs e ) 
            {
               try
               {
                  UnregisterValueChanged (  ) ;
                  
                  if ( DICOMServersDataGridView.SelectedRows.Count != 0 )
                  {
                     DataGridViewRow deleteRow ;
                     ScpInfo server ;
                     
                     
                     deleteRow = DICOMServersDataGridView.SelectedRows [ 0 ] ;
                     
                     server = ( ScpInfo ) deleteRow.Tag ;
                     
                     if ( __DICOMServers.Contains ( server ) )
                     {
                        __DICOMServers.Remove ( server ) ;
                     }
                     
                     DICOMServersDataGridView.Rows.Remove ( deleteRow ) ;
                  }
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
               }
               finally
               {
                  RegisterValueChanged (  ) ;
               }
            }
            
            void grdvDICOMServers_RowValidating
            (
               object sender, 
               DataGridViewCellCancelEventArgs e
            )
            {
               try
               {
                  DataGridViewRow validatingRow ;
                  int number ;
                  
                  validatingRow = DICOMServersDataGridView.Rows [ e.RowIndex ] ;
                  
                  if ( ( null == validatingRow.Cells [ AETitleColumn.Name ].EditedFormattedValue ) ||
                       ( string.IsNullOrEmpty ( validatingRow.Cells [ AETitleColumn.Name ].EditedFormattedValue.ToString ( ) ) ) ) 
                  {
                     validatingRow.ErrorText = "AE Title can't be empty" ; 
                     
                     e.Cancel = true ;
                     
                     return ;
                  }
                  
                  if ( ( null != validatingRow.Cells [ AETitleColumn.Name ].EditedFormattedValue &&
                       validatingRow.Cells [ AETitleColumn.Name ].EditedFormattedValue.ToString ( ).Length > 16 ) ) 
                  {
                     validatingRow.ErrorText = "AE Title must be less than 16 characters" ; 
                     
                     e.Cancel = true ;
                     
                     return ;
                  }
                  
                  if ( ( null == validatingRow.Cells [ IPAddressColumn.Name ].EditedFormattedValue ) ||
                       ( string.IsNullOrEmpty ( validatingRow.Cells [ IPAddressColumn.Name ].EditedFormattedValue.ToString ( ) ) )  )
                  {
                     validatingRow.ErrorText = "host can't be empty." ;
                     
                     e.Cancel = true ;
                     
                     return ;
                  }
                  
                  try
                  {
                     Utils.ResolveIPAddress ( validatingRow.Cells [ IPAddressColumn.Name ].EditedFormattedValue.ToString ( ) ) ;
                  }
                  catch ( Exception exception )
                  {
                     validatingRow.ErrorText = exception.Message ;
                     
                     e.Cancel = true ;
                     
                     return ;
                  }
                  
                  if ( ( null == validatingRow.Cells [ PortColumn.Name ].EditedFormattedValue ) ||
                       ( string.IsNullOrEmpty ( validatingRow.Cells [ PortColumn.Name ].EditedFormattedValue.ToString ( ) ) ) || 
                       ( !int.TryParse ( validatingRow.Cells [ PortColumn.Name ].EditedFormattedValue.ToString ( ).Replace ( ",", "" ), out number ) ) )
                  {
                     validatingRow.ErrorText = "Invalid Port number." ;
                     
                     e.Cancel = true ;
                     
                     return ;
                  }
                  
                  if ( ( null == validatingRow.Cells [ TimeoutColumn.Name ].EditedFormattedValue ) ||
                       ( string.IsNullOrEmpty ( validatingRow.Cells [ TimeoutColumn.Name ].EditedFormattedValue.ToString ( ) ) ) || 
                       ( !int.TryParse ( validatingRow.Cells [ TimeoutColumn.Name ].EditedFormattedValue.ToString ( ).Replace ( ",", "" ), out number ) ) )
                  {
                     validatingRow.ErrorText = "Invalid timeout value." ;
                     
                     e.Cancel = true ;
                     
                     return ;
                  }
                  
                  validatingRow.ErrorText = "" ;
                  
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  throw ;
               }
            }
            
            void grdvDICOMServers_RowValidated 
            ( 
               object sender, 
               DataGridViewCellEventArgs e 
            )
            {
               try
               {
                  UnregisterValueChanged (  ) ;

                  DataGridViewRow validatingRow ;
                  ScpInfo scp ;
                  
                  validatingRow = DICOMServersDataGridView.Rows [ e.RowIndex ] ;
                  
                  scp = ( ScpInfo ) validatingRow.Tag ;
                  
                  scp.AETitle = validatingRow.Cells [ AETitleColumn.Name ].EditedFormattedValue.ToString ( ) ;
                  scp.Address = validatingRow.Cells [ IPAddressColumn.Name ].EditedFormattedValue.ToString ( ) ;
                  scp.Port    = int.Parse ( validatingRow.Cells [ PortColumn.Name ].EditedFormattedValue.ToString ( ).Replace ( ",", "" ) ) ;
                  scp.Timeout = int.Parse ( validatingRow.Cells [ TimeoutColumn.Name ].EditedFormattedValue.ToString ( ).Replace ( ",", "" ) ) ;
                  
                  if ( !__DICOMServers.Contains ( scp ) )
                  {
                     __DICOMServers.Add ( scp ) ;
                  }
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
               }
               finally
               {
                  RegisterValueChanged (  ) ;
               }
            }
            
            void grdvDICOMServers_CellValidated
            (
               object sender, 
               DataGridViewCellEventArgs e
            )
            {
               try
               {
                  DataGridViewRow validatingRow ;
                  ScpInfo scp ;
                  
                  
                  UnregisterValueChanged (  ) ;
                  
                  validatingRow = DICOMServersDataGridView.Rows [ e.RowIndex ] ;
                  
                  scp = ( ScpInfo ) validatingRow.Tag ;
                  
                  if ( e.ColumnIndex == AETitleColumn.Index ) 
                  {
                     scp.AETitle = validatingRow.Cells [ AETitleColumn.Name ].EditedFormattedValue.ToString ( ) ;
                  }
                  else if ( e.ColumnIndex == IPAddressColumn.Index )
                  {
                     scp.Address = validatingRow.Cells [ IPAddressColumn.Name ].EditedFormattedValue.ToString ( ) ;
                  }
                  else if ( e.ColumnIndex == PortColumn.Index ) 
                  {
                     scp.Port = int.Parse ( validatingRow.Cells [ PortColumn.Name ].EditedFormattedValue.ToString ( ).Replace ( ",", "" ) ) ;
                  }
                  else if ( e.ColumnIndex == TimeoutColumn.Index ) 
                  {
                     scp.Timeout = int.Parse ( validatingRow.Cells [ TimeoutColumn.Name ].EditedFormattedValue.ToString ( ).Replace ( ",", "" ) ) ;
                  }
                  else if ( e.ColumnIndex == SecureColumn.Index ) 
                  {
                     scp.Secure = bool.Parse ( validatingRow.Cells [SecureColumn.Name ].Value.ToString());
                  }
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
               }
               finally
               {
                  RegisterValueChanged ( ) ;
               }
            }
            
            void grdvDICOMServers_SelectionChanged ( object sender, EventArgs e )
            {
               try
               {
                  DeleteDicomServerToolStripButton.Enabled = DICOMServersDataGridView.SelectedRows.Count != 0 ;
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
               }
            }
            
            private void grdvDICOMServers_UserDeletedRow
            (
               object sender, 
               DataGridViewRowEventArgs e
            )
            {
               try
               {
                  UnregisterValueChanged ( ) ;
                  
                  ScpInfo server ;
                  
                  
                  server = ( ScpInfo ) e.Row.Tag ;
                  
                  if ( __DICOMServers.Contains (  server ) )
                  {
                     __DICOMServers.Remove ( server ) ;
                  }
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
               }
               finally
               {
                  RegisterValueChanged ( ) ;
               }
            }

            private void RegisterValueChanged ( )
            {
               if ( !_valueChangedRegistered )
               {
                  ConfigurationData.ValueChanged += new EventHandler ( ConfigurationData_ValueChanged ) ;
                  
                  _valueChangedRegistered = true ;
               }
            }

            private void UnregisterValueChanged ( )
            {
               if ( _valueChangedRegistered )
               {
                  ConfigurationData.ValueChanged -= new EventHandler ( ConfigurationData_ValueChanged ) ;
                  
                  _valueChangedRegistered = false ;
               }
            }
            
            void grdvDICOMServers_CellContentClick
            (
               object sender, 
               DataGridViewCellEventArgs e
            )
            {
               try
               {
                  UnregisterValueChanged (  ) ;
                  
                  if ( e.RowIndex < 0 || e.ColumnIndex < 0 ) 
                  {
                     return ;
                  }
                  
                  if ( ( DICOMServersDataGridView.Columns [ e.ColumnIndex ] == DefaultStoreServerColumn ) || 
                       ( DICOMServersDataGridView.Columns [ e.ColumnIndex ] == DefaultQueryRetrieveColumn ) 
                       )
                  {
                     bool defaultChceked ;
                     
                     
                     defaultChceked = ( bool ) DICOMServersDataGridView.Rows [ e.RowIndex ].Cells [ e.ColumnIndex ].EditedFormattedValue ;
                     
                     if ( defaultChceked ) 
                     {
                        for ( int rowIndedx = 0; rowIndedx < DICOMServersDataGridView.Rows.Count; rowIndedx ++ )
                        {
                           if ( rowIndedx != e.RowIndex )
                           {
                              DICOMServersDataGridView.Rows [ rowIndedx ].Cells [ e.ColumnIndex ].Value = false ;
                           }
                        }
                        
                        if ( DICOMServersDataGridView.Columns [ e.ColumnIndex ] == DefaultStoreServerColumn ) 
                        {
                           ConfigurationData.DefaultStorageServer = ( ScpInfo ) DICOMServersDataGridView.Rows [ e.RowIndex ].Tag ;
                        }
                        else
                        {
                           ConfigurationData.DefaultQueryRetrieveServer = ( ScpInfo ) DICOMServersDataGridView.Rows [ e.RowIndex ].Tag ;
                        }
                     }
                     else
                     {
                        if ( DICOMServersDataGridView.Columns [ e.ColumnIndex ] == DefaultStoreServerColumn ) 
                        {
                           ConfigurationData.DefaultStorageServer = null ;
                        }
                        else
                        {
                           ConfigurationData.DefaultQueryRetrieveServer = null ;
                        }
                     }
                  }
                  else if (DICOMServersDataGridView.Columns[e.ColumnIndex] == SecureColumn)
                  {
                     DataGridViewRow validatingRow = DICOMServersDataGridView.Rows[e.RowIndex];
                     ScpInfo scp = (ScpInfo)validatingRow.Tag;
                     scp.Secure = (bool)DICOMServersDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue;
                  }
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
               }
               finally
               {
                  RegisterValueChanged (  ) ;
               }
            }
   
         #endregion
   
         #region Data Members
         
            private IList <ScpInfo> _dicomServers ;
            private bool            _valueChangedRegistered ;
   
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
