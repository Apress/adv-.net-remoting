using System;
using System.Runtime.Remoting;
using System.Threading;
using Server; // from generated_meta.dll

namespace Client
{
	class Client
	{
		static void Main(string[] args)
		{
			String filename = "client.exe.config";
			RemotingConfiguration.Configure(filename);

			DefaultLifeTimeSingleton def = new DefaultLifeTimeSingleton();
			LongerLivingSingleton lng = new LongerLivingSingleton();
			InfinitelyLivingSingleton inf = new InfinitelyLivingSingleton();

			Console.WriteLine("Calling DefaultLifeTimeSingleton");
			def.doSomething();
			Console.WriteLine("Sleeping 100 msecs");
			Thread.Sleep(100);
			Console.WriteLine("Calling DefaultLifeTimeSingleton (will be new)");
			def.doSomething(); // this will be a new instance


			Console.WriteLine("Calling LongerLivingSingleton");
			lng.doSomething();
			Console.WriteLine("Sleeping 100 msecs");
			Thread.Sleep(100);
			Console.WriteLine("Calling LongerLivingSingleton (will be same)");
			lng.doSomething(); // this will be the same instance
			Console.WriteLine("Sleeping 6 seconds");
			Thread.Sleep(6000);
			Console.WriteLine("Calling LongerLivingSingleton (will be new)");
			lng.doSomething(); // this will be a new same instance

			Console.WriteLine("Calling InfinitelyLivingSingleton"); 
			inf.doSomething();
			Console.WriteLine("Sleeping 100 msecs");
			Thread.Sleep(100);
			Console.WriteLine("Calling InfinitelyLivingSingleton (will be same)");
			inf.doSomething(); // this will be the same instance
			Console.WriteLine("Sleeping 6 seconds");
			Thread.Sleep(6000);
			Console.WriteLine("Calling InfinitelyLivingSingleton (will be same)");
			inf.doSomething(); // this will be a new same instance

			Console.ReadLine();
		}	
	}
}

