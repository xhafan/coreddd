using CoreDdd.Domain;

namespace CoreDdd.Nhibernate.Tests.TestEntities
{
    public abstract class AbstractEntity : Entity, IAggregateRoot
    {
    }
}