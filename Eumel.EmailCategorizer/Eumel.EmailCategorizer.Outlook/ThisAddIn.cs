﻿using System;
using Eumel.EmailCategorizer.Outlook.OutlookImpl;
using Eumel.EmailCategorizer.WpfUI;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Outlook;

namespace Eumel.EmailCategorizer.Outlook
{
    public partial class ThisAddIn
    {
        private IEumelCategoryManager categoryManager;
        private IEumelStorage storage;

        private void ThisAddIn_Startup(object sender, EventArgs e)
        {
            Application.ItemSend += Application_ItemSend;

            //storage = new Task<IEumelStorage>(() =>
            //    new OutlookEumelStorageItem(Application.Session.GetDefaultFolder(OlDefaultFolders.olFolderInbox))).Result;
            //categoryManager = new Task<IEumelCategoryManager>(() => new EumelCategoryManager(storage)).Result;
            storage = new OutlookEumelStorageItem(Application.Session.GetDefaultFolder(OlDefaultFolders.olFolderInbox));
            categoryManager = new EumelCategoryManager(storage);
        }

        private void Application_ItemSend(object item, ref bool cancel)
        {
            // since we only handle mail items
            if (!(item is MailItem mail)) return;

            var email = new EnhancedMailItem(mail);

            var window = new EmailSubjectWindow
            {
                Subject = email.Subject,
                CategoryManager = categoryManager
            };
            var dialogResult = window.ShowDialog();
            if (dialogResult.HasValue && dialogResult.Value)
                email.UpdateOriginalMail();
            else
                cancel = true;
        }

        protected override IRibbonExtensibility CreateRibbonExtensibilityObject()
        {
            return new BackstageView();
        }

        #region VSTO generated code

        private void InternalStartup()
        {
            Startup += ThisAddIn_Startup;
        }

        #endregion
    }
}