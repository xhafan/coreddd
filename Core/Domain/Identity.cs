namespace Core.Domain
{
    // todo: rename to Entity?
    public abstract class Identity<T> where T : Identity<T>
    {
        public virtual int Id { get; protected set; }

        public override bool Equals(object obj)
        {
            var tobj = obj as T;
            if (ReferenceEquals(tobj, null)) return false;
            if (tobj.Id == default(int) && Id == default(int)) return ReferenceEquals(this, tobj);
            return Id == tobj.Id;
        }

        private int? _originalHashCode;
        public override int GetHashCode()
        {
            if (!_originalHashCode.HasValue)
            {
                _originalHashCode = Id == default(int) ? base.GetHashCode() : Id;
            }
            return _originalHashCode.Value; // hashset/dictionary requires that GetHashCode() returns the same value for the lifetime of the object
        }

        public static bool operator ==(Identity<T> entityOne, Identity<T> entityTwo)
        {
            return Equals(entityOne, entityTwo);
        }

        public static bool operator !=(Identity<T> entityOne, Identity<T> entityTwo)
        {
            return !Equals(entityOne, entityTwo);
        }

    }
}
