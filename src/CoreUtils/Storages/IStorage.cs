namespace CoreUtils.Storages
{
    public interface IStorage<TData>
    {
        void Set(TData data);
        TData Get();
    }
}