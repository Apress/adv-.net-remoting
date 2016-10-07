using System;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting;
using Shared;


namespace Server
{
	public class RemoteFactory: ExtendedMBRObject, IRemoteFactory
  {
    public IRemoteObject CreateInstance()
    {
      return new RemoteObject();
    }
  }

	public class RemoteObject: ExtendedMBRObject, IRemoteObject
	{
		public void DoSomething() 
		{
			Console.WriteLine("DoSomething() called");
    }
  }
}
