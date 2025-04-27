namespace _Quarantine.Code.Infrastructure.PersistentProgress
{
    public interface ILoadable<TData>
    {
        public void Load(TData data);
    }
}