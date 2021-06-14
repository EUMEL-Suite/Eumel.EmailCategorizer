using System;
using System.Collections.Generic;
using System.Linq;
using Eumel.EmailCategorizer.Outlook.OutlookImpl;
using Eumel.EmailCategorizer.WpfUI;
using Eumel.EmailCategorizer.WpfUI.Manager;
using Eumel.EmailCategorizer.WpfUI.Model;
using Eumel.EmailCategorizer.WpfUI.Storage;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Outlook;

namespace Eumel.EmailCategorizer.Outlook
{
    public partial class ThisAddIn
    {
        private EumelAggregateCategoryManager _categoryManager;
        private ConfigModel _config;
        private IEumelConfigManager _configManager;
        private IEumelStorage _storage;

        private void ThisAddIn_Startup(object sender, EventArgs e)
        {
            var logger = new FileLogger();
            logger.Init();
            logger.Log("Starting Eumel.Categorizer");

            Application.ItemSend += Application_ItemSend;
            var storageFactories = InitStorageFactory();
            logger.Log("Storage Factory initiated for (" + string.Join(",", storageFactories.Keys) + ")");

            // the initial configuration is retrieved by the registry
            var initSettingsStorage = new RegistryEumelStorage(string.Empty);
            var initSettings = new InitConfigManager(initSettingsStorage);
            logger.Log(
                $"Core setting read from registry ('ConfigStore':{initSettings.ConfigStore}, 'ConfigStoreSettings':{initSettings.ConfigStoreSettings}, 'ClearOnStart':{initSettings.ClearOnStart})");

            if (initSettings.ClearOnStart == true.ToString())
            {
                storageFactories.ToList().ForEach(s => s.Value(null).Clear());
                logger.Log("Settings cleared from all storages.");
            }

            // get storage for application data and config settings
            if (string.IsNullOrWhiteSpace(initSettings.ConfigStore) ||
                !storageFactories.ContainsKey(initSettings.ConfigStore) || initSettings.ClearOnStart == true.ToString())
            {
                logger.Log(
                    "ConfigStore not set, not found, or ClearOnStart. It will be initialized from scratch with all defaults.");
                initSettings.ConfigStore = nameof(JsonFileEumelStorage);
                initSettings.ConfigStoreSettings = "%LocalAppData%\\EUMEL Suite";
                initSettings.ClearOnStart = true.ToString();
            }

            logger.Log("Initializing the config manager");
            _storage = storageFactories[initSettings.ConfigStore](initSettings.ConfigStoreSettings);
            _configManager = new EumelConfigManager(_storage, storageFactories);
            if (initSettings.ClearOnStart == true.ToString())
            {
                logger.Log("Storing default values to config manager");
                _configManager.Save(ConfigModel.Default());
            }

            _config = _configManager.GetConfig();

            logger.Log("Initializing the categories manager");
            _categoryManager = new EumelAggregateCategoryManager(
                _config.GetWriteStorage(storageFactories),
                _config.GetReadStorages(storageFactories));

            logger.Log("ClearOnStart is set to False");
            initSettings.ClearOnStart = "False";
        }

        private Dictionary<string, Func<string, IEumelStorage>> InitStorageFactory()
        {
            var result = new Dictionary<string, Func<string, IEumelStorage>>
            {
                {nameof(FileEumelStorage), c => new FileEumelStorage(c)},
                {nameof(JsonFileEumelStorage), c => new JsonFileEumelStorage(c)},
                {nameof(RegistryEumelStorage), c => new RegistryEumelStorage()},
                {nameof(HttpEumelStorage), c => new HttpEumelStorage(c)},
                {
                    nameof(OutlookEumelStorage),
                    c => new OutlookEumelStorage(
                        Application.Session.GetDefaultFolder(OlDefaultFolders.olFolderInbox))
                },
                {string.Empty, c => new EmptyEumelStorage()}
            };
            return result;
        }

        private void Application_ItemSend(object item, ref bool cancel)
        {
            // since we only handle mail items
            if (!(item is MailItem mail)) return;

            var email = new EnhancedMailItem(mail);

            var window = new EmailSubjectWindow
            {
                Subject = email.Subject,
                CategoryManager = _categoryManager
            };

            var dialogResult = window.ShowDialog();
            switch (dialogResult)
            {
                case true:
                    email.UpdateOriginalMail();
                    break;
                case false:
                    cancel = true;
                    break;
                default:
                    cancel = true;
                    break;
            }
        }

        protected override IRibbonExtensibility CreateRibbonExtensibilityObject()
        {
            return new BackstageView(() => _categoryManager.ReadWriteManager, () => _configManager);
        }

        #region VSTO generated code

        private void InternalStartup()
        {
            Startup += ThisAddIn_Startup;
        }

        #endregion
    }
}