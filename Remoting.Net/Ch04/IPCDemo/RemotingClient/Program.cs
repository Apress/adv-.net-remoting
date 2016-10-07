using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Remoting.Activation;
using System.Text;

using RemotedType;

namespace RemotingClient
{
  class Program
  {
    static void Main(string[] args)
    {
      try
      {
        System.Console.WriteLine("Configuring channel...");
        IpcClientChannel clientChannel = new IpcClientChannel();
        ChannelServices.RegisterChannel(clientChannel);

        System.Console.WriteLine("Configuring remote object...");
        IRemotedType TheObject = (IRemotedType)Activator.GetObject(
            typeof(RemotedType.IRemotedType),
            "ipc://MyIpcChannel/MyObject.rem");

        System.Console.WriteLine("Please enter data, 'exit' quits the program!");
        string input = string.Empty;
        do
        {
          System.Console.Write(">> Enter text: ");
          input = System.Console.ReadLine();
          if (string.Compare(input, "exit", true) != 0)
          {
            System.Console.Write(">> Enter number: ");
            int c = Int32.Parse(System.Console.ReadLine());
            TheObject.DoCall(input, 2);
          }
        } while (string.Compare(input, "exit", true) != 0);
      }
      catch (Exception ex)
      {
        System.Console.WriteLine("Exception while processing contents: " + ex.Message);
        System.Console.ReadLine();
      }
    }
  }
}
