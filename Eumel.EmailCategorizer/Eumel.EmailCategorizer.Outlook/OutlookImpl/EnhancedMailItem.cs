using System;
using Eumel.EmailCategorizer.WpfUI;
using Microsoft.Office.Interop.Outlook;

namespace Eumel.EmailCategorizer.Outlook.OutlookImpl
{
    internal class EnhancedMailItem
    {
        private readonly MailItem _mail;

        public EnhancedMailItem(MailItem mail)
        {
            _mail = mail ?? throw new ArgumentNullException(nameof(mail));

            Subject = new EnhancedSubject(mail.Subject);
        }

        public EnhancedSubject Subject { get; }

        public void UpdateOriginalMail()
        {
            _mail.Subject = Subject.ToString();
        }
    }
}