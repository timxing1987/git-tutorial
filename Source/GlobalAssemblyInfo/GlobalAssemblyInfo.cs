using System;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;

[assembly: AssemblyProduct("Smart Rewards")]
[assembly: AssemblyCompany("Smartac")]

#if DEBUG

[assembly: AssemblyConfiguration("Debug")]
#else
[assembly : AssemblyConfiguration("Release")]
#endif

[assembly: AssemblyCopyright("Copyright © 2015 Smartac")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: CLSCompliant(true)]

// Pay attention to the assembly version and file version.
// Example: 2.1.10131. 
// The original idea is to include the year in build number, but in year 2007, we got problem.
// This time, the build number will begin with 1 when we update the minor version to 1.
// General rule:
// If we upgrade the minor version, we shoud reset the build number to start with 1.
// If we upgrade the major version, we should reset the minor version to 0, and the build number start with 1.
// If we didn't upgrade the minor version, we should upgrade 1 to 2 in next year.

[assembly: AssemblyVersion("3.0.0.0")]

[assembly: SuppressMessage("Microsoft.Design", "CA2210:AssembliesShouldHaveValidStrongNames", Scope = "assembly", Justification = "Assemblies are not currently being signed.")]

[assembly: SuppressMessage("Microsoft.Performance", "CA1824:MarkAssembliesWithNeutralResourcesLanguage")]
