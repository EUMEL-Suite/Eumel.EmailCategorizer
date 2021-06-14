using System;
using System.Collections.Generic;
using System.Linq;
using Eumel.EmailCategorizer.WpfUI.Model;
using Eumel.EmailCategorizer.WpfUI.Storage;

namespace Eumel.EmailCategorizer.WpfUI.Manager
{
    public class EumelConfigManager : IEumelConfigManager
    {
        private const string ConfigStorePrefix = "Eumel.Categorizer.";
        private const string Separator = "|";
        private readonly IEumelStorage _storage;

        public EumelConfigManager(IEumelStorage storage, Dictionary<string, Func<string, IEumelStorage>> storageFactory)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            StorageFactory = storageFactory;
        }

        public ConfigModel GetConfig()
        {
            var result = new ConfigModel();

            // for string we just read it 1:1
            var props = result.GetType().GetProperties().Where(x => x.PropertyType == typeof(string));
            foreach (var info in props)
                info.SetValue(result, _storage[ConfigStorePrefix + info.Name]);

            // for lists we need to convert it
            props = result.GetType().GetProperties().Where(x => x.PropertyType == typeof(List<string>));
            foreach (var info in props)
                info.SetValue(result,
                    (_storage[ConfigStorePrefix + info.Name] ?? string.Empty)
                    .Split(new[] {Separator}, StringSplitOptions.RemoveEmptyEntries).ToList());

            // for bool we need to convert it too
            props = result.GetType().GetProperties().Where(x => x.PropertyType == typeof(bool));
            foreach (var info in props)
            {
                bool.TryParse(_storage[ConfigStorePrefix + info.Name], out var value);
                info.SetValue(result, value);
            }

            return result;
        }

        public void Save(ConfigModel config)
        {
            // for string we just store it 1:1
            var props = config.GetType().GetProperties().Where(x => x.PropertyType == typeof(string));
            foreach (var info in props)
                _storage[ConfigStorePrefix + info.Name] = info.GetValue(config) as string;

            // for lists, we need to convert it
            props = config.GetType().GetProperties().Where(x => x.PropertyType == typeof(List<string>));
            foreach (var info in props)
                _storage[ConfigStorePrefix + info.Name] = string.Join(Separator,
                    info.GetValue(config) as List<string> ?? Enumerable.Empty<string>());

            // for bool, we need to convert it, too
            props = config.GetType().GetProperties().Where(x => x.PropertyType == typeof(bool));
            foreach (var info in props)
                _storage[ConfigStorePrefix + info.Name] = ((bool) info.GetValue(config)).ToString();
        }

        public void Clear(bool forAllStorages)
        {
            _storage.Clear();
            // todo, problably here need to be the code to determine the config for the storage
            if (forAllStorages)
                StorageFactory.Values
                    .Select(x => x(null))
                    .Where(x => !x.IsReadOnly).ToList()
                    .ForEach(x => x.Clear());
        }

        public Dictionary<string, Func<string, IEumelStorage>> StorageFactory { get; }
    }
}