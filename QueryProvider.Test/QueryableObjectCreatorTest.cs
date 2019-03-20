using Messerli.QueryProvider.Test.Mock;
using Messerli.QueryProvider.Test.Stub;
using Messerli.ServerCommunication;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Messerli.QueryProvider.Test
{
    public class QueryableObjectCreatorTest
    {
        [Fact]
        public void ReturnsDefault()
        {
            var creator = ResolveCreator();

            Assert.Equal(default(int), creator.CreateInstance<int>());
            Assert.Equal(default(string), creator.CreateInstance<string>());
            Assert.Equal(default(EnumOneTwoThree), creator.CreateInstance<EnumOneTwoThree>());
            Assert.Equal(default(ClassWithSimpleMembers), creator.CreateInstance<ClassWithSimpleMembers>());
        }

        [Fact]
        public void ReturnsEnumerableObject()
        {
            var creator = ResolveCreator();

            Assert.Equal(new List<int> { default(int) }, creator.CreateInstance<List<int>>());
            Assert.Equal(new List<string> { default(string) }, creator.CreateInstance<List<string>>());
        }

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

        public IObjectCreator ResolveCreator()
        {
            return new QueryableObjectCreator(new QueryableFactoryMock());
        }
    }
}
