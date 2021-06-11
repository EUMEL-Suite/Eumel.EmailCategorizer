using System;
using System.Collections.Generic;
using Eumel.EmailCategorizer.WpfUI.Model;
using Eumel.EmailCategorizer.WpfUI.Storage;

namespace Eumel.EmailCategorizer.WpfUI.Manager
{
    public static class EumelConfigManagerExtensions
    {
        public static IEumelStorage GetWriteStorage(this ConfigModel config, Dictionary<string, Func<string, IEumelStorage>> storageFactory)
        {
            return storageFactory[config.WriteStorage ?? string.Empty](config.GetConfigStoreSettingsForStore(config.WriteStorage));
        }

        public static IEumelStorage[] GetReadStorages(this ConfigModel config,
            Dictionary<string, Func<string, IEumelStorage>> storageFactory)
        {
            var result = new List<IEumelStorage>();

            if (config.UseHttpSource)
                result.Add(storageFactory[nameof(HttpEumelStorage)](config.HttpSource));

            if (config.UseJsonFileStorage)
                result.Add(storageFactory[nameof(JsonFileEumelStorage)](config.StorageFolder));

            if (config.UsePlainFileStorage)
                result.Add(storageFactory[nameof(FileEumelStorage)](config.StorageFolder));

            if (config.UseOutlookPst)
                result.Add(storageFactory["OutlookEumelStorage"](config.StorageFolder)); // magic that I know this one exists

            return result.ToArray();
        }

        private static string GetConfigStoreSettingsForStore(this ConfigModel config, string storage)
        {
            switch (storage)
            {
                case nameof(JsonFileEumelStorage):
                    return config.StorageFolder;
                case nameof(FileEumelStorage):
                    return config.StorageFolder;
                case nameof(HttpEumelStorage):
                    return config.HttpSource;
                default:
                    return null;
            }

        }
    }
}