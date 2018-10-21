using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoTools.BLL.DemoNorthwind;
using SqlRepoEx;
using SqlRepoEx.Static;

namespace GettingStartedMySql
{
    public static class ProcedureTest
    {
        public static void StoreProcedureSimpleWithDTO(bool go = false)
        {
            var repository = MySqlRepoFactory.Create<AzCustOrdersDetail>();
            // 实测，参数一定要指定方向，否则无法查询出结果
            var paramDef = new ParameterDefinition[]
                        {
                             new ParameterDefinition
                               {
                                  Name = "OrderID",
                                  Value = "10248",
                                  DbType=DbType.Int32,
                                  Direction=ParameterDirection.Input
                               }
                        };

            var result = repository.ExecuteQueryProcedure().WithParameters(paramDef);

            if (go)
            {
                var resultgo = result.Go();
                foreach (var item in resultgo)
                {
                    Console.WriteLine($"{item.ProductName}\t{item.Quantity}\t{item.ExtendedPrice}");
                }
            }
            Console.WriteLine();

        }


        public static void StoreProcedureSimpleNoDTO()
        {
            var repository = MySqlRepoFactory.Create();

            // 实测，参数一定要指定方向，否则无法查询出结果
            var paramDef = new ParameterDefinition[]
                        {
                             new ParameterDefinition
                               {
                                  Name = "OrderID",
                                  Value = "10248",
                                  DbType=DbType.Int32,
                                  Direction=ParameterDirection.Input

                               }
                        };

            IDataReader dataReader = repository.StatementExecutor.ExecuteStoredProcedure("CustOrdersDetail", paramDef);

            while (dataReader.Read())
            {
                Console.WriteLine($"{dataReader["ProductName"]}\t{dataReader["Quantity"]}\t{dataReader["ExtendedPrice"]}");
            }
            Console.WriteLine();

        }



    }
}
