using System.Collections.Generic;
using Eumel.EmailCategorizer.WpfUI.Model;

namespace Eumel.EmailCategorizer.WpfUI.Manager
{
    public interface IEumelConfigManager
    {
        ConfigModel GetConfig();
    }
}