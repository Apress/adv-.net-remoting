using System;
using General;
using System.Security.Principal;

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
			String machinename = Environment.MachineName;

			IPrincipal principal = 
					System.Threading.Thread.CurrentPrincipal;

			if (! principal.IsInRole(machinename + @"\RemotingUsers"))
			{
				throw new UnauthorizedAccessException(
								"The user is not in group RemotingUsers");
			}

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
