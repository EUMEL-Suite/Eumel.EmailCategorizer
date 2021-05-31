using Eumel.EmailCategorizer.WpfUI;
using Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;

namespace Eumel.EmailCategorizer.Outlook
{
    public partial class ThisAddIn
    {
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            Application.ItemSend += Application_ItemSend;
        }

        private void Application_ItemSend(object item, ref bool cancel)
        {
            // since we only handle mail items
            if (!(item is MailItem mail)) return;

            var email = new EnhancedMailItem(mail);

            var window = new EmailSubjectWindow()
            {
                Subject = email.Subject,
            };
            var dialogResult = window.ShowDialog();
            if (dialogResult.HasValue && dialogResult.Value)
            {
                email.UpdateOriginalMail();
            }
            else
            {
                cancel = true;
            }
        }

        #region VSTO generated code

        private void InternalStartup()
        {
            Startup += ThisAddIn_Startup;
        }

        #endregion
    }
}
