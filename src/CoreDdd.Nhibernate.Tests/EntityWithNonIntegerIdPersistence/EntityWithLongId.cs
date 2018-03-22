using CoreDdd.Domain;

namespace CoreDdd.Nhibernate.Tests.EntityWithNonIntegerIdPersistence
{
    public class EntityWithLongId : Entity<long>, IAggregateRoot<long>
    {
    }
}