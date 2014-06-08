using System;
using System.Collections.Generic;

namespace Uniq
{
    public struct ListSelect<T, TR>:IEnumerable<ListSelect<T,TR>.Enumerator, TR>
    {
        public struct Enumerator : IEnumerator<TR>
        {
            private List<T>.Enumerator inner;
            private readonly Func<T, TR> map;

            public Enumerator(List<T>.Enumerator inner, Func<T, TR> map)
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

        private readonly List<T> list;
        private readonly Func<T, TR> map;

        public ListSelect(List<T> list, Func<T, TR> map)
        {
            this.list = list;
            this.map = map;
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(list.GetEnumerator(), map);
        }
    }

    public struct Select<TEnumerable, TEnumerator, T, TR> : IEnumerable<Select<TEnumerable, TEnumerator, T, TR>.Enumerator, TR> 
        where TEnumerable: struct, IEnumerable<TEnumerator, T>
        where TEnumerator: struct, IEnumerator<T>
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
}
