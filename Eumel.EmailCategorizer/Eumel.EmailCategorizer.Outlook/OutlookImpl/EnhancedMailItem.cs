using System;
using Eumel.EmailCategorizer.WpfUI;
using Eumel.EmailCategorizer.WpfUI.Manager;
using Microsoft.Office.Interop.Outlook;

namespace Eumel.EmailCategorizer.Outlook.OutlookImpl
{
    internal class EnhancedMailItem
    {
        private readonly MailItem _mail;

        public EnhancedMailItem(MailItem mail, IEumelConfigManager configManager)
        {
            _mail = mail ?? throw new ArgumentNullException(nameof(mail));

            var cfg = configManager.GetConfig();
            Subject = new EnhancedSubject(mail.Subject,cfg.CategoryPrefix, cfg.CategoryPostfix);
        }

        public EnhancedSubject Subject { get; }

        public void UpdateOriginalMail()
        {
            _mail.Subject = Subject.ToString();
        }
    }
}