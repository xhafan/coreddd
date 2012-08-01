using Core.Domain;

namespace Core.Tests.Domain.Identities
{
    internal class TestEntity<TId> : Entity<TId, TestEntity<TId>>
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