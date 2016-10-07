using System;
using System.Text;
using System.Net.Sockets;
using System.Net;

public class Receiver
{
  public static void Main()
  {
    Socket sck = new Socket(
      AddressFamily.InterNetwork, 
      SocketType.Dgram, 
      ProtocolType.Udp);

    sck.Bind(new IPEndPoint( IPAddress.Any, 10000));

    byte[] buf = new byte[65000];

    while (true)
    {
      int size = sck.Receive(buf);
      String str = Encoding.ASCII.GetString(buf,0,size);
      Console.WriteLine("Received: {0}", str);
    }
  }
}
