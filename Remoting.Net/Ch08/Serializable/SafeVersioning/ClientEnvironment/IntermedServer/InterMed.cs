using System;
using System.Runtime.Remoting;
using System.Reflection;
using System.Runtime.CompilerServices;

using General;
using General.Client;

[assembly: AssemblyTitle("Intermediary Server Assembly")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyKeyFile("")]

namespace IntermedServer
{
  public class IntermedImpl : MarshalByRefObject, IRemoteFactory
  {
    private IRemoteFactory _server;

    public IntermedImpl() 
    {
      _server = (IRemoteFactory)RemotingHelper.CreateProxy(typeof(IRemoteFactory));
    }

    public int GetAge() 
    {
      Console.WriteLine(">> Routing GetAge()...");
      int ret = _server.GetAge();
      Console.WriteLine(">>>> GetAge() returned {0}", ret);
      return ret;
    }

    public Person GetPerson() 
    {
      Console.WriteLine(">> Routing GetPerson()...");
      Person p = _server.GetPerson();
      Console.WriteLine(">>>> GetPerson() returned {0} {1} {2}", 
        p.Firstname, p.Lastname, p.Age);
      return p;
    }

    public void UploadPerson(Person p) 
    {
      Console.WriteLine(">> Routing UploadPerson()...");
      _server.UploadPerson(p);
      Console.WriteLine(">>>> UploadPerson() routed successfully");
    }
  }

  class IntermedApp
  {
    [STAThread]
    static void Main(string[] args)
    {
      Console.WriteLine("Starting intermediary...");
      RemotingConfiguration.Configure("IntermedServer.exe.config");

      Console.WriteLine("Intermediary configured, waiting for requests!");
      System.Console.ReadLine();
    }
  }
}
