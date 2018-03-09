using EmailMaker.Domain.EmailTemplates.VariableTypes;

namespace EmailMaker.Domain.Emails
{
    public class VariableEmailPart : EmailPart
    {
        public virtual VariableType VariableType { get; protected set; }
        public virtual string Value { get; protected set; }

        protected VariableEmailPart() { }

        public VariableEmailPart(VariableType variableType, string value)
        {
            VariableType = variableType;
            Value = value;
        }

        public virtual void SetValue(string value)
        {
            _SetValue(value);
        }

        private void _SetValue(string value)
        {
            Value = value ?? string.Empty;
        }
    }
}