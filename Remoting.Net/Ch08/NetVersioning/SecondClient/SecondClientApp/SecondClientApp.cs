using System;
using SimpleComponent;

namespace SecondClientApp
{
  class SecondClientApp
  {
    [STAThread]
    static void Main(string[] args)
    {
      Console.WriteLine("Second Client started!");

      SimpleClass cls = new SimpleClass();
      string result = cls.DoSomething("Called from 2nd client...");
      Console.WriteLine("Result: " + result);
    }
  }
}
