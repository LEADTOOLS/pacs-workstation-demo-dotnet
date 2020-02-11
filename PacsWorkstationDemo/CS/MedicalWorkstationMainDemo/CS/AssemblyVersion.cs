// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
//
// AssemblyVersion.cs
// 

partial struct AssemblyVersionNumber
{
   public const string ProductPrefix               = "LEADTOOLS © for .NET";
   public const string CompanyName                 = "LEAD Technologies, Inc.";
   public const string Configuration               = "";
   public const string Trademark                   = "LEADTOOLS © is a trademark of LEAD Technologies, Inc.";
   public const string Copyright                   = "Copyright (c) 1991-2019 LEAD Technologies, Inc.";
#if LEADTOOLS_V19_OR_LATER_MEDICAL_EXTERNAL_STORE
   public const string DllExt                      = "(ES).dll";
#else
   public const string DllExt                      = ".dll";
#endif

#if LEADTOOLS_V19_OR_LATER_MEDICAL_EXTERNAL_STORE
   public const string ExeExt                      = "(ES).exe";
#else
   public const string ExeExt                      = ".exe";
#endif // #if LEADTOOLS_V19_OR_LATER_MEDICAL_EXTERNAL_STORE

#if FOR_WIN64
   public const string Platform                    = " (x64)";
   public const string PlatformFor                 = " for x64";
#else
#if FOR_ANYCPU
      public const string Platform                 = " (Any CPU)";
      public const string PlatformFor              = " for Any CPU";
#else
   public const string Platform                    = " (Win32)";
   public const string PlatformFor                 = " for Win32";
#endif // #if FOR_ANYCPU
#endif   // FOR_WIN64

#if LTV175_CONFIG
   public const string Culture                     = "";
   public const string Product                     = ProductPrefix + Platform;
   public const string Version                     = "17.5.0.0";

   // Leadtools.Medical.Winforms.dll
   public const string TitleMedicalWinforms                    = "Leadtools.Medical.WinForms" + DllExt;
   public const string DescriptionMedicalWinforms              = "Medical Windows Forms";
   public const string FileVersionMedicalWinforms              = "17.5.0.56";

   // Leadtools.Medical.Ae.Configuration.dll
   public const string TitleMedicalAeConfiguration             = "Leadtools.Medical.Ae.Configuration" + DllExt;
   public const string DescriptionMedicalAeConfiguration       = "Leadtools.Medical.Ae.Configuration";
   public const string FileVersionMedicalAeConfiguration       = "17.5.0.4";

   // Leadtools.Medical.AutoCopy.dll
   public const string TitleMedicalAutoCopyAddin               = "Leadtools.Medical.AutoCopy.AddIn" + DllExt;
   public const string DescriptionMedicalAutoCopyAddin         = "Leadtools.Medical.AutoCopy.AddIn";
   public const string FileVersionMedicalAutoCopyAddin         = "17.5.0.4";

   // Leadtools.Medical.Forwarder.AddIn.dll
   public const string TitleMedicalForwarderAddin              = "Leadtools.Medical.Forwarder.AddIn" + DllExt;
   public const string DescriptionMedicalForwarderAddin        = "Leadtools.Medical.Forwarder.AddIn";
   public const string FileVersionMedicalForwarderAddin        = "17.5.0.7";

   // Leadtools.Medical.Gateway.AddIn.dll
   public const string TitleMedicalGatewayAddin                = "Leadtools.Medical.Gateway.AddIn" + DllExt;
   public const string DescriptionMedicalGatewayAddin          = "Leadtools.Medical.Gateway.AddIn";
   public const string FileVersionMedicalGatewayAddin          = "17.5.0.3";

   // Leadtools.Medical.License.Configuration.dll
   public const string TitleMedicalLicenseConfiguration        = "Leadtools.Medical.License.Configuration" + DllExt;
   public const string DescriptionMedicalLicenseConfiguration  = "Leadtools.Medical.License.Configuration";
   public const string FileVersionMedicalLicenseConfiguration  = "17.5.0.1";

   // Leadtools.Medical.Logging.AddIn.dll
   public const string TitleMedicalLoggingAddin                = "Leadtools.Medical.Logging.AddIn" + DllExt;
   public const string DescriptionMedicalLoggingAddin          = "Leadtools.Medical.Logging.AddIn";
   public const string FileVersionMedicalLoggingAddin          = "17.5.0.3";

   // Leadtools.Medical.Media.AddIns.dll
   public const string TitleMedicalMediaAddin                  = "Leadtools.Medical.Media.AddIns" + DllExt;
   public const string DescriptionMedicalMediaAddin            = "Leadtools.Medical.Media.AddIns";
   public const string FileVersionMedicalMediaAddin            = "17.5.0.2";
   
   // Leadtools.Medical.PatientUpdater.AddIn
   public const string TitleMedicalPatientUpdaterAddin         = "Leadtools.Medical.PatientUpdater.AddIn" + DllExt;
   public const string DescriptionMedicalPatientUpdaterAddin   = "Leadtools.Medical.PatientUpdater.AddIn";
   public const string FileVersionMedicalPatientUpdaterAddin   = "17.5.0.6";

   // Leadtools.Medical.Storage.AddIns.dll
   public const string TitleMedicalStorageAddin                = "Leadtools.Medical.Storage.AddIns" + DllExt;
   public const string DescriptionMedicalStorageAddin          = "Leadtools.Medical.Storage.AddIns";
   public const string FileVersionMedicalStorageAddin          = "17.5.0.16";

   // Leadtools.Medical.Worklist.AddIns.dll
   public const string TitleMedicalWorklistAddin               = "Leadtools.Medical.Worklist.AddIns" + DllExt;
   public const string DescriptionMedicalWorklistAddin         = "Leadtools.Medical.Worklist.AddIns";
   public const string FileVersionMedicalWorklistAddin         = "17.5.0.2";

   // Leadtools.Medical.AeManagement.DataAccessLayer.dll
   public const string TitleMedicalAeManagementDataAccessLayer = "Leadtools.Medical.AeManagement.DataAccessLayer" + DllExt;
   public const string DescriptionAeManagementDataAccessLayer  = "Leadtools.Medical.AeManagement.DataAccessLayer";
   public const string FileVersionAeManagementDataAccessLayer  = "17.5.0.4";

   // Leadtools.Medical.Forward.DataAccessLayer.dll
   public const string TitleMedicalForwardDataAccessLayer                            = "Leadtools.Medical.Forward.DataAccessLayer" + DllExt;
   public const string DescriptionMedicalForwardDataAccessLayer                      = "Leadtools.Medical.Forward.DataAccessLayer";
   public const string FileVersionMedicalForwardDataAccessLayer                      = "17.5.0.1";

   // Leadtools.Medical.Options.DataAccessLayer.dll
   public const string TitleMedicalOptionsDataAccessLayer                            = "Leadtools.Medical.Options.DataAccessLayer" + DllExt;
   public const string DescriptionMedicalOptionsDataAccessLayer                      = "Leadtools.Medical.Options.DataAccessLayer";
   public const string FileVersionMedicalOptionsDataAccessLayer                      = "17.5.0.2";

   // Leadtools.Medical.PermissionsManagement.DataAccessLayer.dll
   public const string TitleMedicalPermissionsManagementDataAccessLayer              = "Leadtools.Medical.PermissionsManagement.DataAccessLayer" + DllExt;
   public const string DescriptionMedicalPermissionsManagementDataAccessLayer        = "Leadtools.Medical.PermissionsManagement.DataAccessLayer";
   public const string FileVersionMedicalPermissionsManagementDataAccessLayer        = "17.5.0.2";

   // Leadtools.Medical.UserManagementDataAccessLayer.dll
   public const string TitleMedicalUserManagementDataAccessLayer                     = "Leadtools.Medical.UserManagementDataAccessLayer" + DllExt;
   public const string DescriptionMedicalUserManagementDataAccessLayer               = "Leadtools.Medical.UserManagementDataAccessLayer";
   public const string FileVersionMedicalUserManagementDataAccessLayer               = "17.5.0.3";

   // Leadtools.Windows.Annotations.dll
   public const string TitleAnnotationsWinForms                                      = "Leadtools.Windows.Annotations" + DllExt;
   public const string DescriptionAnnotationsWinForms                                = "Leadtools.Windows.Annotations";
   public const string FileVersionAnnotationsWinForms                                = "17.5.0.1";
#endif


#if LTV18_CONFIG
   public const string Culture                     = "";
   public const string Product                     = ProductPrefix + Platform;

#if FOR_DOTNET4
   public const string Version                     = "18.0.4.0";
#else
   public const string Version                     = "18.0.0.0";
#endif // FOR_DOTNET4


   // Leadtools.Medical.Winforms.dll
   public const string TitleMedicalWinforms                    = "Leadtools.Medical.WinForms" + DllExt;
   public const string DescriptionMedicalWinforms              = "Medical Windows Forms";
   public const string FileVersionMedicalWinforms              = "18.0.0.63";

   // Leadtools.Medical.Ae.Configuration.dll
   public const string TitleMedicalAeConfiguration             = "Leadtools.Medical.Ae.Configuration" + DllExt;
   public const string DescriptionMedicalAeConfiguration       = "Leadtools.Medical.Ae.Configuration";
   public const string FileVersionMedicalAeConfiguration       = "18.0.0.5";

   // Leadtools.Medical.AutoCopy.dll
   public const string TitleMedicalAutoCopyAddin               = "Leadtools.Medical.AutoCopy.AddIn" + DllExt;
   public const string DescriptionMedicalAutoCopyAddin         = "Leadtools.Medical.AutoCopy.AddIn";
   public const string FileVersionMedicalAutoCopyAddin         = "18.0.0.6";

   // Leadtools.Medical.ExternalStore.Sample.AddIn.dll
   public const string TitleMedicalExternalStoreSampleAddin        = "Leadtools.Medical.ExternalStore.Sample.AddIn" + DllExt;
   public const string DescriptionMedicalExternalStoreSampleAddin  = "Leadtools.Medical.ExternalStore.Sample.AddIn";
   public const string FileVersionMedicalExternalStoreSampleAddin  = "18.0.0.5";

   // Leadtools.Medical.ExternalStore.AddIn.dll
   public const string TitleMedicalExternalStoreAddin        = "Leadtools.Medical.ExternalStore.AddIn" + DllExt;
   public const string DescriptionMedicalExternalStoreAddin  = "Leadtools.Medical.ExternalStore.AddIn";
   public const string FileVersionMedicalExternalStoreAddin = "18.0.0.6";

