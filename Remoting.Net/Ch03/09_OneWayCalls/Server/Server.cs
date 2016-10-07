using System;
using System.Runtime.Remoting;
using General;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.Collections;
using System.Threading;

namespace Server
{
	class MyRemoteObject: MarshalByRefObject, IMyRemoteObject
	{
		int myvalue;

		public MyRemoteObject() 
		{
			Console.WriteLine("MyRemoteObject.Constructor: New Object created");
		}

		public void SetValue(int newval)
		{
			Console.WriteLine("MyRemoteObject.setValue(): old {0} new {1}",myvalue,newval);

			// we simulate a long running action 
			Console.WriteLine("     .setValue() -> waiting 5 sec before setting value");
			Thread.Sleep(5000);

			myvalue = newval;
			Console.WriteLine("     .setValue() -> value is now set");
		}

		public int GetValue()
		{
			Console.WriteLine("MyRemoteObject.getValue(): current {0}",myvalue);
			return myvalue;
		}

		public string GetName() 
		{
			Console.WriteLine("MyRemoteObject.getName(): called");

			// we simulate a long running action 
			Console.WriteLine("     .getName() -> waiting 5 sec before continuing");
			Thread.Sleep(5000);

			Console.WriteLine("     .getName() -> returning name");
			return "John Doe";
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
					typeof(MyRemoteObject),
					"MyRemoteObject.soap", 
					WellKnownObjectMode.Singleton);

			// the server will keep running until keypress.
			Console.ReadLine();
		}
	}
}
