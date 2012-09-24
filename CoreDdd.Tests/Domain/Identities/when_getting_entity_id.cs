using NUnit.Framework;

namespace CoreDdd.Tests.Domain.Identities
{
    [TestFixture]
    public class when_getting_entity_id : base_when_getting_entity_id<int>
    {
        protected override int GetId()
        {
            return 12;
        }
    }
}
