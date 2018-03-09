using CoreDdd.Domain;

namespace CoreDdd.Tests.Domain.Identities
{
    internal class TestEntity<TId> : Entity<TId>
    {
        public TestEntity()
        {            
        }

        public TestEntity(TId id)
        {
            Id = id;
        }
    }

}