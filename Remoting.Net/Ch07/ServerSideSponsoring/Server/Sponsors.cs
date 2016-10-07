using System;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting;
using Server; // for ExtendedMBRObject
using Shared; // for IRemoteSponsor and IRemoteSponsorFactory

namespace Sponsors
{
   public class SponsorFactory: MarshalByRefObject, IRemoteSponsorFactory
   {
      public InstanceSponsor CreateSponsor()
      {
         return new InstanceSponsor();
      }
   }
}