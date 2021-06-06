using System.IO;
using System.Threading;
using Eumel.EmailCategorizer.WpfUI.Manager;
using NSubstitute;
using NUnit.Framework;

namespace Eumel.EmailCategorizer.WpfUI.Tests
{
    [TestFixture]
    // ReSharper disable once InconsistentNaming
    public class EditCategoriesWindow_Should : WpfWindowBaseTest
    {
        [Test]
        [Explicit("this opens a UI")]
        [Apartment(ApartmentState.STA)]
        public void OpenWindow_ClearAtLeastOne_Except_EumelCategories()
        {
            var items = new[] { "Categorizer", "Domse", "CodeQualityCoach", "Training", "GitHub", "Protography" };
            var categoryManager = Substitute.For<IEumelCategoryManager>();
            categoryManager.Get().ReturnsForAnyArgs(items);

            var window = new EditCategoriesWindow()
            {
                CategoryManager = categoryManager
            };

            // lets create a screenshot
            window.Show();
            Directory.CreateDirectory(Assets);
            CreateBitmapFromVisual(window, Assets + "eumel_categoryeditor.png");
            window.Hide();

            window.ShowDialog();

            categoryManager.Received().Add("Categorizer");
            categoryManager.Received().Add("Domse");
            categoryManager.Received().Delete("GitHub");
        }
    }
}