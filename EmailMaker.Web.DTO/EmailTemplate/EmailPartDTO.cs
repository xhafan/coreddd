namespace EmailMaker.Web.DTO.EmailTemplate
{
    public class EmailPartDTO
    {
        public int EmailPartId { get; set; }
        public string Html { get; set; }
        public string VariableValue { get; set; }
    }
}