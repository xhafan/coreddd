using Core.Domain;

namespace Core.Domain.Tests.IdentityTests
{
    internal class Entity : Identity<Entity>
    {
        public Entity()
        {            
        }
        
        public Entity(int id)
        {
            _id = id;
        }
    }
}