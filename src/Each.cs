using System.Collections.Generic;

namespace Uniq
{
    public struct ListEach<T> : IEnumerable<ListEach<T>.Enumerator, T>
    {
        public struct Enumerator : IEnumerator<T>
        {
            private List<T>.Enumerator enumerator;

            public Enumerator(List<T>.Enumerator enumerator)
            {
                this.enumerator = enumerator;
            }

            public bool MoveNext()
            {
                return enumerator.MoveNext();
            }

            public T Current { get { return enumerator.Current; } }
        }

        private readonly List<T> list;

        public ListEach(List<T> list)
        {
            this.list = list;
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(list.GetEnumerator());
        }
    }

    public struct HashSetEach<T> : IEnumerable<HashSetEach<T>.Enumerator, T>
    {
        public struct Enumerator : IEnumerator<T>
        {
            private HashSet<T>.Enumerator enumerator;

            public Enumerator(HashSet<T>.Enumerator enumerator)
            {
                this.enumerator = enumerator;
            }

            public bool MoveNext()
            {
                return enumerator.MoveNext();
            }

            public T Current { get { return enumerator.Current; } }
        }

        private readonly HashSet<T> set;

        public HashSetEach(HashSet<T> set)
        {
            this.set = set;
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(set.GetEnumerator());
        }
    }

    public struct LinkedListEach<T> : IEnumerable<LinkedListEach<T>.Enumerator, T>
    {
        public struct Enumerator : IEnumerator<T>
        {
            private LinkedList<T>.Enumerator enumerator;

            public Enumerator(LinkedList<T>.Enumerator enumerator)
            {
                this.enumerator = enumerator;
            }

            public bool MoveNext()
            {
                return enumerator.MoveNext();
            }

            public T Current { get { return enumerator.Current; } }
        }

        private readonly LinkedList<T> list;

        public LinkedListEach(LinkedList<T> list)
        {
            this.list = list;
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(list.GetEnumerator());
        }
    }

    public struct StackEach<T> : IEnumerable<StackEach<T>.Enumerator, T>
    {
        public struct Enumerator : IEnumerator<T>
        {
            private Stack<T>.Enumerator enumerator;

            public Enumerator(Stack<T>.Enumerator enumerator)
            {
                this.enumerator = enumerator;
            }

            public bool MoveNext()
            {
                return enumerator.MoveNext();
            }

            public T Current { get { return enumerator.Current; } }
        }

        private readonly Stack<T> list;

        public StackEach(Stack<T> list)
        {
            this.list = list;
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(list.GetEnumerator());
        }
    }

    public struct QueueEach<T> : IEnumerable<QueueEach<T>.Enumerator, T>
    {
        public struct Enumerator : IEnumerator<T>
        {
            private Queue<T>.Enumerator enumerator;

            public Enumerator(Queue<T>.Enumerator enumerator)
            {
                this.enumerator = enumerator;
            }

            public bool MoveNext()
            {
                return enumerator.MoveNext();
            }

            public T Current { get { return enumerator.Current; } }
        }

        private readonly Queue<T> list;

        public QueueEach(Queue<T> list)
        {
            this.list = list;
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(list.GetEnumerator());
        }
    }

    public struct DictionaryEach<TK, TV> : IEnumerable<DictionaryEach<TK, TV>.Enumerator, KeyValuePair<TK, TV>>
    {
        public struct Enumerator : IEnumerator<KeyValuePair<TK, TV>>
        {
            private Dictionary<TK, TV>.Enumerator enumerator;

            public Enumerator(Dictionary<TK, TV>.Enumerator enumerator)
            {
                this.enumerator = enumerator;
            }

            public bool MoveNext()
            {
                return enumerator.MoveNext();
            }

            public KeyValuePair<TK, TV> Current { get { return enumerator.Current; } }
        }

        private readonly Dictionary<TK, TV> dict;

        public DictionaryEach(Dictionary<TK, TV> dict)
        {
            this.dict = dict;
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(dict.GetEnumerator());
        }
    }

    public struct SortedDictionaryEach<TK, TV> : IEnumerable<SortedDictionaryEach<TK, TV>.Enumerator, KeyValuePair<TK, TV>>
    {
        public struct Enumerator : IEnumerator<KeyValuePair<TK, TV>>
        {
            private SortedDictionary<TK, TV>.Enumerator enumerator;

