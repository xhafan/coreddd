namespace Core.Domain
{    
    public abstract class Entity<TId, TEquality> where TEquality : Entity<TId, TEquality>
    {
        public virtual TId Id { get; protected set; }

        public override bool Equals(object anotherObject)
        {
            var equalityObject = anotherObject as TEquality;
            if (ReferenceEquals(equalityObject, null)) return false;
            if (Equals(equalityObject.Id, default(TId)) && Equals(Id, default(TId))) return ReferenceEquals(this, equalityObject);
            return Id.Equals(equalityObject.Id);
        }

        private int? _originalHashCode;
        public override int GetHashCode()
        {
            if (!_originalHashCode.HasValue)
            {
                _originalHashCode = Equals(Id, default(TId)) ? base.GetHashCode() : Id.GetHashCode();
            }
            return _originalHashCode.Value; // hashset/dictionary requires that GetHashCode() returns the same value for the lifetime of the object
        }

        public static bool operator ==(Entity<TId, TEquality> entityOne, Entity<TId, TEquality> entityTwo)
        {
            return Equals(entityOne, entityTwo);
        }

        public static bool operator !=(Entity<TId, TEquality> entityOne, Entity<TId, TEquality> entityTwo)
        {
            return !Equals(entityOne, entityTwo);
        }
    }
}
