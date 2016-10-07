using System;
using System.Runtime.Remoting;
using Server; // from generated_meta.dll
using System.Threading;

namespace Client
{
	delegate String getPrioAsync();

	class Client
	{
		static void Main(string[] args)
		{
			String filename = "client.exe.config";
			RemotingConfiguration.Configure(filename);

			TestSAO obj = new TestSAO();
			test(obj);

			Thread.CurrentThread.Priority = ThreadPriority.Highest;
			test(obj);

			Thread.CurrentThread.Priority = ThreadPriority.Lowest;
			test(obj);

			Thread.CurrentThread.Priority = ThreadPriority.Normal;
			test(obj);

			
			Console.ReadLine();
		}	

		static void test(TestSAO obj) 
		{
			Console.WriteLine("----------------- START TEST CASE ---------------");
			Console.WriteLine("   Local Priority: {0}",Thread.CurrentThread.Priority.ToString());

			String priority1 = obj.getPriority();

			Console.WriteLine("   Remote priority: {0}",priority1.ToString());
			Console.WriteLine("----------------- END TEST CASE ---------------");
		}
	}
}

