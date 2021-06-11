using System;
using Eumel.EmailCategorizer.WpfUI.Storage;

namespace Eumel.EmailCategorizer.WpfUI.Manager
{
    /// <summary>
    /// config manager just for initial settings. These are the initial settings to get the full settings provider.
    /// For example the registry contains the information, that the settings are store in users "localappdata"
    /// </summary>
    public class InitConfigManager
    {
        private const string ConfigStorePrefix = "Eumel.";
        private readonly IEumelStorage _storage;

        public InitConfigManager(IEumelStorage storage)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        public string ConfigStore
        {
            get => _storage[ConfigStorePrefix + nameof(ConfigStore)];
            set => _storage[ConfigStorePrefix + nameof(ConfigStore)] = value;
        }

        public string ConfigStoreSettings
        {
            get => _storage[ConfigStorePrefix + nameof(ConfigStoreSettings)];
            set => _storage[ConfigStorePrefix + nameof(ConfigStoreSettings)] = value;
        }
    }
}