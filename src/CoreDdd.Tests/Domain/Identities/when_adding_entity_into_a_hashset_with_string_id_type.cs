using NUnit.Framework;

namespace CoreDdd.Tests.Domain.Identities
{
    [TestFixture]
    public class when_adding_entity_into_a_hashset_with_string_id_type : base_when_adding_entity_into_a_hashset<string>
    {
        protected override string GetId()
        {
            return "string id";
        }
    }
}