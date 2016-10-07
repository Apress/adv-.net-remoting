using System;
using System.Runtime.Remoting;
using General;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels;

namespace Server
{

	class CustomerManager: MarshalByRefObject, IRemoteCustomerManager
	{
		public Customer GetCustomer(int id)
		{
			Customer cust = new Customer();
			cust.FirstName = "John";
			cust.LastName = "Doe";
			return cust;
		}
	}

	class ServerStartup
	{
		static void Main(string[] args)
		{
			String filename = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
			RemotingConfiguration.Configure(filename);
			Console.WriteLine ("ServerStartup.Main(): Server started");
			Console.ReadLine();
		}
	}
}
