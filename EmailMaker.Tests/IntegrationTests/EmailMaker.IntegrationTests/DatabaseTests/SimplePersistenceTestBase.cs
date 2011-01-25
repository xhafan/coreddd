namespace EmailMaker.IntegrationTests.DatabaseTests
{
    public abstract class SimplePersistenceTestBase : PersistenceTestBase
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