            public Enumerator(SortedDictionary<TK, TV>.Enumerator enumerator)
            {
                this.enumerator = enumerator;
            }

            public bool MoveNext()
            {
                return enumerator.MoveNext();
            }

            public KeyValuePair<TK, TV> Current { get { return enumerator.Current; } }
        }

        private readonly SortedDictionary<TK, TV> dict;

        public SortedDictionaryEach(SortedDictionary<TK, TV> dict)
        {
            this.dict = dict;
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(dict.GetEnumerator());
        }
    }

    public struct EnumerableEach<T> : IEnumerable<EnumerableEach<T>.Enumerator, T>
    {
        public struct Enumerator : IEnumerator<T>
        {
            private System.Collections.Generic.IEnumerator<T> enumerator;

            public Enumerator(System.Collections.Generic.IEnumerator<T> enumerator)
            {
                this.enumerator = enumerator;
            }

            public bool MoveNext()
            {
                return enumerator.MoveNext();
            }

            public T Current { get { return enumerator.Current; } }
        }

        private readonly IEnumerable<T> list;

        public EnumerableEach(IEnumerable<T> list)
        {
            this.list = list;
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(list.GetEnumerator());
        }
    }

    public static partial class Uniq
    {
        /// <summary>
        /// Each() for Uniq Enumerables
        /// </summary>
        public static Enumerable<TEnumerable, TEnumerator, T>
            Each<TEnumerable, TEnumerator, T>(this Enumerable<TEnumerable, TEnumerator, T> enumerable)
            where TEnumerable : struct, IEnumerable<TEnumerator, T>
            where TEnumerator : struct, IEnumerator<T>
        {
            return enumerable;
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
        public static Enumerable<ListEach<T>, ListEach<T>.Enumerator, T> Each<T>(this List<T> list) { return new Enumerable<ListEach<T>, ListEach<T>.Enumerator, T>(new ListEach<T>(list)); }
        public static Enumerable<HashSetEach<T>, HashSetEach<T>.Enumerator, T> Each<T>(this HashSet<T> list) { return new Enumerable<HashSetEach<T>, HashSetEach<T>.Enumerator, T>(new HashSetEach<T>(list)); }
        public static Enumerable<LinkedListEach<T>, LinkedListEach<T>.Enumerator, T> Each<T>(this LinkedList<T> list) { return new Enumerable<LinkedListEach<T>, LinkedListEach<T>.Enumerator, T>(new LinkedListEach<T>(list)); }
        public static Enumerable<StackEach<T>, StackEach<T>.Enumerator, T> Each<T>(this Stack<T> list) { return new Enumerable<StackEach<T>, StackEach<T>.Enumerator, T>(new StackEach<T>(list)); }
        public static Enumerable<QueueEach<T>, QueueEach<T>.Enumerator, T> Each<T>(this Queue<T> list) { return new Enumerable<QueueEach<T>, QueueEach<T>.Enumerator, T>(new QueueEach<T>(list)); }
        public static Enumerable<DictionaryEach<TK, TV>, DictionaryEach<TK, TV>.Enumerator, KeyValuePair<TK, TV>> Each<TK, TV>(this Dictionary<TK, TV> list) { return new Enumerable<DictionaryEach<TK, TV>, DictionaryEach<TK, TV>.Enumerator, KeyValuePair<TK, TV>>(new DictionaryEach<TK, TV>(list)); }
        public static Enumerable<SortedDictionaryEach<TK, TV>, SortedDictionaryEach<TK, TV>.Enumerator, KeyValuePair<TK, TV>> Each<TK, TV>(this SortedDictionary<TK, TV> list) { return new Enumerable<SortedDictionaryEach<TK, TV>, SortedDictionaryEach<TK, TV>.Enumerator, KeyValuePair<TK, TV>>(new SortedDictionaryEach<TK, TV>(list)); }
        public static Enumerable<EnumerableEach<T>, EnumerableEach<T>.Enumerator, T> Each<T>(this IEnumerable<T> list) { return new Enumerable<EnumerableEach<T>, EnumerableEach<T>.Enumerator, T>(new EnumerableEach<T>(list)); }
    }
}
