using System;
using System.Collections.Generic;

namespace MvcSample.Core.Storage
{
    public interface IStore
    {
        void Clear();
        void Apply<T>(Func<T, bool> predicate, Action<T> change);
        int Count<T>();

        T Get<T>(Guid key);
        T Get<T>(int key);
        T Get<T>(string key);

        void Put<T>(Guid key, T data);
        void Put<T>(int key, T data);
        void Put<T>(string key, T data);

        void Delete<T>(Guid key);
        void Delete<T>(int key);
        void Delete<T>(string key);

        IEnumerable<T> Query<T>();
    }
}