namespace Eumel.EmailCategorizer.WpfUI.Storage
{
    /// <summary>
    ///     gets a specific configuration from a persistent store.
    ///     Important: This is a key-value store (a list of string, string)
    /// </summary>
    public interface IEumelStorage
    {
        bool IsReadOnly { get; }
        string this[string name] { get; set; }
        void Clear();
    }
}