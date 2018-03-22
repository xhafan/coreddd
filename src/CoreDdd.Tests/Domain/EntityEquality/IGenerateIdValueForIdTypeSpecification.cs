namespace CoreDdd.Tests.Domain.EntityEquality
{
    public interface IGenerateIdValueForIdTypeSpecification<out TId>
    {
        TId GetIdForEntityOne();
        TId GetIdForEntityTwo();
    }
}