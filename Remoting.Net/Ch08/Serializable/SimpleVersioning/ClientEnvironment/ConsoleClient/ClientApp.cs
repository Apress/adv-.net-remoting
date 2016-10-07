using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Activation;
using System.Reflection;
using System.Runtime.CompilerServices;

using General;
using General.Client;

[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyDelaySign(false)]
[assembly: AssemblyKeyFile(@"..\..\..\Client.snk")]

namespace ConsoleClient
{
  class ClientApp
  {
    [STAThread]
    static void Main(string[] args)
    {
      Console.WriteLine("Configuring client...");
      RemotingConfiguration.Configure("ConsoleClient.exe.config");

      Console.WriteLine("Creating proxy...");
      IRemoteFactory factory = (IRemoteFactory)RemotingHelper.CreateProxy(typeof(IRemoteFactory));

      Console.WriteLine("Calling GetAge()...");
      int age = factory.GetAge();
      Console.WriteLine(">> Call successful: " + age.ToString());

      Console.WriteLine("Calling GetPerson()...");
      Person p = factory.GetPerson();
      Console.WriteLine(">> Person retrieved: {0} {1}, {2}", p.Firstname, p.Lastname, p.Age.ToString());

      Console.WriteLine("Calling UploadPerson()...");
      factory.UploadPerson(new Person("Upload", "Test", 20));
      Console.WriteLine(">> Upload called successfully!");

      Console.ReadLine();
    }
  }
}
