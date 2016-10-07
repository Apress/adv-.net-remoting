using System;
using System.Runtime.Remoting;
using General;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;

namespace Server
{

   class CustomerManager: MarshalByRefObject, IRemoteCustomerManager
   {
	   public Customer GetCustomer(int id)
	   {
         if (LogSettingContext.EnableLog)
         {
            // simulate write to a logfile 
            Console.WriteLine("LOG: Loading Customer " + id);
         }

		   Customer cust = new Customer();
		   return cust;
	   }
   }

	class ServerStartup
	{
		static void Main(string[] args)
		{
			String filename = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
			RemotingConfiguration.Configure(filename);
			Console.WriteLine ("ServerStartup.Main(): Server started");
			Console.ReadLine();
		}
	}
}
