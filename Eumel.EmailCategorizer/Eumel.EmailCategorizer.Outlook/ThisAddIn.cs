using System;
using System.Collections.Generic;
using Eumel.EmailCategorizer.Outlook.OutlookImpl;
using Eumel.EmailCategorizer.WpfUI;
using Eumel.EmailCategorizer.WpfUI.Manager;
using Eumel.EmailCategorizer.WpfUI.Model;
using Eumel.EmailCategorizer.WpfUI.Storage;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Outlook;
using Microsoft.Win32;

namespace Eumel.EmailCategorizer.Outlook
{
    public partial class ThisAddIn
    {
        private EumelAggregateCategoryManager _categoryManager;
        private IEumelConfigManager _configManager;
        private IEumelStorage _storage;
        private ConfigModel _config;

        private readonly Dictionary<string, Func<string, IEumelStorage>> _storageFactories =
            new Dictionary<string, Func<string, IEumelStorage>>();


        private void ThisAddIn_Startup(object sender, EventArgs e)
        {
            Application.ItemSend += Application_ItemSend;
            InitStorageFactory();

            // reading config store backend from registry
            var initSettingsStorage = new RegistryEumelStorage();
            var initSettings = new InitConfigManager(initSettingsStorage);

            // get storage for application data and config settings
            if (string.IsNullOrWhiteSpace(initSettings.ConfigStore) || !_storageFactories.ContainsKey(initSettings.ConfigStore))
            {
                initSettings.ConfigStore = nameof(JsonFileEumelStorage);
                initSettings.ConfigStoreSettings = string.Empty;
            }

            _storage = _storageFactories[initSettings.ConfigStore](initSettings.ConfigStoreSettings);
            _configManager = new EumelConfigManager(_storage);
            _config = _configManager.GetConfig();

            _categoryManager = new EumelAggregateCategoryManager(
                _config.GetWriteStorage(_storageFactories),
                _config.GetReadStorages(_storageFactories));
        }

        private void InitStorageFactory()
        {
            _storageFactories.Add(nameof(FileEumelStorage), c => new FileEumelStorage(c));
            _storageFactories.Add(nameof(JsonFileEumelStorage), c => new JsonFileEumelStorage(c));
            _storageFactories.Add(nameof(RegistryEumelStorage), c => new RegistryEumelStorage());
            _storageFactories.Add(nameof(HttpEumelStorage), c => new HttpEumelStorage(c));
            _storageFactories.Add(nameof(OutlookEumelStorage), c => new OutlookEumelStorage(Application.Session.GetDefaultFolder(OlDefaultFolders.olFolderInbox)));
            _storageFactories.Add(string.Empty, c => new EmptyEumelStorage());
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