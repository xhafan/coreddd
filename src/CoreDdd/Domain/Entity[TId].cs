using System;
#if NET40
#else
using System.Reflection;
#endif

namespace CoreDdd.Domain
{
    /// <summary>
    /// Base class for domain entities with an id of a different type than int.
    /// Derive from <see cref="Entity"/> if you need an entity with an id of type int.
    /// </summary>
    public abstract class Entity<TId>
    {
        /// <summary>
        /// Entity id.
        /// </summary>
        public virtual TId Id { get; protected set; }

        /// <summary>
        /// Determines whether two entity instances are equal.
        /// </summary>
        /// <param name="otherObject">the object to compare with the current entity</param>
        /// <returns>true if the other object is an entity of the same type with the same id, otherwise returns false</returns>
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

        /// <summary>
        /// Overrides the hash code.
        /// </summary>
        /// <returns>The default hash code for transient (Id == default(TId)) entities, and Id.GetHashCode() for non-transient entities</returns>
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

        /// <summary>
        /// Entity equality == operator compares entities via overridden <see cref="Equals"/> method.
        /// </summary>
        /// <param name="entityOne">Entity one to compare</param>
        /// <param name="entityTwo">Entity two to compare</param>
        /// <returns>True if entities are equal</returns>
        public static bool operator ==(Entity<TId> entityOne, Entity<TId> entityTwo)
        {
            return Equals(entityOne, entityTwo);
        }

        /// <summary>
        /// Entity inequality == operator compares entities via overridden <see cref="Equals"/> method.
        /// </summary>
        /// <param name="entityOne">Entity one to compare</param>
        /// <param name="entityTwo">Entity two to compare</param>
        /// <returns>True if entities are not equal</returns>
        public static bool operator !=(Entity<TId> entityOne, Entity<TId> entityTwo)
        {
            return !Equals(entityOne, entityTwo);
        }
    }
}
