using System;
using System.Collections;
using System.Runtime.Remoting.Channels;

namespace PrioritySinks
{
	public class PriorityChangerSinkProvider: IServerChannelSinkProvider 
	{
		private IServerChannelSinkProvider next = null;

		public PriorityChangerSinkProvider(IDictionary properties, 
			ICollection providerData)
		{
			// not needed
		}

		public void GetChannelData (IChannelDataStore channelData)
		{
			// not needed
		}

		public IServerChannelSink CreateSink (IChannelReceiver channel)
		{
			IServerChannelSink nextSink = next.CreateSink(channel);
			return new PriorityChangerSink(nextSink);
		}

		public IServerChannelSinkProvider Next
		{
			get { return next; }
			set { next = value; }
		}

	}
}