using System;
using System.Collections;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using General;

namespace Server
{

  class MyRemoteFactory: MarshalByRefObject,IRemoteFactory 
  {
    public IRemoteObject GetNewInstance() 
    {
      return new MyRemoteObject();
    }
  }

  class MyRemoteObject: MarshalByRefObject, IRemoteObject
  {
    // ... removed
  }



	class ServerStartup
	{
		static void Main(string[] args)
		{
			Console.WriteLine ("ServerStartup.Main(): Server started");


    Hashtable chnlProps = new Hashtable();
    chnlProps["port"] = 1234;
    chnlProps["machineName"] = "yourserver.domain.com";
    HttpChannel chnl = new HttpChannel(chnlProps,null,null);

    ChannelServices.RegisterChannel(chnl);

			RemotingConfiguration.RegisterWellKnownServiceType(
				typeof(MyRemoteFactory),
				"factory.soap",
				WellKnownObjectMode.Singleton);

			// the server will keep running until keypress.
			Console.ReadLine();
		}
	}
}
