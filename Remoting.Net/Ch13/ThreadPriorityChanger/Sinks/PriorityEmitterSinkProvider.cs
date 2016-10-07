using System;
using System.Collections;
using System.Runtime.Remoting.Channels;

namespace PrioritySinks
{
	public class PriorityEmitterSinkProvider: IClientChannelSinkProvider 
	{

		private IClientChannelSinkProvider next = null;

		public PriorityEmitterSinkProvider(IDictionary properties, 
			ICollection providerData)
		{
			// not needed
		}

		public IClientChannelSink CreateSink(IChannelSender channel, 
			string url, object remoteChannelData)
		{
			IClientChannelSink nextsink = 
				next.CreateSink(channel,url,remoteChannelData);

			return new PriorityEmitterSink(nextsink);
		}

		public IClientChannelSinkProvider Next
		{
			get { return next; }
			set { next = value; }
		}
	}
}