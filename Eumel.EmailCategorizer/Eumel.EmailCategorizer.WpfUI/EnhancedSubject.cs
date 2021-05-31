namespace Eumel.EmailCategorizer.Outlook
{
    public class EnhancedSubject
    {
        private const string Opening = "[";
        private const string Closing = "]";

        public EnhancedSubject(string subject)
        {
            Parse(subject ?? string.Empty);
        }

        public string Category { get; private set; }

        public string Subject { get; private set; }

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
    }
}