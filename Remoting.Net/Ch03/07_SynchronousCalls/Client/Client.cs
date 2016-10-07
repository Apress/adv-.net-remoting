using System;
using System.Runtime.Remoting;
using General;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Proxies;
using System.Threading;
using System.Reflection;

namespace Client
{

	class Client
	{

		static void Main(string[] args)
		{
			DateTime start = System.DateTime.Now;

			HttpChannel channel = new HttpChannel();
			ChannelServices.RegisterChannel(channel);
			IMyRemoteObject obj = (IMyRemoteObject) Activator.GetObject(
				typeof(IMyRemoteObject),
				"http://localhost:1234/MyRemoteObject.soap");
			Console.WriteLine("Client.Main(): Reference to rem.obj. acquired");

			Console.WriteLine("Client.Main(): Will set value to 42");

			obj.SetValue(42);

			Console.WriteLine("Client.Main(): Will now read value");
			int tmp = obj.GetValue();
			Console.WriteLine("Client.Main(): New server side value {0}", tmp);


			Console.WriteLine("Client.Main(): Will call getName()");
			String name = obj.GetName();
			Console.WriteLine("Client.Main(): received name {0}",name);

			DateTime end = System.DateTime.Now;
			TimeSpan duration = end.Subtract(start);
			Console.WriteLine("Client.Main(): Execution took {0} seconds.",
							   duration.Seconds);

			Console.ReadLine();
		}	
	}
}
