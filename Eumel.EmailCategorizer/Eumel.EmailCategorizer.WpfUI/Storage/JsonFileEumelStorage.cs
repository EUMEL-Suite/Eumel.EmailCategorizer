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
        private readonly FileLogger _logger;
        private Dictionary<string, string> _settings = new Dictionary<string, string>();

        public JsonFileEumelStorage(string storageFolder = null)
        {
            _logger = new FileLogger();
            var folder = Environment.ExpandEnvironmentVariables(storageFolder ?? string.Empty);
            if (folder.IsNullOrWhiteSpace())
                folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "EUMEL Suite");

            Directory.CreateDirectory(folder);
            _filename = Path.Combine(folder, ConfigFile);
            _logger.Log($"[{nameof(JsonFileEumelStorage)}] Using file {_filename}");

            if (File.Exists(_filename))
                Load();
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

        public void Clear()
        {
            _logger.Log($"[{nameof(JsonFileEumelStorage)}] Deleting file (clear() invoked)");
            File.Delete(_filename);
            _settings.Clear();
        }

        private void Save()
        {
            _logger.Log($"[{nameof(JsonFileEumelStorage)}] Saving file");
            var json = JsonConvert.SerializeObject(_settings, Formatting.Indented);
            File.WriteAllText(_filename, json);
        }

        private void Load()
        {
            _logger.Log($"[{nameof(JsonFileEumelStorage)}] Loading file");
            var json = File.ReadAllText(_filename);
            _settings = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }
    }
}