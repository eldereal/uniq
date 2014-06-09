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


        /*
         * Auto-generated        
         * Text:
            List<T> T ListEach
            HashSet<T> T HashSetEach
            LinkedList<T> T LinkedListEach
            Stack<T> T StackEach
            Queue<T> T QueueEach
            Dictionary<TK,TV> KeyValuePair<TK,TV> DictionaryEach
            SortedDictionary<TK,TV> KeyValuePair<TK,TV> SortedDictionaryEach
            IEnumerable<T> T EnumerableEach
        
         * Regex:
            (\w+)<([\w,]+)> ([\w,<>]+) (\w+)
        
         * Replace:
            public static Enumerable<Select<$4<$2>, $4<$2>.Enumerator, $3, TR>, Select<$4<$2>, $4<$2>.Enumerator, $3, TR>.Enumerator, TR> Select<$2, TR>(this $1<$2> list, Func<$3, TR> mapFunc){ return list.Each().Select(mapFunc); }
         */
        public static Enumerable<Select<ListEach<T>, ListEach<T>.Enumerator, T, TR>, Select<ListEach<T>, ListEach<T>.Enumerator, T, TR>.Enumerator, TR> Select<T, TR>(this List<T> list, Func<T, TR> mapFunc) { return list.Each().Select(mapFunc); }
        public static Enumerable<Select<HashSetEach<T>, HashSetEach<T>.Enumerator, T, TR>, Select<HashSetEach<T>, HashSetEach<T>.Enumerator, T, TR>.Enumerator, TR> Select<T, TR>(this HashSet<T> list, Func<T, TR> mapFunc) { return list.Each().Select(mapFunc); }
        public static Enumerable<Select<LinkedListEach<T>, LinkedListEach<T>.Enumerator, T, TR>, Select<LinkedListEach<T>, LinkedListEach<T>.Enumerator, T, TR>.Enumerator, TR> Select<T, TR>(this LinkedList<T> list, Func<T, TR> mapFunc) { return list.Each().Select(mapFunc); }
        public static Enumerable<Select<StackEach<T>, StackEach<T>.Enumerator, T, TR>, Select<StackEach<T>, StackEach<T>.Enumerator, T, TR>.Enumerator, TR> Select<T, TR>(this Stack<T> list, Func<T, TR> mapFunc) { return list.Each().Select(mapFunc); }
        public static Enumerable<Select<QueueEach<T>, QueueEach<T>.Enumerator, T, TR>, Select<QueueEach<T>, QueueEach<T>.Enumerator, T, TR>.Enumerator, TR> Select<T, TR>(this Queue<T> list, Func<T, TR> mapFunc) { return list.Each().Select(mapFunc); }
        public static Enumerable<Select<DictionaryEach<TK, TV>, DictionaryEach<TK, TV>.Enumerator, KeyValuePair<TK, TV>, TR>, Select<DictionaryEach<TK, TV>, DictionaryEach<TK, TV>.Enumerator, KeyValuePair<TK, TV>, TR>.Enumerator, TR> Select<TK, TV, TR>(this Dictionary<TK, TV> list, Func<KeyValuePair<TK, TV>, TR> mapFunc) { return list.Each().Select(mapFunc); }
        public static Enumerable<Select<SortedDictionaryEach<TK, TV>, SortedDictionaryEach<TK, TV>.Enumerator, KeyValuePair<TK, TV>, TR>, Select<SortedDictionaryEach<TK, TV>, SortedDictionaryEach<TK, TV>.Enumerator, KeyValuePair<TK, TV>, TR>.Enumerator, TR> Select<TK, TV, TR>(this SortedDictionary<TK, TV> list, Func<KeyValuePair<TK, TV>, TR> mapFunc) { return list.Each().Select(mapFunc); }
        public static Enumerable<Select<EnumerableEach<T>, EnumerableEach<T>.Enumerator, T, TR>, Select<EnumerableEach<T>, EnumerableEach<T>.Enumerator, T, TR>.Enumerator, TR> Select<T, TR>(this IEnumerable<T> list, Func<T, TR> mapFunc) { return list.Each().Select(mapFunc); }

    }
}
