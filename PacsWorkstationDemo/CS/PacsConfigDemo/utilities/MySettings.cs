// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Xml.Serialization;
using System.IO;

using Leadtools.DicomDemos;
using System.Reflection;

namespace PACSConfigDemo
{
   public class MyAppSettings
   {
      
      public string ServerAE
      {
         get
         {
            return _serverAE ;
         }
         
         set
         {
            _serverAE = value ;
         }
      }
      
      public int ServerPort
      {
         get
         {
            return _serverPort ;
         }
         
         set
         {
            _serverPort = value ;
         }
      }

      public int StorageServerPort
      {
         get { return _storageServerPort; }
         set { _storageServerPort = value; }
      }

      public string StorageServerAE
      {
         get { return _storageServerAE; }
         set { _storageServerAE = value; }
      }
      public bool StorageStartServer
      {
         get { return _storageStartServer; }
         set { _storageStartServer = value; }
      }
      
      public int WorklistServerPort
      {
         get;
         set;
      }

      public string WorklistServerAE
      {
         get;
         set;
      }
      
      public bool WorklistStartServer
      {
         get;
         set;
      }
      
      public string ClientAE
      {
         get
         {
            return _clientAE ;
         }
         
         set
         {
            _clientAE = value ;
         }
      }
      
      public int ClientPort
      {
         get
         {
            return _clientPort ;
         }
         
         set
         {
            _clientPort = value ;
         }
      }
      
      public bool StartServer
      {
         get
         {
            return _startServer ;
         }
         
         set
         {
            _startServer = value ;
         }
      }
      
      public bool ShowHelpOnStart
      {
         get
         {
            return _showHelpOnStart ;
         }
         
         set
         {
            _showHelpOnStart = value ;
         }
      }

      public bool FirstRun
      {
         get { return _firstRun; }
         set { _firstRun = value; }
      }

      
      public bool ConfigureDemoServer 
      {
         get
         {
            return _configureDemoServer ;
         }
         
         set
         {
            _configureDemoServer = value ;
         }
      }
      
      public bool ConfigureWSServer 
      {
         get
         {
            return _configureWSServer ;
         }
         
         set
         {
            _configureWSServer = value ;
         }
      }
      
      public bool ConfigureWorklistServer
      {
         get;
         set;
      }
      
      public bool ConfigureStorageServer
      {
         get;
         set;
      }
      
      public bool ConfigureMainClient 
      {
         get
         {
            return _configureMainClient ;
         }
         
         set
         {
            _configureMainClient = value ;
         }
      }
      
      public bool ConfigureWSClient 
      {
         get
         {
            return _configureWSClient ;
         }
         
         set
         {
            _configureWSClient = value ;
         }
      }
      
      public bool WsStartServer 
      {
         get
         {
            return _wsStartServer ;
         }
         
         set
         {
            _wsStartServer = value ;
         }
      }
      
      public string WsServerAE 
      {
         get
         {
            return _wsServerAE ;
         }
         
         set
         {
            _wsServerAE = value ;
         }
      }
      
      public int WsServerPort 
      {
         get
         {
            return _wsServerPort ;
         }
         
         set
         {
            _wsServerPort = value ;
         }
      }
      
      public string WsClientAE 
      {
         get
         {
            return _wsClientAE ;
         }
         
         set
         {
            _wsClientAE = value ;
         }
      }
      
      public int WsClientPort 
      {
         get
         {
            return _wsClientPort ;
         }
         
         set
         {
            _wsClientPort = value ;
         }
      }


      //public string RoutingServerAE
      //{
      //   get { return _routingServerAE; }
      //   set { _routingServerAE = value; }
      //}

      //public int RoutingServerPort
      //{
      //   get { return _routingServerPort; }
      //   set { _routingServerPort = value; }
      //}

      //public bool RoutingStartServer
      //{
      //   get { return _routingStartServer; }
      //   set { _routingStartServer = value; }
      //}

#if LEADTOOLS_V175_OR_LATER
      public bool InstallBrokerHost
      {
         get { return _installBrokerHost; }
         set { _installBrokerHost = value; }
      }

      public int BrokerHostPort
      {
         get { return _brokerHostPort; }
         set { _brokerHostPort = value; }
      }
#endif


      public bool ResetClientConfigurations
      {
         get { return _resetClientConfigurations; }
         set { _resetClientConfigurations = value; }
      }
      
      // Servers
      private string _serverAE ;
      private int    _serverPort ;
      private bool   _startServer ;

      private string _storageServerAE;
      private int _storageServerPort;
      private bool _storageStartServer;

      private string _wsServerAE;
      private int _wsServerPort;
      private bool _wsStartServer;

      //private string _routingServerAE;
      //private int _routingServerPort;
      //private bool _routingStartServer;

      // Clients
      private string _clientAE;
      private int _clientPort;
      private string _wsClientAE;
      private int    _wsClientPort;


