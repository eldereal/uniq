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
            public static Enumerable<$4<$2>, $4<$2>.Enumerator, $3> Each<$2>(this $1<$2> list){return new Enumerable<$4<$2>, $4<$2>.Enumerator, $3>(new $4<$2>(list));}
         */

        public static Enumerable<Where<ListEach<T>, ListEach<T>.Enumerator, T>, Where<ListEach<T>, ListEach<T>.Enumerator, T>.Enumerator, T> Where<T>(this List<T> list, Func<T, bool> predicate) { return list.Each().Where(predicate); }
        public static Enumerable<Where<HashSetEach<T>, HashSetEach<T>.Enumerator, T>, Where<HashSetEach<T>, HashSetEach<T>.Enumerator, T>.Enumerator, T> Where<T>(this HashSet<T> list, Func<T, bool> predicate) { return list.Each().Where(predicate); }
        public static Enumerable<Where<LinkedListEach<T>, LinkedListEach<T>.Enumerator, T>, Where<LinkedListEach<T>, LinkedListEach<T>.Enumerator, T>.Enumerator, T> Where<T>(this LinkedList<T> list, Func<T, bool> predicate) { return list.Each().Where(predicate); }
        public static Enumerable<Where<StackEach<T>, StackEach<T>.Enumerator, T>, Where<StackEach<T>, StackEach<T>.Enumerator, T>.Enumerator, T> Where<T>(this Stack<T> list, Func<T, bool> predicate) { return list.Each().Where(predicate); }
        public static Enumerable<Where<QueueEach<T>, QueueEach<T>.Enumerator, T>, Where<QueueEach<T>, QueueEach<T>.Enumerator, T>.Enumerator, T> Where<T>(this Queue<T> list, Func<T, bool> predicate) { return list.Each().Where(predicate); }
        public static Enumerable<Where<DictionaryEach<TK, TV>, DictionaryEach<TK, TV>.Enumerator, KeyValuePair<TK, TV>>, Where<DictionaryEach<TK, TV>, DictionaryEach<TK, TV>.Enumerator, KeyValuePair<TK, TV>>.Enumerator, KeyValuePair<TK, TV>> Where<TK, TV>(this Dictionary<TK, TV> list, Func<KeyValuePair<TK, TV>, bool> predicate) { return list.Each().Where(predicate); }
        public static Enumerable<Where<SortedDictionaryEach<TK, TV>, SortedDictionaryEach<TK, TV>.Enumerator, KeyValuePair<TK, TV>>, Where<SortedDictionaryEach<TK, TV>, SortedDictionaryEach<TK, TV>.Enumerator, KeyValuePair<TK, TV>>.Enumerator, KeyValuePair<TK, TV>> Where<TK, TV>(this SortedDictionary<TK, TV> list, Func<KeyValuePair<TK, TV>, bool> predicate) { return list.Each().Where(predicate); }
        public static Enumerable<Where<EnumerableEach<T>, EnumerableEach<T>.Enumerator, T>, Where<EnumerableEach<T>, EnumerableEach<T>.Enumerator, T>.Enumerator, T> Where<T>(this IEnumerable<T> list, Func<T, bool> predicate) { return list.Each().Where(predicate); }

        
    }
}
