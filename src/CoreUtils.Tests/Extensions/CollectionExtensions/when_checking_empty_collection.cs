using CoreUtils.Extensions;
using NUnit.Framework;

namespace CoreUtils.Tests.Extensions.CollectionExtensions
{
    [TestFixture]
    public class when_checking_empty_collection
    {
        [Test]
        public void empty_collection()
        {
            Assert.That(new int[0].IsEmpty(), Is.True);
        }

        [Test]
        public void not_empty_collection()
        {
            Assert.That(new[] { 1, 2 }.IsEmpty(), Is.False);
        }
    }
}