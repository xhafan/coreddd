#nullable enable
using CoreDdd.Domain;

namespace IntegrationTestsShared.TestEntities;

public class EntityWithNullableGetOnlyProperty : Entity, IAggregateRoot
{
    protected EntityWithNullableGetOnlyProperty() { }

    public EntityWithNullableGetOnlyProperty(string? text)
    {
        Text = text;
    }

    public virtual string? Text { get; }
}