using System;
using Microsoft.Office.Interop.Outlook;

namespace Eumel.EmailCategorizer.Outlook
{
    internal class EnhancedMailItem
    {
        private readonly MailItem _mail;
        public EnhancedSubject Subject { get; }

        public EnhancedMailItem(MailItem mail)
        {
            _mail = mail ?? throw new ArgumentNullException(nameof(mail));

            Subject = new EnhancedSubject(mail.Subject);
        }

        public void UpdateOriginalMail()
        {
            _mail.Subject = Subject.ToString();
        }
    }
}