using System;
using System.Collections.Generic;

namespace Uniq
{
    public static partial class Uniq
    {
        public static Enumerable<ListWhere<T>, ListWhere<T>.Enumerator, T> Where<T>(this List<T> list, Func<T, bool> predicate)
        {
            return new Enumerable<ListWhere<T>, ListWhere<T>.Enumerator, T>(new ListWhere<T>(list, predicate));
        }

        public static Enumerable<Where<TEnumerable, TEnumerator, T>, Where<TEnumerable, TEnumerator, T>.Enumerator, T>
            Where<TEnumerable, TEnumerator, T>(this Enumerable<TEnumerable, TEnumerator,T> enumerable, Func<T, bool> predicate) 
            where TEnumerable : struct, IEnumerable<TEnumerator, T>
            where TEnumerator : struct, IEnumerator<T>
        {
            return
                new Enumerable<Where<TEnumerable, TEnumerator, T>, Where<TEnumerable, TEnumerator, T>.Enumerator, T>(
                    new Where<TEnumerable, TEnumerator, T>(enumerable.Inner, predicate));
        }
    }
}
