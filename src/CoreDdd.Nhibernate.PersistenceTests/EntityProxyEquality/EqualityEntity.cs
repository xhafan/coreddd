using CoreDdd.Domain;

namespace CoreDdd.Nhibernate.PersistenceTests.EntityProxyEquality
{
    public class EqualityEntity : Entity, IAggregateRoot
    {
        public virtual void SetId(int id)
        {
            Id = id;
        }
    }
}