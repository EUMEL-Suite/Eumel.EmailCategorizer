using System;
using System.Linq;
using System.Windows;
using Eumel.EmailCategorizer.Outlook.OutlookImpl;

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
        
        public static readonly DependencyProperty CategoryManagerProperty = DependencyProperty.Register(
            "CategoryManager", typeof(IEumelCategoryManager), typeof(EmailSubjectWindow), new PropertyMetadata(default(IEumelCategoryManager), CategoryManagerChanged));

        private static void CategoryManagerChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var window = (sender as EmailSubjectWindow) ?? throw new ArgumentNullException(nameof(sender), @"sender is not an EmailSubjectWindow");
            var newValue = (e.NewValue as IEumelCategoryManager)?? throw new ArgumentNullException(nameof(e.NewValue));
            
            window.Category.Items.Clear();
            newValue.Get().ToList().ForEach(x =>window.Category.Items.Add(x));
        }

        public IEumelCategoryManager CategoryManager
        {
            get { return (IEumelCategoryManager) GetValue(CategoryManagerProperty); }
            set { SetValue(CategoryManagerProperty, value); }
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
