using System;
using System.Linq;
using FluentAssertions;
using Microsoft.Win32;
using NUnit.Framework;

namespace Eumel.EmailCategorizer.WpfUI.Tests
{
    [TestFixture]
    public class RegistryEnvironmentAccessTests
    {
        [Test]
        public void RegistryAccessTests()
        {
            var key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\EUMEL Suite");
            if (key.GetValueNames().Contains("Temp"))
                key.DeleteValue("Temp");
            var store = key.GetValue("Temp") as string;
            _ = store.Should().BeNull();
            key.SetValue("Temp", "JsonFileEumelStorage");
            _ = key.GetValueNames().Contains("Temp").Should().BeTrue();
            key.Close();

            key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\EUMEL Suite");
            store = key.GetValue("Temp") as string;
            _ = store.Should().Be("JsonFileEumelStorage");

            key.DeleteValue("Temp");
            key.GetValueNames().Contains("Temp").Should().BeFalse();
            key.Close();
        }

        [Test]
        public void EnvironmentVariableTests()
        {
            const string foo = "%localappdata%";
            var fooP = Environment.ExpandEnvironmentVariables(foo);
            fooP.Should().NotBe(foo);
            Console.WriteLine($@"{foo} => {fooP}");

            Environment.ExpandEnvironmentVariables(string.Empty).Should().BeEmpty();
        }
    }
}