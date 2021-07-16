using Eumel.EmailCategorizer.WpfUI.Model;
using FluentAssertions;
using NUnit.Framework;

namespace Eumel.EmailCategorizer.WpfUI.Manager.Tests.umel.EmailCategorizer.WpfUI.Model
{
    [TestFixture]
    public class ConfigModelTest
    {
        [Test]
        public void Be_Creatable()
        {
            var ctx = new ContextFor<ConfigModel>();
            var sut = ctx.BuildSut();

            sut.Should().NotBeNull();
        }
    }
}