namespace CoreDdd.Tests.Domain.EntityEquality
{
    public class GenerateIdValueForStringIdTypeSpecification : IGenerateIdValueForIdTypeSpecification<string>
    {
        public string GetIdForEntityOne()
        {
            return "string id one";
        }

        public string GetIdForEntityTwo()
        {
            return "string id two";
        }
    }
}