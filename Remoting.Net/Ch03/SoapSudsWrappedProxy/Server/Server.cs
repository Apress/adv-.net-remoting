using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels;

namespace Server
{

	class SomeRemoteObject: MarshalByRefObject
	{
		public void doSomething() 
		{
			Console.WriteLine("SomeRemoteObject.doSomething() called");
		}
	}

	class ServerStartup
	{
		static void Main(string[] args)
		{
			Console.WriteLine ("ServerStartup.Main(): Server started");

			HttpChannel chnl = new HttpChannel(1234);
			ChannelServices.RegisterChannel(chnl);
			RemotingConfiguration.RegisterWellKnownServiceType(
					typeof(SomeRemoteObject),
					"SomeRemoteObject.soap", 
					WellKnownObjectMode.SingleCall);

			// the server will keep running until keypress.
			Console.ReadLine();
		}
	}
}
