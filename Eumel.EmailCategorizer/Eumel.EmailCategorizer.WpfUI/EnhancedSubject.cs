namespace Eumel.EmailCategorizer.WpfUI
{
    public class EnhancedSubject
    {
        private readonly string _opening;
        private readonly string _closing;
        private readonly string _originalSubject;

        public EnhancedSubject(string subject, string opening, string closing)
        {
            _originalSubject = subject;
            _opening = opening;
            _closing = closing;
            Parse(subject ?? string.Empty);
        }

        public string Category { get; set; }

        public string Subject { get; set; }

        public override string ToString()
        {
            return Category.IsNullOrWhiteSpace()
                ? Subject
                : $"{_opening}{Category}{_closing} {Subject}";
        }

        private void Parse(string subject)
        {
            // get topic of email and remove topic
            Category = subject.Between(_opening, _closing).Trim();

            // get subject w/o topic
            Subject = subject
                .Replace(_opening + Category + _closing, "")
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