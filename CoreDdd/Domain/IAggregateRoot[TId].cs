namespace CoreDdd.Domain
{
    public interface IAggregateRoot<out TId>
    {
        TId Id { get; }
    }
}
