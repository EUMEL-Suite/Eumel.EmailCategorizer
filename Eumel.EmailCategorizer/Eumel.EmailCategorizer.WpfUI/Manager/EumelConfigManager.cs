using System;
using System.Linq;
using Eumel.EmailCategorizer.WpfUI.Model;
using Eumel.EmailCategorizer.WpfUI.Storage;
using Xceed.Wpf.Toolkit.Core.Converters;

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
                // TODO MAKE THIS GENERIC
                ForwardMarker = (_storage[ConfigStorePrefix + nameof(ConfigModel.ForwardMarker)] ?? string.Empty)
                    .Split(new[] { Separator }, StringSplitOptions.RemoveEmptyEntries).ToList(),
                ReplyMarker = (_storage[ConfigStorePrefix + nameof(ConfigModel.ReplyMarker)] ?? string.Empty)
                    .Split(new[] { Separator }, StringSplitOptions.RemoveEmptyEntries).ToList(),
                UseHttpSource = bool.Parse(_storage[ConfigStorePrefix + nameof(ConfigModel.UseHttpSource)] ?? "false"),
                UseJsonFileStorage = bool.Parse(_storage[ConfigStorePrefix + nameof(ConfigModel.UseJsonFileStorage)] ?? "true"),
                UseOutlookPst = bool.Parse(_storage[ConfigStorePrefix + nameof(ConfigModel.UseOutlookPst)] ?? "false"),
                UsePlainFileStorage = bool.Parse(_storage[ConfigStorePrefix + nameof(ConfigModel.UsePlainFileStorage)] ?? "false"),
                HttpSource = _storage[ConfigStorePrefix + nameof(ConfigModel.HttpSource)],
                WriteStorage = _storage[ConfigStorePrefix + nameof(ConfigModel.WriteStorage)]
            };
        }

        public void Save(ConfigModel config)
        {
            // TODO MAKE THIS GENERIC
            _storage[ConfigStorePrefix + nameof(ConfigModel.ForwardMarker)] = string.Join(Separator, config.ForwardMarker);
            _storage[ConfigStorePrefix + nameof(ConfigModel.ReplyMarker)] = string.Join(Separator, config.ReplyMarker);
            _storage[ConfigStorePrefix + nameof(ConfigModel.UseHttpSource)] = config.UseHttpSource.ToString();
            _storage[ConfigStorePrefix + nameof(ConfigModel.UseJsonFileStorage)] = config.UseJsonFileStorage.ToString();
            _storage[ConfigStorePrefix + nameof(ConfigModel.UseOutlookPst)] = config.UseOutlookPst.ToString();
            _storage[ConfigStorePrefix + nameof(ConfigModel.UsePlainFileStorage)] = config.UsePlainFileStorage.ToString();
            _storage[ConfigStorePrefix + nameof(ConfigModel.HttpSource)] = config.HttpSource;
            _storage[ConfigStorePrefix + nameof(ConfigModel.WriteStorage)] = config.WriteStorage;
        }
    }
}