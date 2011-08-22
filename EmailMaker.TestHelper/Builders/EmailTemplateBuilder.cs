using System;
using System.Collections.Generic;
using System.Linq;
using Core.TestHelper.Extensions;
using Core.Utilities.Extensions;
using EmailMaker.Domain.EmailTemplates;

namespace EmailMaker.TestHelper.Builders
{
    public class EmailTemplateBuilder
    {       
        private string _initialHtml = "html";
        private int _nextPartId;
        private IList<Tuple<int, int>> _variables = new List<Tuple<int, int>>();
        private int _id;
        private string _name = "name";

        private int NextPartId
        {
            get
            {
                _nextPartId++;
                return _nextPartId;
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

        public EmailTemplate Build()
        {
            var emailTemplate = new EmailTemplate(_initialHtml, _name);
            emailTemplate.SetPrivateAttribute("_id", _id);
            var htmlPart = emailTemplate.Parts.Single();
            var htmlPartId = NextPartId;
            htmlPart.SetPrivateAttribute("_id", htmlPartId);

            _variables.Each(variable =>
                                {
                                    emailTemplate.CreateVariable(htmlPartId, variable.Item1, variable.Item2);
                                    var count = emailTemplate.Parts.Count();
                                    var variablePart = emailTemplate.Parts.ElementAt(count - 2);
                                    variablePart.SetPrivateAttribute("_id", NextPartId);
                                    htmlPartId = NextPartId;
                                    emailTemplate.Parts.ElementAt(count - 1).SetPrivateAttribute("_id", htmlPartId);
                                });

            return emailTemplate;
        }
    }
}
