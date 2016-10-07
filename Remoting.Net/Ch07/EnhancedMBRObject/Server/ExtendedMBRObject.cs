using System;
using System.Configuration;
using System.Runtime.Remoting.Lifetime;

namespace Server
{

  public class ExtendedMBRObject: MarshalByRefObject
  {
    public override object InitializeLifetimeService()
    {
      String myName = this.GetType().FullName;

      String lifetime =
        ConfigurationSettings.AppSettings[myName + "_Lifetime"];

      String renewoncall =
        ConfigurationSettings.AppSettings[myName + "_RenewOnCallTime"];

      String sponsorshiptimeout =
        ConfigurationSettings.AppSettings[myName + "_SponsorShipTimeout"];

      if (lifetime == "infinity")
      {
        return null;
      }
      else
      {
        ILease tmp = (ILease) base.InitializeLifetimeService();
        if (tmp.CurrentState == LeaseState.Initial)
        {
          if (lifetime != null)
          {
            tmp.InitialLeaseTime =
              TimeSpan.FromMilliseconds(Double.Parse(lifetime));
          }

          if (renewoncall != null)
          {
            tmp.RenewOnCallTime =
              TimeSpan.FromMilliseconds(Double.Parse(renewoncall));
          }

          if (sponsorshiptimeout != null)
          {
            tmp.SponsorshipTimeout =
              TimeSpan.FromMilliseconds(Double.Parse(sponsorshiptimeout));
          }
        }
        return tmp;
      }
    }
  }
}
