// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Configuration;
using System.Net;
using System.Linq;
using Leadtools.Dicom.AddIn.Common;
using Leadtools.DicomDemos;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Collections.Specialized;
using System.Globalization;
using Leadtools.Annotations;
using Leadtools.Annotations.Engine;
using Leadtools.Medical.Winforms.SecurityOptions;

namespace Leadtools.Demos.Workstation.Configuration
{
   public static class ConfigurationData
   {
      [DllImport("shell32.dll")]
      private static extern int SHGetFolderPath(IntPtr hwndOwner, int nFolder, IntPtr hToken, uint dwFlags, [System.Runtime.InteropServices.Out] System.Text.StringBuilder pszPath);

      private static string GetCommonDocumentsFolder()
      {
         int SIDL_COMMON_DOCUMENTS = 0x002E;

         System.Text.StringBuilder sb = new System.Text.StringBuilder(1024);
         SHGetFolderPath(IntPtr.Zero, SIDL_COMMON_DOCUMENTS, IntPtr.Zero, 0, sb);
         return sb.ToString();
      }

      public static string ConfigFile = string.Empty;

      #region Public

      #region Methods

      public static bool HasChanges()
      {
         return _isDirty;
      }

      public static void SaveChanges()
      {
         Save();
      }

      #endregion

      #region Properties

      public static string ApplicationName
      {
         get
         {
            return _appName;
         }

         set
         {
            _appName = value;
         }
      }

      public static string HelpFile
      {
         get;
         set;
      }

      public static string DatabaseConfigName
      {
         get;

         set;
      }

      public static string DatabaseConfigEXEName
      {
         get;

         set;
      }

      public static string DatabaseConfigAltEXEName
      {
         get;

         set;
      }

      public static bool MoveToWSClient
      {
         get
         {
            return _moveToClient;
         }

         set
         {
            if (value != _moveToClient)
            {
               _moveToClient = value;

               OnValueChanged(null, new EventArgs());
            }
         }
      }

      public static bool SetClientToAllWorkstations
      {
         get
         {
            return _setClientToAllWS;
         }

         set
         {
            if (value != _setClientToAllWS)
            {
               _setClientToAllWS = value;

               OnValueChanged(null, EventArgs.Empty);
            }
         }
      }

      public static bool RunPacsConfig
      {
         get;
         set;
      }

      public static IList<ScpInfo> PACS
      {
         get
         {
            return _pacs;
         }

         private set
         {
            _pacs = value;
         }
      }

      public static DebuggingConfig Debugging
      {
         get
         {
            return _debuggingConfig;
         }

         private set
         {
            _debuggingConfig = value;
         }
      }

      public static StorageCompression Compression
      {
         get
         {
            return _compression;
         }

         private set
         {
            _compression = value;
         }
      }

      public static ScuInfo WorkstationClient
      {
         get
         {
            return _workstationClient;
         }

         private set
         {
            _workstationClient = value;
         }
      }

      public static ScpInfo ActivePacs
      {
         get
         {
            if (__ActivePacsIndex < 0 || __ActivePacsIndex >= PACS.Count)
            {
               return null;
            }

            return PACS[__ActivePacsIndex];
         }

         set
         {
            if (value != null && !PACS.Contains(value))
            {
               throw new InvalidOperationException("Server " + value + " doens't exist in configured PACS list");
            }

            int newIndex;


            newIndex = PACS.IndexOf(value);

            if (newIndex != __ActivePacsIndex)
            {
               __ActivePacsIndex = newIndex;

               OnValueChanged(null, new EventArgs());
            }
         }
      }

      public static ScpInfo DefaultStorageServer
      {
         get
         {
            return _defaultStorageServer;
         }

         set
         {
            if (_defaultStorageServer != value)
            {
               _defaultStorageServer = value;

               OnValueChanged(null, new EventArgs());
            }
         }
      }

      public static ScpInfo DefaultQueryRetrieveServer
      {
         get
         {
            return _defaultQueryRetrieveServer;
         }

         set
         {
            if (_defaultQueryRetrieveServer != value)
            {
               _defaultQueryRetrieveServer = value;

               OnValueChanged(null, new EventArgs());
            }
         }
      }

      public static DicomClientMode ClientBrowsingMode
      {
         get
         {
            return _clientMode;
         }

         set
         {
            if (value != _clientMode)
            {
               _clientMode = value;

               OnValueChanged(null, new EventArgs());
            }
         }
      }

      public static LazyLoading ViewerLazyLoading
      {
         get
         {
            return _viewerLazyLoading;
         }

         private set
         {
            _viewerLazyLoading = value;
         }
      }

      public static bool RunFullScreen
      {
         get
         {
            return _runFullScreen;
         }

         set
         {
            if (value != _runFullScreen)
            {
               _runFullScreen = value;

               OnValueChanged(null, new EventArgs());
            }
         }
      }

      public static bool QueueAutoLoad
      {
         get
         {
            return _queueAutoLoad;
         }

         set
         {
            if (_queueAutoLoad != value)
            {
               _queueAutoLoad = value;

               OnValueChanged(null, new EventArgs());
            }
         }
      }

      public static bool QueueRemoveItem
      {
         get
         {
            return _queueRemoveItem;
         }

         set
         {
            if (_queueRemoveItem != value)
            {
               _queueRemoveItem = value;

               OnValueChanged(null, new EventArgs());
            }
         }
      }

      public static bool ContinueRetrieveOnDuplicateInstance
      {
         get
         {
            return _storeRetrievedImages;
         }

         set
         {
            if (value != _storeRetrievedImages)
            {
               _storeRetrievedImages = value;

               OnValueChanged(null, new EventArgs());
            }
         }
      }

      //TODO: this should be not wirtten in config, the user should call this from the demo.
      public static string MedicalNetKey
      {
         get
         {
            return _medicalNetKey;
         }

         set
         {
            _medicalNetKey = value;
         }
      }

      //TODO: this should be not wirtten in config, the user should call this from the demo.
      public static string PacsFrmKey
      {
         get
         {
            return _pacsFrmKey;
         }

         set
         {
            _pacsFrmKey = value;
         }
      }

      public static bool AutoCreateService
      {
         get
         {
            return _autoCreateService;
         }

         set
         {
            if (value != _autoCreateService)
            {
               _autoCreateService = value;

               OnValueChanged(null, new EventArgs());
            }
         }
      }

      public static string WorkstationServiceAE
      {
         get
         {
            return _workstationServiceAE;
         }

         set
         {
            if (value != _workstationServiceAE)
            {
               _workstationServiceAE = value;

               OnValueChanged(null, new EventArgs());
            }
         }
      }

      public static string ListenerServiceName
      {
         get
         {
            return _listenerServiceName;
         }

         set
         {
            if (value != _listenerServiceName)
            {
               _listenerServiceName = value;

               OnValueChanged(null, new EventArgs());
            }
         }
      }

