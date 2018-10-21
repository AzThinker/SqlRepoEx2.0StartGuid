using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DemoTools.BLL.DemoNorthwind;
using SqlRepoEx.Core;
using SqlRepoEx.Static;

namespace GettingStartedMySql
{
    public static class DapperTest
    {
        private static IDbConnection dbConnection = MySqlRepoFactory.DbConnection;
        public static void QueryOnly()
        {
            var repository = MySqlRepoFactory.Create<AzProducts>();
            var result = repository.Query().Top(10);
            Console.WriteLine(result.Sql());

            IEnumerable<AzProducts> azProducts = dbConnection.Query<AzProducts>(result.Sql());

            foreach (var item in azProducts)
            {
                Console.WriteLine($"{item.ProductID}\t{item.ProductName2}");
            }
        }

        public static void DoInnerJoin()
        {
            var repository = MySqlRepoFactory.Create<AzProducts>();
            var result = repository.Query()
                                   .InnerJoin<AzSuppliers>()
                                   .On<AzSuppliers>((l, r) => l.SupplierID == r.SupplierID, r => r.CompanyName)
                                   .Top(10);
            Console.WriteLine(result.Sql());
            Console.WriteLine();

            IEnumerable<AzProducts> azProducts = dbConnection.Query<AzProducts>(result.Sql());

            foreach (var item in azProducts)
            {
                Console.WriteLine($"{item.ProductID}\t{item.ProductName2}\t{item.Supplier}");
            }

        }

        public static void LeftOuterJoin()
        {
            var repository = MySqlRepoFactory.Create<AzProducts>();
            var result = repository.Query()
                                   .LeftOuterJoin<AzSuppliers>()
                                   .On<AzSuppliers>((l, r) => l.SupplierID == r.SupplierID, r => r.CompanyName)
                                   .Top(10);
            Console.WriteLine(result.Sql());
            Console.WriteLine();

            IEnumerable<AzProducts> azProducts = dbConnection.Query<AzProducts>(result.Sql());

            foreach (var item in azProducts)
            {
                Console.WriteLine($"{item.ProductID}\t{item.ProductName2}\t{item.Supplier}");
            }
        }

        public static void QueryWhere()
        {
            var repository = MySqlRepoFactory.Create<AzProducts>();
            var result = repository.Query()
                                    .Where(p => p.ProductName2.Contains("t") && p.ProductID < 100)
                                    .Top(10);
            Console.WriteLine(result.Sql());
            Console.WriteLine();

            IEnumerable<AzProducts> azProducts = dbConnection.Query<AzProducts>(result.Sql());

            foreach (var item in azProducts)
            {
                Console.WriteLine($"{item.ProductID}\t{item.ProductName2}");
            }
        }

        public static void QueryWhereIn(bool go = false)
        {
            var repository = MySqlRepoFactory.Create<AzProducts>();
            var result = repository.Query()
                                    .WhereIn(p => p.ProductName2, new string[] { "Konbu", "Chang", "Tunnbröd", "Geitost" });
            Console.WriteLine(result.Sql());
            Console.WriteLine();
            IEnumerable<AzProducts> azProducts = dbConnection.Query<AzProducts>(result.Sql());

            foreach (var item in azProducts)
            {
                Console.WriteLine($"{item.ProductID}\t{item.ProductName2}");
            }
        }

        public static void QueryAvg()
        {
            var repository = MySqlRepoFactory.Create<AzOrder_Details>();
            var result = repository.Query().Select(p => p.ProductID, p => p.ProductName)
                                   .Avg(c => c.Quantity, c => c.QuantityAvg)
                                   .Avg(c => c.UnitPrice)
                                   .GroupBy(c => c.ProductID)
                                   .InnerJoin<AzProducts>()
                                   .On<AzProducts>((r, l) => r.ProductID == l.ProductID, l => l.ProductName2)
                                   .GroupBy<AzProducts>(p => p.ProductName2)
                                   .Top(10);

            Console.WriteLine(result.Sql());
            Console.WriteLine();
            IEnumerable<AzOrder_Details> azOrder_Details = dbConnection.Query<AzOrder_Details>(result.Sql());

            foreach (var item in azOrder_Details)
            {
                Console.WriteLine($"{item.ProductID}\t{item.ProductName}\t{item.QuantityAvg}\t{item.UnitPrice}");
            }
        }
        public static void QueryUnion()
        {
            var repository = MySqlRepoFactory.Create<AzCustomers>();

            // 此语句不会参与数据查询，只是作为Union的包裹
            // 如果此语句本身也是数据查询，请增加到new List<UnionSql>中
            var result = repository.Query()
                                   .Select(c => c.CustomerID, c => c.CompanyName);


            var result01 = repository.Query()
                                    .Select(c => c.CustomerID, c => c.CompanyName)
                                    .Where(c => c.CustomerID == "ANATR");

            var result02 = repository.Query()
                                    .Select(c => c.CustomerID, c => c.CompanyName)
                                    .Where(c => c.CustomerID == "FRANK");


            var result03 = repository.Query()
                                    .Select(c => c.CustomerID, c => c.CompanyName)
                                    .Where(c => c.CustomerID == "TRADH");



            var resultAllSql = result.UnionSql(new List<UnionSql>  {
                UnionSql.New(  result01,UnionType.Union ),
                UnionSql.New(  result02,UnionType.Union ),
                UnionSql.New(  result03,UnionType.Union ), });

            Console.WriteLine(resultAllSql);
            Console.WriteLine();

            IEnumerable<AzCustomers> azCustomers = dbConnection.Query<AzCustomers>(resultAllSql);

            foreach (var item in azCustomers)
            {
                Console.WriteLine($"{item.CustomerID}\t{item.CompanyName}");
            }
        }

