using CoreDdd.Domain;

namespace CoreDdd.Nhibernate.Tests.EntityWithNonIntegerIdPersistence
{
    public class EntityWithCompositeId : Entity<CompositeId>, IAggregateRoot<CompositeId>
    {
        protected EntityWithCompositeId() {}

        public EntityWithCompositeId(CompositeId id)
        {
            Id = id;
        }
    }
}