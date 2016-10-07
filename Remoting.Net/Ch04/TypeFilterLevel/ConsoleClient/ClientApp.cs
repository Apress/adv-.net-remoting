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

			System.Console.WriteLine("Creating proxy...");
			IRemoteFactory factory = (IRemoteFactory)RemotingHelper.CreateProxy(typeof(IRemoteFactory));

			System.Console.WriteLine("Calling UploadPerson()...");
			factory.UploadPerson(new Person("Test", "Upload", 24));
			System.Console.WriteLine(">> Upload called successfully!");

			System.Console.ReadLine();
		}
	}
}
