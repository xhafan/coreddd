using CoreDdd.Domain;

namespace CoreDdd.Nhibernate.Tests.EntityWithNonIntegerIdPersistence
{
    public class EntityWithStringId : Entity<string>, IAggregateRoot
    {
        public EntityWithStringId() {}

        public EntityWithStringId(string id)
        {
            Id = id;
        }
    }
}