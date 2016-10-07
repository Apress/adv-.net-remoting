using System;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: AssemblyTitle("Shared Assembly")]
[assembly: AssemblyVersion("2.0.0.0")]
[assembly: AssemblyKeyFile(@"..\..\..\Server.snk")]

namespace GeneralV2
{
  public interface IRemoteFactory2 : General.IRemoteFactory 
  {
    void SetAge(int age);
  }
}
