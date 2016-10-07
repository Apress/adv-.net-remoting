using System;
using System.Security.Principal;
using System.Runtime.Remoting;

namespace Server
{
	public class PersonManager : MarshalByRefObject, General.IPersonFactory
	{
		public General.Person GetPerson()
		{
			try 
			{
				WindowsIdentity identity = WindowsIdentity.GetCurrent();

				System.Console.WriteLine("\nIncoming request...");
				System.Console.WriteLine("Current windows identity: " + identity.Name);
				System.Console.WriteLine("Current thread identity: " + System.Threading.Thread.CurrentPrincipal.Identity.Name);

				// Get the client's identity and impersonate the token
				identity = System.Threading.Thread.CurrentPrincipal.Identity as WindowsIdentity;
				WindowsImpersonationContext impCtx = identity.Impersonate();

				// Now output the current windows identity
				System.Console.WriteLine("Identity impersonated...");
				System.Console.WriteLine("Current identity: {0}", WindowsIdentity.GetCurrent().Name);

				// Revert the identity to itself and again output the current windows identity
				impCtx.Undo();
				System.Console.WriteLine("Identity reverted: {0}", WindowsIdentity.GetCurrent().Name);
			} 
			catch(Exception ex) 
			{
				System.Console.WriteLine("Exception occured: " + ex.Message);
			}

			System.Console.WriteLine("Returning a new person...");
			return new General.Person("Mini", "Coperground", 50);
		}
	}

	public class ServerApp
	{
		[STAThread]
		static void Main(string[] args)
		{
			string configFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;

			RemotingConfiguration.Configure(configFile);
			System.Console.WriteLine("Sever started, waiting for requests...");
			System.Console.WriteLine("Server's process user: " + WindowsIdentity.GetCurrent().Name);
			System.Console.ReadLine();
		}
	}
}
