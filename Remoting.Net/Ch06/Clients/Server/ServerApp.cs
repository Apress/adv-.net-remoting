using System;
using System.Runtime.Remoting;

using General;

namespace Server
{
	public class ServerImpl : MarshalByRefObject, IRemoteFactory 
	{
		private int _ageCount = 10;

		public Person GetPerson() 
		{
			System.Console.WriteLine(">> Incoming request...");
			System.Console.WriteLine(">> Returning person {0}...", _ageCount);

			Person p = new Person("Test", "App", _ageCount++);
			return p;
		}
	}

	class ServerApp
	{
		[STAThread]
		static void Main(string[] args)
		{
			System.Console.WriteLine("Starting server...");
			RemotingConfiguration.Configure("Server.exe.config");

			System.Console.WriteLine("Server configured, waiting for requests!");
			System.Console.ReadLine();
		}
	}
}
