using NUnit.Framework;

namespace CoreDdd.Tests.Domain.Identities
{
    [TestFixture]
    public class when_getting_entity_id_with_long_id_type : base_when_getting_entity_id<long>
    {
        protected override long GetId()
        {
            return long.MaxValue;
        }
    }
}