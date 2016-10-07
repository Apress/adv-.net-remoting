using System;
using System.Xml;
using System.Collections;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.CompilerServices;

[assembly: AssemblyTitle("Shared Assembly")]
[assembly: AssemblyVersion("2.0.0.2")]
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
  public class Person : ISerializable
  {
    public int Age;
    public string Firstname;
    public string Lastname;
    public DateTime Birthdate;
    public string Comments;

    public Person(string first, string last, int age) 
    {
      this.Age = age;
      this.Firstname = first;
      this.Lastname = last;
    }

    public Person(SerializationInfo info, StreamingContext context) 
    {
      Age = info.GetInt32("Age");
      Firstname = info.GetString("Firstname");
      Lastname = info.GetString("Lastname");

      try 
      {
        Birthdate = info.GetDateTime("Birthdate");
        Comments = info.GetString("Comments");
      } 
      catch { }
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      info.AddValue("Age", Age);
      info.AddValue("Firstname", Firstname);
      info.AddValue("Lastname", Lastname);
      info.AddValue("Birthdate", Birthdate);
      info.AddValue("Comments", Comments);
    }
  }
}
