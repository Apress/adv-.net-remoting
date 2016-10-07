using System;
using General;

namespace Server
{
  class ExceptionTest: MarshalByRefObject, IRemoteExceptionTest
  {
    public void TestException()
    {
      throw new ConcurrencyException("testmessage", "customers");
    }
  }
}
