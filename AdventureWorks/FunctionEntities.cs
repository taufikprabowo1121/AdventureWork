using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.Core.Objects;
using System.Globalization;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data.Entity.SqlServer;

namespace AdventureWorks
{
    class FunctionEntities
    {
        private AdventureWorks2014Entities jambore = new AdventureWorks2014Entities();
        public void CanonicalFunction()
        {
           
            var products = from p in jambore.Product
                           where EntityFunctions.DiffDays(p.SellEndDate, p.SellStartDate) < 365
                           select p;
            foreach(var k in products)
            {
                Console.WriteLine(k.ProductID);
            }
            Console.ReadKey();
        }
        public void CanonicalFunction2()
        {
            double? stdDev = EntityFunctions.StandardDeviation(from o in jambore.SalesOrderHeader select o.SubTotal);
            Console.WriteLine(stdDev);

            Console.ReadKey();
        }
        public void CharIndex()
        {
            var person = from c in jambore.Person
                         where SqlFunctions.CharIndex("Si", c.LastName) == 1
                         select c;

            foreach(var s in person)
            {
                Console.WriteLine(s.LastName);
            }
            Console.ReadKey();
        }
        public void CharIndex2()
        {
            decimal? checkSum = SqlFunctions.ChecksumAggregate(from o in jambore.SalesOrderHeader select o.SalesOrderID);
            Console.WriteLine(checkSum);
            Console.ReadKey();
        }
    }
}
