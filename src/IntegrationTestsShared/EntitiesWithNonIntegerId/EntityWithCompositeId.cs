using CoreDdd.Domain;

namespace IntegrationTestsShared.EntitiesWithNonIntegerId
{
    public class EntityWithCompositeId : Entity<CompositeId>, IAggregateRoot
    {
        protected EntityWithCompositeId() {}

        public EntityWithCompositeId(CompositeId id)
        {
            Id = id;
        }
    }
}