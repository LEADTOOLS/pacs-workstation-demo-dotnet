// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Collections.Generic;
namespace Leadtools.Demos.Workstation
{
   public interface IDicomInstancesSelectionView
   {
      void SetState ( IList<ClientQueryDataSet.ImagesRow> burningImages ) ;
      void AddSeries(ClientQueryDataSet.StudiesRow studyInformation, ClientQueryDataSet.SeriesRow series, ClientQueryDataSet.ImagesRow[] images);
      void ClearItems();
   }
}
