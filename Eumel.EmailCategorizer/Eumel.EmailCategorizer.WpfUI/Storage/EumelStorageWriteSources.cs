using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace Eumel.EmailCategorizer.WpfUI.Storage
{
    public class EumelStorageWriteSources : IItemsSource
    {
        public ItemCollection GetValues()
        {
            var result = new ItemCollection
            {
                nameof(FileEumelStorage),
                nameof(JsonFileEumelStorage),
                nameof(RegistryEumelStorage),
                "OutlookEumelStorage"
            };

            return result;
        }
    }
}