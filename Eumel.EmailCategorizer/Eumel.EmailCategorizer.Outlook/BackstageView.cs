using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Eumel.EmailCategorizer.WpfUI;
using Eumel.EmailCategorizer.WpfUI.Manager;
using Office = Microsoft.Office.Core;

namespace Eumel.EmailCategorizer.Outlook
{
    [ComVisible(true)]
    public class BackstageView : Office.IRibbonExtensibility
    {
        private readonly Func<IEumelCategoryManager> _categoryManager;
        private Office.IRibbonUI ribbon;

        public BackstageView(Func<IEumelCategoryManager> categoryManager)
        {
            _categoryManager = categoryManager ?? throw new ArgumentNullException(nameof(categoryManager));
        }

        #region IRibbonExtensibility Members

        public string GetCustomUI(string ribbonID)
        {
            return GetResourceText("Eumel.EmailCategorizer.Outlook.BackstageView.xml");
        }

        #endregion

        #region Ribbon Callbacks

        //Create callback methods here. For more information about adding callback methods, visit https://go.microsoft.com/fwlink/?LinkID=271226

        public void Ribbon_Load(Office.IRibbonUI ribbonUI)
        {
            ribbon = ribbonUI;
        }

        public void EditCategoriesClick(Office.IRibbonControl control)
        {
            var editWindow = new EditCategoriesWindow() { CategoryManager = _categoryManager() };
            editWindow.ShowDialog();
        }

        #endregion

        #region Helpers

        private static string GetResourceText(string resourceName)
        {
            var asm = Assembly.GetExecutingAssembly();
            var resourceNames = asm.GetManifestResourceNames();
            for (var i = 0; i < resourceNames.Length; ++i)
                if (string.Compare(resourceName, resourceNames[i], StringComparison.OrdinalIgnoreCase) == 0)
                    using (var resourceReader = new StreamReader(asm.GetManifestResourceStream(resourceNames[i])))
                    {
                        if (resourceReader != null) return resourceReader.ReadToEnd();
                    }

            return null;
        }

        #endregion
    }
}