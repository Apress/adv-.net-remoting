using System;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.IO;
using System.Text;

namespace CompressionSink
{
	public class CompressionClientSink: BaseChannelSinkWithProperties, 
								        IClientChannelSink
	{
		private IClientChannelSink _nextSink;

		public CompressionClientSink(IClientChannelSink next) 
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
			headers["X-Compress"]="yes";

			stream = CompressionHelper.GetCompressedStreamCopy(stream);

			// push onto stack and forward the request

			sinkStack.Push(this,null);
			_nextSink.AsyncProcessRequest(sinkStack,msg,headers,stream);
		}


		public void AsyncProcessResponse(IClientResponseChannelSinkStack sinkStack, 
											object state, 
											ITransportHeaders headers, 
											Stream stream) 
		{

			// decompress the stream if necessary
			String xcompress = (String) headers["X-Compress"];
			if (xcompress != null && xcompress == "yes") {
				stream = CompressionHelper.GetUncompressedStreamCopy(stream);
			}


			// forward the request
			sinkStack.AsyncProcessResponse(headers,stream);
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

			requestStream = CompressionHelper.GetCompressedStreamCopy(requestStream);
			requestHeaders["X-Compress"] = "yes";

			// forward the call to the next sink
			_nextSink.ProcessMessage(msg,
										requestHeaders,
										requestStream, 
										out responseHeaders, 
										out responseStream);

			// deflate the response if necessary
			String xcompress = (String) responseHeaders["X-Compress"];
			if (xcompress != null && xcompress == "yes") {
				responseStream = CompressionHelper.GetUncompressedStreamCopy(responseStream);
			}

		}


	}
}
