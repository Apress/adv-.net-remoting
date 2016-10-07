using System;
using System.Runtime.Remoting.Contexts;

namespace ContextBound
{
	public class TestClient
	{
		public static void Main(String[] args) {
			Organization org = new Organization();
			try 
			{
				Console.WriteLine("Will set the name");
				org.Name = "Happy Hackers";
				Console.WriteLine("Will donate");
				org.Donate(99);
			} 
			catch (Exception e) 
			{
				Console.WriteLine("Exception: {0}",e.Message);
			}

			Console.WriteLine("Finished, press <return> to quit.");
			Console.ReadLine();
		}
	}
}
