using System;
using System.Collections.Generic;
using Eumel.EmailCategorizer.WpfUI.Model;
using Eumel.EmailCategorizer.WpfUI.Storage;

namespace Eumel.EmailCategorizer.WpfUI.Manager
{
    public class EumelConfigManager : IEumelConfigManager
    {
        private IEumelStorage _storage;

        public EumelConfigManager(IEumelStorage storage)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        public string ConfigStore { get; set; }
        public IEnumerable<string> ForwardMarker { get; set; }
        public IEnumerable<string> ReplyMarker { get; set; }
        public IEnumerable<CategoryModel> Categories { get; set; }
    }
}