using System;
using System.IO;

namespace Eumel.EmailCategorizer.WpfUI.Storage
{
    public class JsonFileEumelStorage : IEumelStorage
    {
        private readonly string _folder;

        public JsonFileEumelStorage(string storageFolder = null)
        {
            _folder = storageFolder ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Eumel Suite");
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