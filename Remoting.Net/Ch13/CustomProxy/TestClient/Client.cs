using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using Service; // from service.dll

namespace Client
{

	class Client
	{
		static void Main(string[] args)
		{
			ChannelServices.RegisterChannel(new HttpChannel());
			
			CustomProxy prx = new CustomProxy(typeof(Service.SomeSAO),
				"http://localhost:1234/SomeSAO.soap");
			
			SomeSAO obj = (SomeSAO) prx.GetTransparentProxy();

			String res = obj.doSomething();
		
			Console.WriteLine("Got result: {0}",res);
			Console.ReadLine();
		}	
	}
}

