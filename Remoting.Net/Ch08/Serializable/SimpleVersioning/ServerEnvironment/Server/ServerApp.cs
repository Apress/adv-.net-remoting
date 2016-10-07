using System;
using System.Runtime.Remoting;
using System.Reflection;
using System.Runtime.CompilerServices;

using General;

[assembly: AssemblyTitle("Server Assembly")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyKeyFile(@"..\..\..\Server.snk")]

namespace Server
{
  public class ServerImpl : MarshalByRefObject, IRemoteFactory
  {
    private int _ageCount = 10;

    public int GetAge() 
    {
      Console.WriteLine(">> GetAge {0}", _ageCount);
      return _ageCount;
    }

    public Person GetPerson() 
    {
      Console.WriteLine(">> GetPerson()");
      Console.WriteLine(">> Returning person {0}...", _ageCount);

      Person p = new Person("Test", "App", _ageCount++);
      return p;
    }

    public void UploadPerson(Person p) 
    {
      Console.WriteLine(">> UploadPerson()");
      Console.WriteLine(">> Person {0} {1} {2}", p.Firstname, p.Lastname, p.Age);

      _ageCount += p.Age;
    }
  }

  class ServerApp
  {
    [STAThread]
    static void Main(string[] args)
    {
      Console.WriteLine("Starting server...");
      RemotingConfiguration.Configure("Server.exe.config");

      Console.WriteLine("Server configured, waiting for requests!");
      System.Console.ReadLine();
    }
  }
}
