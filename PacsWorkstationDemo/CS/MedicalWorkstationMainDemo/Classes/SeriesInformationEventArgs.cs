// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leadtools.Demos.Workstation.Configuration;
//using Leadtools.Demos.Workstation.UI.Search.QueryDataSet;

namespace Leadtools.Demos.Workstation
{
   public class StoreSeriesEventArgs : ProcessSeriesEventArgs
   {
      public StoreSeriesEventArgs
      ( 
         ScpInfo server,
         ClientQueryDataSet.StudiesRow study, 
         ClientQueryDataSet.SeriesRow series 
      )
      : base ( study, series )
      {
         Server = server ;
      }
      
      public ScpInfo Server
      {
         get
         {
            return _server ;
         }
         
         private set
         {
            _server = value ;
         }
      }
      
      private ScpInfo _server ;
   }
   
   public class ProcessSeriesEventArgs : EventArgs
   {
      public ProcessSeriesEventArgs ( ClientQueryDataSet.StudiesRow study, ClientQueryDataSet.SeriesRow series )
      {
         Study  = study ;
         Series = series ;
      }
      
      
      public ClientQueryDataSet.StudiesRow Study
      {
         get ;
         private set ;
      }
      
      public ClientQueryDataSet.SeriesRow Series
      {
         get ;
         private set ;
      }
   }
}
