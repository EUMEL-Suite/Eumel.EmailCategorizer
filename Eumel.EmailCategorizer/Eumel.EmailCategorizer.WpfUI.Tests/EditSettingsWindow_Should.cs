using System.IO;
using System.Threading;
using Eumel.EmailCategorizer.WpfUI.Model;
using NUnit.Framework;

namespace Eumel.EmailCategorizer.WpfUI.Tests
{
    [TestFixture]
    // ReSharper disable once InconsistentNaming
    public class EditSettingsWindow_Should : WpfWindowBaseTest
    {
        [Test]
        [Explicit("this opens a UI")]
        [Apartment(ApartmentState.STA)]
        public void Open_Window()
        {
            var window = new EditSettingsWindow()
            {
                Config = ConfigModel.Default()
            };
            window.ShowDialog();
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void Open_Window_And_Make_ScreenShot()
        {
            var window = new EditSettingsWindow()
            {
                Config = ConfigModel.Default()
            };

            window.Show();
            Directory.CreateDirectory(Assets);
            CreateBitmapFromVisual(window, Assets + "eumel_editsettings.png");
            window.Hide();
        }
    }
}