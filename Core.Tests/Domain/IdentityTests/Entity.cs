using Core.Domain;

namespace Core.Tests.Domain.IdentityTests
{
    internal class Entity : Identity<Entity>
    {
        public Entity()
        {            
        }
        
        public Entity(int id)
        {
            Id = id;
        }
    }
}