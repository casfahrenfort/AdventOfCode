using System;
using System.Collections.Generic;
using System.Text;

namespace AoC2021
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
    }
}
