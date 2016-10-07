using System;
using System.Runtime.Remoting;
using General;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels;
using System.Collections;

namespace Server
{

	class MyWorkerObject: MarshalByRefObject, IWorkerObject
	{

		public MyWorkerObject() 
		{
			Console.WriteLine("MyWorkerObject.Constructor: New Object created");
		}

		public void DoSomething(IRemoteObject usethis) 
		{
			Console.WriteLine("MyWorkerObject.doSomething(): called");
			Console.WriteLine("MyWorkerObject.doSomething(): Will now call " +
							  "getValue() on the remote obj.");

			int tmp = usethis.GetValue();
			Console.WriteLine("MyWorkerObject.doSomething(): current value of " + 
								"the remote obj.; {0}", tmp);

			Console.WriteLine("MyWorkerObject.doSomething(): changing value to 70");
			usethis.SetValue(70);
		}
	}

	class ServerStartup
	{
		static void Main(string[] args)
		{
			Console.WriteLine ("ServerStartup.Main(): Server [2] started");
			SoapServerFormatterSinkProvider prov = new SoapServerFormatterSinkProvider();
			prov.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;

			IDictionary props = new Hashtable();
			props["port"] = 1235;

			HttpChannel chan = new HttpChannel(props, null, prov);        
			ChannelServices.RegisterChannel( chan );
			
			RemotingConfiguration.RegisterWellKnownServiceType(
					typeof(MyWorkerObject),
					"MyWorkerObject.soap", 
					WellKnownObjectMode.SingleCall);

			// the server will keep running until keypress.
			Console.ReadLine();
		}
	}
}
