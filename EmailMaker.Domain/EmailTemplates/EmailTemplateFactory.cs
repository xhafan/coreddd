namespace EmailMaker.Domain.EmailTemplates
{
    public static class EmailTemplateFactory
    {
        public static EmailTemplates.EmailTemplate CreateEmailTemplate()
        {
            return new EmailTemplates.EmailTemplate();
        }
    }
}