using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoTools.BLL.DemoNorthwind;
using SqlRepoEx.Static;

namespace GettingStartedMsSql
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlRepoExStart.Config();

            //// select test
            // SelectTest.QueryOnly();
            // SelectTest.DoInnerJoin();
            // SelectTest.LeftOuterJoin();
            // SelectTest.QueryWhere();
            // SelectTest.QueryWhereIn();
            // SelectTest.QueryAvg();
            // SelectTest.QuerySum();
            // SelectTest.QueryHavingAvg();
            // SelectTest.QueryCount();
            // SelectTest.QueryUnion(true);


            //// select test Alias
            ///
            //SelectAliasTest.QueryOnly(true);
            //SelectAliasTest.DoInnerJoin();
            //SelectAliasTest.LeftOuterJoin();
            //SelectAliasTest.QueryWhere();
            //SelectAliasTest.QueryWhereIn();
            //SelectAliasTest.QueryAvg();
            //SelectAliasTest.QuerySum();
            //SelectAliasTest.QueryUnion(true);



            //// Insert test
            // InsertTest.DoInsertEntity();
            // InsertTest.DoInsertSelector();
            // InsertTest.DoInsertEntityParam();
            // InsertTest.DoInsertSelectorParam();

            ////Update test
            // UpdateTest.DoUpdateEntity();
            // UpdateTest.DoUpdateSelector();
            // UpdateTest.DoUpdateEntityParam();
            // UpdateTest.DoUpdateSelectorParam();

            //// delete Test
            //DeleteTest.DoDeleteEntity();
            //DeleteTest.DoDeleteWhere();

            /// stroe procedure

            //ProcedureTest.StoreProcedureSimpleWithDTO(true);
            // ProcedureTest.StoreProcedureSimpleNoDTO();

            //// dapper Test
            ///

            // DapperTest.QueryOnly();
            // DapperTest.DoInnerJoin();
            // DapperTest.LeftOuterJoin();
            // DapperTest.QueryWhere();
            // DapperTest.QueryWhereIn();
            // DapperTest.QueryAvg();
            // DapperTest.QueryUnion();
            // DapperTest.DoInsertEntityParam();
            // DapperTest.DoInsertEntityParamBatch();
            // DapperTest.DoUpdateEntityParam();
            // DapperTest.DoDeleteBatch();
            // DapperTest.DoDelete();
            // DapperTest.DoDeleteEntity();
            // DapperTest.DoDeleteTransaction();

            DapperTest.DoUpdateEntityReturnParam();

            Console.ReadLine();
        }
    }
}
