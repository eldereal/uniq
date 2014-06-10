using System;
using System.Collections.Generic;

namespace Uniq
{
    public struct Where<TEnumerable, TEnumerator, T> : IEnumerable<Where<TEnumerable, TEnumerator, T>.Enumerator, T>
        where TEnumerable : struct, IEnumerable<TEnumerator, T>
        where TEnumerator : struct, IEnumerator<T>
    {
        public struct Enumerator : IEnumerator<T>
        {
            private TEnumerator inner;
            private readonly Func<T, bool> predicate;
            private T current;

            public Enumerator(TEnumerator inner, Func<T, bool> predicate)
                : this()
            {
                this.inner = inner;
                this.predicate = predicate;
            }

            public bool MoveNext()
            {
                while (inner.MoveNext())
                {
                    T cur = inner.Current;
                    if (predicate(cur))
                    {
                        current = cur;
                        return true;
                    }
                }
                return false;
            }

            public T Current
            {
                get { return current; }
            }
        }

        private TEnumerable inner;
        private readonly Func<T, bool> predicate;

        public Where(TEnumerable inner, Func<T, bool> predicate)
        {
            this.inner = inner;
            this.predicate = predicate;
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(inner.GetEnumerator(), predicate);
        }
    }

    public static partial class Uniq
    {
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
