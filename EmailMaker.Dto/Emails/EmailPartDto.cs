using CoreDdd.Dtos;

namespace EmailMaker.Dtos.Emails
{
    public class EmailPartDto : Dto
    {
        public virtual int EmailId { get; set; }
        public virtual int PartId { get; set; }
        public virtual PartType PartType { get; set; }
        public virtual string Html { get; set; }
        public virtual string VariableValue { get; set; }

        public virtual bool Equals(EmailPartDto other)
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