using System;
using System.Collections.Generic;
using System.Linq;
using Core.TestHelper.Extensions;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.Utilities.Extensions;

namespace EmailMaker.TestHelper.Builders.EmailTemplates
{
    public class EmailTemplateBuilder
    {       
        private string _initialHtml;
        private int _nextPartId;
        private IList<Tuple<int, int>> _variables = new List<Tuple<int, int>>();

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

        public EmailTemplate Build()
        {
            var emailTemplate = new EmailTemplate();
            var htmlPart = emailTemplate.Parts.Single();
            var htmlPartId = htmlPart.Id;
            htmlPartId = NextPartId;
            htmlPart.SetPrivateAttribute("_id", htmlPartId);
            emailTemplate.SetHtml(htmlPartId, _initialHtml);

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
