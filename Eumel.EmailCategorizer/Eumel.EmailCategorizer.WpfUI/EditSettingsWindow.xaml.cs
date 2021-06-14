using System;
using System.Windows;
using Eumel.EmailCategorizer.WpfUI.Manager;
using Eumel.EmailCategorizer.WpfUI.Model;

namespace Eumel.EmailCategorizer.WpfUI
{
    /// <summary>
    ///     Interaction logic for EditSettingsWindow.xaml
    /// </summary>
    public partial class EditSettingsWindow
    {
        public EditSettingsWindow()
        {
            InitializeComponent();

            Config = Manager?.GetConfig();
        }

        public IEumelConfigManager Manager
        {
            get => _manager;
            set
            {
                _manager = value;
                Config = Manager.GetConfig();
            }
        }

        private void CancelButton(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void SaveButton(object sender, RoutedEventArgs e)
        {
            Manager.Save(Config);
            DialogResult = true;
            Close();
        }

        private void ClearButton(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        #region Config

        public static readonly DependencyProperty ConfigProperty = DependencyProperty.Register(
            "Config", typeof(ConfigModel), typeof(EditSettingsWindow),
            new PropertyMetadata(default(ConfigModel), PropertyChangedCallback));

        private IEumelConfigManager _manager;

        private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var window = d as EditSettingsWindow;
            var newValue = e.NewValue as ConfigModel;

            window.PropertyEditor.SelectedObject = newValue;
        }

        public ConfigModel Config
        {
            get => (ConfigModel) GetValue(ConfigProperty);
            set => SetValue(ConfigProperty, value);
        }

        #endregion Config
    }
}