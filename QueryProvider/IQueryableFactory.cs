using System;
using System.Linq;

namespace Update.Client.ServerCommunication.QueryProvider
{
    public interface IQueryableFactory
    {
        IQueryable<T> CreateQueryable<T>();

        IQueryable CreateQueryable(Type type);
    }
}