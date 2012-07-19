using Core.Domain;

namespace Core.Tests.Domain.IdentityTests
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