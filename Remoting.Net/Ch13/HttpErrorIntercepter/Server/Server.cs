using System;
using System.Runtime.Remoting;
using General;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels;

namespace Server
{

	public class CustomerManager: MarshalByRefObject, IRemoteCustomerManager
	{
		public Customer GetCustomer(int id)
		{
			Customer cust = new Customer();
			cust.FirstName = "John";
			cust.LastName = "Doe";
			return cust;
		}
	}
}
