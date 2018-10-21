using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoTools.BLL.DemoNorthwind;
using SqlRepoEx;
using SqlRepoEx.Static;

namespace GettingStartedMsSql
{
    public static class ProcedureTest
    {
        public static void StoreProcedureSimpleWithDTO(bool go = false)
        {
            var repository = MsSqlRepoFactory.Create<AzCustOrdersDetail>();

            var paramDef = new ParameterDefinition[]
                        {
                             new ParameterDefinition
                               {
                                  Name = "OrderID",
                                  Value = "10248"
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

        }


        public static void StoreProcedureSimpleNoDTO()
        {
            var repository = MsSqlRepoFactory.Create();

            var paramDef = new ParameterDefinition[]
                        {
                             new ParameterDefinition
                               {
                                  Name = "OrderID",
                                  Value = "10248"
                               }
                        };

            IDataReader dataReader = repository.StatementExecutor.ExecuteStoredProcedure("CustOrdersDetail", paramDef);

            while (dataReader.Read())
            {
                Console.WriteLine($"{dataReader["ProductName"]}\t{dataReader["Quantity"]}\t{dataReader["ExtendedPrice"]}");
            }


        }



    }
}
