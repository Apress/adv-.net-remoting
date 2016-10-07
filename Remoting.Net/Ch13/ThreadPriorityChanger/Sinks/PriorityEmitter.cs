using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace PrioritySinks
{


	public class PriorityEmitterSink : BaseChannelObjectWithProperties, IClientChannelSink, IMessageSink
	{	
		private IMessageSink _nextMsgSink;

		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink) 
		{
			// only for method calls 
			if (msg as IMethodCallMessage != null) 
			{
				LogicalCallContext lcc = 
					(LogicalCallContext) msg.Properties["__CallContext"];
				lcc.SetData("priority",Thread.CurrentThread.Priority);
				return _nextMsgSink.AsyncProcessMessage(msg,replySink);
			} 
			else 
			{
				return _nextMsgSink.AsyncProcessMessage(msg,replySink);
			}
		}

		public IMessage SyncProcessMessage(IMessage msg) 
		{
			// only for method calls 
			if (msg as IMethodCallMessage != null) 
			{
				LogicalCallContext lcc = 
					(LogicalCallContext) msg.Properties["__CallContext"];
				lcc.SetData("priority",Thread.CurrentThread.Priority);
				return _nextMsgSink.SyncProcessMessage(msg);
			} 
			else 
			{
				return _nextMsgSink.SyncProcessMessage(msg);
			}
		}

		public PriorityEmitterSink (object next) 
		{
			if (next as IMessageSink != null) 
			{
				_nextMsgSink = (IMessageSink) next;
			}
		}


		public IMessageSink NextSink 
		{
			get 
			{
				return _nextMsgSink;
			}
		}

		public IClientChannelSink NextChannelSink 
		{ 
			get 
			{ 
				throw new RemotingException("Wrong sequence.");
			} 
		}

		// Methods
		public void AsyncProcessRequest(IClientChannelSinkStack sinkStack, 
			IMessage msg, 
			ITransportHeaders headers, 
			Stream stream) 
		{
			throw new RemotingException("Wrong sequence.");
		}

		public void AsyncProcessResponse(
			IClientResponseChannelSinkStack sinkStack, 
			object state, 
			ITransportHeaders headers, 
			Stream stream)
		{
			throw new RemotingException("Wrong sequence.");
		}

		public System.IO.Stream GetRequestStream(IMessage msg, 
			ITransportHeaders headers)
		{
			throw new RemotingException("Wrong sequence.");
		}

		public void ProcessMessage(IMessage msg, 
			ITransportHeaders requestHeaders, 
			Stream requestStream, 
			out ITransportHeaders responseHeaders, 
			out Stream responseStream)
		{
			throw new RemotingException("Wrong sequence.");
		}



	}
}
