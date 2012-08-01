using NUnit.Framework;

namespace Core.Tests.Domain.Identities
{
    [TestFixture]
    public class when_comparing_entities_of_the_same_type_and_of_the_different_id_with_long_id_type : base_when_comparing_entities_of_the_same_type_and_of_the_different_id<long>
    {
        protected override long GetIdOne()
        {
            return long.MaxValue;
        }

        protected override long GetIdTwo()
        {
            return long.MaxValue - 1;
        }
    }
}