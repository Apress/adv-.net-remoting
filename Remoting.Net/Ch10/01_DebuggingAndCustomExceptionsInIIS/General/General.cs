using System;
using System.Runtime.Serialization;

namespace General
{
  public interface IRemoteExceptionTest
  {
    void TestException();
  }

  [Serializable]
  public class ConcurrencyException: ApplicationException
  {
    string _databaseTable;

    public ConcurrencyException(): base() 
    {
    }

    public ConcurrencyException(String msg, String databaseTable): base(msg) 
    {
      _databaseTable = databaseTable;
    }

    public ConcurrencyException(SerializationInfo info, StreamingContext context): base(info, context)
    {
      _databaseTable = info.GetString("table");
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      base.GetObjectData (info, context);
      info.AddValue("table", _databaseTable);
    }

    public String DatabaseTable
    {
      get { return _databaseTable; }
    }

  }

}

