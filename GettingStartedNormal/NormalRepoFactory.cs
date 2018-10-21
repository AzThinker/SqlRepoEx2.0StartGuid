using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlRepoEx;
using SqlRepoEx.Abstractions;
using SqlRepoEx.Core.Abstractions;
using SqlRepoEx.Normal;

namespace GettingStartedNormal

{
    public static class NormalRepoFactory
    {
        private static IRepositoryFactory repositoryFactory;

        /// <summary>
        /// 类工厂创建
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public static IRepository<TEntity> Create<TEntity>() where TEntity : class, new()
        {
            EnsureRepositoryFactoryInstance();
            return repositoryFactory.Create<TEntity>();
        }

        private static void EnsureRepositoryFactoryInstance()
        {
            if (repositoryFactory != null)
            {
                return;
            }
            var statementFactoryProvider = new NormalStatementFactoryProvider();

            repositoryFactory = new RepositoryFactory(statementFactoryProvider);
        }
    }
}
