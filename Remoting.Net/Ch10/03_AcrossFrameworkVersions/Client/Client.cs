using System;
using System.Data;
using System.Runtime.Remoting;
using General;  // from General.DLL

namespace Client
{
	class Client
	{
		static void Main(string[] args)
		{
			String filename = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
			RemotingConfiguration.Configure(filename);

      try 
      {
        ICrossVersionTest tst = (ICrossVersionTest) RemotingHelper.CreateProxy(typeof(ICrossVersionTest));

        Console.WriteLine("Read generic");
        DataSet ds = tst.GetDataset();
        Console.WriteLine("Store generic");
        tst.StoreDataset(ds);
        Console.WriteLine("Read typed");
        TestDataset tds = tst.GetTestDataset();
        Console.WriteLine("Read untyped");
        tst.StoreTestDataset(tds);
      }
      catch (Exception ex)
      {
        Console.WriteLine("Test " + ex.ToString());
      }
      
      Console.WriteLine("Done.");
      Console.ReadLine();
		}	
	}
}

