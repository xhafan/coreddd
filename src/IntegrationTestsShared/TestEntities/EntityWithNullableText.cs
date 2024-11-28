#if NET8_0_OR_GREATER
#nullable enable
using CoreDdd.Domain;

namespace IntegrationTestsShared.TestEntities;

public class EntityWithNullableText : Entity, IAggregateRoot
{
    protected EntityWithNullableText() { }

    public EntityWithNullableText(string? text)
    {
        Text = text;
    }

    public virtual string? Text { get; protected set; }
}
#endif