using CoreDdd.Domain;

namespace IntegrationTestsShared.EntitiesWithNonIntegerId
{
    public class EntityWithLongId : Entity<long>, IAggregateRoot
    {
    }
}