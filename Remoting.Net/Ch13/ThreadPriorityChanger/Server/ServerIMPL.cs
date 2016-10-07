using System;
using System.Runtime.Remoting;
using System.Threading;

namespace Server
{
	public class TestSAO: MarshalByRefObject
	{
		public String getPriority() 
		{
			return System.Threading.Thread.CurrentThread.Priority.ToString();
		}
	}

	class ServerStartup
	{
		static void Main(string[] args)
		{
			String filename = "server.exe.config";
			RemotingConfiguration.Configure(filename);

			Console.WriteLine ("Server is running. Press <return> to exit");
			Console.ReadLine();
		}
	}
}
