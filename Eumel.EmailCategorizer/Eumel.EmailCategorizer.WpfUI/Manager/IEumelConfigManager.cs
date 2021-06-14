using System;
using System.Collections.Generic;
using Eumel.EmailCategorizer.WpfUI.Model;
using Eumel.EmailCategorizer.WpfUI.Storage;

namespace Eumel.EmailCategorizer.WpfUI.Manager
{
    public interface IEumelConfigManager
    {
        Dictionary<string, Func<string, IEumelStorage>> StorageFactory { get; }
        ConfigModel GetConfig();
        void Save(ConfigModel config);

        void Clear(bool forAllStorages);
    }
}