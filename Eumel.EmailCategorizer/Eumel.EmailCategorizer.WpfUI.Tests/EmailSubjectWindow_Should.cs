﻿using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Eumel.EmailCategorizer.WpfUI.Manager;
using NSubstitute;
using NUnit.Framework;

namespace Eumel.EmailCategorizer.WpfUI.Tests
{
    [TestFixture]
    // ReSharper disable once InconsistentNaming
    public class EmailSubjectWindow_Should
    {
        private const string Assets = "C:\\Workspaces\\Eumel.EmailCategorizer\\Assets\\";

        public static void CreateBitmapFromVisual(Visual target, string fileName)
        {
            if (target == null || string.IsNullOrEmpty(fileName)) return;

            var bounds = VisualTreeHelper.GetDescendantBounds(target);

            var renderTarget =
                new RenderTargetBitmap((int) bounds.Width, (int) bounds.Height, 96, 96, PixelFormats.Pbgra32);

            var visual = new DrawingVisual();

            using (var context = visual.RenderOpen())
            {
                var visualBrush = new VisualBrush(target);
                context.DrawRectangle(visualBrush, null, new Rect(new Point(), bounds.Size));
            }

            renderTarget.Render(visual);
            var bitmapEncoder = new PngBitmapEncoder();
            bitmapEncoder.Frames.Add(BitmapFrame.Create(renderTarget));
            using (Stream stm = File.Create(fileName))
            {
                bitmapEncoder.Save(stm);
            }
        }

        [Test]
        [Explicit("this opens a UI")]
        [Apartment(ApartmentState.STA)]
        public void Open_Window()
        {
            var categoryManager = Substitute.For<IEumelCategoryManager>();
            categoryManager.Get().ReturnsForAnyArgs(new[] { "Categorizer", "Domse", "Foo1", "Foo2", "Bar1","Bar2" });

            var window = new EmailSubjectWindow
            {
                Subject = new EnhancedSubject("Fwd: [Eumel] Regression Test Pattern"),
                CategoryManager = categoryManager
            };
            window.Show();
            Directory.CreateDirectory(Assets);
            CreateBitmapFromVisual(window, Assets + "eumel_subjecteditor.png");
            window.Hide();

            window.ShowDialog();
        }

        [Test]
        [Explicit("this opens a UI")]
        [Apartment(ApartmentState.STA)]
        public void Open_EditCategoriesWindow_ClearAtLeastOne_Except_EumelCategories()
        {
            var items = new[] {"Categorizer", "Domse", "CodeQualityCoach", "Training", "GitHub", "Protography"};
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