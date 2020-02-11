// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leadtools.DicomDemos;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using Leadtools.Dicom.Common.DataTypes;

namespace Leadtools.Demos.Workstation
{
   [Serializable]
   public class WorkstationSettings : IWorkstationSettings
   {
      public WorkstationSettings ( ) 
      {
      }
      
      public static WorkstationSettings Load ( string settings )
      {
         XmlSerializer serializer ;
         StringReader  reader ;
         
         serializer = new XmlSerializer ( typeof ( WorkstationSettings ) )  ;
         
         using ( reader = new StringReader ( settings ))
         {
            return serializer.Deserialize ( XmlTextReader.Create ( reader ) )  as WorkstationSettings ;
         }
      }
      
      public static string Save ( WorkstationSettings settings )
      {
         XmlSerializer serializer ;
         StringWriter  writer ;
         
         serializer = new XmlSerializer ( typeof ( WorkstationSettings ) )  ;
         
         using ( writer = new StringWriter ( ) )
         {
            serializer.Serialize ( writer, settings ) ;
            
            return writer.ToString ( ) ; 
         }
      }
      
      public List<DicomAE> ServerList
      {
         get 
         {
            if ( null == _serverList )
            {
               _serverList = new List<DicomAE> ( ) ;
            }
            
            return _serverList ;
         }
         
         set 
         {
            _serverList = value ;
         }
      }
      
      public DicomAE WorkstationDicomServer
      {
         get ;
         set ;
      }

      public DicomAE ClientAe
      {
         get ;
         set ;
      }

      public string WorkstationServer
      {
         get ;
         set ;
      }

      public string DefaultImageQuery
      {
         get ;
         set ;
      }

      public string DefaultStore
      {
         get ;
         set ;
      }
      
      public bool SetClientToAllWorkstations
      {
         get ; 
         set ;
      }
      
      public DicomAE GetServer ( string serverName )
      {
         DicomAE ret = null ;
         
         
         if ( serverName == WorkstationServer ) 
         {
            return WorkstationDicomServer ;
         }
         
         foreach ( DicomAE ae in ServerList )
         {
            if ( string.Compare ( ae.AE, serverName, true ) ==0 )
            {
               ret = ae ;
               
               break ;
            }
         }
         return ret ;
      }
   
      private List<DicomAE> _serverList ;
   }
}
