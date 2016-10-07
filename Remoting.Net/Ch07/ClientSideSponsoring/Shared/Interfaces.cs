using System;

namespace Shared
{

  public interface IRemoteFactory
  {
    IRemoteObject CreateInstance();
  }

  public interface IRemoteObject
  {
    void DoSomething();
  }

}
