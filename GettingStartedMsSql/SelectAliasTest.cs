using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoTools.BLL.DemoNorthwind;
using SqlRepoEx.Core;
using SqlRepoEx.Static;

namespace GettingStartedMsSql
{
    /// <summary>
    /// Select 测试
    /// </summary>
    public static class SelectAliasTest
    {

        /// <summary>
        ///  1.test query
        ///  2.
        /// </summary>
        /// <param name="go"></param>
        public static void QueryOnly(bool go = false)
        {
            // [Column("ProductName")]
            // public string ProductName2 { get; set; }
            // Shoud Show  [dbo].[Products].[ProductName] as [ProductName2]
            var repository = MsSqlRepoFactory.Create<AzProducts>();
            var result = repository.Query().From("a1").Top(10);
            Console.WriteLine(result.Sql());
            Console.WriteLine();
            if (go)
            {
                var resultgo = result.Go();
                foreach (var item in resultgo)
                {
                    Console.WriteLine($"{item.ProductID}\t{item.Supplier}");
                }
            }
            //	SELECT TOP (10) [a1].[ProductID]
            //	, [a1].[ProductName] as [ProductName2]
            //	, [a1].[SupplierID]
            //	, [a1].[CategoryID]
            //	, [a1].[QuantityPerUnit]
            //	, [a1].[UnitPrice]
            //	, [a1].[UnitsInStock]
            //	, [a1].[UnitsOnOrder]
            //	, [a1].[ReorderLevel]
            //	, [a1].[Discontinued]
            //	FROM [dbo].[Products] AS [a1];
        }

        public static void DoInnerJoin()
        {
            var repository = MsSqlRepoFactory.Create<AzProducts>();
            var result = repository.Query().From("a1")
                                   .InnerJoin<AzSuppliers>("a2")
                                   .On<AzSuppliers>((l, r) => l.SupplierID == r.SupplierID,"a1","a2", r => r.CompanyName);
            Console.WriteLine(result.Sql());
            Console.WriteLine();
            //	SELECT [a1].[ProductID]
            //	, [a1].[ProductName] as [ProductName2]
            //	, [a1].[SupplierID]
            //	, [a1].[CategoryID]
            //	, [a1].[QuantityPerUnit]
            //	, [a1].[UnitPrice]
            //	, [a1].[UnitsInStock]
            //	, [a1].[UnitsOnOrder]
            //	, [a1].[ReorderLevel]
            //	, [a1].[Discontinued]
            //	, [a1].[CompanyName] as [Supplier]
            //	FROM [dbo].[Products] AS [a1]
            //	INNER JOIN [dbo].[Suppliers] AS [a2]
            //	ON [a1].[SupplierID] = [a2].[SupplierID];
        }


        public static void LeftOuterJoin()
        {
            var repository = MsSqlRepoFactory.Create<AzProducts>();
            var result = repository.Query().From("a1")
                                   .LeftOuterJoin<AzSuppliers>("a2")
                                   .On<AzSuppliers>((l, r) => l.SupplierID == r.SupplierID, "a1", "a2", r => r.CompanyName);
            Console.WriteLine(result.Sql());
            Console.WriteLine();
            //	SELECT [a1].[ProductID]
            //	, [a1].[ProductName] as [ProductName2]
            //	, [a1].[SupplierID]
            //	, [a1].[CategoryID]
            //	, [a1].[QuantityPerUnit]
            //	, [a1].[UnitPrice]
            //	, [a1].[UnitsInStock]
            //	, [a1].[UnitsOnOrder]
            //	, [a1].[ReorderLevel]
            //	, [a1].[Discontinued]
            //	, [a1].[CompanyName] as [Supplier]
            //	FROM [dbo].[Products] AS [a1]
            //	LEFT OUTER JOIN [dbo].[Suppliers] AS [a2]
            //	ON [a1].[SupplierID] = [a2].[SupplierID];
        }

        public static void QueryWhere(bool go = false)
        {
            var repository = MsSqlRepoFactory.Create<AzProducts>();
            var result = repository.Query().From("a1")
                                    .Where(p => p.ProductName2.Contains("test") && p.ProductID == 12 && p.ProductName2 == "test",alias: "a1");
            Console.WriteLine(result.Sql());
            Console.WriteLine();
            if (go)
            {
                var resultgo = result.Go();
                foreach (var item in resultgo)
                {
                    Console.WriteLine($"{item.ProductID}\t{item.Supplier}");
                }
            }
            //	SELECT [a1].[ProductID]
            //	, [a1].[ProductName] as [ProductName2]
            //	, [a1].[SupplierID]
            //	, [a1].[CategoryID]
            //	, [a1].[QuantityPerUnit]
            //	, [a1].[UnitPrice]
            //	, [a1].[UnitsInStock]
            //	, [a1].[UnitsOnOrder]
            //	, [a1].[ReorderLevel]
            //	, [a1].[Discontinued]
            //	FROM [dbo].[Products] AS [a1]
            //	WHERE (((([a1].[ProductName] LIKE '%test%') and ([a1].[ProductID] = 12)) and ([a1].[ProductName] ='test')));

        }

