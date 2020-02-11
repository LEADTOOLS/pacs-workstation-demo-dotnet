// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms ;
using Leadtools.Medical.Workstation.Client ;
using Leadtools.Medical.Workstation.Client.Local ;
using Leadtools.Medical.Workstation.Client.Pacs ;
using Leadtools.Medical.Workstation.Loader ;
using Leadtools.Medical.Workstation ; 
using Leadtools.Medical.Storage.DataAccessLayer;
using Leadtools.Medical.DataAccessLayer;
using Leadtools.Medical.Storage.DataAccessLayer.Configuration;
using Leadtools.DicomDemos;
using Leadtools.Demos.Workstation.Configuration;

namespace Leadtools.Demos.Workstation
{
   public class DicomClientFactory
   {
      #region Public
   
         #region Methods
         
            public static QueryClient CreateQueryClient (  ) 
            {
               try
               {
                  return CreateQueryClient ( ConfigurationData.ClientBrowsingMode ) ;
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  throw ;
               }
            }

      private static void SetQueryClientTlsSettings(BaseClient client)
      {
         //// Set TLS settings
         client.ClientCertificate = ConfigurationData.SecurityOptions.CertificateFileName;
         client.ClientCertificateType = ConfigurationData.SecurityOptions.CertificateType;
         client.ClientCertificateKey = ConfigurationData.SecurityOptions.KeyFileName;
         client.ClientCertificateKeyPassword = ConfigurationData.SecurityOptions.Password;

         client.CipherSuiteList.Clear();
         foreach (CipherSuiteItem cipherSuiteItem in ConfigurationData.SecurityOptions.CipherSuites.ItemList)
         {
            if (cipherSuiteItem.IsChecked)
            {
               client.CipherSuiteList.Add(cipherSuiteItem.Cipher);
            }
         }

         client.OpenSslContextCreationSettings = new Dicom.DicomOpenSslContextCreationSettings(
            ConfigurationData.SecurityOptions.SslMethodType,
            ConfigurationData.SecurityOptions.CertificationAuthoritiesFileName,
            ConfigurationData.SecurityOptions.VerificationFlags,
            ConfigurationData.SecurityOptions.MaximumVerificationDepth,
            ConfigurationData.SecurityOptions.Options);
      }

      public static QueryClient CreateQueryClient ( DicomClientMode clientMode ) 
            {
               try
               {
                  switch ( clientMode )
                  {
                     case DicomClientMode.LocalDb :
                     {
                        QueryClient client ;
                        IStorageDataAccessAgent dataAccess ;


                        if ( !ConfigurationData.SupportLocalQueriesStore )
                        {
                           throw new InvalidOperationException ( "Feature is not supported." ) ;
                        }
                        
                        dataAccess = DataAccessServices.GetDataAccessService <IStorageDataAccessAgent> ( ) ;
                        
                        if ( null == dataAccess ) 
                        {
                           throw new InvalidOperationException ( "Storage Service is not registered." ) ;
                        }
                        
                        client = new DbQueryClient ( ConfigurationData.WorkstationClient.ToAeInfo ( ), dataAccess ) ;
                                                      
                        client.EnableLog   = ConfigurationData.Debugging.GenerateLogFile ;
                        client.LogFileName = ConfigurationData.Debugging.LogFileName ;
                     
                        return client ;
                     }
                     
                     
                     case DicomClientMode.Pacs:
                     {
                        QueryClient client ;
                        Leadtools.Dicom.Scu.DicomScp scp ;
                        
                        
                        if ( !ConfigurationData.SupportDicomCommunication )
                        {
                           throw new InvalidOperationException ( "Feature is not supported." ) ;
                        }
                        
                        scp = new Leadtools.Dicom.Scu.DicomScp ( ) ;
                        
                        scp.AETitle     = ConfigurationData.ActivePacs.AETitle ;
                        scp.PeerAddress = Utils.ResolveIPAddress ( ConfigurationData.ActivePacs.Address ) ;
                        scp.Port        = ConfigurationData.ActivePacs.Port ;
                        scp.Timeout     = ConfigurationData.ActivePacs.Timeout ;
                        scp.Secure      = ConfigurationData.ActivePacs.Secure;
                        
                        client = new PacsQueryClient (ConfigurationData.WorkstationClient.ToAeInfo(), scp) ;
                                                     
                        client.EnableLog   = ConfigurationData.Debugging.GenerateLogFile ;
                        client.LogFileName = ConfigurationData.Debugging.LogFileName ;

                        SetQueryClientTlsSettings(client);
                        return client ;
                     }
                     
                     case DicomClientMode.DicomDir:
                     {
                        DicomDirQueryClient client = new DicomDirQueryClient ( ConfigurationData.CurrentDicomDir ) ;
                     
                        return client ;
                     }
                     
                     default:
                     {
                        throw new NotImplementedException ( "Dicom Client not implemented." ) ;
                     }
                  }
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  throw ;
               }
            }
            
