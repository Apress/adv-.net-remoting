using System;

namespace FactoryDesignPattern
{

	class MyClass 
	{
	}

	class MyFactory
	{
		public MyClass getNewInstance() 
		{
			return new MyClass();
		}
	}

	class MyClient 
	{
		static void Main(string[] args)
		{
			// creation using "new"
			MyClass obj1 = new MyClass();

			// creating using a factory
			MyFactory fac = new MyFactory();
			MyClass obj2 = fac.getNewInstance();

		}
	}

}