      public static string ListenerServiceDefaultName
      {
         get
         {
            return _listenerServiceDefaultName;
         }

         set
         {
            if (value != _listenerServiceDefaultName)
            {
               _listenerServiceDefaultName = value;

               OnValueChanged(null, new EventArgs());
            }
         }
      }

      public static string ListenerServiceDefaultDisplayName
      {
         get
         {
            return _listenerServiceDisplayName;
         }

         set
         {
            if (value != _listenerServiceDisplayName)
            {
               _listenerServiceDisplayName = value;

               OnValueChanged(null, new EventArgs());
            }
         }
      }

      public static int ViewerOverlayTextSize
      {
         get
         {
            return _viewerOverlayTextSize;
         }

         set
         {
            if (value != _viewerOverlayTextSize)
            {
               _viewerOverlayTextSize = value;

               OnValueChanged(null, new EventArgs());
            }
         }
      }

      public static bool ViewerAutoSizeOverlayText
      {
         get
         {
            return _viewerAutoSizeOverlayText;
         }

         set
         {
            if (value != _viewerAutoSizeOverlayText)
            {
               _viewerAutoSizeOverlayText = value;

               OnValueChanged(null, new EventArgs());
            }
         }
      }

      public static SaveOptions SaveSessionBehavior
      {
         get
         {
            return _saveConfigBehavior;
         }

         set
         {
            if (value != _saveConfigBehavior)
            {
               _saveConfigBehavior = value;

               OnValueChanged(null, EventArgs.Empty);
            }
         }
      }

      public static bool SupportDicomCommunication
      {
         get;
         set;
      }

      public static bool SupportLocalQueriesStore
      {
         get;
         set;
      }

      public static string CurrentDicomDir
      {
         get;
         set;
      }

      public static bool CheckDataAccessServices
      {
         get;
         set;
      }

      public static bool ShowSplashScreen
      {
         get;
         set;
      }

      public static bool AutoQuery
      {
         get;
         set;
      }

      public static Color AnnotationDefaultColor
      {
         get
         {
            return _annotationDefaultColor;
         }

         set
         {
            if (value != _annotationDefaultColor)
            {
               _annotationDefaultColor = value;

               OnValueChanged(null, new EventArgs());
            }
         }
      }

      public static AnnUnit MeasurementUnit
      {
         get
         {
            return _measurementUnit;
         }

         set
         {
            if (value != _measurementUnit)
            {
               _measurementUnit = value;

               OnValueChanged(null, new EventArgs());
            }
         }
      }

      public static bool ShowStudyTimeline
      {
         get
         {
            return _showStudyTimeline;
         }

         set
         {
            if (value != _showStudyTimeline)
            {
               _showStudyTimeline = value;

               OnValueChanged(null, new EventArgs());
            }
         }
      }

      private static DicomSecurityOptions _dicomSecurityOptions = null;
      public static DicomSecurityOptions SecurityOptions
      {
         get
         {
            if (_dicomSecurityOptions == null)
            {
               _dicomSecurityOptions = new DicomSecurityOptions();
            }
            return _dicomSecurityOptions;
         }
         set
         {
            if (!DicomSecurityOptions.IsEqual(value, _dicomSecurityOptions))
            {
               OnValueChanged(null, new EventArgs());
            }
            _dicomSecurityOptions = value;
         }
      }

      #endregion

      #region Events

      public static event EventHandler ValueChanged;
      public static event EmptyHandler ChangesSaved;

      #endregion

      #region Data Types Definition

      public delegate void EmptyHandler();

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

      static ConfigurationData()
      {
         try
         {
            MyAppSettingsReader configReader;


#if LTV20_CONFIG
            ConfigFile = Path.Combine(GetCommonDocumentsFolder(), "ViewerCommon_20.config");
#elif LTV19_CONFIG
                  ConfigFile = Path.Combine(GetCommonDocumentsFolder(),"ViewerCommon_19.config");
#elif LTV18_CONFIG
                  ConfigFile = Path.Combine(GetCommonDocumentsFolder(),"ViewerCommon_18.config");
#else
                  ConfigFile = Path.Combine(GetCommonDocumentsFolder(),"ViewerCommon.config");
#endif
            SupportDicomCommunication = true;
            SupportLocalQueriesStore = true;
            CheckDataAccessServices = true;
            ShowSplashScreen = true;
            AutoQuery = false;

            WorkstationClient = new ScuInfo();
            PACS = new ScpInfoBindingList();
            Debugging = new DebuggingConfig();
            Compression = new StorageCompression();
            ViewerLazyLoading = new LazyLoading();
            configReader = new MyAppSettingsReader();

            DefaultQueryRetrieveServer = null;
            DefaultStorageServer = null;

            ReadKeys(configReader);
            ReadConfiguredPacs(configReader);
            ReadDebugConfig(configReader);
            ReadCompressionConfig(configReader);
            ReadWorkstationClientConfig(configReader);
            ReadGeneralConfigData(configReader);
            ReadLazyLoadingConfigData(configReader);
            ReadAutoCreateServiceConfigData(configReader);
            ReadOverlayTextSizeConfigData(configReader);

            ReadSecuritySettings_ConfigData(configReader);

            ReadViewerCommonSettings();

            if (ActivePacs == null && PACS.Count > 0)
            {
               ActivePacs = PACS[0];
            }

            _isDirty = false;

            RegisterEvents();
         }
         catch (Exception exception)
         {
            System.Diagnostics.Debug.Assert(false, exception.Message);

            throw;
         }
      }

      private static void ReadViewerCommonSettings()
      {
         AnnotationDefaultColor = Color.Yellow;
         MeasurementUnit = AnnUnit.Centimeter;

         ExeConfigurationFileMap exeCommonConfig = new ExeConfigurationFileMap();
         //exeCommonConfig.ExeConfigFilename = Path.Combine ( Application.StartupPath, "ViewerCommon.config" ) ;

         exeCommonConfig.ExeConfigFilename = ConfigFile;
         if (!File.Exists(exeCommonConfig.ExeConfigFilename))
         {
            return;
         }
         System.Configuration.Configuration config = ConfigurationManager.OpenMappedExeConfiguration(exeCommonConfig, ConfigurationUserLevel.None);

         try
         {
            if (config.AppSettings.Settings.AllKeys.Contains(Constants.AnnotationDefaultColor))
            {
               AnnotationDefaultColor = Color.FromArgb(int.Parse(config.AppSettings.Settings[Constants.AnnotationDefaultColor].Value));
            }
         }
         catch { }

         try
         {
            if (config.AppSettings.Settings.AllKeys.Contains(Constants.MeasurementUnit))
            {
               MeasurementUnit = (AnnUnit)Enum.Parse(typeof(AnnUnit), config.AppSettings.Settings[Constants.MeasurementUnit].Value);
            }
         }
         catch { }
      }

