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
			HttpChannel channel = new HttpChannel();
			ChannelServices.RegisterChannel(channel);
			
			ICustomerManager mgr = (ICustomerManager) Activator.GetObject(
				typeof(ICustomerManager),
				"http://localhost:1234/CustomerManager.soap");
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
