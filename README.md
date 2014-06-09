#Uniq: LinQ for Unity3D 

Linq (Language Intergrated Query) is an awesome feature in C#. However, it is considered harmful in Unity3D game developing, especially when targeting mobile platforms. As Unity3D uses Mono as its runtime, Linq has the following two major disadvantages:
  
  * Mono's mobile framework doesn't has a generational garbage collection. Linq produces lots of memory garbage and tremendously harms the GC performance.
  * In some platform (iOS), Mono must run in fully AOT mode. But some features in Linq are incompatiable with this mode which causes runtime exeptions.

Uniq is born to solve these problems. It uses structure-based enumerators to avoid allocating small objects and in most cases Uniq makes **zero** allocations.

##Use Uniq

It is very easy to use Uniq. Just replace `System.Linq` namespace with `Uniq`.

    //using System.Linq;
    using Uniq;

Now you can write Linq statements as usual.

What's more, `foreach` statement is widely used with Linq. However Unity3D's compiler has a bug which makes unwanted temporary objects. So we provide an extension method called `Each()` to fix this bug. Of course, use `foreach` to iterate any Uniq result won't trigger the bug too.

    List<int> a = ...;
    foreach(var i in a){ ... }         //24 bytes GC Alloc
    foreach(var i in a.Each()){ ... }  //No GC Alloc
    foreach(var i in a.Where(x => x > 0)){ ... }  //No GC Alloc

##Use Uniq Better

> **NOTICE**: Uniq is a domain-specific solution and is optimized for certain cases. You need to know a little about the internals to prevent unsuitable usages.

The core concept of Uniq is generic struct types. To avoid boxing, every extending method and every type is defined with actual type rather than interfaces.
For example, there is a method `Where<T>(this IEnumerable<T>, Func<T, bool>)` in Linq. But when calling it with any struct, it will be boxed which allocates a temporary object.
In Uniq I define this method as `Where<TEnumerable, T>(this TEnumerable, Func<T, bool>)`. The first argument is a generic argument and can pass the actual struct without boxing.

So you should know these facts of Uniq:

> Uniq has much more types and methods than Linq which may increase the size of the code, especially run in fully AOT mode.

For example, consider the code `list.Where(test1).Where(test2)`. In Linq it needs just one "where for an enumerable" method. But in Uniq it needs two methods, one is "where for a list" and the other is "where for a where result of a list".  
    
> Uniq is not designed for speed. It is designed to allocate fewer temporary objects.

So Uniq methods may be less efficient than Linq methods. If you need to process large amount of data, Uniq is not a best choice.

> Uniq is designed to use with `var` and `foreach` keywords.   

In most cases you should use Uniq inside a `foreach` statement. If you must store the result, you should store it in `var` variables because the actual type name will be longer than your expectation. 

If you are paid by bytes you've written, I think the follwing type name helps you get a higher paid.

    Enumerable<Select<Where<ListEach<int>, ListEach<int>.Enumerator, int>, Where<ListEach<int>, ListEach<int>.Enumerator, int>.Enumerator, int, int>, Select<Where<ListEach<int>, ListEach<int>.Enumerator, int>, Where<ListEach<int>, ListEach<int>.Enumerator, int>.Enumerator, int, int>.Enumerator, int>

Don't store Uniq result in any interface variable. All Uniq results should be struct and store it in interface variables will lead to boxing.

If you must pass Uniq results between classes, the following method stub will help you. Not only because it avoid writing the long long type names, but also because it can receive any Uniq results. You should be aware that different query results in different types in Uniq. This method stub is a common pattern to receive any Uniq result.

    public static void
        SomeMethod<TL, TI, T>(Uniq.Enumerable<TL, TI, T> enumerable)
        where TL : struct, Uniq.IEnumerable<TI, T>
        where TI : struct, Uniq.IEnumerator<T>
    {
        foreach(var i in enumerable){
            ...
        }
    }

> Uniq works with these collections (All in `System.Collections.Generic`): 
> `List<T>`, `HashSet<T>`, `LinkedList<T>`, `Stack<T>`, `Queue<T>`, `Dictionary<TK,TV>`, `SortedDictionary<TK,TV>`, `IEnumerable<T>`.

> If you use interface type `IEnumerable<T>`, each enumeration comes with allocating an `IEnumerator<T>` object. So don't use interface types unless you cannot avoid it.

##Polling Objects
In consideration

##Configurable Obsolate
In consideration

##Developing Progress

###Basic Feature
  * Aggregate
  * All
  * Any
  * AsEnumerable
  * Average
  * Cast
  * Concat
  * Contains
  * Count
  * DefaultIfEmpty
  * Distinct
  * ElementAt
  * ElementAtOrDefault
  * Empty
  * Except
  * First
  * FirstOrDefault
  * GroupBy
  * GroupJoin
  * Intersect
  * Join
  * Last
  * LastOrDefault
  * LongCount
  * Max
  * Min
  * OfType
  * OrderBy
  * OrderByDescending
  * Range
  * Repeat
  * Reverse
  * **Select**: done
  * SelectMany
  * SequenceEqual
  * Single
  * SingleOrDefault
  * Skip
  * SkipWhile
  * Sum
  * Take
  * TakeWhile
  * ThenBy
  * ThenByDescending
  * ToArray
  * ToDictionary
  * ToList
  * ToLookup
  * Union
  * **Where**: done
  * Zip

###More Features
  * **Each**: done