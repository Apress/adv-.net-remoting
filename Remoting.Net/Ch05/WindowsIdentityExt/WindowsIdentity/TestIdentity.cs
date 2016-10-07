using System;
using System.Security.Principal;
using System.Security.Permissions;

namespace WindowsIdentitySample
{
	class TestApplication
	{
		[STAThread]
		static void Main(string[] args)
		{
			// Get the current windows identity and apply it to the managed thread
			WindowsIdentity identity = WindowsIdentity.GetCurrent();
			WindowsPrincipal principal = new WindowsPrincipal(identity);

			// Output the identity's name as well as authentication method
			System.Console.WriteLine("User: " + identity.Name);
			System.Console.WriteLine("Authenticated through: " + 
				identity.AuthenticationType);
			System.Console.WriteLine("Is Guest: " + identity.IsGuest);

			// Set the identity to the managed thread's CurrentPrincipal
			//System.Threading.Thread.CurrentPrincipal = principal;

			// First of all demonstrate imperative security
			System.Console.WriteLine("\nTesting imperative security...");
			if(principal.IsInRole(@"BUILTIN\Administrators")) 
			{
				System.Console.WriteLine(">> Administrative task performed <<");
			} 
			else 
			{
				System.Console.WriteLine("!! You are not an administrator !!");
			}

			// At last test declarative security
			try 
			{
				System.Console.WriteLine("\nTesting declarative security...");
				DeclarativeSecurity();
				System.Console.WriteLine("Test succeeded!\n");
			} 
			catch(System.Security.SecurityException ex) 
			{
				System.Console.WriteLine("Security exception occured: " + ex.Message);
			}
		}

		[PrincipalPermission(SecurityAction.Demand, Role=@"BUILTIN\Users")]
		static void DeclarativeSecurity() 
		{
			System.Console.WriteLine("Function called successfully!");
		}
	}
}