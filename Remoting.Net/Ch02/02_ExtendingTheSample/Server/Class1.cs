using System;
using System.Runtime.Remoting;
using Sample1_General;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels;

namespace Server
{

  class CustomerManager: MarshalByRefObject, ICustomerManager 
  {

    public Customer GetCustomer(int id) 
    {
      Console.WriteLine("SERVER: getCustomer({0}) has been called",id);
      Customer tmp = new Customer();
      tmp.FirstName = "John";
      tmp.LastName = "Doe";
      tmp.DateOfBirth = new DateTime(1970,7,4);
      return tmp;
    }

    public ValidationResult Validate(Customer cust) 
    {
      int age = cust.GetAge();
      Console.WriteLine("CustomerManager.validate() for {0} aged {1}", cust.FirstName,age);
			
      if ((cust.FirstName == null) || (cust.FirstName.Length == 0)) 
      {
        return new ValidationResult(false,"Firstname missing");
      }

      if ((cust.LastName == null) || (cust.LastName.Length == 0)) 
      {
        return new ValidationResult(false, "Lastname missing");
      }

      if (age < 0 || age > 120) 
      {
        return new ValidationResult(false,"Customer must be younger than 120 years");
      }

      return new ValidationResult(true,"Validation succeeded");
    }
  }

  class ServerStartup
  {
    static void Main(string[] args)
    {
      Console.WriteLine ("Server started");

      HttpChannel chnl = new HttpChannel(1234);
      ChannelServices.RegisterChannel(chnl);
      RemotingConfiguration.RegisterWellKnownServiceType(
        typeof(CustomerManager),
        "CustomerManager.soap", 
        WellKnownObjectMode.Singleton);
      // the current thread will be suspended so that
      // the server will keep running.
      System.Threading.Thread.CurrentThread.Suspend(); 
    }
  }
}
