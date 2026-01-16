using System;
using System.Collections.Concurrent;

namespace CodeGenerator
{
    public static class SimpleCache
    {
        private static readonly ConcurrentDictionary<string, object> Store = new ConcurrentDictionary<string, object>();

        public static T GetOrAdd<T>(string key, Func<T> factory)
        {
            if (Store.TryGetValue(key, out var existing))
            {
                return (T)existing;
            }

            var value = factory();
            Store[key] = value!;
            return value;
        }
    }
}
