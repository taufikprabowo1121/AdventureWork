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
namespace AdventureWorks
{
    class Program
    {
        static void Main(string[] args)
        {
            Where kamana = new Where();
            Expression wow = new Expression();
            FunctionEntities Data = new FunctionEntities();
            CompiledQuery yata = new CompiledQuery();
            yata.QueryExecution3();
        }
    }
}
