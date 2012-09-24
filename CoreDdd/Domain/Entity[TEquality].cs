namespace CoreDdd.Domain
{
    public abstract class Entity<TEquality> : Entity<int, TEquality> where TEquality : Entity<int, TEquality>
    {
    }
}