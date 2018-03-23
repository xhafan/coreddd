using CoreUtils.Extensions;
using NUnit.Framework;
using Shouldly;

namespace CoreUtils.Tests.Extensions.CollectionExtensions
{
    [TestFixture]
    public class when_getting_second_item_from_collection
    {
        [Test]
        public void empty_collection()
        {
            new[] { 1, 2, 3 }.Second().ShouldBe(2);
        }
    }
}