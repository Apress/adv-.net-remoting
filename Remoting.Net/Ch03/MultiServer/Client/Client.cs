using System;
using System.Runtime.Remoting;
using General;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Proxies;

namespace Client
{

	class Client
	{
		static void Main(string[] args)
		{

			HttpChannel channel = new HttpChannel();
			ChannelServices.RegisterChannel(channel);
			
			IRemoteObject obj = (IRemoteObject) Activator.GetObject(
				typeof(IRemoteObject),
				"http://localhost:1234/MyRemoteObject.soap");
			Console.WriteLine("Client.Main(): Reference to rem.obj. on " + 
							  "Server [1] acquired");

			Console.WriteLine("Client.Main(): Will set value to 42");
			obj.SetValue(42);
			int tmp = obj.GetValue();
			Console.WriteLine("Client.Main(): New server side value {0}", tmp);


			IWorkerObject workerobj = (IWorkerObject) Activator.GetObject(
				typeof(IWorkerObject),
				"http://localhost:1235/MyWorkerObject.soap");
			Console.WriteLine("Client.Main(): Reference to rem. workerobj. on " +
							  "Server [2] acquired");


			Console.WriteLine("Client.Main(): Will now call method on Srv [2]");
			workerobj.DoSomething(obj);

			tmp = obj.GetValue();
			Console.WriteLine("Client.Main(): New server side value {0}", tmp);

			Console.ReadLine();
		}	
	}
}
