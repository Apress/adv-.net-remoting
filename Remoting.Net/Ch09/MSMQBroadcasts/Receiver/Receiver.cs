using System;
using System.Messaging;

class Receiver
{
  static void Main(string[] args)
  {
    String queuename = @".\private$\NOTIFICATIONS";

    if (!MessageQueue.Exists(queuename))
    {
      MessageQueue.Create(queuename);
    }

    MessageQueue que = new MessageQueue(queuename);
    que.Formatter = new BinaryMessageFormatter();

    while (true)
    {
      using (Message msg = que.Receive())
      {
        String str = (String) msg.Body;
        Console.WriteLine("Received: {0}", str);
      }
    }
  }
}

