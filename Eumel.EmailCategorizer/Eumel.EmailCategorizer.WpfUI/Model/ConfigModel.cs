using System.Collections.Generic;
using Eumel.EmailCategorizer.WpfUI.Manager;
using Eumel.EmailCategorizer.WpfUI.Storage;

namespace Eumel.EmailCategorizer.WpfUI.Model
{
    public class ConfigModel : IHaveCoreSettings
    {
        public IEnumerable<CategoryModel> Categories { get; set; }
        public IEnumerable<string> ForwardMarker { get; set; }
        public IEnumerable<string> ReplyMarker { get; set; }
        public string ConfigStore { get; set; }

        public static ConfigModel Default()
        {
            return new ConfigModel()
            {
                ConfigStore = nameof(JsonFileEumelStorage),
                ReplyMarker = new []{"RE:", "AW:"},
                ForwardMarker = new []{"FW:","WG:"}
            };
        }
    }
}