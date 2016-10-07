using System;
using System.Runtime.Remoting;

using General;
using GeneralV2;
using General.Client;

namespace ClientConsole2
{
  class Class1
  {
    [STAThread]
    static void Main(string[] args)
    {
      Console.WriteLine("Configuring client...");
      RemotingConfiguration.Configure("ClientConsole2.exe.config");

      Console.WriteLine("Creating proxy...");
      IRemoteFactory2 factory = (IRemoteFactory2)RemotingHelper.CreateProxy(typeof(IRemoteFactory2));

      Console.WriteLine("Calling GetAge()...");
      int age = factory.GetAge();
      Console.WriteLine(">> Call successful: " + age.ToString());

      Console.WriteLine("Calling SetAge()...");
      factory.SetAge(age * 2);
      Console.WriteLine(">> Call successful!");

      Console.WriteLine("Calling GetPerson()...");
      Person p = factory.GetPerson();
      Console.WriteLine(">> Person retrieved: {0} {1}, {2}", p.Firstname, p.Lastname, p.Age.ToString());

      Console.ReadLine();
    }
  }
}
