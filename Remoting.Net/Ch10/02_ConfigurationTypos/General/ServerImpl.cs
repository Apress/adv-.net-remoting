using System;
using System.Runtime.Serialization;

namespace ServerImpl
{

  public class CustomerManager: MarshalByRefObject
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

    public int GetAge() 
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
