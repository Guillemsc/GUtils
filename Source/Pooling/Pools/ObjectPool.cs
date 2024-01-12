using System;
using System.Collections.Generic;
using GUtils.Pooling.Objects;

namespace GUtils.Pooling.Pools;

public sealed class ObjectPool<T>
{
    readonly List<T> _avaliablePool = new();
    readonly List<T> _used = new();
    
    readonly Func<T>? CreateAction;
    
    public ObjectPool(
        Func<T>? createAction
        )
    {
        CreateAction = createAction;
    }

    public T Get()
    {
        if (_avaliablePool.Count <= 0)
        {
            Resize();
        }

        T pooledObject = _avaliablePool[0];
        _avaliablePool.RemoveAt(0);
        _used.Add(pooledObject);
        
        if (pooledObject is IGettablePooledObject gettablePooledObject)
        {
            gettablePooledObject.PooledObjectGetted();
        }
        
        return pooledObject;
    }

    public void Return(T item)
    {
        bool wasUsed = _used.Remove(item);

        if (!wasUsed)
        {
            return;
        }
        
        _avaliablePool.Add(item);
        
        if (item is IReturnablePooledObject returnablePooledObject)
        {
            returnablePooledObject.PooledObjectReturned();
        }
    }

    void Resize()
    {
        T newObject = CreateAction!.Invoke();

        if (newObject is ICreatablePooledObject creatablePooledObject)
        {
            creatablePooledObject.PooledObjectCreated();
        }
        
        _avaliablePool.Add(newObject);
    }
}