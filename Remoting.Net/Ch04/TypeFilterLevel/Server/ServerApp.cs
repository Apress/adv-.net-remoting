using System;
using System.Collections;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;

using General;

namespace Server
{
	public class ServerImpl : MarshalByRefObject, IRemoteFactory 
	{
		public void UploadPerson(Person p) 
		{
			System.Console.WriteLine(">> UploadPerson()");
			System.Console.WriteLine(">> Person {0} {1} {2}", p.Firstname, p.Lastname, p.Age);
		}
	}

	class ServerApp
	{
		[STAThread]
		static void Main(string[] args)
		{
			System.Console.WriteLine("Starting server...");
			
			// Configure the formatters for the channel
			BinaryServerFormatterSinkProvider formatterBin = new BinaryServerFormatterSinkProvider();
			formatterBin.TypeFilterLevel = TypeFilterLevel.Full;

			SoapServerFormatterSinkProvider formatterSoap = new SoapServerFormatterSinkProvider();
			formatterSoap.TypeFilterLevel = TypeFilterLevel.Low;

			formatterBin.Next = formatterSoap;

			// Register the channels
			IDictionary dict = new Hashtable();
			dict.Add("port", "1234");

			TcpChannel channel = new TcpChannel(dict, null, formatterBin);
			ChannelServices.RegisterChannel(channel);

			// Register the wellknown service
			RemotingConfiguration.RegisterWellKnownServiceType(
													typeof(ServerImpl), 
													"MyServer.rem", 
													WellKnownObjectMode.Singleton);

			// Server configured properly
			System.Console.WriteLine("Server configured, waiting for requests!");
			System.Console.ReadLine();
		}
	}
}
