using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("CryptoSandbox")]
[assembly: AssemblyDescription("A sandbox UI for testing storage and retrieval of username/password credentials.")]
[assembly: AssemblyCompany("Eric S Policaro")]
[assembly: AssemblyProduct("CryptoSandbox")]
[assembly: AssemblyCopyright("Copyright ©  2015")]

[assembly: ComVisible(false)]
[assembly: Guid("902e43e3-c102-4f4c-940d-b57cf162da62")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif