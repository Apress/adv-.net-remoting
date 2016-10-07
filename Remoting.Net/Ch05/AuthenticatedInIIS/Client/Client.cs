using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Collections;
using System.Runtime.Remoting.Services;
using General;  // from General.DLL
using Server; // from server.cs

namespace Client
{
	class Client
	{
		static void Main(string[] args)
		{

			try 
			{
				String filename = "client.exe.config";
				RemotingConfiguration.Configure(filename);

				CustomerManager mgr = new CustomerManager();
			
				Console.WriteLine("Client.Main(): Reference to CustomerManager " +
								  " acquired");

				IDictionary props = ChannelServices.GetChannelSinkProperties(mgr);
				props["username"] = "dummyremotinguser";
				props["password"] = "12345";

				Customer cust = mgr.getCustomer(4711);
				cust = mgr.getCustomer(4711);
				cust = mgr.getCustomer(4711);
				cust = mgr.getCustomer(4711);
				int age = cust.getAge();
				Console.WriteLine("Client.Main(): Customer {0} {1} is {2} " + 
					"years old.",
					cust.FirstName,
					cust.LastName,
					age);
			} 
			catch (Exception e) 
			{
				Console.WriteLine("EX: {0}",e.Message);
			}
			
			Console.ReadLine();
		}	
	}
}

