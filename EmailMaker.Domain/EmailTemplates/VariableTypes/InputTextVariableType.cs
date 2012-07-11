using Core.Domain.Persistence;

namespace EmailMaker.Domain.EmailTemplates.VariableTypes
{
    [IgnoreAutoMap]
    internal class InputTextVariableType : VariableType
    {
        internal InputTextVariableType() : base(1, "InputText")
        {
        }
    }
}