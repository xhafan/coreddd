namespace Core.TestHelper.Persistence
{
    public abstract class BaseSimplePersistenceTest : BasePersistenceTest
    {
        public abstract void PersistenceContext();

        public abstract void PersistenceQuery();

        public override void Context()
        {
            PersistenceContext();

            Session.Clear();

            PersistenceQuery();
        }
    }
}