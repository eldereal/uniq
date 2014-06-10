#define SUPPORT_ARRAY
#define SUPPORT_LIST
#define SUPPORT_HASH_SET
#define SUPPORT_LINKED_LIST
#define SUPPORT_STACK
#define SUPPORT_QUEUE
#define SUPPORT_DICTIONARY
#define SUPPORT_SORTED_DICTIONARY
#define SUPPORT_ENUMERABLE

using System.IO;
using System.Text;

namespace codegen
{
    class Program
    {
        private static string[][] Types =
        {
#if SUPPORT_ARRAY
            new[] {"T", "T[]", "ArrayEach", "T", "TR", "TR[]", "ArrayEach", "TR"}, 
#endif

#if SUPPORT_LIST
            new[] {"T", "List<T>", "ListEach", "T", "TR", "List<TR>", "ListEach", "TR"},
#endif

#if SUPPORT_HASH_SET
            new[] {"T", "HashSet<T>", "HashSetEach", "T", "TR", "HashSet<TR>", "HashSetEach", "TR"},
#endif
             
#if SUPPORT_LINKED_LIST
            new[] {"T", "LinkedList<T>", "LinkedListEach", "T", "TR", "LinkedList<TR>", "LinkedListEach", "TR"}, 
#endif

#if SUPPORT_STACK
            new[] {"T", "Stack<T>", "StackEach", "T","TR", "Stack<TR>", "StackEach", "TR"},
#endif

#if SUPPORT_QUEUE
            new[] {"T", "Queue<T>", "QueueEach", "T","TR", "Queue<TR>", "QueueEach", "TR"},
#endif

#if SUPPORT_DICTIONARY
            new[] {"KeyValuePair<TK,TV>", "Dictionary<TK,TV>", "DictionaryEach", "TK,TV","KeyValuePair<TRK,TRV>", "Dictionary<TRK,TRV>", "DictionaryEach", "TRK,TRV"},
#endif

#if SUPPORT_SORTED_DICTIONARY
            new[] {"KeyValuePair<TK,TV>", "SortedDictionary<TK,TV>", "SortedDictionaryEach", "TK,TV","KeyValuePair<TRK,TRV>", "SortedDictionary<TRK,TRV>", "SortedDictionaryEach", "TRK,TRV"},
#endif

#if SUPPORT_ENUMERABLE
            new[] {"T", "IEnumerable<T>", "EnumerableEach", "T","TR", "IEnumerable<TR>", "EnumerableEach", "TR"},
#endif

        };

        private const string EachTemplate =
            "        public static Enumerable<{2}<{3}>, {2}<{3}>.Enumerator, {0}> Each<{3}>(this {1} source){{return new Enumerable<{2}<{3}>, {2}<{3}>.Enumerator, {0}>(new {2}<{3}>(source));}}";

        private const string WhereTemplate =
            "        public static Enumerable<Where<{2}<{3}>, {2}<{3}>.Enumerator, {0}>, Where<{2}<{3}>, {2}<{3}>.Enumerator, {0}>.Enumerator, {0}> Where<{3}>(this {1} source, Func<{0}, bool> predicate) {{ return source.Each().Where(predicate); }}";

        private const string SelectTemplate =
            "        public static Enumerable<Select<{2}<{3}>, {2}<{3}>.Enumerator, {0}, TR>, Select<{2}<{3}>, {2}<{3}>.Enumerator, {0}, TR>.Enumerator, TR> Select<{3}, TR>(this {1} source, Func<{0}, TR> mapFunc){{ return source.Each().Select(mapFunc); }}";

        private const string SelectManySourceTemplate =
            "        public static Enumerable<SelectMany<{2}<{3}>, {2}<{3}>.Enumerator, {0}, TSubEnumerable, TSubEnumerator, TR>, SelectMany<{2}<{3}>, {2}<{3}>.Enumerator, {0}, TSubEnumerable, TSubEnumerator, TR>.Enumerator, TR> SelectMany<{3}, TSubEnumerable, TSubEnumerator, TR>(this {1} source, Func<{0}, Enumerable<TSubEnumerable, TSubEnumerator, TR>> mapper) where TSubEnumerable : struct, IEnumerable<TSubEnumerator, TR> where TSubEnumerator : struct, IEnumerator<TR> {{ return source.Each().SelectMany(mapper); }}";

