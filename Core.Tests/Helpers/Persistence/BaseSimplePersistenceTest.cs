namespace Core.Tests.Helpers.Persistence
{
    public abstract class BaseSimplePersistenceTest : BasePersistenceTest
    {
        protected abstract void PersistenceContext();

        protected abstract void PersistenceQuery();

        protected override void Context()
        {
            PersistenceContext();

            Session.Clear();

            PersistenceQuery();
        }
    }
}