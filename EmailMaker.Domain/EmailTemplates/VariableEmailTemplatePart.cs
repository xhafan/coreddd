namespace EmailMaker.Domain.EmailTemplates
{
    public class VariableEmailTemplatePart : EmailTemplatePart
    {
        public string Value { get; private set; }

        public VariableEmailTemplatePart(string value)
        {
            Value = value;
        }
    }
}