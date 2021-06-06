using System;
using System.Windows;
using System.Windows.Input;
using Eumel.EmailCategorizer.WpfUI.Manager;

namespace Eumel.EmailCategorizer.WpfUI
{
    /// <summary>
    /// Interaction logic for EditSettingsWindow.xaml
    /// </summary>
    public partial class EditSettingsWindow : Window
    {
        #region ConfigManager

        public static readonly DependencyProperty ConfigManagerProperty = DependencyProperty.Register(
            "ConfigManager", typeof(IEumelConfigManager), typeof(EditCategoriesWindow),
            new PropertyMetadata(default(IEumelConfigManager), ConfigManagerChanged));

        private static void ConfigManagerChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var window = sender as EditCategoriesWindow ??
                         throw new ArgumentNullException(nameof(sender), @"sender is not an EditCategoriesWindow");
            var newValue = e.NewValue as IEumelConfigManager ?? throw new ArgumentNullException(nameof(e.NewValue));

            CommandManager.InvalidateRequerySuggested();
        }

        public IEumelConfigManager ConfigManager
        {
            get => (IEumelConfigManager)GetValue(ConfigManagerProperty);
            set => SetValue(ConfigManagerProperty, value);
        }


        #endregion ConfigManager
        
        public EditSettingsWindow()
        {
            InitializeComponent();
        }
    }
}
