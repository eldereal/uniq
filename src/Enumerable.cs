namespace Uniq
{
    /// <summary>
    /// This struct is used to infer generic types. 
    /// Since C# doesn't support infer generic types from type constraints
    /// this struct can give these type from its generic type parameters.
    /// </summary>
    public struct Enumerable<TEnumerable, TEnumerator, T>:IEnumerable<TEnumerator, T> 
        where TEnumerable : struct , IEnumerable<TEnumerator, T>
        where TEnumerator : struct, IEnumerator<T>
    {
        public TEnumerable Inner { get; private set; }

        public Enumerable(TEnumerable inner) : this()
        {
            Inner = inner;
        }

        public TEnumerator GetEnumerator()
        {
            return Inner.GetEnumerator();
        }
    }
}
