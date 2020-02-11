// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq ;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using Leadtools.Dicom.AddIn.Common;
using Leadtools.Dicom;
using Leadtools.Dicom.Server.Admin;
using Leadtools.Demos;
using System.Xml;
using PACSConfigDemo.UI;
using System.Collections;
using Leadtools.DicomDemos;
using System.Threading;
using Leadtools.Medical.OptionsDataAccessLayer.Configuration;
using Leadtools.Medical.OptionsDataAccessLayer;
using Leadtools.Medical.DataAccessLayer;
using Leadtools.Medical.AeManagement.DataAccessLayer.Configuration;
using Leadtools.Medical.AeManagement.DataAccessLayer;
using Leadtools.Demos.StorageServer.DataTypes;
using Leadtools.Medical.PermissionsManagement.DataAccessLayer;
using Leadtools.Medical.PermissionsManagement.DataAccessLayer.Configuration;
using Leadtools.Medical.Winforms.DataAccessLayer.Configuration;

using System.Text.RegularExpressions;
#if LEADTOOLS_V19_OR_LATER
using System.ServiceProcess;
using Leadtools.Medical.Storage.AddIns.Messages;
#endif // #if LEADTOOLS_V19_OR_LATER

namespace PACSConfigDemo
{
   public partial class MainForm : Form
   {
      public MainForm()
      {
         InitializeComponent();
      }

      // Settings
      public static MySettings _mySettings = new MySettings();
      public string _defaultIP = string.Empty;

      public ServiceAdministrator _serviceAdmin;


      private void MainForm_Load(object sender, EventArgs e)
      {
         // Load Settings
         _mySettings.Load();
         
         if (string.IsNullOrEmpty(_mySettings._settings.StorageServerAE))
         {
             _mySettings._settings.StorageServerAE = "L175_PACS_SCP32";
             _mySettings._settings.StorageServerPort = 504;
         }

         Text = Program._demoName;

         _serviceAdmin = Program.CreateServiceAdministrator();

         textBoxServerAe.Text          = _mySettings._settings.ServerAE;
         numericUpDownServerPort.SetValueAndValidate(_mySettings._settings.ServerPort);
         textBoxClientAe.Text          = _mySettings._settings.ClientAE;
         numericUpDownClientPort.SetValueAndValidate(_mySettings._settings.ClientPort);
         checkBoxStartServer.Checked   = _mySettings._settings.StartServer;
         checkBoxResetClientConfigurations.Checked = _mySettings._settings.ResetClientConfigurations;
         
         textBoxStorageServerAe.Text   = _mySettings._settings.StorageServerAE;
         numericUpDownStorageServerPort.SetValueAndValidate(_mySettings._settings.StorageServerPort);
         checkBoxStartStorageServer.Checked = _mySettings._settings.StorageStartServer;
         
         textBoxWorklistServerAe.Text   = _mySettings._settings.WorklistServerAE;
         numericUpDownWorklistServerPort.SetValueAndValidate(_mySettings._settings.WorklistServerPort);
         checkBoxStartWorklistServer.Checked = _mySettings._settings.WorklistStartServer;

#if LEADTOOLS_V19_OR_LATER
         buttonRetest.Visible = true;
         labelRetest.Visible = true;
#else
         buttonRetest.Visible = false;
         labelRetest.Visible = false;
#endif

#if LEADTOOLS_V175_OR_LATER
         checkBoxBrokerHost.Checked = _mySettings._settings.InstallBrokerHost;
         numericUpDownBrokerPort.SetValueAndValidate(_mySettings._settings.BrokerHostPort);
#else
         checkBoxBrokerHost.Visible = false;
         numericUpDownBrokerPort.Visible = false;
#endif
         
         textBoxWsServerAe.Text     = _mySettings._settings.WsServerAE;
         numericUpDownWsServerPort.SetValueAndValidate(_mySettings._settings.WsServerPort);
         textBoxWsClientAe.Text     = _mySettings._settings.WsClientAE;
         numericUpDownWsClientPort.SetValueAndValidate(_mySettings._settings.WsClientPort);
         checkBoxStartWsServer.Checked   = _mySettings._settings.WsStartServer;

         //textBoxRoutingServerAe.Text = _mySettings._settings.RoutingServerAE;
         //numericUpDownRoutingServerPort.SetValueAndValidate(_mySettings._settings.RoutingServerPort);
         //checkBoxStartRoutingServer.Checked = _mySettings._settings.RoutingStartServer;

         // error provider
         errorProviderWarning.Icon = PACSConfigDemo.Properties.Resources.InvalidKey;
         errorProviderWarning.BlinkStyle = ErrorBlinkStyle.NeverBlink;

         // Set up the tooltips
         toolTip.AutoPopDelay = 5000;
         toolTip.InitialDelay = 1000;
         toolTip.ReshowDelay = 500;
         toolTip.ShowAlways = true;

         toolTip.SetToolTip(labelDicomDemoServer, "Main DICOM demo server which privides query/retrieve, storage, storage commitment, MWL, MPPS and Verification.");
         toolTip.SetToolTip(labelWorkstationServer, "DICOM listening service for the Medical Workstation Viewer.");
         // toolTip.SetToolTip(labelRoutingServer, "A demo Routing service. Images stored to this server will be forwarded to the Main Server, and then routed to Workstation Server if modality is MR.");

         toolTip.SetToolTip(labelMainClient, "Settings used by the high level DICOM client demos (client, store, mwl, PrintToPACS).");
         toolTip.SetToolTip(labelWorkstationClient, "Settings that define the Medical Workstation Viewer DICOM client.");

         toolTip.SetToolTip(textBoxServerAe, "Application Entity (name) of the DICOM server.");
         toolTip.SetToolTip(numericUpDownServerPort, "Port on which the DICOM server listens.");
         
         toolTip.SetToolTip(numericUpDownBrokerPort, "Port on which the Broker Host Add-in listens.");

         toolTip.SetToolTip(textBoxWsServerAe, "Application Entity (name) of the Workstation DICOM server.");
         toolTip.SetToolTip(numericUpDownWsServerPort, "Port on which the Workstation server listens.");

         //toolTip.SetToolTip(textBoxRoutingServerAe, "Application Entity (name) of the Routing DICOM server.");
         //toolTip.SetToolTip(numericUpDownRoutingServerPort, "Port on which the Routing DICOM server listens.");


         toolTip.SetToolTip(textBoxClientAe, "Application Entity (name) of the high level DICOM client demos (client, store, mwl, PrintToPACS).");
         toolTip.SetToolTip(numericUpDownClientPort, "Port on which the DICOM high level DICOM client demos listen.");

         toolTip.SetToolTip(this.textBoxWsClientAe, "Application Entity (name) of the Medical Workstation Viewer DICOM client.");
         toolTip.SetToolTip(numericUpDownWsClientPort, "Port on which the Medical Workstation Viewer DICOM client listens.");


         toolTip.SetToolTip(buttonConfigure, "Create DICOM Server Service and configure high level DICOM client demos.");
         toolTip.SetToolTip(buttonRemove, "Remove one or more DICOM server services.");
         toolTip.SetToolTip(buttonClose, "Exit this program.");

         // toolTip.SetToolTip(groupBoxServer, "Server Settings: define a sample DICOM Server Service that will be created and configured.");
         // toolTip.SetToolTip(groupBoxClient, "Client Settings: configure the already installed DICOM High Level Client and Store demos.");
         
         toolTip.SetToolTip(buttonDemoServerHelp, "Shows help for Main Server.");
         toolTip.SetToolTip(buttonWorkstationServerHelp, "Shows help for Workstation Server.");
         // toolTip.SetToolTip(buttonRoutingServerHelp, "Shows help for Routing Server.");
         toolTip.SetToolTip(buttonMainClientHelp, "Shows help for Main Client.");
         toolTip.SetToolTip(buttonWorkstationClientHelp, "Shows help for Workstation Client.");

         if (!DicomDemoSettingsManager.Is64Process())
         {
            labelInstructions.Text = "Click the Configure button to configure the 32-bit PACS Framework Demos.";
         }
         else
         {
            labelInstructions.Text = "Click the Configure button to configure the 64-bit PACS Framework Demos.";
         }

         labelServerInstructions.Text = "Enter the service settings to configure the DICOM server services.";
         labelClientInstructions.Text = "Enter the client settings to configure the demos.";
         labelConfigure.Text = "<= Click to create the DICOM services and configure the client demos.";
         labelRemove.Text = "<= Click to remove the DICOM services.";
         labelRetest.Text = "<= Click to retest the DICOM services";
         _defaultIP = MyUtils.GetDefaultIp();

         ImageList imageListSmall = new ImageList();
         imageListSmall.Images.Add(PACSConfigDemo.Properties.Resources.Start);
         imageListSmall.Images.Add(PACSConfigDemo.Properties.Resources.InvalidKey);
         imageListSmall.Images.Add(errorProvider.Icon);

         listViewStatus.SmallImageList = imageListSmall;
         
         showAdvancedOptions(false);
         
         EnableDialogItems(true, _serviceAdmin );
      }

      private void EnableDialogItems(bool bEnable, ServiceAdministrator serviceAdmin )
      {
         try
         {
            // menu
            menuStrip.Enabled = bEnable;

            // buttons
            buttonClose.Enabled = bEnable;
            buttonConfigure.Enabled = bEnable;

            int nServicesCount = 0;
            if (serviceAdmin != null)
               nServicesCount = serviceAdmin.Services.Count;

            buttonRemove.Enabled = bEnable && (nServicesCount > 0);
            buttonRetest.Enabled = bEnable && (nServicesCount > 0);

#if LEADTOOLS_V175_OR_LATER
            numericUpDownBrokerPort.Enabled = (checkBoxBrokerHost.Checked == true);
#endif

            Application.DoEvents();
         }
         catch (Exception)
         {
         }
      }

      private void ClearErrors ( )
      {
         _showHelp = true;
         try
         {
            listViewStatus.Items.Clear();

            errorProvider.SetError(textBoxServerAe, "");
            errorProvider.SetError(textBoxWsServerAe, "");
            // errorProvider.SetError(textBoxRoutingServerAe, "");
            errorProvider.SetError(textBoxClientAe, "");
            errorProvider.SetError(textBoxWsClientAe, "");

            errorProvider.SetError(numericUpDownServerPort, "");
            errorProvider.SetError(numericUpDownWsServerPort, "");
            // errorProvider.SetError(numericUpDownRoutingServerPort, "");
            errorProvider.SetError(numericUpDownClientPort, "");
            errorProvider.SetError(numericUpDownWsClientPort, "");
         }
         catch (Exception )
         {
         }
      }

