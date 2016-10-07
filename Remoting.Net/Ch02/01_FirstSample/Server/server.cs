using System;
using System.Runtime.Remoting;
using General;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels;

namespace Server
{

	class CustomerManager: MarshalByRefObject, ICustomerManager 
	{
		
		public CustomerManager() 
		{
			Console.WriteLine("CustomerManager.constructor: Object created");
		}

		public Customer GetCustomer(int id) 
		{
			Console.WriteLine("CustomerManager.getCustomer): Called");
			Customer tmp = new Customer();
			tmp.FirstName = "John";
			tmp.LastName = "Doe";
			tmp.DateOfBirth = new DateTime(1970,7,4);
			Console.WriteLine("CustomerManager.getCustomer(): Returning " + 
				              "Customer-Object");
			return tmp;
		}
	}

	class ServerStartup
	{
		static void Main(string[] args)
		{
			Console.WriteLine ("ServerStartup.Main(): Server started");

			HttpChannel chnl = new HttpChannel(1234);
			ChannelServices.RegisterChannel(chnl);
			RemotingConfiguration.RegisterWellKnownServiceType(
					typeof(CustomerManager),
					"CustomerManager.soap", 
					WellKnownObjectMode.Singleton);

			// the server will keep running until keypress.
			Console.ReadLine();
		}
	}
}
