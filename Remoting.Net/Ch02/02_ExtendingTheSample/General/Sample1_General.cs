using System;

namespace Sample1_General
{
  public interface ICustomerManager
  {
    Customer GetCustomer(int id);
    ValidationResult Validate(Customer cust);
  }

  [Serializable] 
  public class ValidationResult 
  {
    public ValidationResult (bool ok, String msg) 
    {
      Console.WriteLine("ValidationResult.ctor: Object created");
      this.Ok = ok;
      this.ValidationMessage = msg;
    }
    public bool Ok;
    public String ValidationMessage;
  }

  [Serializable]
  public class Customer 
  {
    public String FirstName;
    public String LastName;
    public DateTime DateOfBirth;

    public Customer() 
    {
      Console.WriteLine("Customer.ctor: Object created");
    }

    public int GetAge() 
    {
      Console.WriteLine("Customer.getAge(): called for Customer " + FirstName + " born on " + DateOfBirth.ToShortDateString());
      TimeSpan tmp = DateTime.Today.Subtract(DateOfBirth);
      return tmp.Days / 365; // rough estimation
    }
  }
}
