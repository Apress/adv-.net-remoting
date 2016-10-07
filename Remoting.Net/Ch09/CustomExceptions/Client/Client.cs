using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels;

namespace Client
{

	class Client
	{
		static void Main(string[] args)
		{
			String filename = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
			RemotingConfiguration.Configure(filename);

			IRemoteExceptionTest tst = (IRemoteExceptionTest) RemotingHelper.CreateProxy(typeof(IRemoteExceptionTest));

      try
      {
        tst.TestException();
      }
      catch (RemotingException ex)
      {
        Console.WriteLine("-- Remoting Exception");
      }
      catch (ConcurrencyException ex)
      {
        Console.WriteLine("-- Concurrency " + ex.DatabaseTable);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.GetType().Name);
      }


      

      Console.ReadLine();

    }	
	}
}