      private Font _hyperlinkFont = null;

      public Font HyperlinkFont
      {
         get
         {
            if (_hyperlinkFont == null)
            {
               _hyperlinkFont = new Font(listViewStatus.Font, FontStyle.Underline);
            }
            return _hyperlinkFont;
         }
      }

      public void AddItemWithIcon(string sMsg, StatusType status)
      {
         AddItemWithIcon(sMsg, status, false);
      }

      public void AddItemWithIcon(string sMsg, StatusType status, bool hyperlink)
      {
         // List<string> strings = (from Match m in Regex.Matches(sMsg, @"\d{256}") select m.Value).ToList();
         List<string> strings = new List<string>(Regex.Split(sMsg, @"(?<=\G.{256})", RegexOptions.Singleline));
         foreach (string s in strings)
         {
            AddItemWithIcon256(s, status, hyperlink);
         }
      }

      public void AddItemWithIcon256(string sMsg, StatusType status, bool hyperlink)
      {
         if (sMsg.Trim().Length == 0)
            return;

         Color foreColor = listViewStatus.ForeColor;
         Color backColor = Color.White;
         switch (status)
         {
            case StatusType.Nothing:
               foreColor = Color.DarkGreen;
               break;

            case StatusType.Check:
            case StatusType.Warning:
               foreColor = Color.Blue;
               break;
            case StatusType.Error:
               foreColor = Color.Red;
               break;
         }

         if (hyperlink)
         {
            foreColor = Color.Blue;
         }

         ListViewItem li = new ListViewItem(sMsg.Trim(), (int)status);
         listViewStatus.Items.Add(li);
         li.ForeColor = foreColor;
         li.BackColor = backColor;

         if (hyperlink)
         {
            li.Font = HyperlinkFont;
         }
         listViewStatus.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
         listViewStatus.Items[listViewStatus.Items.Count - 1].EnsureVisible();
      }

      private void ShowError ( Control c, string sError )
      {
         try
         {
            if (c != null)
            {
               errorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
               errorProvider.SetError(c, sError);
            }
            //labelError.ForeColor = Color.Red;
            //labelError.Text = sError;
            AddItemWithIcon(sError, StatusType.Error);
            Application.DoEvents();
         }
         catch (Exception)
         {
         }
      }

      private void ShowWarning(Control c,string sMsg)
      {
         try
         {
            if (c != null)
            {
               //labelError.ForeColor = Color.Blue;
               errorProviderWarning.SetError(c, sMsg);
            }
            //labelError.Text = sMsg;
            AddItemWithIcon(sMsg, StatusType.Warning);
            Application.DoEvents();
         }
         catch (Exception)
         {
         }
      }

      private void ShowHyperlink(string sMsg)
      {
         try
         {
            AddItemWithIcon(sMsg, StatusType.Nothing, true);
            Application.DoEvents();
         }
         catch (Exception)
         {
         }
      }



      private void ShowMessage(string sMsg)
      {
         try
         {
            //labelError.ForeColor = Color.Blue;
            //labelError.Text = sMsg;
            AddItemWithIcon(sMsg, StatusType.Nothing);
            Application.DoEvents();
         }
         catch (Exception)
         {
         }
      }

      private void ShowMessage(string sMsg, StatusType status)
      {
         try
         {
            //labelError.ForeColor = Color.Blue;
            //labelError.Text = sMsg;
            AddItemWithIcon(sMsg, status);
            Application.DoEvents();
         }
         catch (Exception)
         {
         }
      }

      private bool ValidatePort ( NumericUpDown nud, ServiceAdministrator serviceAdmin )
      {
         int nPort = (int)nud.Value;
         bool valid = MyUtils.IsPortAvailable(_defaultIP, nPort);

         // See if another LEAD server is using this port, but not currently listening
         if (valid)
         {
            foreach (KeyValuePair<string, DicomService> kv in serviceAdmin.Services)
            {
               if (kv.Value.Settings.Port == nPort)
                  valid = false;
            }
         }

         if (!valid)
         {
            nud.Focus();
            nud.Select(0, nud.Value.ToString().Length);
            valid = false;
            ShowError ( nud, "Port is not avilable." ) ;
         }
         return valid;
      }

      private bool ValidateAE ( TextBox tb )
      {
         bool valid = true;
         int nLen = tb.Text.Trim().Length;
         if ((nLen < 1) || (nLen > 16))
         {
            tb.Focus();
            tb.Select(0, nLen);
            valid = false;
            ShowError(tb, "Invalid AE Title.");

         }
         return valid;
      }

      private bool ValidateAll ( ServiceAdministrator serviceAdmin )
      {
         // Validate ServerAE
         TextBox[] tbArray = new TextBox[] { textBoxServerAe, textBoxWsServerAe, /*textBoxRoutingServerAe, */textBoxClientAe, textBoxWsClientAe };
         foreach (TextBox tb in tbArray)
         {
            if (ValidateAE(tb) == false)
               return false;
         }

         // Validate Ports
         NumericUpDown[] nudArray = new NumericUpDown[] { numericUpDownServerPort, numericUpDownWsServerPort, /*numericUpDownRoutingServerPort, */numericUpDownClientPort, numericUpDownWsClientPort };
         foreach (NumericUpDown nud in nudArray)
         {
            if (ValidatePort(nud, serviceAdmin) == false)
               return false;
         }
         return true;
      }

      private static bool IsDifferentServer(ServerSettings s1, ServerSettings s2)
      {
         if (s1 == null || s2 == null)
         {
            return true;
         }
         bool bDifferent =
            (string.Compare(s1.AETitle, s2.AETitle, true) != 0) ||
            (string.Compare(s1.IpAddress, s2.IpAddress, true) != 0) ||
            (s1.Port != s2.Port);
         return bDifferent;
      }

      private bool IsServerAlreadyConfigured(ServerSettings settings, ServiceAdministrator serviceAdmin )
      {
         // First check to see if it is already installed
         bool bDifferentServer = true;
         
         foreach (KeyValuePair<string, DicomService> kv in serviceAdmin.Services)
         {
            bDifferentServer = bDifferentServer && IsDifferentServer(settings, kv.Value.Settings);
         }

         if (!bDifferentServer)
         {
            string sMsg = sMsg = string.Format("Server {0} is already configured.", settings.AETitle);
            ShowWarning(null,/*labelError,*/ sMsg);
         }
         
         return !bDifferentServer;
      }

      private XmlNode CreateAppSettingsChild(XmlDocument xmlDocument, XmlNode root, XmlNode appSettings, string keyValue)
      {
         XmlNode xmlRet = null;

         //  "//appSettings/add[@key='PacsServers']"
         string search = string.Format("//appSettings/add[@key='{0}']", keyValue);
         xmlRet = root.SelectSingleNode(search);

         if (xmlRet == null)
         {
            // Create the node because it does not exist
            XmlElement element = xmlDocument.CreateElement("add");

            XmlAttribute attKey = xmlDocument.CreateAttribute("key");
            attKey.Value = keyValue;
            element.SetAttributeNode(attKey);

            XmlAttribute attValue = xmlDocument.CreateAttribute("value");
            attValue.Value = string.Empty;
            element.SetAttributeNode(attValue);

            appSettings.AppendChild(element);
            xmlRet = root.SelectSingleNode(search);
         }
         return xmlRet;
      }

      public static string[] GetWorkstationDemos ( ) 
      {
         return new string [ ] { 
            "CSMedicalWorkstationMainDemo_Original.exe", 
            "CSMedicalWorkstationMainDemo.exe", 
            //"CSMedicalWorkstationSkinnedUIControls.exe",

            "VBMedicalWorkstationMainDemo.exe", 
            //"VBMedicalWorkstationSkinnedUIControls.exe",
         
         };
      }

      private DicomService ConfigureServer
      ( 
         ServerSettings settings,
         List<List<string>> addins,
         List<List<string>> configAddins,
         bool installDatabase,
         ServiceAdministrator serviceAdmin
         
      )
      {
         DicomService service = null ;
         // Install the service
         try
         {
            string sMsg = string.Format("Creating Server {0} ...", settings.AETitle);

            ShowMessage(sMsg);

            service = serviceAdmin.InstallService(settings);

            MyUtils.CopyAddIns(addins, service, "addins");

            MyUtils.CopyAddIns(configAddins, service, "Configuration");

            if (installDatabase)
            {
               string errorString;
               if (false == MyUtils.InitializeDatabase(service, out errorString))
               {
                   Messager.ShowError(this, errorString);
               }
            }

            ShowMessage(string.Empty);

            return service;
         }
         catch ( Exception ex )
         {
            if (service != null && !string.IsNullOrEmpty(service.ServiceName))
            {
               // ShowMessage("Exception: " + ex.Message, StatusType.Error);
               ShowMessage("Removing: " + service.ServiceName);
               MyUtils.UninstallOneDicomServer(service, false, serviceAdmin);
            }
            throw ;
         }
      }

      private void StartServer ( DicomService service)
      {
         bool checkboxChecked = false;

         if (string.Compare(service.ServiceName, textBoxServerAe.Text, true) == 0)
            checkboxChecked = checkBoxStartServer.Checked;
         else if (string.Compare(service.ServiceName, textBoxWsServerAe.Text, true) == 0)
            checkboxChecked = checkBoxStartWsServer.Checked;
         else if (string.Compare(service.ServiceName, textBoxStorageServerAe.Text, true) == 0)
            checkboxChecked = checkBoxStartStorageServer.Checked;
         else if (string.Compare(service.ServiceName, textBoxWorklistServerAe.Text, true) == 0)
            checkboxChecked = checkBoxStartWorklistServer.Checked;

         // We are currently hiding the checkbox options, so always start the service
         checkboxChecked = true;

         // Install the service
         if (checkboxChecked)
         {
            string sMsg = string.Format("Starting Server {0} ...", service.Settings.AETitle);
            ShowMessage(sMsg);

            int count = 0;
            Exception e = null;

            // Try to start the service three times
            while ( count < 3 )
            {
               try
               {
                  service.Start ( ) ;
                  break ;
               }
               catch ( Exception ex )
               {
                  if ( count == 2 )
                  {
                     e = ex ;
                  }
               }
               
               count++ ;
            }

            if (e != null)
            {
               throw e ;
            }
         }
         
         ShowMessage ( string.Empty ) ;
      }

