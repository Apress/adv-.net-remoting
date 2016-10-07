using System;
using SimpleComponent;

namespace FirstClientApp
{
  class ClientOne
  {
    [STAThread]
    static void Main(string[] args)
    {
      Console.WriteLine("Client 1");
      
      SimpleClass cls = new SimpleClass();
      Console.WriteLine(cls.DoSomething("Called from client 1"));
      Console.ReadLine();
    }
  }
}
