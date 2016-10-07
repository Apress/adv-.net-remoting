using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Runtime.Remoting;

namespace WindowsService
{
	public class RemotingService : System.ServiceProcess.ServiceBase
	{
		private static EventLog evt = new EventLog("Application");
		public static String SVC_NAME = ".NET Remoting Sample Service";

		public RemotingService()
		{
			this.ServiceName = SVC_NAME;
		}

		static void Main(string[] args)
		{
			evt.Source = SVC_NAME;
			evt.WriteEntry("Remoting Service intializing");
			if (args.Length>0 && args[0].ToUpper() == "/NOSERVICE")
			{
				RemotingService svc = new RemotingService();
				svc.OnStart(args);
				Console.WriteLine("Service simulated. Press <enter> to exit.");
				Console.ReadLine();
				svc.OnStop();
			}
			else
			{
				System.ServiceProcess.ServiceBase.Run(new RemotingService());
			}
		}

		protected override void OnStart(string[] args)
		{
			evt.WriteEntry("Remoting Service started");
			String filename = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
			RemotingConfiguration.Configure(filename);			
		}
 
		protected override void OnStop()
		{
			evt.WriteEntry("Remoting Service stopped");
		}
	}
}
