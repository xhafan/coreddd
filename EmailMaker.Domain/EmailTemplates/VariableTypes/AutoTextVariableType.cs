using Core.Infrastructure;

namespace EmailMaker.Domain.EmailTemplates.VariableTypes
{
    [IgnoreAutoMap]
    internal class AutoTextVariableType : VariableType
    {
        internal AutoTextVariableType() : base(2, "AutoText")
        {
        }
    }
}