   // Leadtools.Medical.ExternalStore.Atmos.AddIn.dll
   public const string TitleMedicalExternalStoreAtmosAddin        = "Leadtools.Medical.ExternalStore.Atmos.AddIn" + DllExt;
   public const string DescriptionMedicalExternalStoreAtmosAddin  = "Leadtools.Medical.ExternalStore.Atmos.AddIn";
   public const string FileVersionMedicalExternalStoreAtmosAddin  = "18.0.0.4";

   // Leadtools.Medical.ExternalStore.Azure.AddIn.dll
   public const string TitleMedicalExternalStoreAzureAddin        = "Leadtools.Medical.ExternalStore.Azure.AddIn" + DllExt;
   public const string DescriptionMedicalExternalStoreAzureAddin  = "Leadtools.Medical.ExternalStore.Azure.AddIn";
   public const string FileVersionMedicalExternalStoreAzureAddin  = "18.0.0.4";

   // Leadtools.Medical.Forwarder.AddIn.dll
   public const string TitleMedicalForwarderAddin              = "Leadtools.Medical.Forwarder.AddIn" + DllExt;
   public const string DescriptionMedicalForwarderAddin        = "Leadtools.Medical.Forwarder.AddIn";
   public const string FileVersionMedicalForwarderAddin        = "18.0.0.9";

   // Leadtools.Medical.Gateway.AddIn.dll
   public const string TitleMedicalGatewayAddin                = "Leadtools.Medical.Gateway.AddIn" + DllExt;
   public const string DescriptionMedicalGatewayAddin          = "Leadtools.Medical.Gateway.AddIn";
   public const string FileVersionMedicalGatewayAddin          = "18.0.0.5";

   // Leadtools.Medical.License.Configuration.dll
   public const string TitleMedicalLicenseConfiguration        = "Leadtools.Medical.License.Configuration" + DllExt;
   public const string DescriptionMedicalLicenseConfiguration  = "Leadtools.Medical.License.Configuration";
   public const string FileVersionMedicalLicenseConfiguration  = "18.0.0.5";

   // Leadtools.Medical.Logging.AddIn.dll
   public const string TitleMedicalLoggingAddin                = "Leadtools.Medical.Logging.AddIn" + DllExt;
   public const string DescriptionMedicalLoggingAddin          = "Leadtools.Medical.Logging.AddIn";
   public const string FileVersionMedicalLoggingAddin          = "18.0.0.3";

   // Leadtools.Medical.Media.AddIns.dll
   public const string TitleMedicalMediaAddin                  = "Leadtools.Medical.Media.AddIns" + DllExt;
   public const string DescriptionMedicalMediaAddin            = "Leadtools.Medical.Media.AddIns";
   public const string FileVersionMedicalMediaAddin            = "18.0.0.4";
   
   // Leadtools.Medical.PatientUpdater.AddIn
   public const string TitleMedicalPatientUpdaterAddin         = "Leadtools.Medical.PatientUpdater.AddIn" + DllExt;
   public const string DescriptionMedicalPatientUpdaterAddin   = "Leadtools.Medical.PatientUpdater.AddIn";
   public const string FileVersionMedicalPatientUpdaterAddin   = "18.0.0.9";

   // Leadtools.Medical.Storage.AddIns.dll
   public const string TitleMedicalStorageAddin                = "Leadtools.Medical.Storage.AddIns" + DllExt;
   public const string DescriptionMedicalStorageAddin          = "Leadtools.Medical.Storage.AddIns";
   public const string FileVersionMedicalStorageAddin          = "18.0.0.21";

   // Leadtools.Medical.Worklist.AddIns.dll
   public const string TitleMedicalWorklistAddin               = "Leadtools.Medical.Worklist.AddIns" + DllExt;
   public const string DescriptionMedicalWorklistAddin         = "Leadtools.Medical.Worklist.AddIns";
   public const string FileVersionMedicalWorklistAddin         = "18.0.0.5";

   // Leadtools.Medical.AeManagement.DataAccessLayer.dll
   public const string TitleMedicalAeManagementDataAccessLayer = "Leadtools.Medical.AeManagement.DataAccessLayer" + DllExt;
   public const string DescriptionAeManagementDataAccessLayer  = "Leadtools.Medical.AeManagement.DataAccessLayer";
   public const string FileVersionAeManagementDataAccessLayer  = "18.0.0.6";

   // Leadtools.Medical.Forward.DataAccessLayer.dll
   public const string TitleMedicalForwardDataAccessLayer                            = "Leadtools.Medical.Forward.DataAccessLayer" + DllExt;
   public const string DescriptionMedicalForwardDataAccessLayer                      = "Leadtools.Medical.Forward.DataAccessLayer";
   public const string FileVersionMedicalForwardDataAccessLayer                      = "18.0.0.3";

   // Leadtools.DataAccessLayers.dll
   public const string TitleDataAccessLayers                            = "Leadtools.DataAccessLayers" + DllExt;
   public const string DescriptionDataAccessLayers                      = "Leadtools.DataAccessLayers";
   public const string FileVersionDataAccessLayers                      = "18.0.0.5";
   
   // Leadtools.DataAccessLayers.Core
   public const string TitleDataAccessLayersCore1                                       = "Leadtools.DataAccessLayers.Core" + DllExt;
   public const string DescriptionDataAccessLayersCore1                                 = "Leadtools.DataAccessLayers.Core";
   public const string FileVersionDataAccessLayersCore1                                 = "18.0.0.3";

   // Leadtools.Medical.Options.DataAccessLayer.dll
   public const string TitleMedicalOptionsDataAccessLayer                            = "Leadtools.Medical.Options.DataAccessLayer" + DllExt;
   public const string DescriptionMedicalOptionsDataAccessLayer                      = "Leadtools.Medical.Options.DataAccessLayer";
   public const string FileVersionMedicalOptionsDataAccessLayer                      = "18.0.0.4";

   // Leadtools.Medical.PermissionsManagement.DataAccessLayer.dll
   public const string TitleMedicalPermissionsManagementDataAccessLayer              = "Leadtools.Medical.PermissionsManagement.DataAccessLayer" + DllExt;
   public const string DescriptionMedicalPermissionsManagementDataAccessLayer        = "Leadtools.Medical.PermissionsManagement.DataAccessLayer";
   public const string FileVersionMedicalPermissionsManagementDataAccessLayer        = "18.0.0.4";

   // Leadtools.Medical.UserManagementDataAccessLayer.dll
   public const string TitleMedicalUserManagementDataAccessLayer                     = "Leadtools.Medical.UserManagementDataAccessLayer" + DllExt;
   public const string DescriptionMedicalUserManagementDataAccessLayer               = "Leadtools.Medical.UserManagementDataAccessLayer";
   public const string FileVersionMedicalUserManagementDataAccessLayer               = "18.0.0.5";

   // Leadtools.Medical.Rules.Addin.dll
   public const string TitleMedicalRulesAddin                                        = "Leadtools.Medical.Rules.Addin" + DllExt;
   public const string DescriptionMedicalRulesAddin                                  = "Leadtools.Medical.Rules.Addin";
   public const string FileVersionMedicalRulesAddin                                  = "18.0.0.2";

    // Leadtools.Medical.DataAccessLayersCore
   public const string TitleDataAccessLayersCore                                       = "Leadtools.Medical.DataAccessLayersCore" + DllExt;
   public const string DescriptionDataAccessLayersCore                                 = "Leadtools.Medical.DataAccessLayersCore";
   public const string FileVersionDataAccessLayersCore                                 = "18.0.0.1";

   // Leadtools.Medical.HL7MWL.AddIn
   public const string TitleMedicalHL7MWLAddIn                                         = "Leadtools.Medical.HL7MWL.AddIn" + DllExt;
   public const string DescriptionMedicalHL7MWLAddIn                                   = "Leadtools.Medical.HL7MWL.AddIn";
   public const string FileVersionMedicalHL7MWLAddIn                                   = "18.0.0.1";

   // Leadtools.Medical.WebViewer.Addins
   public const string TitleMedicalWebViewerAddins                                     = "Leadtools.Medical.WebViewer.Addins" + DllExt;
   public const string DescriptionMedicalWebViewerAddins                               = "Leadtools.Medical.WebViewer.Addins";
   public const string FileVersionMedicalWebViewerAddins                               = "18.0.0.4";

   // Leadtools.Medical.WebViewer.Annotations.DataAccessLayer
   public const string TitleMedicalWebViewerAnnotationsDataAccessLayer                                 = "Leadtools.Medical.WebViewer.Annotations.DataAccessLayer" + DllExt;
   public const string DescriptionMedicalWebViewerAnnotationsDataAccessLayer                           = "Leadtools.Medical.WebViewer.Annotations.DataAccessLayer";
   public const string FileVersionMedicalWebViewerAnnotationsDataAccessLayer                           = "18.0.0.1";

   // Leadtools.Medical.WebViewer.Core
   public const string TitleMedicalWebViewerCore                               = "Leadtools.Medical.WebViewer.Core" + DllExt;
   public const string DescriptionMedicalWebViewerCore                         = "Leadtools.Medical.WebViewer.Core";
   public const string FileVersionMedicalWebViewerCore                         = "18.0.0.17";

   // Leadtools.Medical.WebViewer.ImageDownloadAddin
   public const string TitleMedicalWebViewerImageDownloadAddin                                = "Leadtools.Medical.WebViewer.ImageDownloadAddin" + DllExt;
   public const string DescriptionMedicalWebViewerImageDownloadAddin                          = "Leadtools.Medical.WebViewer.ImageDownloadAddin";
   public const string FileVersionMedicalWebViewerImageDownloadAddin                          = "18.0.0.2";

   // Leadtools.Medical.WebViewer.Jobs
   public const string TitleMedicalWebViewerJobs                               = "Leadtools.Medical.WebViewer.Jobs" + DllExt;
   public const string DescriptionMedicalWebViewerJobs                         = "Leadtools.Medical.WebViewer.Jobs";
   public const string FileVersionMedicalWebViewerJobs                         = "18.0.0.1";

   // Leadtools.Medical.WebViewer.JobsCleanupAddin.dll
   public const string TitleMedicalWebViewerJobsCleanupAddin                    = "Leadtools.Medical.WebViewer.JobsCleanupAddin" + DllExt;
   public const string DescriptionMedicalWebViewerJobsCleanupAddin              = "Leadtools.Medical.WebViewer.JobsCleanupAddin";
   public const string FileVersionMedicalWebViewerJobsCleanupAddin              = "18.0.0.1";

