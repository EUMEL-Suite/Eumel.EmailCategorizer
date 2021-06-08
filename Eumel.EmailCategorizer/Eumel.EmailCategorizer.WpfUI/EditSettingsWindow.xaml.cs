using System.Windows;
using Eumel.EmailCategorizer.WpfUI.Model;

namespace Eumel.EmailCategorizer.WpfUI
{
    /// <summary>
    /// Interaction logic for EditSettingsWindow.xaml
    /// </summary>
    public partial class EditSettingsWindow
    {
        #region Config

        public static readonly DependencyProperty ConfigProperty = DependencyProperty.Register(
            "Config", typeof(ConfigModel), typeof(EditSettingsWindow), new PropertyMetadata(default(ConfigModel), PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var window = d as EditSettingsWindow;
            var newValue = e.NewValue as ConfigModel;

            window.PropertyEditor.SelectedObject = newValue;
        }

        public ConfigModel Config
        {
            get => (ConfigModel)GetValue(ConfigProperty);
            set => SetValue(ConfigProperty, value);
        }

        #endregion Config

        public EditSettingsWindow()
        {
            InitializeComponent();
        }

        private void CancelButton(object sender, RoutedEventArgs e)
        {
        }

        private void SaveButton(object sender, RoutedEventArgs e)
        {

        }
    }
}
