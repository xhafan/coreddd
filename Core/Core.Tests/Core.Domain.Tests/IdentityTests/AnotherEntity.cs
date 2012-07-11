namespace Core.Domain.Tests.IdentityTests
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