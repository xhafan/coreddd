using CoreDdd.Domain;

namespace CoreDdd.Nhibernate.PersistenceTests
{
    public class TestEntityTwo : Entity, IAggregateRoot
    {
        public virtual void SetId(int id)
        {
            Id = id;
        }
    }
}