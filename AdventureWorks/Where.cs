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
using System.Data.Sql;
using System.Globalization;
using System.Data.Entity.Core.Objects;

namespace AdventureWorks
{
    class Where
    {
        private AdventureWorks2014Entities jambore = new AdventureWorks2014Entities();
        public void Dimana()
        {

            var onlinerOrders = (from sales in jambore.SalesOrderHeader

                                 select new { sales.SalesOrderID, sales.OrderDate, sales.SalesOrderNumber }).ToList();


            /*
                var onlinerOrders = jambore.SalesOrderHeader.Where(order => order.OnlineOrderFlag == true)
                   .Select(s => new { s.SalesOrderID, s.OrderDate, s.SalesOrderNumber });
                   */

            if (onlinerOrders.Any())
            {
                foreach (var s in onlinerOrders)
                {

                    Console.WriteLine("Order ID : {0} Order date : {1:d} Order Number : {2}", s.SalesOrderID, s.OrderDate, s.SalesOrderNumber);

                }
            }
            else
            {
                Console.WriteLine("Data Kosong");
            }


            Console.ReadKey();

        }
        public void Dimana2()
        {

            var query = (from sales in jambore.SalesOrderDetail
                         where sales.OrderQty > 2 && sales.OrderQty < 6
                         select new { sales.SalesOrderID, sales.OrderQty }).ToList();
            /*
            var query = jambore.SalesOrderDetail.Where(order => order.OrderQty > 2 && order.OrderQty < 6).Select(s => new { s.SalesOrderID, s.OrderQty });
            */
            foreach (var order in query)
            {
                Console.WriteLine("Order ID: {0} Order quantity: {1}", order.SalesOrderID, order.OrderQty);
            }

            Console.ReadKey();
        }
        public void Warna()
        {
            var query = (from product in jambore.Product
                         where product.Color == "Red"
                         select new { product.Name, product.ProductNumber, product.ListPrice });
            /*
            var query = jambore.Product.Where(Product => Product.Color == "Red").Select(p => new { p.Name, p.ProductNumber, p.ListPrice });
            */
            foreach (var product in query)
            {
                Console.WriteLine("Name : {0}", product.Name);
                Console.WriteLine("Product number : {0}", product.ProductNumber);
                Console.WriteLine("List Price: ${0}", product.ListPrice);
                Console.WriteLine("");
            }
            Console.ReadKey();
        }
        public void Date()
        {
            IQueryable<SalesOrderHeader> query = jambore.SalesOrderHeader.Where(order => order.OrderDate >= new DateTime(2003, 12, 1));
            Console.WriteLine("Order that wew made after December 1, 2003:");
            foreach (SalesOrderHeader order in query)
            {
                Console.WriteLine("OrderID {0} Order date: {1:d}", order.SalesOrderID, order.OrderDate);
                foreach (SalesOrderDetail orderDetail in order.SalesOrderDetail)
                {
                    Console.WriteLine("Product ID: {0} Unit Price {1}", orderDetail.ProductID, orderDetail.UnitPrice);
                }
            }
            Console.ReadKey();
        }
        public void contains1()
        {
            int?[] productModelIds = { 10, 26, 118, 200 };
            var products = jambore.Product.Where(p => productModelIds.Contains(p.ProductModelID));
            foreach (var product in products)
            {
                Console.WriteLine("{0}:{1}", product.ProductModelID, product.ProductID);
            }
            Console.ReadKey();
        }
        public void contains2()
        {
            var products = jambore.Product.
         Where(p => (new int?[] { 19, 26, 18 }).Contains(p.ProductModelID) ||
                    (new string[] { "L", "XL" }).Contains(p.Size));

            foreach (var product in products)
            {
                Console.WriteLine("{0}: {1}, {2}", product.ProductID,
                                                   product.ProductModelID,
                                                   product.Size);
            }
            Console.ReadKey();
        }
        public void ThenBy()
        {
            IQueryable<Product> query = jambore.Product.OrderBy(product => product.ListPrice).ThenByDescending(product => product.Name);

            foreach (Product product in query)
            {
                Console.WriteLine("Product Id: {0} Product Name : {1} List Price {2}", product.ProductID, product.Name, product.ListPrice);
            }
            Console.ReadKey();
        }
        public void Average()
        {
            //438,6662
            /*
            Decimal averageListPrice = jambore.Product.Average(product => product.ListPrice);
            */

            Decimal averageListPrice = (from produk in jambore.Product select produk).Average(produk => produk.ListPrice);

            Console.WriteLine("The average list price of all the products is ${0}", averageListPrice);
            Console.ReadKey();
        }
        public void AverageStyle()
        {
            var query = from product in jambore.Product
                        group product by product.Style into g
                        select new
                        {
                            Style = g.Key,
                            avetageListPrice = g.Average(product => product.ListPrice)
                        };
            foreach (var product in query)
            {
                Console.WriteLine("Product Style : {0} Average list price : {1}", product.Style, product.avetageListPrice);
            }
            Console.ReadKey();
        }
        public void AverageTotalDue()
        {
            // model contack not found

        }
        public void Count()
        {
            var query = from produk in jambore.Product
                        group produk by produk.Color into g
                        select new { Wow = g.Key, ProductCount = g.Count() };
            foreach (var product in query)
            {
                Console.WriteLine("Color = {0} \t ProductCount = {1}", product.Wow, product.ProductCount);
            }
            Console.ReadKey();
        }
        public void Max()
        {
            //187487,8250
            //var maxTotalDue = jambore.SalesOrderHeader.Max(w => w.TotalDue);
            var maxTotalDue = (from sales in jambore.SalesOrderHeader select sales).ToList().Max(w => w.TotalDue);
            Console.WriteLine("The Maxsimum TotalDue is {0}", maxTotalDue);

            Console.ReadKey();
        }
        public void Max2()
        {
            var query = from order in jambore.SalesOrderHeader
                        group order by order.SalesOrderID
                        into g
                        select new
                        {
                            Category = g.Key,
                            Maxtotaldue = g.Max(s => s.TotalDue)
                        };
            foreach (var order in query)
            {
                Console.WriteLine("Maxmimun Total Due = {0} : {1}", order.Category, order.Maxtotaldue);
            }
            Console.ReadKey();
        }
        public void Max3()
        {
            var query = from order in jambore.SalesOrderHeader
                        group order by order.SalesOrderID
                        into g
                        let maxTotalDue = g.Max(order => order.TotalDue)
                        select new
                        {
                            Category = g.Key,
                            CheapestProducts = g.Where(order => order.TotalDue == maxTotalDue)
                        };

        }
        public void Min()
        {
            var query = from order in jambore.SalesOrderHeader
                        group order by order.SalesOrderID into g
                        select new
                        {
                            Category = g.Key,
                            Smallestotaldue = g.Min(order => order.TotalDue)
                        };
            foreach (var order in query)
            {
                Console.WriteLine("Sales Order ID = {0} \t Minimum TotalDoe = {1}", order.Category, order.Smallestotaldue);
            }
        }
        public void Min2()
        {
            var query = from order in jambore.SalesOrderHeader
                        group order by order.SalesOrderID into g
                        let minTotalDue = g.Min(order => order.TotalDue)
                        select new
                        {
                            Category = g.Key,
                            smallestotaldue = g.Where(order => order.TotalDue == minTotalDue)
                        };
            foreach (var ordergrup in query)
            {
                Console.WriteLine("Sales ID : {0}", ordergrup.Category);
                foreach (var order in ordergrup.smallestotaldue)
                {
                    Console.WriteLine("Minimum TotalDue {0} for SalesOrderID {1}", order.TotalDue, order.SalesOrderID);
                }
            }
            Console.ReadKey();
        }
        public void Sum()
        {
            var query = from order in jambore.SalesOrderHeader
                        group order by order.SalesOrderID into g
                        select new
                        {
                            Category = g.Key,
                            TotalDue = g.Sum(order => order.TotalDue)
                        };
            foreach (var order in query)
            {
                Console.WriteLine("ContactID = {0} \t TotalDue sum = {1}", order.Category, order.TotalDue);
            }
            Console.ReadKey();
        }
        public void Skip()
        {
            // Linq to Entities only supports skip on ordered collection
            /*
            IOrderedQueryable<Product> products = jambore.Product.OrderBy(p => p.ListPrice);
            IQueryable<Product> allButFirst3Products = products.Skip(3);
            */
            
            var allButFirst3Products = (from produk in jambore.Product
                                        orderby produk.ListPrice
                                        select produk).ToList().Skip(3);
              
            Console.WriteLine("All but first 3 Products :");
            foreach (var product in allButFirst3Products)
            {
                Console.WriteLine("Name : {0} \t ID : {1}", product.Name, product.ProductID);
            }
            Console.ReadKey();
        }
        public void Skip2()
        {
            var query = (from address in jambore.Address
                         from order in jambore.SalesOrderHeader
                         where address.AddressID == order.Address.AddressID
                         && address.City == "Seattle"
                         orderby order.SalesOrderID
                         select new
                         {
                             City = address.City,
                             OrderID = order.SalesOrderID,
                             OrderDate = order.OrderDate
                         }).Skip(2);

            Console.WriteLine("All but first 2 orders in Seattle:");
            foreach(var order in query)
            {
                Console.WriteLine("City:{0} Order ID : {1} Total Due: {2}", order.City,order.OrderID,order.OrderDate);
            }
            Console.ReadKey();
        }
        public void Take()
        {
            var query = (from adress in jambore.Address
                         from order in jambore.SalesOrderHeader
                         where adress.AddressID == order.Address.AddressID
                         && adress.City == order.Address.City
                         select new
                         {
                             City = adress.City,
                             OrderID = order.SalesOrderID,
                             OrderDate = order.OrderDate
                         }).Take(3);
            Console.WriteLine("First 3 orders in Seattle:");
            foreach (var order in query)
            {
                Console.WriteLine("City: {0} Order ID: {1} Total Due: {2:d}",
                    order.City, order.OrderID, order.OrderDate);
            }
            Console.ReadKey();
        }
        public void GroupJoin()
        {
            var query = from order in jambore.SalesOrderHeader
                        join detail in jambore.SalesOrderDetail
                        on order.SalesOrderID
                        equals detail.SalesOrderID into orderGroup
                        select new
                        {
                            CustomerID = order.SalesOrderID,
                            OrderCount = orderGroup.Count()
                        };
            foreach(var order in query)
            {
                Console.WriteLine("CustomerID: {0} Orders Count: {1}", order.CustomerID, order.OrderCount);
            }
            Console.ReadKey();
        }
        public void GroupJoin2()
        {
            var query =(
                from order in jambore.SalesOrderHeader
                join detail in jambore.SalesOrderDetail
                on order.SalesOrderID equals detail.SalesOrderID
                where order.OnlineOrderFlag == true && order.OrderDate.Month == 8
                select new
                {
                    SalesOrderID = order.SalesOrderID,
                    DetailSalesID = detail.SalesOrderID,
                    SalesOrderDetailID = detail.SalesOrderDetailID,
                    OrderDate = order.OrderDate,
                    ProductID = detail.ProductID
                }).Take(5);
            foreach(var order in query)
            {
                Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", order.SalesOrderID, order.DetailSalesID, order.SalesOrderDetailID, order.OrderDate, order.ProductID);
            }
            Console.ReadKey();
        }
        public void First()
        { 
            var query = (from person in jambore.SalesOrderHeader
                         where person.Address.City == "Sooke"
                         select person).First();
                        Console.WriteLine("City :"+query.Address.City);
                        Console.WriteLine("Adress 1 :"+ query.Address.AddressLine1);
                        Console.WriteLine("Adress ID :"+ query.Address.AddressID);
            Console.ReadKey();
        }
        public void GroupBy()
        {
            var query = (from address in jambore.Address
                        group address by address.PostalCode into addressGroup
                        select new
                        {
                            PostalCode = addressGroup.Key,
                            AddressLine = addressGroup,
                        });
            foreach (var addressGroup in query)
            {
                Console.WriteLine("Postal Code : {0}", addressGroup.PostalCode);
                foreach(var address in addressGroup.AddressLine)
                {
                    Console.WriteLine("\t" + address.AddressLine1 + address.AddressLine2);
                }
                Console.ReadKey();
            }
        }
        public void GroupBy2()
        {
            var query = from order in jambore.SalesOrderHeader
                        group order by order.CustomerID into idGroup
                        select new { CustomerID = idGroup.Key, OrderCount = idGroup.Count(), Sales = idGroup };
            foreach (var orderGroup in query)
            {
                Console.WriteLine("Customer ID : {0}", orderGroup.CustomerID);
                Console.WriteLine("Sale ID : {0}", orderGroup.OrderCount);
                foreach(var sale in orderGroup.Sales)
                {
                    Console.WriteLine("SaleID {0}", sale.SalesOrderID);
                }
                Console.ReadKey();
            }
        }
        public void Expression()
        {
            var ordersQuery = from order in jambore.SalesOrderHeader
                              where order.Address.City == "Seattle"
                              select new
                              {
                                  StreetAddress = order.Address.AddressLine1,
                                  OrderNumber = order.SalesOrderNumber,
                                  TotalDue = order.TotalDue
                              };
            foreach(var orderInfo in ordersQuery)
            {
                Console.WriteLine("Street Address : {0}", orderInfo.StreetAddress);
                Console.WriteLine("Order Number : {0}", orderInfo.OrderNumber);
                Console.WriteLine("Total Due : {0}", orderInfo.TotalDue);
                Console.WriteLine("");
            }
            Console.ReadKey();
        }
        public void Expression2()
        {
            IQueryable<SalesOrderHeader> query = from order in jambore.SalesOrderHeader where order.OrderDate >= new DateTime(2003, 12, 1) select order;
            Console.WriteLine("Orders that were made after December 1, 2003 :");
            foreach(SalesOrderHeader order in query)
            {
                Console.WriteLine("OrderID {0} Order Date : {1:d} ", order.SalesOrderID, order.OrderDate);
                foreach(SalesOrderDetail orderDetail in order.SalesOrderDetail)
                {
                    Console.WriteLine("Product ID : {0} Unit Price {1}", orderDetail.ProductID, orderDetail.UnitPrice);
                }
            }
            Console.ReadKey();
        }
       

    }
}
