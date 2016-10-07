using System;
using System.Runtime.Remoting;
using General;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels;

namespace Client
{

	class Client
	{
		static void Main(string[] args)
		{
			String filename = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
			RemotingConfiguration.Configure(filename);

			IRemoteCustomerManager mgr = (IRemoteCustomerManager) RemotingHelper.CreateProxy(typeof(IRemoteCustomerManager));

			Console.WriteLine("Client.Main(): Reference to rem.obj. acquired");
			Customer cust = mgr.GetCustomer(42);
			Console.WriteLine("Retrieved customer {0} {1}", cust.FirstName,cust.LastName);
			Console.ReadLine();
		}	
	}
}
