using System;
using Eumel.EmailCategorizer.Outlook.OutlookImpl;
using Eumel.EmailCategorizer.WpfUI;
using Eumel.EmailCategorizer.WpfUI.Manager;
using Eumel.EmailCategorizer.WpfUI.Storage;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Outlook;

namespace Eumel.EmailCategorizer.Outlook
{
    public partial class ThisAddIn
    {
        private EumelAggregateCategoryManager _categoryManager;
        private IEumelConfigManager _configManager;
        private IEumelStorage _storage;

        private static IEumelStorage BuildEumelStorage(IEumelConfigManager settings, Func<MAPIFolder> getMapiFolder)
        {
            var store = settings.ConfigStore;

            switch (store)
            {
                case nameof(FileEumelStorage):
                    return new FileEumelStorage();
                case nameof(JsonFileEumelStorage):
                    return new JsonFileEumelStorage();
                case nameof(OutlookEumelStorage):
                    return new OutlookEumelStorage(getMapiFolder());
                default:
                    return new EmptyEumelStorage();
            }
        }

        private void ThisAddIn_Startup(object sender, EventArgs e)
        {
            Application.ItemSend += Application_ItemSend;

            // the information about the config store needs to be hard coded somehow
            //var tmpStore = new OutlookEumelStorage(Application.Session.GetDefaultFolder(OlDefaultFolders.olFolderInbox));
            var tmpStore = new JsonFileEumelStorage();
            var tmpManager = new EumelConfigManager(tmpStore) as IEumelConfigManager;

            _storage = BuildEumelStorage(
                tmpManager, 
                () => Application.Session.GetDefaultFolder(OlDefaultFolders.olFolderInbox));
            _categoryManager = new EumelAggregateCategoryManager(
                _storage,
                new OutlookEumelStorage(Application.Session.GetDefaultFolder(OlDefaultFolders.olFolderInbox)));
            //_categoryManager = new EumelCategoryManager(_storage);
            _configManager = new EumelConfigManager(_storage);
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
            return new BackstageView(() => _categoryManager.ReadWriteManager, ()=> _configManager);
        }

        #region VSTO generated code

        private void InternalStartup()
        {
            Startup += ThisAddIn_Startup;
        }

        #endregion
    }
}