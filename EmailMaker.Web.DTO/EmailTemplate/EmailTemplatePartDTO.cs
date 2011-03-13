namespace EmailMaker.DTO.EmailTemplate
{
    public class EmailTemplatePartDTO
    {
        public virtual int EmailTemplateId { get; set; }
        public virtual int PartId { get; set; }
        public virtual EmailTemplatePartType EmailTemplatePartType { get; set; }
        public virtual string Html { get; set; }
        public virtual string VariableValue { get; set; }

        public virtual bool Equals(EmailTemplatePartDTO other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.EmailTemplateId == EmailTemplateId && other.PartId == PartId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (EmailTemplatePartDTO)) return false;
            return Equals((EmailTemplatePartDTO) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (EmailTemplateId*397) ^ PartId;
            }
        }

        public static bool operator ==(EmailTemplatePartDTO left, EmailTemplatePartDTO right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(EmailTemplatePartDTO left, EmailTemplatePartDTO right)
        {
            return !Equals(left, right);
        }
    }
}