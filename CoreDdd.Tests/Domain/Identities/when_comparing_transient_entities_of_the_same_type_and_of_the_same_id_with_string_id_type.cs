using NUnit.Framework;

namespace CoreDdd.Tests.Domain.Identities
{
    [TestFixture]
    public class when_comparing_transient_entities_of_the_same_type_and_of_the_same_id_with_string_id_type 
        : base_when_comparing_transient_entities_of_the_same_type_and_of_the_same_id<string>
    {
    }
}