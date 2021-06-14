using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace Eumel.EmailCategorizer.WpfUI.Storage
{
    public class HttpEumelStorage : IEumelStorage
    {
        private readonly Dictionary<string, string> _data;

        public HttpEumelStorage(string source)
        {
            try
            {
                var request = (HttpWebRequest) WebRequest.Create(source);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (var response = (HttpWebResponse) request.GetResponse())
                using (var stream = response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    _data = reader
                        .ReadToEnd()
                        .Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => x.Split('='))
                        .Where(x => x.Length == 2)
                        .ToDictionary(x => x[0].ToLower(), x => x[1]);
                }
            }
            catch (Exception ex)
            {
                _data = new Dictionary<string, string> {{"Error", ex.Message}};
            }
        }

        public bool IsReadOnly { get; } = true;

        public string this[string name]
        {
            get => _data.ContainsKey(name.ToLower())
                ? _data[name]
                : string.Empty;
            set => throw new NotImplementedException();
        }

        public void Clear()
        {
        }
    }
}