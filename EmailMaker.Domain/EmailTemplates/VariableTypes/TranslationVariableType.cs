using Core.Infrastructure;

namespace EmailMaker.Domain.EmailTemplates.VariableTypes
{
    [IgnoreAutoMap]
    internal class TranslationVariableType : VariableType
    {
        internal TranslationVariableType() : base(4, "List")
        {
        }
    }
}