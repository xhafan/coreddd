using Core.Domain;

namespace EmailMaker.Domain.EmailTemplates.VariableTypes
{
    public class VariableType : Identity<VariableType>, IAggregateRoot
    {
        public static VariableType InputText = new InputTextVariableType();
        public static VariableType AutoText = new AutoTextVariableType();
        public static VariableType List = new ListVariableType();
        public static VariableType Translation = new TranslationVariableType();
        
        public virtual string Name { get; protected set; }

        protected internal VariableType() {}
        
        protected internal VariableType(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}