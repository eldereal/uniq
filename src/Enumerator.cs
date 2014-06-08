namespace Uniq
{
    /// <summary>
    /// This struct is used to infer generic types. 
    /// Since C# doesn't support infer generic types from type constraints
    /// this struct can give these type from its generic type parameters.
    /// </summary>
    public struct Enumerator<TEnumerator, T>:IEnumerator<T>
        where TEnumerator:struct, IEnumerator<T>
    {
        private TEnumerator inner;

        public Enumerator(TEnumerator inner)
        {
            this.inner = inner;
        }

        public bool MoveNext()
        {
            return inner.MoveNext();
        }

        public T Current
        {
            get { return inner.Current; }
        }
    }
}