using System;

namespace Messerli.QueryProvider.Test.Stub
{
    public class ClassWithSimpleMembers : IEquatable<ClassWithSimpleMembers>
    {
        public ClassWithSimpleMembers(int testInt, string testString, EnumOneTwoThree testEnumOneTwoThree)
        {
            TestInt = testInt;
            TestString = testString;
            TestEnumOneTwoThree = testEnumOneTwoThree;
        }

        public int TestInt { get; }

        public string TestString { get; }

        public EnumOneTwoThree TestEnumOneTwoThree { get; }

        #region manualy created equality functions

        public bool Equals(ClassWithSimpleMembers obj)
        {
            return TestInt.Equals(obj.TestInt)
                   && TestString.Equals(obj.TestString)
                   && TestEnumOneTwoThree.Equals(obj.TestEnumOneTwoThree);
        }

        public override bool Equals(object obj)
        {
            if (obj is ClassWithSimpleMembers instance)
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
                hashCode = (hashCode * 397) ^ (int)TestEnumOneTwoThree;
                return hashCode;
            }
        }

        public static bool operator ==(ClassWithSimpleMembers left, ClassWithSimpleMembers right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ClassWithSimpleMembers left, ClassWithSimpleMembers right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}