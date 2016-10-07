using System;
using System.Runtime.Remoting;
using General;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;

namespace Client
{
	class ClientApp
	{
      static void Main(string[] args)
      {
	      String filename = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
	      RemotingConfiguration.Configure(filename);

         if (args.Length > 0 && args[0].ToLower() == "/enablelog")
         {
            LogSettingContext.EnableLog=true;
         }

         IRemoteCustomerManager mgr = 
            (IRemoteCustomerManager) RemotingHelper.CreateProxy(typeof(IRemoteCustomerManager));
         Customer cust = mgr.GetCustomer(42);

         Console.WriteLine("Done");
	      Console.ReadLine();
      }	
	}
}
