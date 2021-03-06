using System;
using System.Collections.Generic;
using System.Linq;
using Eumel.EmailCategorizer.WpfUI.Storage;

namespace Eumel.EmailCategorizer.WpfUI.Manager
{
    public class EumelAggregateCategoryManager : IEumelCategoryManager
    {
        private readonly IDictionary<IEumelCategoryManager, IEnumerable<string>> _readStorageCache;

        public EumelAggregateCategoryManager(IEumelStorage readWriteStorage, params IEumelStorage[] readStorage)
        {
            if (readWriteStorage == null) throw new ArgumentNullException(nameof(readWriteStorage));

            ReadWriteManager = new EumelCategoryManager(readWriteStorage);
            _readStorageCache = (readStorage ?? Enumerable.Empty<IEumelStorage>())
                .Select(x => new EumelCategoryManager(x))
                .ToDictionary(x => (IEumelCategoryManager) x, x => Enumerable.Empty<string>());
            _readStorageCache.Add(ReadWriteManager, Enumerable.Empty<string>());
        }

        public IEumelCategoryManager ReadWriteManager { get; }

        public bool IsReadOnly => ReadWriteManager != null;

        public void Add(string category)
        {
            // Do not add categories from external source
            var lst = _readStorageCache.Values.SelectMany(x => x);
            if (lst.Contains(category, StringComparer.CurrentCultureIgnoreCase)) return;

            ReadWriteManager.Add(category);
        }

        public void Delete(string category)
        {
            // Do not delete categories from external source
            var lst = _readStorageCache.Values.SelectMany(x => x);
            if (lst.Contains(category, StringComparer.CurrentCultureIgnoreCase)) return;

            ReadWriteManager.Delete(category);
        }

        public IEnumerable<string> Get()
        {
            // TODO I should use the cache here...
            var result = new List<string>();
            foreach (var manager in _readStorageCache.Keys.ToArray())
            {
                var tmp = manager.Get().ToArray();
                result.AddRange(tmp);
                _readStorageCache[manager] = tmp;
            }

            // TODO this can be made more fancy
            return result.Distinct().OrderBy(x => x).ToArray();
        }
    }
}