namespace EmailMaker.Domain.EmailTemplates
{
    public class VariableEmailTemplatePart : EmailTemplatePart
    {
        public virtual string Value { get; private set; }

        protected VariableEmailTemplatePart() {}

        public VariableEmailTemplatePart(string value)
        {
            Value = value;
        }
    }
}