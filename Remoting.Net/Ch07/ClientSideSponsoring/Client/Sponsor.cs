using System;
using System.Runtime.Remoting.Lifetime;

namespace Client
{
  public class MySponsor: MarshalByRefObject, ISponsor 
  {

    public bool doRenewal = true;

    public TimeSpan Renewal(System.Runtime.Remoting.Lifetime.ILease lease) 
    {
      Console.WriteLine("{0} SPONSOR: Renewal() called", DateTime.Now);

      if (doRenewal) 
      {
        Console.WriteLine("{0} SPONSOR: Will renew (10 secs) ", DateTime.Now);
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
