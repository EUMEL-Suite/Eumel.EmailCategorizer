using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace Eumel.EmailCategorizer.WpfUI.Storage
{
    public class EumelStorageWriteSources : IItemsSource
    {
        public ItemCollection GetValues()
        {
            var result = new ItemCollection();
            result.Add(nameof(FileEumelStorage));
            result.Add(nameof(JsonFileEumelStorage));
            result.Add(nameof(RegistryEumelStorage));
            result.Add("OutlookEumelStorage");

            return result;
        }
    }
}