using System;
using Eumel.EmailCategorizer.Outlook.OutlookImpl;
using Eumel.EmailCategorizer.WpfUI;
using Eumel.EmailCategorizer.WpfUI.CategoryManager;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Outlook;

namespace Eumel.EmailCategorizer.Outlook
{
    public partial class ThisAddIn
    {
        private IEumelCategoryManager _categoryManager;
        private IEumelStorage _storage;

        private void ThisAddIn_Startup(object sender, EventArgs e)
        {
            Application.ItemSend += Application_ItemSend;

            //_storage = new OutlookEumelStorageItem(Application.Session.GetDefaultFolder(OlDefaultFolders.olFolderInbox));
            _storage = new FileEumelStorage();
            _categoryManager = new EumelCategoryManager(_storage);
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
            return new BackstageView(()=> _categoryManager);
        }

        #region VSTO generated code

        private void InternalStartup()
        {
            Startup += ThisAddIn_Startup;
        }

        #endregion
    }
}