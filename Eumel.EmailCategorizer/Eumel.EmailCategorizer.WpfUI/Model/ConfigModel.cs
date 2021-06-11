using System.Collections.Generic;
using Eumel.EmailCategorizer.WpfUI.Storage;

namespace Eumel.EmailCategorizer.WpfUI.Model
{
    public class ConfigModel
    {
        public List<string> ForwardMarker { get; set; }
        public List<string> ReplyMarker { get; set; }
        public bool UseOutlookPst { get; set; }
        public bool UsePlainFileStorage { get; set; }
        public bool UseJsonFileStorage { get; set; }
        public bool UseHttpSource { get; set; }
        public string HttpSource { get; set; }
        public string StorageFolder { get; set; }
        public string WriteStorage { get; set; }

        public static ConfigModel Default()
        {
            return new ConfigModel()
            {
                WriteStorage = nameof(JsonFileEumelStorage),
                UseOutlookPst = false,
                UsePlainFileStorage = false,
                UseHttpSource = false,
                ReplyMarker = new List<string>() {"RE:", "AW:"},
                ForwardMarker = new List<string>() {"FW:", "WG:"}
            };
        }
    }
}
