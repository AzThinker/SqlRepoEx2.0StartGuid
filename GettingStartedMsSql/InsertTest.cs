using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoTools.BLL.DemoNorthwind;
using SqlRepoEx.Static;

namespace GettingStartedMsSql
{
    public static class InsertTest
    {
        public static void DoInsertEntity(bool go = false)
        {
            var repository = MsSqlRepoFactory.Create<AzProducts>();
            AzProducts azProducts = new AzProducts { ProductName2 = "testvalue" };
            var resultinsert = repository
                                    .Insert()
                                    .For(azProducts);
            if (go)
            {
                var rest = resultinsert.Go();
            }
            Console.WriteLine(resultinsert.Sql());
            Console.WriteLine();
            //	INSERT [dbo].[Products]([ProductName],[SupplierID],[CategoryID],[QuantityPerUnit],[UnitPrice],[UnitsInStock],[UnitsOnOrder],[ReorderLevel],[Discontinued])
            //	VALUES('testvalue',NULL,NULL,NULL,NULL,NULL,NULL,NULL,0);
            //	SELECT [ProductID],[ProductName] as ProductName2,[SupplierID],[CategoryID],[QuantityPerUnit],[UnitPrice],[UnitsInStock],[UnitsOnOrder],[ReorderLevel],[Discontinued]
            //	FROM [dbo].[Products]
            //	WHERE [ProductID] = SCOPE_IDENTITY();
        }


        public static void DoInsertEntityParam(bool go = false)
        {
            var repository = MsSqlRepoFactory.Create<AzProducts>();
            AzProducts azProducts = new AzProducts { ProductName2 = "testvalue" };
            var resultinsert = repository
                                    .Insert()
                                    .For(azProducts);
            if (go)
            {
                var rest = resultinsert.Go();
            }
            Console.WriteLine(resultinsert.ParamSql());
            Console.WriteLine();
            //	INSERT [dbo].[Products]([ProductName],[SupplierID],[CategoryID],[QuantityPerUnit],[UnitPrice],[UnitsInStock],[UnitsOnOrder],[ReorderLevel],[Discontinued])
            //	VALUES(@ProductName2,@SupplierID,@CategoryID,@QuantityPerUnit,@UnitPrice,@UnitsInStock,@UnitsOnOrder,@ReorderLevel,@Discontinued);
            //	SELECT [ProductID],[ProductName] as ProductName2,[SupplierID],[CategoryID],[QuantityPerUnit],[UnitPrice],[UnitsInStock],[UnitsOnOrder],[ReorderLevel],[Discontinued]
            //	FROM [dbo].[Products]
            //	WHERE [ProductID] = SCOPE_IDENTITY();
        }

        public static void DoInsertSelector(bool go = false)
        {
            var repository = MsSqlRepoFactory.Create<AzProducts>();
            var resultinsert = repository
                                    .Insert()
                                    .With(c => c.ProductName2, "testvalue");
            if (go)
            {
                var rest = resultinsert.Go();
            }
            Console.WriteLine(resultinsert.Sql());
            Console.WriteLine();
            // 	INSERT [dbo].[Products]([ProductName])
            // 	VALUES('testvalue');
            // 	SELECT [ProductName] as ProductName2,[ProductID]
            // 	FROM [dbo].[Products]
            // 	WHERE [ProductID] = SCOPE_IDENTITY();
        }

        public static void DoInsertSelectorParam(bool go = false)
        {
            var repository = MsSqlRepoFactory.Create<AzProducts>();
            var resultinsert = repository
                                    .Insert()
                                    .With(c => c.ProductName2, "testvalue");
            if (go)
            {
                var rest = resultinsert.Go();
            }
            Console.WriteLine(resultinsert.ParamSql());
            Console.WriteLine();
            //	INSERT [dbo].[Products]([ProductName])
            //	VALUES(@ProductName2);
            //	SELECT [ProductName] as ProductName2,[ProductID]
            //	FROM [dbo].[Products]
            //	WHERE [ProductID] = SCOPE_IDENTITY();
        }
    }
}
