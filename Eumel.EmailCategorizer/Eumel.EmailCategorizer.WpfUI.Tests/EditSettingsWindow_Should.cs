using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Eumel.EmailCategorizer.WpfUI.Manager;
using Eumel.EmailCategorizer.WpfUI.Model;
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
            configManager.GetConfig().ReturnsForAnyArgs(new ConfigModel()
            {
                ReplyMarker = new List<string>() {"RE:", "AW:"},
                ForwardMarker = new List<string>() {"FW:", "WG:"}
            });

            var window = new EditSettingsWindow()
            {
                Config = new ConfigModel() { ConfigStore = "Hello"}
            };
            window.ShowDialog();
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void Open_Window_And_Make_ScreenShot()
        {
            var configManager = Substitute.For<IEumelConfigManager>();
            configManager.GetConfig().ReturnsForAnyArgs(new ConfigModel()
            {
                ReplyMarker = new List<string>() {"RE:", "AW:"},
                ForwardMarker = new List<string>() {"FW:", "WG:"}
            });

            var window = new EditSettingsWindow()
            {
                Config = new ConfigModel()// configManager
            };

            window.Show();
            Directory.CreateDirectory(Assets);
            CreateBitmapFromVisual(window, Assets + "eumel_editsettings.png");
            window.Hide();
        }
    }
}