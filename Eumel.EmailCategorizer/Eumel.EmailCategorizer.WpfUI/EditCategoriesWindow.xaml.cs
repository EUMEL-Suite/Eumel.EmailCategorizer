using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Eumel.EmailCategorizer.WpfUI.Manager;
using Eumel.EmailCategorizer.WpfUI.Model;

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

            window.Categories = new ObservableCollection<CategoryModel>(
                newValue.Get().Select(x => new CategoryModel() { Name = x }));
            CommandManager.InvalidateRequerySuggested();
        }

        public IEumelCategoryManager CategoryManager
        {
            get => (IEumelCategoryManager)GetValue(CategoryManagerProperty);
            set => SetValue(CategoryManagerProperty, value);
        }


        #endregion CategoryManager

        #region Categories

        public static readonly DependencyProperty CategoriesProperty = DependencyProperty.Register(
            "Categories", typeof(ObservableCollection<CategoryModel>), typeof(EditCategoriesWindow), new PropertyMetadata(default(ObservableCollection<CategoryModel>)));

        private readonly IList<CategoryModel> _deletedCategories = new List<CategoryModel>();

        public ObservableCollection<CategoryModel> Categories
        {
            get => (ObservableCollection<CategoryModel>)GetValue(CategoriesProperty);
            set => SetValue(CategoriesProperty, value);
        }

        #endregion Categories

        public EditCategoriesWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void CancelButton(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void SaveButton(object sender, RoutedEventArgs e)
        {
            foreach (var item in Categories)
                CategoryManager.Add(item.Name);
            foreach (var item in _deletedCategories)
                CategoryManager.Delete(item.Name);

            DialogResult = true;
            Close();
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
            _deletedCategories.Add(Categories.ElementAt(index));
            Categories.RemoveAt(index);
        }

        private void ClearAllButton(object sender, RoutedEventArgs e)
        {
            Categories.Clear();
        }
    }
}
