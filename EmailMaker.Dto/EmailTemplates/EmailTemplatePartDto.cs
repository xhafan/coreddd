namespace EmailMaker.Dtos.EmailTemplates
{
    public class EmailTemplatePartDto
    {
        public int EmailTemplateId { get; set; }
        public int PartId { get; set; }
        public PartType PartType { get; set; }
        public string Html { get; set; }
        public string VariableValue { get; set; }

        public override bool Equals(object obj)
        {
            return obj is EmailTemplatePartDto dto &&
                   EmailTemplateId == dto.EmailTemplateId &&
                   PartId == dto.PartId;
        }

        public override int GetHashCode()
        {
            var hashCode = 1296822809;
            hashCode = hashCode * -1521134295 + EmailTemplateId.GetHashCode();
            hashCode = hashCode * -1521134295 + PartId.GetHashCode();
            return hashCode;
        }
    }
}