// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using Leadtools.Medical.Workstation.Interfaces.Views;
namespace Leadtools.Demos.Workstation
{
   public interface ILocalMediaInformationView : IMediaInformationView
   {
      void SetMediaCreationWarning ( string message ) ;
      
      bool CanBurnMedia { get; set; }
      bool CanPrepareMedia { get; set; }
      bool CreateAutoRun { get; set; }
      bool IncludeViewer { get; set; }
      string MediaBaseFolder { get; set; }
      string MediaDirectory { get ; set; }
      string ViewerSize {get ;set ;}
      string TotalSize { get ; set ;}
      
      event EventHandler PrepareMedia;
      event EventHandler MediaBaseFolderChanged ;
      event EventHandler BurnMedia;
      event EventHandler ViewerDirectoryChanged ;
      event EventHandler CreateAutoRunChanged ;
      event EventHandler IncludeViewerChanged ;
      string ViewerDirectory { get; set; }

      bool ClearInstancesAfterRequest
      {
         get;
         set;
      }
   }
}
