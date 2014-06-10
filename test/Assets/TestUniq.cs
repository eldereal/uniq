using System.Collections.Generic;
using UnityEngine;
using Uniq;

public static class TestUniq
{

    public static void DictionarySelectWhere()
    {
        Test.d.Clear();
        Profiler.BeginSample("DictionarySelectWhere");
        foreach (
            var p in Test.d.Where(x => x.Key < 3 || x.Value > 6).Select(x => new KeyValuePair<int, int>(x.Key, x.Key + x.Value)))
        {
            Test.d.Add(p.Key, p.Value);
        }
        Profiler.EndSample();
    }

    public static void ListSelectWhere()
    {
        Test.List2.Clear();
        Profiler.BeginSample("ListSelectWhere");
        foreach (var i in Test.List1.Select(x => x + 1).Where(x => x % 2 == 0).Select(x => x + 1))
        {
            Test.List2.Add(i);
        }
        Profiler.EndSample();
    }

    public static void ListSelectManyWithCache()
    {
        Test.List2.Clear();
        Profiler.BeginSample("ListSelectManyWithCache");
        foreach (var i in Test.List1.Where(x => x % 2 != 0).SelectMany(i =>
        {
            Test.List3.Clear();
            Test.List3.Add(i);
            Test.List3.Add(i + 10);
            return Test.List3;
        }))
        {
            Test.List2.Add(i);
        }
        Profiler.EndSample();
    }
}

