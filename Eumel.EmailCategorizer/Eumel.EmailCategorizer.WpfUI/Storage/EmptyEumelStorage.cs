namespace Eumel.EmailCategorizer.WpfUI.Storage
{
    public class EmptyEumelStorage : IEumelStorage
    {
        public bool IsReadOnly { get; } = true;

        public string this[string name]
        {
            get => null;
            set { }
        }
    }
}