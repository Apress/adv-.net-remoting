using System;
using System.Reflection;

[assembly: AssemblyDelaySign(false)]
[assembly: AssemblyKeyFile(@"..\..\SimpleComponent.snk")]
[assembly: AssemblyVersion("2.0.0.0")]

namespace SimpleComponent
{
  public class SimpleClass
  {
    public SimpleClass()
    {
    }

    public string DoSomething(string a) 
    {
      return string.Format("Second version {0}", a);
    }
  }
}
