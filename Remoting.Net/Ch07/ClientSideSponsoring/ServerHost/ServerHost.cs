using System;
using System.Runtime.Remoting;

namespace ServerHost
{
  class ServerStartup
  {
    public static void Main(String[] args) 
    {
      String cfg = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
      RemotingConfiguration.Configure(cfg);
      Console.WriteLine("Press <return> to exit");
      Console.ReadLine();
    }
  }
}
