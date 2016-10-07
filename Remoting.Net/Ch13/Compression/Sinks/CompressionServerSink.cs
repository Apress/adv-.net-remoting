using System;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.IO;

namespace CompressionSink
{

	public class CompressionServerSink: BaseChannelSinkWithProperties,
		IServerChannelSink
	{

		private IServerChannelSink _nextSink;

		public CompressionServerSink(IServerChannelSink next) 
		{
			_nextSink = next;
		}

		public IServerChannelSink NextChannelSink 
		{
			get 
			{
				return _nextSink;
			}
		}

		public void AsyncProcessResponse(IServerResponseChannelSinkStack sinkStack, 
			object state, 
			IMessage msg, 
			ITransportHeaders headers, 
			Stream stream) 
		{
			// compressing the response
			stream=CompressionHelper.getCompressedStreamCopy(stream);

			// forwarding to the stack for further processing
			sinkStack.AsyncProcessResponse(msg,headers,stream);
		}

		public Stream GetResponseStream(IServerResponseChannelSinkStack sinkStack, 
			object state, 
			IMessage msg, 
			ITransportHeaders headers)
		{
			return null;
		}

		public ServerProcessing ProcessMessage(IServerChannelSinkStack sinkStack, 
			IMessage requestMsg, 
			ITransportHeaders requestHeaders,
			Stream requestStream, 
			out IMessage responseMsg, 
			out ITransportHeaders responseHeaders, 
			out Stream responseStream) 
		{
			// uncompressing the request
			requestStream = 
				CompressionHelper.getUncompressedStreamCopy(requestStream);

			// pushing onto stack and forwarding the call
			sinkStack.Push(this,null);

			ServerProcessing srvProc = _nextSink.ProcessMessage(sinkStack,
				requestMsg,
				requestHeaders,
				requestStream,
				out responseMsg,
				out responseHeaders,
				out responseStream);

			// compressing the response
			responseStream=
				CompressionHelper.getCompressedStreamCopy(responseStream);

			// returning status information
			return srvProc;
		}
	}
}
