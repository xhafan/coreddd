namespace EmailMaker.Web.DTO.EmailTemplate
{
    public class EmailTemplatePartDTO
    {
        public int PartId { get; set; }
        public string Html { get; set; }
        public string VariableValue { get; set; }
    }
}