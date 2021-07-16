using Eumel.EmailCategorizer.WpfUI.Storage;
using FluentAssertions;
using NUnit.Framework;

namespace Eumel.EmailCategorizer.WpfUI.Manager.Tests.umel.EmailCategorizer.WpfUI.Storage
{
    [TestFixture]
    public class RegistryEumelStorageTest
    {
        [Test]
        public void Be_Creatable()
        {
            var ctx = new ContextFor<RegistryEumelStorage>();
            var sut = ctx.BuildSut();

            sut.Should().NotBeNull();
        }
    }
}