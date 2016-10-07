using System;
using System.Runtime.Remoting;
using System.Collections ;
using System.Threading;
using System.Runtime.Remoting.Channels;
using UrlAuthenticationSink;
using Service; // from service.dll

namespace Client
{

	class Client
	{
		delegate string GetPrincipal();

		static void Main(string[] args)
		{
			String filename = "client.exe.config";
			RemotingConfiguration.Configure(filename);
			

			UrlAuthenticator.AddAuthenticationEntry(
				"http://localhost",
				"dummyremotinguser",
				"12345");

			UrlAuthenticator.AddAuthenticationEntry(
				"http://www.somewhere.org",
				"MyUser",
				"12345");


			TestSAO obj = new TestSAO();
			String res = obj.GetPrincipalName();

		
			Console.WriteLine("Remote principal: {0}",res);
			Console.ReadLine();
		}	
	}
}

