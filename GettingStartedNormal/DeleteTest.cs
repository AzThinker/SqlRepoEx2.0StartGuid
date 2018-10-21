using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoTools.BLL.DemoNorthwind;


namespace GettingStartedNormal
{
    public static class DeleteTest
    {
        public static void DoDeleteEntity()
        {
            var repository = NormalRepoFactory.Create<AzProducts>();
            AzProducts azProducts = new AzProducts { ProductName2 = "testvalue", ProductID = 82 };
            var resultUpdate = repository.Delete().For(azProducts);

            Console.WriteLine(resultUpdate.Sql());
            Console.WriteLine();
            //	DELETE [dbo].[Products] WHERE  [ProductID] = 82;
        }


        public static void DoDelete()
        {
            var repository = NormalRepoFactory.Create<AzProducts>();

            var resultUpdate = repository.Delete().Where(p => p.ProductID == 82);

            Console.WriteLine(resultUpdate.Sql());
            Console.WriteLine();
            //	DELETE [dbo].[Products] WHERE  [ProductID] = 82;
        }

        public static void DoDeleteWhere()
        {
            var repository = NormalRepoFactory.Create<AzProducts>();

            var resultUpdate = repository.Delete().Where(p => p.ProductID == 82).And(p => p.ProductName2 == "test2");

            Console.WriteLine(resultUpdate.Sql());
            Console.WriteLine();
            // 	DELETE [dbo].[Products]
            // 	WHERE ([dbo].[Products].[ProductID] = 82 AND [dbo].[Products].[ProductName] = 'test2');
        }
    }
}
