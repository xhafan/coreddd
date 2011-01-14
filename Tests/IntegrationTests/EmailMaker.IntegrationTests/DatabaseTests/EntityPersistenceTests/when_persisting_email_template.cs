using System.Configuration;
using System.Linq;
using EmailMaker.Domain.EmailTemplates;
using FluentNHibernate;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using Shouldly;
using TestHelper.Builders.EmailTemplates;
using Configuration = NHibernate.Cfg.Configuration;

namespace EmailMaker.IntegrationTests.DatabaseTests.EntityPersistenceTests
{
    [TestFixture]
    public class when_persisting_email_template
    {
        private ISession _session;
        private EmailTemplate _emailTemplate;

        [TestFixtureSetUp]
        public void Context()
        {
            var configuration = new Configuration();
            configuration.Configure();
            var persistenceModel = new PersistenceModel();
            persistenceModel.AddMappingsFromAssembly(typeof(EmailTemplate).Assembly);
            persistenceModel.Configure(configuration);

            var sessionFactory = configuration.BuildSessionFactory();
            _session = sessionFactory.OpenSession();

            using (var tx = _session.BeginTransaction())
            {
                _session.Delete("from EmailTemplate");
                tx.Commit();
            }
            
            _emailTemplate = EmailTemplateBuilder.New.WithInitialHtml("html").Build();
            using (var tx = _session.BeginTransaction())
            {
                _session.Save(_emailTemplate);
                tx.Commit();
            }
            _session.Clear();
        }

        [Test]
        public void email_template_correctly_retrieved()
        {
            var retrievedEmailTemplate = _session.Get<EmailTemplate>(_emailTemplate.Id);
            retrievedEmailTemplate.Id.ShouldBe(_emailTemplate.Id);
            retrievedEmailTemplate.Parts.Count().ShouldBe(_emailTemplate.Parts.Count());
            foreach (var retrievedPart in retrievedEmailTemplate.Parts)
            {
                var htmlRetrievedPart = retrievedPart as HtmlEmailTemplatePart;
                var htmlPart = _emailTemplate.Parts.First(x => x.Id == htmlRetrievedPart.Id) as HtmlEmailTemplatePart;
                htmlRetrievedPart.Position.ShouldBe(htmlPart.Position);
                htmlRetrievedPart.Html.ShouldBe(htmlPart.Html);
            }

        }
    }
}
