using CoreDdd.Domain;

namespace CoreDdd.Tests.Domain.Identities
{
    internal class AnotherTestEntity<TId> : Entity<TId, AnotherTestEntity<TId>>
    {
        public AnotherTestEntity()
        {            
        }

        public AnotherTestEntity(TId id)
        {
            Id = id;
        }
    }
}