   // Leadtools.Medical.WebViewer.PatientAccessRights.DataAccessAgent
   public const string TitleMedicalWebViewerPatientAccessRightsDataAccessAgent                               = "Leadtools.Medical.WebViewer.PatientAccessRights.DataAccessAgent" + DllExt;
   public const string DescriptionMedicalWebViewerPatientAccessRightsDataAccessAgent                         = "Leadtools.Medical.WebViewer.PatientAccessRights.DataAccessAgent";
   public const string FileVersionMedicalWebViewerPatientAccessRightsDataAccessAgent                         = "18.0.0.4";

   // Leadtools.Medical.WebViewer.WCF
   public const string TitleMedicalWebViewerWCF                               = "Leadtools.Medical.WebViewer.WCF"+ DllExt;
   public const string DescriptionMedicalWebViewerWCF                         = "Leadtools.Medical.WebViewer.WCF";
   public const string FileVersionMedicalWebViewerWCF                         = "18.0.0.16";

   // Leadtools.Medical.WebViewer.ExternalControl
   public const string TitleMedicalWebViewerExternalControl                               = "Leadtools.Medical.WebViewer.ExternalControl"+ DllExt;
   public const string DescriptionMedicalWebViewerExternalControl                         = "Leadtools.Medical.WebViewer.ExternalControl";
   public const string FileVersionMedicalWebViewerExternalControl                         = "18.0.0.5";

   // Leadtools.MedicalViewer.CS.WebForms
   public const string TitleMedicalViewerCSWebForms                               = "MedicalViewer.CS.WebForms"+ DllExt;
   public const string DescriptionMedicalViewerCSWebForms                         = "MedicalViewer.CS.WebForms";
   public const string FileVersionMedicalViewerCSWebForms                         = "18.0.0.1";

   // Leadtools.Medical.SearchOtherPatientIDs.Addin.dll 
   public const string TitleMedicalSearchOtherPatientIdsAddin                               = "Leadtools.Medical.SearchOtherPatientIDs.Addin"+ DllExt;
   public const string DescriptionMedicalSearchOtherPatientIdsAddin                         = "Leadtools.Medical.SearchOtherPatientIDs.Addin";
   public const string FileVersionMedicalSearchOtherPatientIdsAddin                         = "18.0.0.1";

   // Leadtools.Windows.Annotations.dll
   public const string TitleAnnotationsWinForms                                      = "Leadtools.Windows.Annotations" + DllExt;
   public const string DescriptionAnnotationsWinForms                                = "Leadtools.Windows.Annotations";
   public const string FileVersionAnnotationsWinForms                                = "18.0.0.1";

   //*********************************************************************
   //
   // Demo EXE
   //
   //*********************************************************************
   // DicomHighLevelClientDemo
   public const string TitleDicomHighLevelClientDemo                                        = "DicomHighLevelClientDemo" + ExeExt;
   public const string DescriptionDicomHighLevelClientDemo                                  = "DicomHighLevelClientDemo";
   public const string FileVersionDicomHighLevelClientDemo                                  = "18.0.0.12";

   // DicomHighLevelMwlScuDemo
   public const string TitleDicomHighLevelMwlScuDemo                                        = "DicomHighLevelMwlScuDemo"+ ExeExt;
   public const string DescriptionDicomHighLevelMwlScuDemo                                  = "DicomHighLevelMwlScuDemo";
   public const string FileVersionDicomHighLevelMwlScuDemo                                  = "18.0.0.4";
   
   // DicomHighlevelPatientUpdaterDemo
   public const string TitleDicomHighlevelPatientUpdaterDemo                                 = "DicomHighlevelPatientUpdaterDemo"+ ExeExt;
   public const string DescriptionDicomHighlevelPatientUpdaterDemo                           = "DicomHighlevelPatientUpdaterDemo";
   public const string FileVersionDicomHighlevelPatientUpdaterDemo                           = "18.0.0.8";

   // DicomHighlevelStoreDemo
   public const string TitleDicomHighlevelStoreDemo                                          = "DicomHighlevelStoreDemo"+ ExeExt;
   public const string DescriptionDicomHighlevelStoreDemo                                    = "DicomHighlevelStoreDemo";
   public const string FileVersionDicomHighlevelStoreDemo                                    = "18.0.0.8";

   // Leadtools.Dicom.Server.Manager
   public const string TitleLeadtoolsDicomServerManager                                      = "Leadtools.Dicom.Server.Manager"+ ExeExt;
   public const string DescriptionLeadtoolsDicomServerManager                                = "Leadtools.Dicom.Server.Manager";
   public const string FileVersionLeadtoolsDicomServerManager                                = "18.0.0.6";

   // MedicalWorkstationMainDemo
   public const string TitleMedicalWorkstationMainDemo                                       = "MedicalWorkstationMainDemo"+ ExeExt;
   public const string DescriptionMedicalWorkstationMainDemo                                 = "MedicalWorkstationMainDemo";
   public const string FileVersionMedicalWorkstationMainDemo                                 = "18.0.0.7";

   // PacsConfigDemo
   public const string TitlePacsConfigDemo                                                   = "PacsConfigDemo"+ ExeExt;
   public const string DescriptionPacsConfigDemo                                             = "PacsConfigDemo";
   public const string FileVersionPacsConfigDemo                                             = "18.0.0.23";

   // PacsDatabaseConfigurationDemo
   public const string TitlePacsDatabaseConfigurationDemo                                    = "PacsDatabaseConfigurationDemo"+ ExeExt;
   public const string DescriptionPacsDatabaseConfigurationDemo                              = "PacsDatabaseConfigurationDemo";
   public const string FileVersionPacsDatabaseConfigurationDemo                              = "18.0.0.38";

   // StorageServerManager
   public const string TitleStorageServerManager                                             = "StorageServerManager"+ ExeExt;
   public const string DescriptionStorageServerManager                                       = "StorageServerManager";
   public const string FileVersionStorageServerManager                                       = "18.0.0.33";

   // CCowClientDemo
   public const string TitleCCowClientDemo                                                   = "CCowClientDemo"+ ExeExt;
   public const string DescriptionCCowClientDemo                                             = "CCowClientDemo";
   public const string FileVersionCCowClientDemo                                             = "18.0.0.1";

   // ModalityWorklistWCFDemo
   public const string TitleModalityWorklistWCFDemo                                          = "ModalityWorklistWCFDemo"+ ExeExt;
   public const string DescriptionModalityWorklistWCFDemo                                    = "ModalityWorklistWCFDemo";
   public const string FileVersionModalityWorklistWCFDemo                                    = "18.0.0.2";

   // MPPSWCFDemo
   public const string TitleMPPSWCFDemo                                                      = "MPPSWCFDemo"+ ExeExt;
   public const string DescriptionMPPSWCFDemo                                                = "MPPSWCFDemo";
   public const string FileVersionMPPSWCFDemo                                                = "18.0.0.2";

   // ExternalControlSample
   public const string TitleExternalControlSample                                = "ExternalControlSample"+ ExeExt;
   public const string DescriptionExternalControlSample                          = "ExternalControlSample";
   public const string FileVersionExternalControlSample                          = "18.0.0.2";

   // WebViewerConfiguration
   public const string TitleWebViewerConfiguration                               = "WebViewerConfiguration"+ ExeExt;
   public const string DescriptionWebViewerConfiguration                         = "WebViewerConfiguration";
   public const string FileVersionWebViewerConfiguration                                     = "18.0.0.64";

#endif // #if  LTV18_CONFIG

#if LTV19_CONFIG
   public const string Culture                     = "";
   public const string Product                     = ProductPrefix + Platform;

#if FOR_DOTNET4
   public const string Version                     = "19.0.4.0";
#else
   public const string Version                     = "19.0.0.0";
#endif // FOR_DOTNET4

   // Leadtools.Medical.Winforms.dll
   public const string TitleMedicalWinforms                    = "Leadtools.Medical.WinForms" + DllExt;
   public const string DescriptionMedicalWinforms              = "Medical Windows Forms";
   public const string FileVersionMedicalWinforms              = "19.0.0.59";

   // Leadtools.Medical.Ae.Configuration.dll
   public const string TitleMedicalAeConfiguration             = "Leadtools.Medical.Ae.Configuration" + DllExt;
   public const string DescriptionMedicalAeConfiguration       = "Leadtools.Medical.Ae.Configuration";
   public const string FileVersionMedicalAeConfiguration       = "19.0.0.4";

   // Leadtools.Medical.AutoCopy.dll
   public const string TitleMedicalAutoCopyAddin               = "Leadtools.Medical.AutoCopy.AddIn" + DllExt;
   public const string DescriptionMedicalAutoCopyAddin         = "Leadtools.Medical.AutoCopy.AddIn";
   public const string FileVersionMedicalAutoCopyAddin         = "19.0.0.5";

   // Leadtools.Medical.ExternalStore.AddIn.dll
   public const string TitleMedicalExternalStoreAddin        = "Leadtools.Medical.ExternalStore.AddIn" + DllExt;
   public const string DescriptionMedicalExternalStoreAddin  = "Leadtools.Medical.ExternalStore.AddIn";
   public const string FileVersionMedicalExternalStoreAddin  = "19.0.0.5";

   // Leadtools.Medical.ExternalStore.Atmos.AddIn.dll
   public const string TitleMedicalExternalStoreAtmosAddin        = "Leadtools.Medical.ExternalStore.Atmos.AddIn" + DllExt;
   public const string DescriptionMedicalExternalStoreAtmosAddin  = "Leadtools.Medical.ExternalStore.Atmos.AddIn";
   public const string FileVersionMedicalExternalStoreAtmosAddin  = "19.0.0.2";

   // Leadtools.Medical.ExternalStore.Azure.AddIn.dll
   public const string TitleMedicalExternalStoreAzureAddin        = "Leadtools.Medical.ExternalStore.Azure.AddIn" + DllExt;
   public const string DescriptionMedicalExternalStoreAzureAddin  = "Leadtools.Medical.ExternalStore.Azure.AddIn";
   public const string FileVersionMedicalExternalStoreAzureAddin  = "19.0.0.2";

   // Leadtools.Medical.ExternalStore.Sample.AddIn.dll
   public const string TitleMedicalExternalStoreSampleAddin        = "Leadtools.Medical.ExternalStore.Sample.AddIn" + DllExt;
   public const string DescriptionMedicalExternalStoreSampleAddin  = "Leadtools.Medical.ExternalStore.Sample.AddIn";
   public const string FileVersionMedicalExternalStoreSampleAddin  = "19.0.0.5";

