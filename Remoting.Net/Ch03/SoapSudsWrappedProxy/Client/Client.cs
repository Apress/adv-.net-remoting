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
			Console.WriteLine("Client.Main(): creating rem. reference");
			SomeRemoteObject obj = new SomeRemoteObject();
			Console.WriteLine("Client.Main(): calling doSomething()");
			obj.doSomething();
			Console.WriteLine("Client.Main(): done ");

			Console.ReadLine();
		}	
	}
}
