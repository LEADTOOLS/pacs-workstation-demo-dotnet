// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leadtools.Medical.Workstation.Presenters;
using Leadtools.Medical.Workstation;
using System.Windows.Forms;
using System.IO;
using Leadtools.Medical.Media.AddIns.Composing;
using Leadtools.Dicom.Common.DataTypes.MediaCreation;
using Leadtools.Dicom.Common.DataTypes;
using Leadtools.Medical.Media.AddIns.UI;
using Leadtools.Medical.Media.AddIns;
using System.Configuration;
using Leadtools.DicomDemos;
using Leadtools.Medical.Workstation.UI.Factory;
#if FOR_DOTNET4
using System.Threading.Tasks;
#endif
namespace Leadtools.Demos.Workstation
{
   class LocalMediaInformationPresenter : MediaInformationPresenter <ILocalMediaInformationView>
   {
      protected override void DoInitializeView ( WorkstationContainer container, ILocalMediaInformationView view )
      {
         LocalMediaBurningPrefrences prefrences ;
         
         
         __View = (ILocalMediaInformationView) view ;

         prefrences = GetPreferences ( container ) ;
         
         __MediaObject          = null ;
         __View.CreateAutoRun   = prefrences.CreateAutoRun ;
         __View.IncludeViewer   = prefrences.IncludeViewer ;
         __View.MediaBaseFolder = prefrences.MediaBaseFolder ;
         __View.ViewerDirectory = prefrences.ViewerDirectory ;
         
         __View.PrepareMedia += new EventHandler ( view_PrepareMedia ) ;
         __View.BurnMedia    += new EventHandler ( view_BurnMedia ) ;
         
         __View.MediaBaseFolderChanged += new EventHandler ( OnUpdateUI ) ;
         __View.ViewerDirectoryChanged += new EventHandler ( OnUpdateUI ) ;
         __View.IncludeViewerChanged   += new EventHandler ( OnUpdateUI ) ;

         __View.CreateAutoRunChanged   += new EventHandler ( __View_SettingsChanged ) ;
         __View.MediaBaseFolderChanged += new EventHandler ( __View_SettingsChanged ) ;
         __View.MediaTitleChanged      += new EventHandler ( __View_SettingsChanged ) ;
         __View.ViewerDirectoryChanged += new EventHandler ( __View_SettingsChanged ) ;
         __View.IncludeViewerChanged   += new EventHandler ( __View_SettingsChanged ) ;
         
         BurningImagesState.ListChanged += new System.ComponentModel.ListChangedEventHandler ( __View_SettingsChanged ) ;
         
         UpdateUI ( ) ;
         
         __View.ActivateView ( container.State.ActiveWorkstation ) ;
         __View.ClearInstancesAfterRequest = true;
      }

      void __View_SettingsChanged(object sender, EventArgs e)
      {
         if ( null != __MediaObject )
         {
            __View.SetMediaCreationWarning ( "*The current created media may not reflect the current settings." ) ;
         }
      }

      private static LocalMediaBurningPrefrences GetPreferences(WorkstationContainer container)
      {
         LocalMediaBurningPrefrences prefrences ;
         
         if ( container.State.DataServices.IsRegistered<LocalMediaBurningPrefrences> ( ) )
         {
            prefrences = container.State.DataServices.Get<LocalMediaBurningPrefrences> ( ) ;
         }
         else 
         {
            prefrences = LocalMediaBurningPrefrences.Load ( ) ;
            
            container.State.DataServices.Register<LocalMediaBurningPrefrences> ( prefrences ) ;
         }
         
         return prefrences ;
      }

      private void SavePreferences ( )
      {
         LocalMediaBurningPrefrences prefrences ;
         
         
         prefrences = GetPreferences ( ViewerContainer ) ;
         
         
         prefrences.CreateAutoRun   = __View.CreateAutoRun ;
         prefrences.IncludeViewer   = __View.IncludeViewer ;
         prefrences.MediaBaseFolder = __View.MediaBaseFolder ;
         prefrences.ViewerDirectory = __View.ViewerDirectory ;
         
         prefrences.Save ( ) ;
      }
      
