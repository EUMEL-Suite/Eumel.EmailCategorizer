using Eumel.EmailCategorizer.WpfUI.Model;
using FluentAssertions;
using NUnit.Framework;

namespace Eumel.EmailCategorizer.WpfUI.Manager.Tests.umel.EmailCategorizer.WpfUI.Model
{
    [TestFixture]
    public class CategoryModelTest
    {
        [Test]
        public void Be_Creatable()
        {
            var ctx = new ContextFor<CategoryModel>();
            var sut = ctx.BuildSut();

            sut.Should().NotBeNull();
        }
    }
}