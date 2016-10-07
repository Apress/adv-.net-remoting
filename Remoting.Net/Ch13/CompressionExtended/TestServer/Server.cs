using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;

namespace Server
{
	class ServerStartup
	{
		public static void Main(String[] args) 
		{
			RemotingConfiguration.Configure("server.exe.config");

			Console.WriteLine("Press <return> to exit");
			Console.ReadLine();
		}
	}
}
