// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using Leadtools.Dicom.Common.DataTypes;
using Leadtools.Demos.Workstation.Configuration;
using Leadtools.Medical.Workstation.Interfaces.Views;


namespace Leadtools.Demos.Workstation
{
   public interface IMediaInformationView : IWorkstationView
   {
      string MediaTitle
      {
         get ;
         set ;
      }
      
      event EventHandler MediaTitleChanged ;
      
      void MediaTitleValidationError(string errorText);
   }
}
