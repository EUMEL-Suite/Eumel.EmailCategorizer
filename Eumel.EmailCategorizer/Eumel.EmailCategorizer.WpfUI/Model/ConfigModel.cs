using System.Collections.Generic;
using Eumel.EmailCategorizer.WpfUI.Manager;

namespace Eumel.EmailCategorizer.WpfUI.Model
{
    public class ConfigModel : IHaveConfigSettings
    {
        public IEnumerable<CategoryModel> Categories { get; set; }
        public IEnumerable<string> ForwardMarker { get; set; }
        public IEnumerable<string> ReplyMarker { get; set; }
        public string ConfigStore { get; set; }
    }
}