      class LocalMediaBurningPrefrences : ConfigurationSection
      {
         [System.Configuration.ConfigurationProperty ("mediaBaseFolder")]
         public string MediaBaseFolder 
         { get 
            {
               return this [ "mediaBaseFolder" ] as string ;
            }
            set 
            {
               this [ "mediaBaseFolder" ] = value ;
            }
         }
         [System.Configuration.ConfigurationProperty ("viewerDirectory")]
         public string ViewerDirectory 
         { 
         
            get 
            {
               return this [ "viewerDirectory" ] as string ;
            }
            
            set 
            {
               this [ "viewerDirectory" ] = value ;
            }
         }
         
         [System.Configuration.ConfigurationProperty ("includeViewer", DefaultValue=false)]
         public bool IncludeViewer 
         { 
            get 
            {
               return (bool) this [ "includeViewer" ] ;
            }
            
            set 
            {
               this ["includeViewer"] = value ;
            }
         }
         
         [System.Configuration.ConfigurationProperty ("createAutoRun", DefaultValue=false)]
         public bool CreateAutoRun 
         { 
            get 
            {
               return (bool) this [ "createAutoRun" ] ;
            }
            
            set 
            {
               this["createAutoRun"] = value ;
            }
         }
         
         public const string PreferencesSectionName = "localMediaBurningPrefrences" ;
         
         public static LocalMediaBurningPrefrences Load ( ) 
         {
            LocalMediaBurningPrefrences        prefrences ;
            System.Configuration.Configuration configuration ;
            
            
            if ( null == __configuration ) 
            {
               configuration = ConfigurationManager.OpenExeConfiguration ( ConfigurationUserLevel.PerUserRoaming ) ;
               
               __configuration = configuration ;
            }
            else
            {
               configuration = __configuration ;
            }
            
            if ( ( prefrences = ( LocalMediaBurningPrefrences ) configuration.Sections [ LocalMediaBurningPrefrences.PreferencesSectionName ] ) == null )
            {
               prefrences = new LocalMediaBurningPrefrences ( ) ;
               
               prefrences.SectionInformation.AllowExeDefinition = ConfigurationAllowExeDefinition.MachineToLocalUser ;
               
               
               prefrences.MediaBaseFolder = Path.Combine ( DicomDemoSettingsManager.GetFolderPath ( ), "Media" ) ;
               
               configuration.Sections.Add ( LocalMediaBurningPrefrences.PreferencesSectionName, prefrences ) ;
               
               configuration.Save ( ) ;
            }
            
            prefrences.SectionInformation.ForceSave = true ;
            
            return prefrences ;
         }
         
         public void Save (  ) 
         {
            System.Configuration.Configuration configuration ;
            
            
            if ( null == __configuration )
            {
               configuration = ConfigurationManager.OpenExeConfiguration ( ConfigurationUserLevel.PerUserRoaming ) ;
               
               __configuration = configuration ;
            }
            else
            {
               configuration = __configuration ;
            }
            
            configuration.Save ( ConfigurationSaveMode.Modified ) ;
         }
         
         private static System.Configuration.Configuration __configuration ;
      }

      protected override void OnMediaTitleChanged()
      {
         base.OnMediaTitleChanged ( ) ;
         
         UpdateUI ( ) ;
      }
      private double GetFolderSize ( string folderPath ) 
      {
         FileInfo[] files = new DirectoryInfo ( folderPath ).GetFiles ( "*", SearchOption.AllDirectories ) ;
         double     size  = 0 ;
         
         foreach ( FileInfo file in files ) 
         {
            size += file.Length ;
         }
         
         return size ;
      }
      
