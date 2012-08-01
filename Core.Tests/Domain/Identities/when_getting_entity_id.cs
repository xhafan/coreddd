using NUnit.Framework;

namespace Core.Tests.Domain.Identities
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