      // Functions for configuring the DICOM High Level Client demos
      public void ConfigureClients
      ( 
         DicomService [] services,
         string defaultStoreServer,
         string defaultImagesQueryServer,
         string defaultMwlServer,
         string defaultMppsServer,
         string workstationServer,
         string highLevelStorageServer,
         string clientAe,
         string clientIp,
         int clientPort,
         string[] demos
      ) 
      {
         // Try to configure all the clients.
         // Configure original clients, rebuilt clients, 32-bit and 64-bit
         foreach ( string client in demos ) 
         {
            DicomDemoSettings settings = null;

            if (checkBoxResetClientConfigurations.Checked)
               settings = new DicomDemoSettings(); 
            else 
               settings = DicomDemoSettingsManager.LoadSettings(client);
            
            if ( null == settings ) 
            {
               settings = new DicomDemoSettings ( ) ;
            }
         
            foreach ( DicomService service in services ) 
            {
               DicomAE server = settings.ServerList.FirstOrDefault (  n=> n.AE == service.Settings.AETitle ) ;
               
               if ( null == server )
               {
                  settings.ServerList.Add ( new DicomAE ( service.Settings.AETitle, 
                                                          service.Settings.IpAddress, 
                                                          service.Settings.Port, 
                                                          service.Settings.ReconnectTimeout, 
                                                          service.Settings.Secure ) ) ;
               }
               else
               {
                  server.IPAddress = service.Settings.IpAddress ;
                  server.Port      = service.Settings.Port ;
                  server.Timeout   = service.Settings.ReconnectTimeout ;
                  server.UseTls    = service.Settings.Secure ;
               }
            }
            
            settings.ClientAe.AE        = clientAe ;
            settings.ClientAe.IPAddress = clientIp ; 
            settings.ClientAe.Port      = clientPort ;
            settings.ClientAe.Timeout   = 30 ;

            settings.DefaultStore       = defaultStoreServer ;
            settings.DefaultImageQuery  = defaultImagesQueryServer ;
            settings.DefaultMwlQuery    = defaultMwlServer ;
            settings.DefaultMpps        = defaultMppsServer ;
            settings.WorkstationServer  = workstationServer ;
            settings.HighLevelStorageServer = highLevelStorageServer;
            settings.IsPreconfigured    = true;
            settings.FirstRun           = true;
         
         
            DicomDemoSettingsManager.SaveSettings ( client, settings ) ;
            
            ShowMessage ( "   " + client ) ;
         }
      }

      public static string[] GetMainMwlDemos()
      {
         string[] clients;
         clients = new string[] { 
            "CSDicomHighLevelMWLScuDemo.exe",
            "CSDicomHighLevelMWLScuDemo_Original.exe",
            "VBDicomHighLevelMWLScuDemo.exe",
            
            "CSPrintToPACSDemo.exe",
            "CSPrintToPACSDemo_Original.exe",
            "VBPrintToPACSDemo.exe",
         };
         return clients;
      }

      public static string[] GetMainClientDemos()
      {
         string[] clients;
         clients = new string[] { 
            "CSDicomHighlevelClientDemo.exe", 
            "CSDicomHighlevelClientDemo_Original.exe",

            "VBDicomHighlevelClientDemo.exe", 
            
            "CSStorageServerManagerDemo_Original.exe",
            "CSStorageServerManagerDemo.exe",
            "VBStorageServerManagerDemo.exe",
         };
         return clients;
      }

      public static string[] GetMainStoreDemos()
      {
         string[] clients;
         clients = new string[] {
            "CSDicomHighlevelStoreDemo.exe", 
            "CSDicomHighlevelStoreDemo_Original.exe",
            
            "CSDicomHighlevelPatientUpdaterDemo.exe",
            "CSDicomHighlevelPatientUpdaterDemo_Original.exe",

            "VBDicomHighlevelStoreDemo.exe",
            "VBDicomHighlevelPatientUpdaterDemo.exe",

            "WebViewerConfiguration.exe",
            "WebViewerConfiguration_Original.exe",
         };
         return clients;
      }

      public static string[] GetOtherDemosWithConfigurationFiles()
      {
         string[] otherDemos = new string[] { 
            "CSLeadtools.Dicom.Server.Manager_Original.exe",
            "CSLeadtools.Dicom.Server.Manager.exe",
            "VBLeadtools.Dicom.Server.Manager.exe"
         };
         return otherDemos;
      }
      
            
      private bool VerifyPortsAreDifferent()
      {
         List<int> portList = new List<int>();

         NumericUpDown[] numericUpDownPortList = new NumericUpDown[]{
         this.numericUpDownServerPort,
         this.numericUpDownBrokerPort,
         this.numericUpDownWsServerPort,
         // this.numericUpDownRoutingServerPort,
         this.numericUpDownStorageServerPort,
         this.numericUpDownClientPort,
         this.numericUpDownWsClientPort
         };

         foreach (NumericUpDown numericUpDownPort in numericUpDownPortList)
         {
            int port = (int)numericUpDownPort.Value;
            if (portList.Contains(port))
            {
               string errorMessage = string.Format("Port {0} already assigned.  Choose a different port.", port);
               errorProvider.SetError(numericUpDownPort, errorMessage);
               return false;
            }
            else
            {
               portList.Add(port);
            }
         }
         return true;
      }

      private void Configure ( )
      {
         ServerSettings demoServerSettings = null;
         ServerSettings worklistServerSettings = null;
         ServerSettings storageServerSettings = null;
         ServerSettings wsServerSettings = null;
         // ServerSettings routingServerSettings = null;

         _serviceAdmin.Dispose();
         _serviceAdmin = Program.CreateServiceAdministrator();

         EnableDialogItems(false, _serviceAdmin);

         try
         {
            // Demo server
            demoServerSettings = new ServerSettings();
            demoServerSettings.Description = "LEADTOOLS PACS Framework Sample DICOM Server: " + textBoxServerAe.Text;
            demoServerSettings.ImplementationVersionName = "LT_PACS_DEMO";
            demoServerSettings.AETitle = textBoxServerAe.Text;
            demoServerSettings.IpAddress = _defaultIP;
            demoServerSettings.Port = (int)numericUpDownServerPort.Value;
            demoServerSettings.DisplayName = string.Empty;
            demoServerSettings.StartMode = "Manual";
            demoServerSettings.MaxPduLength = 16384;
            demoServerSettings.AllowMultipleConnections = true;

            if (IsServerAlreadyConfigured(demoServerSettings, _serviceAdmin))
               return;
               
            // Worklist server
            worklistServerSettings = new ServerSettings();
            worklistServerSettings.Description = "LEADTOOLS PACS DICOM Worklist Server";
            worklistServerSettings.ImplementationVersionName = "LT_PACS_WORKLIST";
            worklistServerSettings.AETitle = textBoxWorklistServerAe.Text;
            worklistServerSettings.IpAddress = _defaultIP;
            worklistServerSettings.Port = (int)numericUpDownWorklistServerPort.Value;
            worklistServerSettings.DisplayName = string.Empty;
            worklistServerSettings.StartMode = "Manual";
            worklistServerSettings.MaxPduLength = 16384;
            worklistServerSettings.AllowMultipleConnections = true;

            if (IsServerAlreadyConfigured(worklistServerSettings, _serviceAdmin))
               return;
               
            // Storage server
            storageServerSettings = new ServerSettings();
            storageServerSettings.Description = "LEADTOOLS PACS DICOM Storage Server";
            storageServerSettings.ImplementationVersionName = "LT_PACS_STORAGE";
            storageServerSettings.AETitle = textBoxStorageServerAe.Text;
            storageServerSettings.IpAddress = _defaultIP;
            storageServerSettings.Port = (int)numericUpDownStorageServerPort.Value;
            storageServerSettings.DisplayName = string.Empty;
            storageServerSettings.StartMode = "Manual";
            storageServerSettings.MaxPduLength = 16384;
            storageServerSettings.AllowMultipleConnections = true;

            if (IsServerAlreadyConfigured(storageServerSettings, _serviceAdmin))
               return;

            // Workstation Server
            wsServerSettings = new ServerSettings();
            wsServerSettings.Description = "LEADTOOLS PACS Framework Sample DICOM Server: " + textBoxWsServerAe.Text;
            wsServerSettings.ImplementationVersionName = "LT_PACS_WS";
            wsServerSettings.AETitle = textBoxWsServerAe.Text;
            wsServerSettings.IpAddress = _defaultIP;
            wsServerSettings.Port = (int)numericUpDownWsServerPort.Value;
            wsServerSettings.DisplayName = string.Empty;
            wsServerSettings.StartMode = "Automatic";
            wsServerSettings.MaxPduLength = 16384;
            wsServerSettings.AllowMultipleConnections = true;

            if (IsServerAlreadyConfigured(wsServerSettings, _serviceAdmin))
               return;

            //// Routing Server
            //routingServerSettings = new ServerSettings();
            //routingServerSettings.Description = "LEADTOOLS PACS Routing DICOM Server: " + textBoxRoutingServerAe.Text;
            //routingServerSettings.ImplementationVersionName = "LT_PACS_ROUTER";
            //routingServerSettings.AETitle = textBoxRoutingServerAe.Text;
            //routingServerSettings.IpAddress = _defaultIP;
            //routingServerSettings.Port = (int)numericUpDownRoutingServerPort.Value;
            //routingServerSettings.DisplayName = string.Empty;
            //routingServerSettings.StartMode = "Manual";
            //routingServerSettings.MaxPduLength = 46726;
            //routingServerSettings.AllowMultipleConnections = true;

            // if (IsServerAlreadyConfigured(routingServerSettings, _serviceAdmin))
            //    return;

            if (ValidateAll(_serviceAdmin))
            {
               if (DoConfigure(demoServerSettings, storageServerSettings, wsServerSettings, null /*routingServerSettings*/, worklistServerSettings, _serviceAdmin))
               {
                  DoConfigureAdvancedConfig(demoServerSettings, null /*routingServerSettings*/, wsServerSettings);
                  // DoConfigureRouterScript(demoServerSettings, routingServerSettings, wsServerSettings);

                  DoConfigureRulesScripts(demoServerSettings, storageServerSettings);

#if LEADTOOLS_V175_OR_LATER
                  // If installing the Broker, then configure the broker
                  if (checkBoxBrokerHost.Checked == true)
                  {
                     DoConfigureBrokerConfig(worklistServerSettings, null, null);
                     DoConfigureWcfClientConfig(worklistServerSettings, null, null);
                  }
#endif
               }
            }
         }
         finally
         {
            EnableDialogItems(true, _serviceAdmin);
         }
      }

