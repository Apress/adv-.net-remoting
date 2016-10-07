using System;
using System.Text;
using System.Collections;
using System.Messaging;

class Sender
{
  static void Main(string[] args)
  {
    Console.Write("Enter String to broadcast:");
    String str = Console.ReadLine();

    ArrayList clients = new ArrayList();
    clients.Add("localhost");
    clients.Add("ingox31");

    String formatName = BuildFormatName(clients);
    MessageQueue que = new MessageQueue(formatName);

    Message msg = new Message();
    msg.Formatter = new BinaryMessageFormatter();
    msg.Body = str;
    que.Send(msg);

    Console.ReadLine();
  }

  static string BuildFormatName(ArrayList clients)
  {
    if (clients.Count == 0) 
      throw new ArgumentException("List of clients empty.", "clients");

    StringBuilder bld = new StringBuilder();
    bld.Append("FormatName:");
    foreach (String cli in clients)
    {
      bld.Append("direct=os:");
      bld.Append(cli);
      bld.Append("\\private$\\NOTIFICATIONS");
      bld.Append(",");
    }
    bld.Remove(bld.Length-1,1);
    return bld.ToString();
  }
}
