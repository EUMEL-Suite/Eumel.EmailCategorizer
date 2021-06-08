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
        private readonly IEumelStorage _storage;
        private const string Separator = "|";

        public EumelConfigManager(IEumelStorage storage)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        public ConfigModel GetConfig()
        {
            return new ConfigModel()
            {
                ConfigStore = _storage[ConfigStorePrefix + nameof(ConfigModel.ConfigStore)],
                ForwardMarker = (_storage[ConfigStorePrefix + nameof(ConfigModel.ForwardMarker)] ?? string.Empty)
                    .Split(new[] { Separator }, StringSplitOptions.RemoveEmptyEntries).ToList(),
                ReplyMarker = (_storage[ConfigStorePrefix + nameof(ConfigModel.ReplyMarker)] ?? string.Empty)
                    .Split(new[] { Separator }, StringSplitOptions.RemoveEmptyEntries).ToList()
            };
        }
    }
}