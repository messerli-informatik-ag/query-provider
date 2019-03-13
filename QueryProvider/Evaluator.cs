using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Update.Client.ServerCommunication.QueryProvider
{
    public static class Evaluator
    {
        /// <summary>
        /// Performs evaluation & replacement of independent sub-trees
        /// </summary>
        /// <param name="expression">The root of the expression tree.</param>
        /// <param name="canBeEvaluatedPredicate">A function that decides whether a given expression node can be part of the local function.</param>
        /// <returns>A new tree with sub-trees evaluated and replaced.</returns>

        public static Expression PartialEval(Expression expression, Predicate<Expression> canBeEvaluatedPredicate)
        {
            return new SubtreeEvaluator(new Nominator(canBeEvaluatedPredicate).Nominate(expression)).Eval(expression);
        }

        /// <summary>
        /// Performs evaluation & replacement of independent sub-trees
        /// </summary>
        /// <param name="expression">The root of the expression tree.</param>
        /// <returns>A new tree with sub-trees evaluated and replaced.</returns>
        public static Expression PartialEval(Expression expression)
        {
            return PartialEval(expression, CanBeEvaluatedLocally);
        }

        private static bool CanBeEvaluatedLocally(Expression expression)
        {
            return expression.NodeType != ExpressionType.Parameter;
        }

        /// <inheritdoc />
        /// <summary>
        /// Evaluates & replaces sub-trees when first candidate is reached (top-down)
        /// </summary>
        private class SubtreeEvaluator : ExpressionVisitor
        {
            private readonly HashSet<Expression> _candidates;

            internal SubtreeEvaluator(HashSet<Expression> candidates)
            {
                _candidates = candidates;
            }

            internal Expression Eval(Expression expression)
            {
                return Visit(expression);
            }

            public override Expression Visit(Expression expression)
            {
                if (expression is null)
                {
                    return null;
                }

                return _candidates.Contains(expression)
                    ? Evaluate(expression)
                    : base.Visit(expression);
            }

            private static Expression Evaluate(Expression expression)
            {
                if (expression.NodeType == ExpressionType.Constant)
                {
                    return expression;
                }

                var lambda = Expression.Lambda(expression);
                var @delegate = lambda.Compile();
                return Expression.Constant(@delegate.DynamicInvoke(null), expression.Type);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Performs bottom-up analysis to determine which nodes can possibly
        /// be part of an evaluated sub-tree.
        /// </summary>
        private class Nominator : ExpressionVisitor
        {
            private readonly Predicate<Expression> _canBeEvaluatedPredicate;
            private HashSet<Expression> _candidates;
            private bool _cannotBeEvaluated;

            internal Nominator(Predicate<Expression> canBeEvaluatedPredicate)
            {
                _canBeEvaluatedPredicate = canBeEvaluatedPredicate;
            }

            internal HashSet<Expression> Nominate(Expression expression)
            {
                _candidates = new HashSet<Expression>();
                Visit(expression);
                return _candidates;
            }

            public override Expression Visit(Expression expression)
            {
                if (expression == null)
                {
                    return null;
                }

                var saveCannotBeEvaluated = _cannotBeEvaluated;
                _cannotBeEvaluated = false;
                base.Visit(expression);

                if (!_cannotBeEvaluated)
                {
                    if (_canBeEvaluatedPredicate(expression))
                    {
                        _candidates.Add(expression);
                    }
                    else
                    {
                        _cannotBeEvaluated = true;
                    }
                }

                _cannotBeEvaluated |= saveCannotBeEvaluated;
                return expression;
            }
        }
    }
}