// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System ;
using System.Collections.Generic ;
using System.Text ;
using System.IO ;
using Leadtools.Dicom.Server.Admin ;
using Leadtools.Dicom.AddIn.Common ;
using Leadtools.Medical.Workstation.DataAccessLayer ;
using Leadtools.Medical.DataAccessLayer.Configuration ;
using Leadtools.Medical.Storage.DataAccessLayer.Configuration ;
using System.Configuration;
using System.Xml;
using Leadtools.Demos.Workstation.Configuration;
using Leadtools.Demos.StorageServer.DataTypes;
using Leadtools.DicomDemos;


namespace Leadtools.Demos.Workstation
{
   public class WorkstatoinServiceManager : IDisposable
   {
      #region Public
         
         #region Methods
         
            public WorkstatoinServiceManager ( string baseDirectory ) 
            {
               BaseDirectory = baseDirectory ;
            }

            public DicomService LoadWorkstationListenerService ( string serviceName )
            {
               if ( WorkstationService != null ) 
               {
                  throw new InvalidOperationException ( "Another service is already loaded" ) ;
               }
               
               CreateServiceAdmin ( serviceName ) ;
               
               foreach ( DicomService service  in ServiceAdmin.Services.Values )
               {
                  WorkstationService = service ;
                  
                  return service ;
               }
               
               return null ;
            }
            
            public void UnloadWorkstationListenerService ( )
            {
               if ( WorkstationService == null ) 
               {
                  return ;
               }
               
               WorkstationService.Dispose ( ) ;
               
               WorkstationService = null ;
               
               if ( null != ServiceAdmin ) 
               {
                  ServiceAdmin.Error -= new EventHandler<Leadtools.Dicom.Server.Admin.ErrorEventArgs> ( ServiceAdmin_Error ) ;
                  ServiceAdmin.Dispose ( ) ;
                  ServiceAdmin = null ;
               }
            }
            
            public DicomService InstallWorkstationService ( ServerSettings settings, string [ ] addInsDlls, string [] configurationAddInsDlls ) 
            {
               if ( WorkstationService != null )
               {
                  throw new InvalidOperationException ( "Workstation Service already installed." ) ;
               }
               
               CreateServiceAdmin ( settings.AETitle ) ;
               
               DicomService service ;
               
               
               InstallAddIns ( addInsDlls, settings.AETitle ) ;
               
               InstallConfigurationAddIns(configurationAddInsDlls, settings.AETitle);
               
               GlobalPacsUpdater.ModifyGlobalPacsConfiguration(DicomDemoSettingsManager.ProductNameWorkstation, settings.AETitle, GlobalPacsUpdater.ModifyConfigurationType.Add);
               
               service = ServiceAdmin.InstallService ( settings ) ;
               
               WorkstationService = service ;
               
               return service ;
            }
            
            public void UninstallWorkstationService ( ) 
            {
               if ( null != WorkstationService ) 
               {
                  ServiceAdmin.UnInstallService ( WorkstationService ) ;
                  
                  WorkstationService.Dispose ( ) ;
                  
                  WorkstationService = null ;
               }
            }

         #endregion
         
         #region Properties
         
            private ServiceAdministrator ServiceAdmin 
            {
               get
               {
                  return _serviceAdmin ;
               }
               
               set
               {
                  _serviceAdmin = value ;
               }
            }
            
            public string BaseDirectory
            {
               get ;
               private set ;
            }
            
            public DicomService WorkstationService
            {
               get ;
               private set ;
            }
            
            public string ServiceName 
            {
               get
               {
                  if ( WorkstationService == null ) 
                  {
                     return null ;
                  }
                  else
                  {
                     return WorkstationService.ServiceName ;
                  }
               }
            }
            
            
         #endregion
         
         #region Events
            
         #endregion
         
         #region Data Types Definition
            
         #endregion
         
         #region Callbacks
            
            public event EventHandler<Leadtools.Dicom.Server.Admin.ErrorEventArgs> ServerError ;
            
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
         
