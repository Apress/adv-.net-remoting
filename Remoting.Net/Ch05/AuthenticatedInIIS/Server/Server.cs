using System;
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

}
