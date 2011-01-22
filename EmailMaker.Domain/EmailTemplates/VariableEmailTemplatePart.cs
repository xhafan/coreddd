using EmailMaker.Domain.EmailTemplates.VariableTypes;

namespace EmailMaker.Domain.EmailTemplates
{
    public class VariableEmailTemplatePart : EmailTemplatePart
    {
        public virtual VariableType VariableType { get; private set; }
        public virtual string Value { get; private set; }

        protected VariableEmailTemplatePart() {}

        public VariableEmailTemplatePart(string value)
        {
            VariableType = VariableType.InputText;
            Value = value;
        }
    }
}