namespace EmailMaker.Dtos.Emails
{
    public class EmailPartDto
    {
        public int EmailId { get; set; }
        public int PartId { get; set; }
        public PartType PartType { get; set; }
        public string Html { get; set; }
        public string VariableValue { get; set; }

        public override bool Equals(object obj)
        {
            return obj is EmailPartDto dto &&
                   EmailId == dto.EmailId &&
                   PartId == dto.PartId;
        }

        public override int GetHashCode()
        {
            var hashCode = 1230688051;
            hashCode = hashCode * -1521134295 + EmailId.GetHashCode();
            hashCode = hashCode * -1521134295 + PartId.GetHashCode();
            return hashCode;
        }
    }
}