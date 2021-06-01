using System;
using System.Collections.Generic;
using System.Linq;
using Eumel.EmailCategorizer.WpfUI;

namespace Eumel.EmailCategorizer.Outlook.OutlookImpl
{
    public class EumelCategoryManager : IEumelCategoryManager
    {
        private const string CategoryDataString = "Categories";
        private const string CategorySeparator = "|";
        private readonly IEumelStorage _storage;

        public EumelCategoryManager(IEumelStorage storage)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        public bool IsReadOnly { get; } = false;

        public void Add(string category)
        {
            // get all without the category to add (ignore case)
            var existing = Get().Where(x => !string.Equals(x, category, StringComparison.CurrentCultureIgnoreCase));

            var categories = existing.Concat(new[] { category }).OrderBy(x => x).Implode(CategorySeparator);
            _storage[CategoryDataString] = categories;
        }

        public void Delete(string category)
        {
            // get all without the category to add (ignore case)
            var existing = Get().Where(x => !string.Equals(x, category, StringComparison.CurrentCultureIgnoreCase));
            var categories = existing.OrderBy(x => x).Implode(CategorySeparator);
            _storage[CategoryDataString] = categories;
        }

        public IEnumerable<string> Get()
        {
            var result = (_storage[CategoryDataString] ?? string.Empty)
                .Split(new[] { CategorySeparator }, StringSplitOptions.RemoveEmptyEntries);

            return result;
        }
    }
}