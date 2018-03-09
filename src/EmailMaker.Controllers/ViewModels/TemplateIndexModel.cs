using System.Collections.Generic;
using EmailMaker.Dtos.EmailTemplates;

namespace EmailMaker.Controllers.ViewModels
{
    public class TemplateIndexModel
    {
        public IEnumerable<EmailTemplateDetailsDto> EmailTemplate { get; set; }
    }
}