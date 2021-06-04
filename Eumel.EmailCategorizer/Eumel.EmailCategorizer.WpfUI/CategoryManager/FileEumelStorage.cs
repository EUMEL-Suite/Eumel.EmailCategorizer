using System.IO;

namespace Eumel.EmailCategorizer.WpfUI.CategoryManager
{
    public class FileEumelStorage : IEumelStorage
    {
        public bool IsReadOnly { get; } = false;

        public string this[string name]
        {
            get
            {
                 return File.Exists($"c:\\Workspaces\\{name.ToLower()}.txt")
                    ? File.ReadAllText($"c:\\Workspaces\\{name.ToLower()}.txt")
                    : string.Empty;
            }
            set => File.WriteAllText($"c:\\Workspaces\\{name.ToLower()}.txt", value);
        }
    }
}