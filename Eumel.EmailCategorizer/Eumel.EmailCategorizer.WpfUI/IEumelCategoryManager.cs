using System.Collections.Generic;

namespace Eumel.EmailCategorizer.WpfUI
{
    public interface IEumelCategoryManager
    {
        bool IsReadOnly { get; }
        void Add(string category);
        void Delete(string category);
        IEnumerable<string> Get();
    }
}