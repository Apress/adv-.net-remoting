using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels;
using Server;

namespace Client
{
	class Client
	{
		static void Main(string[] args)
		{
			HttpChannel chnl = new HttpChannel();
			ChannelServices.RegisterChannel(chnl);

			Console.WriteLine("Client.Main(): creating rem. reference");
			SomeRemoteObject obj = (SomeRemoteObject) Activator.GetObject (
				typeof(SomeRemoteObject),
				"http://localhost:1234/SomeRemoteObject.soap");

			Console.WriteLine("Client.Main(): calling doSomething()");
			obj.doSomething();

			Console.WriteLine("Client.Main(): done ");
			Console.ReadLine();
		}	
	}
}
