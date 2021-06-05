using System.Collections.Generic;

namespace Eumel.EmailCategorizer.WpfUI.Manager
{
    public interface IEumelConfigManager
    {
        IEnumerable<string> ForwardMarker { get; set; }
        IEnumerable<string> ReplyMarker { get; set; }
        string ConfigStore { get; set; }
    }
}