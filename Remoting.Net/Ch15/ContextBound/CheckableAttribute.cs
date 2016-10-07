using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;

namespace ContextBound
{
	[AttributeUsage(AttributeTargets.Class)]
	public class CheckableAttribute: ContextAttribute
	{
		public CheckableAttribute(): base ("MyInterception") { }

		public override bool IsContextOK(Context ctx, 
			IConstructionCallMessage ctor) 
		{
			// if this is already an intercepting context, it's ok for us
			return ctx.GetProperty("Interception") != null;
		}

		public override void GetPropertiesForNewContext(
			IConstructionCallMessage ctor)
		{
			// add the context property which will later create a sink
			ctor.ContextProperties.Add(new CheckableContextProperty());
		}
	}
}
