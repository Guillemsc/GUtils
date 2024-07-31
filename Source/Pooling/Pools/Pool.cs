using System;
using System.Collections.Generic;

namespace GUtils.Pooling
{
    public class Pool<T>
    {
        private readonly List<T> _available = new List<T>();
        private readonly List<T> _all = new List<T>();
        
        private readonly Func<T> _createAction;
        
        public event Action<T> GetAction;
        public event Action<T> ReturnAction;

        public Pool(Func<T> createAction)
        {
            _createAction = createAction;
        }

        public T Get(out bool isNew)
        {
            T item;
            isNew = false;
            
            if (_available.Count > 0)
            {
                item = _available[0];
                _available.RemoveAt(0);
            }
            else
            {
                item = _createAction.Invoke();
                _all.Add(item);
                isNew = true;
            }
            
            GetAction?.Invoke(item);

            return item;
        }

        public T Get() => Get(out _);

        public Pooled<T> GetPooled()
        {
            T item = Get();
            return new Pooled<T>(this, item);
        }

        public void Return(T item)
        {
            bool isOwner = _all.Contains(item);

            if (!isOwner)
            {
                return;
            }

            ReturnFast(item);
        }

        public void ReturnAll()
        {
            foreach (T item in _all)
            {
                ReturnFast(item);
            }
        }

        void ReturnFast(T item)
        {
            bool alreadyAvailable = _available.Contains(item);

            if (alreadyAvailable)
            {
                return;
            }
            
            ReturnAction?.Invoke(item);
            
            _available.Add(item);
        }
    }
}