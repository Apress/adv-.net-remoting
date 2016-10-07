using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Services;

namespace ServerHost
{
  class ServerStartup
  {
    public static void Main(String[] args) 
    {
       String cfg = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
       RemotingConfiguration.Configure(cfg);
       TrackingServices.RegisterTrackingHandler(new MyTracking());
       Console.WriteLine("Press <return> to exit");
       Console.ReadLine();
    }
  }

   class MyTracking: ITrackingHandler
   {
      public void DisconnectedObject(object obj)
      {
         Console.WriteLine("Disconnected " + obj.GetType().Name);
      }

      public void UnmarshaledObject(object obj, ObjRef or)
      {
         Console.WriteLine("Unmarshaled " + obj.GetType().Name + " local:" + or.IsFromThisAppDomain());
      }

      public void MarshaledObject(object obj, ObjRef or)
      {
         Console.WriteLine("Marshaled " + obj.GetType().Name);
      }
   }

}
