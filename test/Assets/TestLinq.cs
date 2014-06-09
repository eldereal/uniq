using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class TestLinq
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
        Test.b.Clear();
        Profiler.BeginSample("ListSelectWhere");
        foreach (var i in Test.a.Select(x => x + 1).Where(x => x % 2 == 0).Select(x => x + 1))
        {
            Test.b.Add(i);
        }
        Profiler.EndSample();
    }
}

