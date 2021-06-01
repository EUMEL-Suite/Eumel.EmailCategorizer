using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Eumel.EmailCategorizer.WpfUI
{
    /// <summary>
    /// Interaction logic for EditCategoriesWindow.xaml
    /// </summary>
    public partial class EditCategoriesWindow : Window
    {
        #region CategoryManager

        public static readonly DependencyProperty CategoryManagerProperty = DependencyProperty.Register(
            "CategoryManager", typeof(IEumelCategoryManager), typeof(EditCategoriesWindow),
            new PropertyMetadata(default(IEumelCategoryManager), CategoryManagerChanged));

        private static void CategoryManagerChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var window = sender as EditCategoriesWindow ??
                         throw new ArgumentNullException(nameof(sender), @"sender is not an EditCategoriesWindow");
            var newValue = e.NewValue as IEumelCategoryManager ?? throw new ArgumentNullException(nameof(e.NewValue));

            window.CategoryList.ItemsSource = newValue.Get();
            //newValue.Get().ToList().ForEach(x => window.CategoryList.Items.Add(x));
        }

        public IEumelCategoryManager CategoryManager
        {
            get => (IEumelCategoryManager)GetValue(CategoryManagerProperty);
            set => SetValue(CategoryManagerProperty, value);
        }

        public IEnumerable<string> Categories => CategoryManager.Get();

        #endregion

        public EditCategoriesWindow()
        {
            InitializeComponent();
        }

        private void CancelButton(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SaveButton(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DeleteCategory(object sender, RoutedEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;

            while ((dep != null) && !(dep is ListViewItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
                return;

            int index = CategoryList.ItemContainerGenerator.IndexFromContainer(dep);

            // TODO two way binding
            //People.RemoveAt(index);
        }
    }
}
