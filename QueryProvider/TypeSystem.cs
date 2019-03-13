﻿using System;
using System.Collections.Generic;

namespace QueryProvider.QueryProvider
{
    internal static class TypeSystem
    {
        internal static Type GetElementType(Type seqType)
        {
            var ienum = FindIEnumerable(seqType);
            return ienum is null ? seqType : ienum.GetGenericArguments()[0];
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
                        var ienum = typeof(IEnumerable<>).MakeGenericType(arg);

                        if (ienum.IsAssignableFrom(seqType))
                        {
                            return ienum;
                        }
                    }
                }

                var ifaces = seqType.GetInterfaces();

                if (ifaces.Length > 0)
                {
                    foreach (var iface in ifaces)
                    {
                        var ienum = FindIEnumerable(iface);
                        if (ienum != null) return ienum;
                    }
                }

                if (seqType.BaseType is null || seqType.BaseType == typeof(object)) return null;
                seqType = seqType.BaseType;
            }
        }
    }
}