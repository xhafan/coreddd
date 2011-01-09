namespace EmailMaker.Domain.EmailTemplates
{
    public class VariableTemplatePart : TemplatePart
    {
        public string Value { get; private set; }

        public VariableTemplatePart(string value)
        {
            Value = value;
        }
    }
}