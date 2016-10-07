using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Activation;

using System.Security;
using System.Security.Principal;

namespace RemotingServer
{
  public class MyRemoteObject : MarshalByRefObject, RemotedType.IRemotedType
  {
    public void DoCall(string message, int counter)
    {
      // Get some information about the caller's context
      IIdentity remoteIdentity = CallContext.GetData("__remotePrincipal") as IIdentity;
      if (remoteIdentity != null)
      {
        System.Console.WriteLine("Authenticated user:\n-){0}\n-){1}",
                                    remoteIdentity.Name,
                                    remoteIdentity.AuthenticationType.ToString());
      }
      else
      {
        System.Console.Write("!! Attention, not authenticated !!");
      }

      // Just do the stupid work
      for (int i = 0; i < counter; i++)
      {
        System.Console.WriteLine("You told me to say {0}: {1}!", counter.ToString(), message);
      }
    }
  }

  public class RemoteServerApp
  {
    static void Main(string[] args)
    {
      try
      {
        System.Console.WriteLine("Configuring server...");
        System.Runtime.Remoting.RemotingConfiguration.Configure("RemotingServer.exe.config");

        System.Console.WriteLine("Configured channels:");
        foreach (IChannel channel in ChannelServices.RegisteredChannels)
        {
          System.Console.WriteLine("Registered channel: " + channel.ChannelName);
          if (channel is IpcChannel)
          {
            if (((IpcChannel)channel).ChannelData != null)
            {
              ChannelDataStore dataStore = (ChannelDataStore)((IpcChannel)channel).ChannelData;
              foreach (string uri in dataStore.ChannelUris)
              {
                System.Console.WriteLine("-) Found URI: " + uri);
              }
            }
            else
            {
              System.Console.WriteLine("-) No channel data");
            }
          }
          else
          {
            System.Console.WriteLine("-) not a IpcChannel data store");
          }
        }

        System.Console.WriteLine("--- waiting for requests...");
        System.Console.ReadLine();
        System.Console.WriteLine("Finished!!");
      }
      catch (Exception ex)
      {
        System.Console.WriteLine("Error while configuring server: " + ex.Message);
        System.Console.ReadLine();
      }
    }
  }
}
