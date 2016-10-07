using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using General;

namespace Server
{
	class MyRemoteObject: MarshalByRefObject, IRemoteObject
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

	class MyRemoteFactory: MarshalByRefObject,IRemoteFactory 
	{
		public MyRemoteFactory() {
			Console.WriteLine("MyRemoteFactory.ctor() called");
		}
		public IRemoteObject getNewInstance() 
		{
			Console.WriteLine("MyRemoteFactory.getNewInstance() called");
			return new MyRemoteObject();
		}

		public IRemoteObject getNewInstance(int initvalue) 
		{
			Console.WriteLine("MyRemoteFactory.getNewInstance(int) called");
			return new MyRemoteObject(initvalue);
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
				typeof(MyRemoteFactory),
				"factory.soap",
				WellKnownObjectMode.Singleton);

			// the server will keep running until keypress.
			Console.ReadLine();
		}
	}
}
