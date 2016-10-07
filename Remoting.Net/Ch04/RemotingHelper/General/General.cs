using System;

namespace General
{
	public interface IRemoteCustomerManager
	{
		Customer GetCustomer(int id);
	}

	[Serializable]
	public class Customer
	{
		public String FirstName;
		public String LastName;
		public DateTime DateOfBirth;

		public int GetAge()
		{
			TimeSpan tmp = DateTime.Today.Subtract(DateOfBirth);
			return tmp.Days / 365; // rough estimation
		}
	}
}
