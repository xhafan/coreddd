using System.Collections.Generic;
using CoreTest;
using CoreUtils.Extensions;
using NUnit.Framework;

namespace CoreUtils.Tests.Extensions.CollectionExtensions
{
    [TestFixture]
    public class when_invoking_action_for_each_item : BaseTest
    {
        private List<int> _list;
        private int[] _items;

        [SetUp]
        public void Context()
        {
            _items = new[] {1, 2, 3};
            _list = new List<int>();
            _items.Each(_list.Add);
        }

        [Test]
        public void all_items_are_added_to_list()
        {
            Assert.That(_list, Is.EqualTo(_items));
        }
    }
}