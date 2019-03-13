﻿using ResourceRetriever;
using System;
using Utility.Utility.Extension;

namespace QueryProvider.QueryProvider
{
    public class QueryableObjectResolver : DefaultObjectResolver
    {
        private readonly IQueryableFactory _queryableFactory;

        public QueryableObjectResolver(IQueryableFactory queryableFactory)
        {
            _queryableFactory = queryableFactory;
        }

        public override object Resolve(Type type, object current)
        {
            return type.IsQueryable()
                ? _queryableFactory.CreateQueryable(type.GetInnerType())
                : base.Resolve(type, current);
        }
    }
}