      private static void RegisterEvents()
      {
         (PACS as BindingList<ScpInfo>).ListChanged += new ListChangedEventHandler(PacsConfiguration_ListChanged);

         (PACS as ScpInfoBindingList).ScpValueChanged += new EventHandler(OnValueChanged);
         Debugging.ValueChanged += new EventHandler(OnValueChanged);
         Compression.ValueChanged += new EventHandler(OnValueChanged);
         ViewerLazyLoading.ValueChanged += new EventHandler(OnValueChanged);
         WorkstationClient.ValueChanged += new EventHandler(OnValueChanged);
      }

      private static void ReadKeys(MyAppSettingsReader configReader)
      {
         try
         {
            MedicalNetKey = (string)configReader.GetValue(Constants.MedicalNetKey, typeof(string));
         }
         catch
         {
            MedicalNetKey = string.Empty;
         }

         try
         {
            PacsFrmKey = (string)configReader.GetValue(Constants.PacsFrmKey, typeof(string));
         }
         catch
         {
            PacsFrmKey = string.Empty;
         }
      }

      private static void ReadWorkstationClientConfig(MyAppSettingsReader configReader)
      {
         try
         {
            // WorkstationAETitle
            try
            {
               WorkstationClient.AETitle = (string)configReader.GetValue(Constants.WorkstationAETitle, typeof(string));
            }
            catch
            {
               WorkstationClient.AETitle = "LTSTATION_CLIENT";
            }

            // WorkstationAddress
            try
            {
               WorkstationClient.Address = ValidateAndGetValidHostAddress((string)configReader.GetValue(Constants.WorkstationAddress, typeof(string)));
            }
            catch
            {
               WorkstationClient.Address = Dns.GetHostName();
            }

            // WorkstationPort
            try
            {
               WorkstationClient.Port = (int)configReader.GetValue(Constants.WorkstationPort, typeof(int));
            }
            catch
            {
               WorkstationClient.Port = 1000;
            }

            // Secure
            try
            {
               WorkstationClient.Secure = (bool)configReader.GetValue(Constants.WorkstationSecure, typeof(bool));
            }
            catch
            {
               WorkstationClient.Secure = false;
            }
         }
         catch (Exception exception)
         {
            System.Diagnostics.Debug.Assert(false, exception.Message);
         }
      }

      private static void ReadConfiguredPacs(MyAppSettingsReader configReader)
      {
         try
         {
            string scpServers;

            try
            {
               scpServers = (string)configReader.GetValue(Constants.PacsServers, typeof(string));
            }
            catch
            {
               scpServers = "";
            }

            if (!string.IsNullOrEmpty(scpServers))
            {
               string[] serversArray;


               serversArray = scpServers.Split(';');

               foreach (string server in serversArray)
               {
                  string[] serverInfoArray;


                  serverInfoArray = server.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                  if (serverInfoArray.Length != 6 && serverInfoArray.Length != 7)
                  {
                     continue;
                  }

                  string aeTitle = serverInfoArray[0];
                  string address = serverInfoArray[1];
                  int port = int.Parse(serverInfoArray[2]);
                  int timeout = int.Parse(serverInfoArray[3]);
                  bool secure = false;

                  if (serverInfoArray.Length >= 7)
                  {
                     secure = (int.Parse(serverInfoArray[6]) == 1);
                  }

                  PACS.Add(new ScpInfo(aeTitle,
                                           address,
                                           port,
                                           timeout,
                                           secure));

                  if (int.Parse(serverInfoArray[4]) == 1)
                  {
                     DefaultQueryRetrieveServer = PACS[PACS.Count - 1];
                  }

                  if (int.Parse(serverInfoArray[5]) == 1)
                  {
                     DefaultStorageServer = PACS[PACS.Count - 1];
                  }
               }
            }

            try
            {
               if (ConfigurationManager.AppSettings.AllKeys.Contains(Constants.ActivePacs))
               {
                  __ActivePacsIndex = (int)configReader.GetValue(Constants.ActivePacs, typeof(int));
               }
            }
            catch
            {
               __ActivePacsIndex = -1;
            }
         }
         catch (Exception exception)
         {
            System.Diagnostics.Debug.Assert(false, exception.Message);

            throw;
         }
      }

      private static void ReadDebugConfig(MyAppSettingsReader configReader)
      {
         try
         {
            string debugInfo;

            try
            {
               debugInfo = (string)configReader.GetValue(Constants.DebugInfo,
                                                            typeof(string));

               debugInfo = debugInfo.Trim();
            }
            catch
            {
               debugInfo = "";
            }

            if (!string.IsNullOrEmpty(debugInfo))
            {
               string[] debugInfoArray;


               debugInfoArray = debugInfo.Split(';');

               if (debugInfoArray.Length == 4)
               {
                  Debugging.Enable = bool.Parse(debugInfoArray[0]);
                  Debugging.DisplayDetailedErrors = bool.Parse(debugInfoArray[1]);
                  Debugging.GenerateLogFile = bool.Parse(debugInfoArray[2]);
                  Debugging.LogFileName = debugInfoArray[3];
               }
            }
         }
         catch (Exception exception)
         {
            System.Diagnostics.Debug.Assert(false, exception.Message);

            Debugging.Enable = false;
         }
      }

      private static void ReadCompressionConfig(MyAppSettingsReader configReader)
      {
         try
         {
            string compressionInfo;

            try
            {
               compressionInfo = (string)configReader.GetValue(Constants.CompressionInfo,
                                                            typeof(string));

               compressionInfo = compressionInfo.Trim(';');
               compressionInfo = compressionInfo.Trim();
            }
            catch
            {
               compressionInfo = "";
            }

            if (!string.IsNullOrEmpty(compressionInfo))
            {
               string[] compressionInfoArray;


               compressionInfoArray = compressionInfo.Split(';');

               if (compressionInfoArray.Length == 2)
               {
                  Compression.Enable = bool.Parse(compressionInfoArray[0]);
                  Compression.Lossy = bool.Parse(compressionInfoArray[1]);
               }
            }
         }
         catch (Exception exception)
         {
            System.Diagnostics.Debug.Assert(false, exception.Message);

            Compression.Enable = false;
         }
      }

