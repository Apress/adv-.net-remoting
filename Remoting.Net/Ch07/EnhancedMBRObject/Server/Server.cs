using System;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting;

namespace Server
{
	public class DefaultLifeTimeSingleton: ExtendedMBRObject
	{
    public DefaultLifeTimeSingleton()
    {
      Console.WriteLine("DefaultLifeTimeSingleton.CTOR called");
    }
		public void DoSomething() 
		{
			Console.WriteLine("DefaultLifeTimeSingleton.doSomething called");
		}
	}

	public class LongerLivingSingleton: ExtendedMBRObject
	{
		public LongerLivingSingleton() 
		{
			Console.WriteLine("LongerLivingSingleton.CTOR called");
		}

		public void DoSomething() 
		{
			Console.WriteLine("LongerLivingSingleton.doSomething called");
		}
	}

	public class InfinitelyLivingSingleton: ExtendedMBRObject
	{
		public InfinitelyLivingSingleton() 
		{
			Console.WriteLine("InfinitelyLivingSingleton.CTOR called");
		}

		public void DoSomething() 
		{
			Console.WriteLine("InfinitelyLivingSingleton.doSomething called");
		}
	}
}
