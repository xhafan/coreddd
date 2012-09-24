using NUnit.Framework;

namespace CoreDdd.Tests.Domain.Identities
{
    [TestFixture]
    public class when_adding_entity_into_a_hashset_with_long_id_type : base_when_adding_entity_into_a_hashset<long>
    {
        protected override long GetId()
        {
            return long.MaxValue;
        }
    }
}