   // Leadtools.Medical.Forwarder.AddIn.dll
   public const string TitleMedicalForwarderAddin              = "Leadtools.Medical.Forwarder.AddIn" + DllExt;
   public const string DescriptionMedicalForwarderAddin        = "Leadtools.Medical.Forwarder.AddIn";
   public const string FileVersionMedicalForwarderAddin        = "19.0.0.8";

   // Leadtools.Medical.Gateway.AddIn.dll
   public const string TitleMedicalGatewayAddin                = "Leadtools.Medical.Gateway.AddIn" + DllExt;
   public const string DescriptionMedicalGatewayAddin          = "Leadtools.Medical.Gateway.AddIn";
   public const string FileVersionMedicalGatewayAddin          = "19.0.0.4";

   // Leadtools.Medical.License.Configuration.dll
   public const string TitleMedicalLicenseConfiguration        = "Leadtools.Medical.License.Configuration" + DllExt;
   public const string DescriptionMedicalLicenseConfiguration  = "Leadtools.Medical.License.Configuration";
   public const string FileVersionMedicalLicenseConfiguration  = "19.0.0.2";

   // Leadtools.Medical.Logging.AddIn.dll
   public const string TitleMedicalLoggingAddin                = "Leadtools.Medical.Logging.AddIn" + DllExt;
   public const string DescriptionMedicalLoggingAddin          = "Leadtools.Medical.Logging.AddIn";
   public const string FileVersionMedicalLoggingAddin          = "19.0.0.2";

   // Leadtools.Medical.Media.AddIns.dll
   public const string TitleMedicalMediaAddin                  = "Leadtools.Medical.Media.AddIns" + DllExt;
   public const string DescriptionMedicalMediaAddin            = "Leadtools.Medical.Media.AddIns";
   public const string FileVersionMedicalMediaAddin            = "19.0.0.4";
   
   // Leadtools.Medical.PatientUpdater.AddIn
   public const string TitleMedicalPatientUpdaterAddin         = "Leadtools.Medical.PatientUpdater.AddIn" + DllExt;
   public const string DescriptionMedicalPatientUpdaterAddin   = "Leadtools.Medical.PatientUpdater.AddIn";
   public const string FileVersionMedicalPatientUpdaterAddin   = "19.0.0.9";

   // Leadtools.Medical.Storage.AddIns.dll
   public const string TitleMedicalStorageAddin                = "Leadtools.Medical.Storage.AddIns" + DllExt;
   public const string DescriptionMedicalStorageAddin          = "Leadtools.Medical.Storage.AddIns";
   public const string FileVersionMedicalStorageAddin          = "19.0.0.24";

   // Leadtools.Medical.Worklist.AddIns.dll
   public const string TitleMedicalWorklistAddin               = "Leadtools.Medical.Worklist.AddIns" + DllExt;
   public const string DescriptionMedicalWorklistAddin         = "Leadtools.Medical.Worklist.AddIns";
   public const string FileVersionMedicalWorklistAddin         = "19.0.0.5";

   // Leadtools.Medical.AeManagement.DataAccessLayer.dll
   public const string TitleMedicalAeManagementDataAccessLayer = "Leadtools.Medical.AeManagement.DataAccessLayer" + DllExt;
   public const string DescriptionAeManagementDataAccessLayer  = "Leadtools.Medical.AeManagement.DataAccessLayer";
   public const string FileVersionAeManagementDataAccessLayer  = "19.0.0.5";

   // Leadtools.Medical.Forward.DataAccessLayer.dll
   public const string TitleMedicalForwardDataAccessLayer                            = "Leadtools.Medical.Forward.DataAccessLayer" + DllExt;
   public const string DescriptionMedicalForwardDataAccessLayer                      = "Leadtools.Medical.Forward.DataAccessLayer";
   public const string FileVersionMedicalForwardDataAccessLayer                      = "19.0.0.1";
      
   // Leadtools.DataAccessLayers.dll
   public const string TitleDataAccessLayers                            = "Leadtools.DataAccessLayers" + DllExt;
   public const string DescriptionDataAccessLayers                      = "Leadtools.DataAccessLayers";
   public const string FileVersionDataAccessLayers                      = "19.0.0.6";
   
   // Leadtools.DataAccessLayers.Core
   public const string TitleDataAccessLayersCore1                                       = "Leadtools.DataAccessLayers.Core" + DllExt;
   public const string DescriptionDataAccessLayersCore1                                 = "Leadtools.DataAccessLayers.Core";
   public const string FileVersionDataAccessLayersCore1                                 = "19.0.0.3";
   
   // Leadtools.Medical.Options.DataAccessLayer.dll
   public const string TitleMedicalOptionsDataAccessLayer                            = "Leadtools.Medical.Options.DataAccessLayer" + DllExt;
   public const string DescriptionMedicalOptionsDataAccessLayer                      = "Leadtools.Medical.Options.DataAccessLayer";
   public const string FileVersionMedicalOptionsDataAccessLayer                      = "19.0.0.4";

   // Leadtools.Medical.PermissionsManagement.DataAccessLayer.dll
   public const string TitleMedicalPermissionsManagementDataAccessLayer              = "Leadtools.Medical.PermissionsManagement.DataAccessLayer" + DllExt;
   public const string DescriptionMedicalPermissionsManagementDataAccessLayer        = "Leadtools.Medical.PermissionsManagement.DataAccessLayer";
   public const string FileVersionMedicalPermissionsManagementDataAccessLayer        = "19.0.0.2";

   // Leadtools.Medical.UserManagementDataAccessLayer.dll
   public const string TitleMedicalUserManagementDataAccessLayer                     = "Leadtools.Medical.UserManagementDataAccessLayer" + DllExt;
   public const string DescriptionMedicalUserManagementDataAccessLayer               = "Leadtools.Medical.UserManagementDataAccessLayer";
   public const string FileVersionMedicalUserManagementDataAccessLayer               = "19.0.0.4";

   // Leadtools.Medical.Rules.Addin.dll
   public const string TitleMedicalRulesAddin                                        = "Leadtools.Medical.Rules.Addin" + DllExt;
   public const string DescriptionMedicalRulesAddin                                  = "Leadtools.Medical.Rules.Addin";
   public const string FileVersionMedicalRulesAddin                                  = "19.0.0.2";

   // Leadtools.Medical.DataAccessLayersCore
   public const string TitleDataAccessLayersCore                                       = "Leadtools.Medical.DataAccessLayersCore" + DllExt;
   public const string DescriptionDataAccessLayersCore                                 = "Leadtools.Medical.DataAccessLayersCore";
   public const string FileVersionDataAccessLayersCore                                 = "19.0.0.3";

   // Leadtools.Medical.HL7MWL.AddIn
   public const string TitleMedicalHL7MWLAddIn                                         = "Leadtools.Medical.HL7MWL.AddIn" + DllExt;
   public const string DescriptionMedicalHL7MWLAddIn                                   = "Leadtools.Medical.HL7MWL.AddIn";
   public const string FileVersionMedicalHL7MWLAddIn                                   = "19.0.0.1";

   // Leadtools.Medical.HL7MWL.AddIn
   public const string TitleMedicalHL7PatientUpdateAddIn                               = "Leadtools.Medical.HL7PatientUpdate.AddIn" + DllExt;
   public const string DescriptionMedicalHL7PatientUpdateAddIn                         = "Leadtools.Medical.HL7PatientUpdate.AddIn";
   public const string FileVersionMedicalHL7PatientUpdateAddIn                         = "19.0.0.1";

   // Leadtools.Medical.WebViewer.Addins
   public const string TitleMedicalWebViewerAddins                                     = "Leadtools.Medical.WebViewer.Addins" + DllExt;
   public const string DescriptionMedicalWebViewerAddins                               = "Leadtools.Medical.WebViewer.Addins";
   public const string FileVersionMedicalWebViewerAddins                               = "19.0.0.41";

   // Leadtools.Medical.WebViewer.Annotations.DataAccessLayer
   public const string TitleMedicalWebViewerAnnotationsDataAccessLayer                                 = "Leadtools.Medical.WebViewer.Annotations.DataAccessLayer" + DllExt;
   public const string DescriptionMedicalWebViewerAnnotationsDataAccessLayer                           = "Leadtools.Medical.WebViewer.Annotations.DataAccessLayer";
   public const string FileVersionMedicalWebViewerAnnotationsDataAccessLayer                           = "19.0.0.1";

   // Leadtools.Medical.WebViewer.Core
   public const string TitleMedicalWebViewerCore                               = "Leadtools.Medical.WebViewer.Core" + DllExt;
   public const string DescriptionMedicalWebViewerCore                         = "Leadtools.Medical.WebViewer.Core";
   public const string FileVersionMedicalWebViewerCore                         = "19.0.0.20";

   // Leadtools.Medical.WebViewer.Wado
   public const string TitleMedicalWebViewerWado                               = "Leadtools.Medical.WebViewer.Wado" + DllExt;
   public const string DescriptionMedicalWebViewerWado                         = "Leadtools.Medical.WebViewer.Wado";
   public const string FileVersionMedicalWebViewerWado                         = "19.0.0.1";

   // Leadtools.Medical.WebViewer.ImageDownloadAddin
   public const string TitleMedicalWebViewerImageDownloadAddin                                = "Leadtools.Medical.WebViewer.ImageDownloadAddin" + DllExt;
   public const string DescriptionMedicalWebViewerImageDownloadAddin                          = "Leadtools.Medical.WebViewer.ImageDownloadAddin";
   public const string FileVersionMedicalWebViewerImageDownloadAddin                          = "19.0.0.2";

   // Leadtools.Medical.WebViewer.Jobs
   public const string TitleMedicalWebViewerJobs                               = "Leadtools.Medical.WebViewer.Jobs" + DllExt;
   public const string DescriptionMedicalWebViewerJobs                         = "Leadtools.Medical.WebViewer.Jobs";
   public const string FileVersionMedicalWebViewerJobs                         = "19.0.0.1";

   // Leadtools.Medical.WebViewer.JobsCleanupAddin.dll
   public const string TitleMedicalWebViewerJobsCleanupAddin                    = "Leadtools.Medical.WebViewer.JobsCleanupAddin" + DllExt;
   public const string DescriptionMedicalWebViewerJobsCleanupAddin              = "Leadtools.Medical.WebViewer.JobsCleanupAddin";
   public const string FileVersionMedicalWebViewerJobsCleanupAddin              = "19.0.0.1";

