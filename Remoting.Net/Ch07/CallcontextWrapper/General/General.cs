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

   public class LogSettingContext
   {
      public static bool EnableLog
      {
         get
         {
            LogSettings ls = CallContext.GetData("log_settings") as LogSettings;
         
            if (ls!= null)
            {
               return ls.EnableLog;
            }
            else
            {
               return false;
            }
         }
         set
         {
            LogSettings ls = new LogSettings();
            ls.EnableLog = value;
            CallContext.SetData("log_settings", ls);
         }
      }
   }
}