      private void UpdateUI ( ) 
      {
         if ( BurningImagesState.Count == 0 || 
              __View.MediaTitle.Length == 0 ||
              !string.IsNullOrEmpty ( ValidateMediaTitle ( __View.MediaTitle ) ) ||
              __View.MediaBaseFolder.Length == 0 ||
              ( __View.IncludeViewer && __View.ViewerDirectory.Length == 0 ) ) 
            {
               __View.CanBurnMedia    = false ;
               __View.CanPrepareMedia = false ;
            }
            else
            {
               __View.CanPrepareMedia = true ;
               
               if ( __View.MediaDirectory.Length > 0 && null != __MediaObject )
               {
                  __View.CanBurnMedia = true ;
               }
               else
               {
                  __View.CanBurnMedia = false ;
               }
            }
            
            if ( __View.ViewerDirectory.Length > 0 )
            {
               __View.ViewerSize = GetFormattedSize ( GetFolderSize ( __View.ViewerDirectory ) ) ;
            }
            else
            {
               __View.ViewerSize = "0.0" ;
            }
      }
      
      
      private string GetFormattedSize ( double numberOfBytes ) 
      {
         string formattedCapacity ;
         
         if ( numberOfBytes >= 0x40000000 )
         {
            formattedCapacity = Convert.ToString ( numberOfBytes / 0x40000000 ) ;
            
            if ( formattedCapacity.Length > 5 )
            {
               formattedCapacity = formattedCapacity.Substring ( 0, 5 ) + " GB" ;
            }
            
         }
         else if ( numberOfBytes >= 0x100000 )
         {
            formattedCapacity = Convert.ToString ( numberOfBytes / 0x100000 ) ;
            
            if ( formattedCapacity.Length > 4 ) 
            {
               formattedCapacity = formattedCapacity.Substring(0, 4) + " MB" ;
            }
         }
         else
         {
            formattedCapacity = Convert.ToString(numberOfBytes / 0x400) ;
            
            if ( formattedCapacity.Length > 3 ) 
            {
               formattedCapacity = formattedCapacity.Substring(0, 3) + " KB" ;
            }
         }
         
         return formattedCapacity ;
      }
      
      private MediaCreationManagement GetMediaCreationObject ( )
      {
         MediaCreationManagement mediaObject ;
         
         
         mediaObject  = new MediaCreationManagement ( ) ;
         
         mediaObject.IncludeDisplayApplication         = ( __View.IncludeViewer ) ?  YesNo.Yes : YesNo.No ;
         mediaObject.MediaSet.FileSetID                = __View.MediaTitle ;
         mediaObject.ReferencedSopSequence             = GetMediaReferencedSops ( MediaType.DVD ) ;
         mediaObject.RequestInformation.NumberOfCopies = 1 ;
         mediaObject.SopCommon.SOPInstanceUID          = Leadtools.DicomDemos.Utils.GenerateDicomUniqueIdentifier ( ) ;
         
         return mediaObject ;
      }
      
      void view_PrepareMedia ( object sender, EventArgs e )
      {
         MediaCreationManagement mediaObject;
         GeneralPurposeCDDVDProfileProcessor processor;

         mediaObject = GetMediaCreationObject();

         MediaComposer mediaComposer = new MediaComposer(mediaObject, ViewerContainer.State.ActiveWorkstation.StorageDataAccess);

         processor = new GeneralPurposeCDDVDProfileProcessor();

         processor.SetProfile(GetProfile(MediaType.DVD));

         processor.ViewerDirectory = (__View.IncludeViewer) ? __View.ViewerDirectory : string.Empty;

         if (__View.IncludeViewer && __View.CreateAutoRun)
         {
            processor.NonDicomFiles = new string[] { CreateAutoRunFile() };
         }

         mediaComposer.ProfileProcessors.Add(processor);

         //__View.MediaDirectory = mediaComposer.ComposeMedia ( __View.MediaBaseFolder ) ;
         {
            var taskCreateMedia = new LTTask<string>();
            taskCreateMedia.SetTask(() => { return mediaComposer.ComposeMedia(__View.MediaBaseFolder); });
            TaskDialogFactory.Create(taskCreateMedia, "Preparing media...").ShowDialog();
            if (taskCreateMedia.IsCompletedWithErrors())
            {
               throw taskCreateMedia.GetError();
            }
            __View.MediaDirectory = taskCreateMedia.GetResult();
         }
                  
         __View.TotalSize = GetFormattedSize(GetFolderSize(__View.MediaDirectory));

         __MediaObject = mediaObject;

         __MediaObject.SetCreationPath(__View.MediaDirectory);

         UpdateUI();

         SavePreferences();

         __View.SetMediaCreationWarning("");

         view_BurnMedia(this, e);
      }

