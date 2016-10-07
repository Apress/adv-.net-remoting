using System;
using System.Runtime.Remoting.Messaging;

namespace General
{
	public interface IRemoteCustomerManager
	{
		Customer GetCustomer(int id);
	}

	[Serializable]
	public class Customer
	{
      // implementation removed
	}

   [Serializable]
   public class LogSettings: ILogicalThreadAffinative
   {
      public bool EnableLog;
   }
}
