using System;
using System.Runtime.Serialization;

namespace General
{
	public interface IRemoteFactory 
	{
		void UploadPerson(Person p);
	}

	[Serializable]
	public class Person
	{
		public int Age;
		public string Firstname, Lastname;

		public Person(string first, string last, int age) 
		{
			this.Firstname = first;
			this.Lastname = last;
			this.Age = age;
		}
	}
}
