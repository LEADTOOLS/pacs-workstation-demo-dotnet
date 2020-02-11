// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.ComponentModel;
namespace Leadtools.Demos.Workstation
{
   interface IMediaCreationPresenter
   {
      BindingList<Leadtools.Demos.Workstation.ClientQueryDataSet.ImagesRow> BurningImagesState { get; }
      event EventHandler ClearInstances;
      void SetCurrentPatient(string patientName, IMediaInformationView view );
   }
}
