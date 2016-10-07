using System;
using System.Threading;


namespace Service 
{

	public class SomeSAO: MarshalByRefObject
	{
				
		public String doSomething() 
		{
			Console.WriteLine("SomeSAO.doSomething called");
//			Thread.Sleep(10000);
			Console.WriteLine("SomeSAO.doSomething returning");
			return ("SomeSAO.doSomething called");
		}
	}
}