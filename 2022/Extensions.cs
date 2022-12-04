using System.Collections.Generic;
using System.Linq;

namespace AoC2022
{
    public static class Extensions
    {
        public static void AddOrAssign<TKey, TValue>(this Dictionary<TKey, TValue> source, TKey key, TValue value)
        {
            if (source.ContainsKey(key))
                source[key] = value;
            else
                source.Add(key, value);
        }

        public static void InitializeOrIncrement<TKey>(this Dictionary<TKey, int> source, TKey key, int value = 1)
        {
            if (source.ContainsKey(key))
                source[key] += value;
            else
                source.Add(key, value);
        }

        public static IEnumerable<IEnumerable<T>> Sublists<T>(this IEnumerable<T> source, int sublistSize)
        {
            return source.Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / sublistSize)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }
    }
}
