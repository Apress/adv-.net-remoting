using System;

namespace General
{
	public interface IRemoteFactory 
	{
		Person GetPerson();
	}

	public interface IRemoteSecond 
	{
		int GetNewAge();
	}

	[Serializable]
	public class Person 
	{
		public int Age;
		public string Firstname, Lastname;

		public Person(string first, string last, int age) 
		{
			this.Age = age;
			this.Firstname = first;
			this.Lastname = last;
		}
	}
}
