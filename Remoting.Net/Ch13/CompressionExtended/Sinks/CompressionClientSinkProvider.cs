using System;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;
using System.Collections;

namespace CompressionSink
{
	public class CompressionClientSinkProvider: IClientChannelSinkProvider
	{
		private IClientChannelSinkProvider _nextProvider;

		public CompressionClientSinkProvider(IDictionary properties, ICollection providerData) 
		{
		}

		public IClientChannelSinkProvider Next
		{
			get {return _nextProvider; }
			set {_nextProvider = value;}
		}

		public IClientChannelSink CreateSink(IChannelSender channel, string url, object remoteChannelData) 
		{
			// create other sinks in the chain
			IClientChannelSink next = _nextProvider.CreateSink(channel,
				url,
				remoteChannelData);	
	
			// put our sink on top of the chain and return it				
			return new CompressionClientSink(next);
		}
	}
}
