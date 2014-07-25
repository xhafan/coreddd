namespace CoreDdd.UnitOfWorks
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
        void Flush();
        bool IsActive();
    }
}