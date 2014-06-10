using System.Collections.Generic;
using UnityEngine;
using Uniq;

public class Test : MonoBehaviour {

    public static List<int> List1 = new List<int>
    {
        1,2,3,4,5
    };

    public static List<int> List2 = new List<int>(10);

    public static List<int> List3 = new List<int>(10);

    public static Dictionary<int, int> c = new Dictionary<int, int>
    {
        {1, 2},
        {2, 4},
        {3, 6},
        {4, 8},
        {5, 10}
    };

    public static Dictionary<int, int> d = new Dictionary<int, int>(10);
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Profiler.BeginSample("Foreach");
	    ListForeach();
	    UniqListForeach();
        DictionaryForeach();
        UniqDictionaryForeach();
        Profiler.EndSample();

        Profiler.BeginSample("Uniq");
	    TestUniq.ListSelectWhere();
        TestUniq.DictionarySelectWhere();
        TestUniq.ListSelectManyWithCache();
        Profiler.EndSample();

        Profiler.BeginSample("LinQ");
        TestLinq.ListSelectWhere();
        TestLinq.DictionarySelectWhere();
        TestLinq.ListSelectManyWithCache();
        Profiler.EndSample();

	}

    private void ListForeach()
    {
        List2.Clear();
        Profiler.BeginSample("List");
        foreach (var p in List1)
        {
            List2.Add(p);
        }
        Profiler.EndSample();
    }

    private void UniqListForeach()
    {
        List2.Clear();
        Profiler.BeginSample("List.Each");
        foreach (var p in List1.Each())
        {
            List2.Add(p);
        }
        Profiler.EndSample();
    }

    private void DictionaryForeach()
    {
        d.Clear();
        Profiler.BeginSample("Dictionary");
        foreach (var p in c)
        {
            d.Add(p.Key, p.Value);
        }
        Profiler.EndSample();
    }

    private void UniqDictionaryForeach()
    {
        d.Clear();
        Profiler.BeginSample("Dictionary.Each");
        foreach (var p in c.Each())
        {
            d.Add(p.Key, p.Value);
        }
        Profiler.EndSample();
    }


    
}
