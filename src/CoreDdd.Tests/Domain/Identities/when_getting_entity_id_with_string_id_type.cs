using NUnit.Framework;

namespace CoreDdd.Tests.Domain.Identities
{
    [TestFixture]
    public class when_getting_entity_id_with_string_id_type : base_when_getting_entity_id<string>
    {
        protected override string GetId()
        {
            return "string id";
        }
    }
}