      public static XmlDocument GetEmbeddedXml(string resourceFileName)
      {
         XmlDocument xml = null;

         try
         {
            string fullname = MyUtils.GetResourceFullName(resourceFileName);
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fullname);
            XmlTextReader tr = new XmlTextReader(stream);
            xml = new XmlDocument();
            xml.Load(tr);
         }
         catch (Exception ex)
         {
            throw ex;
         }
         return xml;
      }
            
      private void DoConfigureRulesScripts(ServerSettings demoServerSettings, ServerSettings storageServerSettings)
      {
         try
         {
            // Get the first script
            XmlDocument xmlDocument = GetEmbeddedXml(@"RulesScripts\RouteToWS.rule");
            XmlNode root = xmlDocument.DocumentElement;
            if (root != null)
            {
               // StoreProxy Settings
               XmlNode xmlNodeRouterServer = root.SelectSingleNode("//ServerRule");
               if (xmlNodeRouterServer != null)
               {
                  XmlNode xmlNode = xmlNodeRouterServer.SelectSingleNode("Script");
                  if (xmlNode != null)
                  {
                     xmlNode.InnerText = xmlNode.InnerText.Replace("DEMO_SERVER", demoServerSettings.AETitle);
                  }
               }

               // Save the 'RouteToWS.rule' file in the Router sub-directory
               string baseDirectory = MyUtils.MyCombine(Program._baseDir, storageServerSettings.AETitle, @"AddIns\rules\SendCStoreResponse");
               Directory.CreateDirectory(baseDirectory);
               string fileName = Path.Combine(baseDirectory, @"RouteToPACS.rule");
               xmlDocument.Save(fileName);
               
               // Get the second script
               xmlDocument = GetEmbeddedXml(@"RulesScripts\AddInstitutionName.rule");
               baseDirectory = MyUtils.MyCombine(Program._baseDir, storageServerSettings.AETitle, @"AddIns\rules\ReceiveCStoreRequest");
               Directory.CreateDirectory(baseDirectory);
               fileName = Path.Combine(baseDirectory, @"AddInstitutionName.rule");
               xmlDocument.Save(fileName);
            }
         }
         catch (Exception ex)
         {
            throw ex;
         }
      }
      

      //private void DoConfigureRouterScript(ServerSettings demoServerSettings, ServerSettings routingServerSettings, ServerSettings wsServerSettings)
      //{
      //   try
      //   {
      //      XmlDocument xmlDocument = GetEmbeddedXml("RouteToWS.rule");
      //      XmlNode root = xmlDocument.DocumentElement;
      //      if (root != null)
      //      {
      //         // StoreProxy Settings
      //         XmlNode xmlNodeRouterServer = root.SelectSingleNode("//RouterScript/Servers/RouterServer");
      //         if (xmlNodeRouterServer != null)
      //         {
      //            XmlNode xmlNode = xmlNodeRouterServer.SelectSingleNode("AETitle");
      //            if (xmlNode != null)
      //               xmlNode.InnerText = wsServerSettings.AETitle;

      //            xmlNode = xmlNodeRouterServer.SelectSingleNode("IPHostname");
      //            if (xmlNode != null)
      //               xmlNode.InnerText = wsServerSettings.IpAddress;

      //            xmlNode = xmlNodeRouterServer.SelectSingleNode("Port");
      //            if (xmlNode != null)
      //               xmlNode.InnerText = wsServerSettings.Port.ToString();

      //            xmlNode = xmlNodeRouterServer.SelectSingleNode("Timeout");
      //            if (xmlNode != null)
      //               xmlNode.InnerText = wsServerSettings.ReconnectTimeout.ToString();

      //            xmlNode = xmlNodeRouterServer.SelectSingleNode("Certificate");
      //            if (xmlNode != null)
      //               xmlNode.InnerText = DicomDemoSettingsManager.GetClientCertificateFullPath();

      //            xmlNode = xmlNodeRouterServer.SelectSingleNode("PrivateKey");
      //            if (xmlNode != null)
      //               xmlNode.InnerText = DicomDemoSettingsManager.GetClientCertificateFullPath();

      //            xmlNode = xmlNodeRouterServer.SelectSingleNode("KeyPassword");
      //            if (xmlNode != null)
      //               xmlNode.InnerText = DicomDemoSettingsManager.GetClientCertificatePassword();

      //         }

      //         // Save the 'RouteToWS.rule' file in the Router sub-directory
      //         string baseDirectory = Program._baseDir + @"\" + routingServerSettings.AETitle + @"\Router\Scripts\";
      //         Directory.CreateDirectory(baseDirectory);
      //         string fileName = baseDirectory + @"RouteToWS.rule";
      //         xmlDocument.Save(fileName);
      //      }
      //   }
      //   catch (Exception ex)
      //   {
      //      throw ex;
      //   }
      //}

#if LEADTOOLS_V175_OR_LATER
      private void DoConfigureBrokerConfig(ServerSettings demoServerSettings, ServerSettings routingServerSettingsNotUsed, ServerSettings wsServerSettings)
      {
         try
         {
            XmlDocument xmlDocument = GetEmbeddedXml("broker.config");
            XmlNode root = xmlDocument.DocumentElement;
            if (root != null)
            {
               // StoreProxy Settings
               XmlNode xmlNodebaseAddress = root.SelectSingleNode("//configuration/system.serviceModel/services/service/host/baseAddresses");
               if (xmlNodebaseAddress != null)
               {
                  XmlNode xmlNode = xmlNodebaseAddress.SelectSingleNode("add");
                  if (xmlNode != null)
                  {
                     XmlElement xmlElement = (XmlElement)xmlNode;
                     if (xmlElement != null)
                     {
                        string address = string.Format("http://localhost:{0}/Design_Time_Addresses/Leadtools.Medical.Worklist.Wcf.Service/BrokerService/",
                          (int)numericUpDownBrokerPort.Value);
                        xmlElement.SetAttribute("baseAddress", address);
                     }
                  }

                  // Save the 'broker.config' file in the Main Server sub-directory
                  string baseDirectory = Program._baseDir + @"\" + demoServerSettings.AETitle + @"\";
                  Directory.CreateDirectory(baseDirectory);

                  string fileName = baseDirectory + @"broker.config";

                  xmlDocument.Save(fileName);
               }
            }
         }
         catch (Exception ex)
         {
            throw (ex);
         }
      }

      private void DoConfigureWcfClientConfig(ServerSettings demoServerSettings, ServerSettings routingServerSettingsNotUsed, ServerSettings wsServerSettings)
      {
         try
         {
            XmlDocument xmlDocument = GetEmbeddedXml("wcfClient.config");
            XmlNode root = xmlDocument.DocumentElement;
            if (root != null)
            {
               // StoreProxy Settings
               XmlNode xmlNodebaseAddress = root.SelectSingleNode("//configuration/system.serviceModel/client");
               if (xmlNodebaseAddress != null)
               {
                  XmlNode xmlNode = xmlNodebaseAddress.SelectSingleNode("endpoint");
                  if (xmlNode != null)
                  {
                     XmlElement xmlElement = (XmlElement)xmlNode;
                     if (xmlElement != null)
                     {
                        string address = string.Format("http://localhost:{0}/Design_Time_Addresses/Leadtools.Medical.Worklist.Wcf.Service/BrokerService/",
                          (int)numericUpDownBrokerPort.Value);
                        xmlElement.SetAttribute("address", address);
                     }
                  }

                  // Save the 'wcfClient.config' file in the Public Documents sub-directory
                  // string baseDirectory = Program._baseDir + @"\" + mainServerSettings.AETitle + @"\";
                  string baseDirectory = DicomDemoSettingsManager.GetFolderPath();
                  Directory.CreateDirectory(baseDirectory);

                  //string fileName = Path.Combine(baseDirectory, @"wcfClient.config"); ;
                  string fileName = DicomDemoSettingsManager.GetSettingsFilename("WCFClient");

                  xmlDocument.Save(fileName);
               }
            }
         }
         catch (Exception ex)
         {
            throw (ex);
         }
      }
#endif

