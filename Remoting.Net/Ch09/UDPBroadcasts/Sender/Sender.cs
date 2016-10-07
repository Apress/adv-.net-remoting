using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

public class Sender
{
  public static void Main()
  {
    Console.Write("Enter String to broadcast:");
    String str = Console.ReadLine();
    byte[] data = Encoding.ASCII.GetBytes(str);

    Socket sck = new Socket(
      AddressFamily.InterNetwork, 
      SocketType.Dgram, 
      ProtocolType.Udp);
    
    sck.Connect(new IPEndPoint( IPAddress.Broadcast,10000));
    sck.Send(data);
    sck.Close();
  }
}
