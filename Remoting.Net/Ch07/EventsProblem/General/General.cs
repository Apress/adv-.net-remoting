using System;
using System.Runtime.Remoting.Messaging;

namespace General {

	public delegate void MessageArrivedHandler(String msg);

	public interface IBroadcaster {
		void BroadcastMessage(String msg);
		event MessageArrivedHandler MessageArrived;
	}
}
