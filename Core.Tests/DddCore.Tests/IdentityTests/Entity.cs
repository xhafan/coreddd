namespace DddCore.Tests.IdentityTests
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