using CoreDdd.Nhibernate.Configurations;

namespace EmailMaker.Domain.EmailTemplates.VariableTypes
{
    [IgnoreAutoMap]
    internal class ListVariableType : VariableType
    {
        internal ListVariableType() : base(3, "List")
        {
        }
    }
}