            public static RetrieveClient CreateRetrieveClient ( ) 
            {
               try
               {
                  return CreateRetrieveClient ( ConfigurationData.ClientBrowsingMode ) ;
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
               
                  throw ;
               }
            }
            
            public static RetrieveClient CreateRetrieveClient ( DicomClientMode clientMode ) 
            {
               try
               {
                  switch ( clientMode )
                  {
                     case DicomClientMode.LocalDb :
                     {
                        return CreateLocalRetrieveClient ( ) ;
                     }
                     
                     
                     case DicomClientMode.Pacs:
                     {
                        Leadtools.Dicom.Scu.DicomScp scp ;
                        
                        
                        if ( ConfigurationData.ActivePacs == null ) 
                        {
                           throw new InvalidOperationException ( "No active PACS Server defined" ) ;
                        }
                        
                        scp = new Leadtools.Dicom.Scu.DicomScp ( ) ;
                        
                        scp.AETitle     = ConfigurationData.ActivePacs.AETitle ;
                        scp.PeerAddress = Utils.ResolveIPAddress ( ConfigurationData.ActivePacs.Address ) ;
                        scp.Port        = ConfigurationData.ActivePacs.Port ;
                        scp.Timeout     = ConfigurationData.ActivePacs.Timeout ;

                        
                        return CreatePacsRetrieveClient ( scp ) ;
                     }
                     
                     case DicomClientMode.DicomDir:
                     {
                        DicomDirRetrieveClient client ;
                        
                        
                        client = new DicomDirRetrieveClient ( ConfigurationData.WorkstationClient.ToAeInfo ( ),
                                                              ConfigurationData.CurrentDicomDir ) ;
                     
                        return client ;
                     }
                     
                     default:
                     {
                        throw new NotImplementedException ( "Dicom Client not implemented." ) ;
                     }
                  }
               }
               catch ( Exception exception )
               {
                  System.Diagnostics.Debug.Assert ( false, exception.Message ) ;
                                 
                  throw ;
               }
            }
            
            public static DbRetrieveClient CreateLocalRetrieveClient ( )
            {
               IStorageDataAccessAgent dataAccess ;
               DbRetrieveClient client ;
               
               
               if ( !ConfigurationData.SupportLocalQueriesStore )
               {
                  throw new InvalidOperationException ( "Feature is not supported." ) ;
               }
               
               dataAccess = DataAccessServices.GetDataAccessService <IStorageDataAccessAgent> ( ) ;
            
               if ( null == dataAccess ) 
               {
                  throw new InvalidOperationException ( "Storage Service is not registered." ) ;
               }
               
               client = new DbRetrieveClient ( ConfigurationData.WorkstationClient.ToAeInfo ( ), dataAccess ) ;
                                               
               client.EnableLog   = ConfigurationData.Debugging.GenerateLogFile ;
               client.LogFileName = ConfigurationData.Debugging.LogFileName ;
            
               return client ;
            }
            
            public static PacsRetrieveClient CreatePacsRetrieveClient ( Leadtools.Dicom.Scu.DicomScp scp )
            {
               PacsRetrieveClient client ;
               
               
               if ( !ConfigurationData.SupportDicomCommunication )
               {
                  throw new InvalidOperationException ( "Feature is not supported." ) ;
               }

               client =  new PacsRetrieveClient ( ConfigurationData.WorkstationClient.ToAeInfo ( ),
                                                  scp ) ;
                                                  
               client.EnableLog   = ConfigurationData.Debugging.GenerateLogFile ;
               client.LogFileName = ConfigurationData.Debugging.LogFileName ;

               SetQueryClientTlsSettings(client);


               client.ValidateForDuplicateImages = !ConfigurationData.ContinueRetrieveOnDuplicateInstance ;
               client.StoreRetrievedImages       = ConfigurationData.MoveToWSClient ;
               client.MoveLocally                = ConfigurationData.MoveToWSClient ;
               client.MoveServerAeTitle          = ConfigurationData.WorkstationServiceAE ;
               
               
               return client ;
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
   
   public enum DicomClientMode
   {
      Pacs,
      LocalDb,
      DicomDir
   }
}