   // Leadtools.Medical.WebViewer.PatientAccessRights.DataAccessAgent
   public const string TitleMedicalWebViewerPatientAccessRightsDataAccessAgent                               = "Leadtools.Medical.WebViewer.PatientAccessRights.DataAccessAgent" + DllExt;
   public const string DescriptionMedicalWebViewerPatientAccessRightsDataAccessAgent                         = "Leadtools.Medical.WebViewer.PatientAccessRights.DataAccessAgent";
   public const string FileVersionMedicalWebViewerPatientAccessRightsDataAccessAgent                         = "19.0.0.5";

   // Leadtools.Medical.WebViewer.WCF
   public const string TitleMedicalWebViewerWCF                               = "Leadtools.Medical.WebViewer.WCF"+ DllExt;
   public const string DescriptionMedicalWebViewerWCF                         = "Leadtools.Medical.WebViewer.WCF";
   public const string FileVersionMedicalWebViewerWCF                         = "19.0.0.20";

   // Leadtools.Medical.WebViewer.ExternalControl
   public const string TitleMedicalWebViewerExternalControl                               = "Leadtools.Medical.WebViewer.ExternalControl"+ DllExt;
   public const string DescriptionMedicalWebViewerExternalControl                         = "Leadtools.Medical.WebViewer.ExternalControl";
   public const string FileVersionMedicalWebViewerExternalControl                         = "19.0.0.5";

   // Leadtools.MedicalViewer.CS.WebForms
   public const string TitleMedicalViewerCSWebForms                               = "MedicalViewer.CS.WebForms"+ DllExt;
   public const string DescriptionMedicalViewerCSWebForms                         = "MedicalViewer.CS.WebForms";
   public const string FileVersionMedicalViewerCSWebForms                         = "19.0.0.1";

   // Leadtools.Medical.SearchOtherPatientIDs.Addin.dll 
   public const string TitleMedicalSearchOtherPatientIdsAddin                               = "Leadtools.Medical.SearchOtherPatientIDs.Addin"+ DllExt;
   public const string DescriptionMedicalSearchOtherPatientIdsAddin                         = "Leadtools.Medical.SearchOtherPatientIDs.Addin";
   public const string FileVersionMedicalSearchOtherPatientIdsAddin                         = "19.0.0.1";

   // Leadtools.Windows.Annotations.dll
   public const string TitleAnnotationsWinForms                                             = "Leadtools.Windows.Annotations" + DllExt;
   public const string DescriptionAnnotationsWinForms                                       = "Leadtools.Windows.Annotations";
   public const string FileVersionAnnotationsWinForms                                       = "19.0.0.1";

   // Leadtools.SDCreator.dll
   public const string TitleLeadtoolsSDCreator                                              = "Leadtools.SDCreator" + DllExt;
   public const string DescriptionLeadtoolsSDCreator                                        = "Automatically converts Dental Imgages with anatomic information to a structured display dataset";
   public const string FileVersionLeadtoolsSDCreator                                        = "19.0.0.0";

   // Leadtools.AddIn.Find.dll
   public const string TitleLeadtoolsAddInFind = "Leadtools.AddIn.Find" + DllExt;
   public const string DescriptionLeadtoolsAddInFind = "Leadtools.AddIn.Find";
   public const string FileVersionLeadtoolsAddInFind = "19.0.0.1";

   // Leadtools.AddIn.Move.dll
   public const string TitleLeadtoolsAddInMove = "Leadtools.AddIn.Move" + DllExt;
   public const string DescriptionLeadtoolsAddInMove = "Leadtools.AddIn.Move";
   public const string FileVersionLeadtoolsAddInMove = "19.0.0.1";

   // Leadtools.AddIn.Security.dll
   public const string TitleLeadtoolsAddInSecurity = "Leadtools.AddIn.Security" + DllExt;
   public const string DescriptionLeadtoolsAddInSecurity = "Leadtools.AddIn.Security";
   public const string FileVersionLeadtoolsAddInSecurity = "19.0.0.1";

   // Leadtools.AddIn.StorageCommit.dll
   public const string TitleLeadtoolsAddInStorageCommit = "Leadtools.AddIn.StorageCommit" + DllExt;
   public const string DescriptionLeadtoolsAddInStorageCommit = "Leadtools.AddIn.StorageCommit";
   public const string FileVersionLeadtoolsAddInStorageCommit = "19.0.0.1";

   // Leadtools.AddIn.Store.dll
   public const string TitleLeadtoolsAddInStore = "Leadtools.AddIn.Store" + DllExt;
   public const string DescriptionLeadtoolsAddInStore = "Leadtools.AddIn.Store";
   public const string FileVersionLeadtoolsAddInStore = "19.0.0.1";

   // Leadtools.Configuration.Logging.dll
   public const string TitleLeadtoolsConfigurationLogging = "Leadtools.Configuration.Logging" + DllExt;
   public const string DescriptionLeadtoolsConfigurationLogging = "Leadtools.Configuration.Logging";
   public const string FileVersionLeadtoolsConfigurationLogging = "19.0.0.1";

   // Leadtools.AddIn.AutoConfigure.dll
   public const string TitleLeadtoolsAddInAutoConfigure = "Leadtools.AddIn.AutoConfigure" + DllExt;
   public const string DescriptionLeadtoolsAddInAutoConfigure = "Leadtools.AddIn.AutoConfigure";
   public const string FileVersionLeadtoolsAddInAutoConfigure = "19.0.0.1";

   // Leadtools.AddIn.MWLFind.dll
   public const string TitleLeadtoolsAddInMWLFind = "Leadtools.AddIn.MWLFind" + DllExt;
   public const string DescriptionLeadtoolsAddInMWLFind = "Leadtools.AddIn.MWLFind";
   public const string FileVersionLeadtoolsAddInMWLFind = "19.0.0.1";

   // Leadtools.AddIn.DicomLog.dll
   public const string TitleLeadtoolsAddInDicomLog = "Leadtools.AddIn.DicomLog" + DllExt;
   public const string DescriptionLeadtoolsAddInDicomLog = "Leadtools.AddIn.DicomLog";
   public const string FileVersionLeadtoolsAddInDicomLog = "19.0.0.1";

   // Leadtools.AddIn.Performance.dll
   public const string TitleLeadtoolsAddInPerformance = "Leadtools.AddIn.Performance" + DllExt;
   public const string DescriptionLeadtoolsAddInPerformance = "Leadtools.AddIn.Performance";
   public const string FileVersionLeadtoolsAddInPerformance = "19.0.0.1";


   //*********************************************************************
   //
   // Demo EXE
   //
   //*********************************************************************
   // DicomHighLevelClientDemo
   public const string TitleDicomHighLevelClientDemo                                        = "DicomHighLevelClientDemo"+ ExeExt;
   public const string DescriptionDicomHighLevelClientDemo                                  = "DicomHighLevelClientDemo";
   public const string FileVersionDicomHighLevelClientDemo                                  = "19.0.0.12";

   // DicomHighLevelMwlScuDemo
   public const string TitleDicomHighLevelMwlScuDemo                                        = "DicomHighLevelMwlScuDemo"+ ExeExt;
   public const string DescriptionDicomHighLevelMwlScuDemo                                  = "DicomHighLevelMwlScuDemo";
   public const string FileVersionDicomHighLevelMwlScuDemo                                  = "19.0.0.4";
   
   // DicomHighlevelPatientUpdaterDemo
   public const string TitleDicomHighlevelPatientUpdaterDemo                                 = "DicomHighlevelPatientUpdaterDemo"+ ExeExt;
   public const string DescriptionDicomHighlevelPatientUpdaterDemo                           = "DicomHighlevelPatientUpdaterDemo";
   public const string FileVersionDicomHighlevelPatientUpdaterDemo                           = "19.0.0.10";

   // DicomHighlevelStoreDemo
   public const string TitleDicomHighlevelStoreDemo                                          = "DicomHighlevelStoreDemo"+ ExeExt;
   public const string DescriptionDicomHighlevelStoreDemo                                    = "DicomHighlevelStoreDemo";
   public const string FileVersionDicomHighlevelStoreDemo                                    = "19.0.0.8";

   // Leadtools.Dicom.Server.Manager
   public const string TitleLeadtoolsDicomServerManager                                      = "Leadtools.Dicom.Server.Manager"+ ExeExt;
   public const string DescriptionLeadtoolsDicomServerManager                                = "Leadtools.Dicom.Server.Manager";
   public const string FileVersionLeadtoolsDicomServerManager                                = "19.0.0.7";

   // MedicalWorkstationMainDemo
   public const string TitleMedicalWorkstationMainDemo                                       = "MedicalWorkstationMainDemo"+ ExeExt;
   public const string DescriptionMedicalWorkstationMainDemo                                 = "MedicalWorkstationMainDemo";
   public const string FileVersionMedicalWorkstationMainDemo                                 = "19.0.0.8";

   // PacsConfigDemo
   public const string TitlePacsConfigDemo                                                   = "PacsConfigDemo"+ ExeExt;
   public const string DescriptionPacsConfigDemo                                             = "PacsConfigDemo";
   public const string FileVersionPacsConfigDemo                                             = "19.0.0.23";

   // PacsDatabaseConfigurationDemo
   public const string TitlePacsDatabaseConfigurationDemo                                    = "PacsDatabaseConfigurationDemo"+ ExeExt;
   public const string DescriptionPacsDatabaseConfigurationDemo                              = "PacsDatabaseConfigurationDemo";
   public const string FileVersionPacsDatabaseConfigurationDemo                              = "19.0.0.44";

   // StorageServerManager
   public const string TitleStorageServerManager                                             = "StorageServerManager"+ ExeExt;
   public const string DescriptionStorageServerManager                                       = "StorageServerManager";
   public const string FileVersionStorageServerManager                                       = "19.0.0.39";

   // CCowClientDemo
   public const string TitleCCowClientDemo                                 = "CCowClientDemo"+ ExeExt;
   public const string DescriptionCCowClientDemo                           = "CCowClientDemo";
   public const string FileVersionCCowClientDemo                           = "19.0.0.1";

   // ModalityWorklistWCFDemo
   public const string TitleModalityWorklistWCFDemo                                 = "ModalityWorklistWCFDemo"+ ExeExt;
   public const string DescriptionModalityWorklistWCFDemo                           = "ModalityWorklistWCFDemo";
   public const string FileVersionModalityWorklistWCFDemo                           = "19.0.0.2";

