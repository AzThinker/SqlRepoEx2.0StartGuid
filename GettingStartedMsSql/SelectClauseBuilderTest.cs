using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoTools.BLL.DemoNorthwind;
using SqlRepoEx.Abstractions;
using SqlRepoEx.Core.Abstractions;
using SqlRepoEx.Static;

namespace GettingStartedMsSql
{
    public static  class SelectClauseBuilderTest
    {
        

        public static void SelectTest()
        {
            var repository = MsSqlRepoFactory.Create<AzOrder_Details>();
            var result = repository.Query();


            if (1==1)
            {
                result = result.Select(e => e.ProductID, e => e.Quantity,e=>e.ProductName);
            }


            if (2 == 2)
            {
                result = result.Where(e => e.ProductID>1);
            }


            if (3 == 3)
            {
                result = result.Or(e => e.ProductID == 2);
            }

            if (4==4)
            {
                result = result.InnerJoin<AzProducts>()
                    .On<AzProducts>((l, r) => l.ProductID == r.ProductID,r=>r.ProductName2);
            }




            Console.WriteLine(result.Sql());
            Console.WriteLine();

        }


    }
}
