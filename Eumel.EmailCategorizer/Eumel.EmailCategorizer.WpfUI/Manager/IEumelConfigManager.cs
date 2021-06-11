﻿using Eumel.EmailCategorizer.WpfUI.Model;

namespace Eumel.EmailCategorizer.WpfUI.Manager
{
    public interface IEumelConfigManager
    {
        ConfigModel GetConfig();
        void Save(ConfigModel config);
    }
}