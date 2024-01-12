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
}