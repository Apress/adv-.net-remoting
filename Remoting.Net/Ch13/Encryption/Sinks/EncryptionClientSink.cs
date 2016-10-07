using System;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.IO;
using System.Text;

namespace EncryptionSink
{
	public class EncryptionClientSink: BaseChannelSinkWithProperties, 
		IClientChannelSink
	{
		private IClientChannelSink _nextSink;
		private byte[] _encryptionKey;
		private String _encryptionAlgorithm;

		public EncryptionClientSink(IClientChannelSink next, 
			byte[] encryptionKey,
			String encryptionAlgorithm)
 
		{
			_nextSink = next;
			_encryptionKey = encryptionKey;
			_encryptionAlgorithm = encryptionAlgorithm;
		}

		public void ProcessMessage(IMessage msg, 
			ITransportHeaders requestHeaders, 
			Stream requestStream, 
			out ITransportHeaders responseHeaders, 
			out Stream responseStream) 
		{

			byte[] IV;

			requestStream = EncryptionHelper.ProcessOutboundStream(requestStream,
				_encryptionAlgorithm,_encryptionKey,out IV);

			requestHeaders["X-Encrypt"]="yes";
			requestHeaders["X-EncryptIV"]= Convert.ToBase64String(IV);


			// forward the call to the next sink
			_nextSink.ProcessMessage(msg,
				requestHeaders,
				requestStream, 
				out responseHeaders, 
				out responseStream);


			if (responseHeaders["X-Encrypt"] != null && 
				responseHeaders["X-Encrypt"].Equals("yes")) 
			{

				IV = Convert.FromBase64String((String) responseHeaders["X-EncryptIV"]);

				responseStream = EncryptionHelper.ProcessInboundStream(
					responseStream,
					_encryptionAlgorithm,
					_encryptionKey,
					IV);
			}

		}

		public void AsyncProcessRequest(IClientChannelSinkStack sinkStack, 
			IMessage msg, 
			ITransportHeaders headers, 
			Stream stream) 
		{
			byte[] IV;

			stream = EncryptionHelper.ProcessOutboundStream(stream,
				_encryptionAlgorithm,_encryptionKey,out IV);

			headers["X-Encrypt"]="yes";
			headers["X-EncryptIV"]= Convert.ToBase64String(IV);

			// push onto stack and forward the request
			sinkStack.Push(this,null);
			_nextSink.AsyncProcessRequest(sinkStack,msg,headers,stream);
		}


		public void AsyncProcessResponse(IClientResponseChannelSinkStack sinkStack, 
			object state, 
			ITransportHeaders headers, 
			Stream stream) 
		{


			if (headers["X-Encrypt"] != null && headers["X-Encrypt"].Equals("yes")) 
			{

				byte[] IV = 
					Convert.FromBase64String((String) headers["X-EncryptIV"]);

				stream = EncryptionHelper.ProcessInboundStream(
					stream,
					_encryptionAlgorithm,
					_encryptionKey,
					IV);
			}

			// forward the request
			sinkStack.AsyncProcessResponse(headers,stream);
		}


		public Stream GetRequestStream(IMessage msg, 
			ITransportHeaders headers) 
		{
			return null; // request stream will be manipulated later
		}

		public IClientChannelSink NextChannelSink 
		{
			get 
			{
				return _nextSink;
			}
		}

	}
}
