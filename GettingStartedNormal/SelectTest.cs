using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoTools.BLL.DemoNorthwind;
using SqlRepoEx.Core;
 

namespace GettingStartedNormal
{
    /// <summary>
    /// Select 测试
    /// </summary>
    public static class SelectTest
    {

        /// <summary>
        ///  1.test query
        ///  2.
        /// </summary>
        /// <param name="go"></param>
        public static void QueryOnly( )
        {
            // [Column("ProductName")]
            // public string ProductName2 { get; set; }
            // Shoud Show  [dbo].[Products].[ProductName] as [ProductName2]
            var repository = NormalRepoFactory.Create<AzProducts>();
            var result = repository.Query();
            Console.WriteLine(result.Sql());
            Console.WriteLine();
      
            // SELECT [dbo].[Products].[ProductID]
            // , [dbo].[Products].[ProductName] as [ProductName2]
            // , [dbo].[Products].[SupplierID]
            // , [dbo].[Products].[CategoryID]
            // , [dbo].[Products].[QuantityPerUnit]
            // , [dbo].[Products].[UnitPrice]
            // , [dbo].[Products].[UnitsInStock]
            // , [dbo].[Products].[UnitsOnOrder]
            // , [dbo].[Products].[ReorderLevel]
            // , [dbo].[Products].[Discontinued]
            // FROM [dbo].[Products];
        }

        public static void DoInnerJoin()
        {
            var repository = NormalRepoFactory.Create<AzProducts>();
            var result = repository.Query()
                                   .InnerJoin<AzSuppliers>()
                                   .On<AzSuppliers>((l, r) => l.SupplierID == r.SupplierID, r => r.CompanyName);
            Console.WriteLine(result.Sql());
            Console.WriteLine();
            // SELECT [dbo].[Products].[ProductID]
            // , [dbo].[Products].[ProductName] as [ProductName2]
            // , [dbo].[Products].[SupplierID]
            // , [dbo].[Products].[CategoryID]
            // , [dbo].[Products].[QuantityPerUnit]
            // , [dbo].[Products].[UnitPrice]
            // , [dbo].[Products].[UnitsInStock]
            // , [dbo].[Products].[UnitsOnOrder]
            // , [dbo].[Products].[ReorderLevel]
            // , [dbo].[Products].[Discontinued]
            // , [dbo].[Suppliers].[CompanyName] as [Supplier]
            // FROM [dbo].[Products]
            // INNER JOIN [dbo].[Suppliers]
            // ON [dbo].[Products].[SupplierID] = [dbo].[Suppliers].[SupplierID];
        }


        public static void LeftOuterJoin()
        {
            var repository = NormalRepoFactory.Create<AzProducts>();
            var result = repository.Query()
                                   .LeftOuterJoin<AzSuppliers>()
                                   .On<AzSuppliers>((l, r) => l.SupplierID == r.SupplierID, r => r.CompanyName);
            Console.WriteLine(result.Sql());
            Console.WriteLine();
            //	SELECT [dbo].[Products].[ProductID]
            //	, [dbo].[Products].[ProductName] as [ProductName2]
            //	, [dbo].[Products].[SupplierID]
            //	, [dbo].[Products].[CategoryID]
            //	, [dbo].[Products].[QuantityPerUnit]
            //	, [dbo].[Products].[UnitPrice]
            //	, [dbo].[Products].[UnitsInStock]
            //	, [dbo].[Products].[UnitsOnOrder]
            //	, [dbo].[Products].[ReorderLevel]
            //	, [dbo].[Products].[Discontinued]
            //	, [dbo].[Suppliers].[CompanyName] as [Supplier]
            //	FROM [dbo].[Products]
            //	LEFT OUTER JOIN [dbo].[Suppliers]
            //	ON [dbo].[Products].[SupplierID] = [dbo].[Suppliers].[SupplierID];
        }

        public static void QueryWhere( )
        {
            var repository = NormalRepoFactory.Create<AzProducts>();
            var result = repository.Query().From("a1")
                                    .Where(p => p.ProductName2.Contains("test") && p.ProductID == @p.@ProductID && p.ProductName2 == @p.ProductName2,alias: "a1");
            Console.WriteLine(result.Sql());
            Console.WriteLine();
 
            //	SELECT [dbo].[Products].[ProductID]
            //	, [dbo].[Products].[ProductName] as [ProductName2]
            //	, [dbo].[Products].[SupplierID]
            //	, [dbo].[Products].[CategoryID]
            //	, [dbo].[Products].[QuantityPerUnit]
            //	, [dbo].[Products].[UnitPrice]
            //	, [dbo].[Products].[UnitsInStock]
            //	, [dbo].[Products].[UnitsOnOrder]
            //	, [dbo].[Products].[ReorderLevel]
            //	, [dbo].[Products].[Discontinued]
            //	FROM [dbo].[Products]
            //	WHERE ((([dbo].[Products].[ProductName] LIKE '%' + 'test' + '%') and ([dbo].[Products].[ProductID] = 100)));

        }

        public static void QueryWhereIn( )
        {
            var repository = NormalRepoFactory.Create<AzProducts>();
            var result = repository.Query()
                                    .WhereIn(p => p.ProductName2, new string[] { "test1", "test2" });
            Console.WriteLine(result.Sql());
            Console.WriteLine();
 
            //	SELECT [dbo].[Products].[ProductID]
            //	, [dbo].[Products].[ProductName] as [ProductName2]
            //	, [dbo].[Products].[SupplierID]
            //	, [dbo].[Products].[CategoryID]
            //	, [dbo].[Products].[QuantityPerUnit]
            //	, [dbo].[Products].[UnitPrice]
            //	, [dbo].[Products].[UnitsInStock]
            //	, [dbo].[Products].[UnitsOnOrder]
            //	, [dbo].[Products].[ReorderLevel]
            //	, [dbo].[Products].[Discontinued]
            //	FROM [dbo].[Products]
            //	WHERE ([dbo].[Products].[ProductName] IN ('test1', 'test2'));

        }

