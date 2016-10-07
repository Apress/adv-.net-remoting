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
      Console.WriteLine(">>>> New properties: {0} {1}", p.Birthdate, p.Comments);

      Console.WriteLine("Calling UploadPerson()...");
      Person up = new Person("Upload", "Test", 20);
      up.Birthdate = DateTime.Now.AddDays(2);
      up.Comments = "Two days older person!";
      factory.UploadPerson(up);
      Console.WriteLine(">> Upload called successfully!");

      Console.ReadLine();
    }
  }
}
