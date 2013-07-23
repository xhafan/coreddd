using System;
using System.Collections.Generic;
using System.Linq;
using CoreDdd.Tests.Helpers.Extensions;
using CoreUtils.Extensions;
using EmailMaker.Domain.EmailTemplates;

namespace EmailMaker.TestHelper.Builders
{
    public class EmailTemplateBuilder
    {       
        private string _initialHtml = "html";
        private int _nextPartId;
        private readonly IList<Tuple<int, int>> _variables = new List<Tuple<int, int>>();
        private int _id;
        private string _name = "name";
        private int _userId;

        private int NextPartId
        {
            get
            {
                return _nextPartId++;
            }
        }

        public static EmailTemplateBuilder New
        {
            get
            {
                return new EmailTemplateBuilder();
            }
        }

        public EmailTemplateBuilder WithInitialHtml(string html)
        {
            _initialHtml = html;
            return this;
        }

        public EmailTemplateBuilder WithVariable(int startIndexOfLastHtmlPart, int length)
        {
            _variables.Add(new Tuple<int, int>(startIndexOfLastHtmlPart, length));
            return this;
        }

        public EmailTemplateBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public EmailTemplateBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public EmailTemplateBuilder WithUserId(int userId)
        {
            _userId = userId;
            return this;
        }

        public EmailTemplate Build()
        {
            var emailTemplate = new EmailTemplate(_userId);
            ((HtmlEmailTemplatePart)emailTemplate.Parts.Single()).SetHtml(_initialHtml);
            emailTemplate.SetPrivateProperty(x => x.Name, _name);
            emailTemplate.SetPrivateProperty(x => x.Id, _id);
            var htmlPart = emailTemplate.Parts.Single();
            var htmlPartId = NextPartId;
            htmlPart.SetPrivateProperty(x => x.Id, htmlPartId);

            _variables.Each(variable =>
                                {
                                    emailTemplate.CreateVariable(htmlPartId, variable.Item1, variable.Item2);
                                    var count = emailTemplate.Parts.Count();
                                    var variablePart = emailTemplate.Parts.ElementAt(count - 2);
                                    variablePart.SetPrivateProperty(x => x.Id, NextPartId);
                                    htmlPartId = NextPartId;
                                    emailTemplate.Parts.ElementAt(count - 1).SetPrivateProperty(x => x.Id, htmlPartId);
                                });

            return emailTemplate;
        }
    }
}
