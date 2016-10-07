using System;
using System.Security.Principal;

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
		}
	}
}