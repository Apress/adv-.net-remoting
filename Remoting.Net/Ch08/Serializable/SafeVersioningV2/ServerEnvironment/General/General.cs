using System;
using System.Xml;
using System.Collections;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.CompilerServices;

[assembly: AssemblyTitle("Shared Assembly")]
[assembly: AssemblyVersion("2.0.0.40")]
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
    public DateTime Birthdate;    // new !!
    public string Comments;       // new !!
    private ArrayList Reserved=null;

    public Person(string first, string last, int age) 
    {
      this.Age = age;
      this.Firstname = first;
      this.Lastname = last;
    }

    public Person(SerializationInfo info, StreamingContext context) 
    {
      ArrayList values = (ArrayList)info.GetValue("personData", typeof(ArrayList));

      this.Age = (int)values[0];
      this.Firstname = (string)values[1];
      this.Lastname = (string)values[2];
      
      try 
      {
        if(values.Count >= 5) 
        {
          this.Birthdate = (DateTime)values[3];
          this.Comments = (string)values[4];
        }
      } 
      catch { }

      Console.WriteLine("[Person]: Deserialized person: {0} {1} {2}", Firstname, Lastname, Age);

      if(values.Count > 5) 
      {
        Console.WriteLine("[Person]: Found additional values...");

        Reserved = new ArrayList();
        for(int i=5; i < values.Count; i++)
          Reserved.Add(values[i]);

        Console.WriteLine("[Person]: Additional values saved!");
      }
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      ArrayList data = new ArrayList();

      Console.WriteLine("[Person]: serializing data...");
      data.Add(Age);
      data.Add(Firstname);
      data.Add(Lastname);
      data.Add(Birthdate);
      data.Add(Comments);

      if(Reserved != null) 
      {
        Console.WriteLine("[Person]: storing unknown data...");

        foreach(object obj in Reserved)
          data.Add(obj);
      }

      info.AddValue("personData", data, typeof(ArrayList));
    }
  }
}
