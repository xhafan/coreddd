namespace CoreUtils.Storages
{
    public class Storage<TData> : IStorage<TData>
    {
        private TData _data;

        public void Set(TData data)
        {
            _data = data;
        }

        public TData Get()
        {
            return _data;
        }
    }
}