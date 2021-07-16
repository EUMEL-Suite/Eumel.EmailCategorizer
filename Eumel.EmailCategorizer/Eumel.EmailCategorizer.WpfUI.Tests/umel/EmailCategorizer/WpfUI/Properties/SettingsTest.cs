using Eumel.EmailCategorizer.WpfUI.Properties;
using FluentAssertions;
using NUnit.Framework;

namespace Eumel.EmailCategorizer.WpfUI.Manager.Tests.umel.EmailCategorizer.WpfUI.Properties
{
    [TestFixture]
    public class SettingsTest
    {
        [Test]
        public void Be_Creatable()
        {
            var ctx = new ContextFor<Settings>();
            var sut = ctx.BuildSut();

            sut.Should().NotBeNull();
        }
    }
}