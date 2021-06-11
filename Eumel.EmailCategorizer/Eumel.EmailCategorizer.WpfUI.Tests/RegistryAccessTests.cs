using System.Linq;
using FluentAssertions;
using Microsoft.Win32;
using NUnit.Framework;

namespace Eumel.EmailCategorizer.WpfUI.Tests
{
    [TestFixture]
    public class RegistryAccessTests
    {
        [Test]
        public void Test()
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\EUMEL Suite");
            if (key.GetValueNames().Contains("Temp"))
                key.DeleteValue("Temp");
            var store = key.GetValue("Temp") as string;
            store.Should().BeNull();
            key.SetValue("Temp", "JsonFileEumelStorage");
            key.GetValueNames().Contains("Temp").Should().BeTrue();
            key.Close();

            key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\EUMEL Suite");
            store = key.GetValue("Temp") as string;
            store.Should().Be("JsonFileEumelStorage");

            key.DeleteValue("Temp");
            key.GetValueNames().Contains("Temp").Should().BeFalse();
            key.Close();
        }
    }
}