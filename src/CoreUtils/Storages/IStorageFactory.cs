namespace CoreUtils.Storages
{
    public interface IStorageFactory
    {
        IStorage<TData> Create<TData>();
        void Release<TData>(IStorage<TData> storage);
    }
}