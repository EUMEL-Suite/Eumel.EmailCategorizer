using System.Linq;
using Microsoft.Win32;

namespace Eumel.EmailCategorizer.WpfUI.Storage
{
    public class RegistryEumelStorage : IEumelStorage
    {
        private const string EumelRegistryKey = @"SOFTWARE\EUMEL Suite";

        public bool IsReadOnly { get; } = false;

        public string this[string name]
        {
            get
            {
                var result = (string)null;
                var key = Registry.CurrentUser.CreateSubKey(EumelRegistryKey);
                if (key?.GetValueNames()?.Contains(name) ?? false)
                    result = key.GetValue(name) as string;
                key?.Close();
                return result;
            }
            set
            {
                var key = Registry.CurrentUser.CreateSubKey(EumelRegistryKey);
                key?.SetValue(name, value);
                key?.Close();
            }
        }
    }
}