   // MPPSWCFDemo
   public const string TitleMPPSWCFDemo                                 = "MPPSWCFDemo"+ ExeExt;
   public const string DescriptionMPPSWCFDemo                           = "MPPSWCFDemo";
   public const string FileVersionMPPSWCFDemo                           = "19.0.0.2";

   // ExternalControlSample
   public const string TitleExternalControlSample                                = "ExternalControlSample"+ ExeExt;
   public const string DescriptionExternalControlSample                          = "ExternalControlSample";
   public const string FileVersionExternalControlSample                          = "19.0.0.2";

   // WebViewerConfiguration
   public const string TitleWebViewerConfiguration                               = "WebViewerConfiguration"+ ExeExt;
   public const string DescriptionWebViewerConfiguration                         = "WebViewerConfiguration";
   public const string FileVersionWebViewerConfiguration                         = "19.0.0.71";

   // ChangeMedicalViewerToDental
   public const string TitleChangeMedicalViewerToDental                          = "ChangeMedicalViewerToDental"+ ExeExt;
   public const string DescriptionChangeMedicalViewerToDental                    = "ChangeMedicalViewerToDental";
   public const string FileVersionChangeMedicalViewerToDental                    = "19.0.0.1";

   // LeadtoolsServicesHostManager
   public const string TitleLeadtoolsServicesHostManager                               = "LeadtoolsServicesHostManager"+ ExeExt;
   public const string DescriptionLeadtoolsServicesHostManager                         = "LeadtoolsServicesHostManager";
   public const string FileVersionLeadtoolsServicesHostManager                         = "19.0.0.15";

   // Leadtools.Medical.ViewImage
   public const string TitleMedicalViewImage = "Leadtools.Medical.ViewImage" + DllExt;
   public const string DescriptionMedicalViewImage = "Leadtools.Medical.ViewImage";
   public const string FileVersionMedicalViewImage = "19.0.0.1";

#endif // LTV19_CONFIG


#if LTV20_CONFIG
   public const string Culture = "";
   public const string Product = ProductPrefix + Platform;

#if FOR_DOTNET4
   public const string Version = "20.0.4.0";
#else
   public const string Version                     = "20.0.0.0";
#endif // FOR_DOTNET4

   // Leadtools.Medical.Winforms.dll
   public const string TitleMedicalWinforms = "Leadtools.Medical.WinForms" + DllExt;
   public const string DescriptionMedicalWinforms = "Medical Windows Forms";
   public const string FileVersionMedicalWinforms = "20.0.0.24";

   // Leadtools.Medical.Ae.Configuration.dll
   public const string TitleMedicalAeConfiguration = "Leadtools.Medical.Ae.Configuration" + DllExt;
   public const string DescriptionMedicalAeConfiguration = "Leadtools.Medical.Ae.Configuration";
   public const string FileVersionMedicalAeConfiguration = "20.0.0.3";

   // Leadtools.Medical.AutoCopy.dll
   public const string TitleMedicalAutoCopyAddin = "Leadtools.Medical.AutoCopy.AddIn" + DllExt;
   public const string DescriptionMedicalAutoCopyAddin = "Leadtools.Medical.AutoCopy.AddIn";
   public const string FileVersionMedicalAutoCopyAddin = "20.0.0.4";

   // Leadtools.Medical.ExternalStore.AddIn.dll
   public const string TitleMedicalExternalStoreAddin = "Leadtools.Medical.ExternalStore.AddIn" + DllExt;
   public const string DescriptionMedicalExternalStoreAddin = "Leadtools.Medical.ExternalStore.AddIn";
   public const string FileVersionMedicalExternalStoreAddin = "20.0.0.1";

   // Leadtools.Medical.ExternalStore.Atmos.AddIn.dll
   public const string TitleMedicalExternalStoreAtmosAddin = "Leadtools.Medical.ExternalStore.Atmos.AddIn" + DllExt;
   public const string DescriptionMedicalExternalStoreAtmosAddin = "Leadtools.Medical.ExternalStore.Atmos.AddIn";
   public const string FileVersionMedicalExternalStoreAtmosAddin = "20.0.0.1";

   // Leadtools.Medical.ExternalStore.Azure.AddIn.dll
   public const string TitleMedicalExternalStoreAzureAddin = "Leadtools.Medical.ExternalStore.Azure.AddIn" + DllExt;
   public const string DescriptionMedicalExternalStoreAzureAddin = "Leadtools.Medical.ExternalStore.Azure.AddIn";
   public const string FileVersionMedicalExternalStoreAzureAddin = "20.0.0.1";

   // Leadtools.Medical.ExternalStore.AmazonS3.AddIn.dll
   public const string TitleMedicalExternalStoreAmazonS3Addin = "Leadtools.Medical.ExternalStore.AmazonS3.AddIn" + DllExt;
   public const string DescriptionMedicalExternalStoreAmazonS3Addin = "Leadtools.Medical.ExternalStore.AmazonS3.AddIn";
   public const string FileVersionMedicalExternalStoreAmazonS3Addin = "20.0.0.2";

   // Leadtools.Medical.ExternalStore.Sample.AddIn.dll
   public const string TitleMedicalExternalStoreSampleAddin = "Leadtools.Medical.ExternalStore.Sample.AddIn" + DllExt;
   public const string DescriptionMedicalExternalStoreSampleAddin = "Leadtools.Medical.ExternalStore.Sample.AddIn";
   public const string FileVersionMedicalExternalStoreSampleAddin = "20.0.0.2";

   // Leadtools.Medical.Forwarder.AddIn.dll
   public const string TitleMedicalForwarderAddin = "Leadtools.Medical.Forwarder.AddIn" + DllExt;
   public const string DescriptionMedicalForwarderAddin = "Leadtools.Medical.Forwarder.AddIn";
   public const string FileVersionMedicalForwarderAddin = "20.0.0.5";

   // Leadtools.Medical.Gateway.AddIn.dll
   public const string TitleMedicalGatewayAddin = "Leadtools.Medical.Gateway.AddIn" + DllExt;
   public const string DescriptionMedicalGatewayAddin = "Leadtools.Medical.Gateway.AddIn";
   public const string FileVersionMedicalGatewayAddin = "20.0.0.2";

   // Leadtools.Medical.License.Configuration.dll
   public const string TitleMedicalLicenseConfiguration = "Leadtools.Medical.License.Configuration" + DllExt;
   public const string DescriptionMedicalLicenseConfiguration = "Leadtools.Medical.License.Configuration";
   public const string FileVersionMedicalLicenseConfiguration = "20.0.0.1";

   // Leadtools.Medical.Logging.AddIn.dll
   public const string TitleMedicalLoggingAddin = "Leadtools.Medical.Logging.AddIn" + DllExt;
   public const string DescriptionMedicalLoggingAddin = "Leadtools.Medical.Logging.AddIn";
   public const string FileVersionMedicalLoggingAddin = "20.0.0.1";

   // Leadtools.Medical.Media.AddIns.dll
   public const string TitleMedicalMediaAddin = "Leadtools.Medical.Media.AddIns" + DllExt;
   public const string DescriptionMedicalMediaAddin = "Leadtools.Medical.Media.AddIns";
   public const string FileVersionMedicalMediaAddin = "20.0.0.1";

   // Leadtools.Medical.PatientUpdater.AddIn
   public const string TitleMedicalPatientUpdaterAddin = "Leadtools.Medical.PatientUpdater.AddIn" + DllExt;
   public const string DescriptionMedicalPatientUpdaterAddin = "Leadtools.Medical.PatientUpdater.AddIn";
   public const string FileVersionMedicalPatientUpdaterAddin = "20.0.0.2";

   // Leadtools.Medical.Storage.AddIns.dll
   public const string TitleMedicalStorageAddin = "Leadtools.Medical.Storage.AddIns" + DllExt;
   public const string DescriptionMedicalStorageAddin = "Leadtools.Medical.Storage.AddIns";
   public const string FileVersionMedicalStorageAddin = "20.0.0.2";

   // Leadtools.Medical.Worklist.AddIns.dll
   public const string TitleMedicalWorklistAddin = "Leadtools.Medical.Worklist.AddIns" + DllExt;
   public const string DescriptionMedicalWorklistAddin = "Leadtools.Medical.Worklist.AddIns";
   public const string FileVersionMedicalWorklistAddin = "20.0.0.1";

   // Leadtools.Medical.AeManagement.DataAccessLayer.dll
   public const string TitleMedicalAeManagementDataAccessLayer = "Leadtools.Medical.AeManagement.DataAccessLayer" + DllExt;
   public const string DescriptionAeManagementDataAccessLayer = "Leadtools.Medical.AeManagement.DataAccessLayer";
   public const string FileVersionAeManagementDataAccessLayer = "20.0.0.4";

   // Leadtools.Medical.Forward.DataAccessLayer.dll
   public const string TitleMedicalForwardDataAccessLayer = "Leadtools.Medical.Forward.DataAccessLayer" + DllExt;
   public const string DescriptionMedicalForwardDataAccessLayer = "Leadtools.Medical.Forward.DataAccessLayer";
   public const string FileVersionMedicalForwardDataAccessLayer = "20.0.0.1";

   // Leadtools.DataAccessLayers.dll
   public const string TitleDataAccessLayers = "Leadtools.DataAccessLayers" + DllExt;
   public const string DescriptionDataAccessLayers = "Leadtools.DataAccessLayers";
   public const string FileVersionDataAccessLayers = "20.0.0.1";

   // Leadtools.DataAccessLayers.Core
   public const string TitleDataAccessLayersCore1 = "Leadtools.DataAccessLayers.Core" + DllExt;
   public const string DescriptionDataAccessLayersCore1 = "Leadtools.DataAccessLayers.Core";
   public const string FileVersionDataAccessLayersCore1 = "20.0.0.1";

   // Leadtools.Medical.Options.DataAccessLayer.dll
   public const string TitleMedicalOptionsDataAccessLayer = "Leadtools.Medical.Options.DataAccessLayer" + DllExt;
   public const string DescriptionMedicalOptionsDataAccessLayer = "Leadtools.Medical.Options.DataAccessLayer";
   public const string FileVersionMedicalOptionsDataAccessLayer = "20.0.0.2";

   // Leadtools.Medical.PermissionsManagement.DataAccessLayer.dll
   public const string TitleMedicalPermissionsManagementDataAccessLayer = "Leadtools.Medical.PermissionsManagement.DataAccessLayer" + DllExt;
   public const string DescriptionMedicalPermissionsManagementDataAccessLayer = "Leadtools.Medical.PermissionsManagement.DataAccessLayer";
   public const string FileVersionMedicalPermissionsManagementDataAccessLayer = "20.0.0.3";

