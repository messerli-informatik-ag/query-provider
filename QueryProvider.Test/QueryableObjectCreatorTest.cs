using JsonDeserializer;
using JsonDeserializer.Test;
using System.Linq;
using Update.Client.ServerCommunication.QueryProvider;
using Update.Client.ServerCommunication.Test.QueryProvider.Mocks;
using Xunit;

namespace Update.Client.ServerCommunication.Test.QueryProvider
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
