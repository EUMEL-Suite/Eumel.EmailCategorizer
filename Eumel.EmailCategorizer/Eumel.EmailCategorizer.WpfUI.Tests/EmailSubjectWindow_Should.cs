using System.IO;
using System.Threading;
using Eumel.EmailCategorizer.WpfUI.Manager;
using NSubstitute;
using NUnit.Framework;

namespace Eumel.EmailCategorizer.WpfUI.Tests
{
    [TestFixture]
    // ReSharper disable once InconsistentNaming
    public class EmailSubjectWindow_Should : WpfWindowBaseTest
    {
        [Test]
        [Explicit("this opens a UI")]
        [Apartment(ApartmentState.STA)]
        public void Open_Window()
        {
            var categoryManager = Substitute.For<IEumelCategoryManager>();
            categoryManager.Get().ReturnsForAnyArgs(new[] { "Categorizer", "Domse", "Foo1", "Foo2", "Bar1", "Bar2" });

            var window = new EmailSubjectWindow
            {
                Subject = new EnhancedSubject("Fwd: [Eumel] Regression Test Pattern"),
                CategoryManager = categoryManager
            };
            
            window.ShowDialog();
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void Open_Window_And_Make_ScreenShot()
        {
            var categoryManager = Substitute.For<IEumelCategoryManager>();
            categoryManager.Get().ReturnsForAnyArgs(new[] { "Categorizer", "Domse", "Foo1", "Foo2", "Bar1", "Bar2" });

            var window = new EmailSubjectWindow
            {
                Subject = new EnhancedSubject("Fwd: [Eumel] Regression Test Pattern"),
                CategoryManager = categoryManager
            };
            window.Show();
            Directory.CreateDirectory(Assets);
            CreateBitmapFromVisual(window, Assets + "eumel_subjecteditor.png");
            window.Hide();
        }

    }
}