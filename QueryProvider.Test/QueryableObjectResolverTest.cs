using System.Collections.Generic;
using System.Linq;
using Messerli.ServerCommunication;
using QueryProvider.Test.Mock;
using QueryProvider.Test.Stub;
using Xunit;

namespace QueryProvider.Test
{
    public class QueryableObjectResolverTest
    {
        [Theory]
        [MemberData(nameof(GetTestObjects))]
        public void ReturnsObject(object obj)
        {
            var resolver = ResolveResolver();

            Assert.Equal(obj, resolver.Resolve(obj.GetType(), obj));
        }

        [Fact]
        public void ReturnsNull()
        {
            var resolver = ResolveResolver();

            Assert.Null(resolver.Resolve(typeof(int?), null));
            Assert.Null(resolver.Resolve(typeof(string), null));
        }

        [Theory]
        [MemberData(nameof(GetQueryableTestObjects))]
        public void ReturnsQueryObject(object obj)
        {
            var resolver = ResolveResolver();

            Assert.Equal(obj, resolver.Resolve(obj.GetType(), obj));
        }

        public IObjectResolver ResolveResolver()
        {
            return new QueryableObjectResolver(new QueryableFactoryMock());
        }

        public static IEnumerable<object[]> GetTestObjects()
        {
            yield return new object[] { 2 };
            yield return new object[] { "Test" };
            yield return new object[] { new[] { 1, 2, 3, 4 } };
        }

        public static IEnumerable<object[]> GetQueryableTestObjects()
        {
            yield return new object[] { new List<int>().AsQueryable() };
            yield return new object[] { new List<string>().AsQueryable() };
            yield return new object[] { new ClassWithQueryableMember("Test", new List<string>().AsQueryable()) };
        }
    }
}