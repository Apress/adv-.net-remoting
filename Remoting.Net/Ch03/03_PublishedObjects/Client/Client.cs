using System;
using System.Runtime.Remoting;
using General;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels;

namespace Client
{

	class Client
	{
		static void Main(string[] args)
		{
			HttpChannel channel = new HttpChannel();
			ChannelServices.RegisterChannel(channel);
			
			IMyRemoteObject obj = (IMyRemoteObject) Activator.GetObject(
				typeof(IMyRemoteObject),
				"http://localhost:1234/MyRemoteObject.soap");
			Console.WriteLine("Client.Main(): Reference to rem.obj. acquired");

			int tmp = obj.GetValue();

			Console.WriteLine("Client.Main(): Original server side value: {0}",tmp);
			Console.WriteLine("Client.Main(): Will set value to 42");
			obj.SetValue(42);
			tmp = obj.GetValue();
			Console.WriteLine("Client.Main(): New server side value {0}", tmp);


			Console.ReadLine();
		}	
	}
}
