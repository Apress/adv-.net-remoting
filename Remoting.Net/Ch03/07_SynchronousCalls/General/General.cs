using System;
using System.Runtime.Remoting.Messaging;

namespace General
{
	public interface IMyRemoteObject
	{
		void SetValue(int newval);
		int GetValue();
		String GetName();
	}
}
