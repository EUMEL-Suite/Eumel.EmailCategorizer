using Eumel.EmailCategorizer.WpfUI;
using FluentAssertions;
using NUnit.Framework;

namespace Eumel.EmailCategorizer.WpfUI.Manager.Tests.umel.EmailCategorizer.WpfUI
{
    [TestFixture]
    public class EditCategoriesWindowTest
    {
        [Test]
        public void Be_Creatable()
        {
            var ctx = new ContextFor<EditCategoriesWindow>();
            var sut = ctx.BuildSut();

            sut.Should().NotBeNull();
        }
    }
}