using NUnit.Framework;

namespace Core.Tests.Domain.Identities
{
    [TestFixture]
    public class when_comparing_entities_of_the_same_type_and_of_the_different_id : base_when_comparing_entities_of_the_same_type_and_of_the_different_id<int>
    {
        protected override int GetIdOne()
        {
            return 11;
        }

        protected override int GetIdTwo()
        {
            return 12;
        }
    }
}