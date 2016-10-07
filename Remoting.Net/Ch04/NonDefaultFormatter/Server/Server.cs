using System;
using System.Runtime.Remoting;
using General;

namespace Server
{

	class CustomerManager: MarshalByRefObject
	{
		
		public CustomerManager() 
		{
			Console.WriteLine("CustomerManager.constructor: Object created");
		}

		public Customer getCustomer(int id) 
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

			String filename = "server.exe.config";
			RemotingConfiguration.Configure(filename);

			// the server will keep running until keypress.
			Console.ReadLine();
		}
	}
}
