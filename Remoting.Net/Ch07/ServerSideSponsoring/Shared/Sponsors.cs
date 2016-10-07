using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Lifetime;
using System.Threading;
using Shared;

namespace Sponsors
{
   public class InstanceSponsor: ExtendedMBRObject, ISponsor
   {
      public DateTime lastKeepAlive = DateTime.Now;

      public InstanceSponsor()
      {
         Console.WriteLine("{0} SPONSOR: Created ", DateTime.Now);
         lastKeepAlive = DateTime.Now;
      }

      public void KeepAlive()
      {
         Console.WriteLine("{0} SPONSOR: KeepAlive() called", DateTime.Now);
         // tracks the time of the last keepalive call
         lastKeepAlive = DateTime.Now;
      }

      public TimeSpan Renewal(System.Runtime.Remoting.Lifetime.ILease lease)
      {
         Console.WriteLine("{0} SPONSOR: Renewal() called", DateTime.Now);

         // keepalive needs to be called at least every 5 seconds
         TimeSpan duration = DateTime.Now.Subtract(lastKeepAlive);
         if (duration.TotalSeconds < 5)
         {
            Console.WriteLine("{0} SPONSOR: Will renew (10 secs) ",
               DateTime.Now);
            return TimeSpan.FromSeconds(10);
         }
         else
         {
            Console.WriteLine("{0} SPONSOR: Won't renew further", DateTime.Now);
            return TimeSpan.Zero;
         }
      }
   }

}