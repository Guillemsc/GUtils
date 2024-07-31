namespace GUtils.Pooling
{
    public readonly struct Pooled<T>
    {
        private readonly Pool<T> _pool;
        
        public readonly T Value;
        
        public Pooled(Pool<T> pool, T value)
        {
            _pool = pool;
            Value = value;
        }

        public void Return()
        {
            _pool.Return(Value);
        }
    }
}