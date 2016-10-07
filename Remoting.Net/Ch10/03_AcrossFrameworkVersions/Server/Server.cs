using System;
using System.Data;
using General;

namespace Server
{
  public class CrossVersionTest: MarshalByRefObject, ICrossVersionTest
  {
    public System.Data.DataSet GetDataset()
    {
      return Helpers.GetDS();
    }

    public void StoreDataset(DataSet ds)
    {
    }

    public TestDataset GetTestDataset()
    {
      return Helpers.GetTestDS();
    }


    public void StoreTestDataset(TestDataset ds)
    {

    }

  }

  public class Helpers
  {
    public static DataSet GetDS()
    {
      DataSet ds = new DataSet();
      DataTable dt = new DataTable("Customers");
      ds.Tables.Add(dt);

      dt.Columns.Add("ID", typeof(long));
      dt.Columns.Add("Name", typeof(string));
      dt.Columns.Add("Date", typeof(DateTime));


      for (int i=0;i<100;i++)
      {
        dt.Rows.Add(new object[]{ i, "Testname " + i, DateTime.Now.Subtract(TimeSpan.FromDays(i*350))});
      }

      return ds;
    }

    public static TestDataset GetTestDS()
    {
      TestDataset ds = new TestDataset();


      for (int i=0;i<100;i++)
      {
        TestDataset.CustomersRow rw = ds.Customers.AddCustomersRow(
          "KEY" + i,
          "Company " + i,
          "",
          "",
          "",
          "",
          "",
          "",
          "",
          "",
          "");

        for (int j=0;j<5;j++)
        {
          ds.Orders.AddOrdersRow(
            rw,
            7,
            DateTime.Now,
            DateTime.Now.AddDays(3),
            DateTime.Now.AddDays(1),
            2,
            1,
            "",
            "",
            "",
            "",
            "",
            "");
        }
      }

      return ds;
    }
  }
}
