using System;
using System.Collections.Generic;

namespace QueryProvider.QueryProvider
{
    public static class TypeSystem
    {
        public static Type GetElementType(Type seqType)
        {
            var foundEnumerable = FindIEnumerable(seqType);
            return foundEnumerable is null ? seqType : foundEnumerable.GetGenericArguments()[0];
        }

        private static Type FindIEnumerable(Type seqType)
        {
            while (true)
            {
                if (seqType is null || seqType == typeof(string))
                {
                    return null;
                }
                if (seqType.IsArray)
                {
                    return typeof(IEnumerable<>).MakeGenericType(seqType.GetElementType());
                }

                if (seqType.IsGenericType)
                {
                    foreach (var arg in seqType.GetGenericArguments())
                    {
                        var enumType = typeof(IEnumerable<>).MakeGenericType(arg);

                        if (enumType.IsAssignableFrom(seqType))
                        {
                            return enumType;
                        }
                    }
                }

                var interfaces = seqType.GetInterfaces();

                if (interfaces.Length > 0)
                {
                    foreach (var interfaceType in interfaces)
                    {
                        var foundEnumerable = FindIEnumerable(interfaceType);
                        if (foundEnumerable != null) return foundEnumerable;
                    }
                }

                if (seqType.BaseType is null || seqType.BaseType == typeof(object)) return null;
                seqType = seqType.BaseType;
            }
        }
    }
}