   // Leadtools.Medical.UserManagementDataAccessLayer.dll
   public const string TitleMedicalUserManagementDataAccessLayer = "Leadtools.Medical.UserManagementDataAccessLayer" + DllExt;
   public const string DescriptionMedicalUserManagementDataAccessLayer = "Leadtools.Medical.UserManagementDataAccessLayer";
   public const string FileVersionMedicalUserManagementDataAccessLayer = "20.0.0.2";

   // Leadtools.Medical.Rules.Addin.dll
   public const string TitleMedicalRulesAddin = "Leadtools.Medical.Rules.Addin" + DllExt;
   public const string DescriptionMedicalRulesAddin = "Leadtools.Medical.Rules.Addin";
   public const string FileVersionMedicalRulesAddin = "20.0.0.2";

   // Leadtools.Medical.DataAccessLayersCore
   public const string TitleDataAccessLayersCore = "Leadtools.Medical.DataAccessLayersCore" + DllExt;
   public const string DescriptionDataAccessLayersCore = "Leadtools.Medical.DataAccessLayersCore";
   public const string FileVersionDataAccessLayersCore = "20.0.0.1";

   // Leadtools.Medical.HL7MWL.AddIn
   public const string TitleMedicalHL7MWLAddIn = "Leadtools.Medical.HL7MWL.AddIn" + DllExt;
   public const string DescriptionMedicalHL7MWLAddIn = "Leadtools.Medical.HL7MWL.AddIn";
   public const string FileVersionMedicalHL7MWLAddIn = "20.0.0.1";

   // Leadtools.Medical.HL7PatientUpdate.AddIn
   public const string TitleMedicalHL7PatientUpdateAddIn = "Leadtools.Medical.HL7PatientUpdate.AddIn" + DllExt;
   public const string DescriptionMedicalHL7PatientUpdateAddIn = "Leadtools.Medical.HL7PatientUpdate.AddIn";
   public const string FileVersionMedicalHL7PatientUpdateAddIn = "20.0.0.2";

   // Leadtools.Medical.WebViewer.Addins
   public const string TitleMedicalWebViewerAddins = "Leadtools.Medical.WebViewer.Addins" + DllExt;
   public const string DescriptionMedicalWebViewerAddins = "Leadtools.Medical.WebViewer.Addins";
   public const string FileVersionMedicalWebViewerAddins = "20.0.0.6";

   // Leadtools.Medical.WebViewer.Annotations.DataAccessLayer
   public const string TitleMedicalWebViewerAnnotationsDataAccessLayer = "Leadtools.Medical.WebViewer.Annotations.DataAccessLayer" + DllExt;
   public const string DescriptionMedicalWebViewerAnnotationsDataAccessLayer = "Leadtools.Medical.WebViewer.Annotations.DataAccessLayer";
   public const string FileVersionMedicalWebViewerAnnotationsDataAccessLayer = "20.0.0.1";

   // Leadtools.Medical.WebViewer.Core
   public const string TitleMedicalWebViewerCore = "Leadtools.Medical.WebViewer.Core" + DllExt;
   public const string DescriptionMedicalWebViewerCore = "Leadtools.Medical.WebViewer.Core";
   public const string FileVersionMedicalWebViewerCore = "20.0.0.3";

   // Leadtools.Medical.WebViewer.Wado
   public const string TitleMedicalWebViewerWado = "Leadtools.Medical.WebViewer.Wado" + DllExt;
   public const string DescriptionMedicalWebViewerWado = "Leadtools.Medical.WebViewer.Wado";
   public const string FileVersionMedicalWebViewerWado = "20.0.0.1";

   // Leadtools.Medical.WebViewer.ImageDownloadAddin
   public const string TitleMedicalWebViewerImageDownloadAddin = "Leadtools.Medical.WebViewer.ImageDownloadAddin" + DllExt;
   public const string DescriptionMedicalWebViewerImageDownloadAddin = "Leadtools.Medical.WebViewer.ImageDownloadAddin";
   public const string FileVersionMedicalWebViewerImageDownloadAddin = "20.0.0.1";

   // Leadtools.Medical.WebViewer.Jobs
   public const string TitleMedicalWebViewerJobs = "Leadtools.Medical.WebViewer.Jobs" + DllExt;
   public const string DescriptionMedicalWebViewerJobs = "Leadtools.Medical.WebViewer.Jobs";
   public const string FileVersionMedicalWebViewerJobs = "20.0.0.1";

   // Leadtools.Medical.WebViewer.JobsCleanupAddin.dll
   public const string TitleMedicalWebViewerJobsCleanupAddin = "Leadtools.Medical.WebViewer.JobsCleanupAddin" + DllExt;
   public const string DescriptionMedicalWebViewerJobsCleanupAddin = "Leadtools.Medical.WebViewer.JobsCleanupAddin";
   public const string FileVersionMedicalWebViewerJobsCleanupAddin = "20.0.0.1";

   // Leadtools.Medical.WebViewer.PatientAccessRights.DataAccessAgent
   public const string TitleMedicalWebViewerPatientAccessRightsDataAccessAgent = "Leadtools.Medical.WebViewer.PatientAccessRights.DataAccessAgent" + DllExt;
   public const string DescriptionMedicalWebViewerPatientAccessRightsDataAccessAgent = "Leadtools.Medical.WebViewer.PatientAccessRights.DataAccessAgent";
   public const string FileVersionMedicalWebViewerPatientAccessRightsDataAccessAgent = "20.0.0.3";

   // Leadtools.Medical.WebViewer.WCF
   public const string TitleMedicalWebViewerWCF = "Leadtools.Medical.WebViewer.WCF" + DllExt;
   public const string DescriptionMedicalWebViewerWCF = "Leadtools.Medical.WebViewer.WCF";
   public const string FileVersionMedicalWebViewerWCF = "20.0.0.6";

   // Leadtools.Medical.WebViewer.Asp
   public const string TitleMedicalWebViewerAsp = "Leadtools.Medical.WebViewer.Asp" + DllExt;
   public const string DescriptionMedicalWebViewerAsp = "Leadtools.Medical.WebViewer.Asp";
   public const string FileVersionMedicalWebViewerAsp = "20.0.0.7";

   // Leadtools.Medical.WebViewer.IdP
   public const string TitleMedicalWebViewerIdP = "Leadtools.Medical.WebViewer.IdP" + DllExt;
   public const string DescriptionMedicalWebViewerIdP = "Leadtools.Medical.WebViewer.IdP";
   public const string FileVersionMedicalWebViewerIdP = "20.0.0.1";

   // Leadtools.Medical.WebViewer.ExternalControl
   public const string TitleMedicalWebViewerExternalControl = "Leadtools.Medical.WebViewer.ExternalControl" + DllExt;
   public const string DescriptionMedicalWebViewerExternalControl = "Leadtools.Medical.WebViewer.ExternalControl";
   public const string FileVersionMedicalWebViewerExternalControl = "20.0.0.1";

   // Leadtools.MedicalViewer.CS.WebForms
   public const string TitleMedicalViewerCSWebForms = "MedicalViewer.CS.WebForms" + DllExt;
   public const string DescriptionMedicalViewerCSWebForms = "MedicalViewer.CS.WebForms";
   public const string FileVersionMedicalViewerCSWebForms = "20.0.0.1";

   // Leadtools.Medical.SearchOtherPatientIDs.Addin.dll 
   public const string TitleMedicalSearchOtherPatientIdsAddin = "Leadtools.Medical.SearchOtherPatientIDs.Addin" + DllExt;
   public const string DescriptionMedicalSearchOtherPatientIdsAddin = "Leadtools.Medical.SearchOtherPatientIDs.Addin";
   public const string FileVersionMedicalSearchOtherPatientIdsAddin = "20.0.0.1";

   // Leadtools.Windows.Annotations.dll
   public const string TitleAnnotationsWinForms = "Leadtools.Windows.Annotations" + DllExt;
   public const string DescriptionAnnotationsWinForms = "Leadtools.Windows.Annotations";
   public const string FileVersionAnnotationsWinForms = "20.0.0.1";

   // Leadtools.SDCreator.dll
   public const string TitleLeadtoolsSDCreator = "Leadtools.SDCreator" + DllExt;
   public const string DescriptionLeadtoolsSDCreator = "Automatically converts Dental Images with anatomic information to a structured display dataset";
   public const string FileVersionLeadtoolsSDCreator = "20.0.0.1";

   // Leadtools.AddIn.Find.dll
   public const string TitleLeadtoolsAddInFind = "Leadtools.AddIn.Find" + DllExt;
   public const string DescriptionLeadtoolsAddInFind = "Leadtools.AddIn.Find";
   public const string FileVersionLeadtoolsAddInFind = "20.0.0.1";

   // Leadtools.AddIn.Move.dll
   public const string TitleLeadtoolsAddInMove = "Leadtools.AddIn.Move" + DllExt;
   public const string DescriptionLeadtoolsAddInMove = "Leadtools.AddIn.Move";
   public const string FileVersionLeadtoolsAddInMove = "20.0.0.1";

   // Leadtools.AddIn.Security.dll
   public const string TitleLeadtoolsAddInSecurity = "Leadtools.AddIn.Security" + DllExt;
   public const string DescriptionLeadtoolsAddInSecurity = "Leadtools.AddIn.Security";
   public const string FileVersionLeadtoolsAddInSecurity = "20.0.0.1";

   // Leadtools.AddIn.StorageCommit.dll
   public const string TitleLeadtoolsAddInStorageCommit = "Leadtools.AddIn.StorageCommit" + DllExt;
   public const string DescriptionLeadtoolsAddInStorageCommit = "Leadtools.AddIn.StorageCommit";
   public const string FileVersionLeadtoolsAddInStorageCommit = "20.0.0.1";

   // Leadtools.AddIn.Store.dll
   public const string TitleLeadtoolsAddInStore = "Leadtools.AddIn.Store" + DllExt;
   public const string DescriptionLeadtoolsAddInStore = "Leadtools.AddIn.Store";
   public const string FileVersionLeadtoolsAddInStore = "20.0.0.1";

   // Leadtools.Configuration.Logging.dll
   public const string TitleLeadtoolsConfigurationLogging = "Leadtools.Configuration.Logging" + DllExt;
   public const string DescriptionLeadtoolsConfigurationLogging = "Leadtools.Configuration.Logging";
   public const string FileVersionLeadtoolsConfigurationLogging = "20.0.0.1";

