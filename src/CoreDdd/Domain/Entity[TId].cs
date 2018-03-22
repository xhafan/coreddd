using System;
#if NET40
#else
using System.Reflection;
#endif

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

            if (!_isTransient(other) && !_isTransient(this) && Id.Equals(other.Id))
            {
                var otherType = other.GetUnproxiedType();
                var thisType = GetUnproxiedType();
#if NET40
                return thisType.IsAssignableFrom(otherType)
                       || otherType.IsAssignableFrom(thisType);
#else
                return thisType.GetTypeInfo().IsAssignableFrom(otherType.GetTypeInfo()) 
                       || otherType.GetTypeInfo().IsAssignableFrom(thisType.GetTypeInfo());
#endif
            }
            return false;

            bool _isTransient(Entity<TId> entity)
            {
                return Equals(entity.Id, default(TId));
            }
        }

        protected virtual Type GetUnproxiedType()
        {
            return GetType();
        }  

        private int? _originalHashCode;
        public override int GetHashCode()
        {
            if (!_originalHashCode.HasValue)
            {
                _originalHashCode = Equals(Id, default(TId))
                    ? base.GetHashCode()
                    : Id.GetHashCode();
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
