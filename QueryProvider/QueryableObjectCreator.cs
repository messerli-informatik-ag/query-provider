using JsonDeserializer;
using System;
using System.Collections.Generic;
using Utility.Utility.Extension;

namespace QueryProvider.QueryProvider
{
    public class QueryableObjectCreator : EnumerableObjectCreator
    {
        private readonly IQueryableFactory _queryableFactory;

        public QueryableObjectCreator(IQueryableFactory queryableFactory)
        {
            _queryableFactory = queryableFactory;
        }

        public override object CreateInstance(Type type)
        {
            return type.IsQueryable()
                ? _queryableFactory.CreateQueryable(type.GetInnerType())
                : base.CreateInstance(type);
        }

        public override object CreateInstance(Type type, IEnumerable<object> parameters)
        {
            return type.IsQueryable()
                ? _queryableFactory.CreateQueryable(type.GetInnerType())
                : base.CreateInstance(type, parameters);
        }
    }
}