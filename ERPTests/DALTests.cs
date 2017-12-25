using System;
using System.Collections.Generic;
using System.Linq;

using ERP;
using ERP.Models;
using ERP.Utils;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ERPTests
{
    [TestClass]
    public class DALTests
    {
        [TestInitialize]
        public void Setup()
        {
            ERPEntities context = new ERPEntities();

            // delete all books in DB
            var books = context.Set<Book>();
            context.Books.RemoveRange(books);

            // delete all deals in DB
            var deals = context.Set<Deal>();
            context.Deals.RemoveRange(deals);

            context.SaveChanges();
        }


        [TestCleanup]
        public void Teardown()
        {
            ;
        }

        [TestMethod]
        public void TestSQLDataAccessor()
        {
            // create data accessor
            IDataAccessor dataAccessor = new SQLDataAccessor();

            // two fake deal objects
            Deal deal1 = new Deal
            {
                Id = 1,
                Cost = 100,
                Createtime = DateTime.Now,
                Updatetime = DateTime.Now,
                Price = 200,
                Profit = 100,
                Status = "Open"
            };

            Deal deal2 = new Deal
            {
                Id = 2,
                Cost = 50,
                Createtime = DateTime.Now,
                Updatetime = DateTime.Now,
                Price = 100,
                Profit = 50,
                Status = "Close",
            };

            // write two fake deals, assert 2 objects written
            Assert.AreEqual(2, dataAccessor.Post<Deal>(new List<Deal>() { deal1, deal2 }));

            // create two conditions
            Condition condition1 = new Condition
            {
                field = "Price",
                op = Operator.GreaterThan,
                valueType = ValType.Int,
                value = "10"
            };
            Condition condition2 = new Condition
            {
                field = "Status",
                op = Operator.EqualTo,
                valueType = ValType.String,
                value = "Close"
            };

            // get data for condidtion1
            IList<Deal> data1 = dataAccessor.Get<Deal>(new List<Condition>() { condition1 });

            // assert 2 objects got
            Assert.AreEqual(2, data1.Count);

            // assert 2 objects' ids are 1 or 2
            IList<int> ids = new List<int>() { 1, 2 };
            Assert.IsTrue(ids.Contains(data1.ElementAt(0).Id));
            Assert.IsTrue(ids.Contains(data1.ElementAt(1).Id));

            // get data for condition1 and condition2
            IList<Deal> data2 = dataAccessor.Get<Deal>(new List<Condition>() { condition1, condition2 });
            // assert the object id is 2
            Assert.AreEqual(2, data2.ElementAt(0).Id);
            
            // delete the 2 objects, assert 2 objects deleted
            Assert.AreEqual(2, dataAccessor.Delete<Deal>(data1));

            // get all objects, assert we get nothing
            Assert.AreEqual(0, dataAccessor.Get<Deal>(null).Count);
        }
    }
}
