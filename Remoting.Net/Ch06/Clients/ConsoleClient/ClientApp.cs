using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Activation;

using General;
using General.Client;

namespace ConsoleClient
{
	class ClientApp
	{
		[STAThread]
		static void Main(string[] args)
		{
			System.Console.WriteLine("Configuring client...");
			RemotingConfiguration.Configure("ConsoleClient.exe.config");

			System.Console.WriteLine("Calling server 1...");
			IRemoteFactory factory = (IRemoteFactory)RemotingHelper.CreateProxy(typeof(IRemoteFactory));
			Person p = factory.GetPerson();
			System.Console.WriteLine(">> Person retrieved: {0} {1}, {2}", p.Firstname, p.Lastname, p.Age.ToString());
			System.Console.WriteLine();

         Console.ReadLine();
		}
	}
}