      private void DoConfigureAdvancedConfig(ServerSettings demoServerSettings, ServerSettings routingServerSettingsNotUsed, ServerSettings wsServerSettings)
      {
         try
         {
            XmlDocument xmlDocument = GetEmbeddedXml("advanced.config");
            XmlNode root = xmlDocument.DocumentElement;
            if (root != null)
            {
               // StoreProxy Settings
               XmlNode xmlNodeStoreProxy = root.SelectSingleNode("//configuration/advancedSettings/addins/addin/customData/custom/ProxyOptions/Server");
               if (xmlNodeStoreProxy != null)
               {
                  XmlNode xmlNode = xmlNodeStoreProxy.SelectSingleNode("AETitle");
                  if (xmlNode != null)
                     xmlNode.InnerText = demoServerSettings.AETitle;

                  xmlNode = xmlNodeStoreProxy.SelectSingleNode("IPHostname");
                  if (xmlNode != null)
                     xmlNode.InnerText = demoServerSettings.IpAddress;

                  xmlNode = xmlNodeStoreProxy.SelectSingleNode("Port");
                  if (xmlNode != null)
                     xmlNode.InnerText = demoServerSettings.Port.ToString();

                  xmlNode = xmlNodeStoreProxy.SelectSingleNode("Timeout");
                  if (xmlNode != null)
                     xmlNode.InnerText = demoServerSettings.ReconnectTimeout.ToString();

                  xmlNode = xmlNodeStoreProxy.SelectSingleNode("Certificate");
                  if (xmlNode != null)
                     xmlNode.InnerText = DicomDemoSettingsManager.GetClientCertificateFullPath();

                  xmlNode = xmlNodeStoreProxy.SelectSingleNode("PrivateKey");
                  if (xmlNode != null)
                     xmlNode.InnerText = DicomDemoSettingsManager.GetClientCertificateFullPath();

                  xmlNode = xmlNodeStoreProxy.SelectSingleNode("KeyPassword");
                  if (xmlNode != null)
                     xmlNode.InnerText = DicomDemoSettingsManager.GetClientCertificatePassword();
               }

               // Router Settings
               //XmlNode xmlNodeRouter = root.SelectSingleNode("//configuration/advancedSettings/addins/addin/customData/custom/RouterOptions");
               //if (xmlNodeRouter != null)
               //{
               //   XmlNode xmlNode = xmlNodeRouter.SelectSingleNode("ScriptDirectory");
               //   if (xmlNode != null)
               //      xmlNode.InnerText = Program._baseDir + @"\" + routingServerSettings.AETitle + @"\Router\Scripts\";

               //   xmlNode = xmlNodeRouter.SelectSingleNode("FailureDirectory");
               //   if (xmlNode != null)
               //      xmlNode.InnerText = Program._baseDir + @"\" + routingServerSettings.AETitle + @"\Router\Failures\";

               //   xmlNode = xmlNodeRouter.SelectSingleNode("ImageDirectory");
               //   if (xmlNode != null)
               //      xmlNode.InnerText = Program._baseDir + @"\" + routingServerSettings.AETitle + @"\Router\Images\";

               //   xmlNode = xmlNodeRouter.SelectSingleNode(@"Scu/AETitle");
               //   if (xmlNode != null)
               //      xmlNode.InnerText = routingServerSettings.AETitle;
               //}

               //// Save the 'advanced.config' file in the Router sub-directory
               //string baseDirectory = Program._baseDir + @"\" + routingServerSettings.AETitle + @"\";
               //Directory.CreateDirectory(baseDirectory);

               //string fileName = baseDirectory + @"advanced.config";

               //xmlDocument.Save(fileName);
            }
         }
         catch (Exception ex)
         {
            throw (ex);
         }
      }

      private const uint ERROR_SERVICE_EXISTS =           1073;

      private string DoConfigureOne(ServiceAdministrator serviceAdmin, List<DicomService> services, ServerSettings serverSettings, List<List<string>> serverAddins, List<List<string>> serverConfigAddins, TextBox tbServerAe)
      {
         string ret = null;
         if (serverSettings == null)
            return null;

         if (serverAddins == null || serverAddins.Count == 0)
            return null;

         DicomService newService = null;
         try
         {
            bool bIsDemoServer = (tbServerAe.Name == "textBoxServerAe");
            newService = ConfigureServer(serverSettings, serverAddins, serverConfigAddins, bIsDemoServer, serviceAdmin);
            if (null != newService)
            {
               services.Add(newService);
               ret = newService.Settings.AETitle;
            }
         }
         catch (Win32Exception win32Exception)
         {
            if (win32Exception.Message.Contains("The specified service already exists") || (win32Exception.NativeErrorCode == ERROR_SERVICE_EXISTS))
            {
               string sMsg = string.Format("The Server AE \"{0}\" already exists.  Please enter a different Server AE", serverSettings.AETitle);
               ShowError(tbServerAe, sMsg);
            }
            else
            {
               if (newService != null)
                  serviceAdmin.SafeUnInstallService(newService);
            }
            throw;
         }
         catch (Exception)
         {
            if (newService != null)
               serviceAdmin.SafeUnInstallService(newService);
            throw;
         }
         return ret;
      }

      private bool DoConfigure 
      ( 
         ServerSettings demoServerSettings, 
         ServerSettings storageServerSettings, 
         ServerSettings wsServerSettings,
         ServerSettings routingServerSettings,
         ServerSettings worklistServerSettings, 
         ServiceAdministrator serviceAdmin
      )
      {
         bool ret = true;
         string defaultStoreServer = string.Empty;
         string defaultImagesQueryServer = string.Empty;
         string defaultMwlServer = string.Empty;
         string defaultMppsServer = string.Empty;
         string workstationServer = string.Empty;
         string highLevelStorageServer = string.Empty;

         List<DicomService> services = new List<DicomService>();

         try
         {
            // Configure the demo server
            string aeTitle = DoConfigureOne(serviceAdmin, services, demoServerSettings, GetDemoServerAddins(), GetDemoServerConfigurationAddins(), textBoxServerAe);
            if (!string.IsNullOrEmpty(aeTitle))
            {
               defaultStoreServer = aeTitle;
               defaultImagesQueryServer = aeTitle;
            }
            
            // Configure the Storage Server
            aeTitle = DoConfigureOne(serviceAdmin, services, storageServerSettings, GetStorageServerAddins(), GetStorageServerConfigurationAddins(), textBoxStorageServerAe);
            if (!string.IsNullOrEmpty(aeTitle))
            {
               defaultStoreServer = aeTitle;
               defaultImagesQueryServer = aeTitle;
               // defaultMwlServer = aeTitle;
               // defaultMppsServer = aeTitle;
               highLevelStorageServer = aeTitle;
            }
            ConfigureStorageServerService(storageServerSettings);
            ConfigureStorageServerClients();
            GlobalPacsUpdater.ModifyGlobalPacsConfiguration(DicomDemoSettingsManager.ProductNameStorageServer, storageServerSettings.ServiceName, GlobalPacsUpdater.ModifyConfigurationType.Add);

            aeTitle = DoConfigureOne(serviceAdmin, services, wsServerSettings, GetWorkstationServerAddins(), GetWorkstationConfigurationAddins(), textBoxWsServerAe);
            if (!string.IsNullOrEmpty(aeTitle))
            {
               workstationServer = aeTitle;
               if (string.IsNullOrEmpty(defaultStoreServer))
                  defaultStoreServer = aeTitle;

               if (string.IsNullOrEmpty(defaultImagesQueryServer))
                  defaultImagesQueryServer = aeTitle;
            }
            GlobalPacsUpdater.ModifyGlobalPacsConfiguration(DicomDemoSettingsManager.ProductNameWorkstation, wsServerSettings.ServiceName, GlobalPacsUpdater.ModifyConfigurationType.Add);

            // Configure clients
            //MyUtils.AddAeTitle(textBoxClientAe.Text, _defaultIP, (short)numericUpDownClientPort.Value);
            ShowMessage("Configuring PACS Framework Clients:");
            
            ConfigureClients(services.ToArray(),
                               defaultStoreServer,
                               defaultImagesQueryServer,
                               defaultMwlServer,
                               defaultMppsServer,
                               workstationServer,
                               highLevelStorageServer,
                               textBoxClientAe.Text,
                               _defaultIP,
                               (int)numericUpDownClientPort.Value,
                               GetMainClientDemos());


            //MyUtils.AddAeTitle(textBoxWsClientAe.Text, _defaultIP, (short)numericUpDownWsClientPort.Value);
            ShowMessage("Configuring Workstation Client demos:");
            ConfigureClients(services.ToArray(),
                               defaultStoreServer,
                               defaultImagesQueryServer,
                               defaultMwlServer,
                               defaultMppsServer,
                               workstationServer,
                               highLevelStorageServer,
                               textBoxWsClientAe.Text,
                               _defaultIP,
                               (int)numericUpDownWsClientPort.Value,
                               GetWorkstationDemos());

            // Configure the router
            // aeTitle = DoConfigureOne(serviceAdmin, services, routingServerSettings, GetRoutingServerAddins(), null, textBoxRoutingServerAe);

            // Configure the store demo
            ShowMessage("Configuring PACS Framework Store Demos:");
            ConfigureClients(services.ToArray(),
                               defaultStoreServer,
                               defaultImagesQueryServer,
                               defaultMwlServer,
                               defaultMppsServer,
                               workstationServer,
                               highLevelStorageServer,
                               textBoxClientAe.Text,
                               _defaultIP,
                               (int)numericUpDownClientPort.Value,
                               GetMainStoreDemos());
                               
            // Configure the worklist server
            aeTitle = DoConfigureOne(serviceAdmin, services, worklistServerSettings, GetWorklistServerAddins(), GetWorklistServerConfigurationAddins(), textBoxWorklistServerAe);
            if (!string.IsNullOrEmpty(aeTitle))
            {
               defaultMwlServer = aeTitle;
               defaultMppsServer = aeTitle;
            }
            GlobalPacsUpdater.ModifyGlobalPacsConfiguration(DicomDemoSettingsManager.ProductNameDemoServer, worklistServerSettings.ServiceName, GlobalPacsUpdater.ModifyConfigurationType.Add);


            // Configure the MWL-SCU demo
            ShowMessage("Configuring PACS Framework MWL-SCU demo:");
            ConfigureClients(services.ToArray(),
                               defaultStoreServer,
                               defaultImagesQueryServer,
                               defaultMwlServer,
                               defaultMppsServer,
                               workstationServer,
                               highLevelStorageServer,
                               textBoxClientAe.Text,
                               _defaultIP,
                               (int)numericUpDownClientPort.Value,
                               GetMainMwlDemos());



            foreach (DicomService service in services)
            {
               MyUtils.ClearAeTitles();
               MyUtils.AddAeTitle(textBoxClientAe.Text, _defaultIP, (short)numericUpDownClientPort.Value);
               MyUtils.AddAeTitle(textBoxWsClientAe.Text, _defaultIP, (short)numericUpDownWsClientPort.Value);
               MyUtils.AddAeTitle(textBoxStorageServerAe.Text, _defaultIP, (short)numericUpDownStorageServerPort.Value);
               MyUtils.AddAeTitle(textBoxWorklistServerAe.Text, _defaultIP, (short)numericUpDownWorklistServerPort.Value);

               // Workstation server: add the main server as a client
               // Add the Router as a client as well
               if (string.Compare(service.Settings.AETitle, textBoxWsServerAe.Text, true) == 0)
               {
                  MyUtils.AddAeTitle(textBoxServerAe.Text, _defaultIP, (short)numericUpDownServerPort.Value);
                  // MyUtils.AddAeTitle(textBoxRoutingServerAe.Text, _defaultIP, (short)numericUpDownRoutingServerPort.Value);
               }

               // Main server: add the workstation server as a client
               else if (string.Compare(service.Settings.AETitle, textBoxServerAe.Text, true) == 0)
               {
                  MyUtils.AddAeTitle(textBoxWsServerAe.Text, _defaultIP, (short)numericUpDownWsServerPort.Value);
               }

               MyUtils.WriteAeTitlesXml(service);
               StartServer(service);
            }


            ShowMessage("Verifying Services ...");
            bool servicesValid = VerifyServers(5000, false);

            if (servicesValid)
            {
               ShowMessage("Configuration is successful!", StatusType.Check);
            }

            return true;
         }
         catch (Exception exception)
         {
             string message = exception.Message;
             if (message.Contains("The specified service has been marked for deletion"))
             {
                 string sourceName = Assembly.GetExecutingAssembly().GetName().Name;
                 message = message + string.Format(".  Please close and restart {0}", sourceName);
             }
            ret = false;
            ShowError(null, message);
            UninstallServers(serviceAdmin, services);
         }
         return ret;
      }

