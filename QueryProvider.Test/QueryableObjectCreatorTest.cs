using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using QueryProvider.Test.Mock;
using ResourceRetriever;
using ResourceRetriever.Test;
using Xunit;

namespace QueryProvider.Test
{
    public class QueryableObjectCreatorTest
    {
        [Fact]
        public void ReturnsDefault()
        {
            var creator = ResolveCreator();

            Assert.Equal(default(int), creator.CreateInstance<int>());
            Assert.Equal(default(string), creator.CreateInstance<string>());
            Assert.Equal(default(ObjectCreatorTestEnum), creator.CreateInstance<ObjectCreatorTestEnum>());
            Assert.Equal(default(ObjectCreatorTestClass), creator.CreateInstance<ObjectCreatorTestClass>());
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

        #region TestStructures
        public enum ObjectCreatorTestEnum
        {
            [UsedImplicitly] One,
            [UsedImplicitly] Two,
            [UsedImplicitly] Three,
        }

        public class ObjectCreatorTestClass : IEquatable<ObjectCreatorTestClass>
        {
            public ObjectCreatorTestClass(int testInt, string testString, ObjectCreatorTestEnum testEnum)
            {
                TestInt = testInt;
                TestString = testString;
                TestEnum = testEnum;
            }

            public int TestInt { get; }

            public string TestString { get; }

            public ObjectCreatorTestEnum TestEnum { get; }

            #region manualy created equality functions

            public bool Equals(ObjectCreatorTestClass obj)
            {
                return TestInt.Equals(obj.TestInt)
                       && TestString.Equals(obj.TestString)
                       && TestEnum.Equals(obj.TestEnum);
            }

            public override bool Equals(object obj)
            {
                if (obj is ObjectCreatorTestClass instance)
                {
                    return Equals(instance);
                }

                return false;
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = TestInt;
                    hashCode = (hashCode * 397) ^ (TestString != null ? TestString.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (int)TestEnum;
                    return hashCode;
                }
            }

            public static bool operator ==(ObjectCreatorTestClass left, ObjectCreatorTestClass right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(ObjectCreatorTestClass left, ObjectCreatorTestClass right)
            {
                return !Equals(left, right);
            }

            #endregion
        }

        #endregion
    }
}
