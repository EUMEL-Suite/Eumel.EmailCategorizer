using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Eumel.EmailCategorizer.WpfUI.Storage
{
    public class JsonFileEumelStorage : IEumelStorage
    {
        private const string ConfigFile = "eumel.settings.json";
        private readonly string _filename;
        private Dictionary<string, string> _settings = new Dictionary<string, string>();

        public JsonFileEumelStorage(string storageFolder = null)
        {
            var folder = Environment.ExpandEnvironmentVariables(storageFolder ?? string.Empty);
            if (folder.IsNullOrWhiteSpace())
                folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "EUMEL Suite"); ;

            Directory.CreateDirectory(folder);
            _filename = Path.Combine(folder, ConfigFile);

            if (File.Exists(_filename))
                Load();
            else
                Save();
        }

        private void Save()
        {
            var json = JsonConvert.SerializeObject(_settings, Formatting.Indented);
            File.WriteAllText(_filename, json);
        }

        private void Load()
        {
            var json = File.ReadAllText(_filename);
            _settings = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }

        public bool IsReadOnly { get; } = false;

        public string this[string name]
        {
            get => _settings.ContainsKey(name)
                    ? _settings[name]
                    : null;
            set
            {
                _settings[name] = value;
                Save();
            }
        }
    }
}