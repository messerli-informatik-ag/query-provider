using System;
using System.Linq;

namespace QueryProvider.QueryProvider.Test.Stub
{
    internal class ClassWithQueryableMember : IEquatable<ClassWithQueryableMember>
    {
        public ClassWithQueryableMember(string name, IQueryable<string> stringQuery)
        {
            Name = name;
            StringQuery = stringQuery;
        }

        public string Name { get; }
        public IQueryable<string> StringQuery { get; }

        #region manualy created equality functions

        public bool Equals(ClassWithQueryableMember right)
        {
            return Name.Equals(right.Name)
                   && StringQuery.ToString().Equals(right.StringQuery.ToString());
        }

        public override bool Equals(object right)
        {
            if (right is ClassWithQueryableMember typedRight)
            {
                return Equals(typedRight);
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ (StringQuery != null ? StringQuery.GetHashCode() : 0);
            }
        }

        public static bool operator ==(ClassWithQueryableMember left, ClassWithQueryableMember right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ClassWithQueryableMember left, ClassWithQueryableMember right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}