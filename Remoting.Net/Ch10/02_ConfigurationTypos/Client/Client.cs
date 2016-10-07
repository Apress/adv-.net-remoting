using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Services;

using ServerImpl;

namespace Client
{

  class Client
  {
    static void Main(string[] args)
    {
      String filename = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
      RemotingConfiguration.Configure(filename);

      VerifyRemotingConfiguration();

      CustomerManager mgr = new CustomerManager();



      Customer cust = mgr.GetCustomer(43);

      int age = cust.GetAge();
      Console.WriteLine("Client.Main(): Customer {0} {1} is {2} years old.",
        cust.FirstName,
        cust.LastName,
        age);
      
      Console.ReadLine();
    }	

    static void VerifyRemotingConfiguration()
    {
      foreach (WellKnownClientTypeEntry en in RemotingConfiguration.GetRegisteredWellKnownClientTypes())
      {
        if (en.ObjectType == null)
        {
          throw new Exception("Could not find type " + en.TypeName + " in assembly " + en.AssemblyName);
        }
      }
    }
	}
}
