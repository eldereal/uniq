using System;
using System.Collections.Generic;
using Uniq;

namespace test
{
    static class TestSelect
    {
        public static void TestList()
        {
            var a = new List<int>
            {
                1,2,3,4,5
            };
            var d = new Dictionary<int, int>
            {
                {1, 2},
                {2, 4},
                {3, 7},
                {4, 8},
                {5, 10}
            };

            Console.WriteLine("a.Select(x => x + 1).Where(x => x%2 == 0).Select(x => x + 1)");
            var b = a.Select(x => x + 1).Where(x => x%2 == 0).Select(x => x + 1);
            foreach (var i in b)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine("d.Where(x => x.Key*2 == x.Value)");
            foreach (var p in d.Select(x=>new {a=x.Key, b=x.Value}).Where(x => x.a*2 == x.b))
            {
                Console.WriteLine(p.a + ", " + p.b);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            TestSelect.TestList();
        }
    }
}
