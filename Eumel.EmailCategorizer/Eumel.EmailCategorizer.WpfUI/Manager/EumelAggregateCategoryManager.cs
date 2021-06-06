using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Eumel.EmailCategorizer.WpfUI.Storage;

namespace Eumel.EmailCategorizer.WpfUI.Manager
{
    public class EumelAggregateCategoryManager : IEumelCategoryManager
    {
        private readonly IEumelCategoryManager _readWriteStorage;
        private readonly IDictionary<IEumelCategoryManager, IEnumerable<string>> _readStorageCache;

        public EumelAggregateCategoryManager(IEumelStorage readWriteStorage = null, params IEumelStorage[] readStorage)
        {
            _readWriteStorage = new EumelCategoryManager(readWriteStorage ?? new EmptyEumelStorage());
            _readStorageCache = (readStorage ?? Enumerable.Empty<IEumelStorage>())
                .Select(x => new EumelCategoryManager(x))
                .ToDictionary(x => (IEumelCategoryManager)x, x => Enumerable.Empty<string>());
        }

        public bool IsReadOnly => _readWriteStorage != null;
        public IEumelCategoryManager ReadWriteManager => _readWriteStorage;

        public void Add(string category)
        {
            // Do not add categories from external source
            var lst = _readStorageCache.Values.SelectMany(x => x);
            if (lst.Contains(category, StringComparer.CurrentCultureIgnoreCase)) return;

            _readWriteStorage.Add(category);
        }

        public void Delete(string category)
        {
            // Do not delete categories from external source
            var lst = _readStorageCache.Values.SelectMany(x => x);
            if (lst.Contains(category, StringComparer.CurrentCultureIgnoreCase)) return;

            _readWriteStorage.Delete(category);
        }

        public IEnumerable<string> Get()
        {
            // TODO I should use the cache here...
            var result = new List<string>(_readWriteStorage.Get());
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