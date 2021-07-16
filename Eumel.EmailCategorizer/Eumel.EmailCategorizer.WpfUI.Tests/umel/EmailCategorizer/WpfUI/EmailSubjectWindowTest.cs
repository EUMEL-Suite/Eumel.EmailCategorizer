using Eumel.EmailCategorizer.WpfUI;
using FluentAssertions;
using NUnit.Framework;

namespace Eumel.EmailCategorizer.WpfUI.Manager.Tests.umel.EmailCategorizer.WpfUI
{
    [TestFixture]
    public class EmailSubjectWindowTest
    {
        [Test]
        public void Be_Creatable()
        {
            var ctx = new ContextFor<EmailSubjectWindow>();
            var sut = ctx.BuildSut();

            sut.Should().NotBeNull();
        }
    }
}