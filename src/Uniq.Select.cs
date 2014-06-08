using System;
using System.Collections.Generic;

namespace Uniq
{
    public static partial class Uniq
    {
        public static Enumerable<ListSelect<T, TR>, ListSelect<T, TR>.Enumerator, TR> Select<T, TR>(this List<T> list,
            Func<T, TR> mapFunc)
        {
            return
                new Enumerable<ListSelect<T, TR>, ListSelect<T, TR>.Enumerator, TR>(
                    new ListSelect<T, TR>(list, mapFunc));
        }

        public static Enumerable<Select<TEnumerable, TEnumerator, T, TR>, Select<TEnumerable, TEnumerator, T, TR>.Enumerator, TR>
            Select<TEnumerable, TEnumerator, T, TR>
            (this Enumerable<TEnumerable, TEnumerator, T> enumerable, Func<T, TR> mapFunc)
            where TEnumerable : struct, IEnumerable<TEnumerator, T>
            where TEnumerator : struct, IEnumerator<T>
        {
            return new Enumerable
                <Select<TEnumerable, TEnumerator, T, TR>, Select<TEnumerable, TEnumerator, T, TR>.Enumerator, TR>(
                new Select<TEnumerable, TEnumerator, T, TR>(enumerable.Inner, mapFunc));
        }

    }
}
