namespace CoreDdd.Tests.Domain.EntityEquality
{
    public class GenerateIdValueForIntegerIdTypeSpecification : IGenerateIdValueForIdTypeSpecification<int>
    {
        public int GetIdForEntityOne()
        {
            return 23;
        }

        public int GetIdForEntityTwo()
        {
            return 24;
        }
    }
}