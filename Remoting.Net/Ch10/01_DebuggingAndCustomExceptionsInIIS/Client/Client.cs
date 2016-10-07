using System;
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
            IRemoteExceptionTest tst = (IRemoteExceptionTest) RemotingHelper.CreateProxy(typeof(IRemoteExceptionTest));
            tst.TestException();
         }
         catch (Exception ex)
         {
            Console.WriteLine("Test " + ex.ToString());
         }
      
         Console.ReadLine();

      }	
   }
}

