using System;
using System.Linq;

namespace QueryProvider
{
    public class QueryableFactory : IQueryableFactory
    {
        private readonly QueryProvider _queryProvider;

        public QueryableFactory(QueryProvider queryProvider)
        {
            _queryProvider = queryProvider;
        }

        public IQueryable<T> CreateQueryable<T>()
        {
            return new Query<T>(_queryProvider);
        }

        public IQueryable CreateQueryable(Type type)
        {
            return typeof(Query<>)
                .MakeGenericType(type)
                .GetConstructors().First()
                .Invoke(new object[] { _queryProvider }) as IQueryable;
        }
    }
}