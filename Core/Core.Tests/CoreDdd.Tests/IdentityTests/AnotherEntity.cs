using Core.Ddd;

namespace CoreDdd.Tests.IdentityTests
{
    internal class AnotherEntity : Identity<Entity>
    {
        public AnotherEntity()
        {            
        }

        public AnotherEntity(int id)
        {
            _id = id;
        }
    }
}