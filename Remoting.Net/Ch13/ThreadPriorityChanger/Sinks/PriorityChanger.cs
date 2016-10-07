using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging ;
using System.Runtime.Remoting.Channels;
using System.Threading;

namespace PrioritySinks
{

	public class PriorityChangerSink : BaseChannelObjectWithProperties, 
		IServerChannelSink, IChannelSinkBase
	{	

		private IServerChannelSink _next;

		public PriorityChangerSink (IServerChannelSink next) 
		{
			_next = next;
		}

		public void AsyncProcessResponse (IServerResponseChannelSinkStack sinkStack, Object state , IMessage msg , ITransportHeaders headers , Stream stream ) 
		{
			// restore the priority
			ThreadPriority priority = (ThreadPriority) state;
			Console.WriteLine("  -> Post-execution change back to {0}",priority);
			Thread.CurrentThread.Priority = priority;
		}

		public Stream GetResponseStream ( System.Runtime.Remoting.Channels.IServerResponseChannelSinkStack sinkStack , System.Object state , System.Runtime.Remoting.Messaging.IMessage msg , System.Runtime.Remoting.Channels.ITransportHeaders headers ) 
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
			LogicalCallContext lcc = 
				(LogicalCallContext) requestMsg.Properties["__CallContext"];

			// storing the current priority
			ThreadPriority oldprio = Thread.CurrentThread.Priority;

			// check if the logical call context contains "priority"
			if (lcc != null && lcc.GetData("priority") != null) 
			{
				// fetch the priorty from the call context
				ThreadPriority priority = 
					(ThreadPriority) lcc.GetData("priority");

				Console.WriteLine("  -> Pre-execution priority change {0} to {1}",
					oldprio.ToString(),priority.ToString());

				// set the priority
				Thread.CurrentThread.Priority = priority;
			}



			// push on the stack and pass the call to the next sink
			// the old priority will be used as "state" for the response
			sinkStack.Push(this,oldprio);
			ServerProcessing spres =  _next.ProcessMessage (sinkStack,
				requestMsg, requestHeaders, requestStream, 
				out responseMsg,out responseHeaders,out responseStream);
			
			//restore priority if call is not asynchronous

			if (spres != ServerProcessing.Async) 
			{
				if (lcc != null && lcc.GetData("priority") != null) 
				{
					Console.WriteLine("  -> Post-execution change back to {0}",oldprio);
					Thread.CurrentThread.Priority = oldprio; 
				}
			}

			return spres;

		}
   
		public IServerChannelSink NextChannelSink 
		{
			get {return _next;}
			set {_next = value;}
		}
	}
}
