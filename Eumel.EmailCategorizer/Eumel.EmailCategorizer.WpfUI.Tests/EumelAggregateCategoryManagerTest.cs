using Eumel.EmailCategorizer.WpfUI.Manager;
using FluentAssertions;
using NUnit.Framework;

namespace Eumel.EmailCategorizer.WpfUI.Manager.Tests
{
    [TestFixture]
    public class EumelAggregateCategoryManagerTest
    {
        [Test]
        public void Be_Creatable()
        {
            var ctx = new ContextFor<EumelAggregateCategoryManager>();
            var sut = ctx.BuildSut();

            sut.Should().NotBeNull();
        }
    }
}