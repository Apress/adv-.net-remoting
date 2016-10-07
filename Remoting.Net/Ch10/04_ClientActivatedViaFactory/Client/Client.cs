using System;
using System.Collections;
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
				"http://localhost:1235/factory.soap");
			
			IRemoteObject obj1 = fact.GetNewInstance();
      

      Console.WriteLine("Done");
			Console.ReadLine();
		}	
	}
}
