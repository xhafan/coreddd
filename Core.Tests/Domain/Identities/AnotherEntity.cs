using Core.Domain;

namespace Core.Tests.Domain.Identities
{
    internal class AnotherEntity : Identity<Entity>
    {
        public AnotherEntity()
        {            
        }

        public AnotherEntity(int id)
        {
            Id = id;
        }
    }
}