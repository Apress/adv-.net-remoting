using System;
using System.Windows.Forms;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Activation;

using General;
using General.Client;

namespace WinClient
{
	public class WinApplication
	{
		private static IRemoteFactory _factory = null;

		public static IRemoteFactory ServerProxy 
		{
			get 
			{
				if(_factory == null) 
				{
					_factory = (IRemoteFactory)RemotingHelper.CreateProxy(typeof(IRemoteFactory));
				}

				return _factory;
			}
		}

		public static void Main(string[] args) 
		{
			// First of all configure remoting services
			RemotingConfiguration.Configure("WinClient.exe.config");

			// Create the windows form and start message looping
			Application.Run(new MainForm());
		}
	}
}
