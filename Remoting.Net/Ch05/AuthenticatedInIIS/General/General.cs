using System;

namespace General
{

	[Serializable]
	public class Customer 
	{

		public String FirstName;
		public String LastName;
		public DateTime DateOfBirth;

		public Customer() 
		{
			Console.WriteLine("Customer.constructor: Object created");
		}

		public int getAge() 
		{
			Console.WriteLine("Customer.getAge(): Calculating age of {0}, " +
				"born on {1}.", 
				FirstName, 
				DateOfBirth.ToShortDateString());

			TimeSpan tmp = DateTime.Today.Subtract(DateOfBirth);
			return tmp.Days / 365; // rough estimation
		}
	}
}
