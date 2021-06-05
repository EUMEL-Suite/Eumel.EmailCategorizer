using System;
using Eumel.EmailCategorizer.WpfUI.Storage;

namespace Eumel.EmailCategorizer.WpfUI.Manager
{
    public class CoreSettingsManager : IHaveCoreSettings
    {
        private const string ConfigStoreDataString = "Eumel.Categorizer.ConfigStore";
        private readonly IEumelStorage _storage;

        public CoreSettingsManager(IEumelStorage storage)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }


        public string ConfigStore
        {
            get => _storage[ConfigStoreDataString];
            set => _storage[ConfigStoreDataString] = value;
        }

    }
}