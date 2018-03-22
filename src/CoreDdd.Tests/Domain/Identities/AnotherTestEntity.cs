using CoreDdd.Domain;

namespace CoreDdd.Tests.Domain.EntityEquality
{
    public class AnotherTestEntity<TId> : Entity<TId>
    {
        public AnotherTestEntity(TId id)
        {
            Id = id;
        }
    }
}