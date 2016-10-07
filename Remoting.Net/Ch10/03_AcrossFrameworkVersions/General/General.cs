using System;
using System.Data;
using System.Runtime.Serialization;

namespace General
{
  public interface ICrossVersionTest
  {
    DataSet GetDataset();
    void StoreDataset(DataSet ds);

    TestDataset GetTestDataset();
    void StoreTestDataset(TestDataset ds);
  }


}

