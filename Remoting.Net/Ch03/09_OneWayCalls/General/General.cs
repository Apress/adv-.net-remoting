using System;
using System.Runtime.Remoting.Messaging;

namespace General
{
	public interface IMyRemoteObject
	{
		// no more oneway attribute [OneWay()]
		void SetValue(int newval);
		int GetValue();
		String GetName();
	}
}
