using System;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.IO;

namespace EncryptionSink
{

	public class EncryptionServerSink: BaseChannelSinkWithProperties,
		IServerChannelSink
	{

		private IServerChannelSink _nextSink;
		private byte[] _encryptionKey;
		private String _encryptionAlgorithm;

		public EncryptionServerSink(IServerChannelSink next, byte[] encryptionKey, String encryptionAlgorithm)
		{
			_encryptionKey = encryptionKey;
			_encryptionAlgorithm = encryptionAlgorithm;
			_nextSink = next;
		}

		public ServerProcessing ProcessMessage(IServerChannelSinkStack sinkStack, 
			IMessage requestMsg, 
			ITransportHeaders requestHeaders,
			Stream requestStream, 
			out IMessage responseMsg, 
			out ITransportHeaders responseHeaders, 
			out Stream responseStream) 
		{

			bool isEncrypted=false;

			//checking the headers
			if (requestHeaders["X-Encrypt"] != null && 
				requestHeaders["X-Encrypt"].Equals("yes")) 
			{

				isEncrypted = true;

				byte[] IV = Convert.FromBase64String(
					(String) requestHeaders["X-EncryptIV"]);

				// decrypt the request
				requestStream = EncryptionHelper.ProcessInboundStream(
					requestStream,
					_encryptionAlgorithm, 
					_encryptionKey,
					IV);
			}


			// pushing onto stack and forwarding the call,
			// the flag "isEncrypted" will be used as state
			sinkStack.Push(this,isEncrypted);

			ServerProcessing srvProc = _nextSink.ProcessMessage(sinkStack,
				requestMsg,
				requestHeaders,
				requestStream,
				out responseMsg,
				out responseHeaders,
				out responseStream);

			if (isEncrypted) 
			{
				// encrypting the response if necessary
				byte[] IV;

				responseStream = EncryptionHelper.ProcessOutboundStream(responseStream,
					_encryptionAlgorithm,_encryptionKey,out IV);

				responseHeaders["X-Encrypt"]="yes";
				responseHeaders["X-EncryptIV"]= Convert.ToBase64String(IV);
			}

			// returning status information
			return srvProc;
		}

		public void AsyncProcessResponse(IServerResponseChannelSinkStack sinkStack, 
			object state, 
			IMessage msg, 
			ITransportHeaders headers, 
			Stream stream) 
		{
			// fetching the flag from the async-state
			bool isEncrypted = (bool) state;


			if (isEncrypted) 
			{
				// encrypting the response if necessary
				byte[] IV;

				stream = EncryptionHelper.ProcessOutboundStream(stream,
					_encryptionAlgorithm,_encryptionKey,out IV);

				headers["X-Encrypt"]="yes";
				headers["X-EncryptIV"]= Convert.ToBase64String(IV);
			}


			// forwarding to the stack for further ProcessIng
			sinkStack.AsyncProcessResponse(msg,headers,stream);
		}

		public Stream GetResponseStream(IServerResponseChannelSinkStack sinkStack, 
			object state, 
			IMessage msg, 
			ITransportHeaders headers)
		{
			return null;
		}

		public IServerChannelSink NextChannelSink 
		{
			get 
			{
				return _nextSink;
			}
		}


	}
}
