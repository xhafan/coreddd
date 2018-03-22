namespace CoreDdd.Tests.Domain.EntityEquality
{
    public class GenerateIdValueForLongIdTypeSpecification : IGenerateIdValueForIdTypeSpecification<long>
    {
        public long GetIdForEntityOne()
        {
            return long.MaxValue;
        }

        public long GetIdForEntityTwo()
        {
            return long.MaxValue - 1;
        }
    }
}