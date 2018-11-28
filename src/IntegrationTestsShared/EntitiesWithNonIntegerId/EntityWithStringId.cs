using CoreDdd.Domain;

namespace IntegrationTestsShared.EntitiesWithNonIntegerId
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