using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoTools.BLL.DemoNorthwind;
using SqlRepoEx.Static;

namespace GettingStartedMsSql
{
    public static class UpdateTest
    {
        public static void DoUpdateEntity(bool go = false)
        {
            var repository = MsSqlRepoFactory.Create<AzProducts>();
            AzProducts azProducts = new AzProducts { ProductName2 = "testvalue", ProductID = 82 };
            var resultUpdate = repository.Update().For(azProducts);
            if (go)
            {
                var rest = resultUpdate.Go();
            }
            Console.WriteLine(resultUpdate.Sql());
            Console.WriteLine();
            //	UPDATE [dbo].[Products]
            //	SET [ProductName] = 'testvalue', [SupplierID] = NULL, [CategoryID] = NULL, [QuantityPerUnit] = NULL, [UnitPrice] = NULL,
            //	[UnitsInStock] = NULL, [UnitsOnOrder] = NULL, [ReorderLevel] = NULL, [Discontinued] = 0
            //	WHERE  [ProductID] = 82;
        }


        public static void DoUpdateEntityParam(bool go = false)
        {
            var repository = MsSqlRepoFactory.Create<AzProducts>();
            AzProducts azProducts = new AzProducts { ProductName2 = "testvalue", ProductID = 82 };
            var resultUpdate = repository.Update().For(azProducts);
            if (go)
            {
                var rest = resultUpdate.Go();
            }
            Console.WriteLine(resultUpdate.ParamSql());
            Console.WriteLine();
            //	UPDATE [dbo].[Products]
            //	SET ProductName  = @ProductName2, SupplierID  = @SupplierID, CategoryID  = @CategoryID, QuantityPerUnit  = @QuantityPerUnit,
            //	UnitPrice  = @UnitPrice, UnitsInStock  = @UnitsInStock, UnitsOnOrder  = @UnitsOnOrder, ReorderLevel  = @ReorderLevel, Discontinued  = @Discontinued
            //	WHERE ProductID  = @ProductID;
        }

        public static void DoUpdateSelector(bool go = false)
        {
            var repository = MsSqlRepoFactory.Create<AzProducts>();
            var resultUpdate = repository.Update().Set(c => c.ProductName2, "testvalue")
                .Where(p => p.ProductID == 82);
            if (go)
            {
                var rest = resultUpdate.Go();
            }
            Console.WriteLine(resultUpdate.Sql());
            Console.WriteLine();
            //	UPDATE [dbo].[Products]
            //	SET [ProductName] = 'testvalue'
            //	WHERE ([dbo].[Products].[ProductID] = 82);
        }

        public static void DoUpdateSelectorParam(bool go = false)
        {
            var repository = MsSqlRepoFactory.Create<AzProducts>();
            var resultUpdate = repository.Update().Set(c => c.ProductName2, "testvalue").Where(p => p.ProductID == 82);
            if (go)
            {
                var rest = resultUpdate.Go();
            }
            Console.WriteLine(resultUpdate.ParamSql());
            Console.WriteLine();
            //	UPDATE [dbo].[Products]
            //	SET ProductName  = @ProductName2
            //	WHERE ([dbo].[Products].[ProductID] = 82);
        }

        
    }
}
