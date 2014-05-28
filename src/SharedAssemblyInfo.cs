using System.Runtime.InteropServices;
﻿using System.Reflection;

#if DEBUG
[assembly: AssemblyProduct("Paynova Api Client (Debug)")]
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyProduct("Paynova Api Client (Release)")]
[assembly: AssemblyConfiguration("Release")]
#endif

[assembly: AssemblyDescription("Paynova Api Client - the official .Net client for Paynovas API.")]
[assembly: AssemblyCompany("Paynova")]
[assembly: AssemblyCopyright("Copyright © 2014 Paynova")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

[assembly: AssemblyVersion("0.1.0.*")]
[assembly: AssemblyFileVersion("0.1.0")]