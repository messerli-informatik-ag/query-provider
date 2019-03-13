using System;
using System.Linq;

namespace QueryProvider.QueryProvider
{
    public interface IQueryableFactory
    {
        IQueryable<T> CreateQueryable<T>();

        IQueryable CreateQueryable(Type type);
    }
}