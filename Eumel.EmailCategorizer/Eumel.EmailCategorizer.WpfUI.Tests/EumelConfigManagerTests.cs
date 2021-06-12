using Eumel.EmailCategorizer.WpfUI.Manager;
using Eumel.EmailCategorizer.WpfUI.Model;
using Eumel.EmailCategorizer.WpfUI.Storage;
using NSubstitute;
using NUnit.Framework;

namespace Eumel.EmailCategorizer.WpfUI.Tests
{
    [TestFixture]
    public class EumelConfigManagerTests
    {
        [Test]
        [TestCase("CategoryPrefix", "[")]
        [TestCase("CategoryPostfix", "]")]
        [TestCase("ForwardMarker", "FW:|WG:")]
        public void Check_If_Props_Saved(string propName, string value)
        {
            var storage = Substitute.For<IEumelStorage>();

            var sut = new EumelConfigManager(storage);

            var cfg = ConfigModel.Default();

            sut.Save(cfg);

            storage.Received(1)["Eumel.Categorizer." + propName] = value;
        }
    }
}