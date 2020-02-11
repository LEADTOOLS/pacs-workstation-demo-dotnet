// *************************************************************
// Copyright (c) 1991-2019 LEAD Technologies, Inc.              
// All Rights Reserved.                                         
// *************************************************************
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Leadtools.Demos;

[assembly: AssemblyTitle         (AssemblyVersionNumber.TitleMedicalWorkstationMainDemo)]
[assembly: AssemblyDescription   (AssemblyVersionNumber.DescriptionMedicalWorkstationMainDemo)]
[assembly: AssemblyConfiguration (AssemblyVersionNumber.Configuration)]
[assembly: AssemblyCompany       (AssemblyVersionNumber.CompanyName)]
[assembly: AssemblyProduct       (AssemblyVersionNumber.Product)]
[assembly: AssemblyCopyright     (AssemblyVersionNumber.Copyright)]
[assembly: AssemblyTrademark     (AssemblyVersionNumber.Trademark)]
[assembly: AssemblyCulture       (AssemblyVersionNumber.Culture)]

[assembly: AssemblyVersion                (AssemblyVersionNumber.Version)]
[assembly: AssemblyInformationalVersion   (AssemblyVersionNumber.Version)]
[assembly: AssemblyFileVersion            (AssemblyVersionNumber.FileVersionMedicalWorkstationMainDemo)]

public static class Elevation
{
   public static bool Restart()
   {
      return DemosGlobal.MustRestartElevated();
   }
}
