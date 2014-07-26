namespace CoreIntegrationTest.Nhibernate
{
    public abstract class BaseNhibernateSimplePersistenceTest : BaseNhibernatePersistenceTest
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