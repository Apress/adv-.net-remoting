using System;
using System.Collections;
using System.Runtime.Remoting.Channels;

namespace HttpErrorInterceptor
{
	public class InterceptorSinkProvider: IClientChannelSinkProvider 
	{

		private IClientChannelSinkProvider next = null;

		public InterceptorSinkProvider(IDictionary properties, 
			ICollection providerData)
		{
			// nothing special here
		}

		public IClientChannelSink CreateSink(IChannelSender channel, 
			string url, object remoteChannelData)
		{
			IClientChannelSink nextsink = 
				next.CreateSink(channel,url,remoteChannelData);

			return new InterceptorSink(nextsink);
		}

		public IClientChannelSinkProvider Next
		{
			get { return next; }
			set { next = value; }
		}
	}
}