using System;
using System.Collections.Generic;
using Eumel.EmailCategorizer.WpfUI.Storage;

namespace Eumel.EmailCategorizer.WpfUI.Manager
{
    public class EumelConfigManager : IEumelConfigManager
    {
        private const string ConfigStorePrefix = "Eumel.Categorizer.";
        private readonly IEumelStorage _storage;
        private const string Separator = "|";

        public EumelConfigManager(IEumelStorage storage)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        public string ConfigStore
        {
            get => _storage[ConfigStorePrefix + nameof(ConfigStore)];
            set => _storage[ConfigStorePrefix + nameof(ConfigStore)] = value;
        }

        public IEnumerable<string> ForwardMarker
        {
            get => _storage[ConfigStorePrefix + nameof(ForwardMarker)].Split(new[] { Separator }, StringSplitOptions.RemoveEmptyEntries);
            set => _storage[ConfigStorePrefix + nameof(ForwardMarker)] = string.Join(Separator, value);
        }

        public IEnumerable<string> ReplyMarker
        {
            get => _storage[ConfigStorePrefix + nameof(ReplyMarker)].Split(new[] { Separator }, StringSplitOptions.RemoveEmptyEntries);
            set => _storage[ConfigStorePrefix + nameof(ReplyMarker)] = string.Join(Separator, value);
        }
    }
}