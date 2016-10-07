using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;

namespace ContextBound
{

	public class CheckableContextProperty: IContextProperty, 
		IContributeObjectSink
	{
		public bool IsNewContextOK(System.Runtime.Remoting.Contexts.Context newCtx) 
		{
			return true;
		}

		public void Freeze(System.Runtime.Remoting.Contexts.Context newContext) 
		{
			// nothing to do
		}

		public string Name 
		{
			get 
			{
				return "Interception";
			}
		}

		public IMessageSink GetObjectSink(MarshalByRefObject obj, IMessageSink nextSink) 
		{
			return new CheckerSink(nextSink);
		}

	}
}
