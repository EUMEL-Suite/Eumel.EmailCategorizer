namespace Eumel.EmailCategorizer.WpfUI
{
    /// <summary>
    ///     gets a specific configuration from a persistent store
    /// </summary>
    public interface IEumelStorage
    {
        string this[string name] { get; set; }
    }
}