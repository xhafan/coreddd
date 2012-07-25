using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using EmailMaker.Domain.EmailTemplates;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.UnitTests.Domain.EmailTemplates
{
    // todo: Temporarily commented out until templates are multilingual
    //    [TestFixture]
//    public class when_creating_email_template_names_with_culture
//    {
//        private EmailTemplate _emailTemplate;
//        private CultureInfo english = new CultureInfo("en");
//        private CultureInfo japanese = new CultureInfo("ja");
//        [SetUp]
//        public void Context()
//        {
//            var templateNames = new Dictionary<CultureInfo, string>
//                                    {
//                                        {english , "englishTemplateName"},
//                                        {japanese, "japaneseTemplateName"}
//                                    };
//            
//            _emailTemplate = new EmailTemplate(templateNames);
//        }
//
//        [Test]
//        public void email_template_retrieved_correctly()
//        {
//            _emailTemplate.Names.Count().ShouldBe(2);
//            var template = _emailTemplate.Names[english];
//            template.ShouldBe("englishTemplateName");
//
//            var templateJa = _emailTemplate.Names[japanese];
//            templateJa.ShouldBe("japaneseTemplateName");
//            
//        }
//    }
}