using System.Collections.Generic;
using EmailMaker.Commands.Handlers;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.UnitTests.Commands.EmailTemplates
{
    [TestFixture]
    public class when_parsing_recipients
    {
        private IDictionary<string, string> _result;

        [SetUp]
        public void Context()
        {
            var parser = new RecipientParser();
            _result = parser.Parse(@"""Tomas Marny"" <tomas.marny@test.com>, John Smith <john.smith@test.com>
                          zeds.dead@baby.com; ");
        }

        [Test]
        public void recipients_correctly_retrieved()
        {
            _result.Count.ShouldBe(3);
            _result["tomas.marny@test.com"].ShouldBe("Tomas Marny");
            _result["john.smith@test.com"].ShouldBe("John Smith");
            _result["zeds.dead@baby.com"].ShouldBe(string.Empty);
        }
    }
}