using System;
using System.Runtime.Remoting;
using General;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels;

namespace Server
{

	class MyRemoteObject: MarshalByRefObject, IMyRemoteObject
	{
		int myvalue;

		public MyRemoteObject() 
		{
			Console.WriteLine("MyRemoteObject.Constructor: New Object created");
		}

		public MyRemoteObject(int startvalue) 
		{
			Console.WriteLine("MyRemoteObject.Constructor: .ctor called with {0}",startvalue);
			myvalue = startvalue;
		}

		public void SetValue(int newval) 
		{
			Console.WriteLine("MyRemoteObject.setValue(): old {0} new {1}",myvalue,newval);
			myvalue = newval;
		}
		public int GetValue() 
		{
			Console.WriteLine("MyRemoteObject.getValue(): current {0}",myvalue);
			return myvalue;
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
					WellKnownObjectMode.SingleCall);

			// the server will keep running until keypress.
			Console.ReadLine();
		}
	}
}
