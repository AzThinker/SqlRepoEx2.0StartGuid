using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoTools.BLL.DemoNorthwind;
 

namespace GettingStartedNormal
{
    public static class UpdateTest
    {
        public static void DoUpdateEntity()
        {
            var repository = NormalRepoFactory.Create<AzProducts>();
            AzProducts azProducts = new AzProducts { ProductName2 = "testvalue", ProductID = 82 };
            var resultUpdate = repository.Update().For(azProducts);

            Console.WriteLine(resultUpdate.Sql());
            Console.WriteLine();
            //	UPDATE [dbo].[Products]
            //	SET [ProductName] = 'testvalue', [SupplierID] = NULL, [CategoryID] = NULL, [QuantityPerUnit] = NULL, [UnitPrice] = NULL,
            //	[UnitsInStock] = NULL, [UnitsOnOrder] = NULL, [ReorderLevel] = NULL, [Discontinued] = 0
            //	WHERE  [ProductID] = 82;
        }


        public static void DoUpdateEntityParam()
        {
            var repository = NormalRepoFactory.Create<AzProducts>();
            AzProducts azProducts = new AzProducts { ProductName2 = "testvalue", ProductID = 82 };
            var resultUpdate = repository.Update().For(azProducts);

            Console.WriteLine(resultUpdate.ParamSql());
            Console.WriteLine();
            //	UPDATE [dbo].[Products]
            //	SET ProductName  = @ProductName2, SupplierID  = @SupplierID, CategoryID  = @CategoryID, QuantityPerUnit  = @QuantityPerUnit,
            //	UnitPrice  = @UnitPrice, UnitsInStock  = @UnitsInStock, UnitsOnOrder  = @UnitsOnOrder, ReorderLevel  = @ReorderLevel, Discontinued  = @Discontinued
            //	WHERE ProductID  = @ProductID;
        }

        public static void DoUpdateSelector()
        {
            var repository = NormalRepoFactory.Create<AzProducts>();
            var resultUpdate = repository.Update().Set(c => c.ProductName2, "testvalue")
                .Where(p => p.ProductID == 82);

            Console.WriteLine(resultUpdate.Sql());
            Console.WriteLine();
            //	UPDATE [dbo].[Products]
            //	SET [ProductName] = 'testvalue'
            //	WHERE ([dbo].[Products].[ProductID] = 82);
        }

        public static void DoUpdateSelectorParam()
        {
            var repository = NormalRepoFactory.Create<AzProducts>();
            var resultUpdate = repository.Update().Set(c => c.ProductName2, "testvalue").Where(p => p.ProductID == 82);

            Console.WriteLine(resultUpdate.ParamSql());
            Console.WriteLine();
            //	UPDATE [dbo].[Products]
            //	SET ProductName  = @ProductName2
            //	WHERE ([dbo].[Products].[ProductID] = 82);
        }


    }
}
