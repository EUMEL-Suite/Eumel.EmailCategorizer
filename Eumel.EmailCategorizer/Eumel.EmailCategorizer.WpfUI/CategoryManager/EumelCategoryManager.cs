using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;

namespace Eumel.EmailCategorizer.WpfUI.CategoryManager
{
    public class EumelCategoryManager : IEumelCategoryManager
    {
        private const string CategoryDataString = "Eumel.Categorizer.Categories";
        private const string CategorySeparator = "|";
        private readonly IEumelStorage _storage;

        public EumelCategoryManager(IEumelStorage storage)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        public bool IsReadOnly { get; } = false;

        public virtual void Add(string category)
        {
            // get all without the category to add (ignore case)
            var existing = Get().Where(x => !string.Equals(x, category, StringComparison.CurrentCultureIgnoreCase));

            var categories = existing.Concat(new[] { category }).OrderBy(x => x).Implode(CategorySeparator);
            _storage[CategoryDataString] = categories;
        }

        public virtual void Delete(string category)
        {
            // get all without the category to add (ignore case)
            var existing = Get().Where(x => !string.Equals(x, category, StringComparison.CurrentCultureIgnoreCase));
            var categories = existing.OrderBy(x => x).Implode(CategorySeparator);
            _storage[CategoryDataString] = categories;
        }

        public virtual IEnumerable<string> Get()
        {
            var result = (_storage[CategoryDataString] ?? string.Empty)
                .Split(new[] { CategorySeparator }, StringSplitOptions.RemoveEmptyEntries);

            return result;
        }
    }

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
            var result = new List<string>(_readWriteStorage.Get());
            foreach (var manager in _readStorageCache.Keys)
            {
                var tmp = manager.Get().ToArray();
                result.AddRange(tmp);
                _readStorageCache[manager] = tmp;
            }

            return result;
        }
    }
}