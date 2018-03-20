using System;
using CoreDdd.Domain;

namespace CoreDdd.Nhibernate.PersistenceTests.Conventions
{
    public class EntityWithDateTime : Entity, IAggregateRoot
    {
        protected EntityWithDateTime() { }

        public EntityWithDateTime(DateTime dateTime)
        {
            DateTime = dateTime;
        }

        public virtual DateTime DateTime { get; protected set; }
    }
}