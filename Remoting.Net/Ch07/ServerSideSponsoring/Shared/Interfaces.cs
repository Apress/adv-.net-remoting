using System;
using System.Runtime.Remoting.Lifetime;
using Sponsors;

namespace Shared
{

   public interface IRemoteFactory
   {
      IRemoteObject CreateInstance();
   }

   public interface IRemoteObject
   {
      void DoSomething();
   }

   public interface IRemoteSponsorFactory
   {
      InstanceSponsor CreateSponsor();
   }


}