      private static void ReadGeneralConfigData(MyAppSettingsReader configReader)
      {
         try
         {
            try
            {
               ApplicationName = configReader.GetValue(Constants.ApplicationName, typeof(string)) as string;

               if (string.IsNullOrEmpty(ApplicationName))
               {
                  ApplicationName = DefaultValues.ApplicationName;
               }
            }
            catch
            {
               ApplicationName = ConfigurationManager.AppSettings["ApplicationName"];

               if (string.IsNullOrEmpty(ApplicationName))
                  ApplicationName = DefaultValues.ApplicationName;
            }

            try
            {
               HelpFile = configReader.GetValue(Constants.HelpFile, typeof(string)) as string;
            }
            catch
            {
               HelpFile = ConfigurationManager.AppSettings["HelpFile"];

               if (string.IsNullOrEmpty(HelpFile))
                  HelpFile = null;
            }

            try
            {
               DatabaseConfigName = configReader.GetValue(Constants.DatabaseConfigName, typeof(string)) as string;

               if (string.IsNullOrEmpty(DatabaseConfigName))
               {
                  DatabaseConfigName = DefaultValues.DatabaseConfigName;
               }
            }
            catch
            {
               DatabaseConfigName = ConfigurationManager.AppSettings["DatabaseConfigName"];

               if (string.IsNullOrEmpty(DatabaseConfigName))
                  DatabaseConfigName = DefaultValues.DatabaseConfigName;
            }

            try
            {
               DatabaseConfigEXEName = configReader.GetValue(Constants.DatabaseConfigEXEName, typeof(string)) as string;

               if (string.IsNullOrEmpty(DatabaseConfigEXEName))
               {
                  DatabaseConfigEXEName = DefaultValues.DatabaseConfigEXEName;
               }
            }
            catch
            {
               DatabaseConfigEXEName = ConfigurationManager.AppSettings["DatabaseConfigEXENam"];

               if (string.IsNullOrEmpty(DatabaseConfigEXEName))
                  DatabaseConfigEXEName = DefaultValues.DatabaseConfigEXEName;
            }

            try
            {
               DatabaseConfigAltEXEName = configReader.GetValue(Constants.DatabaseConfigAltEXEName, typeof(string)) as string;
               if (string.IsNullOrEmpty(DatabaseConfigAltEXEName))
               {
                  DatabaseConfigAltEXEName = DefaultValues.DatabaseConfigAltEXEName;
               }

            }
            catch
            {
               DatabaseConfigAltEXEName = ConfigurationManager.AppSettings["DatabaseConfigAltEXENam"];

               if (string.IsNullOrEmpty(DatabaseConfigAltEXEName))
                  DatabaseConfigAltEXEName = DefaultValues.DatabaseConfigAltEXEName;
            }

            try
            {
               RunPacsConfig = bool.Parse((string)configReader.GetValue(Constants.RunPacsConfig, typeof(string)));

            }
            catch
            {
               RunPacsConfig = false;
            }

            try
            {
               MoveToWSClient = bool.Parse((string)configReader.GetValue(Constants.MoveToWSClient, typeof(string)));
            }
            catch
            {
               MoveToWSClient = false;
            }

            try
            {
               SetClientToAllWorkstations = bool.Parse((string)configReader.GetValue(Constants.SetClientToAllWS, typeof(string)));
            }
            catch
            {
               SetClientToAllWorkstations = false;
            }

            try
            {
               ClientBrowsingMode = (DicomClientMode)Enum.Parse(typeof(DicomClientMode),
                                                                     (string)configReader.GetValue(Constants.ClientBrowsingMode,
                                                                                                       typeof(string)));
            }
            catch
            {
               ClientBrowsingMode = DicomClientMode.LocalDb;
            }

            try
            {
               RunFullScreen = bool.Parse((string)configReader.GetValue(Constants.RunFullScreen, typeof(string)));
            }
            catch
            {
               RunFullScreen = false;
            }

            try
            {
               QueueAutoLoad = bool.Parse((string)configReader.GetValue(Constants.QueueAutoLoad, typeof(string)));
            }
            catch
            {
               QueueAutoLoad = false;
            }

            try
            {
               QueueRemoveItem = bool.Parse((string)configReader.GetValue(Constants.QueueRemoveItem, typeof(string)));
            }
            catch
            {
               QueueRemoveItem = false;
            }

            try
            {
               ContinueRetrieveOnDuplicateInstance = bool.Parse((string)configReader.GetValue(Constants.ContinueRetrieveOnDuplicateInstance, typeof(string)));
            }
            catch
            {
               ContinueRetrieveOnDuplicateInstance = false;
            }

            try
            {
               if (ConfigurationManager.AppSettings.AllKeys.Contains(Constants.SaveSessionBehavior))
               {
                  SaveSessionBehavior = (SaveOptions)Enum.Parse(typeof(SaveOptions), configReader.GetValue(Constants.SaveSessionBehavior, typeof(string)) as string);
               }
               else
               {
                  SaveSessionBehavior = SaveOptions.PromptUser;
               }
            }
            catch
            {
               SaveSessionBehavior = SaveOptions.PromptUser;
            }

            try
            {
               ShowStudyTimeline = bool.Parse((string)configReader.GetValue(Constants.ShowStudyTimeline, typeof(string)));
            }
            catch
            {
               ShowStudyTimeline = false;
            }

         }
         catch (Exception exception)
         {
            System.Diagnostics.Debug.Assert(false, exception.Message);

            throw;
         }
      }

      private static void ReadLazyLoadingConfigData(MyAppSettingsReader configReader)
      {
         try
         {
            ViewerLazyLoading.Enable = (bool)bool.Parse((string)configReader.GetValue(Constants.EnableLazyLoading,
                                                                                               typeof(string)));
         }
         catch
         {
            ViewerLazyLoading.Enable = true;
         }

         try
         {
            ViewerLazyLoading.HiddenImages = int.Parse((string)configReader.GetValue(Constants.LazyLoadingHiddenImages,
                                                                                           typeof(string)));
         }
         catch
         {
            ViewerLazyLoading.HiddenImages = 2;
         }

      }

      private static void ReadAutoCreateServiceConfigData(MyAppSettingsReader configReader)
      {
         try
         {
            AutoCreateService = (bool)bool.Parse((string)configReader.GetValue(Constants.AutoCreateService,
                                                                                         typeof(string)));
         }
         catch
         {
            if (ConfigurationManager.AppSettings["AutoCreateListenerService"] != null)
            {
               bool value = true;

               bool.TryParse(ConfigurationManager.AppSettings["AutoCreateListenerService"], out value);
               AutoCreateService = value;
            }
            else
               AutoCreateService = true;
         }

         try
         {
            ListenerServiceName = (string)configReader.GetValue(Constants.WorkstationServiceName,
                                                                      typeof(string));
         }
         catch
         {
            ListenerServiceName = "";
         }

         try
         {
            WorkstationServiceAE = (string)configReader.GetValue(Constants.WorkstationServiceAE,
                                                                      typeof(string));
         }
         catch
         {
            WorkstationServiceAE = ConfigurationManager.AppSettings["WsAETitle"];

            if (string.IsNullOrEmpty(WorkstationServiceAE))
               WorkstationServiceAE = string.Empty;
         }

         try
         {
            ListenerServiceDefaultName = (string)configReader.GetValue(Constants.DefualtServiceName,
                                                                            typeof(string));
         }
         catch
         {
            ListenerServiceDefaultName = ConfigurationManager.AppSettings["DefaultListenerServiceName"];

            if (string.IsNullOrEmpty(ListenerServiceDefaultName))
               ListenerServiceDefaultName = "LTSTATION_SERVER";
         }

         try
         {
            ListenerServiceDefaultDisplayName = (string)configReader.GetValue(Constants.DefualtServiceDisplay,
                                                                            typeof(string));
         }
         catch
         {
            ListenerServiceDefaultDisplayName = ConfigurationManager.AppSettings["DefaultListenerServiceDisplayName"];

            if (string.IsNullOrEmpty(ListenerServiceDefaultDisplayName))
               ListenerServiceDefaultDisplayName = "LEADTOOLS Workstation Listener Service";
         }
      }