      private void UninstallServers(ServiceAdministrator serviceAdmin, List<DicomService>services)
      {
         try
         {
            foreach (DicomService service in services)

               if (service != null)
               {
                  ShowMessage("Removing: " + service.ServiceName);
                  MyUtils.UninstallOneDicomServer(service, false, serviceAdmin);

               }
         }
         catch (Exception)
         {
         }
      }

      private static void AddOneAddin( List<List<string>> lists, string fullPath)
      {
         List<string> addinList = new List<string>();
         addinList.Add(fullPath);
         lists.Add(addinList);
      }

      private static void AddOneAddin( List<List<string>> lists, string[] fullPathArray)
      {
         List<string> addinList = new List<string>();
         foreach(string s in fullPathArray)
         {
            addinList.Add(s);
         }
         lists.Add(addinList);
      }

      private List<List<string>> GetDemoServerAddins()
      {
         string addinsDir = Path.Combine(Program._baseDir, @"PACSAddIns");

         List<List<string>> addinsLists = new List<List<string>>();

         AddOneAddin(addinsLists, Path.Combine(addinsDir, "Leadtools.AddIn.Find.dll"));
         AddOneAddin(addinsLists, Path.Combine(addinsDir, "Leadtools.AddIn.Move.dll"));
         AddOneAddin(addinsLists, Path.Combine(addinsDir, "Leadtools.AddIn.Security.dll"));
         AddOneAddin(addinsLists, Path.Combine(addinsDir, "Leadtools.AddIn.StorageCommit.dll"));
         AddOneAddin(addinsLists, Path.Combine(addinsDir, "Leadtools.AddIn.Store.dll"));
         return addinsLists;
      }
      
      private List<List<string>> GetDemoServerConfigurationAddins()
      {
         string addinsDir = Path.Combine(Program._baseDir, @"PACSAddIns");
         List<List<string>> addinsLists = new List<List<string>>();

         // Note this is not in the PACSAddins folder
         string loggingFirstLocation = Path.Combine(addinsDir, "Leadtools.Configuration.Logging.dll");
         string loggingSecondLocation = Path.Combine(Program._baseDir, "Leadtools.Configuration.Logging.dll");
         AddOneAddin(addinsLists, new string[] {loggingFirstLocation, loggingSecondLocation});
         return addinsLists;
      } 
      
      private List<List<string>> GetWorklistServerAddins()
      {
         string addinsDir = Path.Combine(Program._baseDir, @"PACSAddIns");

         List<List<string>> addinsLists = new List<List<string>>();
         AddOneAddin(addinsLists, Path.Combine(Program._baseDir, "Leadtools.Medical.Worklist.AddIns.dll"));
         
#if LEADTOOLS_V175_OR_LATER
         if (checkBoxBrokerHost.Checked == true)
            AddOneAddin(addinsLists, Path.Combine(addinsDir , "Leadtools.AddIn.Broker.Host.dll"));
#endif
         AddOneAddin(addinsLists, Path.Combine(addinsDir, "Leadtools.AddIn.Security.dll"));

#if LEADTOOLS_V19_OR_LATER
         AddOneAddin(addinsLists, Path.Combine(Program._baseDir, "Leadtools.Medical.HL7MWL.AddIn.dll"));
#endif // #if LEADTOOLS_V19_OR_LATER
         
         return addinsLists;
      }
      
      private List<List<string>> GetWorklistServerConfigurationAddins()
      {
         string addinsDir = Path.Combine(Program._baseDir, @"PACSAddIns");
         List<List<string>> addinsLists = new List<List<string>>();

         // Note this is not in the PACSAddins folder
         string loggingFirstLocation = Path.Combine(addinsDir, "Leadtools.Configuration.Logging.dll");
         string loggingSecondLocation = Path.Combine(Program._baseDir, "Leadtools.Configuration.Logging.dll");
         AddOneAddin(addinsLists, new string[] { loggingFirstLocation, loggingSecondLocation });
         return addinsLists;
      }
      
      private List<List<string>> GetStorageServerAddins()
      {
         string addinsDir = Path.Combine(Program._baseDir, @"PACSAddIns");

         List<List<string>> addinsLists = new List<List<string>>();
         // addins.Add(addinsDir + "Leadtools.Medical.Forwarder.AddIn.dll");

         // Note this is not in the PACSAddins folder
         AddOneAddin(addinsLists, Path.Combine(Program._baseDir, "Leadtools.Medical.AutoCopy.AddIn.dll"));
         AddOneAddin(addinsLists, Path.Combine(Program._baseDir, "Leadtools.Medical.PatientUpdater.AddIn.dll"));
         AddOneAddin(addinsLists, Path.Combine(Program._baseDir, "Leadtools.Medical.Storage.AddIns.dll"));
         AddOneAddin(addinsLists, Path.Combine(Program._baseDir, "Leadtools.Medical.Forwarder.AddIn.dll"));
         //  AddOneAddin(addinsLists,Path.Combine(Program._baseDir, "Leadtools.Medical.Worklist.AddIns.dll"));
         //  AddOneAddin(addinsLists,Path.Combine(Program._baseDir, "Leadtools.Medical.Media.AddIns.dll"));
         AddOneAddin(addinsLists, Path.Combine(Program._baseDir, "Leadtools.Medical.Rules.AddIn.dll"));

#if LEADTOOLS_V19_OR_LATER
         // External Store Base Addin
         AddOneAddin(addinsLists, Path.Combine(addinsDir, "Leadtools.Medical.ExternalStore.Addin.dll"));

         // Atmos External Store Addin
         AddOneAddin(addinsLists, Path.Combine(Program._baseDir, "EsuApiLib.dll"));
         AddOneAddin(addinsLists, Path.Combine(addinsDir, "Leadtools.Medical.ExternalStore.Atmos.Addin.dll"));

         // Azure External Store Addin
         //  AddOneAddin(addinsLists,Path.Combine(Program._baseDir, "Microsoft.WindowsAzure.Storage.dll"));
#if FOR_DOTNET4
         AddOneAddin(addinsLists, Path.Combine(addinsDir, "Leadtools.Medical.ExternalStore.Azure.Addin.dll"));
#endif

         // LEAD Sample External Store Addin
         AddOneAddin(addinsLists, Path.Combine(addinsDir, "Leadtools.Medical.ExternalStore.Sample.Addin.dll"));
         AddOneAddin(addinsLists, Path.Combine(Program._baseDir, "Leadtools.Medical.HL7PatientUpdate.AddIn.dll"));
         AddOneAddin(addinsLists, Path.Combine(addinsDir, "Leadtools.Medical.SearchOtherPatientIds.Addin.dll"));
#endif // #if LEADTOOLS_V19_OR_LATER

         AddOneAddin(addinsLists, Path.Combine(Program._baseDir, "Leadtools.Medical.Security.Addin.dll"));

#if LEADTOOLS_V20_OR_LATER
         AddOneAddin(addinsLists, Path.Combine(addinsDir, "Leadtools.Medical.PatientRestrict.AddIn.dll"));

         // AmazonS3 External Store Addin
         AddOneAddin(addinsLists, Path.Combine(Program._baseDir, "AWSSDK.Core.dll"));
         AddOneAddin(addinsLists, Path.Combine(Program._baseDir, "AWSSDK.S3.dll"));
         AddOneAddin(addinsLists, Path.Combine(addinsDir, "Leadtools.Medical.ExternalStore.AmazonS3.Addin.dll"));

         // ExportLayout Addin
         AddOneAddin(addinsLists, Path.Combine(addinsDir, "Leadtools.Medical.ExportLayout.AddIn.dll"));
#endif


         return addinsLists;
      }
      
      private List<List<string>> GetStorageServerConfigurationAddins()
      {
         string addinsDir = Program._baseDir + @"\Configuration\";
         List<List<string>> addinsLists = new List<List<string>>();

         // Note this is not in the PACSAddins folder
         AddOneAddin(addinsLists, Path.Combine(Program._baseDir, "Leadtools.Medical.Ae.Configuration.dll"));
         AddOneAddin(addinsLists, Path.Combine(Program._baseDir, "Leadtools.Medical.Logging.Addin.dll"));
         AddOneAddin(addinsLists, Path.Combine(Program._baseDir, "Leadtools.Medical.License.Configuration.dll"));
         return addinsLists;
      }
      

      private List<List<string>> GetWorkstationServerAddins ( )
      {
         string storageAddIn = Path.Combine ( Program._baseDir, "Leadtools.Medical.Storage.AddIns.dll" ) ;
         string mediaAddIn = Path.Combine ( Program._baseDir, "Leadtools.Medical.Media.AddIns.dll" ) ;
         List<List<string>> addinsLists = new List<List<string>>();

         if ( File.Exists(mediaAddIn))
         {
            AddOneAddin(addinsLists, mediaAddIn);
         }
         AddOneAddin(addinsLists, storageAddIn);
         AddOneAddin(addinsLists, Path.Combine(Program._baseDir, "Leadtools.Medical.Security.Addin.dll"));

         return addinsLists;
      }
      
      private List<List<string>> GetWorkstationConfigurationAddins()
      {
         string addinsDir = Program._baseDir + @"\Configuration\";
         List<List<string>> addinsLists = new List<List<string>>();

         // Note this is not in the PACSAddins folder
         AddOneAddin(addinsLists, Path.Combine(Program._baseDir, "Leadtools.Medical.Logging.Addin.dll"));
         return addinsLists;
      }

      private void showHelpToolStripMenuItem_Click(object sender, EventArgs e)
      {
         HelpDialog dlg = new HelpDialog();
         dlg.ShowDialog();
      }

