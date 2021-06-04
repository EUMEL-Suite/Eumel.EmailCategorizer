namespace Eumel.EmailCategorizer.WpfUI.CategoryManager
{
    /// <summary>
    ///     gets a specific configuration from a persistent store
    /// </summary>
    public interface IEumelStorage
    {
        bool IsReadOnly { get; }

        string this[string name] { get; set; }
    }
}