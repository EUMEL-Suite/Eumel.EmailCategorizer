using System.IO;
using System.Threading;
using Eumel.EmailCategorizer.WpfUI.Manager;
using NSubstitute;
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
            var configManager = Substitute.For<IEumelConfigManager>();
            configManager.ReplyMarker.ReturnsForAnyArgs(new[] { "RE:", "AW:" });
            configManager.ForwardMarker.ReturnsForAnyArgs(new[] { "FW:", "WG:" });

            var window = new EditSettingsWindow()
            {
                ConfigManager = configManager
            };
            window.Show();
            Directory.CreateDirectory(Assets);
            CreateBitmapFromVisual(window, Assets + "eumel_editsettings.png");
            window.Hide();

            window.ShowDialog();
        }
    }
}