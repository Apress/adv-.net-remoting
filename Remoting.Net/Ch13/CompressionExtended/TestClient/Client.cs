using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Contexts ;
using System.Runtime.Remoting.Channels ;
using System.Runtime.Remoting.Services  ;
using System.Threading;
using Service; // from service.dll

namespace Client
{

	class Client
	{
		static void Main(string[] args)
		{
			String filename = "client.exe.config";
			RemotingConfiguration.Configure(filename);
			
			SomeSAO obj = new SomeSAO();

			String res = obj.doSomething();
		
			Console.WriteLine("Got result: {0}",res);
			Console.ReadLine();
		}	
	}
}

