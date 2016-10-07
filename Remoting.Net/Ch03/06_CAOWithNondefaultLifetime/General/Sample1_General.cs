using System;

namespace General
{
	public interface IRemoteObject
	{
		void setValue(int newval);
		int getValue();
	}

	public interface IRemoteFactory
	{
		IRemoteObject getNewInstance();
		IRemoteObject getNewInstance(int initvalue);
	}
}
