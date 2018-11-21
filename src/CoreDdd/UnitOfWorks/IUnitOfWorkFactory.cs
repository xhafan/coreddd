namespace CoreDdd.UnitOfWorks
{
    /// <summary>
    /// A factory to create an unit of work.
    /// Usually implemented auto-magically by an IoC container.
    /// </summary>
    public interface IUnitOfWorkFactory
    {
        /// <summary>
        /// Creates a new unit of work.
        /// </summary>
        /// <returns></returns>
        IUnitOfWork Create();

        /// <summary>
        /// Releases a unit of work instance previously created by <see cref="Create"/> method.
        /// This is needed by Castle Windsor IoC container, other IoC containers (e.g. Ninject, ServiceProvider) don't support it.
        /// </summary>
        /// <param name="unitOfWork">An unit of work instance</param>
        void Release(IUnitOfWork unitOfWork);
    }
}