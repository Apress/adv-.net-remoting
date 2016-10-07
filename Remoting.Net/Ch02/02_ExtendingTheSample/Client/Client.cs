using System;
using System.Runtime.Remoting;
using Sample1_General;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels;

namespace Client
{

  class Client
  {
    static void Main(string[] args)
    {
      HttpChannel channel = new HttpChannel();
      ChannelServices.RegisterChannel(channel);
	
      ICustomerManager mgr = (ICustomerManager) Activator.GetObject(
        typeof(ICustomerManager),
        "http://localhost:1234/CustomerManager.soap");
      Console.WriteLine("Client.main(): Reference to rem. object acquired");

      Console.WriteLine("Client.main(): Creating customer");
      Customer cust = new Customer();
      cust.FirstName = "Joe";
      cust.LastName = "Smith";
      cust.DateOfBirth = new DateTime(1800,5,12);

      Console.WriteLine("Client.main(): Will call validate");
      ValidationResult res = mgr.Validate (cust);
      Console.WriteLine("Client.main(): Validation finished");
      Console.WriteLine("Validation result for {0} {1}\n-> {2}: {3}", 
        cust.FirstName, cust.LastName,res.Ok.ToString(),
        res.ValidationMessage);

      Console.ReadLine();
    }	
  }
}
