using Messerli.ServerCommunication;
using Messerli.Utility.Extension;

namespace Messerli.QueryProvider
{
    public class QueryableObjectResolver : DefaultObjectResolver
    {
        private readonly IQueryableFactory _queryableFactory;

        public QueryableObjectResolver(IQueryableFactory queryableFactory)
        {
            _queryableFactory = queryableFactory;
        }

        public override object Resolve(ObjectToResolve objectToResolve)
        {
            return objectToResolve.Type.IsQueryable()
                ? _queryableFactory.CreateQueryable(objectToResolve)
                : base.Resolve(objectToResolve);
        }
    }
}