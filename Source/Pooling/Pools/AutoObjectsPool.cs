using System;
using System.Collections.Generic;

namespace GUtils.Pooling.Pools;

public sealed class AutoObjectsPool
{
    static readonly AutoObjectsPool _instance = new();
    
    readonly Dictionary<Type, ObjectPool<object>> _pools = new();

    AutoObjectsPool() { }

    public static T Get<T>() where T : class
        => _instance.GetInternal<T>();
    
    public static void Return<T>(T item) where T : class
        => _instance.ReturnInternal<T>(item);
    
    T GetInternal<T>() where T : class
    {
        Type type = typeof(T);

        bool poolFound = _pools.TryGetValue(type, out ObjectPool<object>? pool);

        if (!poolFound)
        {
            pool = new ObjectPool<object>(() => Activator.CreateInstance<T>()!);
            _pools.Add(type, pool);
        }

        return (T)pool!.Get();
    }
    
    void ReturnInternal<T>(T item) where T : class
    {
        Type type = typeof(T);

        bool poolFound = _pools.TryGetValue(type, out ObjectPool<object>? pool);

        if (!poolFound)
        {
            return;
        }

        pool!.Return(item!);
    }
}