      private static void ReadOverlayTextSizeConfigData(MyAppSettingsReader configReader)
      {
         try
         {
            ViewerOverlayTextSize = int.Parse((string)configReader.GetValue(Constants.ViewerOverlayTextSize,
                                                                                   typeof(string)));
         }
         catch
         {
            ViewerOverlayTextSize = 14;
         }

         try
         {
            ViewerAutoSizeOverlayText = bool.Parse((string)configReader.GetValue(Constants.ViewerAutoSizeOverlayText,
                                                                                        typeof(string)));
         }
         catch
         {
            ViewerAutoSizeOverlayText = true;
         }
      }

      private static void AddConfigValue
      (
         System.Configuration.Configuration config,
         List<string> keys,
         string key,
         string value
      )
      {
         try
         {
            if (keys.Contains(key) && config.AppSettings.Settings.AllKeys.Contains(key))
            {
               config.AppSettings.Settings[key].Value = value;
            }
            else
            {
               config.AppSettings.Settings.Add(key, value); ;
            }
         }
         catch (Exception exception)
         {
            System.Diagnostics.Debug.Assert(false, exception.Message);

            throw;
         }
      }

      private static void OnValueChanged(object sender, EventArgs e)
      {
         _isDirty = true;

         if (null != ValueChanged)
         {
            ValueChanged(null, new EventArgs());
         }
      }

      private static string ValidateAndGetValidHostAddress(string testAddress)
      {
         string validAddress;


         validAddress = Dns.GetHostName();

         if (validAddress.ToLower() == testAddress.ToLower())
         {
            return testAddress;
         }
         else
         {
            IPAddress[] localIpAddress = Dns.GetHostAddresses(validAddress);

            foreach (IPAddress address in localIpAddress)
            {
               if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
               {
                  if (testAddress == address.ToString())
                  {
                     return testAddress;
                  }
               }
            }
         }

         return validAddress;
      }

      #endregion

      #region Properties

      private static int __ActivePacsIndex
      {
         get;
         set;
      }

      #endregion

      #region Events

      private static void PacsConfiguration_ListChanged
      (
         object sender,
         ListChangedEventArgs e
      )
      {
         try
         {
            if (null != DefaultStorageServer && !PACS.Contains(DefaultStorageServer))
            {
               DefaultStorageServer = null;
            }

            if (null != DefaultQueryRetrieveServer && !PACS.Contains(DefaultQueryRetrieveServer))
            {
               DefaultQueryRetrieveServer = null;
            }

            OnValueChanged(null, new EventArgs());
         }
         catch (Exception exception)
         {
            System.Diagnostics.Debug.Assert(false, exception.Message);

            throw;
         }
      }

      #endregion

      #region Data Members

      private static IList<ScpInfo> _pacs;
      private static ScuInfo _workstationClient;
      private static ScpInfo _defaultStorageServer;
      private static ScpInfo _defaultQueryRetrieveServer;
      private static DicomClientMode _clientMode;
      private static DebuggingConfig _debuggingConfig;
      private static StorageCompression _compression;
      private static LazyLoading _viewerLazyLoading;
      private static bool _runFullScreen;
      private static bool _queueAutoLoad;
      private static bool _queueRemoveItem;
      private static bool _storeRetrievedImages;
      private static bool _autoCreateService;
      private static bool _moveToClient;
      private static bool _setClientToAllWS;
      private static bool _isDirty = false;
      private static string _medicalNetKey;
      private static string _pacsFrmKey;
      private static string _workstationServiceAE;
      private static string _listenerServiceName;
      private static string _listenerServiceDefaultName;
      private static string _listenerServiceDisplayName;
      private static string _appName;
      private static bool _viewerAutoSizeOverlayText;
      private static int _viewerOverlayTextSize;
      private static SaveOptions _saveConfigBehavior;
      private static Color _annotationDefaultColor;
      private static AnnUnit _measurementUnit;
      private static bool _showStudyTimeline = false;

      #endregion

      #region Data Types Definition

      private abstract class Constants
      {
         public const string MedicalNetKey = "MedicalNetKey";
         public const string PacsFrmKey = "PacsFrmKey";
         public const string ScpServerFormat = "{0},{1},{2},{3},{4},{5},{6};";
         public const string PacsServers = "PacsServers";
         public const string WorkstationAETitle = "WsAETitle";
         public const string WorkstationPort = "WsPort";
         public const string WorkstationSecure = "WsSecure";
         public const string WorkstationAddress = "WsAddress";
         public const string DebugInfo = "DebugInfo";
         public const string CompressionInfo = "CompressionInfo";
         public const string ApplicationName = "ApplicationName";

         public const string DatabaseConfigName = "DatabaseConfigName";
         public const string DatabaseConfigEXEName = "DatabaseConfigEXENam";
         public const string DatabaseConfigAltEXEName = "DatabaseConfigAltEXENam";
         public const string RunPacsConfig = "RunPacsConfig";
         public const string MoveToWSClient = "MoveToWSClient";
         public const string SetClientToAllWS = "SetClientToAllWS";
         public const string ClientBrowsingMode = "ClientBrowsingMode";
         public const string RunFullScreen = "RunFullScreen";
         public const string QueueAutoLoad = "QueueAutoLoad";
         public const string QueueRemoveItem = "QueueRemoveItem";
         public const string EnableLazyLoading = "EnableLazyLoading";
         public const string AutoCreateService = "AutoCreateListenerService";
         public const string WorkstationServiceAE = "WorkstationServiceAE";
         public const string DefualtServiceName = "DefaultListenerServiceName";
         public const string WorkstationServiceName = "WorkstationServiceName";
         public const string DefualtServiceDisplay = "DefaultListenerServiceDisplayName";
         public const string ActivePacs = "ActivePacs";
         public const string LazyLoadingHiddenImages = "LazyLoadingHiddenImages";
         public const string ContinueRetrieveOnDuplicateInstance = "ContinueRetrieveOnDuplicateInstance";

