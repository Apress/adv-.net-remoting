using System;
using System.Runtime.Remoting;
using System.Threading;
using General;

namespace Server
{

	public class Broadcaster: MarshalByRefObject, IBroadcaster
	{

		public event General.MessageArrivedHandler MessageArrived;

		public void BroadcastMessage(string msg) {
			// call the delegate to notify all listeners
			Console.WriteLine("Will broadcast message: {0}");
			MessageArrived(msg);
		}

		public override object InitializeLifetimeService() {
			// this object has to live "forever"
			return null;
		}
	}


	class ServerStartup
	{
		static void Main(string[] args)
		{
			String filename = "server.exe.config";
			RemotingConfiguration.Configure(filename);

			Console.WriteLine ("Server started, press <return> to exit.");
			Console.ReadLine();
		}
	}
}
