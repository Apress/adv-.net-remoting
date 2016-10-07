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
		delegate void SetValueDelegate(int value);
		delegate String GetNameDelegate();

		static void Main(string[] args)
		{
			DateTime start = System.DateTime.Now;

			HttpChannel channel = new HttpChannel();
			ChannelServices.RegisterChannel(channel);
			IMyRemoteObject obj = (IMyRemoteObject) Activator.GetObject(
				typeof(IMyRemoteObject),
				"http://localhost:1234/MyRemoteObject.soap");
			Console.WriteLine("Client.Main(): Reference to rem.obj. acquired");


			Console.WriteLine("Client.Main(): Will call setValue(42)");
			SetValueDelegate svDelegate = new SetValueDelegate(obj.SetValue);
			IAsyncResult svAsyncres = svDelegate.BeginInvoke(42,null,null);
			Console.WriteLine("Client.Main(): Invocation done");

			Console.WriteLine("Client.Main(): Will call getName()");
			GetNameDelegate gnDelegate = new GetNameDelegate(obj.GetName);
			IAsyncResult gnAsyncres = gnDelegate.BeginInvoke(null,null);
			Console.WriteLine("Client.Main(): Invocation done");

			Console.WriteLine("Client.Main(): EndInvoke for setValue()");
			svDelegate.EndInvoke(svAsyncres);
			Console.WriteLine("Client.Main(): EndInvoke for getName()");
			String name = gnDelegate.EndInvoke(gnAsyncres);

			Console.WriteLine("Client.Main(): received name {0}",name);

			Console.WriteLine("Client.Main(): Will now read value");
			int tmp = obj.GetValue();
			Console.WriteLine("Client.Main(): New server side value {0}", tmp);

			DateTime end = System.DateTime.Now;
			TimeSpan duration = end.Subtract(start);
			Console.WriteLine("Client.Main(): Execution took {0} seconds.",
							   duration.Seconds);

			Console.ReadLine();
		}	
	}
}
