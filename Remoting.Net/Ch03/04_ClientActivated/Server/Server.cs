using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels;

namespace Server
{

	public class MyRemoteObject: MarshalByRefObject
	{
		int myvalue;

		public MyRemoteObject(int val)
		{
			Console.WriteLine("MyRemoteObject.ctor(int) called");
			myvalue = val;
		}

		public MyRemoteObject() 
		{
			Console.WriteLine("MyRemoteObject.ctor() called");
		}

		public void setValue(int newval)
		{
			Console.WriteLine("MyRemoteObject.setValue(): old {0} new {1}",myvalue,newval);
			myvalue = newval;
		}

		public int getValue()
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

			RemotingConfiguration.ApplicationName = "MyServer";
			RemotingConfiguration.RegisterActivatedServiceType(
									 typeof(MyRemoteObject));

			// the server will keep running until keypress.
			Console.ReadLine();
		}
	}
}
