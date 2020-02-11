// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Leadtools.Demos.Workstation
{
   class ShowMediaManagerCommand : ShowModelessDialogCommand
   {
      MediaViewType           _mediaType ;
      MediaBurningManagerView _mediaManager ;
      
      public ShowMediaManagerCommand ( IWin32Window parent, MediaBurningManagerView dialog, MediaViewType mediaType )
      : base ( parent, dialog ) 
      {
         _mediaManager = dialog ;
         _mediaType     = mediaType ;
      }

      public override void Execute()
      {
         _mediaManager.ShowMedia ( _mediaType ) ;
         
         base.Execute();
      }
   }
}