      // Notused
      private bool _configureDemoServer;
      private bool _configureWSServer;
      private bool _configureMainClient;
      private bool _configureWSClient;

      // Misc
      private bool _resetClientConfigurations;
      private bool _showHelpOnStart;
      private bool _firstRun;

#if LEADTOOLS_V175_OR_LATER
      // Broker Host
      private bool _installBrokerHost;
      private int _brokerHostPort;
#endif


      public MyAppSettings ( )
      {
         ConfigureStorageServer = true;
         ConfigureWorklistServer = true;
         ConfigureDemoServer = true ;
         ConfigureWSServer   = true ;
         ConfigureMainClient = true ;
         ConfigureWSClient   = true ;
         ResetClientConfigurations = true;
         ShowHelpOnStart = true;
         FirstRun = true;

#if LEADTOOLS_V175_OR_LATER
         InstallBrokerHost = true;
#endif

         if (!DicomDemoSettingsManager.Is64Process())
         {
#if LTV17_CONFIG
            ServerAE = "L17_SERVER_32";
            ServerPort = 204;
            ClientAE = "L17_CLIENT_32";
            ClientPort = 1000;

            WsServerAE = "L17_WS_SERVER_32";
            WsServerPort = 205;
            WsClientAE = "L17_WS_CLIENT_32";
            WsClientPort = 1001;

            RoutingServerAE = "L17_ROUTER_32";
            RoutingServerPort = 206;
#elif LTV175_CONFIG
            ServerAE = "L175_SERVER32";
            ServerPort = 304;
            ClientAE = "L175_CLIENT32";
            ClientPort = 1000;

            StorageServerAE = "L175_PACS_SCP32";
            StorageServerPort = 504;
            
            WorklistServerAE = "L175_MWL_SCP32";
            WorklistServerPort = 604;

            WsServerAE = "L175_WS_SERVER32";
            WsServerPort = 305;
            WsClientAE = "L175_WS_CLIENT32";
            WsClientPort = 1001;

            //RoutingServerAE = "L175_ROUTER32";
            //RoutingServerPort = 306;
            BrokerHostPort = 8731;
#elif LTV18_CONFIG
            ServerAE = "L18_SERVER32";
            ServerPort = 304;
            ClientAE = "L18_CLIENT32";
            ClientPort = 1000;

            StorageServerAE = "L18_PACS_SCP32";
            StorageServerPort = 504;
            
            WorklistServerAE = "L18_MWL_SCP32";
            WorklistServerPort = 604;

            WsServerAE = "L18_WS_SERVER32";
            WsServerPort = 305;
            WsClientAE = "L18_WS_CLIENT32";
            WsClientPort = 1001;

            //RoutingServerAE = "L18_ROUTER32";
            //RoutingServerPort = 306;
            BrokerHostPort = 8731;
#elif LTV19_CONFIG
            ServerAE = "L19_SERVER32";
            ServerPort = 324;
            ClientAE = "L19_CLIENT32";
            ClientPort = 1020;

            StorageServerAE = "L19_PACS_SCP32";
            StorageServerPort = 524;
            
            WorklistServerAE = "L19_MWL_SCP32";
            WorklistServerPort = 624;

            WsServerAE = "L19_WS_SERVER32";
            WsServerPort = 325;
            WsClientAE = "L19_WS_CLIENT32";
            WsClientPort = 1021;

            //RoutingServerAE = "L19_ROUTER32";
            //RoutingServerPort = 306;
            BrokerHostPort = 8751;
#elif LTV20_CONFIG
            ServerAE = "L20_SERVER32";
            ServerPort = 324;
            ClientAE = "L20_CLIENT32";
            ClientPort = 1020;

            StorageServerAE = "L20_PACS_SCP32";
            StorageServerPort = 524;
            
            WorklistServerAE = "L20_MWL_SCP32";
            WorklistServerPort = 624;

            WsServerAE = "L20_WS_SERVER32";
            WsServerPort = 325;
            WsClientAE = "L20_WS_CLIENT32";
            WsClientPort = 1021;

            //RoutingServerAE = "L20_ROUTER32";
            //RoutingServerPort = 306;
            BrokerHostPort = 8751;
#endif
            StartServer = true;
            StorageStartServer = true;
            WsStartServer = true;
            WorklistStartServer = true;
            // RoutingStartServer = true;
         }
         else
         {
#if LTV17_CONFIG
            ServerAE = "L17_SERVER_64";
            ServerPort = 214;
            ClientAE = "L17_CLIENT_64";
            ClientPort = 1010;

            WsServerAE = "L17_WS_SERVER_64";
            WsServerPort = 215;
            WsClientAE = "L17_WS_CLIENT_64";
            WsClientPort = 1011;

            RoutingServerAE = "L17_ROUTER_64";
            RoutingServerPort = 216;
#elif LTV175_CONFIG
            ServerAE = "L175_SERVER64";
            ServerPort = 314;
            ClientAE = "L175_CLIENT64";
            ClientPort = 1010;

            StorageServerAE = "L175_PACS_SCP64";
            StorageServerPort = 514;
            
            WorklistServerAE = "L175_MWL_SCP_64";
            WorklistServerPort = 614;

            WsServerAE = "L175_WS_SERVER64";
            WsServerPort = 315;
            WsClientAE = "L175_WS_CLIENT64";
            WsClientPort = 1011;

            // RoutingServerAE = "L175_ROUTER64";
            // RoutingServerPort = 316;
#elif LTV18_CONFIG
            ServerAE = "L18_SERVER64";
            ServerPort = 314;
            ClientAE = "L18_CLIENT64";
            ClientPort = 1010;

            StorageServerAE = "L18_PACS_SCP64";
            StorageServerPort = 514;
            
            WorklistServerAE = "L18_MWL_SCP_64";
            WorklistServerPort = 614;

            WsServerAE = "L18_WS_SERVER64";
            WsServerPort = 315;
            WsClientAE = "L18_WS_CLIENT64";
            WsClientPort = 1011;

            // RoutingServerAE = "L18_ROUTER64";
            // RoutingServerPort = 316;
#elif LTV19_CONFIG
            ServerAE = "L19_SERVER64";
            ServerPort = 334;
            ClientAE = "L19_CLIENT64";
            ClientPort = 1030;

            StorageServerAE = "L19_PACS_SCP64";
            StorageServerPort = 534;
            
            WorklistServerAE = "L19_MWL_SCP_64";
            WorklistServerPort = 634;

            WsServerAE = "L19_WS_SERVER64";
            WsServerPort = 335;
            WsClientAE = "L19_WS_CLIENT64";
            WsClientPort = 1031;

            // RoutingServerAE = "L19_ROUTER64";
            // RoutingServerPort = 316;
            BrokerHostPort = 8761;
#elif LTV20_CONFIG
            ServerAE = "L20_SERVER64";
            ServerPort = 334;
            ClientAE = "L20_CLIENT64";
            ClientPort = 1030;

            StorageServerAE = "L20_PACS_SCP64";
            StorageServerPort = 534;
            
            WorklistServerAE = "L20_MWL_SCP64";
            WorklistServerPort = 634;

            WsServerAE = "L20_WS_SERVER64";
            WsServerPort = 335;
            WsClientAE = "L20_WS_CLIENT64";
            WsClientPort = 1031;

            // RoutingServerAE = "L20_ROUTER64";
            // RoutingServerPort = 316;
            BrokerHostPort = 8761;
#endif

            StartServer = true;
            StorageStartServer = true;
            WsStartServer = true;
            WorklistStartServer = true;
            // RoutingStartServer = true;

#if LEADTOOLS_V175_OR_LATER
            BrokerHostPort = 8800;
#endif

         }
      }
   }

