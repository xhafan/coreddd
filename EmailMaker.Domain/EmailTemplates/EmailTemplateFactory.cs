namespace EmailMaker.Domain.EmailTemplates
{
    public static class EmailTemplateFactory
    {
        public static EmailTemplate CreateEmailTemplate()
        {
            return new EmailTemplate();
        }
    }
}