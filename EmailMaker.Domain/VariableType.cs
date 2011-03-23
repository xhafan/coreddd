using Core.Domain;
using EmailMaker.Domain.EmailTemplates.VariableTypes;

namespace EmailMaker.Domain
{
    public class VariableType : Identity<VariableType>, IAggregateRootEntity
    {
        public static VariableType InputText = new InputTextVariableType();
        public static VariableType AutoText = new AutoTextVariableType();
        public static VariableType List = new ListVariableType();
        public static VariableType Translation = new InputTextVariableType();
        
        public virtual string Name { get; private set; }

        protected internal VariableType() {}
        
        protected internal VariableType(int id, string name)
        {
            _id = id;
            Name = name;
        }
    }
}