        public static void QueryAvg( )
        {
            var repository = NormalRepoFactory.Create<AzOrder_Details>();
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
 
            //	SELECT TOP (10) [dbo].[Order Details].[ProductID]
            //	, [dbo].[Products].[ProductName]
            //	, AVG([dbo].[Order Details].[Quantity]) AS [QuantityAvg]
            //	, AVG([dbo].[Order Details].[UnitPrice]) AS [UnitPrice]
            //	FROM [dbo].[Order Details]
            //	INNER JOIN [dbo].[Products]
            //	ON [dbo].[Order Details].[ProductID] = [dbo].[Products].[ProductID]
            //	GROUP BY [dbo].[Order Details].[ProductID]
            //	, [dbo].[Products].[ProductName];

        }
        public static void QuerySum( )
        {
            // 已知问题，当使用平均、合计时，short 类型,合转换成 int 所以类型需要是 int
            var repository = NormalRepoFactory.Create<AzOrder_Details>();
            var result = repository.Query().Select(p => p.ProductID, p => p.ProductName)
                                   .Sum(c => c.Quantity)
                                   .GroupBy(c => c.ProductID)
                                   .InnerJoin<AzProducts>()
                                   .On<AzProducts>((r, l) => r.ProductID == l.ProductID, l => l.ProductName2)
                                   .GroupBy<AzProducts>(p => p.ProductName2)
                                   .Top(10);

            Console.WriteLine(result.Sql());
            Console.WriteLine();
 
            //	SELECT TOP (10) [dbo].[Order Details].[ProductID]
            //	, [dbo].[Products].[ProductName]
            //	, SUM([dbo].[Order Details].[Quantity]) AS [Quantity]
            //	FROM [dbo].[Order Details]
            //	INNER JOIN [dbo].[Products]
            //	ON [dbo].[Order Details].[ProductID] = [dbo].[Products].[ProductID]
            //	GROUP BY [dbo].[Order Details].[ProductID]
            //	, [dbo].[Products].[ProductName];

        }

        public static void QueryHavingAvg( )
        {
            // 已知问题，当使用平均、合计时，short 类型,合转换成 int 所以类型需要是 int
            var repository = NormalRepoFactory.Create<AzOrder_Details>();
            var result = repository.Query().Select(p => p.ProductID, p => p.ProductName)
                                   .Avg(c => c.UnitPrice)
                                   .GroupBy(c => c.ProductID)
                                   .InnerJoin<AzProducts>()
                                   .On<AzProducts>((r, l) => r.ProductID == l.ProductID, l => l.ProductName2)
                                   .GroupBy<AzProducts>(p => p.ProductName2)
                                   .HavingAvg(c => c.Quantity > 10)
                                   .Top(10);

            Console.WriteLine(result.Sql());
            Console.WriteLine();
  
            //	SELECT TOP (10) [dbo].[Order Details].[ProductID]
            //	, [dbo].[Products].[ProductName]
            //	, AVG([dbo].[Order Details].[UnitPrice]) AS [UnitPrice]
            //	FROM [dbo].[Order Details]
            //	INNER JOIN [dbo].[Products]
            //	ON [dbo].[Order Details].[ProductID] = [dbo].[Products].[ProductID]
            //	GROUP BY [dbo].[Order Details].[ProductID]
            //	, [dbo].[Products].[ProductName]
            //	HAVING AVG([dbo].[Order Details].[Quantity]) > 10;

        }

        public static void QueryCount( )
        {
            // 已知问题，当使用平均、合计时，short 类型,合转换成 int 所以类型需要是 int
            var repository = NormalRepoFactory.Create<AzOrder_Details>();
            var result = repository.Query().Select(p => p.ProductID, p => p.ProductName)
                                   .Count(c => c.OrderID, c => c.OrderCount)
                                   .GroupBy(c => c.ProductID)
                                   .InnerJoin<AzProducts>()
                                   .On<AzProducts>((r, l) => r.ProductID == l.ProductID, l => l.ProductName2)
                                   .GroupBy<AzProducts>(p => p.ProductName2)
                                   .Top(10);

            Console.WriteLine(result.Sql());
            Console.WriteLine();
 
            //	SELECT TOP (10) [dbo].[Order Details].[ProductID]
            //	, [dbo].[Products].[ProductName]
            //	, SUM([dbo].[Order Details].[Quantity]) AS [Quantity]
            //	FROM [dbo].[Order Details]
            //	INNER JOIN [dbo].[Products]
            //	ON [dbo].[Order Details].[ProductID] = [dbo].[Products].[ProductID]
            //	GROUP BY [dbo].[Order Details].[ProductID]
            //	, [dbo].[Products].[ProductName]
            //	HAVING AVG([dbo].[Order Details].[Quantity]) > 10;

        }

        public static void QueryUnion( )
        {
            var repository = NormalRepoFactory.Create<AzCustomers>();

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
 
            //	SELECT [_this_is_union].[CustomerID]
            //	, [_this_is_union].[CompanyName]
            //	FROM ( SELECT [dbo].[Customers].[CustomerID]
            //	, [dbo].[Customers].[CompanyName]
            //	FROM [dbo].[Customers]
            //	WHERE (([dbo].[Customers].[CustomerID] = 'ANATR'))
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
