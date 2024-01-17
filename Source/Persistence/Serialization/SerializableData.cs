using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using GUtils.DiscriminatedUnions;
using GUtils.Extensions;
using GUtils.Json.Extensions;
using GUtils.Logging.Enums;
using GUtils.Logging.Loggers;
using GUtils.Optionals;
using GUtils.Persistence.Migrations;
using GUtils.Persistence.StorageMethods;
using GUtils.Types;

namespace GUtils.Persistence.Serialization
{
    /// <inheritdoc />
    public sealed class SerializableData<T> : ISerializableData<T> where T : class
    {
        readonly IPersistenceStorageMethod _persistenceStorageMethod;
        readonly string _localPath;
        readonly Func<T> _createDataAction;
        readonly ILogger _logger;
        readonly IMigration<T>[] _migrations;

        T? _data;

        public event Action<T>? OnSave;
        public event Action<T>? OnLoad;

        public T Data => GetData();

        public SerializableData(
            IPersistenceStorageMethod persistenceStorageMethod, 
            string localPath, 
            Func<T> createDataAction, 
            ILogger logger
            )
        {
            _persistenceStorageMethod = persistenceStorageMethod;
            _localPath = localPath;
            _createDataAction = createDataAction;
            _logger = logger;
            _migrations = Array.Empty<IMigration<T>>();
        }

        public SerializableData(
            IPersistenceStorageMethod persistenceStorageMethod, 
            string localPath, 
            Func<T> createDataAction, 
            ILogger logger, 
            params IMigration<T>[] migrations
            )
        {
            _persistenceStorageMethod = persistenceStorageMethod;
            _localPath = localPath;
            _createDataAction = createDataAction;
            _logger = logger;
            _migrations = migrations;
        }

        public async Task Save(CancellationToken cancellationToken)
        {
            OnSave?.Invoke(Data);
            
            Stopwatch saveStopWatch = Stopwatch.StartNew();

            OneOf<string, ErrorMessage> serializeResult = JsonExtensions.Serialize(_data);

            if (serializeResult.HasSecond)
            {
                _logger.Log(
                    LogType.Info,
                    "{0} {1} could not be saved: {2}.",
                    typeof(SerializableData<T>).Name,
                    typeof(T).Name,
                    serializeResult.UnsafeGetSecond()
                );
                return;
            }

            string dataString = serializeResult.UnsafeGetFirst();

            Optional<ErrorMessage> optionalError = await _persistenceStorageMethod.Save(_localPath, dataString, cancellationToken);

            saveStopWatch.Stop();

            bool hasOptionalError = optionalError.TryGet(out ErrorMessage errorMessage);

            if (hasOptionalError)
            {
                _logger.Log(
                    LogType.Info,
                    "{0} {1} could not be saved: {2}.",
                    typeof(SerializableData<T>).Name,
                    typeof(T).Name,
                    errorMessage
                );
                return;
            }

            _logger.Log(
                LogType.Info,
                "{0} {1} saved ({2}ms) \n{3}",
                typeof(SerializableData<T>).Name,
                typeof(T).Name,
                saveStopWatch.ElapsedMilliseconds,
                Data
            );
        }

        public async Task Load(CancellationToken cancellationToken)
        {
            Stopwatch loadStopWatch = Stopwatch.StartNew();

            OneOf<string, ErrorMessage> optionalLoadedData = await _persistenceStorageMethod.Load(_localPath, cancellationToken);

            bool hasError = optionalLoadedData.TryGetSecond(out ErrorMessage errorMessage);

            if (hasError)
            {
                _logger.Log(
                    LogType.Info,
                    "{0} {1} could not be loaded: {2}. Creating with default values",
                    typeof(SerializableData<T>).Name,
                    typeof(T).Name,
                    errorMessage
                );
                
                OnLoad?.Invoke(Data);
                return;
            }
            
            string dataString = optionalLoadedData.UnsafeGetFirst();

            OneOf<T, ErrorMessage> deserializeResult = JsonExtensions.Deserialize<T>(dataString);

            if (deserializeResult.HasSecond)
            {
                _logger.Log(
                    LogType.Error,
                    "Error deserializing {0} {1} with the following error: {2}",
                    typeof(SerializableData<T>).Name,
                    typeof(T).Name,
                    deserializeResult.UnsafeGetSecond()
                );
                
                OnLoad?.Invoke(Data);
                return;
            }

            _data = deserializeResult.UnsafeGetFirst();

            foreach (IMigration<T> migration in _migrations)
            {
                migration.Migrate(Data);
            }

            OnLoad?.Invoke(Data);

            loadStopWatch.Stop();
            
            _logger.Log(
                LogType.Info,
                "{0} {1} loaded ({2}ms) \n{3}",
                typeof(SerializableData<T>).Name,
                typeof(T).Name,
                loadStopWatch.ElapsedMilliseconds,
                Data
            );
        }

        public void SaveAsync()
        {
            Save(CancellationToken.None).RunAsync();
        }

        public void LoadAsync()
        {
            Load(CancellationToken.None).RunAsync();
        }

        T GetData()
        {
            TryGenerateData();

            return _data!;
        }

        void TryGenerateData()
        {
            if (_data != null)
            {
                return;
            }

            _data = _createDataAction.Invoke();
        }
    }
}