         public const string ViewerOverlayTextSize = "ViewerOverlayTextSize";
         public const string ViewerAutoSizeOverlayText = "ViewerAutoSizeOverlayText";
         public const string HelpFile = "HelpFile";
         public const string SaveSessionBehavior = "SaveSessionBehavior";

         public const string MeasurementUnit = "MeasurementUnit";
         public const string AnnotationDefaultColor = "AnnotationDefaultColor";

         public const string ShowStudyTimeline = "ShowStudyTimeline";
         public const string AllSecurityOptions = "AllSecurityOptions";

         public const string SecurityCertificationAuthoritiesFileName = "SecurityCertificationAuthoritiesFileName";
         public const string SecurityCertificateFileName = "SecurityCertificateFileName";
         public const string SecurityKeyFileName = "SecurityKeyFileName";
         public const string SecurityPassword = "SecurityPassword";
         public const string SecurityCertificateType = "SecurityCertificateType";
         public const string SecurityMaximumVerificationDepth = "SecurityMaximumVerificationDepth";
         public const string SecurityOptions = "SecurityOptions";
         public const string SecurityVerificationFlags = "SecurityVerificationFlags";
         public const string SecuritySslMethodType = "SecuritySslMethodType";
         public const string SecurityShowPassword = "SecurityShowPassword";
         public const string SecurityCipherSuites = "SecurityCipherSuites";
      }

      private abstract class DefaultValues
      {
         public const string ApplicationName = "Medical Workstation Viewer Main Demo";
         public const string DatabaseConfigName = "CSPacsDatabaseConfigurationDemo";
         public const string DatabaseConfigEXEName = "CSPacsDatabaseConfigurationDemo_Original.exe";
         public const string DatabaseConfigAltEXEName = "CSPacsDatabaseConfigurationDemo.exe";
      }

      #endregion

      #endregion

      #region internal

      #region Methods

      internal static T MyParseEnum<T>(string value)
      {
         return (T)Enum.Parse(typeof(T), value, true);
      }

      internal static DicomSecurityOptions ReadSecuritySettingsFromConfigData(MyAppSettingsReader configReader)
      {
         DicomSecurityOptions securityOptions = new DicomSecurityOptions();

         // CertificationAuthoritiesFileName
         try
         {
            securityOptions.CertificationAuthoritiesFileName = (string)configReader.GetValue(Constants.SecurityCertificationAuthoritiesFileName, typeof(string));
         }
         catch
         {
         }

         // CertificateFileName
         try
         {
            securityOptions.CertificateFileName = (string)configReader.GetValue(Constants.SecurityCertificateFileName, typeof(string));
         }
         catch
         {
         }

         // KeyFileName
         try
         {
            securityOptions.KeyFileName = (string)configReader.GetValue(Constants.SecurityKeyFileName, typeof(string));
         }
         catch
         {
         }

         // Password
         try
         {
            securityOptions.Password = (string)configReader.GetValue(Constants.SecurityPassword, typeof(string));
         }
         catch
         {
         }

         // CertificateType
         try
         {
            string sValue = (string)configReader.GetValue(Constants.SecurityCertificateType, typeof(string));
            securityOptions.CertificateType = MyParseEnum<Dicom.DicomTlsCertificateType>(sValue);
         }
         catch
         {
         }

         // MaximumVerificationDepth
         try
         {
            securityOptions.MaximumVerificationDepth = int.Parse((string)configReader.GetValue(Constants.SecurityMaximumVerificationDepth, typeof(string)));
         }
         catch
         {
         }

         // Options (flags)
         try
         {
            string sValue = (string)configReader.GetValue(Constants.SecurityOptions, typeof(string));
            securityOptions.Options = MyParseEnum<Dicom.DicomOpenSslOptionsFlags>(sValue);
         }
         catch
         {
         }

         // VerificationFlags (flags)
         try
         {
            string sValue = (string)configReader.GetValue(Constants.SecurityVerificationFlags, typeof(string));
            securityOptions.VerificationFlags = MyParseEnum<Dicom.DicomOpenSslVerificationFlags>(sValue);
         }
         catch
         {
         }

         // SslMethodType
         try
         {
            string sValue = (string)configReader.GetValue(Constants.SecuritySslMethodType, typeof(string));
            securityOptions.SslMethodType = MyParseEnum<Dicom.DicomSslMethodType>(sValue);
         }
         catch
         {
         }

         // SecurityShowPassword
         try
         {
            securityOptions.ShowPassword = bool.Parse((string)configReader.GetValue(Constants.SecurityShowPassword, typeof(string)));
         }
         catch
         {
         }

         // CipherSuiteItems
         try
         {
            string stringCipherSuiteItems = (string)configReader.GetValue(Constants.SecurityCipherSuites, typeof(string));
            securityOptions.CipherSuites = CipherSuiteItems.Deserialize(stringCipherSuiteItems);
         }
         catch
         {
         }

         return securityOptions;
      }

      internal static void ReadSecuritySettings_ConfigData(MyAppSettingsReader configReader)
      {
         DicomSecurityOptions options = ReadSecuritySettingsFromConfigData(configReader);
         _dicomSecurityOptions = options;
      }

