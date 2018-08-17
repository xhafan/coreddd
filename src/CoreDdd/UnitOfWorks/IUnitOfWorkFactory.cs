namespace CoreDdd.UnitOfWorks
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
        void Release(IUnitOfWork unitOfWork);
    }
}