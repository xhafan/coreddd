namespace CoreDdd.Tests.Domain.EntityEquality
{
    public class GenerateIdValueForCompositeIdIdTypeSpecification : IGenerateIdValueForIdTypeSpecification<CompositeId>
    {
        public CompositeId GetIdForEntityOne()
        {
            return new CompositeId(23, 24);
        }

        public CompositeId GetIdForEntityTwo()
        {
            return new CompositeId(23, 25);
        }
    }
}