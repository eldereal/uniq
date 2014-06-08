namespace Uniq
{
    /// <summary>
    /// Unity (Mono) compiler has a bug which causes a GC Alloc when a struct type
    /// with IDisposable is used in a using block. 
    /// This interface is similar to System.Collection.Generic.IEnumerator&lt;T&gt; 
    /// but doesn't super IDisposable to prevent the bug. 
    /// </summary>
    public interface IEnumerator<T>
    {
        bool MoveNext();
        T Current { get; }
    }
}