      private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
      {
         //string sourceName = Assembly.GetExecutingAssembly().GetName().Name;
         //EventLog.WriteEntry(sourceName, "This is a test");

         // To test the exception handling:
         // 1
         //throw new ArgumentException("This is a UI exception");


         // 2
         //ThreadStart newThreadStart = new ThreadStart(newThread_Execute);
         //Thread newThread = new Thread(newThreadStart);
         //newThread.Start();

         AboutDialog dlg = new AboutDialog("PACS Configuration");
         dlg.ShowDialog(this);
      }



      //// The thread we start up to demonstrate non-UI exception handling. 
      //void newThread_Execute()
      //{
      //   throw new Exception("This is a thread exception.");
      //}

      private void AllConfigure()
      {
         ClearErrors();
         if (!GlobalPacsUpdater.IsProductDatabaseUpTodate(DicomDemoSettingsManager.ProductNameStorageServer))
         {
            const string upgradeMessage = "Upgrading the Storage Server database ...";
            ShowMessage(upgradeMessage);
            GlobalPacsUpdater.UpgradeProductDatabase(DicomDemoSettingsManager.ProductNameStorageServer);
             if (!GlobalPacsUpdater.IsProductDatabaseUpTodate(DicomDemoSettingsManager.ProductNameStorageServer))
             {
                ShowError(null, "Configuration failed --Error upgrading the Storage Server database.");
             }
         }

         try
         {
            Cursor = Cursors.WaitCursor;

            if (false == VerifyPortsAreDifferent())
               return;

            Configure();
         }
         catch (Exception exception)
         {
            ShowError(null, exception.Message);
         }
         finally
         {
            Cursor = Cursors.Default;
         }
      }

      private void configureToolStripMenuItem_Click(object sender, EventArgs e)
      {
         AllConfigure();
      }

      private void buttonConfigure_Click(object sender, EventArgs e)
      {
         _showHelp = true;
         AllConfigure();
         EnableDialogItems(true, _serviceAdmin);
      }


      private void Remove ( )
      {
         try
         {
            ClearErrors();
            RemoveDialog dlg = new RemoveDialog();
            dlg.ServiceAdmin = _serviceAdmin;
            DialogResult dr = dlg.ShowDialog();

            if (null != dlg.UninstalledServers && dlg.UninstalledServers.Length > 0)
            {
               RemoveServersFromDemosSettings(GetMainClientDemos(), dlg.UninstalledServers);
               RemoveServersFromDemosSettings(GetMainStoreDemos(), dlg.UninstalledServers);
               RemoveServersFromDemosSettings(GetWorkstationDemos(), dlg.UninstalledServers);
               RemoveServersFromDemosSettings(GetMainMwlDemos(), dlg.UninstalledServers);
            }
         }
         catch ( Exception exception ) 
         {
            ShowError ( null, exception.Message ) ;
         }

      }

      private void RemoveServersFromDemosSettings ( string [] mainClientDemos, DicomAE[] servers )
      {
         
         foreach ( string demo in mainClientDemos ) 
         {
            DicomDemoSettings settings = DicomDemoSettingsManager.LoadSettings ( demo )  ;

            if (settings != null)
            {

               foreach (DicomAE server in servers)
               {
                  DicomAE removedServer = null;

                  foreach (DicomAE ae in settings.ServerList)
                  {
                     if (ae.AE == server.AE && ae.IPAddress == server.IPAddress && ae.Port == server.Port)
                     {
                        removedServer = ae;

                        break;
                     }
                  }

                  if (null != removedServer)
                  {
                     settings.ServerList.Remove(removedServer);
                  }
               }

               DicomDemoSettingsManager.SaveSettings(demo, settings);
            }
         }
      }

      private void removeToolStripMenuItem_Click(object sender, EventArgs e)
      {
         try
         {
            Remove();
         }
         catch ( Exception exception ) 
         {
            Messager.ShowError ( this, exception ) ;
         }
      }

      private void buttonRemove_Click(object sender, EventArgs e)
      {
         try
         {
            Remove();
         }
         catch ( Exception exception ) 
         {
            Messager.ShowError ( this, exception ) ;
         }
      }

 

      private void buttonRetest_Click(object sender, EventArgs e)
      {
         ClearErrors();
         VerifyServers(0, true);
      }

      private void closeToolStripMenuItem_Click(object sender, EventArgs e)
      {
         this.DialogResult = DialogResult.OK;
         this.Close();
      }

      private void buttonClose_Click(object sender, EventArgs e)
      {
         this.DialogResult = DialogResult.OK;
         this.Close();
      }

      private void MainForm_Shown(object sender, EventArgs e)
      {
         _mySettings.Load();

         if (_mySettings._settings.ShowHelpOnStart)
         {
            HelpDialog dlg = new HelpDialog(_mySettings._settings.ServerAE, _mySettings._settings.ShowHelpOnStart);
            dlg.ShowDialog(this);
            if (dlg.CheckBoxNoShowAgainResult)
            {
               _mySettings._settings.ShowHelpOnStart = false;
            }
         }
         _mySettings.Save();
      }

      private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
      {
         _mySettings._settings.ConfigureDemoServer = true;// ConfigureMainDicomServerCheckBox.Checked;
         _mySettings._settings.ConfigureWSServer = true;// ConfigureWSServerCheckBox.Checked;
         _mySettings._settings.ConfigureWorklistServer = true;// ConfigureWSServerCheckBox.Checked;
         _mySettings._settings.ConfigureStorageServer = true;// ConfigureWSServerCheckBox.Checked;

         _mySettings._settings.ConfigureMainClient = true;// ConfigureDICOMClientCheckBox.Checked;
         _mySettings._settings.ConfigureWSClient = true;// ConfigureWSClientCheckBox.Checked;

         _mySettings._settings.ServerAE = textBoxServerAe.Text;
         _mySettings._settings.ServerPort = (int)numericUpDownServerPort.Value;

         _mySettings._settings.StorageServerAE = textBoxStorageServerAe.Text;
         _mySettings._settings.StorageServerPort = (int)numericUpDownStorageServerPort.Value;
         _mySettings._settings.StorageStartServer = checkBoxStartStorageServer.Checked;
         
         _mySettings._settings.WorklistServerAE = textBoxWorklistServerAe.Text;
         _mySettings._settings.WorklistServerPort = (int)numericUpDownWorklistServerPort.Value;
         _mySettings._settings.WorklistStartServer = checkBoxStartWorklistServer.Checked;
         
         _mySettings._settings.ClientAE = textBoxClientAe.Text;
         _mySettings._settings.ClientPort = (int)numericUpDownClientPort.Value;
         _mySettings._settings.StartServer = checkBoxStartServer.Checked;
#if LEADTOOLS_V175_OR_LATER
         _mySettings._settings.InstallBrokerHost = checkBoxBrokerHost.Checked;
         _mySettings._settings.BrokerHostPort = (int)numericUpDownBrokerPort.Value;
#endif


         //// Router service
         //_mySettings._settings.RoutingServerAE = textBoxRoutingServerAe.Text;
         //_mySettings._settings.RoutingServerPort = (int)numericUpDownRoutingServerPort.Value;
         //_mySettings._settings.RoutingStartServer = checkBoxStartRoutingServer.Checked;
         
         _mySettings._settings.WsServerAE    = textBoxWsServerAe.Text     ;
         _mySettings._settings.WsServerPort  = ( int ) numericUpDownWsServerPort.Value ;
         _mySettings._settings.WsClientAE    = textBoxWsClientAe.Text     ;
         _mySettings._settings.WsClientPort  = ( int )numericUpDownWsClientPort.Value ;
         _mySettings._settings.WsStartServer = checkBoxStartWsServer.Checked   ;
         _mySettings._settings.ResetClientConfigurations = checkBoxResetClientConfigurations.Checked;
         
         _mySettings.Save();

         if (_serviceAdmin != null)
            _serviceAdmin.Dispose();
      }

      private void textBoxServerAe_KeyPress(object sender, KeyPressEventArgs e)
      {
         ClearErrors();
      }

      private void numericUpDownServerPort_KeyPress(object sender, KeyPressEventArgs e)
      {
         ClearErrors();
      }


      private void numericUpDownServerPort_ValueChanged(object sender, EventArgs e)
      {
         ClearErrors();
      }

      private void checkBoxStartServer_CheckedChanged(object sender, EventArgs e)
      {
         ClearErrors();
      }

      private void textBoxClientAe_KeyPress(object sender, KeyPressEventArgs e)
      {
         ClearErrors();
      }

      private void numericUpDownClientPort_KeyPress(object sender, KeyPressEventArgs e)
      {
         ClearErrors();
      }

      private void numericUpDownClientPort_ValueChanged(object sender, EventArgs e)
      {
         ClearErrors();
      }

      private void checkBoxResetClientConfigurations_CheckedChanged(object sender, EventArgs e)
      {
         ClearErrors();
      }

      private void howToToolStripMenuItem_Click(object sender, EventArgs e)
      {
         DemosGlobal.LaunchHowTo("PACSConfigurationDemo.html");
      }

      private void buttonServersHelp_Click(object sender, EventArgs e)
      {
         DemosGlobal.LaunchHowTo("DICOMServerSettings.html");
      }

      private bool _showHelp = true;
#if LEADTOOLS_V19_OR_LATER
      private bool  VerifyServers(int milliseconds, bool logSuccess)
      {
         string storageServerAE = textBoxStorageServerAe.Text.ToUpper();
         string wsServerAE = this.textBoxWsServerAe.Text.ToUpper();
         string worklistServerAE = this.textBoxWorklistServerAe.Text.ToUpper();
         // string demoServerAe = this.textBoxServerAe.Text.ToUpper();

         DicomService storageServerService = null;
         DicomService wsServerService = null;
         DicomService worklistServerService = null;
         // DicomService demoServerService = null;


         foreach (KeyValuePair<string, DicomService> kv in _serviceAdmin.Services)
         {
            string key = kv.Key.ToUpper();
            if (key.Contains("PACS_SCP") || key.Contains(storageServerAE))
            {
               storageServerService = kv.Value;
            }

            if (key.Contains(wsServerAE))
            {
               wsServerService = kv.Value;
            }

            if (key.Contains(worklistServerAE))
            {
               worklistServerService = kv.Value;
            }

            // Don't need to test the demo server -- it does not use an SQL database
         }

         bool result = true;

         if (storageServerService != null)
         {
            result = result && VerifyServer(storageServerService, milliseconds, logSuccess);
         }

         if (wsServerService != null)
         {
            result = result && VerifyServer(wsServerService, 0, logSuccess);
         }

         if (worklistServerService != null)
         {
            result = result && VerifyServer(worklistServerService, 0, logSuccess);
         }

         if (result == false)
         {
            if (_showHelp)
            {
               ShowHyperlink("Click here to fix this problem (click 'Retest' button to verify fixed)");
               _showHelp = false;
            }
         }

         return result;
      }

