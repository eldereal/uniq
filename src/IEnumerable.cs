namespace Uniq
{
    /// <summary>
    /// This a struct-based enumerable implement, it returns a struct to avoid heap memory alloc and boxing.
    /// </summary>
    public interface IEnumerable<TEnumerator, TElem>
        where TEnumerator: struct, IEnumerator<TElem>
    {
        TEnumerator GetEnumerator();
    }
}
