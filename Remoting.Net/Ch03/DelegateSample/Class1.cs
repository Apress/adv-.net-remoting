using System;

namespace DelegateSample
{
	/// <summary>
	/// Zusammenfassungsbeschreibung für Class1.
	/// </summary>
	class SomethingClass
	{
		delegate String doSthDelegate(int myValue);

		public static String doSomething(int myValue) 
		{
			return "HEY:" + myValue.ToString();
		}

		static void Main(string[] args)
		{
			doSthDelegate del = new doSthDelegate(doSomething);
			IAsyncResult ar = del.BeginInvoke(42,null,null);
			
			// ... do something different here

			String res = del.EndInvoke(ar);

			Console.WriteLine("Got result: '{0}'",res);

			//wait for return to close
			Console.ReadLine();
		}
	}
}
