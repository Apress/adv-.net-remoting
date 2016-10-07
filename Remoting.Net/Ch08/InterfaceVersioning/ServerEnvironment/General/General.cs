using System;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: AssemblyTitle("Shared Assembly")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyKeyFile(@"..\..\..\Server.snk")]

namespace General
{
  public interface IRemoteFactory 
  {
    int GetAge();
    Person GetPerson();
    void UploadPerson(Person p);
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