      private bool VerifyServer(DicomService serverService, int milliseconds, bool logSuccess)
      {
         if (milliseconds > 0)
         {
            serverService.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 0, 16));

            // Wait a few more seconds for the administrative pipe to get connected

            Thread.Sleep(milliseconds);
         }

         string errorMessage = string.Empty;
         string message = string.Empty;
         DicomServiceTesterResult result = DicomServiceTester.Test(serverService, out errorMessage);
         switch (result)
         {
            case DicomServiceTesterResult.Success:
               message = string.Format("{0} Listening Service is valid", serverService.ServiceName);
               break;
            case DicomServiceTesterResult.ErrorServiceIsNull:
               message = string.Format("{0} Listening Service has not been created", serverService.ServiceName);
               break;
            case DicomServiceTesterResult.ErrorServiceNotRunning:
               message = string.Format("{0} Listening Service must be running to verify", serverService.ServiceName);
               break;
            case DicomServiceTesterResult.ErrorServiceCannotAccessDatabase:
               message = string.Format("{0} Listening Service cannot access database. {1}",  serverService.ServiceName, errorMessage);
               break;
            case DicomServiceTesterResult.ErrorServiceNotResponding:
               message = string.Format("Unable to communicate with {0} Listening Service", serverService.ServiceName);
               break;
         }

         if (result != DicomServiceTesterResult.Success)
         {
            ShowError(null, message);
  
         }
         else
         {
            if (logSuccess)
            {
               ShowMessage(message, StatusType.Check);
            }
         }
         return (result == DicomServiceTesterResult.Success);
      }
#else
 private bool  VerifyServers(int milliseconds, bool logSuccess)
      {
         return true;
      }
#endif // #if LEADTOOLS_V19_OR_LATER

      private void buttonStorageServerHelp_Click(object sender, EventArgs e)
      {
         DemosGlobal.LaunchHowTo("DICOMServerSettings.html");
      }
       
      private void buttonWorklistServerHelp_Click(object sender, EventArgs e)
      {
         DemosGlobal.LaunchHowTo("DICOMServerSettings.html");
      }


      private void buttonClientsHelp_Click(object sender, EventArgs e)
      {
         DemosGlobal.LaunchHowTo("DICOM_Client_Settings.html");
      }

      private void checkBoxBrokerHost_CheckedChanged(object sender, EventArgs e)
      {
         numericUpDownBrokerPort.Enabled = (checkBoxBrokerHost.Checked == true);
      }


      private void ConfigureStorageServerService(ServerSettings storageServerSettings)
      {
         StorageServerInformation information = null;

         DicomAE ae = new DicomAE(storageServerSettings.AETitle,
                                    storageServerSettings.IpAddress,
                                    storageServerSettings.Port,
                                    0,
                                    storageServerSettings.Secure);

         information = new StorageServerInformation(ae, storageServerSettings.ServiceName, Environment.MachineName);

         try
         {
            OptionsDataAccessConfigurationView optionsConfigView = new OptionsDataAccessConfigurationView(DicomDemoSettingsManager.GetGlobalPacsConfiguration(), DicomDemoSettingsManager.ProductNameStorageServer, null);
            IOptionsDataAccessAgent optionsAgent = DataAccessFactory.GetInstance(optionsConfigView).CreateDataAccessAgent<IOptionsDataAccessAgent>();
            optionsAgent.Set<StorageServerInformation>(typeof(StorageServerInformation).Name, information, new Type[0]);
         }
         catch (Exception ex)
         {
            Messager.ShowError(this, "ConfigureStorageServerService Failed");
            throw ex;
         }
      }
      
      private void ConfigureStorageServerClients()
      {
         AeInfoExtended mainClient = new AeInfoExtended(textBoxClientAe.Text, _defaultIP, (int)numericUpDownClientPort.Value);
         AeInfoExtended wsClient = new AeInfoExtended(textBoxWsClientAe.Text, _defaultIP, (int)numericUpDownWsClientPort.Value);
         AeInfoExtended demoServer = new AeInfoExtended(textBoxServerAe.Text, _defaultIP, (int)numericUpDownServerPort.Value);
         AeInfoExtended wsServer = new AeInfoExtended(textBoxWsServerAe.Text, _defaultIP, (int)numericUpDownWsServerPort.Value);
         
         mainClient.SecurePort = 2762;
         wsClient.SecurePort = 2762;
         demoServer.SecurePort = 2762;
         wsServer.SecurePort = 2762;
         try
         {
            AeManagementDataAccessConfigurationView aeConfigView = new AeManagementDataAccessConfigurationView(DicomDemoSettingsManager.GetGlobalPacsConfiguration(), DicomDemoSettingsManager.ProductNameStorageServer, null);
            IAeManagementDataAccessAgent aeAgent = DataAccessFactory.GetInstance(aeConfigView).CreateDataAccessAgent<IAeManagementDataAccessAgent>();
            
            bool bMainClientExists = false;
            bool bWsClientExists = false;
            bool bDemoServerExists = false;
            bool bWsServerExists = false;
            
            if (aeAgent != null)
            {
               AeInfoExtended []existingClients = aeAgent.GetAeTitles();
               foreach (AeInfoExtended client in existingClients)
               {
                  if (string.Compare(client.AETitle, mainClient.AETitle, true)== 0)
                     bMainClientExists = true;

                  if (string.Compare(client.AETitle, wsClient.AETitle, true) == 0)
                     bWsClientExists = true;

                  if (string.Compare(client.AETitle, demoServer.AETitle, true) == 0)
                     bDemoServerExists = true;

                  if (string.Compare(client.AETitle, wsServer.AETitle, true) == 0)
                     bWsServerExists = true;
               }

               if (bMainClientExists)
                  aeAgent.Update(mainClient.AETitle, mainClient);
               else
                  aeAgent.Add(mainClient);

               if (bWsClientExists)
                  aeAgent.Update(wsClient.AETitle, wsClient);
               else
                  aeAgent.Add(wsClient);
                  
               if (bDemoServerExists)
                  aeAgent.Update(demoServer.AETitle, demoServer);
               else
                  aeAgent.Add(demoServer);

               if (bWsServerExists)
                  aeAgent.Update(wsServer.AETitle, wsServer);
               else
                  aeAgent.Add(wsServer);
            }

            AePermissionManagementDataAccessConfigurationView permssionsView = new AePermissionManagementDataAccessConfigurationView(DicomDemoSettingsManager.GetGlobalPacsConfiguration(), DicomDemoSettingsManager.ProductNameStorageServer, null);
            IPermissionManagementDataAccessAgent permissionsAgent = DataAccessFactory.GetInstance(permssionsView).CreateDataAccessAgent<IPermissionManagementDataAccessAgent>();
            if (permissionsAgent != null)
            {
               Permission[] permissions = permissionsAgent.GetPermissions();
               foreach (Permission permission in permissions)
               {
                  permissionsAgent.AddUserPermission(permission.Name, mainClient.AETitle);
                  permissionsAgent.AddUserPermission(permission.Name, wsClient.AETitle);
                  permissionsAgent.AddUserPermission(permission.Name, demoServer.AETitle);
                  permissionsAgent.AddUserPermission(permission.Name, wsServer.AETitle);
               }
            }

         }
         catch (Exception ex)
         {
            Messager.ShowError(this, "ConfigureStorageServerClients Failed");
            throw ex;
         }
      }
      
      private static string showText = "Show Advanced &Options";
      private static string hideText = "Hide Advanced &Options";
      
      private void showAdvancedOptions(bool visible)
      {
         labelStartServer.Visible = visible;
         checkBoxStartStorageServer.Visible = visible;
         checkBoxStartWsServer.Visible = visible;
         checkBoxStartServer.Visible = visible;
         checkBoxStartWorklistServer.Visible = visible;
         checkBoxBrokerHost.Visible = visible;
         labelBrokerHost.Visible = !visible;
         checkBoxResetClientConfigurations.Visible = visible;
      }

      private void showAdvancedOptionsToolStripMenuItem_Click(object sender, EventArgs e)
      {
         if (showAdvancedOptionsToolStripMenuItem.Text.Contains("Show"))
         {
            showAdvancedOptionsToolStripMenuItem.Text = hideText;
            showAdvancedOptions(true);
         }
         else
         {
            showAdvancedOptionsToolStripMenuItem.Text = showText;
            showAdvancedOptions(false);
         }
      }


      private void listViewStatus_MouseMove(object sender, MouseEventArgs e)
      {
         ListViewItem lvi = listViewStatus.GetItemAt(e.X, e.Y);
         if (lvi != null)
         {
            if (lvi.Font.Underline)
            {
               listViewStatus.Cursor = Cursors.Hand;
               return;
            }
         }
         listViewStatus.Cursor = Cursors.Arrow;
      }

      private void listViewStatus_Click(object sender, EventArgs e)
      {

      }

      private void listViewStatus_MouseClick(object sender, MouseEventArgs e)
      {
         ListViewItem lvi = listViewStatus.GetItemAt(e.X, e.Y);
         if (lvi != null)
         {
            if (lvi.Font.Underline)
            {
               Process.Start("https://www.leadtools.com/support/guides/pacs-troubleshooting-guide.pdf");
               return;
            }
         }
      }







      
   }

   public enum StatusType
   {
      Check = 0,
      Warning = 1,
      Error = 2,
      Nothing = 3
   }

   // Extension methods
   public static class MyExtensions
   {
      public static void SetValueAndValidate(this NumericUpDown numericUpDown, decimal value)
      {
         if (value < numericUpDown.Minimum)
            value = numericUpDown.Minimum;

         if (value > numericUpDown.Maximum)
            value = numericUpDown.Maximum;

         numericUpDown.Value = value;
      }
   }
   
   
}
