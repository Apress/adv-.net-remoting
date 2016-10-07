using System;

namespace General
{
	public interface IPersonFactory 
	{
		Person GetPerson();
	}

	[Serializable]
	public class Person
	{
		private int _age;
		public string Firstname, Lastname;

		public Person(string firstname, string lastname, int age) 
		{
			this.Age = age;
			this.Firstname = firstname;
			this.Lastname = lastname;
		}

		public int Age 
		{
			get { return _age; }
			set 
			{
					if(value >= 0) 
						_age = value; 
					else
						throw new ArgumentException("Age must be zero or positive!"); 
			}
		}
	}
}
