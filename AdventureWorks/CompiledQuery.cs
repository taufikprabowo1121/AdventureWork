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
    class CompiledQuery
    {
        private AdventureWorks2014Entities db = new AdventureWorks2014Entities();
        public void QueryExecution()
        {
            IQueryable<Product> productsQuery = from p in db.Product select p;
            IQueryable<Product> largeProducts = productsQuery.Where(p => p.Size == "L");
            Console.WriteLine("Products of size 'L' :");
            foreach(var product in largeProducts)
            {
                Console.WriteLine(product.Name);
            }
            Console.ReadKey();
        }
        public void QueryExecution2()
        {
            Product[] prodArray = (from product in db.Product orderby product.ListPrice descending select product).ToArray();
            Console.WriteLine("Every price from highest to lowest :");
            foreach(Product product in prodArray)
            {
                Console.WriteLine(product.ListPrice);
            }
            Console.ReadKey();
        }
        public void QueryExecution3()
        {
            int orderID = 51987;
            IQueryable<SalesOrderHeader> salesInfo = from s in db.SalesOrderHeader where s.SalesOrderID == orderID select s;
            foreach(SalesOrderHeader sale in salesInfo)
            {
                Console.WriteLine("OrderID: {0}, Total due: {1}", sale.SalesOrderID, sale.TotalDue);
            }
            Console.ReadKey();
        }
    }
}
