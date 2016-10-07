using System;
using System.Runtime.Remoting;
using General;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels;

namespace Client
{

	class Client
	{
      delegate void DoSomethingDelegate();

		static void Main(string[] args)
		{
			String filename = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
			RemotingConfiguration.Configure(filename);

			ISomeInterface obj = (ISomeInterface) RemotingHelper.CreateProxy(typeof(ISomeInterface));

         Console.WriteLine("Calling synchronously...");
         obj.DoSomething();
         Console.WriteLine("Completed ...");

         Console.WriteLine("Retrieved customer {0} {1}", cust.FirstName,cust.LastName);
			Console.ReadLine();
		}	
	}
}
