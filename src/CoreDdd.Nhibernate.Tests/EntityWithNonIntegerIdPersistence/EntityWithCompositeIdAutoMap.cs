using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace CoreDdd.Nhibernate.Tests.EntityWithNonIntegerIdPersistence
{
    public class EntityWithCompositeIdAutoMap : IAutoMappingOverride<EntityWithCompositeId>
    {
        public void Override(AutoMapping<EntityWithCompositeId> mapping)
        {
            mapping.CompositeId(x => x.Id)
                .KeyProperty(x => x.IdOne)
                .KeyProperty(x => x.IdTwo);
        }
    }
}