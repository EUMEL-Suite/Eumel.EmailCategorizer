namespace Eumel.EmailCategorizer.WpfUI
{
    public class EnhancedSubject
    {
        private const string Opening = "[";
        private const string Closing = "]";
        private readonly string _originalSubject;

        public EnhancedSubject(string subject)
        {
            _originalSubject = subject;
            Parse(subject ?? string.Empty);
        }

        public string Category { get; set; }

        public string Subject { get; set; }

        public override string ToString()
        {
            return Category.IsNullOrWhiteSpace()
                ? Subject
                : $"{Opening}{Category}{Closing} {Subject}";
        }

        private void Parse(string subject)
        {
            // get topic of email and remove topic
            Category = subject.Between(Opening, Closing).Trim();

            // get subject w/o topic
            Subject = subject
                .Replace(Opening + Category + Closing, "")
                .Trim()
                .Replace("  ", " ").Replace("  ", " "); // replace all multiple spaces.
        }

        public void UndoParse()
        {
            Category = string.Empty;
            Subject = _originalSubject;
        }
    }
}