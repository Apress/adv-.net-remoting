using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
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
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("impersonationLevel", "Identify");
                dict.Add("encryption", "EncryptAndSign");

                System.Console.WriteLine("Configuring channel...");
                TcpClientChannel clientChannel = new TcpClientChannel(dict, null);
                ChannelServices.RegisterChannel(clientChannel);

                System.Console.WriteLine("Configuring remote object...");
                IRemotedType TheObject = (IRemotedType) Activator.GetObject(
                    typeof(RemotedType.IRemotedType),
                    "tcp://localhost:9001/MyObject.rem");

                System.Console.WriteLine("Please enter data, 'exit' quits the program!");
                int c = 0;
                string input = string.Empty;
                do
                {
                    System.Console.Write("Enter message: ");
                    input = System.Console.ReadLine();
                    if (string.Compare(input, "exit", true) != 0)
                    {
                        System.Console.Write("Enter counter: ");
                        c = Int32.Parse(System.Console.ReadLine());

                        TheObject.DoCall(input, c);
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
