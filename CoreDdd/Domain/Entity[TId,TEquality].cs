using System;

namespace CoreDdd.Domain
{    
    public abstract class Entity<TId>
    {
        public virtual TId Id { get; protected set; }

        public override bool Equals(object otherObject)
        {
            var other = otherObject as Entity<TId>;
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(other, this)) return true;

            if (!IsTransient(other) && !IsTransient(this) && Id.Equals(other.Id))
            {
                var otherType = other.GetUnproxiedType();
                var thisType = GetUnproxiedType();
                return thisType.IsAssignableFrom(otherType) || otherType.IsAssignableFrom(thisType);
            }
            return false;
        }

        private bool IsTransient(Entity<TId> entity)
        {
            return Equals(entity.Id, default(TId));
        }

        public virtual Type GetUnproxiedType()
        {
            return GetType();
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

        public static bool operator ==(Entity<TId> entityOne, Entity<TId> entityTwo)
        {
            return Equals(entityOne, entityTwo);
        }

        public static bool operator !=(Entity<TId> entityOne, Entity<TId> entityTwo)
        {
            return !Equals(entityOne, entityTwo);
        }
    }
}