        private const string SelectManyMapperTemplate =
            "        public static Enumerable <SelectManyIndirect<TEnumerable, TEnumerator, TS, {1}, {2}<{3}>, {2}<{3}>.Enumerator, {0}>, SelectManyIndirect<TEnumerable, TEnumerator, TS, {1}, {2}<{3}>, {2}<{3}>.Enumerator, {0}>. Enumerator, {0}> SelectMany<TEnumerable, TEnumerator, TS, {3}>( this Enumerable<TEnumerable, TEnumerator, TS> source, Func<TS, {1}> mapper) where TEnumerable : struct, IEnumerable<TEnumerator, TS> where TEnumerator : struct, IEnumerator<TS> {{ Func<{1}, Enumerable<{2}<{3}>, {2}<{3}>.Enumerator, {0}>> mapper2; if (Static<Func<{1}, Enumerable<{2}<{3}>, {2}<{3}>.Enumerator, {0}>>>.instance == null) {{ mapper2 = m => m.Each(); Static<Func<{1}, Enumerable<{2}<{3}>, {2}<{3}>.Enumerator, {0}>>>.instance = mapper2; }} else {{ mapper2 = Static<Func<{1}, Enumerable<{2}<{3}>, {2}<{3}>.Enumerator, {0}>>>.instance; }} return new Enumerable <SelectManyIndirect<TEnumerable, TEnumerator, TS, {1}, {2}<{3}>, {2}<{3}>.Enumerator, {0}>, SelectManyIndirect <TEnumerable, TEnumerator, TS, {1}, {2}<{3}>, {2}<{3}>.Enumerator, {0}>.Enumerator, {0}>( new SelectManyIndirect <TEnumerable, TEnumerator, TS, {1}, {2}<{3}>, {2}<{3}>.Enumerator, {0}>(source.Inner, mapper, mapper2)); }}";

        private const string SelectManyBothTemplate =
            "        public static Enumerable<SelectManyIndirect<{2}<{3}>, {2}<{3}>.Enumerator, {0}, {5}, {6}<{7}>, {6}<{7}>.Enumerator, {4}>, SelectManyIndirect<{2}<{3}>, {2}<{3}>.Enumerator, {0}, {5}, {6}<{7}>, {6}<{7}>.Enumerator, {4}>.Enumerator, {4}> SelectMany<{3}, {7}>(this {1} source, Func<{0}, {5}> mapper){{ return source.Each().SelectMany(mapper); }}";

        static void Main(string[] args)
        {
            GenerateSingleTemplate("Each.Gen.cs", args[0], EachTemplate);
            GenerateSingleTemplate("Where.Gen.cs", args[0], WhereTemplate);
            GenerateSingleTemplate("Select.Gen.cs", args[0], SelectTemplate);
            GenerateSingleTemplate("SelectMany.Source.Gen.cs", args[0], SelectManySourceTemplate,
                "SelectMany for system sources and Uniq mapper");
            GenerateSingleTemplate("SelectMany.Mapper.Gen.cs", args[0], SelectManyMapperTemplate,
                "SelectMany for Uniq sources and system mapper");
            GenerateMapTemplate("SelectMany.Both.Gen.cs", args[0], SelectManyBothTemplate,
                "SelectMany for system sources and system mapper");
        }

        static void WriteFile(string filename, string dir, string text)
        {
            using (var s = new StreamWriter(Path.Combine(dir,filename), false))
            {
                s.Write(text);
            }
        }

        static void AppendHead(StringBuilder sb, string describe = null)
        {
            sb.AppendLine("/* This file is auto generated by codegen project. Don't modify this file directly */");
            if (describe != null)
            {
                sb.Append("/* ");
                sb.Append(describe);
                sb.AppendLine(" */");
            }
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("namespace Uniq");
            sb.AppendLine("{");
            sb.AppendLine("    public static partial class Uniq");
            sb.AppendLine("    {");
        }

        static void AppendTail(StringBuilder sb)
        {
            sb.AppendLine("    }");
            sb.AppendLine("}");
        }

        private static void GenerateSingleTemplate(string file, string dir, string template, string describe = null)
        {
            var sb = new StringBuilder();
            AppendHead(sb, describe);
            foreach (var type in Types)
            {
                sb.AppendLine(
                    string.Format(template,
                        type[0], type[1], type[2], type[3]
                        ));
            }
            AppendTail(sb);
            WriteFile(file, dir, sb.ToString());
        }

        private static void GenerateMapTemplate(string file, string dir, string template, string describe = null)
        {
            var sb = new StringBuilder();
            AppendHead(sb, describe);
            foreach (var type in Types)
            {
                foreach (var type2 in Types)
                {
                    sb.AppendLine(
                        string.Format(template,
                            type[0], type[1], type[2], type[3], type2[4], type2[5], type2[6], type2[7]
                            ));
                }
            }
            AppendTail(sb);
            WriteFile(file, dir, sb.ToString());
        }

    }
}
