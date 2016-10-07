using System;

namespace General
{
	public interface IRemoteObject
	{
		void SetValue(int newval);
		int GetValue();
	}

	public interface IWorkerObject
	{
		void DoSomething(IRemoteObject usethis);
	}
}
