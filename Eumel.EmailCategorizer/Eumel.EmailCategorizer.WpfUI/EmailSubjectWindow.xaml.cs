using System;
using System.Windows;
using Eumel.EmailCategorizer.Outlook;

namespace Eumel.EmailCategorizer.WpfUI
{
    /// <summary>
    /// Interaction logic for EmailSubjectWindow.xaml
    /// </summary>
    public partial class EmailSubjectWindow
    {
        public static readonly DependencyProperty SubjectProperty = DependencyProperty.Register(
            "Subject", typeof(EnhancedSubject), typeof(EmailSubjectWindow), new PropertyMetadata(default(EnhancedSubject), SubjectChanged));

        private static void SubjectChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var window = (sender as EmailSubjectWindow) ?? throw new ArgumentNullException(nameof(sender), @"sender is not an EmailSubjectWindow");
            var newValue = (e.NewValue as EnhancedSubject) ?? new EnhancedSubject("");
            window.Category.Text = newValue.Category;
            window.MailSubject.Text = newValue.Subject;
        }

        public EnhancedSubject Subject
        {
            get => (EnhancedSubject)GetValue(SubjectProperty);
            set => SetValue(SubjectProperty, value);
        }
        public EmailSubjectWindow()
        {
            InitializeComponent();
        }

        private void SendButton(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelButton(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
