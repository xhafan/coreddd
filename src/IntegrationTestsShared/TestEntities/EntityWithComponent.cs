using CoreDdd.Domain;

namespace IntegrationTestsShared.TestEntities
{
    public class EntityWithComponent : Entity, IAggregateRoot
    {
        protected EntityWithComponent() { }

        public EntityWithComponent(int number, string text)
        {
            MyComponent = new Component(number, text);
        }

        public virtual Component MyComponent { get; protected set; }
    }
}