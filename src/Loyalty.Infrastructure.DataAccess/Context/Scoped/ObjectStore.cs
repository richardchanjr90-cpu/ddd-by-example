using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.DataAccess.Context.Scoped
{
    public class ObjectStore : IDisposable
    {
        private static readonly Dictionary<string, object> ObjectDictionary = new Dictionary<string, object>();

        public void Save<T>(int hashCode, T context)
        {
            string key = hashCode + typeof(T).FullName;

            if (!ObjectDictionary.ContainsKey(key))
            {
                ObjectDictionary.Add(key, context);
            }
        }

        public T GetOrNull<T>(int hashCode)
        {
            string key = hashCode + typeof(T).FullName;

            if (ObjectDictionary.ContainsKey(key))
            {
                return (T)ObjectDictionary[key];
            }

            return default;
        }

        //public void RemoveAndDispose(int hashCode)
        //{
        //    if (ObjectDictionary.ContainsKey(hashCode))
        //    {
        //        var context = ObjectDictionary[hashCode];
        //        context?.Dispose();
        //    }
        //}
        public void Dispose()
        {
            foreach (var context in ObjectDictionary.Values)
            {
                if (context is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
        }
    }
}
