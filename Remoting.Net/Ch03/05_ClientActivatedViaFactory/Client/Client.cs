using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using General;

namespace Client
{
	class Client
	{
		static void Main(string[] args)
		{
			HttpChannel channel = new HttpChannel();
			ChannelServices.RegisterChannel(channel);

			Console.WriteLine("Client.Main(): Creating factory");
			IRemoteFactory fact = (IRemoteFactory) Activator.GetObject(
				typeof(IRemoteFactory),
				"http://localhost:1234/factory.soap");
			
			Console.WriteLine("Client.Main(): Acquiring first object from factory");
			IRemoteObject obj1 = fact.getNewInstance();
			obj1.setValue(42);

			Console.WriteLine("Client.Main(): Acquiring second object from factory");
			IRemoteObject obj2 = fact.getNewInstance(4711);

			Console.WriteLine("Obj1.getValue(): {0}",obj1.getValue());
			Console.WriteLine("Obj2.getValue(): {0}",obj2.getValue());

			Console.ReadLine();
		}	
	}
}
