using System.Collections.Generic;

namespace EmailMaker.Domain.EmailTemplates
{
    public abstract class TemplatePartContainer : TemplatePart
    {
        public IList<TemplatePart> Parts { get; private set; }

        protected TemplatePartContainer()
        {
            Parts = new List<TemplatePart>();
        }

//        protected void CreateVariable()
//        {
//            
//        }
    }
}