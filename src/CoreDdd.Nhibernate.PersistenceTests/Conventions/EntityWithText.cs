using CoreDdd.Domain;

namespace CoreDdd.Nhibernate.PersistenceTests.Conventions
{
    public class EntityWithText : Entity, IAggregateRoot
    {
        protected EntityWithText() { }

        public EntityWithText(string text)
        {
            Text = text;
        }

        public virtual string Text { get; protected set; }
    }
}