      private string CreateAutoRunFile ( )
      {
         string   autoRunFile ;
         string   binariesFolder ;
         string[] mediatWorkstations ;
         string   viewerName ;
         string   viewerPath ;
         string   autoRunContents ;
         
         autoRunFile        = Path.Combine ( __View.MediaBaseFolder, "autorun.inf" ) ;
         binariesFolder     = __View.ViewerDirectory ;
         mediatWorkstations = Directory.GetFiles ( binariesFolder, "*workstationdicomdir*.exe", SearchOption.TopDirectoryOnly ) ;
         
         if ( mediatWorkstations.Length == 0 )
         {
            throw new InvalidOperationException ( "Can't create AutoRun file. No Workstation Media Viewer found." ) ;
         }
         
         if ( !File.Exists ( autoRunFile ) )
         {
            File.Delete ( autoRunFile ) ;
         }
         
         
         viewerName = new FileInfo ( mediatWorkstations [ 0 ] ).Name ;
         
         viewerPath      = Path.Combine ( new DirectoryInfo ( __View.ViewerDirectory ).Name, viewerName ) ;
         autoRunContents = @"[autorun]
open={0}
icon={0}" ;
      
         autoRunContents = string.Format ( autoRunContents, viewerPath ) ;
            
         File.WriteAllText ( autoRunFile, autoRunContents ) ;
         
         return autoRunFile ;
      }
      
      void view_BurnMedia ( object sender, EventArgs e )
      {
         if ( null != __MediaObject ) 
         {
            BurnMedia burnMediaForm = new BurnMedia ( ) ;
            MediaObjectsStateService state = new MediaObjectsStateService ( new MediaCreationQueue ( ) ) ;
            
            __MediaObject.ExecutionStatus.ExecutionStatus = ExecutionStatus.Creating ;
            
            state.MediaQueue.Add ( __MediaObject ) ;
            state.ActiveMediaItem = __MediaObject ;
            
            BurnMediaPresenter burnPresenter = new BurnMediaPresenter ( burnMediaForm.BurnMediaControl,
                                                                       state,
                                                                       System.Windows.Forms.WindowsFormsSynchronizationContext.Current  ) ;

            burnMediaForm.Tag = burnPresenter ;
            
            burnMediaForm.FormClosing += new FormClosingEventHandler ( burnMediaForm_FormClosing ) ;
            
            burnMediaForm.StartPosition = FormStartPosition.CenterParent ;

            if ( DialogResult.OK == burnMediaForm.ShowDialog ( ViewerContainer.State.ActiveWorkstation ) )
            {
               //currently always cleared after a successful burn
               if (__View.ClearInstancesAfterRequest)
               {
                  try
                  {
                     if (burnPresenter.Status == BurnStatus.BurnCompleted)
                     {
                        OnClearInstances();
                     }                     
                  }
                  catch { }
               }
            }
         }
      }
      
      
      void burnMediaForm_FormClosing(object sender, FormClosingEventArgs e)
      {
         BurnMediaPresenter burnPresenter ;
         
         
         burnPresenter  = ( sender as Form ).Tag as BurnMediaPresenter ;
         
         if ( null != burnPresenter ) 
         {
            if ( burnPresenter.IsProcessing ) 
            {
               Messager.ShowWarning ( sender as Form, 
                                      "A media operation is currently in progress. Wait for the operation to finish or cancel." ) ;
            
               e.Cancel = true ;
            }
            else
            {
               burnPresenter.TearDown ( ) ;
            }
         }
      }
      
      void OnUpdateUI (object sender, EventArgs e)
      {
         UpdateUI ( ) ;
      }
      
      private ILocalMediaInformationView __View
      {
         get ;
         set ;
      }
      
      private MediaCreationManagement __MediaObject { get ; set ; }
   }
}
