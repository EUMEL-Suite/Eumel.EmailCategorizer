using System.Collections.Generic;
using Eumel.EmailCategorizer.WpfUI.Model;

namespace Eumel.EmailCategorizer.WpfUI.Manager
{
    public interface IHaveConfigSettings
    {
        IEnumerable<string> ForwardMarker { get; set; }
        IEnumerable<string> ReplyMarker { get; set; }
        IEnumerable<CategoryModel> Categories { get; set; }
    }
}