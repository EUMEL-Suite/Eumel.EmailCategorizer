using System.Collections.Generic;
using Eumel.EmailCategorizer.WpfUI.Storage;

namespace Eumel.EmailCategorizer.WpfUI.Model
{
    public class ConfigModel
    {
        public List<string> ForwardMarker { get; set; }
        public List<string> ReplyMarker { get; set; }
        
        public string ConfigStore { get; set; }

        public static ConfigModel Default()
        {
            return new ConfigModel()
            {
                ConfigStore = nameof(JsonFileEumelStorage),
                ReplyMarker = new List<string>() {"RE:", "AW:"},
                ForwardMarker = new List<string>() {"FW:", "WG:"}
            };
        }
    }
}