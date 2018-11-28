using System;
using CoreDdd.Domain;

namespace IntegrationTestsShared.TestEntities
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