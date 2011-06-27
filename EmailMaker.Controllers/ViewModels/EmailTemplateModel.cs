using System.Collections.Generic;
using System.Globalization;
using EmailMaker.DTO.EmailTemplates;

namespace EmailMaker.Controllers.ViewModels
{
    public class EmailTemplateModel
    {
        public IEnumerable<EmailTemplateDetailsDTO> EmailTemplate { get; set; }
    }
}