using System;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;
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
			// fetching the flag from the async-state
			bool hasBeenCompressed = (bool) state;

			// compressing the response if necessary
			if (hasBeenCompressed) {
				stream=CompressionHelper.GetCompressedStreamCopy(stream);
				headers["X-Compress"] = "yes";
			}


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

			bool isCompressed=false;

			
			// decompress the stream if necessary
			String xcompress = (String) requestHeaders["X-Compress"];
			if (xcompress != null && xcompress == "yes") {
				requestStream = CompressionHelper.GetUncompressedStreamCopy(requestStream);
				isCompressed = true;
			}


			// Pushing onto stack and forwarding the call.
			// The state object contains true if the request has been compressed,
			// else false.
			sinkStack.Push(this,isCompressed);

			ServerProcessing srvProc = _nextSink.ProcessMessage(sinkStack,
				requestMsg,
				requestHeaders,
				requestStream,
				out responseMsg,
				out responseHeaders,
				out responseStream);

			if (srvProc == ServerProcessing.Complete ) {
				// compressing the response if necessary
				if (isCompressed) {
					responseStream= CompressionHelper.GetCompressedStreamCopy(responseStream);
					responseHeaders["X-Compress"] = "yes";
				}

			}
			// returning status information
			return srvProc;
		}
	}
}
