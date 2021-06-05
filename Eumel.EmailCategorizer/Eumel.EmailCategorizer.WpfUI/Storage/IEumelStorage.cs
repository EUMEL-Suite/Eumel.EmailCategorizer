namespace Eumel.EmailCategorizer.WpfUI.Storage
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