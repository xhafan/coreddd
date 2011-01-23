using EmailMaker.Domain.EmailTemplates.VariableTypes;

namespace EmailMaker.Domain.Emails
{
    public class VariableEmailPart : EmailPart
    {
        public virtual VariableType VariableType { get; private set; }
        public virtual string Value { get; private set; }

        protected VariableEmailPart() { }

        public VariableEmailPart(VariableType variableType, string value)
        {
            VariableType = variableType;
            Value = value;
        }
    }
}