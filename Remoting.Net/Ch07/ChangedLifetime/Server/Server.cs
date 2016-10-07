using System;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting;

namespace Server
{
	public class DefaultLifeTimeSingleton: MarshalByRefObject
	{
		
		public DefaultLifeTimeSingleton() 
		{
			Console.WriteLine("DefaultLifeTimeSingleton.CTOR called");
		}

		public void doSomething() 
		{
			Console.WriteLine("DefaultLifeTimeSingleton.doSomething called");
		}
	}

	public class LongerLivingSingleton: MarshalByRefObject
	{
				
		public override object InitializeLifetimeService() 
		{
			ILease tmp = (ILease) base.InitializeLifetimeService();
			if (tmp.CurrentState == LeaseState.Initial)  
			{
				tmp.InitialLeaseTime = TimeSpan.FromSeconds(5);
				tmp.RenewOnCallTime = TimeSpan.FromSeconds(1);
			}
			return tmp;
		}

		public LongerLivingSingleton() 
		{
			Console.WriteLine("LongerLivingSingleton.CTOR called");
		}

		public void doSomething() 
		{
			Console.WriteLine("LongerLivingSingleton.doSomething called");
		}
	}

	public class InfinitelyLivingSingleton: MarshalByRefObject
	{
		public override object InitializeLifetimeService() 
		{
			return null;
		}		
		public InfinitelyLivingSingleton() 
		{
			Console.WriteLine("InfinitelyLivingSingleton.CTOR called");
		}

		public void doSomething() 
		{
			Console.WriteLine("InfinitelyLivingSingleton.doSomething called");
		}
	}
}
