using CoreDdd.Domain;

namespace CoreDdd.Tests.Domain.EntityEquality
{
    public class TestEntity<TId> : Entity<TId>
    {
        public TestEntity()
        {            
        }

        public TestEntity(TId id)
        {
            Id = id;
        }

        public void SetId(TId id)
        {
            Id = id;
        }
    }
}