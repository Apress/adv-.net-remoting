using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Activation;
//using Server;

namespace Client
{
	class Client
	{
		static void Main(string[] args)
		{
			HttpChannel channel = new HttpChannel();
			ChannelServices.RegisterChannel(channel);

			RemotingConfiguration.RegisterActivatedClientType(
				typeof(Server.MyRemoteObject),
				"http://localhost:1234/MyServer");
			
			Console.WriteLine("Client.Main(): Creating first object");

			Server.MyRemoteObject obj1 = new Server.MyRemoteObject();

			obj1.setValue(42);

			Console.WriteLine("Client.Main(): Creating second object");
			Server.MyRemoteObject obj2 = new Server.MyRemoteObject();
			obj2.setValue(4711);

			Console.WriteLine("Obj1.getValue(): {0}",obj1.getValue());
			Console.WriteLine("Obj2.getValue(): {0}",obj2.getValue());

			Console.ReadLine();
		}	
	}
}
