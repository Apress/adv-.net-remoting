using System;
using System.Runtime.Remoting.Channels;
using System.Collections;

namespace UrlAuthenticationSink
{
	public class UrlAuthenticationSinkProvider: IClientChannelSinkProvider
	{
		private IClientChannelSinkProvider _nextProvider;

		public UrlAuthenticationSinkProvider(IDictionary properties, ICollection providerData) 
		{
			foreach (SinkProviderData obj in providerData) 
			{
				if (obj.Name == "url") 
				{
					if (obj.Properties["base"] != null)
					{
						UrlAuthenticator.AddAuthenticationEntry(
							(String) obj.Properties["base"],
							(String) obj.Properties["username"],
							(String) obj.Properties["password"]);
					} 
					else 
					{
						UrlAuthenticator.SetDefaultAuthenticationEntry(
							(String) obj.Properties["username"],
							(String) obj.Properties["password"]);
					}
				}

			}
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
			return new UrlAuthenticationSink(next);
		}
	}
}
