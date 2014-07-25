using CoreDdd.Nhibernate;

namespace EmailMaker.Dtos.Emails
{
    public class EmailPartDto : IAutoMappedDto
    {
        public int EmailId { get; set; }
        public int PartId { get; set; }
        public PartType PartType { get; set; }
        public string Html { get; set; }
        public string VariableValue { get; set; }

        public bool Equals(EmailPartDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.EmailId == EmailId && other.PartId == PartId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(EmailPartDto)) return false;
            return Equals((EmailPartDto)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (EmailId * 397) ^ PartId;
            }
        }

        public static bool operator ==(EmailPartDto left, EmailPartDto right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(EmailPartDto left, EmailPartDto right)
        {
            return !Equals(left, right);
        }
    }
}