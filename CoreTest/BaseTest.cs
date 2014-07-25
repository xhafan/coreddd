using CoreDdd.Domain;
using CoreUtils.Extensions;
using Rhino.Mocks;

namespace CoreTest
{
    public abstract class BaseTest
    {
        protected T Stub<T>(params object[] argumentsForConstructor) where T : class
        {
            var stub = Mock<T>(argumentsForConstructor);
            StubEqualityForEntities(stub);
            return stub; // mock is here otherwise you cannot stub properties (bug in RhinoMocks 3.6)
        }

        protected T Mock<T>(params object[] argumentsForConstructor) where T : class
        {
            var mock = MockRepository.GenerateMock<T>(argumentsForConstructor);
            StubEqualityForEntities(mock);
            return mock;
        }

        protected T PartialMock<T>(params object[] argumentsForConstructor) where T : class
        {
            return MockRepository.GeneratePartialMock<T>(argumentsForConstructor);
        }

        protected void StubEqualityForEntities(object obj)
        {
            var type = obj.GetType();
            if (type.IsSubclassOfRawGeneric(typeof(Entity<>)))
            {
                obj.Stub(x => x.Equals(obj)).Return(true);
            }
        }
    }
}
