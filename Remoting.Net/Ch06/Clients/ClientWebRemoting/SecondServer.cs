using System;
using System.Runtime.Remoting;

using General;
using General.Client;

namespace ClientWebRemoting
{
	public class SecondServer : MarshalByRefObject, IRemoteSecond
	{
		private int _counter = 1;
		private IRemoteFactory _proxy;

		public SecondServer()
		{
			System.Diagnostics.Debug.WriteLine("Initializing server...");

			_proxy = (IRemoteFactory)RemotingHelper.CreateProxy(typeof(IRemoteFactory));

			System.Diagnostics.Debug.WriteLine("Server initialized!");
		}

		public int GetNewAge()
		{
			Person p = _proxy.GetPerson();
			int ret = p.Age + (_counter++);

			System.Diagnostics.Debug.WriteLine(">> Incoming request returns " + ret.ToString());

			return ret;
		}
	}
}
