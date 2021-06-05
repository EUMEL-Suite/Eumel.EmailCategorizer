using System;
using Eumel.EmailCategorizer.WpfUI.Storage;

namespace Eumel.EmailCategorizer.WpfUI.Manager
{
    public class CoreSettingsManager : IHaveCoreSettings
    {
        private const string ConfigStorePrefix = "Eumel.Categorizer.";
        private readonly IEumelStorage _storage;

        public CoreSettingsManager(IEumelStorage storage)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }


        public string ConfigStore
        {
            get => _storage[ConfigStorePrefix + nameof(ConfigStore)];
            set => _storage[ConfigStorePrefix + nameof(ConfigStore)] = value;
        }

    }
}