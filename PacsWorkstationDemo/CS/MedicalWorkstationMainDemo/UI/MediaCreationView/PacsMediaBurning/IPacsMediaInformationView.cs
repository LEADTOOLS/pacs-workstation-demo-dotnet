// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leadtools.Dicom.Common.DataTypes;
using Leadtools.Demos.Workstation.Configuration;

namespace Leadtools.Demos.Workstation
{
   public interface IPacsMediaInformationView : IMediaInformationView
   {
      void Initialize     ( MediaType[] mediaTypes, RequestPriority[] priorities ) ;
      void SetPacsServers ( ScpInfo[] servers ) ;
      void ReportMediaCreationFailure ( string error ) ;
      void ReportMediaCreationSuccess ( ) ;
      void ReportServerVerificationSuccess ( ) ; 
      void ReportServerVerificationFailure ( string errorMessage ) ;
      
      string LabelText
      {
         get ;
         set ;
      }
      
      int NumberOfCopies
      {
         get ;
         set ;
      }
      
      RequestPriority Prioirty
      {
         get ;
         set ;
      }
      
      MediaType MediaType
      {
         get ;
         set ;
      }
      
      ScpInfo SelectedServer
      {
         get ;
         set ;
      }
      
      bool IncludeDisplayApplication
      {
         get ;
         set ;
      }
      
      bool ClearInstancesAfterRequest
      {
         get ;
         set ;
      }
      
      bool CanVerify
      {
         set ;
      }
      
      
      bool CanSendCreateRequest
      {
         set ;
      }
      
      event EventHandler VerifySelectedServer ;
      event EventHandler SendMediaCreationRequest ;
      event EventHandler SelectedServerChanged ;
   }
}
