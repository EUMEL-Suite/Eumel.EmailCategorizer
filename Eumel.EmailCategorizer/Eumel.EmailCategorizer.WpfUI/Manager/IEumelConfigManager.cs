using System.Collections.Generic;

namespace Eumel.EmailCategorizer.WpfUI.Manager
{
    public interface IEumelConfigManager : IHaveCoreSettings
    {
        IEnumerable<string> ForwardMarker { get; set; }
        IEnumerable<string> ReplyMarker { get; set; }

    }
}