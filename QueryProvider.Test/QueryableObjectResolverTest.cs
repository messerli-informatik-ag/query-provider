using System.Collections.Generic;
using System.Linq;
using QueryProvider.Test.Mock;
using QueryProvider.Test.Stub;
using ResourceRetriever;
using ResourceRetriever.Test;
using Xunit;

namespace QueryProvider.Test
{
    public class QueryableObjectResolverTest : DefaultObjectResolverTest
    {
        [Theory]
        [MemberData(nameof(GetQueryableTestObjects))]
        public void ReturnsQueryObject(object obj)
        {
            var resolver = ResolveResolver();

            Assert.Equal(obj, resolver.Resolve(obj.GetType(), obj));
        }

        public override IObjectResolver ResolveResolver()
        {
            return new QueryableObjectResolver(new QueryableFactoryMock());
        }

        public static IEnumerable<object[]> GetQueryableTestObjects()
        {
            yield return new object[] { new List<int>().AsQueryable() };
            yield return new object[] { new List<string>().AsQueryable() };
            yield return new object[] { new ClassWithQueryableMember("Test", new List<string>().AsQueryable()) };
        }
    }
}