      internal static void Save()
      {
         try
         {
            System.Configuration.Configuration config;
            System.Configuration.Configuration commonConfig;
            string localPacsServers = string.Empty;
            string localDebugging = string.Empty;
            string localCompression = string.Empty;


            foreach (ScpInfo scp in PACS)
            {
               localPacsServers += string.Format(Constants.ScpServerFormat,
                                              scp.AETitle,
                                              scp.Address,
                                              scp.Port,
                                              scp.Timeout,
                                              (scp == DefaultQueryRetrieveServer) ? 1 : 0,
                                              (scp == DefaultStorageServer) ? 1 : 0,
                                              scp.Secure ? 1 : 0);
            }

            localDebugging = string.Format("{0};{1};{2};{3}",
                                        Debugging.Enable.ToString(),
                                        Debugging.DisplayDetailedErrors.ToString(),
                                        Debugging.GenerateLogFile.ToString(),
                                        Debugging.LogFileName);

            localCompression = string.Format("{0};{1}",
                                          Compression.Enable.ToString(),
                                          Compression.Lossy.ToString());

            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            ExeConfigurationFileMap exeCommonConfig = new ExeConfigurationFileMap();

            exeCommonConfig.ExeConfigFilename = ConfigFile;// Path.Combine(Application.StartupPath, "ViewerCommon.config");                  
            commonConfig = ConfigurationManager.OpenMappedExeConfiguration(exeCommonConfig, ConfigurationUserLevel.None);

            List<string> keys = new List<string>(config.AppSettings.Settings.AllKeys);
            List<string> commonKeys = new List<string>(commonConfig.AppSettings.Settings.AllKeys);

            commonConfig.AppSettings.Settings.Clear();

            AddConfigValue(commonConfig, keys, Constants.WorkstationAETitle, WorkstationClient.AETitle);
            AddConfigValue(commonConfig, keys, Constants.WorkstationAddress, WorkstationClient.Address);
            AddConfigValue(commonConfig, keys, Constants.WorkstationPort, WorkstationClient.Port.ToString());
            AddConfigValue(commonConfig, keys, Constants.WorkstationSecure, WorkstationClient.Secure.ToString());

            AddConfigValue(commonConfig, keys, Constants.PacsServers, localPacsServers);
            AddConfigValue(commonConfig, keys, Constants.DebugInfo, localDebugging);
            AddConfigValue(commonConfig, keys, Constants.CompressionInfo, localCompression);
            AddConfigValue(commonConfig, keys, Constants.ApplicationName, ApplicationName);
            AddConfigValue(commonConfig, keys, Constants.HelpFile, HelpFile);
            AddConfigValue(commonConfig, keys, Constants.DatabaseConfigName, DatabaseConfigName);
            AddConfigValue(commonConfig, keys, Constants.DatabaseConfigEXEName, DatabaseConfigEXEName);
            AddConfigValue(commonConfig, keys, Constants.DatabaseConfigAltEXEName, DatabaseConfigAltEXEName);
            AddConfigValue(commonConfig, keys, Constants.RunPacsConfig, RunPacsConfig.ToString());
            AddConfigValue(commonConfig, keys, Constants.MoveToWSClient, MoveToWSClient.ToString());
            AddConfigValue(commonConfig, keys, Constants.SetClientToAllWS, SetClientToAllWorkstations.ToString());
            AddConfigValue(commonConfig, keys, Constants.ClientBrowsingMode, ClientBrowsingMode.ToString());
            AddConfigValue(commonConfig, keys, Constants.RunFullScreen, RunFullScreen.ToString());
            AddConfigValue(commonConfig, keys, Constants.QueueAutoLoad, QueueAutoLoad.ToString());
            AddConfigValue(commonConfig, keys, Constants.QueueRemoveItem, QueueRemoveItem.ToString());
            AddConfigValue(commonConfig, keys, Constants.ContinueRetrieveOnDuplicateInstance, ContinueRetrieveOnDuplicateInstance.ToString());
            AddConfigValue(commonConfig, keys, Constants.EnableLazyLoading, ViewerLazyLoading.Enable.ToString());
            AddConfigValue(commonConfig, keys, Constants.AutoCreateService, AutoCreateService.ToString());
            AddConfigValue(commonConfig, keys, Constants.LazyLoadingHiddenImages, ViewerLazyLoading.HiddenImages.ToString());
            AddConfigValue(commonConfig, keys, Constants.ViewerOverlayTextSize, ViewerOverlayTextSize.ToString());
            AddConfigValue(commonConfig, keys, Constants.ViewerAutoSizeOverlayText, ViewerAutoSizeOverlayText.ToString());
            AddConfigValue(commonConfig, keys, Constants.ActivePacs, __ActivePacsIndex.ToString());
            AddConfigValue(commonConfig, keys, Constants.SaveSessionBehavior, SaveSessionBehavior.ToString());
            AddConfigValue(commonConfig, keys, Constants.DefualtServiceDisplay, ListenerServiceDefaultDisplayName);
            AddConfigValue(commonConfig, keys, Constants.DefualtServiceName, ListenerServiceDefaultName);
            AddConfigValue(commonConfig, keys, Constants.WorkstationServiceName, ListenerServiceName);
            AddConfigValue(commonConfig, keys, Constants.WorkstationServiceAE, WorkstationServiceAE);
            AddConfigValue(commonConfig, commonKeys, Constants.AnnotationDefaultColor, AnnotationDefaultColor.ToArgb().ToString());
            AddConfigValue(commonConfig, commonKeys, Constants.MeasurementUnit, MeasurementUnit.ToString());

            AddConfigValue(commonConfig, keys, Constants.SecurityCertificationAuthoritiesFileName, SecurityOptions.CertificationAuthoritiesFileName);
            AddConfigValue(commonConfig, keys, Constants.SecurityCertificateFileName, SecurityOptions.CertificateFileName);
            AddConfigValue(commonConfig, keys, Constants.SecurityKeyFileName, SecurityOptions.KeyFileName);
            AddConfigValue(commonConfig, keys, Constants.SecurityPassword, SecurityOptions.Password);
            AddConfigValue(commonConfig, keys, Constants.SecurityCertificateType, SecurityOptions.CertificateType.ToString());
            AddConfigValue(commonConfig, keys, Constants.SecurityMaximumVerificationDepth, SecurityOptions.MaximumVerificationDepth.ToString());

            AddConfigValue(commonConfig, keys, Constants.SecurityOptions, SecurityOptions.Options.ToString());
            AddConfigValue(commonConfig, keys, Constants.SecurityVerificationFlags, SecurityOptions.VerificationFlags.ToString());
            AddConfigValue(commonConfig, keys, Constants.SecuritySslMethodType, SecurityOptions.SslMethodType.ToString());
            AddConfigValue(commonConfig, keys, Constants.SecurityShowPassword, SecurityOptions.ShowPassword.ToString());

            // AddConfigValue(commonConfig, keys, Constants.SecurityCipherSuites, CipherSuiteItems.Serialize(SecurityOptions.CipherSuites));

            string notUSed = SecurityOptions.CipherSuites.Serialize();

            AddConfigValue(commonConfig, keys, Constants.SecurityCipherSuites, SecurityOptions.CipherSuites.Serialize());

            //config.Save       ( ConfigurationSaveMode.Modified ) ;
            commonConfig.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("appSettings");

            _isDirty = false;

            OnChangesSaved();
         }
         catch (Exception exception)
         {
            System.Diagnostics.Debug.Assert(false, exception.Message);

            throw;
         }
      }

