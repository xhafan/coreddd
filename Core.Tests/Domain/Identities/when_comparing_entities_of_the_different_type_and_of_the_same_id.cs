using NUnit.Framework;

namespace Core.Tests.Domain.Identities
{
    [TestFixture]
    public class when_comparing_entities_of_the_different_type_and_of_the_same_id : base_when_comparing_entities_of_the_different_type_and_of_the_same_id<int>
    {
        protected override int GetId()
        {
            return 11;
        }
    }
}