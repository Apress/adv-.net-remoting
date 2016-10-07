using System;
using System.Security.Principal;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Activation;

using General;

namespace Client
{
	class ClientApp
	{
		[STAThread]
		static void Main(string[] args)
		{
			// Start-up information
			System.Console.WriteLine("Starting client...");
			System.Console.WriteLine("Current identity: " + WindowsIdentity.GetCurrent().Name);

			// Configure the remoting runtime
			RemotingConfiguration.Configure(
				AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

			// Instanciate the type
			try 
			{
				IPersonFactory personCreator = RemotingHelper.CreateProxy(typeof(IPersonFactory)) as IPersonFactory;
				Person p = personCreator.GetPerson();
				System.Console.WriteLine("Got the person: {0} {1} {2}", p.Firstname, p.Lastname, p.Age);
				System.Console.ReadLine();
			} 
			catch(Exception ex) 
			{
				System.Console.WriteLine("Exception occured: " + ex.Message);
			}

			System.Console.WriteLine("Press key to stop...");
			System.Console.Read();
		}
	}
}
