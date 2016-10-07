using System;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;
using System.Collections;

namespace CompressionSink
{
	public class CompressionServerSinkProvider: IServerChannelSinkProvider
	{

		private IServerChannelSinkProvider _nextProvider;

		public CompressionServerSinkProvider(IDictionary properties, ICollection providerData) 
		{ 
		}

		public IServerChannelSinkProvider Next
		{
			get {return _nextProvider; }
			set {_nextProvider = value;}
		}

		public IServerChannelSink CreateSink(IChannelReceiver channel)
		{
			// create other sinks in the chain
			IServerChannelSink next = _nextProvider.CreateSink(channel);				
	
			// put our sink on top of the chain and return it				
			return new CompressionServerSink(next);
		}

		public void GetChannelData(IChannelDataStore channelData)
		{
			// not yet needed
		}

	}
}
