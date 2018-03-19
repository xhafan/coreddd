using CoreDdd.Domain;

namespace CoreDdd.Nhibernate.PersistenceTests
{
    public class TestEntityOne : Entity, IAggregateRoot
    {
        public virtual void SetId(int id)
        {
            Id = id;
        }
    }
}