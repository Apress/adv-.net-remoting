using System;

namespace General
{
  public interface IMyRemoteObject
  {
    void SetValue(int newval);
    int GetValue();
  }
}