        public static void DoInsertEntityParam()
        {
            var repository = MySqlRepoFactory.Create<AzProducts>();
            AzProducts azProduct = new AzProducts { ProductName2 = "testvalue" };
            var resultinsert = repository
                                    .Insert();



            Console.WriteLine(resultinsert.ParamSql());
            Console.WriteLine();

            // 需返回自增字段，所以用Query
            IEnumerable<AzProducts> azProducts = dbConnection.Query<AzProducts>(resultinsert.ParamSql(), azProduct);

            foreach (var item in azProducts)
            {
                Console.WriteLine($"{item.ProductID}\t{item.ProductName2}");
            }

        }

        public static void DoInsertEntityParamBatch()
        {
            var repository = MySqlRepoFactory.Create<AzProducts>();
            List<AzProducts> azProductList = new List<AzProducts>{
              new AzProducts { ProductName2 = "testvalue1" ,CategoryID=1,UnitPrice=123},
              new AzProducts { ProductName2 = "testvalue2" ,CategoryID=1,UnitPrice=123},
              new AzProducts { ProductName2 = "testvalue3" ,CategoryID=1,UnitPrice=123},
              new AzProducts { ProductName2 = "testvalue4" ,CategoryID=1,UnitPrice=123 },
              new AzProducts { ProductName2 = "testvalue5" ,CategoryID=1,UnitPrice=123},
              new AzProducts { ProductName2 = "testvalue6" ,CategoryID=1,UnitPrice=123},
            };
            var resultinsert = repository
                                    .Insert().ParamWith(c => c.ProductName2, c => c.UnitPrice, c => c.CategoryID);



            Console.WriteLine(resultinsert.ParamSql());
            Console.WriteLine();

            // 需返回自增字段，所以用Query
            dbConnection.Execute(resultinsert.ParamSql(), azProductList);



        }

        public static void DoUpdateEntityParam()
        {
            var repository = MySqlRepoFactory.Create<AzProducts>();
            var resultUpdate = repository
                                    .Update()
                                    .ParamSet(p => p.ProductName2, p => p.CategoryID)
                                    .Where(p => p.ProductID == p.ProductID);

            Console.WriteLine(resultUpdate.ParamSql());
            Console.WriteLine();

            AzProducts products = new AzProducts() { ProductID = 84, ProductName2 = "testvalue100", CategoryID = 7 };

            int result = dbConnection.Execute(resultUpdate.ParamSql(), products);


            Console.WriteLine($"{result}");
        }


        public static void DoDeleteEntity(bool go = false)
        {
            var repository = MySqlRepoFactory.Create<AzProducts>();
            AzProducts azProducts = new AzProducts { ProductName2 = "testvalue", ProductID = 81 };
            var resultUpdate = repository.Delete().For(azProducts);
            Console.WriteLine(resultUpdate.Sql());
            Console.WriteLine();
            int result = dbConnection.Execute(resultUpdate.Sql());
            Console.WriteLine($"{result}");

        }


        public static void DoDeleteBatch()
        {
            var repository = MySqlRepoFactory.Create<AzProducts>();

            var resultUpdate = repository.Delete().Where(p => p.ProductID == p.ProductID);
            List<AzProducts> azProductList = new List<AzProducts>
            {
                new AzProducts{ProductID=88},
                new AzProducts{ProductID=89},
                new AzProducts{ProductID=90},
                new AzProducts{ProductID=91},

            };
            Console.WriteLine(resultUpdate.Sql());
            Console.WriteLine();
            int result = dbConnection.Execute(resultUpdate.Sql(), azProductList);
            Console.WriteLine($"{result}");
        }

        public static void DoDelete()
        {
            var repository = MySqlRepoFactory.Create<AzProducts>();

            var resultUpdate = repository.Delete().Where(p => p.ProductID == 90);

            Console.WriteLine(resultUpdate.Sql());
            Console.WriteLine();
            int result = dbConnection.Execute(resultUpdate.Sql());
            Console.WriteLine($"{result}");
        }


        public static void DoDeleteTransaction()
        {
            var repository = MySqlRepoFactory.Create<AzProducts>();

            var resultUpdate = repository.Delete().Where(p => p.ProductID == p.ProductID);
            List<AzProducts> azProductList = new List<AzProducts>
            {
                new AzProducts{ProductID=92},
                new AzProducts{ProductID=93},
                new AzProducts{ProductID=94},
                new AzProducts{ProductID=91},
            };
            Console.WriteLine(resultUpdate.Sql());
            Console.WriteLine();
            using (var transaction = dbConnection.BeginTransaction())
            {
                dbConnection.Execute(resultUpdate.Sql(), azProductList, transaction: transaction);
                transaction.Rollback();
            }

        }
    }
}
