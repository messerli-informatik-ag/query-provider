using Messerli.ServerCommunication;
using System;
using System.Linq;
using Messerli.Utility.Extension;

namespace Messerli.QueryProvider
{
    public class QueryableFactory : IQueryableFactory
    {
        private readonly IQueryProviderFactory _queryProviderFactory;

        public QueryableFactory(IQueryProviderFactory queryProviderFactory)
        {
            _queryProviderFactory = queryProviderFactory;
        }

        public IQueryable<T> CreateQueryable<T>()
        {
            return new Query<T>(_queryProviderFactory.Create());
        }

        public IQueryable CreateQueryable(Type type)
        {
            return CreateQueryable(type, _queryProviderFactory.Create());
        }

        public IQueryable CreateQueryable(ObjectToResolve objectToResolve)
        {
            return CreateQueryable(objectToResolve.Type.GetInnerType(), _queryProviderFactory.Create(objectToResolve));
        }

        private static IQueryable CreateQueryable(Type type, QueryProvider queryProvider)
        {
            return typeof(Query<>)
                .MakeGenericType(type)
                .GetConstructors().First()
                .Invoke(new object[] { queryProvider }) as IQueryable;
        }
    }
}