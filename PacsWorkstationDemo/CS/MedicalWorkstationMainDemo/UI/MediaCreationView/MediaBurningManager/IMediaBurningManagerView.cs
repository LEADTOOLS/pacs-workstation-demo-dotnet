// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
namespace Leadtools.Demos.Workstation
{
   interface IMediaBurningManagerView<T>
   {
      event EventHandler CloseViewRequested;
      IDicomInstancesSelectionView DicomInstancesSelectionView { get; }
      T MediaInformationView { get; }
   }
}
