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
		private String _firstName;
		public String LastName;
		public DateTime DateOfBirth;

    public String FirstName
    {
      get
      {
        return _firstName;
      }
      set
      {
        _firstName = value;
      }
    }

		public int GetAge()
		{
			TimeSpan tmp = DateTime.Today.Subtract(DateOfBirth);
			return tmp.Days / 365; // rough estimation
		}
	}
}
