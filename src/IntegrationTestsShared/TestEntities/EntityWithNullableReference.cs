#if NET6_0_OR_GREATER
#nullable enable
using CoreDdd.Domain;

namespace IntegrationTestsShared.TestEntities;

public class EntityWithNullableReference : Entity, IAggregateRoot
{
    protected EntityWithNullableReference() { }

    public EntityWithNullableReference(EntityWithNullableText? referencedEntity)
    {
        ReferencedEntity = referencedEntity;
    }

    public virtual EntityWithNullableText? ReferencedEntity { get; protected set; }
}
#endif