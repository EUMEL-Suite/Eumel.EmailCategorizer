using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
                WriteStorage = _storage[ConfigStorePrefix + nameof(ConfigModel.WriteStorage)],
                CategoryPrefix = _storage[ConfigStorePrefix + nameof(ConfigModel.CategoryPrefix)],
                CategoryPostfix = _storage[ConfigStorePrefix + nameof(ConfigModel.CategoryPostfix)]
            };
        }

        public void Save(ConfigModel config)
        {
            // for string we just store it 1:1
            var props = config.GetType()
                .GetProperties()
                .Where(x => x.PropertyType == typeof(string));
            foreach (var info in props)
            {
                var name = info.Name;
                var value = info.GetValue(config) as string;

                _storage[ConfigStorePrefix + name] = value;
            }

            // for lists, we need to convert it
            props = config.GetType()
                .GetProperties()
                .Where(x => x.PropertyType == typeof(List<string>));
            foreach (var info in props)
            {
                var name = info.Name;
                var value = (info.GetValue(config) as List<string>) ?? Enumerable.Empty<string>();

                _storage[ConfigStorePrefix + name] = string.Join(Separator, value);
            }
        }
    }
}