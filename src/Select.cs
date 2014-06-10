using System;
using System.Collections.Generic;

namespace Uniq
{
    public struct Select<TEnumerable, TEnumerator, T, TR> : IEnumerable<Select<TEnumerable, TEnumerator, T, TR>.Enumerator, TR>
        where TEnumerable : struct, IEnumerable<TEnumerator, T>
        where TEnumerator : struct, IEnumerator<T>
    {
        public struct Enumerator : IEnumerator<TR>
        {
            private TEnumerator inner;
            private readonly Func<T, TR> map;

            public Enumerator(TEnumerator inner, Func<T, TR> map)
                : this()
            {
                this.inner = inner;
                this.map = map;
            }

            public bool MoveNext()
            {
                return inner.MoveNext();
            }

            public TR Current
            {
                get { return map(inner.Current); }
            }
        }

        private TEnumerable inner;
        private readonly Func<T, TR> map;

        public Select(TEnumerable inner, Func<T, TR> map)
        {
            this.inner = inner;
            this.map = map;
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(inner.GetEnumerator(), map);
        }
    }

    public static partial class Uniq
    {
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
