using CoreUtils.Extensions;
using NUnit.Framework;
using Shouldly;

namespace CoreUtils.Tests.Extensions.CollectionExtensions
{
    [TestFixture]
    public class when_checking_empty_collection
    {
        [Test]
        public void empty_collection()
        {
           new int[0].IsEmpty().ShouldBeTrue();
        }

        [Test]
        public void not_empty_collection()
        {
            new[] { 1, 2 }.IsEmpty().ShouldBeFalse();
        }
    }
}