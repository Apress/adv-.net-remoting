using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Lifetime;
using System.Threading;
using Shared;
using Sponsors;

namespace Client
{
   class ClientApp
   {
      static void Main(string[] args)
      {
         String filename = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
         RemotingConfiguration.Configure(filename);
   
         IRemoteFactory fact = (IRemoteFactory) RemotingHelper.CreateProxy(typeof(IRemoteFactory));
         IRemoteObject cao = fact.CreateInstance();

         IRemoteSponsorFactory sf = (IRemoteSponsorFactory) RemotingHelper.CreateProxy(typeof(IRemoteSponsorFactory));
         InstanceSponsor sp = sf.CreateSponsor();

         EnsureKeepAlive keepalive = new EnsureKeepAlive(sp);

         ILease le = (ILease) ((MarshalByRefObject) cao).GetLifetimeService();
         le.Register(sp);

         try 
         {
            Console.WriteLine("{0} CLIENT: Calling DoSomething()", DateTime.Now);
            cao.DoSomething();
         } 
         catch (Exception e) 
         {
            Console.WriteLine(" --> EX: Timeout in first call\n{0}",e.Message);
         }
		
         Console.WriteLine("{0} CLIENT: Sleeping for 6 seconds", DateTime.Now);
         Thread.Sleep(6000);

         try 
         {
            Console.WriteLine("{0} CLIENT: Calling DoSomething()", DateTime.Now);
            cao.DoSomething();
         } 
         catch (Exception e) 
         {
            Console.WriteLine(" --> EX: Timeout in second call\n{0}",e.Message );
         }

         Console.WriteLine("{0} CLIENT: Unregistering sponsor", DateTime.Now);
         le.Unregister(sp);
         keepalive.StopKeepAlive();

         Console.WriteLine("Finished ... press <return> to exit");
         Console.ReadLine();
         Console.ReadLine();
      }	
   }

   class EnsureKeepAlive
   {
      private bool _keepServerAlive;
      private InstanceSponsor _sponsor;

      public EnsureKeepAlive(InstanceSponsor sponsor)
      {
         _sponsor = sponsor;
         _keepServerAlive = true;
         Console.WriteLine("{0} KEEPALIVE: Starting thread()", DateTime.Now);
         Thread thrd = new Thread(new ThreadStart(this.KeepAliveThread));
         thrd.Start();
      }

      public void StopKeepAlive()
      {
         _keepServerAlive= false;
      }

      public void KeepAliveThread()
      {
         while (_keepServerAlive)
         {
            Console.WriteLine("{0} KEEPALIVE: Will KeepAlive()", DateTime.Now);
            _sponsor.KeepAlive();
            Thread.Sleep(3000);
         }
      }
   }

}