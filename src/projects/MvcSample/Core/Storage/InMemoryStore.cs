using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MvcSample.Core.Storage
{
    public class InMemoryStore : IStore
    {
        protected IFormatProvider KeyFormatProvider { get; private set; }
        protected ConcurrentDictionary<Type, ConcurrentDictionary<string, dynamic>> State { get; private set; }

        public InMemoryStore()
        {
            KeyFormatProvider = CultureInfo.InvariantCulture;
            State = new ConcurrentDictionary<Type, ConcurrentDictionary<string, dynamic>>();
        }

        public virtual void Clear()
        {
            foreach (var dic in State.Values)
            {
                dic.Clear();
            }

            State.Clear();
        }

        public virtual void Apply<T>(Func<T, bool> predicate, Action<T> change)
        {
            //No need to snapshot. Running in-memory.
            var selection = GetStoreFor(typeof (T))
                .Values.OfType<T>()
                .Where(predicate);

            foreach (var item in selection)
                change(item);
        }

        public virtual int Count<T>()
        {
            return GetStoreFor(typeof (T)).Count;
        }

        public virtual T Get<T>(Guid key)
        {
            return Get<T>(GenerateStoreKey(key));
        }

        public virtual T Get<T>(int key)
        {
            return Get<T>(GenerateStoreKey(key));
        }

        public virtual T Get<T>(string key)
        {
            dynamic value;

            return GetStoreFor(typeof(T)).TryGetValue(key, out value) 
                ? (T)value
                : default(T);
        }

        public virtual void Put<T>(Guid key, T data)
        {
            Put(GenerateStoreKey(key), data);
        }

        public void Put<T>(int key, T data)
        {
            Put(GenerateStoreKey(key), data);
        }

        public virtual void Put<T>(string key, T data)
        {
            var store = GetStoreFor(typeof (T));
            store.AddOrUpdate(key, data, (k, existing) => data);
        }

        public virtual void Delete<T>(Guid key)
        {
            Delete<T>(GenerateStoreKey(key));
        }

        public virtual void Delete<T>(int key)
        {
            Delete<T>(GenerateStoreKey(key));
        }

        public virtual void Delete<T>(string key)
        {
            var store = GetStoreFor(typeof (T));
            
            dynamic value;
            store.TryRemove(key, out value);
        }

        public virtual IEnumerable<T> Query<T>()
        {
            var store = GetStoreFor(typeof (T));

            return store.Values.OfType<T>();
        }

        protected virtual string GenerateStoreKey(Guid key)
        {
            return key.ToString("n");
        }

        protected virtual string GenerateStoreKey(int key)
        {
            return key.ToString(KeyFormatProvider);
        }

        protected virtual ConcurrentDictionary<string, dynamic> GetStoreFor(Type type)
        {
            return State.GetOrAdd(type, new ConcurrentDictionary<string, dynamic>());
        }
    }
}