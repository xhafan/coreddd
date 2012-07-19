using System.Collections.Generic;
using EmailMaker.Dtos.EmailTemplates;

namespace EmailMaker.Controllers.ViewModels
{
    public class EmailIndexModel
    {
        public IEnumerable<EmailTemplateDetailsDto> EmailTemplate { get; set; }
    }
}