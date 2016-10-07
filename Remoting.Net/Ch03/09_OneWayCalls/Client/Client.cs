using System;
using System.Runtime.Remoting;
using General;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Proxies;
using System.Threading;

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

			Console.WriteLine("Client.Main(): Will call setValue(42)");
			try 
			{
				obj.SetValue(42);
				Console.WriteLine("Client.Main(): Value successfully set.");
			} 
			catch (Exception e) 
			{
				Console.WriteLine("Client.Main(): EXCEPTION.\n{0}",e.Message);
			}
			// wait for keypress
			Console.ReadLine();
		}	
	}
}
