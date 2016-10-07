using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using System;
using General;

namespace Client
{
	class Client
	{
		static void Main(string[] args)
		{
			HttpChannel channel = new HttpChannel();
			ChannelServices.RegisterChannel(channel);

  		        Console.WriteLine("Acqu. Rem. Instance");
                        IRemoteFactory fact = (IRemoteFactory) Activator.GetObject(
				    typeof(IRemoteFactory),
				    "http://localhost:1234/factory.soap");
			
			Console.WriteLine("Client.Main(): Acquiring object from factory");
			IRemoteObject obj1 = fact.getNewInstance();

			Console.WriteLine("Client.Main(): Sleeping one second");
			System.Threading.Thread.Sleep(1000);

			Console.WriteLine("Client.Main(): Setting value");
			try 
			{
			  obj1.setValue(42);
			} 
			catch (Exception e) 
			{
				Console.WriteLine("Client.Main(). EXCEPTION \n{0}",e.Message);
			}

			Console.ReadLine();
		}	
	}
}
