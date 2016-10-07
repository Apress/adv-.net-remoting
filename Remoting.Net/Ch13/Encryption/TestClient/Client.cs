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

			try { 
				String res = obj.doSomething();
				Console.WriteLine("Got result: {0}",res);
			} catch (Exception e) {
				Console.WriteLine("EX: {0}\n\n{1}",e.Message,e.ToString());
			}
		
			Console.WriteLine("Finished ...");
			Console.ReadLine();
		}	
	}
}

