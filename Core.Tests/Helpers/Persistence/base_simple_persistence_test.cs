namespace Core.Tests.Helpers.Persistence
{
    public abstract class base_simple_persistence_test : base_persistence_test
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