      private static void OnChangesSaved()
      {
         if (null != ChangesSaved)
         {
            ChangesSaved();
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
   }

   [Serializable]
   public class ScpInfo
   {
      public ScpInfo() :
      this("", null, 1000, 30, false)
      {

      }

      public ScpInfo
      (
         string aeTitle,
         string address,
         int port,
         int timeout,
         bool secure
      )
      {
         AETitle = aeTitle;
         Address = address;
         Port = port;
         Timeout = timeout;
         Secure = secure;
      }

      public string Address
      {
         get
         {
            return _address;
         }

         set
         {

            if (_address == null ||
               (value.ToString() != _address.ToString()))
            {
               _address = value;

               OnValueChanged();
            }
         }
      }

      public string AETitle
      {
         get
         {
            return _aeTitle;
         }

         set
         {
            if (value != _aeTitle)
            {
               _aeTitle = value;

               OnValueChanged();
            }
         }
      }

      public int Port
      {
         get
         {
            return _port;
         }

         set
         {
            if (_port != value)
            {
               _port = value;

               OnValueChanged();
            }
         }
      }

      public int Timeout
      {
         get
         {
            return _timeout;
         }

         set
         {
            if (_timeout != value)
            {
               _timeout = value;

               OnValueChanged();
            }
         }
      }

      public bool Secure
      {
         get
         {
            return _secure;
         }

         set
         {
            if (_secure != value)
            {
               _secure = value;

               OnValueChanged();
            }
         }
      }

      public override string ToString()
      {
         string name = AETitle;
         if (Secure)
         {
            name = name + "  (Secure)";
         }
         return name;
      }

      private void OnValueChanged()
      {
         if (null != ValueChanged)
         {
            ValueChanged(this, new EventArgs());
         }
      }

      public event EventHandler ValueChanged;

      private string _address;
      private string _aeTitle;
      private int _port;
      private int _timeout;
      private bool _secure;


      public override bool Equals(object obj)
      {
         try
         {
            ScpInfo scpObj;


            scpObj = obj as ScpInfo;

            if (null == scpObj)
            {
               return false;
            }

            return (this.AETitle == scpObj.AETitle && this.Address == scpObj.Address && this.Port == scpObj.Port && this.Timeout == scpObj.Timeout);
         }
         catch (Exception)
         {
            return false;
         }
      }

      public override int GetHashCode()
      {
         return AETitle.GetHashCode() ^ Address.GetHashCode() ^ Port.GetHashCode() ^ Timeout.GetHashCode();
      }
   }


   [Serializable]
   public class ScuInfo
   {
      public ScuInfo()
      {

      }


      internal AeInfo ToAeInfo()
      {
         AeInfo ae = new AeInfo();

         ae.Address = Address;
         ae.AETitle = AETitle;
         ae.Port = Port;
         ae.SecurePort = Port;

         ae.ClientPortUsage = Secure ? Dicom.ClientPortUsageType.Secure : Dicom.ClientPortUsageType.Unsecure;


         return ae;
      }

      internal DicomAE ToDicomAE()
      {
         DicomAE ae = new DicomAE();

         ae.AE = AETitle;
         ae.IPAddress = Address;
         ae.Port = Port;

         return ae;
      }

      public string Address
      {
         get
         {
            return _address;
         }

         set
         {
            if (value != _address)
            {
               _address = value;

               OnValueChanged();
            }
         }
      }

      public string AETitle
      {
         get
         {
            return _aeTitle;
         }

         set
         {
            if (_aeTitle != value)
            {
               _aeTitle = value;

               OnValueChanged();
            }
         }
      }

      public int Port
      {
         get
         {
            return _port;
         }

         set
         {
            if (_port != value)
            {
               _port = value;

               OnValueChanged();
            }
         }
      }

      public bool Secure
      {
         get
         {
            return _secure;
         }

         set
         {
            if (_secure != value)
            {
               _secure = value;

               OnValueChanged();
            }
         }
      }

      private void OnValueChanged()
      {
         if (null != ValueChanged)
         {
            ValueChanged(this, new EventArgs());
         }
      }

      public event EventHandler ValueChanged;
      private string _address = string.Empty;
      private string _aeTitle = string.Empty;
      private int _port = -1;
      private bool _secure = false;
   }

   class ScpInfoBindingList : BindingList<ScpInfo>
   {
      protected override void InsertItem(int index, ScpInfo item)
      {
         base.InsertItem(index, item);

         item.ValueChanged += new EventHandler(item_ValueChanged);
      }

      void item_ValueChanged(object sender, EventArgs e)
      {
         if (null != ScpValueChanged)
         {
            ScpValueChanged(sender, new EventArgs());
         }
      }

      protected override void RemoveItem(int index)
      {
         if (index >= 0 && index < Count)
         {
            this[index].ValueChanged -= new EventHandler(item_ValueChanged);
         }

         base.RemoveItem(index);
      }

      public event EventHandler ScpValueChanged;
   }

   public enum SaveOptions
   {
      AlwaysSave,
      NeverSave,
      PromptUser
   }

   public class MyAppSettingsReader
   {
      private NameValueCollection map;

      private static string NullString;

      private static Type[] paramsArray;

      private static Type stringType;

      static MyAppSettingsReader()
      {
         MyAppSettingsReader.stringType = typeof(string);
         Type[] typeArray = new Type[1];
         typeArray[0] = MyAppSettingsReader.stringType;
         MyAppSettingsReader.paramsArray = typeArray;
         MyAppSettingsReader.NullString = "None";
      }

      public MyAppSettingsReader()
      {
         ExeConfigurationFileMap exeCommonConfig = new ExeConfigurationFileMap();
         System.Configuration.Configuration config;

         exeCommonConfig.ExeConfigFilename = ConfigurationData.ConfigFile;
         config = ConfigurationManager.OpenMappedExeConfiguration(exeCommonConfig, ConfigurationUserLevel.None);
         map = new NameValueCollection();
         foreach (string key in config.AppSettings.Settings.AllKeys)
         {
            map.Add(key, config.AppSettings.Settings[key].Value);
         }
      }

      private int GetNoneNesting(string val)
      {
         int num = 0;
         int length = val.Length;
         if (length > 1)
         {
            while (val[num] == '(' && val[length - num - 1] == ')')
            {
               num++;
            }
            if (num > 0 && string.Compare(MyAppSettingsReader.NullString, 0, val, num, length - 2 * num, StringComparison.Ordinal) != 0)
            {
               num = 0;
            }
         }
         return num;
      }

      public object GetValue(string key, Type type)
      {
         object obj;
         string str;
         if (key != null)
         {
            if (type != null)
            {
               string item = this.map[key];
               if (item != null)
               {
                  if (type != MyAppSettingsReader.stringType)
                  {
                     try
                     {
                        obj = Convert.ChangeType(item, type, CultureInfo.InvariantCulture);
                     }
                     catch
                     {
                        if (item.Length == 0)
                        {
                           str = "AppSettingsReaderEmptyString";
                        }
                        else
                        {
                           str = item;
                        }
                        string str1 = str;
                        object[] objArray = new object[3];
                        objArray[0] = str1;
                        objArray[1] = key;
                        objArray[2] = type.ToString();
                        throw;
                     }
                     return obj;
                  }
                  else
                  {
                     int noneNesting = this.GetNoneNesting(item);
                     if (noneNesting != 0)
                     {
                        if (noneNesting != 1)
                        {
                           return item.Substring(1, item.Length - 2);
                        }
                        else
                        {
                           return null;
                        }
                     }
                     else
                     {
                        return item;
                     }
                  }
               }
               else
               {
                  object[] objArray1 = new object[1];
                  objArray1[0] = key;
                  throw new InvalidOperationException("Key not found");
               }
            }
            else
            {
               throw new ArgumentNullException("type");
            }
         }
         else
         {
            throw new ArgumentNullException("key");
         }
      }
   }
}
