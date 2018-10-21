using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlRepoEx.MsSqlServer.ConnectionProviders;
using SqlRepoEx.Static;

namespace GettingStartedMsSql
{
    public static class SqlRepoExStart
    {
        public static void Config()
        {
            string ConnectionString = "Data Source=(Local);Initial Catalog=Northwind;User ID=test;Password=test";
            var connectionProvider = new ConnectionStringConnectionProvider(ConnectionString);
            MsSqlRepoFactory.UseConnectionProvider(connectionProvider);
        }
    }
}
