using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels;

namespace Server
{

	class CustomerManager: MarshalByRefObject, IRemoteExceptionTest
	{
		public void TestException()
    {
      throw new ConcurrencyException("FK Constraint", "Customer");
		}
	}

	class ServerStartup
	{
		static void Main(string[] args)
		{
			String filename = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
			RemotingConfiguration.Configure(filename);
			Console.WriteLine ("ServerStartup.Main(): Server started");
			Console.ReadLine();
		}
	}
}
