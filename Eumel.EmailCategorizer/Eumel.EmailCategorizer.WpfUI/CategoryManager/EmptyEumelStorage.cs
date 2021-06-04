namespace Eumel.EmailCategorizer.WpfUI.CategoryManager
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