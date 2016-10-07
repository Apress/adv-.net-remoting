using System;
using System.Collections;
using System.Configuration.Install;
using System.ServiceProcess;
using System.ComponentModel;
using WindowsService;

[RunInstallerAttribute(true)]
public class MyProjectInstaller: Installer
{
	private ServiceInstaller serviceInstaller;
	private ServiceProcessInstaller processInstaller;

	public MyProjectInstaller()
	{
		processInstaller = new ServiceProcessInstaller();
		serviceInstaller = new ServiceInstaller();

		processInstaller.Account = ServiceAccount.LocalSystem;
		serviceInstaller.StartType = ServiceStartMode.Automatic;
		serviceInstaller.ServiceName = RemotingService.SVC_NAME;

		Installers.Add(serviceInstaller);
		Installers.Add(processInstaller);
	}
}