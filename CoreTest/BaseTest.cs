using Rhino.Mocks;

namespace CoreTest
{
    public abstract class BaseTest
    {
        protected T Stub<T>(params object[] argumentsForConstructor) where T : class
        {
            return Mock<T>(argumentsForConstructor); // mock is here otherwise you cannot stub properties (bug in RhinoMocks 3.6)
        }

        protected T Mock<T>(params object[] argumentsForConstructor) where T : class
        {
            return MockRepository.GenerateMock<T>(argumentsForConstructor);
        }

        protected T PartialMock<T>(params object[] argumentsForConstructor) where T : class
        {
            return MockRepository.GeneratePartialMock<T>(argumentsForConstructor);
        }
    }
}