        public static void QueryWhereIn(bool go = false)
        {
            var repository = MsSqlRepoFactory.Create<AzProducts>();
            var result = repository.Query().From("a1")
                                    .WhereIn(p => p.ProductName2, new string[] { "test1", "test2" }, "a1");
            Console.WriteLine(result.Sql());
            Console.WriteLine();
            if (go)
            {
                var resultgo = result.Go();
                foreach (var item in resultgo)
                {
                    Console.WriteLine($"{item.ProductID}\t{item.Supplier}");
                }
            }
            //	SELECT [a1].[ProductID]
            //	, [a1].[ProductName] as [ProductName2]
            //	, [a1].[SupplierID]
            //	, [a1].[CategoryID]
            //	, [a1].[QuantityPerUnit]
            //	, [a1].[UnitPrice]
            //	, [a1].[UnitsInStock]
            //	, [a1].[UnitsOnOrder]
            //	, [a1].[ReorderLevel]
            //	, [a1].[Discontinued]
            //	FROM [dbo].[Products] AS [a1]
            //	WHERE ([a1].[ProductName] IN ('test1', 'test2'));

        }

        public static void QueryAvg(bool go = false)
        {
            var repository = MsSqlRepoFactory.Create<AzOrder_Details>();
            var result = repository.Query().From("a1").Select(p => p.ProductID,alias:"a1",additionalSelectors: p => p.ProductName )
                                   .Avg(c => c.Quantity, c => c.QuantityAvg,"a1")
                                   .Avg(c => c.UnitPrice,"a1")
                                   .GroupBy(c => c.ProductID,"a1")
                                   .InnerJoin<AzProducts>("a2")
                                   .On<AzProducts>((r, l) => r.ProductID == l.ProductID,"a1","a2", l => l.ProductName2)
                                   .GroupBy<AzProducts>(p => p.ProductName2,"a2")
                                   .Top(10);

            Console.WriteLine(result.Sql());
            Console.WriteLine();
            if (go)
            {
                var resultgo = result.Go();
                foreach (var item in resultgo)
                {
                    Console.WriteLine($"{item.ProductID}\t{item.ProductName}\t{item.QuantityAvg}\t{item.UnitPrice}");
                }
            }
            //	SELECT TOP (10) [a1].[ProductID]
            //	, [a1].[ProductName]
            //	, AVG([a1].[Quantity]) AS [QuantityAvg]
            //	, AVG([a1].[UnitPrice]) AS [UnitPrice]
            //	FROM [dbo].[Order Details] AS [a1]
            //	INNER JOIN [dbo].[Products] AS [a2]
            //	ON [a1].[ProductID] = [a2].[ProductID]
            //	GROUP BY [a1].[ProductID]
            //	, [a2].[ProductName];

        }
        public static void QuerySum(bool go = false)
        {
            // 已知问题，当使用平均、合计时，short 类型,合转换成 int 所以类型需要是 int
            var repository = MsSqlRepoFactory.Create<AzOrder_Details>();
            var result = repository.Query().From("a1").Select(p => p.ProductID,"a1", p => p.ProductName)
                                   .Sum(c => c.Quantity,"a1")
                                   .GroupBy(c => c.ProductID,"a1")
                                   .InnerJoin<AzProducts>("a2")
                                   .On<AzProducts>((r, l) => r.ProductID == l.ProductID,"a1","a2", l => l.ProductName2)
                                   .GroupBy<AzProducts>(p => p.ProductName2,"a2")
                                   .Top(10);

            Console.WriteLine(result.Sql());
            Console.WriteLine();
            if (go)
            {
                var resultgo = result.Go();
                foreach (var item in resultgo)
                {
                    Console.WriteLine($"{item.ProductID}\t{item.ProductName}\t{item.Quantity}\t{item.UnitPrice}");
                }
            }
            //	SELECT TOP (10) [a1].[ProductID]
            //	, [a1].[ProductName]
            //	, SUM([a1].[Quantity]) AS [Quantity]
            //	FROM [dbo].[Order Details] AS [a1]
            //	INNER JOIN [dbo].[Products] AS [a2]
            //	ON [a1].[ProductID] = [a2].[ProductID]
            //	GROUP BY [a1].[ProductID]
            //	, [a2].[ProductName];

        }


        public static void QueryUnion(bool go = false)
        {
            var repository = MsSqlRepoFactory.Create<AzCustomers>();

            // 此语句不会参与数据查询，只是作为Union的包裹
            // 如果此语句本身也是数据查询，请增加到new List<UnionSql>中
            var result = repository.Query()
                                   .Select(c => c.CustomerID, c => c.CompanyName);


            var result01 = repository.Query().From("a1")
                                    .Select(c => c.CustomerID,"a1", c => c.CompanyName)
                                    .Where(c => c.CustomerID == "ANATR","a1");

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
            if (go)
            {
                var resultAll = result.Union(new List<UnionSql>  {
                UnionSql.New(  result01,UnionType.Union ),
                UnionSql.New(  result02,UnionType.Union ),
                UnionSql.New(  result03,UnionType.Union ), });

                foreach (var item in resultAll)
                {
                    Console.WriteLine($"{item.CustomerID}\t{item.CompanyName}");
                }
            }
            //	SELECT [_this_is_union].[CustomerID]
            //	, [_this_is_union].[CompanyName]
            //	FROM ( SELECT [a1].[CustomerID]
            //	, [a1].[CompanyName]
            //	FROM [dbo].[Customers] AS [a1]
            //	WHERE (([a1].[CustomerID] = 'ANATR'))
            //	UNION
            //	 SELECT [dbo].[Customers].[CustomerID]
            //	, [dbo].[Customers].[CompanyName]
            //	FROM [dbo].[Customers]
            //	WHERE (([dbo].[Customers].[CustomerID] = 'FRANK'))
            //	UNION
            //	 SELECT [dbo].[Customers].[CustomerID]
            //	, [dbo].[Customers].[CompanyName]
            //	FROM [dbo].[Customers]
            //	WHERE (([dbo].[Customers].[CustomerID] = 'TRADH')) )
            //	AS  _this_is_union

        }


    }
}
