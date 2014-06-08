using System;
using System.Collections.Generic;

namespace Uniq
{
    public struct ListWhere<T>:IEnumerable<ListWhere<T>.Enumerator, T>
    {
        public struct Enumerator : IEnumerator<T>
        {
            private List<T>.Enumerator inner;
            private readonly Func<T, bool> predicate;
            private T current;

            public Enumerator(List<T>.Enumerator inner, Func<T, bool> predicate)
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

        private readonly List<T> inner;
        private readonly Func<T, bool> predicate;

        public ListWhere(List<T> inner, Func<T, bool> predicate)
        {
            this.inner = inner;
            this.predicate = predicate;
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(inner.GetEnumerator(), predicate);
        }
    }

    public struct Where<TEnumerable, TEnumerator, T> : IEnumerable<Where<TEnumerable, TEnumerator, T>.Enumerator, T>
        where TEnumerable : struct, IEnumerable<TEnumerator, T> 
        where TEnumerator : struct, IEnumerator<T>
    {
        public struct Enumerator:IEnumerator<T>
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
}
