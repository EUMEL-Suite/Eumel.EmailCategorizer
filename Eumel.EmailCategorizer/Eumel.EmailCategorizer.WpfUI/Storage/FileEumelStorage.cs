using System;
using System.IO;

namespace Eumel.EmailCategorizer.WpfUI.Storage
{
    public class FileEumelStorage : IEumelStorage
    {
        private readonly string _folder;

        public FileEumelStorage(string storageFolder = null)
        {
            var defaultValue = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "EUMEL Suite");
            _folder = Environment.ExpandEnvironmentVariables(storageFolder ?? defaultValue);
            Directory.CreateDirectory(_folder);
        }
        public bool IsReadOnly { get; } = false;

        public string this[string name]
        {
            get => File.Exists($"{_folder}\\{name.ToLower()}.txt")
                ? File.ReadAllText($"c{_folder}\\{name.ToLower()}.txt")
                : string.Empty;
            set => File.WriteAllText($"{_folder}\\{name.ToLower()}.txt", value);
        }
    }
}