   // Leadtools.AddIn.AutoConfigure.dll
   public const string TitleLeadtoolsAddInAutoConfigure = "Leadtools.AddIn.AutoConfigure" + DllExt;
   public const string DescriptionLeadtoolsAddInAutoConfigure = "Leadtools.AddIn.AutoConfigure";
   public const string FileVersionLeadtoolsAddInAutoConfigure = "20.0.0.1";

   // Leadtools.AddIn.MWLFind.dll
   public const string TitleLeadtoolsAddInMWLFind = "Leadtools.AddIn.MWLFind" + DllExt;
   public const string DescriptionLeadtoolsAddInMWLFind = "Leadtools.AddIn.MWLFind";
   public const string FileVersionLeadtoolsAddInMWLFind = "20.0.0.1";

   // Leadtools.AddIn.DicomLog.dll
   public const string TitleLeadtoolsAddInDicomLog = "Leadtools.AddIn.DicomLog" + DllExt;
   public const string DescriptionLeadtoolsAddInDicomLog = "Leadtools.AddIn.DicomLog";
   public const string FileVersionLeadtoolsAddInDicomLog = "20.0.0.1";

   // Leadtools.AddIn.Performance.dll
   public const string TitleLeadtoolsAddInPerformance = "Leadtools.AddIn.Performance" + DllExt;
   public const string DescriptionLeadtoolsAddInPerformance = "Leadtools.AddIn.Performance";
   public const string FileVersionLeadtoolsAddInPerformance = "20.0.0.1";

   // Leadtools.AddIn.Sample.Events.dll
   public const string TitleLeadtoolsAddInSampleEvents = "Leadtools.AddIn.Sample.Events" + DllExt;
   public const string DescriptionLeadtoolsAddInSampleEvents = "Leadtools.AddIn.Sample.Events";
   public const string FileVersionLeadtoolsAddInSampleEvents = "20.0.0.2";

   // Leadtools.Medical.Security.AddIn.dll
   public const string TitleMedicalSecurityAddin = "Leadtools.Medical.Security.AddIn" + DllExt;
   public const string DescriptionMedicalSecurityAddin = "Leadtools.Medical.Security.AddIn";
   public const string FileVersionMedicalSecurityAddin = "20.0.0.2";

   // Leadtools.Medical.PatientRestrict.AddIn.dll
   public const string TitleMedicalPatientRestrictAddIn = "Leadtools.Medical.PatientRestrict.AddIn" + DllExt;
   public const string DescriptionMedicalPatientRestrictAddIn = "Leadtools.Medical.PatientRestrict.AddIn";
   public const string FileVersionMedicalPatientRestrictAddIn = "20.0.0.3";

   // Leadtools.Medical.ExportLayout.AddIn.dll
   public const string TitleMedicalExportLayoutAddIn = "Leadtools.Medical.ExportLayout.AddIn" + DllExt;
   public const string DescriptionMedicalExportLayoutAddIn = "Leadtools.Medical.ExportLayout.AddIn";
   public const string FileVersionMedicalExportLayoutAddIn = "20.0.0.13";

   // Leadtools.Medical.ExportLayout.DataAccessLayer.dll
   public const string TitleMedicalExportLayoutDataAccessLayer = "Leadtools.Medical.ExportLayout.DataAccessLayer" + DllExt;
   public const string DescriptionMedicalExportLayoutDataAccessLayer = "Leadtools.Medical.ExportLayout.DataAccessLayer";
   public const string FileVersionMedicalExportLayoutDataAccessLayer = "20.0.0.2";


   //*********************************************************************
   //
   // Demo EXE
   //
   //*********************************************************************
   // DicomHighLevelClientDemo
   public const string TitleDicomHighLevelClientDemo = "DicomHighLevelClientDemo" + ExeExt;
   public const string DescriptionDicomHighLevelClientDemo = "DicomHighLevelClientDemo";
   public const string FileVersionDicomHighLevelClientDemo = "20.0.0.12";

   // DicomHighLevelMwlScuDemo
   public const string TitleDicomHighLevelMwlScuDemo = "DicomHighLevelMwlScuDemo" + ExeExt;
   public const string DescriptionDicomHighLevelMwlScuDemo = "DicomHighLevelMwlScuDemo";
   public const string FileVersionDicomHighLevelMwlScuDemo = "20.0.0.6";

   // DicomHighlevelPatientUpdaterDemo
   public const string TitleDicomHighlevelPatientUpdaterDemo = "DicomHighlevelPatientUpdaterDemo" + ExeExt;
   public const string DescriptionDicomHighlevelPatientUpdaterDemo = "DicomHighlevelPatientUpdaterDemo";
   public const string FileVersionDicomHighlevelPatientUpdaterDemo = "20.0.0.6";

   // DicomHighlevelStoreDemo
   public const string TitleDicomHighlevelStoreDemo = "DicomHighlevelStoreDemo" + ExeExt;
   public const string DescriptionDicomHighlevelStoreDemo = "DicomHighlevelStoreDemo";
   public const string FileVersionDicomHighlevelStoreDemo = "20.0.0.8";

   // Leadtools.Dicom.Server.Manager
   public const string TitleLeadtoolsDicomServerManager = "Leadtools.Dicom.Server.Manager" + ExeExt;
   public const string DescriptionLeadtoolsDicomServerManager = "Leadtools.Dicom.Server.Manager";
   public const string FileVersionLeadtoolsDicomServerManager = "20.0.0.5";

   // MedicalWorkstationMainDemo
   public const string TitleMedicalWorkstationMainDemo = "MedicalWorkstationMainDemo" + ExeExt;
   public const string DescriptionMedicalWorkstationMainDemo = "MedicalWorkstationMainDemo";
   public const string FileVersionMedicalWorkstationMainDemo = "20.0.0.7";

   // MedicalWorkstationDicomDirDemo
   public const string TitleMedicalWorkstationDicomDirDemo = "MedicalWorkstationDicomDirDemo" + ExeExt;
   public const string DescriptionMedicalWorkstationDicomDirDemo = "MedicalWorkstationDicomDirDemo";
   public const string FileVersionMedicalWorkstationDicomDirDemo = "20.0.0.1";

   // PacsConfigDemo
   public const string TitlePacsConfigDemo = "PacsConfigDemo" + ExeExt;
   public const string DescriptionPacsConfigDemo = "PacsConfigDemo";
   public const string FileVersionPacsConfigDemo = "20.0.0.9";

   // PacsDatabaseConfigurationDemo
   public const string TitlePacsDatabaseConfigurationDemo = "PacsDatabaseConfigurationDemo" + ExeExt;
   public const string DescriptionPacsDatabaseConfigurationDemo = "PacsDatabaseConfigurationDemo";
   public const string FileVersionPacsDatabaseConfigurationDemo = "20.0.0.20";

   // StorageServerManager
   public const string TitleStorageServerManager = "StorageServerManager" + ExeExt;
   public const string DescriptionStorageServerManager = "StorageServerManager";
   public const string FileVersionStorageServerManager = "20.0.0.19";

   // CCowClientDemo
   public const string TitleCCowClientDemo = "CCowClientDemo" + ExeExt;
   public const string DescriptionCCowClientDemo = "CCowClientDemo";
   public const string FileVersionCCowClientDemo = "20.0.0.1";

   // ModalityWorklistWCFDemo
   public const string TitleModalityWorklistWCFDemo = "ModalityWorklistWCFDemo" + ExeExt;
   public const string DescriptionModalityWorklistWCFDemo = "ModalityWorklistWCFDemo";
   public const string FileVersionModalityWorklistWCFDemo = "20.0.0.1";

   // MPPSWCFDemo
   public const string TitleMPPSWCFDemo = "MPPSWCFDemo" + ExeExt;
   public const string DescriptionMPPSWCFDemo = "MPPSWCFDemo";
   public const string FileVersionMPPSWCFDemo = "20.0.0.1";

   // ExternalControlSample
   public const string TitleExternalControlSample = "ExternalControlSample" + ExeExt;
   public const string DescriptionExternalControlSample = "ExternalControlSample";
   public const string FileVersionExternalControlSample = "20.0.0.1";

   // WebViewerConfiguration
   public const string TitleWebViewerConfiguration = "WebViewerConfiguration" + ExeExt;
   public const string DescriptionWebViewerConfiguration = "WebViewerConfiguration";
   public const string FileVersionWebViewerConfiguration = "20.0.0.46";

   // ChangeMedicalViewerToDental
   public const string TitleChangeMedicalViewerToDental = "ChangeMedicalViewerToDental" + ExeExt;
   public const string DescriptionChangeMedicalViewerToDental = "ChangeMedicalViewerToDental";
   public const string FileVersionChangeMedicalViewerToDental = "20.0.0.1";

   // LeadtoolsServicesHostManager
   public const string TitleLeadtoolsServicesHostManager = "LeadtoolsServicesHostManager" + ExeExt;
   public const string DescriptionLeadtoolsServicesHostManager = "LeadtoolsServicesHostManager";
   public const string FileVersionLeadtoolsServicesHostManager = "20.0.0.14";

   // Leadtools.Medical.ViewImage
   public const string TitleMedicalViewImage = "Leadtools.Medical.ViewImage" + DllExt;
   public const string DescriptionMedicalViewImage = "Leadtools.Medical.ViewImage";
   public const string FileVersionMedicalViewImage = "20.0.0.1";

   // CSMedicalMainMenu.exe
   public const string TitleMedicalMainMenu = "MedicalMainMenu" + DllExt;
   public const string DescriptionMedicalMainMenu = "MedicalMainMenu";
   public const string FileVersionMedicalMainMenu = "20.0.0.2";

   // CSDicomDemo.exe
   public const string TitleDicomDemo  = "DicomDemo" + DllExt;
   public const string DescriptionDicomDemo = "DicomDemo";
   public const string FileVersionDicomDemo = "20.0.0.2";

   // CSDicomAnnDemo.exe
   public const string TitleDicomAnnDemo = "DicomAnnDemo" + DllExt;
   public const string DescriptionDicomAnnDemo = "DicomAnnDemo";
   public const string FileVersionDicomAnnDemo = "20.0.0.1";

#endif // LTV20_CONFIG


    /*
    // Leadtools.Xxxxxx.dll
    public const string TitleXxxx                            = "Leadtools.Xxxxxx" + DllExt;
    public const string DescriptionXxxx                      = "Xxxxx";
    public const string FileVersionXxxx                      = "18.0.0.1";
  */

}
