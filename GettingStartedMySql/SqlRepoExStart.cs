using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlRepoEx.MySql.ConnectionProviders;
using SqlRepoEx.Static;

namespace GettingStartedMySql
{
    public static class SqlRepoExStart
    {
        public static void Config()
        {
            string ConnectionString = "datasource=127.0.0.1;username=test;password=test;database=northwind;charset=gb2312;SslMode = none;";
            var connectionProvider = new ConnectionStringConnectionProvider(ConnectionString);
            MySqlRepoFactory.UseConnectionProvider(connectionProvider);
        }
    }
}
