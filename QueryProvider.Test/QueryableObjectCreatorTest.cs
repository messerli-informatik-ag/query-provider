using QueryProvider.QueryProvider.Test.Mock;
using System.Linq;
using Xunit;

namespace QueryProvider.QueryProvider.Test
{
    public class QueryableObjectCreatorTest : EnumerableObjectCreatorTest
    {
        [Fact]
        public void ReturnsQueryObject()
        {
            var creator = ResolveCreator();

            Assert.IsAssignableFrom<IQueryable<int>>(creator.CreateInstance<IQueryable<int>>());
            Assert.IsAssignableFrom<IQueryable<int>>(creator.CreateInstance(typeof(IQueryable<int>)));

            Assert.IsAssignableFrom<IQueryable<string>>(creator.CreateInstance<IQueryable<string>>());
            Assert.IsAssignableFrom<IQueryable<string>>(creator.CreateInstance(typeof(IQueryable<string>)));

            Assert.IsAssignableFrom<IQueryable<int>>(creator.CreateInstance<IQueryable<int>>());
            Assert.IsAssignableFrom<IQueryable<int>>(creator.CreateInstance(typeof(IQueryable<int>)));
        }

        public override IObjectCreator ResolveCreator()
        {
            return new QueryableObjectCreator(new QueryableFactoryMock());
        }
    }
}
