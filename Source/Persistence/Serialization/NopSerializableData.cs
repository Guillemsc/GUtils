using System;
using System.Threading;
using System.Threading.Tasks;

namespace GUtils.Persistence.Serialization
{
    public sealed class NopSerializableData<T> : ISerializableData<T> where T : class
    {
        public event Action<T>? OnSave;
        public event Action<T>? OnLoad;

        public T Data { get; }
        
        public NopSerializableData(T data)
        {
            Data = data;
        }

        public Task Save(CancellationToken cancellationToken) => Task.CompletedTask;
        public Task Load(CancellationToken cancellationToken) => Task.CompletedTask;
        public void SaveAsync() { }
        public void LoadAsync() { }
    }
}
