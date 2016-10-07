using System;
using System.Runtime.Remoting;
using System.Threading;
using Shared;

namespace Client
{
	class Client
	{
		static void Main(string[] args)
		{
      String filename = AppDomain .CurrentDomain.SetupInformation.ConfigurationFile;
			RemotingConfiguration.Configure(filename);
      
      IRemoteFactory fact = (IRemoteFactory) RemotingHelper.CreateProxy(typeof(IRemoteFactory));
      IRemoteObject cao = fact.CreateInstance();

      try 
      {
        Console.WriteLine("{0} CLIENT: Calling doSomething()", DateTime.Now);
        cao.DoSomething();
      } 
      catch (Exception e) 
      {
        Console.WriteLine(" --> EX: Timeout in first call\n{0}",e.Message);
      }
			
      Console.WriteLine("{0} CLIENT: Sleeping for 5 seconds", DateTime.Now);
      Thread.Sleep(5000);

      try 
      {
        Console.WriteLine("{0} CLIENT: Calling doSomething()", DateTime.Now);
        cao.DoSomething();
      } 
      catch (Exception e) 
      {
        Console.WriteLine(" --> EX: Timeout in second call\n{0}",e.Message );
      }

      Console.WriteLine("Finished ... press <return> to exit");
      Console.ReadLine();
			Console.ReadLine();
		}	
	}
}

