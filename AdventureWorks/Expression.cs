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
    class Expression
    {
        private AdventureWorks2014Entities jambore = new AdventureWorks2014Entities();
        public void Expres()
        {
           
            var salesInfo = from s in jambore.SalesOrderHeader
                                        where s.TotalDue >= 200
                                        select s.SalesOrderID;
            Console.WriteLine("Sales order info :");
            foreach(var orderNumber in salesInfo)
            {
                Console.WriteLine("Order Number :" + orderNumber);
            }
            Console.ReadKey();
        }
        public void constant()
        {
           
            var salesInfo = from s in jambore.SalesOrderHeader
                            where s.TotalDue >= 203
                            select s.SalesOrderNumber;

            Console.WriteLine("Sales Order Number");
            foreach(var orderNum in salesInfo.Take(5))
            {
                Console.WriteLine(orderNum);
            }
            Console.ReadKey();
        }
        public void Comparisson()
        {
            var salesInfo = from s in jambore.SalesOrderHeader
                            where s.SalesOrderNumber == "SO43663"
                            select s;

            Console.WriteLine("Sales info-");
            foreach (var sale in salesInfo)
            {
                Console.WriteLine("Sales ID  :" + sale.SalesOrderID);
                Console.WriteLine("Ship date :{0:d}",sale.ShipDate);
            }
            Console.ReadKey();
        }
        public void Comparisson2()
        {
            var salesInfo = jambore.SalesOrderHeader.Where(s => s.SalesOrderNumber == "SO43663");
            Console.WriteLine("Sales Info -");
            foreach(var sale in salesInfo)
            {
                Console.WriteLine("Sales ID :" + sale.SalesOrderID);
                Console.WriteLine("Ship Date:{0:d}", sale.ShipDate);
            }
            Console.ReadKey();
        }
        public void Comparisson3()
        {
            DateTime dt = new DateTime(2001, 7, 8);
            var salesInfo = jambore.SalesOrderHeader.Where(s => s.ShipDate == dt);
            Console.WriteLine("Orders shipped on august 7, 2001 :");
            foreach(var sale in salesInfo)
            {
                Console.WriteLine("Sales ID :" + sale.SalesOrderID);
                Console.WriteLine("Total Due :" + sale.TotalDue);
            }
            Console.ReadKey();
        }
        public void Comparisson4()
        {
            var salesInfo = from s in jambore.SalesOrderHeader
                            where s.SalesOrderID == 43663
                            select s;
            foreach (var sale in salesInfo)
            {
                Console.WriteLine("Sales order number : " + sale.SalesOrderNumber);
                Console.WriteLine("Total Due : " + sale.TotalDue);
                Console.WriteLine();
            }
            Console.ReadKey();
        }
        public void NullComparison()
        {
            var query = from order in jambore.SalesOrderHeader
                        join detail in jambore.SalesOrderDetail
                        on order.SalesOrderID equals detail.SalesOrderID
                        where order.ShipDate == null
                        select order.SalesOrderID;
            if (query.Any())
            {
                foreach (var OrderID in query)
                {
                    Console.WriteLine("OrderID : {0}", OrderID);
                }
            }
            else
            {
                Console.WriteLine("Data Tidak Ada");
            }
            Console.ReadKey();
        }
        public void Initialization()
        {
            var salesInfo = from s in jambore.SalesOrderHeader where s.TotalDue >= 200 select new { s.SalesOrderNumber, s.TotalDue };
            Console.WriteLine("Sales order Number :");
            foreach(var sale in salesInfo.Take(5))
            {
                Console.WriteLine("Order Number : " + sale.SalesOrderNumber);
                Console.WriteLine("Total Due : " + sale.TotalDue);
                Console.WriteLine("");
            }
            Console.ReadKey();
        }
        public void Initialization2()
        {
            var salesInfo = jambore.SalesOrderHeader.Where(s => s.TotalDue >= 200).Select(s => new { s.SalesOrderNumber, s.TotalDue});
            Console.WriteLine("Sales order numbers :");
            foreach(var sale in salesInfo)
            {
                Console.WriteLine("Order Number :" + sale.SalesOrderNumber);
                Console.WriteLine("Total Due : " + sale.TotalDue);
                Console.WriteLine("");
            }
            Console.ReadKey();
        }
        public void Lol3()
        {
            var izin = from s in jambore.SalesOrderHeader
                       where s.TotalDue >= 200
                       select new MyOrder
                       {
                           SalesOrderNumber = s.SalesOrderNumber,
                           ShipDate = s.ShipDate
                       };
            Console.WriteLine("Sales order info :");
            foreach(MyOrder order in izin)
            {
                Console.WriteLine("Order Number : " + order.SalesOrderNumber);
                Console.WriteLine("Ship Date : "+ order.ShipDate);
                Console.WriteLine("");
            }
            Console.ReadKey();
        }
    }
}
