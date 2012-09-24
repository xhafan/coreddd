using NUnit.Framework;

namespace CoreDdd.Tests.Domain.Identities
{
    [TestFixture]
    public class when_adding_entity_into_a_hashset : base_when_adding_entity_into_a_hashset<int>
    {
        protected override int GetId()
        {
            return 12;
        }
    }
}