using System;
using System.Runtime.Remoting;
using General;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels;

namespace Server
{

	class MyRemoteObject: MarshalByRefObject, ISomeInterface
	{
      public void DoSomething() 
      {
         // simulating a long-running query
         Console.WriteLine("DoSomething() called");
         Thread.Sleep(5000);
         Console.WriteLine("DoSomething() done");
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
