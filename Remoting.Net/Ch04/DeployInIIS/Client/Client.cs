using System;
using System.Runtime.Remoting;
using General;  // from General.DLL
using Server; // from server.cs

namespace Client
{
	class Client
	{
		static void Main(string[] args)
		{
			String filename = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
			RemotingConfiguration.Configure(filename);

			IRemoteCustomerManager mgr = (IRemoteCustomerManager) RemotingHelper.CreateProxy(typeof(IRemoteCustomerManager));
			Console.WriteLine("Client.Main(): Reference to CustomerManager acquired");

			Customer cust = mgr.GetCustomer(4711);
			int age = cust.GetAge();
			Console.WriteLine("Client.Main(): Customer {0} {1} is {2} years old.",
				cust.FirstName,
				cust.LastName,
				age);
			
			Console.ReadLine();

		}	
	}
}

