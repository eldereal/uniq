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
            var b = a.Select(x => x + 1).Where(x => x%2 == 0).Select(x => x + 1);
            foreach (var i in b)
            {
                Console.WriteLine(i);
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
