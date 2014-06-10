using System;

namespace Uniq
{
    public struct SelectMany<TEnumerable, TEnumerator, TS, TSubEnumerable, TSubEnumerator, TR>
        : IEnumerable<SelectMany<TEnumerable, TEnumerator, TS, TSubEnumerable, TSubEnumerator, TR>.Enumerator, TR>
        where TSubEnumerator: struct, IEnumerator<TR>
        where TSubEnumerable: struct, IEnumerable<TSubEnumerator, TR>
        where TEnumerator: struct, IEnumerator<TS>
        where TEnumerable: struct, IEnumerable<TEnumerator, TS>
    {
        public struct Enumerator:IEnumerator<TR>
        {
            private TEnumerator enumerator;
            private TSubEnumerator sub;
            private bool first;
            private readonly Func<TS, Enumerable<TSubEnumerable, TSubEnumerator, TR>> mapper;

            public Enumerator(TEnumerator enumerator, Func<TS, Enumerable<TSubEnumerable, TSubEnumerator, TR>> mapper)
            {
                this.enumerator = enumerator;
                this.mapper = mapper;
                sub = new TSubEnumerator();
                first = true;
            }

            public bool MoveNext()
            {
                if (first)
                {
                    first = false;
                    while (enumerator.MoveNext())
                    {
                        sub = mapper(enumerator.Current).GetEnumerator();
                        if (sub.MoveNext())
                        {
                            return true;
                        }
                    }
                    return false;
                }
                else
                {
                    if (!sub.MoveNext())
                    {
                        while (enumerator.MoveNext())
                        {
                            sub = mapper(enumerator.Current).GetEnumerator();
                            if (sub.MoveNext())
                            {
                                return true;
                            }
                        }
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }

            public TR Current
            {
                get { return sub.Current; }
            }
        }

        private TEnumerable source;
        private readonly Func<TS, Enumerable<TSubEnumerable, TSubEnumerator, TR>> mapper;

        public SelectMany(TEnumerable source, Func<TS, Enumerable<TSubEnumerable, TSubEnumerator, TR>> mapper)
        {
            this.source = source;
            this.mapper = mapper;
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(source.GetEnumerator(), mapper);
        }
    }


    public struct SelectManyIndirect<TEnumerable, TEnumerator, TS, TM, TSubEnumerable, TSubEnumerator, TR>
         : IEnumerable<SelectManyIndirect<TEnumerable, TEnumerator, TS, TM, TSubEnumerable, TSubEnumerator, TR>.Enumerator, TR>
        where TSubEnumerator : struct, IEnumerator<TR>
        where TSubEnumerable : struct, IEnumerable<TSubEnumerator, TR>
        where TEnumerator : struct, IEnumerator<TS>
        where TEnumerable : struct, IEnumerable<TEnumerator, TS>
    {
        public struct Enumerator : IEnumerator<TR>
        {
            private TEnumerator enumerator;
            private TSubEnumerator sub;
            private bool first;
            private readonly Func<TS, TM> mapper;
            private readonly Func<TM, Enumerable<TSubEnumerable, TSubEnumerator, TR>> mapper2; 

            public Enumerator(TEnumerator enumerator, Func<TS, TM> mapper, Func<TM, Enumerable<TSubEnumerable, TSubEnumerator, TR>> mapper2)
            {
                this.enumerator = enumerator;
                this.mapper = mapper;
                this.mapper2 = mapper2;
                sub = new TSubEnumerator();
                first = true;
            }

            public bool MoveNext()
            {
                if (first)
                {
                    first = false;
                    while (enumerator.MoveNext())
                    {
                        sub = mapper2(mapper(enumerator.Current)).GetEnumerator();
                        if (sub.MoveNext())
                        {
                            return true;
                        }
                    }
                    return false;
                }
                else
                {
                    if (!sub.MoveNext())
                    {
                        while (enumerator.MoveNext())
                        {
                            sub = mapper2(mapper(enumerator.Current)).GetEnumerator();
                            if (sub.MoveNext())
                            {
                                return true;
                            }
                        }
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }

            public TR Current
            {
                get { return sub.Current; }
            }
        }

        private TEnumerable source;
        private readonly Func<TS, TM> mapper;
        private readonly Func<TM, Enumerable<TSubEnumerable, TSubEnumerator, TR>> mapper2; 

        public SelectManyIndirect(TEnumerable source, Func<TS, TM> mapper, Func<TM, Enumerable<TSubEnumerable, TSubEnumerator, TR>> mapper2)
        {
            this.source = source;
            this.mapper = mapper;
            this.mapper2 = mapper2;
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(source.GetEnumerator(), mapper, mapper2);
        }
    }



    public static partial class Uniq
    {
        public static Enumerable<SelectMany<TEnumerable, TEnumerator, TS, TSubEnumerable, TSubEnumerator, TR>, SelectMany<TEnumerable, TEnumerator, TS, TSubEnumerable, TSubEnumerator, TR>.Enumerator, TR> SelectMany<TEnumerable, TEnumerator, TS, TSubEnumerable, TSubEnumerator, TR>
            (this Enumerable<TEnumerable,TEnumerator,TS> source, Func<TS, Enumerable<TSubEnumerable, TSubEnumerator, TR>> mapper)
            where TSubEnumerator : struct, IEnumerator<TR>
            where TSubEnumerable : struct, IEnumerable<TSubEnumerator, TR>
            where TEnumerator : struct, IEnumerator<TS>
            where TEnumerable : struct, IEnumerable<TEnumerator, TS>
        {
            return new Enumerable
                <SelectMany<TEnumerable, TEnumerator, TS, TSubEnumerable, TSubEnumerator, TR>,
                    SelectMany<TEnumerable, TEnumerator, TS, TSubEnumerable, TSubEnumerator, TR>.Enumerator, TR>(
                new SelectMany<TEnumerable, TEnumerator, TS, TSubEnumerable, TSubEnumerator, TR>(source.Inner, mapper));
        }
    }
}
