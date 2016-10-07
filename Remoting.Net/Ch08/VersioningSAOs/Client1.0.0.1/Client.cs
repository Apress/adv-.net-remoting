using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Lifetime;
using System.Threading;
using VersionedSAO; // from generated_meta_xxx.dll

namespace Client
{
	class Client
	{
		static void Main(string[] args)
		{
			String filename = "client.exe.config";
			RemotingConfiguration.Configure(filename);

			SomeSAO obj = new SomeSAO();
			String result = obj.getSAOVersion();

			Console.WriteLine("Result: {0}",result);

			Console.WriteLine("Finished ... press <return> to exit");
			Console.ReadLine();
		}	
	}
}

