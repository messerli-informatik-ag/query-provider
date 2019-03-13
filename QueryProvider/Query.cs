﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace QueryProvider
{
    [DebuggerDisplay(nameof(Query<T>))]
    public class Query<T> : IOrderedQueryable<T>, IEquatable<Query<T>>
    {
        private readonly QueryProvider _provider;

        private readonly Expression _expression;

        public Query(QueryProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
            _expression = Expression.Constant(this);
        }

        public Query(QueryProvider provider, Expression expression)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            if (!typeof(IQueryable<T>).IsAssignableFrom(expression.Type))
            {
                throw new ArgumentOutOfRangeException(nameof(expression));
            }

            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
            _expression = expression;
        }

        Expression IQueryable.Expression => _expression;

        Type IQueryable.ElementType => typeof(T);

        IQueryProvider IQueryable.Provider => _provider;

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_provider.Execute(_expression)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_provider.Execute(_expression)).GetEnumerator();
        }

        public override string ToString()
        {
            return _provider.GetQueryText(_expression);
        }

        #region Manually created equality methods

        public override bool Equals(object obj)
        {
            if (obj is Query<T> other)
            {
                return Equals(other);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public bool Equals(Query<T> other)
        {
            return other != null && ToString() == other.ToString();
        }

        public static bool operator ==(Query<T> left, Query<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Query<T> left, Query<T> right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}