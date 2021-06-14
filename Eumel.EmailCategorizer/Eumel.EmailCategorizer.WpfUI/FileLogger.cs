using System;
using System.IO;

namespace Eumel.EmailCategorizer.WpfUI
{
    public class FileLogger : IEumelLogger
    {
        private const string FilenameConst = "%LocalAppData%\\Eumel Suite\\Eumel.Categorizer.log";
        private readonly string _filename;

        public FileLogger()
        {
            _filename = Environment.ExpandEnvironmentVariables(FilenameConst);
        }

        public void Init()
        {
            if (File.Exists(_filename))
                File.Delete(_filename);
        }

        public void Log(string message)
        {
            var now = DateTime.Now.ToUniversalTime();
            File.AppendAllText(_filename, now + @": " + message + Environment.NewLine);
        }
    }
}