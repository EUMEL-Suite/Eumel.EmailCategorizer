using System;
using System.IO;

namespace Eumel.EmailCategorizer.WpfUI.Storage
{
    public class FileEumelStorage : IEumelStorage
    {
        private readonly string _folder;

        public FileEumelStorage(string storageFolder = null)
        {
            _folder = Environment.ExpandEnvironmentVariables(storageFolder ?? string.Empty);
            if (_folder.IsNullOrWhiteSpace())
                _folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "EUMEL Suite"); ;

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