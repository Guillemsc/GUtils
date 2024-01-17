using System;
using GUtils.Di.Builder;
using GUtils.Di.Container;
using GUtils.Pooling.Pools;

namespace GUtils.Pooling.Extensions;

public static class PoolsDiExtensions
{
    public static IDiBindingActionBuilder<T> FromPool<T>(
        this IDiBindingBuilder<T> builder,
        ObjectPool<T> pool,
        Action<IDiResolveContainer, T> init
        )
    {
        return builder
            .FromFunction(c =>
            {
                T item = pool.Get();
                init.Invoke(c, item);
                return item;
            })
            .WhenDispose(pool.Return);
    }

    public static IDiBindingActionBuilder<T> FromAutoPool<T>(
        this IDiBindingBuilder<T> builder,
        Action<IDiResolveContainer, T> init
        ) where T : class
    {
        return builder
            .FromFunction(c =>
            {
                T item = AutoObjectsPool.Get<T>();
                init.Invoke(c, item);
                return item;
            })
            .WhenDispose(AutoObjectsPool.Return);
    }
    
    public static IDiBindingActionBuilder<T> FromAutoPool<T>(
        this IDiBindingBuilder<T> builder
    ) where T : class
    {
        return builder
            .FromFunction(c =>
            {
                T item = AutoObjectsPool.Get<T>();
                return item;
            })
            .WhenDispose(AutoObjectsPool.Return);
    }
}