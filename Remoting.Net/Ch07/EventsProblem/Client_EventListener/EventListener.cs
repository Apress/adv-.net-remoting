using System;
using System.Runtime.Remoting;
using General;
using RemotingTools; // RemotingHelper

namespace EventListener
{
	class EventListener
	{
		static void Main(string[] args)
		{
			String filename = "eventlistener.exe.config";
			RemotingConfiguration.Configure(filename);

			IBroadcaster bcaster = 
				(IBroadcaster) RemotingHelper.GetObject(typeof(IBroadcaster));

			Console.WriteLine("Registering event at server");

			// callbacks can only work on MarshalByRefObjects, so 
			// I created a different class for this as well
			EventHandler eh = new EventHandler();
			bcaster.MessageArrived += 
				new MessageArrivedHandler(eh.HandleMessage);

			Console.WriteLine("Event registered. Waiting for messages.");
			Console.ReadLine();
		}	

	}

	public class EventHandler: MarshalByRefObject {
		public void HandleMessage(String msg) {
			Console.WriteLine("Received: {0}",msg);
		}

		public override object InitializeLifetimeService() {
			// this object has to live "forever"
			return null;
		}	
	}


}

