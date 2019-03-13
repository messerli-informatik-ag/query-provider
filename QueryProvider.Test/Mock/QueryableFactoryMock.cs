using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Update.Client.ServerCommunication.QueryProvider;

namespace Update.Client.ServerCommunication.Test.QueryProvider.Mocks
{
    public class QueryableFactoryMock : IQueryableFactory
    {
        public IQueryable<T> CreateQueryable<T>()
        {
            return new List<T>().AsQueryable();
        }

        public IQueryable CreateQueryable(Type type)
        {
            var list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(type));
            return list.AsQueryable();
        }
    }
}