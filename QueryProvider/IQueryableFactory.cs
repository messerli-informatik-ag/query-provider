using System;
using System.Linq;

namespace Messerli.QueryProvider
{
    public interface IQueryableFactory
    {
        IQueryable<T> CreateQueryable<T>();

        IQueryable CreateQueryable(Type type);
    }
}
