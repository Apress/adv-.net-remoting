using System;

namespace General
{
  public interface IRemoteFactory
  {
    IRemoteObject GetNewInstance();
  }

  public interface IRemoteObject
  {
    // ... removed
  }

}