            private void InstallAddIns ( string [ ] addIns, string serviceName )
            {
               string addInsDirectory ;
               
               
               addInsDirectory = Path.Combine ( Path.Combine ( ServiceAdmin.BaseDirectory, serviceName ), "AddIns" ) ;
               
               if ( !Directory.Exists ( addInsDirectory ) )
               {
                  Directory.CreateDirectory ( addInsDirectory ) ;
               }
               
               foreach ( string addInDll in addIns ) 
               {
                  if ( File.Exists ( Path.Combine ( ServiceAdmin.BaseDirectory, addInDll ) ) )
                  {
                     File.Copy ( Path.Combine ( ServiceAdmin.BaseDirectory, addInDll ), 
                                 Path.Combine ( addInsDirectory, addInDll ),
                                 true ) ;
                  }
               }
               
               CreateAddInConfigurationFile ( Path.Combine ( ServiceAdmin.BaseDirectory, serviceName ) ) ;
            }
            
            private void InstallConfigurationAddIns ( string [ ] addIns, string serviceName )
            {
               string addInsDirectory = Path.Combine(Path.Combine(ServiceAdmin.BaseDirectory, serviceName), "Configuration");

               if (!Directory.Exists(addInsDirectory))
               {
                  Directory.CreateDirectory(addInsDirectory);
               }

               foreach (string addInDll in addIns)
               {
                  if (File.Exists(Path.Combine(ServiceAdmin.BaseDirectory, addInDll)))
                  {
                     File.Copy(Path.Combine(ServiceAdmin.BaseDirectory, addInDll),
                                 Path.Combine(addInsDirectory, addInDll),
                                 true);
                  }
               }
            }
            
            private void CreateAddInConfigurationFile ( string serviceDirectory ) 
            {
               string addInConfigurationFile ;
               System.Configuration.Configuration config ;
               System.Configuration.ConfigXmlDocument xml = new ConfigXmlDocument ( ) ;
               XmlNodeList nodes ;
               
               
               
               addInConfigurationFile = Path.Combine ( serviceDirectory, "service.config" ) ;
               config = ConfigurationManager.OpenExeConfiguration ( System.Configuration.ConfigurationUserLevel.None ) ;
               
               config.SaveAs ( addInConfigurationFile ) ;
               
               xml.Load ( addInConfigurationFile ) ;
               
               nodes = xml.GetElementsByTagName ( "appSettings" ) ;
               
               if ( nodes.Count > 0 )
               {
                  nodes [ 0 ].ParentNode.RemoveChild ( nodes [ 0 ] ) ;
                  
                  xml.Save ( addInConfigurationFile ) ;
               }
            }
            
            private void CreateServiceAdmin ( string serviceName )
            {
               List <string> services ;
               
               
               ServiceAdmin = new ServiceAdministrator ( BaseDirectory ) ;
               services     = new List <string> ( ) ;

               ServiceAdmin.Error += new EventHandler<Leadtools.Dicom.Server.Admin.ErrorEventArgs> ( ServiceAdmin_Error ) ;
               
               services.Add ( serviceName ) ;
               
#if LEADTOOLS_V175_OR_LATER
               ServiceAdmin.Initialize(services);
#else
               ServiceAdmin.Unlock ( ConfigurationData.PacsFrmKey, services ) ;
#endif

               if ( ServiceAdmin.IsLocked )
               {
                  throw new InvalidOperationException ( "PACS Framework locked." );
               }
            }
            
         #endregion
         
         #region Properties
            
         #endregion
         
         #region Events
         
            void ServiceAdmin_Error ( object sender, Leadtools.Dicom.Server.Admin.ErrorEventArgs e )
            {
               if ( null != ServerError )
               {
                  ServerError ( sender, e ) ;
               }
            }
            
         #endregion
         
         #region Data Members
         
            private ServiceAdministrator _serviceAdmin ;
            
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

      #region IDisposable Members

      public void Dispose()
      {
         try
         {
            UnloadWorkstationListenerService ( ) ;
         }
         catch {}
      }

      #endregion
   }
}
