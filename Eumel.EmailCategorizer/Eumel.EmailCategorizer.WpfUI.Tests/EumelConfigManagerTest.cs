using Eumel.EmailCategorizer.WpfUI.Manager;
using FluentAssertions;
using NUnit.Framework;

namespace Eumel.EmailCategorizer.WpfUI.Manager.Tests
{
    [TestFixture]
    public class EumelConfigManagerTest
    {
        [Test]
        public void Be_Creatable()
        {
            var ctx = new ContextFor<EumelConfigManager>();
            var sut = ctx.BuildSut();

            sut.Should().NotBeNull();
        }
    }
}