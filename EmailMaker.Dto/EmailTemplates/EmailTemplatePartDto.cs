using CoreDto;

namespace EmailMaker.Dtos.EmailTemplates
{
    public class EmailTemplatePartDto : IDto
    {
        public int EmailTemplateId { get; set; }
        public int PartId { get; set; }
        public PartType PartType { get; set; }
        public string Html { get; set; }
        public string VariableValue { get; set; }

        public bool Equals(EmailTemplatePartDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.EmailTemplateId == EmailTemplateId && other.PartId == PartId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (EmailTemplatePartDto)) return false;
            return Equals((EmailTemplatePartDto) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (EmailTemplateId*397) ^ PartId;
            }
        }

        public static bool operator ==(EmailTemplatePartDto left, EmailTemplatePartDto right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(EmailTemplatePartDto left, EmailTemplatePartDto right)
        {
            return !Equals(left, right);
        }
    }
}