using System;
using General;

namespace Server
{
	class CustomerManager: MarshalByRefObject, IRemoteCustomerManager
	{
		public Customer GetCustomer(int id) 
		{
			Customer tmp = new Customer();
			tmp.FirstName = "John";
			tmp.LastName = "Doe";
			tmp.DateOfBirth = new DateTime(1970,7,4);
			return tmp;
		}
	}
}
