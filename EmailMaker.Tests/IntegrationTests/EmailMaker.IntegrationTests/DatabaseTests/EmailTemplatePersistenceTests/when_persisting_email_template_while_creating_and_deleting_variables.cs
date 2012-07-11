using System.Linq;
using Core.TestHelper.Persistence;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.TestHelper.Builders;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.IntegrationTests.DatabaseTests.EmailTemplatePersistenceTests
{
    [TestFixture]
    public class when_persisting_email_template_while_creating_and_deleting_variables : BaseEmailMakerPersistenceTest
    {
        private EmailTemplate _emailTemplate;
        private EmailTemplate _retrievedEmailTemplate;

        protected override void Context() {}

        [Test]
        public void test_multiple_variable_creation_and_deletion_persistence()
        {
            var user = UserBuilder.New.Build();
            Save(user);
            _emailTemplate = new EmailTemplate("12345", null, user.Id);
            Save(_emailTemplate);
            Session.Clear();
            _retrievedEmailTemplate = Get<EmailTemplate>(_emailTemplate.Id);
            _CheckThatRetrievedEmailTemplateIsTheSameAsEmailTemplate();
            
            _emailTemplate = _retrievedEmailTemplate;
            _emailTemplate.CreateVariable(_emailTemplate.Parts.First().Id, 1, 1);
            Save(_emailTemplate);
            Session.Clear();
            _retrievedEmailTemplate = Get<EmailTemplate>(_emailTemplate.Id);
            _CheckThatRetrievedEmailTemplateIsTheSameAsEmailTemplate();

            _emailTemplate = _retrievedEmailTemplate;
            _emailTemplate.CreateVariable(_emailTemplate.Parts.Last().Id, 1, 1);
            Save(_emailTemplate);
            Session.Clear();
            _retrievedEmailTemplate = Get<EmailTemplate>(_emailTemplate.Id);
            _CheckThatRetrievedEmailTemplateIsTheSameAsEmailTemplate();

            _emailTemplate = _retrievedEmailTemplate;
            _emailTemplate.DeleteVariable(_emailTemplate.Parts.ElementAt(1).Id);
            Save(_emailTemplate);
            Session.Clear();
            _retrievedEmailTemplate = Get<EmailTemplate>(_emailTemplate.Id);
            _CheckThatRetrievedEmailTemplateIsTheSameAsEmailTemplate();        
        }

        private void _CheckThatRetrievedEmailTemplateIsTheSameAsEmailTemplate()
        {
            _retrievedEmailTemplate.Id.ShouldBe(_emailTemplate.Id);
            _retrievedEmailTemplate.Parts.Count().ShouldBe(_emailTemplate.Parts.Count());
            var position = 0;
            foreach (var retrievedPart in _retrievedEmailTemplate.Parts)
            {
                var part = _emailTemplate.Parts.First(x => x.Id == retrievedPart.Id);
                retrievedPart.Position.ShouldBe(position++);

                if (retrievedPart is HtmlEmailTemplatePart)
                {
                    var htmlRetrievedPart = (HtmlEmailTemplatePart)retrievedPart;
                    var htmlPart = (HtmlEmailTemplatePart)part;
                    htmlRetrievedPart.Html.ShouldBe(htmlPart.Html);                    
                }
                else if (retrievedPart is VariableEmailTemplatePart)
                {
                    var variableRetrievedPart = (VariableEmailTemplatePart)retrievedPart;
                    var variablePart = (VariableEmailTemplatePart)part;
                    variableRetrievedPart.Value.ShouldBe(variablePart.Value);
                }                
            }
        }
    }
}