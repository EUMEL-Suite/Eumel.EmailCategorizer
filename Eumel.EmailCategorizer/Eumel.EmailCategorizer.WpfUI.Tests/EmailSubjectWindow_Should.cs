using System;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Eumel.EmailCategorizer.Outlook.OutlookImpl;
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
            if (target == null || string.IsNullOrEmpty(fileName))
            {
                return;
            }

            Rect bounds = VisualTreeHelper.GetDescendantBounds(target);

            RenderTargetBitmap renderTarget = new RenderTargetBitmap((Int32)bounds.Width, (Int32)bounds.Height, 96, 96, PixelFormats.Pbgra32);

            DrawingVisual visual = new DrawingVisual();

            using (DrawingContext context = visual.RenderOpen())
            {
                VisualBrush visualBrush = new VisualBrush(target);
                context.DrawRectangle(visualBrush, null, new Rect(new Point(), bounds.Size));
            }

            renderTarget.Render(visual);
            PngBitmapEncoder bitmapEncoder = new PngBitmapEncoder();
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
            categoryManager.Get().ReturnsForAnyArgs(new[] {"Categorizer", "Domse"});
            
            var window = new EmailSubjectWindow()
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
    }
}
