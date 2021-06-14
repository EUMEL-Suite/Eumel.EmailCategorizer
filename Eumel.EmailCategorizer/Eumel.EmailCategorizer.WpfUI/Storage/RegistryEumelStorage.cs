using System.Linq;
using Microsoft.Win32;

namespace Eumel.EmailCategorizer.WpfUI.Storage
{
    public class RegistryEumelStorage : IEumelStorage
    {
        private const string EumelRegistryKey = @"SOFTWARE\EUMEL Suite";
        private readonly string _prefix;

        public RegistryEumelStorage(string prefix = "Eumel.Categorizer.")
        {
            _prefix = prefix ?? "Eumel.Categorizer.";
        }

        public bool IsReadOnly { get; } = false;

        public string this[string name]
        {
            get
            {
                var result = (string) null;
                var key = Registry.CurrentUser.CreateSubKey(EumelRegistryKey);
                if (key?.GetValueNames()?.Contains(_prefix + name) ?? false)
                    result = key.GetValue(_prefix + name) as string;
                key?.Close();
                return result;
            }
            set
            {
                var key = Registry.CurrentUser.CreateSubKey(EumelRegistryKey);
                key?.SetValue(_prefix + name, value);
                key?.Close();
            }
        }

        public void Clear()
        {
            var key = Registry.CurrentUser.CreateSubKey(EumelRegistryKey);
            foreach (var valKey in (key?.GetValueNames() ?? Enumerable.Empty<string>()).ToArray())
                key?.DeleteValue(valKey);
            key?.Close();
        }
    }
}