   public class MySettings
   {
      public MyAppSettings _settings;
      public MySettings()
      {
         _settings = new MyAppSettings();
      }

      [DllImport("shfolder.dll", CharSet = CharSet.Auto)]
      private static extern int SHGetFolderPath(IntPtr hwndOwner, int nFolder, IntPtr hToken, int dwFlags, StringBuilder lpszPath);

      private const int CommonDocumentsFolder = 0x2e;

      public static string GetFolderPath()
      {
         StringBuilder lpszPath = new StringBuilder(260);
         // CommonDocuments is the folder than any Vista user (including 'guest') has read/write access
         SHGetFolderPath(IntPtr.Zero, (int)CommonDocumentsFolder, IntPtr.Zero, 0, lpszPath);
         string path = lpszPath.ToString();
         new FileIOPermission(FileIOPermissionAccess.PathDiscovery, path).Demand();
         return path;
      }

      public static string GetSettingsFilename()
      {
         string fullname = Assembly.GetExecutingAssembly().Location;
         string settingsFilename = DicomDemoSettingsManager.GetSettingsFilename(Path.GetFileName(fullname));
         return settingsFilename;
      }

      public void Save()
      {
         try
         {
            string filename = GetSettingsFilename();
            XmlSerializer xs = new XmlSerializer(typeof(MyAppSettings));
            TextWriter xmlTextWriter = new StreamWriter(filename);

            xs.Serialize(xmlTextWriter, this._settings);
            xmlTextWriter.Close();
         }
         catch (Exception ex)
         {
            System.Windows.Forms.MessageBox.Show(ex.Message);
         }
      }

      public void Load()
      {
         XmlSerializer SerializerObj = new XmlSerializer(typeof(MyAppSettings));
         string filename = GetSettingsFilename();

         if (File.Exists(filename))
         {
            try
            {
               // Create a new file stream for reading the XML file
               FileStream ReadFileStream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);

               // Load the object saved above by using the Deserialize function
               _settings = (MyAppSettings)SerializerObj.Deserialize(ReadFileStream);

               // Cleanup
               ReadFileStream.Close();
            }
            catch (Exception)
            {
            }
         }
      }
   }
}
