using System;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.IO;


namespace UrlAuthenticationSink
{
	public class UrlAuthenticationSink: BaseChannelSinkWithProperties, 
								        IClientChannelSink
	{
		private IClientChannelSink _nextSink;
		private bool _authenticationParamsSet;

		public UrlAuthenticationSink(IClientChannelSink next) 
		{
			_nextSink = next;
		}

		public IClientChannelSink NextChannelSink 
		{
			get {
				return _nextSink;
			}
		}


		public void AsyncProcessRequest(IClientChannelSinkStack sinkStack, 
			IMessage msg, 
			ITransportHeaders headers, 
			Stream stream) 
		{
			SetSinkProperties(msg);

			// don't push on the sinkstack because this sink doesn’t need
			// to handle any replies!

			_nextSink.AsyncProcessRequest(sinkStack,msg,headers,stream);

		}


		public void AsyncProcessResponse(
			IClientResponseChannelSinkStack sinkStack, 
			object state, 
			ITransportHeaders headers, 
			Stream stream) 
		{
			// not needed
		}


		public Stream GetRequestStream(IMessage msg, 
		                               ITransportHeaders headers) 
		{
			return _nextSink.GetRequestStream(msg, headers);
		}


		public void ProcessMessage(IMessage msg, 
		                           ITransportHeaders requestHeaders, 
		                           Stream requestStream, 
		                           out ITransportHeaders responseHeaders, 
		                           out Stream responseStream) 
		{

			SetSinkProperties(msg);

			_nextSink.ProcessMessage(msg,requestHeaders,requestStream,
				out responseHeaders,out responseStream);
		}

		private void SetSinkProperties(IMessage msg) 
		{
			if (! _authenticationParamsSet) 
			{
				String url = (String) msg.Properties["__Uri"];
				
				UrlAuthenticationEntry entr = 
					UrlAuthenticator.GetAuthenticationEntry(url);

				if (entr != null) 
				{
					IClientChannelSink last = this;

					while (last.NextChannelSink != null) 
					{
						last = last.NextChannelSink;
					}

					// last now contains the transport channel sink 

					last.Properties["username"] = entr.Username;
					last.Properties["password"] = entr.Password;
				}
				



				_authenticationParamsSet = true;
			}

		}
													 


	}
}
