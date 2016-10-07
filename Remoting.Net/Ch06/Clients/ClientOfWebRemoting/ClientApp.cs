using System;
using System.Runtime.Remoting;

using General;
using General.Client;

namespace ClientOfWebRemoting
{
	class ClientApp
	{
		[STAThread]
		static void Main(string[] args)
		{
			System.Console.WriteLine("Configuring client...");
			RemotingConfiguration.Configure("ClientOfWebRemoting.exe.config");

			System.Console.WriteLine("Calling server...");
			IRemoteSecond second = (IRemoteSecond)RemotingHelper.CreateProxy(typeof(IRemoteSecond));
			for(int i=0; i < 5; i++)
			{
				System.Console.WriteLine("Result: {0}", second.GetNewAge());
			}

			System.Console.ReadLine();
		}
	}
}
