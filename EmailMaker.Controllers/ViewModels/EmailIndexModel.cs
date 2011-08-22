using System.Collections.Generic;
using EmailMaker.DTO.EmailTemplates;

namespace EmailMaker.Controllers.ViewModels
{
    public class EmailIndexModel
    {
        public IEnumerable<EmailTemplateDetailsDTO> EmailTemplate { get; set; }
    }
}